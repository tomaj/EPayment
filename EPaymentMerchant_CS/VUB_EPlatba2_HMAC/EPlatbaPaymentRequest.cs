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

using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Collections.Generic;
using System.IO;
namespace Monogram.EPayment.Merchant.VUB.EPlatba2HMAC
{
  /// <summary>
  /// Trieda vytv�raj�ca payment request protokolu V�B EPlatba z roku 2010 vyu��vaj�ceho HMAC podpisovanie spr�v
  /// </summary>
  public class EPlatbaPaymentRequest : EPaymentHmacSignedMessage, IPaymentRequest, IHttpPostPaymentRequest
  {
    #region required fields
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
    /// Variabiln� symbol
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
    public string StrAmt
    {
      get { return amt.ToString("0.00").Replace(',', '.'); }
    }

    //protected string vs = null;
    //public string Vs
    //{
    //  get { return vs; }
    //  set { vs = value; }
    //}

    protected string cs = null;
    /// <summary>
    /// Kon�tantn� symbol
    /// </summary>
    public string Cs
    {
      get { return cs; }
      set { cs = value; }
    }

    protected string rurl = null;
    /// <summary>
    /// N�vratov� URL, na ktor� bude zaslan� payment response
    /// </summary>
    public string Rurl
    {
      get { return rurl; }
      set { rurl = value; }
    }
    #endregion

    #region optional fields
    protected string ss = null;
    /// <summary>
    /// (nepovinn�) �pecifick� symbol
    /// </summary>
    public string Ss
    {
      get { return ss; }
      set { ss = value; }
    }

    protected string desc = null;
    /// <summary>
    /// (nepovinn�) Popis platby
    /// </summary>
    public string Desc
    {
      get { return desc; }
      set { desc = value; }
    }

    protected string rem = null;
    /// <summary>
    /// (nepovinn�) E-mailov� adresa obchodn�ka, na ktor� bude zaslan� potvrdenie o platba
    /// </summary>
    public string Rem
    {
      get { return rem; }
      set { rem = value; }
    }

    protected string rsms = null;
    /// <summary>
    /// (nepovinn�) ��slo obchodn�ka, na ktor� bude zaslan� SMS s potvrden�m o platbe
    /// </summary>
    public string Rsms
    {
      get { return rsms; }
      set { rsms = value; }
    }
    #endregion

    /// <summary>
    /// URL adresa, na ktor� sa posielaj� payment requesty v produk�nej prev�dzke
    /// </summary>
    public const string DefaultUrlBase = "https://ib.vub.sk/e-platbyeuro.aspx";
    protected string urlBase = DefaultUrlBase;
    /// <summary>
    /// URL, na ktor� bude zaslan� payment request. T�to hodnotu je mo�n� zmeni� na URL EPayment simul�tora, ktor�m m��ete otestova� v�sledn� aplik�ciu.
    /// </summary>
    public string UrlBase
    {
      get { return urlBase; }
      set { urlBase = value; }
    }

    /// <summary>
    /// Odtla�ok payment requestu
    /// </summary>
    public override string SignatureBase
    {
      get { return string.Format("{0}{1}{2}{3}{4}{5}", mid, StrAmt, vs, ss, cs, rurl); }
    }

    /// <summary>
    /// Met�da validuj�ca form�t payment requestu
    /// </summary>
    /// <returns>True ak je spr�va val�dna, inak false</returns>
    public override bool Validate()
    {
      if (mid == null) return false;
      if (mid.Length > 20) return false;
      if (StrAmt.Length > 13) return false;
      if (StrAmt.IndexOf(',') != -1) return false;
      if (vs == null) vs = string.Empty;
      if (vs.Length > 10) return false;
      if (cs == null) cs = string.Empty;
      if (cs.Length > 4) return false;
      if (rurl == null) return false;
      if (!(new Regex(@"^https?://.+$", RegexOptions.IgnoreCase).IsMatch(rurl))) return false;

      return true;
    }

    /// <summary>
    /// Met�da vracaj�ca v�etky hodnoty parametrov payment request-u
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, string> GetPaymentRequestFields()
    {
      Dictionary<string, string> result = new Dictionary<string, string>();

      result["MID"] = Mid;
      result["AMT"] = StrAmt;
      result["VS"] = Vs;
      result["CS"] = Cs;
      result["RURL"] = Rurl;
      result["SIGN"] = Signature;

      if (Ss != null)
      {
        result["SS"] = Ss;
      }

      if (Desc != null)
      {
        result["DESC"] = Desc;
      }

      if (Rem != null)
      {
        result["REM"] = Rem;
      }

      if (Rsms != null)
      {
        result["RSMS"] = Rsms;
      }

      return result;
    }

    /// <summary>
    /// Met�da do TextWriter-a vp�e prvky XHTML formul�ra nes�ce inform�cie o payment requeste.
    /// Pou��vanie tejto met�dy je odpor��an� ak potrebujete na v�slednej str�nke sami implementova�
    /// odosielanie formul�ra alebo v�sledn� str�nku prisp�sobova� vo vy��ej miere.
    /// </summary>
    /// <param name="tw">TextWriter, do ktor�ho sa vyp�e v�stup.</param>
    public void RenderPaymentRequestFields(TextWriter tw)
    {
      Dictionary<string, string> paymentRequestFields = GetPaymentRequestFields();
      foreach (string key in paymentRequestFields.Keys)
      {
        tw.Write("<input type=\"hidden\" name=\"");
        tw.Write(HttpUtility.HtmlEncode(key));
        tw.Write("\" value=\"");
        tw.Write(HttpUtility.HtmlEncode(paymentRequestFields[key]));
        tw.Write("\" />");
      }
    }

    /// <summary>
    /// Met�da do TextWriter-a vp�e XHTML formul�r s inform�ciami o payment requeste.
    /// </summary>
    /// <param name="tw">TextWriter, do ktor�ho sa vyp�e v�stup.</param>
    public void RenderPaymentRequestForm(TextWriter tw)
    {
      tw.Write("<form action=\""); tw.Write(HttpUtility.HtmlEncode(UrlBase)); tw.Write("\" method=\"post\">");
      RenderPaymentRequestFields(tw);
      tw.Write("<input type=\"submit\" value=\"Send payment request\" />");
      tw.Write("</form>");
    }
  }
}
