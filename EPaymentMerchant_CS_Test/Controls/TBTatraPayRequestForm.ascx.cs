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
using Monogram.EPayment.Merchant.TB.TatraPay;
using Monogram.EPayment.Merchant;
using System.Web.Configuration;

public partial class Controls_TBTatraPayRequestForm : System.Web.UI.UserControl, IPaymentRequestForm
{
  public string VariableSymbol
  {
    get { return (string)ViewState["VariableSymbol"]; }
    set { ViewState["VariableSymbol"] = value; }
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

  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      initForm();
    }
  }

  private void initForm()
  {
    tbMid.Text = WebConfigurationManager.AppSettings["TB_TatraPay_Mid"];
    tbCs.Text = "0905";
    tbRurl.Text = Common.GetUrl(this, "~/TB_TatraPay.aspx");

    Array paymentTypes = Enum.GetValues(typeof(TatraPayPaymentType));
    ddlPt.DataSource = paymentTypes;
    ddlPt.DataBind();

    ddlAredir.DataSource = new string[] { "1", "0" };
    ddlAredir.DataBind();

    Array languages = Enum.GetValues(typeof(TatraPayLanguage));
    ddlLang.DataSource = languages;
    ddlLang.DataBind();
  }

  public IPaymentRequest GetPaymentRequest()
  {
    TatraPayPaymentRequest pr = new TatraPayPaymentRequest();
    pr.Vs = VariableSymbol;
    pr.Amt = Amount;
    pr.Mid = tbMid.Text;
    pr.Cs = tbCs.Text;
    pr.Rurl = tbRurl.Text;

    if (chbPt.Checked)
      pr.Pt = (TatraPayPaymentType)Enum.Parse(typeof(TatraPayPaymentType), ddlPt.SelectedValue);
    else
      pr.Pt = null;
    if (chbSs.Checked)
      pr.Ss = tbSs.Text;
    if (chbRsms.Checked)
      pr.Rsms = tbRsms.Text;
    if (chbRem.Checked)
      pr.Rem = tbRem.Text;
    if (chbDesc.Checked)
      pr.Desc = tbDesc.Text;
    if (chbAredir.Checked)
      pr.Aredir = (ddlAredir.SelectedValue == "1");
    if (chbLang.Checked)
      pr.Lang = (TatraPayLanguage)Enum.Parse(typeof(TatraPayLanguage), ddlLang.SelectedValue);

    pr.UrlBase = WebConfigurationManager.AppSettings["TB_TatraPay_UrlBase"];

    return pr;
  }
}
