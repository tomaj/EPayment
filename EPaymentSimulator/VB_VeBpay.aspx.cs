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
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Text;

public partial class VB_VeBpay : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      saveRequestToViewState();

      bool isOK = processPaymentRequest();

      PaymentRequestProcessingLogControl.DataBind();

      if (isOK)
      {
        initResponseForm();
      }
    }
  }

  private bool processPaymentRequest()
  {
    bool result;
    result = validatePaymentRequest();

    if (result)
    {
      result = verifyPaymentRequest();
    }

    return result;
  }

  private void saveRequestToViewState()
  {
    NameValueCollection fields = null;

    if (Request.HttpMethod == "GET")
    {
      fields = Request.QueryString;
    }
    else if (Request.HttpMethod == "POST")
    {
      fields = Request.Form;
    }
    else
    {
      fields = new NameValueCollection();
    }

    ViewState["paymentRequestFields"] = fields;
  }

  private NameValueCollection _paymentRequestFields = null;
  private NameValueCollection paymentRequestFields
  {
    get
    {
      if (_paymentRequestFields == null)
      {
        _paymentRequestFields = (NameValueCollection)ViewState["paymentRequestFields"];
        if (_paymentRequestFields == null)
        {
          _paymentRequestFields = new NameValueCollection();
        }
      }
      return _paymentRequestFields;
    }
  }

  private bool validatePaymentRequest()
  {
    bool validationResult = true;
    ArrayList validationLog = new ArrayList();

    if (!(Request.HttpMethod == "GET"))
    {
      validationResult = false;
      validationLog.Add(string.Format("Payment request bol prijatý HTTP metódou {0} . Povolenou metódou je iba GET.", Request.HttpMethod));
    }

    foreach (string field in new string[] { "MID", "AMT", "VS", "CS", "RURL", "SIGN" })
    {
      string value = paymentRequestFields[field];
      if (value == null)
      {
        validationResult = false;
        validationLog.Add("Chýba parameter " + field + " .");
      }
      else
      {
        switch (field)
        {
          case "MID":
            if (!(new Regex("^[0-9a-zA-Z]{1,20}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Parameter MID musí byť maximálne 20 alfanumerických znakov.");
            }
            if (value != EPaymentConstants.VB_VeBpay_mid)
            {
              validationResult = false;
              validationLog.Add("E-payment simulátor akceptuje ako MID parameter z bezpečnostných príčin iba hodnotu \"" + EPaymentConstants.VB_VeBpay_mid + "\", prosím, pri testovaní používajte túto hodnotu.");
            }
            break;

          case "AMT":
            if (value.Length > 13)
            {
              validationResult = false;
              validationLog.Add("Parameter AMT nesmie by dlhší ako 13 znakov.");
            }
            if (!(new Regex(@"^([0-9]+|[0-9]*\.[0-9]{1,2})$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Parameter AMT musí byť suma - číslo. Ako oddeľovač desatinných miest sa použije znak \".\" .");
            }
            break;

          case "VS":
            if (!(new Regex("^[0-9]{1,10}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Parameter VS musí byť zložený iba z numerických znakov a nesmie byť dlhší ako 10 znakov.");
            }
            break;

          case "CS":
            if (!(new Regex("^[0-9]{1,4}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Parameter CS musí byť zložený iba z numerických znakov a nesmie byť dlhší ako 4 znaky.");
            }
            break;

          case "RURL":
            if (!(new Regex(@"^https?\://.+", RegexOptions.IgnoreCase)).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Parameter RURL musí byť URL.");
            }
            break;
        }
      }
    }

    foreach (string field in new string[] { "DESC" })
    {
      if (Request[field] != null)
      {
        String value = Request[field];
        switch (field)
        {
          case "DESC":
            if (value.Length > 35)
            {
              validationResult = false;
              validationLog.Add("Nepovinný parameter DESC nesmie byť dlhší ako 35 znakov.");
            }
            break;
        }
      }
    }

    PaymentRequestProcessingLogControl.IsValidated = validationResult;
    PaymentRequestProcessingLogControl.ValidationLog = (string[])validationLog.ToArray(typeof(string));

    return validationResult;
  }

  private bool verifyPaymentRequest()
  {
    string signatureBase = string.Format("{0}{1}{2}{3}{4}",
      paymentRequestFields["MID"],
      paymentRequestFields["AMT"],
      paymentRequestFields["VS"],
      paymentRequestFields["CS"],
      paymentRequestFields["RURL"]
    );

    PaymentRequestProcessingLogControl.UncryptedSignature = signatureBase;
    string signature = CryptoHelper.GetDesSignatureInHexdec(signatureBase, EPaymentConstants.VB_VeBpay_sharedSecret);
    PaymentRequestProcessingLogControl.CryptedSignature = signature;
    PaymentRequestProcessingLogControl.ReceivedSignature = paymentRequestFields["SIGN"];

    bool result = (signature == paymentRequestFields["SIGN"]);

    PaymentRequestProcessingLogControl.IsVerified = result;

    return result;
  }

  private void initResponseForm()
  {
    DictionaryEntry[] responses = new DictionaryEntry[] {
      new DictionaryEntry("OK","OK"),
      new DictionaryEntry("FAIL","Fail")
    };

    ddlResponse.DataSource = responses;
    ddlResponse.DataValueField = "Key";
    ddlResponse.DataTextField = "Value";
    ddlResponse.DataBind();

    ddlResponse.Enabled = true;
    btnSendResponse.Enabled = true;
    btnGetResponseUrl.Enabled = true;
    hlResponse.Visible = true;

    createResponse("OK");
  }

  private void createResponse(string response)
  {
    Dictionary<string, string> responseFields = new Dictionary<string, string>();

    responseFields["VS"] = paymentRequestFields["VS"];
    responseFields["RES"] = response;

    string signature = createSignature(responseFields);

    responseFields["SIGN"] = signature;

    ResponseEditFormControl.InitialValues = responseFields;
    ResponseEditFormControl.DataBind();
  }

  private string createSignature(Dictionary<string, string> fields)
  {

    StringBuilder sb = new StringBuilder(fields["VS"]);

    string signatureBase = string.Format("{0}{1}", fields["VS"], fields["RES"]);
    string signature = CryptoHelper.GetDesSignatureInHexdec(signatureBase, EPaymentConstants.VB_VeBpay_sharedSecret);
    return signature;
  }

  protected void ddlResponse_SelectedIndexChanged(Object sender, EventArgs args)
  {
    createResponse(ddlResponse.SelectedValue);
  }

  private string getResponseUrl()
  {
    string url = Common.CreateHttpGetResponseUrl(paymentRequestFields["RURL"], ResponseEditFormControl.Values);
    return url;
  }

  protected void btnGetResponseUrl_Click(Object sender, EventArgs args)
  {
    hlResponse.Text = hlResponse.NavigateUrl = getResponseUrl();
  }

  protected void btnSendResponse_Click(Object sender, EventArgs args)
  {
    Response.Redirect(getResponseUrl());
  }
}
