//  Copyright 2009 MONOGRAM Technologies
//  
//  This file is part of MONOGRAM EPayment libraries
//  
//  MONOGRAM EPayment libraries is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MONOGRAM EPayment libraries is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with MONOGRAM EPayment libraries.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace Monogram.EPayment.Merchant.UCB.UniPlatba
{
  /// <summary>
  /// Trieda sprac˙vaj˙ca payment response protokolu UCB UniPlatba
  /// </summary>
  public class UniPlatbaPaymentHttpResponse : EPaymentDesSignedMessage, ISignedResponse
  {
    protected UniPlatbaResponse? res = null;
    /// <summary>
    /// Payment result
    /// </summary>
    public UniPlatbaResponse? Res
    {
      get { return res; }
    }
    public string ResStr
    {
      get
      {
        switch (res)
        {
          case UniPlatbaResponse.NO: return "NO";
          case UniPlatbaResponse.OK: return "OK";
          default: throw new EPaymentException("Unknown payment response code: " + res.ToString());
        }
      }
    }

    protected string receivedSignature = null;
    /// <summary>
    /// Received signature
    /// </summary>
    public string ReceivedSignature
    {
      get { return receivedSignature; }
    }

    /// <summary>
    /// Vytvorenie objektu payment response-u
    /// </summary>
    /// <param name="request">HttpRequest obsahuj˙ci payment response</param>
    public UniPlatbaPaymentHttpResponse(HttpRequest request)
    {
      vs = request["VS"];
      if (request["RES"] == "OK") res = UniPlatbaResponse.OK;
      if (request["RES"] == "NO") res = UniPlatbaResponse.NO;
      receivedSignature = request["SIGN"];
    }

    /// <summary>
    /// MetÛda validuj˙ca payment response
    /// </summary>
    /// <returns></returns>
    public override bool Validate()
    {
      if (string.IsNullOrEmpty(vs)) return false;
      if (!(new Regex(@"^[0-9]{1,10}$")).IsMatch(vs)) return false;

      if (res == null) return false;
      if (res != UniPlatbaResponse.OK && res != UniPlatbaResponse.NO) return false;

      if (string.IsNullOrEmpty(receivedSignature)) return false;
      if (!(new Regex(@"^[0-9A-F]{16}$")).IsMatch(receivedSignature)) return false;

      return true;
    }

    /// <summary>
    /// Verifik·cia integrity a autentickosti payment response-u
    /// </summary>
    /// <param name="sharedSecret">Tajn˝ kæ˙Ë na generovanie podpisov spr·v UCB UniPlatba</param>
    /// <returns></returns>
    public bool VerifySignature(string sharedSecret)
    {
      if (receivedSignature == null)
      {
        throw new ApplicationException("No signature was received.");
      }

      SignMessage(sharedSecret);

      if (signature == null)
      {
        throw new ApplicationException("Verification signature was not calculated yet.");
      }

      isVerified = (signature == receivedSignature);

      return isVerified;
    }

    private bool isVerified = false;
    /// <summary>
    /// Stav verifik·cie prÌchodzieho payment response-u. True ak je spr·va overen·, inak false.
    /// </summary>
    public bool IsVerified
    {
      get { return isVerified; }
    }

    /// <summary>
    /// OdtlaËok prijatej spr·vy
    /// </summary>
    public override string SignatureBase
    {
      get
      {
        return string.Format("{0}{1}", vs, ResStr);
      }
    }

    /// <summary>
    /// Vracia v˝sledok platby. VyhodÌ EPaymentException ak nebola overen· integrita a autentickosù prijatej spr·vy
    /// </summary>
    /// <returns>V˝sledok platby</returns>
    public PaymentResponse GetPaymentResponse()
    {
      if (!isVerified)
      {
        throw new EPaymentException("Spr·va nebola verifikovan·.");
      }

      if (res == UniPlatbaResponse.OK)
      {
        return PaymentResponse.OK;
      }

      if (res == UniPlatbaResponse.NO)
      {
        return PaymentResponse.Fail;
      }

      throw new EPaymentException("Platba m· nedefinovan˝ v˝sledok.");
    }
  }
}
