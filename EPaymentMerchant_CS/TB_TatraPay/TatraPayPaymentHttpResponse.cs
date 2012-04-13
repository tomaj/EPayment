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

namespace Monogram.EPayment.Merchant.TB.TatraPay
{
  /// <summary>
  /// Trieda spracúvajúca príchodzie payment response-y
  /// </summary>
  public class TatraPayPaymentHttpResponse : EPaymentDesSignedMessage, ISignedResponse
  {
    protected TatraPayPaymentResult? res = null;
    /// <summary>
    /// Payment result
    /// </summary>
    public TatraPayPaymentResult? Res
    {
      get { return res; }
    }
    public string ResStr
    {
      get
      {
        switch (res)
        {
          case TatraPayPaymentResult.OK:
            return "OK";
          case TatraPayPaymentResult.FAIL:
            return "FAIL";
          case TatraPayPaymentResult.TOUT:
            return "TOUT";
        }
        throw new ApplicationException("Unknown payment result");
      }
    }

    protected string ss = null;
    /// <summary>
    /// Specificky symbol
    /// </summary>
    public string Ss
    {
      get { return ss; }
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
    /// Konštruktor payment response-u
    /// </summary>
    /// <param name="request">HttpRequest obsahujúci payment response</param>
    public TatraPayPaymentHttpResponse(HttpRequest request)
    {
      vs = request["VS"];
      ss = request["SS"];
      if (request["RES"] == "OK") res = TatraPayPaymentResult.OK;
      if (request["RES"] == "FAIL") res = TatraPayPaymentResult.FAIL;
      if (request["RES"] == "TOUT") res = TatraPayPaymentResult.TOUT;
      receivedSignature = request["SIGN"];
    }

    /// <summary>
    /// Metóda overujúca formát prijatého payment response-u
    /// </summary>
    /// <returns></returns>
    public override bool Validate()
    {
      if (vs == null) return false;
      if (vs.Length > 10) return false;
      if (!(new Regex(@"^[0-9]+$").IsMatch(vs))) return false;

      if (ss != null)
      {
        if (ss.Length > 10) return false;
        if (!(new Regex(@"^[0-9]+$").IsMatch(ss))) return false;
      }

      if (res == null) return false;
      if (res != TatraPayPaymentResult.OK && res != TatraPayPaymentResult.FAIL && res != TatraPayPaymentResult.TOUT) return false;

      if (receivedSignature == null) return false;

      return true;
    }
    
    /// <summary>
    /// Overovanie integrity a autentickosti prijatého payment response-u
    /// </summary>
    /// <param name="sharedSecret">Tajný k¾úè urèený na generovanie podpisu správy</param>
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
    /// Stav overenia autentickosti a integrity payment response-u
    /// </summary>
    public bool IsVerified
    {
      get { return isVerified; }
    }

    /// <summary>
    /// Odtlaèok správy
    /// </summary>
    public override string SignatureBase
    {
      get
      {
        return string.Format("{0}{1}{2}", vs, ss, ResStr);
      }
    }

    /// <summary>
    /// Vracia výsledok platby. V prípade, že payment request ešte nebol overený, vyhodí EPaymentException.
    /// </summary>
    /// <returns>Výsledok platby (OK, Fail alebo Timeout)</returns>
    public PaymentResponse GetPaymentResponse()
    {
      if (!isVerified)
      {
        throw new EPaymentException("Správa nebola verifikovaná.");
      }

      if (res == TatraPayPaymentResult.OK)
      {
        return PaymentResponse.OK;
      }

      if (res == TatraPayPaymentResult.FAIL)
      {
        return PaymentResponse.Fail;
      }

      if (res == TatraPayPaymentResult.TOUT)
      {
        return PaymentResponse.Timeout;
      }

      throw new EPaymentException("Platba má nedefinovaný výsledok.");
    }
  }
}
