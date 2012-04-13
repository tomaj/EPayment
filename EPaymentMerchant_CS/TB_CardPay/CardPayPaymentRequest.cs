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
using System.Web;

namespace Monogram.EPayment.Merchant.TB.CardPay
{
  /// <summary>
  /// Payment request protokolu TB CardPay
  /// </summary>
  public class CardPayPaymentRequest : EPaymentDesSignedMessage, IHttpRedirectPaymentRequest, IPaymentRequest
  {
    #region povinne parametre
    protected string mid = null;
    /// <summary>
    /// Merchant ID
    /// </summary>
    public string Mid
    {
      get { return mid; }
      set { mid = value; }
    }

    /// <summary>
    /// Variabiln˝ symbol - jednoznaËn˝ identifik·tor platby
    /// </summary>
    public string Vs
    {
      get { return vs; }
      set { vs = value; }
    }

    protected double amt = 0;
    /// <summary>
    /// Suma
    /// </summary>
    public double Amt
    {
      get { return amt; }
      set { amt = value; }
    }
    public string AmtStr
    {
      get { return amt.ToString("0.00").Replace(',', '.'); }
    }

    protected ISO4217Currency curr = ISO4217Currency.EUR;
    /// <summary>
    /// Mena
    /// </summary>
    public ISO4217Currency Curr
    {
      get { return curr; }
      set { curr = value; }
    }
    public string CurrStr
    {
      get { return new ISO4217CurrencyDetail(curr).NumCode.ToString(); }
    }

    protected string cs = null;
    /// <summary>
    /// Konötantn˝ symbol
    /// </summary>
    public string Cs
    {
      get { return cs; }
      set { cs = value; }
    }

    protected string rurl = null;
    /// <summary>
    /// N·vratov· URL, na ktor˙ bude poslan˝ payment response
    /// </summary>
    public string Rurl
    {
      get { return rurl; }
      set { rurl = value; }
    }

    protected string ipc = null;
    /// <summary>
    /// IP adresa klienta
    /// </summary>
    public string Ipc
    {
      get { return ipc; }
      set { ipc = value; }
    }

    protected string name = null;
    /// <summary>
    /// Meno klienta
    /// </summary>
    public string Name
    {
      get { return name; }
      set { name = value; }
    }
    #endregion

    #region nepovinne parametre
    protected CardPayPaymentType? pt = CardPayPaymentType.CardPay;
    /// <summary>
    /// (nepovinnÈ) Typ platby
    /// </summary>
    public CardPayPaymentType? Pt
    {
      get { return pt; }
      set { pt = value; }
    }
    public string PtStr
    {
      get
      {
        switch (pt)
        {
          case CardPayPaymentType.CardPay:
            return "CardPay";
          default:
            throw new ApplicationException("Unknown Payment type.");
        }
      }
    }

    protected string rsms = null;
    /// <summary>
    /// »Ìslo, na ktorÈ bude zaslan· notifikaËn· SMS pre obchodnÌka
    /// </summary>
    public string Rsms
    {
      get { return rsms; }
      set { rsms = value; }
    }

    protected string rem = null;
    /// <summary>
    /// E-mailov· adresa, na ktor˙ bude zaslan˝ notifikaËn˝ e-mail pre obchodnÌka
    /// </summary>
    public string Rem
    {
      get { return rem; }
      set { rem = value; }
    }

    protected string desc = null;
    /// <summary>
    /// Popis platby
    /// </summary>
    public string Desc
    {
      get { return desc; }
      set { desc = value; }
    }

    protected bool? aredir = null;
    /// <summary>
    /// UmoûÚuje automatickÈ presmerovanie z·kaznÌka na str·nku obchodnÌka
    /// </summary>
    public bool? Aredir
    {
      get { return aredir; }
      set { aredir = value; }
    }
    public string AredirStr
    {
      get { return (bool)aredir ? "1" : "0"; }
    }

    protected CardPayLanguage? lang = null;
    /// <summary>
    /// (nepovinnÈ) Nastavenie jazyka e-bankingovÈho systÈmu pre klienta
    /// </summary>
    public CardPayLanguage? Lang
    {
      get { return lang; }
      set { lang = value; }
    }
    public string LangStr
    {
      get
      {
        switch (lang)
        {
          case CardPayLanguage.SK:
            return "sk";
          case CardPayLanguage.EN:
            return "en";
          case CardPayLanguage.DE:
            return "de";
          case CardPayLanguage.HU:
            return "hu";
        }
        throw new ApplicationException("Unknown language.");
      }
    }
    #endregion
    
