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
namespace Monogram.EPayment.Merchant.VUB.EPlatba
{
  /// <summary>
  /// Trieda vytvárajúca payment request protokolu VÚB EPlatba
  /// </summary>
  public class EPlatbaPaymentRequest : EPaymentDesSignedMessage, IHttpRedirectPaymentRequest, IPaymentRequest
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
    /// Variabilnı symbol
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
    /// Konštantnı symbol
    /// </summary>
    public string Cs
    {
      get { return cs; }
      set { cs = value; }
    }

    protected string rurl = null;
    /// <summary>
    /// Návratová URL, na ktorú bude zaslanı payment response
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
    /// (nepovinné) Špecifickı symbol
    /// </summary>
    public string Ss
    {
      get { return ss; }
      set { ss = value; }
    }

    protected string desc = null;
    /// <summary>
    /// (nepovinné) Popis platby
    /// </summary>
    public string Desc
    {
      get { return desc; }
      set { desc = value; }
    }

    protected string rem = null;
    /// <summary>
    /// (nepovinné) E-mailová adresa obchodníka, na ktorú bude zaslané potvrdenie o platba
    /// </summary>
    public string Rem
    {
      get { return rem; }
      set { rem = value; }
    }

    protected string rsms = null;
    /// <summary>
    /// (nepovinné) Èíslo obchodníka, na ktoré bude zaslaná SMS s potvrdením o platbe
    /// </summary>
    public string Rsms
    {
      get { return rsms; }
      set { rsms = value; }
    }
    #endregion

    /// <summary>
    /// URL adresa, na ktorú sa posielajú payment requesty v produkènej prevádzke
    /// </summary>
    public const string DefaultUrlBase = "https://ib.vub.sk/e-platbyeuro.aspx";
    protected string urlBase = DefaultUrlBase;
    /// <summary>
    /// URL, na ktorú bude zaslanı payment request. Túto hodnotu je moné zmeni na URL EPayment simulátora, ktorım môete otestova vıslednú aplikáciu.
    /// </summary>
    public string UrlBase
    {
      get { return urlBase; }
      set { urlBase = value; }
    }

    /// <summary>
    /// Odtlaèok payment requestu
    /// </summary>
    public override string SignatureBase
    {
      get { return string.Format("{0}{1}{2}{3}{4}", mid, StrAmt, vs, cs, rurl); }
    }

    /// <summary>
    /// Metóda validujúca formát payment requestu
    /// </summary>
    /// <returns>True ak je správa valídna, inak false</returns>
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
    /// URL obsahujúca payment request - na túto adresu má by klient presmerovanı
    /// </summary>
    public string PaymentRequestUrl
    {
      get
      {
        StringBuilder sb = new StringBuilder(string.Format("{0}?MID={1}&AMT={2}&VS={3}&CS={4}&RURL={5}&SIGN={6}",
                                                            urlBase, Mid, StrAmt, Vs, Cs, Rurl, Signature));
        if (ss != null)
          sb.AppendFormat("&SS={0}", ss);
        if (desc != null)
          sb.AppendFormat("&DESC={0}", HttpUtility.UrlEncode(desc));
        if (rem != null)
          sb.AppendFormat("&REM={0}", HttpUtility.UrlEncode(rem));
        if (rsms != null)
          sb.AppendFormat("&RSMS={0}", HttpUtility.UrlEncode(rsms));
        return sb.ToString();
      }
    }
  }
}
