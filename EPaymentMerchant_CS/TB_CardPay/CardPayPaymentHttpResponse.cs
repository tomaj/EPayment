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

namespace Monogram.EPayment.Merchant.TB.CardPay
{
  /// <summary>
  /// Trieda sprac˙vaj˙ca payment response protokolu TB CardPay
  /// </summary>
  public class CardPayPaymentHttpResponse : EPaymentDesSignedMessage, ISignedResponse
  {
    protected CardPayPaymentResult? res = null;
    /// <summary>
    /// Payment result
    /// </summary>
    public CardPayPaymentResult? Res
    {
      get { return res; }
    }
    public string ResStr
    {
      get
      {
        switch (res)
        {
          case CardPayPaymentResult.OK:
            return "OK";
          case CardPayPaymentResult.FAIL:
            return "FAIL";
          case CardPayPaymentResult.TOUT:
            return "TOUT";
        }
        throw new ApplicationException("Unknown payment result");
      }
    }
    
    protected string ac = null;
    /// <summary>
    /// Approval code
    /// </summary>
    public string Ac
    {
      get
      {
        if (ac == null)
          return string.Empty;
        return ac;
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
    /// Konötruktor payment response-u
    /// </summary>
    /// <param name="request">HttpRequest, obsahuj˙ci payment response</param>
    public CardPayPaymentHttpResponse(HttpRequest request)
    {
      vs = request["VS"];
      ac = request["AC"];
      if (request["RES"] == "OK") res = CardPayPaymentResult.OK;
      if (request["RES"] == "FAIL") res = CardPayPaymentResult.FAIL;
      if (request["RES"] == "TOUT") res = CardPayPaymentResult.TOUT;
      receivedSignature = request["SIGN"];
    }

    /// <summary>
    /// MetÛda validuj˙ca payment response
    /// </summary>
    /// <returns></returns>
    public override bool Validate()
    {
      if (vs == null) return false;
      if (vs.Length > 10) return false;
      if (!(new Regex(@"^[0-9]+$").IsMatch(vs))) return false;

      if (res == null) return false;
      if (res != CardPayPaymentResult.OK && res != CardPayPaymentResult.FAIL && res != CardPayPaymentResult.TOUT) return false;

      if (receivedSignature == null) return false;

      return true;
    }

    /// <summary>
    /// Verifik·cia integrity a autentickosti payment response-u
    /// </summary>
    /// <param name="sharedSecret">Tajn˝ kæ˙Ë na generovanie podpisov spr·v TB CardPay</param>
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

      isVerified = signature == receivedSignature;

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
        return string.Format("{0}{1}{2}", vs, ResStr, ac);
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

      if (res == CardPayPaymentResult.OK)
      {
        return PaymentResponse.OK;
      }

      if (res == CardPayPaymentResult.FAIL)
      {
        return PaymentResponse.Fail;
      }

      if (res == CardPayPaymentResult.TOUT)
      {
        return PaymentResponse.Timeout;
      }

      throw new EPaymentException("Platba m· nedefinovan˝ v˝sledok.");
    }
  }
}
