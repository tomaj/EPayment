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
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

public partial class VUB_EPlatba2_HMAC : System.Web.UI.Page
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

    if (Request.HttpMethod == "POST")
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

    if (Request.HttpMethod != "POST")
    {
      validationResult = false;
      validationLog.Add(string.Format("Payment request bol prijat˝ HTTP metÛdou {0} . Vyûadovanou metÛdou zasielania payment requestu je POST.", Request.HttpMethod));
    }

    foreach (string field in new string[] { "MID", "AMT", "VS", "CS", "RURL", "SIGN" })
    {
      string value = paymentRequestFields[field];
      if (value == null)
      {
        validationResult = false;
        validationLog.Add("Ch˝ba parameter " + field + " .");
      }
      else
      {
        switch (field)
        {
          case "MID":
            if (!(new Regex("^[0-9a-zA-Z]{1,20}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Parameter MID musÌ byù maxim·lne 20 alfanumerick˝ch znakov.");
            }
            if (value != EPaymentConstants.VUB_EPlatba2_HMAC_mid)
            {
              validationResult = false;
              validationLog.Add("Parameter MID sa nezhoduje s hodnotou urËenou pre tento testovacÌ server.");
            }
            break;

          case "AMT":
            if (value.Length > 13)
            {
              validationResult = false;
              validationLog.Add("Parameter AMT nesmie byù dlhöÌ ako 13 znakov.");
            }
            if (!(new Regex(@"^([0-9]+|[0-9]*\.[0-9]{1,2})$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Parameter AMT musÌ byù suma - ËÌslo. Ako oddeæovaË desatinn˝ch miest sa pouûije znak \".\" .");
            }
            break;

          case "VS":
            if (!(new Regex("^[0-9]{1,10}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Parameter VS musÌ byù zloûen˝ iba z numerick˝ch znakov a nesmie byù dlhöÌ ako 10 znakov.");
            }
            break;

          case "CS":
            if (!(new Regex("^[0-9]{1,4}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Parameter CS musÌ byù zloûen˝ iba z numerick˝ch znakov a nesmie byù dlhöÌ ako 4 znaky.");
            }
            break;

          case "RURL":
            if (!(new Regex(@"^https?\://.+", RegexOptions.IgnoreCase)).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Parameter RURL musÌ byù platnou URL.");
            }
            break;
        }
      }
    }

    foreach (string field in new string[] { "SS", "DESC", "REM", "RSMS" })
    {
      if (paymentRequestFields[field] != null)
      {
        string value = paymentRequestFields[field];
        switch (field)
        {
          case "SS":
            if (!(new Regex("^[0-9]{1,10}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Nepovinn˝ parameter SS musÌ byù zloûen˝ iba z numerick˝ch znakov a nesmie byù dlhöÌ ako 10 znakov.");
            }
            break;

          case "DESC":
            if (value.Length > 30)
            {
              validationResult = false;
              validationLog.Add("Hodnota nepovinnÈho parametra DESC nesmie byù dlhöia ako 30 znakov.");
            }
            break;

          case "REM":
            if (!(new Regex("^.+@.+$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Nepovinn˝ parameter REM musÌ byù emailov· adresa.");
            }
            break;

          case "RSMS":
            if (!(new Regex("^09[0-9]{8}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Nepovinn˝ parameter RSMS musÌ byù zloûen˝ z 10 numerick˝ch znakov, zaËÌnaù na \"09\".");
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
    string signatureBase = string.Format("{0}{1}{2}{3}{4}{5}",
      paymentRequestFields["MID"],
      paymentRequestFields["AMT"],
      paymentRequestFields["VS"],
      paymentRequestFields["SS"],
      paymentRequestFields["CS"],
      paymentRequestFields["RURL"]
    );

    PaymentRequestProcessingLogControl.UncryptedSignature = signatureBase;
    string signature = CryptoHelper.GetHmacSignatureInHexdec(signatureBase, EPaymentConstants.VUB_EPlatba2_HMAC_sharedSecret);
    PaymentRequestProcessingLogControl.CryptedSignature = signature;
    PaymentRequestProcessingLogControl.ReceivedSignature = paymentRequestFields["SIGN"];

    bool result = (signature == paymentRequestFields["SIGN"]);

    PaymentRequestProcessingLogControl.IsVerified = result;

    return result;
  }

  private void initResponseForm()
  {
    DictionaryEntry[] responses = new DictionaryEntry[] {
      new DictionaryEntry("OK", "OK"),
      new DictionaryEntry("FAIL", "Fail")
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
    if (paymentRequestFields["SS"] != null)
    {
      responseFields["SS"] = paymentRequestFields["SS"];
    }
    responseFields["RES"] = response;

    string signature = createSignature(responseFields);

    responseFields["SIGN"] = signature;

    ResponseEditFormControl.InitialValues = responseFields;
    ResponseEditFormControl.DataBind();
  }

  private string createSignature(Dictionary<string, string> fields)
  {
    StringBuilder sb = new StringBuilder(fields["VS"]);
    if (fields.ContainsKey("SS"))
    {
      sb.Append(fields["SS"]);
    }
    sb.Append(fields["RES"]);

    string signatureBase = sb.ToString();
    string signature = CryptoHelper.GetHmacSignatureInHexdec(signatureBase, EPaymentConstants.VUB_EPlatba2_HMAC_sharedSecret);
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
