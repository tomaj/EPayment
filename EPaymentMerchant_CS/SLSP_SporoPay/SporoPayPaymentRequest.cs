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

namespace Monogram.EPayment.Merchant.SLSP.SporoPay
{
  /// <summary>
  /// Trieda payment requestu protokolu SLSP SporoPay
  /// </summary>
  public class SporoPayPaymentRequest : SporoPayMessage, IHttpRedirectPaymentRequest, IPaymentRequest
  {
    #region povinne udaje
    protected string pu_predcislo = null;
    /// <summary>
    /// Predcislo uctu obchodnika
    /// </summary>
    public string Pu_predcislo
    {
      get { return pu_predcislo; }
      set { pu_predcislo = value.PadLeft(6, '0'); }
    }

    protected string pu_cislo = null;
    /// <summary>
    /// Cislo uctu obchodnika
    /// </summary>
    public string Pu_cislo
    {
      get { return pu_cislo; }
      set { pu_cislo = value.PadLeft(10, '0'); }
    }

    protected string pu_kbanky = "0900"; // v sucasnej verzii SporoPay konstanta
    /// <summary>
    /// Kod banky obchodnika
    /// </summary>
    public string Pu_kbanky
    {
      get { return pu_kbanky; }
    }

    protected double suma = 0;
    /// <summary>
    /// Suma na zaplatenie
    /// </summary>
    public double Suma
    {
      get { return suma; }
      set { suma = value; }
    }
    public string StrSuma
    {
      get { return suma.ToString("0.00").Replace(',', '.'); }
    }

    protected ISO4217Currency mena = ISO4217Currency.EUR; // v sucasnej verzii SporoPay konstanta
    /// <summary>
    /// Penazna mena (v ktorej je zadana suma)
    /// </summary>
    public ISO4217Currency Mena
    {
      get { return mena; }
    }
    public string StrMena
    {
      get { return new ISO4217CurrencyDetail(mena).StrCode; }
    }
    
    protected string ss = null;
    /// <summary>
    /// Specificky symbol
    /// </summary>
    public string Ss
    {
      get { return ss; }
      set { ss = value.PadLeft(10, '0'); }
    }
    
    /// <summary>
    /// Variabiln˝ symbol
    /// </summary>
    public string Vs
    {
      get { return vs; }
      set { vs = value.PadLeft(10, '0'); }
    }

    protected string param = null;
    /// <summary>
    /// Vlastny parameter, pouzity na lubovolny ucel na strane obchodnika
    /// </summary>
    public string Param
    {
      get { return param; }
      set { param = value; }
    }
    
    protected string url = null;
    /// <summary>
    /// Navratova URL
    /// </summary>
    public string Url
    {
      get { return url; }
      set { url = value; }
    }
    #endregion

    #region nepovinne udaje
    protected string acc_prefix = null;
    /// <summary>
    /// (nepovinne) Predcislo uctu klienta
    /// </summary>
    public string Acc_prefix
    {
      get { return acc_prefix; }
      set { acc_prefix = value; }
    }

    protected string acc_number = null;
    /// <summary>
    /// (nepovinne) Cislo uctu klienta
    /// </summary>
    public string Acc_number
    {
      get { return acc_number; }
      set { acc_number = value; }
    }

    protected SporoPayClientNotification? mail_notify_att = null;
    /// <summary>
    /// (nepovinne) Nastavenie notifikacie klienta
    /// </summary>
    public SporoPayClientNotification? Mail_notify_att
    {
      get { return mail_notify_att; }
      set { mail_notify_att = value; }
    }

    protected string email_adr = null;
    /// <summary>
    /// (nepovinne) E-mailova adresa klienta, ktoru ma byt zaslana notifikacia pre Klienta
    /// </summary>
    public string Email_adr
    {
      get { return email_adr; }
      set { email_adr = value; }
    }

    protected string client_login = null;
    /// <summary>
    /// (nepovinne) Identifikacne cislo klienta
    /// </summary>
    public string Client_login
    {
      get { return client_login; }
      set { client_login = value; }
    }
    
