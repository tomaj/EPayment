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
using Monogram.EPayment.Merchant;

namespace Monogram.EPayment.Merchant.TB.TatraPay
{
  /// <summary>
  /// Trieda vytv·raj˙ca payment request pre protokol TatraPay
  /// </summary>
  public class TatraPayPaymentRequest : EPaymentDesSignedMessage, IHttpRedirectPaymentRequest, IPaymentRequest
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
    /// Variabiln˝ symbol platby
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
    /// N·vratov· URL, na ktor˙ bude zaslan˝ payment response
    /// </summary>
    public string Rurl
    {
      get { return rurl; }
      set { rurl = value; }
    }
    #endregion

    #region nepovinne parametre
    protected TatraPayPaymentType? pt = TatraPayPaymentType.TatraPay;
    /// <summary>
    /// (nepovinnÈ) Typ platby
    /// </summary>
    public TatraPayPaymentType? Pt
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
          case TatraPayPaymentType.TatraPay:
            return "TatraPay";
          default:
            throw new ApplicationException("Unknown Payment type.");
        }
      }
    }

    protected string ss = null;
    /// <summary>
    /// (nepovinnÈ) äpecifick˝ symbol
    /// </summary>
    public string Ss
    {
      get { return ss; }
      set { ss = value; }
    }

    protected string rsms = null;
    /// <summary>
    /// (nepovinnÈ) »Ìslo, na ktorÈ bude obchodnÌkovi poslan· SMS notifik·cia o realiz·cii platby
    /// </summary>
    public string Rsms
    {
      get { return rsms; }
      set { rsms = value; }
    }

    protected string rem = null;
    /// <summary>
    /// (nepovinnÈ) E-mailova adresa, na ktor˙ bude obchodnÌkovi zaslan· notifik·cia o realiz·cii platby
    /// </summary>
    public string Rem
    {
      get { return rem; }
      set { rem = value; }
    }

    protected string desc = null;
    /// <summary>
    /// (nepovinnÈ) Popis platby
    /// </summary>
    public string Desc
    {
      get { return desc; }
      set { desc = value; }
    }

    protected bool? aredir = null;
    /// <summary>
    /// UmoûÚuje automatickÈ presmerovanie klienta na str·nku obchodnÌka.
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

    protected TatraPayLanguage? lang = null;
    /// <summary>
    /// (nepovinnÈ) Nastavenie jazyka e-bankingovÈho systÈmu pre klienta
    /// </summary>
    public TatraPayLanguage? Lang
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
          case TatraPayLanguage.SK:
            return "sk";
          case TatraPayLanguage.EN:
            return "en";
          case TatraPayLanguage.DE:
            return "de";
          case TatraPayLanguage.HU:
            return "hu";
        }
        throw new ApplicationException("Unknown language.");
      }
    }
    #endregion

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
      
      // nepovinne
      if (ss != null)
      {
        if (!(new Regex(@"^[0-9]{1,10}$").IsMatch(ss))) return false;
      }
      if (pt != null && pt != TatraPayPaymentType.TatraPay) return false;
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
        if (desc.Length > 20) return false;
        // TODO check na diakritiku
      }
      if (lang != null)
      {
        if (lang != TatraPayLanguage.SK && lang != TatraPayLanguage.EN && lang != TatraPayLanguage.DE && lang != TatraPayLanguage.HU)
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
        return String.Format("{0}{1}{2}{3}{4}{5}{6}", mid, AmtStr, CurrStr, vs, ss, cs, rurl);
      }
    }

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
    /// URL obsahuj˙ca payment request.
    /// </summary>
    public string PaymentRequestUrl
    {
      get
      {
        StringBuilder parameters = new StringBuilder();
        
        if (pt != null)
          parameters.AppendFormat("PT={0}", PtStr);

        if (pt == null)
          parameters.AppendFormat("MID={0}", mid);
        else
          parameters.AppendFormat("&MID={0}", mid);

        parameters.AppendFormat("&AMT={0}", AmtStr);
        parameters.AppendFormat("&CURR={0}", CurrStr);
        parameters.AppendFormat("&VS={0}", vs);
        if (ss != null)
          parameters.AppendFormat("&SS={0}", ss);
        parameters.AppendFormat("&CS={0}", cs);
        parameters.AppendFormat("&RURL={0}", HttpUtility.UrlEncode(rurl, Encoding.ASCII));
        parameters.AppendFormat("&SIGN={0}", Signature);

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

        return urlBase + "?" + parameters.ToString();
      }
    }
  }
}