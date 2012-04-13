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
using Monogram.EPayment.Merchant.UCB.UniPlatba;
using Monogram.EPayment.Merchant;

public partial class Controls_UCBUniPlatbaRequestForm : System.Web.UI.UserControl, IPaymentRequestForm
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
    tbMid.Text = WebConfigurationManager.AppSettings["UCB_UniPlatba_Mid"];
    tbCs.Text = "0308";

    Array languages = Enum.GetValues(typeof(UniPlatbaPaymentLanguage));
    ddlLng.DataSource = languages;
    ddlLng.DataBind();
  }

  public IPaymentRequest GetPaymentRequest()
  {
    UniPlatbaPaymentRequest pr = new UniPlatbaPaymentRequest();
    pr.Vs = VariableSymbol;
    pr.Amt = Amount;
    pr.Mid = tbMid.Text;
    pr.Cs = tbCs.Text;
    pr.Lng = (UniPlatbaPaymentLanguage)Enum.Parse(typeof(UniPlatbaPaymentLanguage), ddlLng.SelectedValue);

    if (!string.IsNullOrEmpty(tbSs.Text))
      pr.Ss = tbSs.Text;

    if (!string.IsNullOrEmpty(tbDesc.Text))
      pr.Desc = tbDesc.Text;

    pr.UrlBase = WebConfigurationManager.AppSettings["UCB_UniPlatba_UrlBase"];

    return pr;
  }
}
