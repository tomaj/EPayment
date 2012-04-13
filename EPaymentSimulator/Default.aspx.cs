//  Copyright 2009 MONOGRAM Technologies
// 
//  This file is part of MONOGRAM EPayment libraries
//  
//  MONOGRAM EPayment libraries is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MONOGRAM EPayment libraries is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with MONOGRAM EPayment libraries.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      fillTestParameters();
    }
  }

  private void fillTestParameters()
  {
    lSLSP_SporoPay_pu_cislo.Text = EPaymentConstants.SLSP_SporoPay_pu_cislo;
    lSLSP_SporoPay_pu_kbanky.Text = EPaymentConstants.SLSP_SporoPay_pu_kbanky;
    lSLSP_SporoPay_pu_predcislo.Text = EPaymentConstants.SLSP_SporoPay_pu_predcislo;
    lSLSP_SporoPay_sharedSecret.Text = EPaymentConstants.SLSP_SporoPay_sharedSecret;
    lSLSP_SporoPay_UrlBase.Text = Common.GetUrl(this, "SLSP_SporoPay.aspx");

    lVUB_EPlatba_mid.Text = EPaymentConstants.VUB_EPlatba_mid;
    lVUB_EPlatba_sharedSecret.Text = EPaymentConstants.VUB_EPlatba_sharedSecret;
    lVUB_EPlatba_UrlBase.Text = Common.GetUrl(this, "VUB_EPlatba.aspx");

    lVUB_EPlatba2_HMAC_mid.Text = EPaymentConstants.VUB_EPlatba2_HMAC_mid;
    lVUB_EPlatba2_HMAC_sharedSecret.Text = EPaymentConstants.VUB_EPlatba2_HMAC_sharedSecret;
    lVUB_EPlatba2_HMAC_UrlBase.Text = Common.GetUrl(this, "VUB_EPlatba2_HMAC.aspx");

    lTB_TatraPay_mid.Text = EPaymentConstants.TB_TatraPay_mid;
    lTB_TatraPay_sharedSecret.Text = EPaymentConstants.TB_TatraPay_sharedSecret;
    lTB_TatraPay_UrlBase.Text = Common.GetUrl(this, "TB_TatraPay.aspx");

    lTB_CardPay_mid.Text = EPaymentConstants.TB_CardPay_mid;
    lTB_CardPay_sharedSecret.Text = EPaymentConstants.TB_CardPay_sharedSecret;
    lTB_CardPay_UrlBase.Text = Common.GetUrl(this, "TB_CardPay.aspx");

    lUCB_UniPlatba_mid.Text = EPaymentConstants.UCB_UniPlatba_mid;
    lUCB_UniPlatba_sharedSecret.Text = EPaymentConstants.UCB_UniPlatba_sharedSecret;
    lUCB_UniPlatba_UrlBase.Text = Common.GetUrl(this, "UCB_UniPlatba.aspx");

    lVB_VeBpay_mid.Text = EPaymentConstants.VB_VeBpay_mid;
    lVB_VeBpay_sharedSecret.Text = EPaymentConstants.VB_VeBpay_sharedSecret;
    lVB_VeBpay_UrlBase.Text = Common.GetUrl(this, "VB_VeBpay.aspx");
  }
}
