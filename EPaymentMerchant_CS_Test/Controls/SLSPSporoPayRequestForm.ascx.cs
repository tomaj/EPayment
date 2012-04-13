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
using Monogram.EPayment.Merchant.SLSP.SporoPay;
using Monogram.EPayment.Merchant;
using System.Web.Configuration;

public partial class Controls_SLSPSporoPayRequestForm : System.Web.UI.UserControl, IPaymentRequestForm
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
    Array authorizationTypes = (Array)Enum.GetValues(typeof(SporoPayAuthorizationType));
    ddlAuth_tool_type.DataSource = authorizationTypes;
    ddlAuth_tool_type.DataBind();

    Array clientNotificationTypes = (Array)Enum.GetValues(typeof(SporoPayClientNotification));
    ddlMail_notify_att.DataSource = clientNotificationTypes;
    ddlMail_notify_att.DataBind();

    tbPu_predcislo.Text = WebConfigurationManager.AppSettings["SLSP_SporoPay_Pu_predcislo"];
    tbPu_cislo.Text = WebConfigurationManager.AppSettings["SLSP_SporoPay_Pu_cislo"];

    tbSs.Text = Common.Random.Next().ToString().PadLeft(10, '0');
    tbParam.Text = "myParam=" + Common.Random.Next();
    tbUrl.Text = Common.GetUrl(this, "~/SLSP_SporoPay.aspx");
  }

  public IPaymentRequest GetPaymentRequest()
  {
    SporoPayPaymentRequest pr = new SporoPayPaymentRequest();
    pr.Vs = VariableSymbol;
    pr.Suma = Amount;
    pr.Pu_predcislo = tbPu_predcislo.Text;
    pr.Pu_cislo = tbPu_cislo.Text;
    pr.Ss = tbSs.Text;
    pr.Param = tbParam.Text;
    pr.Url = tbUrl.Text;
    if (chbAcc_prefix.Checked)
      pr.Acc_prefix = tbAcc_prefix.Text;
    if (chbAcc_number.Checked)
      pr.Acc_number = tbAcc_number.Text;
    if (chbMail_notify_att.Checked)
      pr.Mail_notify_att = (SporoPayClientNotification)Enum.Parse(typeof(SporoPayClientNotification), ddlMail_notify_att.SelectedValue);
    if (chbEmail_adr.Checked)
      pr.Email_adr = tbEmail_adr.Text;
    if (chbClient_login.Checked)
      pr.Client_login = tbClient_login.Text;
    if (chbAuth_tool_type.Checked)
      pr.Auth_tool_type = (SporoPayAuthorizationType)Enum.Parse(typeof(SporoPayAuthorizationType), ddlAuth_tool_type.SelectedValue);

    pr.UrlBase = WebConfigurationManager.AppSettings["SLSP_SporoPay_UrlBase"];

    return pr;
  }
}
