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

namespace Monogram.EPayment.Merchant.VUB.EPlatba
{
  /// <summary>
  /// Trieda na sprac�vanie pr�chodz�ch payment response-ov V�B EPlatby
  /// </summary>
  public class EPlatbaPaymentHttpResponse : EPaymentDesSignedMessage, ISignedResponse
  {
    protected string ss = null;
    /// <summary>
    /// �pecifick� symbol
    /// </summary>
    public string Ss
    {
      get { return ss; }
    }

    protected EPlatbaPaymentResult? res = null;
    /// <summary>
    /// V�sledok platby
    /// </summary>
    public EPlatbaPaymentResult Res
    {
      get { return (EPlatbaPaymentResult)res; }
    }
    public string StrRes
    {
      get
      {
        if (res == EPlatbaPaymentResult.OK) { return "OK"; }
        if (res == EPlatbaPaymentResult.FAIL) { return "FAIL"; }
        throw new ApplicationException("Result is not set.");
      }
    }

    protected string receivedSignature = null;
    /// <summary>
    /// Prijat� podpis spr�vy
    /// </summary>
    public string ReceivedSignature
    {
      get { return receivedSignature; }
    }

    /// <summary>
    /// Odtla�ok spr�vy
    /// </summary>
    public override string SignatureBase
    {
      get { return String.Format("{0}{1}{2}", vs, ((ss == null) ? "" : ss), StrRes); }
    }

    /// <summary>
    /// Kon�truktor payment response-u
    /// </summary>
    /// <param name="request">HttpRequest obsahuj�ci payment response</param>
    public EPlatbaPaymentHttpResponse(System.Web.HttpRequest request)
    {
      vs = request["VS"];
      ss = request["SS"];
      if (request["RES"] == "FAIL") res = EPlatbaPaymentResult.FAIL;
      if (request["RES"] == "OK") res = EPlatbaPaymentResult.OK;
      receivedSignature = request["SIGN"];
    }

    /// <summary>
    /// Met�da validuj�ca formu prich�dzaj�cej spr�vy
    /// </summary>
    /// <returns>True ak m� spr�va val�dny form�t, inak false</returns>
    public override bool Validate()
    {
      if (vs == null) return false;
      if (vs.Length > 10) return false;
      if (res == null) return false;
      if (receivedSignature == null) return false;

      return true;
    }
    
    /// <summary>
    /// Overenie integrity a autentickosti payment response-u
    /// </summary>
    /// <param name="PWD"></param>
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
    /// Stav verifikovania autentickosti a integrity payment response-u
    /// </summary>
    public bool IsVerified
    {
      get { return isVerified; }
    }

    /// <summary>
    /// Vracia v�sledok platby. Ak payment response e�te nebol verifikovan�, vyhod� EPaymentException
    /// </summary>
    /// <returns>V�sledok platby (iba OK alebo Fail)</returns>
    public PaymentResponse GetPaymentResponse()
    {
      if (!isVerified)
      {
        throw new EPaymentException("Spr�va nebola verifikovan�.");
      }

      if (res == EPlatbaPaymentResult.OK)
      {
        return PaymentResponse.OK;
      }

      if (res == EPlatbaPaymentResult.FAIL)
      {
        return PaymentResponse.Fail;
      }

      throw new EPaymentException("Platba m� nedefinovan� v�sledok.");
    }
  }
}