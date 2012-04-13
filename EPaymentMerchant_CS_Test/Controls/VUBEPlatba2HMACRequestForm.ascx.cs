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
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using Monogram.EPayment.Merchant;
using Monogram.EPayment.Merchant.VUB.EPlatba2HMAC;

public partial class Controls_VUBEPlatba2HMACRequestForm : System.Web.UI.UserControl, IPaymentRequestForm
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      initForm();
    }
  }

  private void initForm()
  {
    tbMid.Text = WebConfigurationManager.AppSettings["VUB_EPlatba2_HMAC_Mid"];
    tbCs.Text = "0905";
    tbRurl.Text = Common.GetUrl(this, "~/VUB_EPlatba2_HMAC.aspx");
  }

  public double Amount
  {
    get
    {
      if (ViewState["Amount"] == null)
      {
        throw new ApplicationException("Amount was not set yet.");
      }
      return (double)ViewState["Amount"];
    }
    set
    {
      ViewState["Amount"] = value;
    }
  }

  public string VariableSymbol
  {
    get { return (string)ViewState["VariableSymbol"]; }
    set { ViewState["VariableSymbol"] = value; }
  }

  public IPaymentRequest GetPaymentRequest()
  {
    EPlatbaPaymentRequest pr = new EPlatbaPaymentRequest();
    pr.Vs = VariableSymbol;
    pr.Amt = Amount;
    pr.Mid = tbMid.Text;
    pr.Cs = tbCs.Text;
    pr.Rurl = tbRurl.Text;

    if (chbSs.Checked)
      pr.Ss = tbSs.Text;
    if (chbRsms.Checked)
      pr.Rsms = tbRsms.Text;
    if (chbRem.Checked)
      pr.Rem = tbRem.Text;
    if (chbDesc.Checked)
      pr.Desc = tbDesc.Text;

    pr.UrlBase = WebConfigurationManager.AppSettings["VUB_EPlatba2_HMAC_UrlBase"];

    return pr;
  }
}