    /// <summary>
    /// URL, na ktor˙ maj˙ byù zasielanÈ payment requesty v produkËnom nasadenÌ aplik·cie
    /// </summary>
    public const string DefaultUrlBase = "https://moja.tatrabanka.sk/cgi-bin/e-commerce/start/e-commerce.jsp";
    protected string urlBase = DefaultUrlBase;
    /// <summary>
    /// URL, na ktor˙ bude zaslan˝ payment request. T˙to hodnotu je moûnÈ zmeniù na URL EPayment simul·tora a tak testovaù v˝sledn˙ aplik·ciu.
    /// </summary>
    public string UrlBase
    {
      get { return urlBase; }
      set { urlBase = value; }
    }
    
    /// <summary>
    /// MetÛda urËen· na valid·ciu payment requestu
    /// </summary>
    /// <returns>True ak je form·t spr·vy valÌdny, inak false</returns>
    public override bool Validate()
    {
      // povinne
      if (mid == null) return false;
      if (!(new Regex(@"^[0-9A-Z]{3,4}$", RegexOptions.IgnoreCase).IsMatch(mid))) return false;
      if (amt < 0) return false;
      if (!(new Regex(@"^[0-9]+(\.[0-9]+)?$", RegexOptions.IgnoreCase).IsMatch(AmtStr))) return false;
      if (AmtStr.Length > 13+2) return false;
      if (vs == null) return false;
      if (vs.Length > 10) return false;
      if (!(new Regex(@"^[0-9]+$").IsMatch(vs))) return false;
      if (cs == null) return false;
      if (cs.Length > 4) return false;
      if (!(new Regex(@"^[0-9]+$").IsMatch(cs))) return false;
      if (rurl == null) return false;
      char[] rurlRestrictedChars = { '&', '?', ';', '=', '+', '%' };
      if (rurl.IndexOfAny(rurlRestrictedChars) > -1) return false;
      //if (!(new Regex(@"^https?://[a-z0-9]+(\.[a-z0-9]+)(/|(/[a-z0-9\._-\(\)~])+)?").IsMatch(rurl))) return false;
      if (ipc == null) return false;
      // TODO check na ipv4 a ipv6 adresy
      
      // nepovinne
      if (pt != null && pt != CardPayPaymentType.CardPay) return false;
      if (rsms != null)
      {
        if (!(new Regex(@"^(0|\+421)9[0-9]{2}( ?[0-9]{3}){2}?$").IsMatch(rsms))) return false;
      }
      if (rem != null)
      {
        if (!(new Regex(@"^[0-9a-z_]+(\.[0-9a-z_]+)*@([12]?[0-9]{0,2}(\.[12]?[0-9]{0,2}){3}|([a-z][0-9a-z\-]*\.)+[a-z]{2,6})$", RegexOptions.IgnoreCase).IsMatch(rem))) return false;
      }
      if (desc != null)
      {
        // TODO check na diakritiku
      }
      if (lang != null)
      {
        if (lang != CardPayLanguage.SK && lang != CardPayLanguage.EN && lang != CardPayLanguage.DE && lang != CardPayLanguage.HU)
          return false;
      }

      return true;
    }

    /// <summary>
    /// OdtlaËok payment requestu
    /// </summary>
    public override string SignatureBase
    {
      get
      {
        return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", mid, AmtStr, CurrStr, vs, cs, rurl, ipc, name);
      }
    }

    /// <summary>
    /// URL obsahuj˙ca payment request.
    /// </summary>
    public string PaymentRequestUrl
    {
      get
      {
        StringBuilder parameters = new StringBuilder();
        
        if (pt != null)
          parameters.AppendFormat("PT={0}&", PtStr);

        parameters.AppendFormat("MID={0}", mid);
        
        parameters.AppendFormat("&AMT={0}", AmtStr);
        parameters.AppendFormat("&CURR={0}", CurrStr);
        parameters.AppendFormat("&VS={0}", vs);
        parameters.AppendFormat("&CS={0}", cs);
        parameters.AppendFormat("&RURL={0}", HttpUtility.UrlEncode(rurl, Encoding.ASCII));
        parameters.AppendFormat("&IPC={0}", HttpUtility.UrlEncode(ipc, Encoding.ASCII));
        parameters.AppendFormat("&NAME={0}", HttpUtility.UrlEncode(name, Encoding.ASCII));

        if (rsms != null)
          parameters.AppendFormat("&RSMS={0}", HttpUtility.UrlEncode(rsms));

        if (rem != null)
          parameters.AppendFormat("&REM={0}", HttpUtility.UrlEncode(rem));

        if (desc != null)
          parameters.AppendFormat("&DESC={0}", HttpUtility.UrlEncode(desc, Encoding.ASCII));

        if (aredir != null)
          parameters.AppendFormat("&AREDIR={0}", AredirStr);

        if (lang != null)
          parameters.AppendFormat("&LANG={0}", LangStr);

        parameters.AppendFormat("&SIGN={0}", Signature);

        return urlBase + "?" + parameters.ToString();
      }
    }
  }
}