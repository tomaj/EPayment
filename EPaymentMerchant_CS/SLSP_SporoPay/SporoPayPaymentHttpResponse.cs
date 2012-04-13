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
using System.Text.RegularExpressions;
using System.Globalization;
using Monogram.EPayment.Merchant;

namespace Monogram.EPayment.Merchant.SLSP.SporoPay
{
  /// <summary>
  /// Trieda spracúvajúca príchodzie payment response-y SLSP SporoPay
  /// </summary>
  public class SporoPayPaymentHttpResponse : SporoPayMessage, ISignedResponse
  {
    protected string u_predcislo;
    /// <summary>
    /// Predcislo uctu klienta
    /// </summary>
    public string U_predcislo
    {
      get { return u_predcislo; }
    }

    protected string u_cislo;
    /// <summary>
    /// Cislo uctu klienta
    /// </summary>
    public string U_cislo
    {
      get { return u_cislo; }
    }

    protected string u_kbanky;
    /// <summary>
    /// Kod banky klienta
    /// </summary>
    public string U_kbanky
    {
      get { return u_kbanky; }
    }

    protected string pu_predcislo;
    /// <summary>
    /// Predcislo uctu obchodnika
    /// </summary>
    public string Pu_predcislo
    {
      get { return pu_predcislo; }
    }
    
    protected string pu_cislo;
    /// <summary>
    /// Cislo uctu obchodnika
    /// </summary>
    public string Pu_cislo
    {
      get { return pu_cislo; }
    }

    protected string pu_kbanky;
    /// <summary>
    /// Kod banky obchodnika
    /// </summary>
    public string Pu_kbanky
    {
      get { return pu_kbanky; }
    }

    protected double suma;
    /// <summary>
    /// Suma
    /// </summary>
    public double Suma
    {
      get { return suma; }
    }
    public string StrSuma
    {
      get { return suma.ToString("0.00").Replace(',', '.'); }
    }

    protected ISO4217Currency mena;
    /// <summary>
    /// Penazna mena (v ktorej je suma)
    /// </summary>
    public ISO4217Currency Mena
    {
      get { return mena; }
    }
    public string StrMena
    {
      get { return new ISO4217CurrencyDetail(mena).StrCode; }      
    }

    protected string ss;
    /// <summary>
    /// Specificky symbol
    /// </summary>
    public string Ss
    {
      get { return ss; }
    }

    protected string url;
    /// <summary>
    /// Navratova URL obchodnika
    /// </summary>
    public string Url
    {
      get { return url; }
    }

    protected string param;
    /// <summary>
    /// Parameter internetoveho obchodnika
    /// </summary>
    public string Param
    {
      get { return param; }
    }

    protected SporoPayPaymentResult result;
    /// <summary>
    /// Vysledok transakcie
    /// </summary>
    public SporoPayPaymentResult Result
    {
      get { return result; }
    }
    public string StrResult
    {
      get
      {
        switch (result)
        {
          case SporoPayPaymentResult.OK: return "OK";
          default:
          case SporoPayPaymentResult.NOK: return "NOK";
        }
      }
    }

    protected SporoPayPaymentRealResult real;
    /// <summary>
    /// Skutocny vysledok transakcie
    /// </summary>
    public SporoPayPaymentRealResult Real
    {
      get { return real; }
    }
    public string StrReal
    {
      get
      {
        switch (real)
        {
          case SporoPayPaymentRealResult.OK: return "OK";
          default:
          case SporoPayPaymentRealResult.NOK: return "NOK";
        }
      }
    }

    protected string receivedSignature;
    /// <summary>
    /// Podpis prislej spravy
    /// </summary>
    public string ReceivedSignature
    {
      get { return receivedSignature; }
    }

