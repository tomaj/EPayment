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
using Monogram.EPayment.Merchant;
using Monogram.EPayment.Merchant.SLSP.SporoPay;
using Monogram.EPayment.Merchant.TB.CardPay;
using Monogram.EPayment.Merchant.TB.TatraPay;
using Monogram.EPayment.Merchant.VUB.EPlatba;
using Monogram.EPayment.Merchant.UCB.UniPlatba;
using System.Web.Configuration;
using System.Collections.Generic;

public partial class Controls_CommonPaymentResponseForm : System.Web.UI.UserControl
{
  public KnownPaymentTypes PaymentType
  {
    get { return (KnownPaymentTypes)ViewState["PaymentType"]; }
    set { ViewState["PaymentType"] = value; }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      processPaymentResponse();
    }
  }

  protected void processPaymentResponse()
  {
    mvPaymentInfo.SetActiveView(vFailure);
    lPaymentType.Text = PaymentType.ToString();

    ISignedResponse paymentResponse;
    string sharedSecret;

    switch (PaymentType)
    {
      case KnownPaymentTypes.SLSP_SporoPay:
        paymentResponse = new SporoPayPaymentHttpResponse(Request);
        sharedSecret = WebConfigurationManager.AppSettings["SLSP_SporoPay_SharedSecret"];
        break;

      case KnownPaymentTypes.TB_CardPay:
        paymentResponse = new CardPayPaymentHttpResponse(Request);
        sharedSecret = WebConfigurationManager.AppSettings["TB_CardPay_SharedSecret"];
        break;

      case KnownPaymentTypes.TB_TatraPay:
        paymentResponse = new TatraPayPaymentHttpResponse(Request);
        sharedSecret = WebConfigurationManager.AppSettings["TB_TatraPay_SharedSecret"];
        break;

      case KnownPaymentTypes.VUB_EPlatba:
        paymentResponse = new Monogram.EPayment.Merchant.VUB.EPlatba.EPlatbaPaymentHttpResponse(Request);
        sharedSecret = WebConfigurationManager.AppSettings["VUB_EPlatba_SharedSecret"];
        break;

      case KnownPaymentTypes.VUB_EPlatba2_HMAC:
        paymentResponse = new Monogram.EPayment.Merchant.VUB.EPlatba2HMAC.EPlatbaPaymentHttpResponse(Request);
        sharedSecret = WebConfigurationManager.AppSettings["VUB_EPlatba2_HMAC_SharedSecret"];
        break;

      case KnownPaymentTypes.UCB_UniPlatba:
        paymentResponse = new UniPlatbaPaymentHttpResponse(Request);
        sharedSecret = WebConfigurationManager.AppSettings["UCB_UniPlatba_SharedSecret"];
        break;

      default:
        throw new ApplicationException(string.Format("Payment type {0} is not implemented.", PaymentType));
    }

    bool validationResult = ((EPaymentMessage)paymentResponse).Validate();

    lValidationResult.Text = validationResult ? "OK" : "Fail";

    if (validationResult)
    {
      bool signatureVerificationResult = paymentResponse.VerifySignature(sharedSecret);

      lVerificationResult.Text = validationResult ? "OK" : "Fail";

      lSignatureBase.Text = ((EPaymentMessage)paymentResponse).SignatureBase;
      lSignature.Text = ((EPaymentMessage)paymentResponse).Signature;
      lReceivedSignature.Text = paymentResponse.ReceivedSignature;

      if (signatureVerificationResult)
      {
        mvPaymentInfo.SetActiveView(vResponseInfo);

        lPaymentResult.Text = paymentResponse.GetPaymentResponse().ToString();
        lVariableSymbol.Text = ((EPaymentMessage)paymentResponse).Vs;

        rAdditionalInformation.DataSource = getAdditionalPaymentResponseInformation(paymentResponse);
        rAdditionalInformation.DataBind();
      }
    }
  }

  private Dictionary<string, string> getAdditionalPaymentResponseInformation(ISignedResponse paymentResponse)
  {
    Dictionary<string, string> result = new Dictionary<string, string>();
    switch (PaymentType)
    {
      case KnownPaymentTypes.SLSP_SporoPay:
        {
          SporoPayPaymentHttpResponse pr = (SporoPayPaymentHttpResponse)paymentResponse;
          result.Add("u_predcislo", pr.U_predcislo);
          result.Add("u_cislo", pr.U_cislo);
          result.Add("u_kbanky", pr.U_kbanky);
          result.Add("pu_predcislo", pr.Pu_predcislo);
          result.Add("pu_cislo", pr.Pu_cislo);
          result.Add("pu_kbanky", pr.Pu_kbanky);
          result.Add("suma", pr.StrSuma);
          result.Add("mena", pr.Mena.ToString());
          result.Add("ss", pr.Ss);
          result.Add("url", pr.Url);
          result.Add("param", pr.Param);
          result.Add("result", pr.Result.ToString());
          result.Add("real", pr.Real.ToString());
        }
        break;

      case KnownPaymentTypes.TB_CardPay:
        {
          CardPayPaymentHttpResponse pr = (CardPayPaymentHttpResponse)paymentResponse;
          result.Add("AC", pr.Ac);
        }
        break;

      case KnownPaymentTypes.TB_TatraPay:
        {
          TatraPayPaymentHttpResponse pr = (TatraPayPaymentHttpResponse)paymentResponse;
          result.Add("RES", pr.ResStr);
        }
        break;

      case KnownPaymentTypes.VUB_EPlatba:
        {
          EPlatbaPaymentHttpResponse pr = (EPlatbaPaymentHttpResponse)paymentResponse;
          result.Add("RES", pr.StrRes);
        }
        break;
    }
    return result;
  }
}