    protected SporoPayAuthorizationType? auth_tool_type = null;
    /// <summary>
    /// (nepovinne) Autorizacny predmet klienta
    /// </summary>
    public SporoPayAuthorizationType? Auth_tool_type
    {
      get { return auth_tool_type; }
      set { auth_tool_type = value; }
    }
    #endregion
    
    /// <summary>
    /// URL, na ktor˙ m· byù v produkËnej verzii aplik·cie odoslan˝ payment request
    /// </summary>
    public const string DefaultUrlBase = "https://ib.slsp.sk/epayment/epayment/epayment.xml";
    protected string urlBase = DefaultUrlBase;
    /// <summary>
    /// URL, na ktor˙ bude odoslan˝ payment request, t˙to hodnotu je moûnÈ zmeniù na URL simul·tora, ktor˝m je moûnÈ testovaù fin·lnu aplik·ciu
    /// </summary>
    public string UrlBase
    {
      get { return urlBase; }
      set { urlBase = value; }
    }

    /// <summary>
    /// Zaklad na generovanie podpisu udajov.
    /// </summary>
    public override string SignatureBase
    {
      get
      {
        return String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}", pu_predcislo, pu_cislo, pu_kbanky, StrSuma, StrMena, vs, ss, url, param );
      }
    }

    /// <summary>
    /// Validacia udajov.
    /// </summary>
    /// <returns></returns>
    public override bool Validate()
    {
      if (pu_predcislo == null) pu_predcislo = String.Empty;
      if (!(new Regex("^[0-9]{6}$").IsMatch(pu_predcislo))) return false;
      if (pu_cislo == null) return false;
      if (!(new Regex("^[0-9]+$").IsMatch(pu_cislo))) return false;
      if (pu_kbanky != "0900") return false; // v sucasnej verzii SporoPay konstanta
      if (suma < 0) return false;
      if (mena != ISO4217Currency.EUR) return false;
      if (vs == null) return false;
      if (!(new Regex("^[0-9]{10}$").IsMatch(vs))) return false;
      if (ss == null) return false;
      if (!(new Regex("^[0-9]{10}$").IsMatch(ss))) return false;

      char[] restrictedUrlChars = {';', '?', '&'};
      if (url == null) return false;
      if (url.IndexOfAny(restrictedUrlChars) != -1) return false;
      if (!(new Regex(@"^https?\://.+$", RegexOptions.IgnoreCase).IsMatch(url))) return false;

      if (param == null) param = String.Empty;
      if (param.IndexOfAny(restrictedUrlChars) != -1) return false;

      return true;
    }

    /// <summary>
    /// V˝sledn· URL s payment requestom
    /// </summary>
    public string PaymentRequestUrl
    {
      get
      {
        StringBuilder result = new StringBuilder(String.Format("{0}?pu_predcislo={1}&pu_cislo={2}&pu_kbanky={3}&suma={4}&mena={5}&vs={6}&ss={7}&url={8}&param={9}&sign1={10}",
        urlBase, Pu_predcislo, Pu_cislo, Pu_kbanky, StrSuma, StrMena, Vs, Ss, HttpUtility.UrlEncode(Url), HttpUtility.UrlEncode(Param, Encoding.ASCII), HttpUtility.UrlEncode(Signature)));

        if (acc_prefix != null)
          result.AppendFormat("&acc_prefix={0}", acc_prefix);
        if (acc_number != null)
          result.AppendFormat("&acc_number={0}", acc_number);
        if (mail_notify_att != null)
          result.AppendFormat("&mail_notify_att={0}", (int)mail_notify_att);
        if (email_adr != null)
          result.AppendFormat("&email_adr={0}", HttpUtility.UrlEncode(email_adr));
        if (client_login != null)
          result.AppendFormat("&client_login={0}", HttpUtility.UrlEncode(client_login));
        if (auth_tool_type != null)
          result.AppendFormat("&auth_tool_type={0}", (int)auth_tool_type);

        return result.ToString();
      }
    }
  }
}