    /// <summary>
    /// Odtlaèok správy
    /// </summary>
    public override string SignatureBase
    {
      get
      {
        return String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13}", u_predcislo, u_cislo, u_kbanky, pu_predcislo, pu_cislo, pu_kbanky,
                             StrSuma, StrMena, vs, ss, url, param, StrResult, StrReal);
      }
    }

    /// <summary>
    /// Metóda na validovanie údajov v správe
    /// </summary>
    /// <returns></returns>
    public override bool Validate()
    {
      if (u_predcislo == null) u_predcislo = String.Empty;
      if (!(new Regex("^[0-9]+$").IsMatch(u_predcislo))) return false;
      if (u_cislo == null) return false;
      if (!(new Regex("^[0-9]+$").IsMatch(u_cislo))) return false;
      if (u_kbanky == null) return false;
      if (!(new Regex("^[0-9]{4}$").IsMatch(u_kbanky))) return false;
      if (pu_predcislo == null) pu_predcislo = String.Empty;
      if (!(new Regex("^[0-9]+$").IsMatch(pu_predcislo))) return false;
      if (pu_cislo == null) return false;
      if (!(new Regex("^[0-9]+$").IsMatch(pu_cislo))) return false;
      if (pu_kbanky == null) return false;
      if (!(new Regex("^[0-9]{4}$").IsMatch(pu_kbanky))) return false;
      if (suma < 0) return false;
      if (mena != ISO4217Currency.EUR) return false;
      if (vs == null) return false;
      if (!(new Regex("^[0-9]{10}$").IsMatch(vs))) return false;
      if (ss == null) return false;
      if (!(new Regex("^[0-9]{10}$").IsMatch(ss))) return false;
      if (url == null) return false;
      char[] restrictedUrlChars = { ';', '?', '&' };
      if (url.IndexOfAny(restrictedUrlChars) != -1) return false;
      if (!(new Regex("^https?://.+$", RegexOptions.IgnoreCase).IsMatch(url))) return false;
      if (param != null)
        if (param.IndexOfAny(restrictedUrlChars) != -1) return false;
      //if (result == null) return false;
      //if (real == null) return false;

      return true;
    }
    
    /// <summary>
    /// Konštruktor SLSP SporoPay payment response-u
    /// </summary>
    /// <param name="request">HttpRequest, ktorý obsahuje payment response</param>
    public SporoPayPaymentHttpResponse(System.Web.HttpRequest request)
    {
      u_predcislo = request["u_predcislo"];
      u_cislo = request["u_cislo"];
      u_kbanky = request["u_kbanky"];
      pu_predcislo = request["pu_predcislo"];
      pu_cislo = request["pu_cislo"];
      pu_kbanky = request["pu_kbanky"];
      suma = double.Parse(request["suma"], new CultureInfo("en-GB"));
      mena = ISO4217CurrencyDetail.GetCurrencyFromStrCode(request["mena"]);
      vs = request["vs"];
      ss = request["ss"];
      param = request["param"];
      url = request["url"];
      
      if (request["result"] == "OK")
        result = SporoPayPaymentResult.OK;
      else if (request["result"] == "NOK")
        result = SporoPayPaymentResult.NOK;

      if (request["real"] == "OK")
        real = SporoPayPaymentRealResult.OK;
      else if (request["real"] == "NOK")
        real = SporoPayPaymentRealResult.NOK;

      receivedSignature = request["SIGN2"];
    }

    /// <summary>
    /// Overenie podpisu správy
    /// </summary>
    /// <param name="sharedSecret">Heslo internetoveho obchodnika</param>
    /// <returns>Pravda ak sprava koresponduje s podpisom</returns>
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
    /// True ak bola príchodzia správa verifikovaná. (Metódou VerifySignature)
    /// </summary>
    public bool IsVerified
    {
      get { return isVerified; }
    }

    /// <summary>
    /// Výsledok správy
    /// </summary>
    /// <returns>PaymentResponse .OK .Fail .Timeout</returns>
    public PaymentResponse GetPaymentResponse()
    {
      if (!isVerified)
      {
        throw new EPaymentException("Správa nebola verifikovaná.");
      }

      if (result == SporoPayPaymentResult.OK && real == SporoPayPaymentRealResult.OK)
      {
        return PaymentResponse.OK;
      }

      if (result == SporoPayPaymentResult.OK && real == SporoPayPaymentRealResult.NOK)
      {
        return PaymentResponse.Timeout;
      }

      if (result == SporoPayPaymentResult.NOK && real == SporoPayPaymentRealResult.NOK)
      {
        return PaymentResponse.Fail;
      }

      throw new EPaymentException("Platba má nedefinovaný výsledok.");
    }
  }
}
