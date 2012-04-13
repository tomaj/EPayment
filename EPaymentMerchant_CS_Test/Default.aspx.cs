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
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Monogram.EPayment.Merchant;
using System.Web.Configuration;
using System.IO;

public partial class _Default : System.Web.UI.Page 
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
    Array paymentTypes = (Array)Enum.GetValues(typeof(KnownPaymentTypes));
    ddlPaymentType.DataSource = paymentTypes;
    ddlPaymentType.DataBind();

    tbVariableSymbol.Text = Common.Random.Next().ToString().PadLeft(10, '0');
    tbAmount.Text = "100";

    initPaymentForm(KnownPaymentTypes.SLSP_SporoPay);
  }

  private void initPaymentForm(KnownPaymentTypes paymentType)
  {
    SLSPSporoPayRequestFormControl.Visible = false;
    TBTatraPayRequestFormControl.Visible = false;
    TBCardPayRequestFormControl.Visible = false;
    VUBEPlatbaRequestFormControl.Visible = false;
    VUBEPlatba2HMACRequestFormControl.Visible = false;

    switch (paymentType)
    {
      case KnownPaymentTypes.SLSP_SporoPay:
        SLSPSporoPayRequestFormControl.Visible = true;
        SLSPSporoPayRequestFormControl.VariableSymbol = tbVariableSymbol.Text;
        SLSPSporoPayRequestFormControl.Amount = double.Parse(tbAmount.Text);
        tbSharedSecret.Text = WebConfigurationManager.AppSettings["SLSP_SporoPay_SharedSecret"];
        break;

      case KnownPaymentTypes.TB_TatraPay:
        TBTatraPayRequestFormControl.Visible = true;
        TBTatraPayRequestFormControl.VariableSymbol = tbVariableSymbol.Text;
        TBTatraPayRequestFormControl.Amount = double.Parse(tbAmount.Text);
        tbSharedSecret.Text = WebConfigurationManager.AppSettings["TB_TatraPay_SharedSecret"];
        break;

      case KnownPaymentTypes.TB_CardPay:
        TBCardPayRequestFormControl.Visible = true;
        TBCardPayRequestFormControl.VariableSymbol = tbVariableSymbol.Text;
        TBCardPayRequestFormControl.Amount = double.Parse(tbAmount.Text);
        tbSharedSecret.Text = WebConfigurationManager.AppSettings["TB_CardPay_SharedSecret"];
        break;

      case KnownPaymentTypes.VUB_EPlatba:
        VUBEPlatbaRequestFormControl.Visible = true;
        VUBEPlatbaRequestFormControl.VariableSymbol = tbVariableSymbol.Text;
        VUBEPlatbaRequestFormControl.Amount = double.Parse(tbAmount.Text);
        tbSharedSecret.Text = WebConfigurationManager.AppSettings["VUB_EPlatba_SharedSecret"];
        break;

      case KnownPaymentTypes.VUB_EPlatba2_HMAC:
        VUBEPlatba2HMACRequestFormControl.Visible = true;
        VUBEPlatba2HMACRequestFormControl.VariableSymbol = tbVariableSymbol.Text;
        VUBEPlatba2HMACRequestFormControl.Amount = double.Parse(tbAmount.Text);
        tbSharedSecret.Text = WebConfigurationManager.AppSettings["VUB_EPlatba2_HMAC_SharedSecret"];
        break;

      case KnownPaymentTypes.UCB_UniPlatba:
        UCBUniPlatbaRequestFormControl.Visible = true;
        UCBUniPlatbaRequestFormControl.VariableSymbol = tbVariableSymbol.Text;
        UCBUniPlatbaRequestFormControl.Amount = double.Parse(tbAmount.Text);
        tbSharedSecret.Text = WebConfigurationManager.AppSettings["UCB_UniPlatba_SharedSecret"];
        break;
    }
  }

  private IPaymentRequest getActualPaymentRequest()
  {
    initPaymentForm((KnownPaymentTypes)Enum.Parse(typeof(KnownPaymentTypes), ddlPaymentType.SelectedValue));
    switch ((KnownPaymentTypes)Enum.Parse(typeof(KnownPaymentTypes), ddlPaymentType.SelectedValue))
    {
      case KnownPaymentTypes.SLSP_SporoPay:
        return SLSPSporoPayRequestFormControl.GetPaymentRequest();
        break;

      case KnownPaymentTypes.TB_TatraPay:
        return TBTatraPayRequestFormControl.GetPaymentRequest();
        break;

      case KnownPaymentTypes.TB_CardPay:
        return TBCardPayRequestFormControl.GetPaymentRequest();
        break;

      case KnownPaymentTypes.VUB_EPlatba:
        return VUBEPlatbaRequestFormControl.GetPaymentRequest();
        break;

      case KnownPaymentTypes.VUB_EPlatba2_HMAC:
        return VUBEPlatba2HMACRequestFormControl.GetPaymentRequest();
        break;

      case KnownPaymentTypes.UCB_UniPlatba:
        return UCBUniPlatbaRequestFormControl.GetPaymentRequest();
        break;
    }
    throw new ApplicationException("Not implemented.");
  }

  protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs args)
  {
    initPaymentForm((KnownPaymentTypes)Enum.Parse(typeof(KnownPaymentTypes), ddlPaymentType.SelectedValue));
    createPaymentRequest(getActualPaymentRequest());
  }

  protected void btnSendRequest_Click(object sender, EventArgs args)
  {
    createPaymentRequest(getActualPaymentRequest());
  }

  private void resetPaymentRequest()
  {
    PaymentRequestCreationLogControl.Reset();
    hlPaymentRequest.Text = string.Empty;
    hlPaymentRequest.NavigateUrl = string.Empty;
  }

  private void createPaymentRequest(IPaymentRequest pr)
  {
    resetPaymentRequest();

    bool validationResult = pr.Validate();

    PaymentRequestCreationLogControl.IsValid = validationResult;

    if (validationResult)
    {
      if (pr is EPaymentMessage)
      {
        PaymentRequestCreationLogControl.UncryptedSignature = ((EPaymentMessage)pr).SignatureBase;
      }
      pr.SignMessage(tbSharedSecret.Text);
      if (pr is EPaymentMessage)
      {
        PaymentRequestCreationLogControl.CryptedSignature = ((EPaymentMessage)pr).Signature;
      }

      if (pr is IHttpRedirectPaymentRequest)
      {
        displayPaymentRequestLink(((IHttpRedirectPaymentRequest)pr).PaymentRequestUrl);
      }
      else
      {
        hlPaymentRequest.NavigateUrl = null;
        hlPaymentRequest.Visible = false;
      }

      if (pr is IHttpPostPaymentRequest)
      {
        postPaymentRequestToRender = (IHttpPostPaymentRequest)pr;
      }
    }
  }

  private void displayPaymentRequestLink(string url)
  {
    hlPaymentRequest.Text = hlPaymentRequest.NavigateUrl = url;
    hlPaymentRequest.Visible = true;
  }

  private IHttpPostPaymentRequest postPaymentRequestToRender = null;
  public void renderSendFormIfNecessary(TextWriter tw)
  {
    if (postPaymentRequestToRender != null)
    {
      postPaymentRequestToRender.RenderPaymentRequestForm(tw);
      postPaymentRequestToRender = null;
    }
  }
}
