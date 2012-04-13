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

namespace Monogram.EPayment.Merchant.UCB.UniPlatba
{
  // Payment request protokolu UCB UniPlatba
  public class UniPlatbaPaymentRequest : EPaymentDesSignedMessage, IHttpRedirectPaymentRequest, IPaymentRequest
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

    protected UniPlatbaPaymentLanguage lng = UniPlatbaPaymentLanguage.SK;
    /// <summary>
    /// Jazyk
    /// </summary>
    public UniPlatbaPaymentLanguage Lng
    {
      get { return lng; }
      set { lng = value; }
    }
    public string LngStr
    {
      get
      {
        switch (lng)
        {
          case UniPlatbaPaymentLanguage.EN: return "EN";
          case UniPlatbaPaymentLanguage.SK: return "SK";
          default:
            throw new EPaymentException("Unknown language: " + lng.ToString());
        }
      }
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

    /// <summary>
    /// Variabiln˝ symbol - jednoznaËn˝ identifik·tor platby
    /// </summary>
    public string Vs
    {
      get { return vs; }
      set { vs = value; }
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
    #endregion

    #region nepovinne parametre

    protected string ss = null;
    /// <summary>
    /// äpecifick˝ symbol
    /// </summary>
    public string Ss
    {
      get { return ss; }
      set { ss = value; }
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
    #endregion

    /// <summary>
    /// URL, na ktor˙ maj˙ byù zasielanÈ payment requesty v produkËnom nasadenÌ aplik·cie
    /// </summary>
    public const string DefaultUrlBase = "https://sk.unicreditbanking.net/disp?restart=true&link=login.tplogin.system_login";
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
      if (!(new Regex(@"^[0-9]{1,10}$", RegexOptions.IgnoreCase).IsMatch(mid))) return false;
      if (amt < 0) return false;
      if (lng != UniPlatbaPaymentLanguage.EN && lng != UniPlatbaPaymentLanguage.SK) return false;
      if (!(new Regex(@"^[0-9]{1,13}\.[0-9]{2}$", RegexOptions.IgnoreCase).IsMatch(AmtStr))) return false;
      if (vs == null) return false;
      if (vs.Length > 10) return false;
      if (!(new Regex(@"^[0-9]+$").IsMatch(vs))) return false;
      if (cs == null) return false;
      if (cs.Length != 4) return false;
      if (!(new Regex(@"^[0-9]+$").IsMatch(cs))) return false;
      
      // nepovinne
      if (ss != null)
      {
        if (ss.Length > 10) return false;
        if (!(new Regex(@"^[0-9]+$").IsMatch(ss))) return false;
      }
      if (desc != null)
      {
        if (desc.IndexOfAny(new char[] { ' ', '\t', '\r', '\n' }) >= 0) return false;
        if (desc.Length > 35) return false;
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
        StringBuilder sb = new StringBuilder(String.Format("{0}{1}{2}{3}{4}", mid, LngStr, AmtStr, vs, cs));
        if (ss != null) sb.Append(ss);
        if (desc != null) sb.Append(desc);
        return sb.ToString();
      }
    }

    /// <summary>
    /// URL obsahuj˙ca payment request.
    /// </summary>
    public string PaymentRequestUrl
    {
      get
      {
        StringBuilder result = new StringBuilder(urlBase);
        if (urlBase.Contains("?"))
        {
          result.Append('&');
        }
        else
        {
          result.Append('?');
        }

        result.AppendFormat("MID={0}", mid);
        result.AppendFormat("&LNG={0}", LngStr);
        result.AppendFormat("&AMT={0}", AmtStr);
        result.AppendFormat("&VS={0}", vs);
        result.AppendFormat("&CS={0}", cs);

        if (ss != null)
        {
          result.AppendFormat("&SS={0}", ss);
        }
        if (desc != null)
        {
          result.AppendFormat("&DESC={0}", HttpUtility.UrlEncode(desc));
        }

        result.AppendFormat("&SIGN={0}", Signature);

        return result.ToString();
      }
    }
  }
}
