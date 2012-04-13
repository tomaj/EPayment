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
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;

public partial class UCB_UniPlatba : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      if (Request.UrlReferrer != null)
      {
        ViewState["UCB_Referer"] = Request.UrlReferrer.Scheme + "://" + Request.UrlReferrer.Host + ":" + Request.UrlReferrer.Port + Request.UrlReferrer.AbsolutePath.ToString();
      }

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

  private bool validatePaymentRequest()
  {
    bool validationResult = true;
    ArrayList validationLog = new ArrayList();

    if (!(Request.HttpMethod == "GET"))
    {
        validationResult = false;
        validationLog.Add(string.Format("Payment request bol prijat˝ HTTP metÛdou {0} namiesto metÛdy GET.", Request.HttpMethod));
    }

    foreach (string field in new string[] {"MID", "LNG", "AMT", "VS", "CS", "SIGN"})
    {
      string value = Request[field];
      if (value == null)
      {
        validationResult = false;
        validationLog.Add(string.Format("Ch˝ba parameter {0}", field));
      }
      else
      {
        switch (field)
        {
          case "MID":
            if (!(new Regex(@"^[0-9]{1,10}$")).IsMatch(value))
            {
                validationResult = false;
                validationLog.Add("Parameter MID musÌ byù celÈ ËÌslo o maxim·lne desiatich ËÌsliciach.");
            }
            break;

          case "LNG":
            if (!(new Regex(@"^(SK|EN)$")).IsMatch(value))
            {
                validationResult = false;
                validationLog.Add("Parameter LNG musÌ byù \"SK\" alebo \"EN\".");
            }
            break;

          case "AMT":
            if (!(new Regex(@"^[0-9]{1,13}\.[0-9]{2}$")).IsMatch(value))
            {
                validationResult = false;
                validationLog.Add("Parameter AMT musÌ byù desatinnÈ ËÌslo (pouûÌvaj˙ce bodku ako oddeæovaË desatinn˝ch miest) s maxim·lne 13 ciframi pred desatinnou bodkou a 2 ciframi za desatinnou bodkou.");
            }
            break;

          case "VS":
            if (!(new Regex(@"^[0-9]{1,10}$")).IsMatch(value))
            {
                validationResult = false;
                validationLog.Add("Parameter VS musÌ pozost·vaù iba z numerick˝ch znakov a nesmie byù dlhöÌ ako 10 znakov.");
            }
            break;

          case "CS":
            if (!(new Regex(@"^[0-9]{4}$")).IsMatch(value))
            {
                validationResult = false;
                validationLog.Add("Parameter CS musÌ pozost·vaù zo 4 ËÌslic.");
            }
            break;

          case "SIGN":
            if (!(new Regex("^[0-9A-F]{16}$")).IsMatch(value))
            {
                validationResult = false;
                validationLog.Add("Parameter sign musÌ byù dlh˝ 16 znakov a byù zloûen˝ z numerick˝ch znakov a znakov A-F .");
            }
            break;
        }
      }
    }

    foreach (string field in new string[] {"SS", "DESC"})
    {
      string value = Request[field];
      if (value != null)
      {
        switch (field)
        {
          case "SS":
            if (!(new Regex(@"^[0-9]{1,10}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Parameter SS musÌ pozost·vaù iba z numerick˝ch znakov a nesmie byù dlhöÌ ako 10 znakov.");
            }
            break;

          case "DESC":
            if ((value.IndexOfAny(new char[] {' ', '\t', '\r', '\n'}) >= 0) || (value.Length > 35))
            {
              validationResult = false;
              validationLog.Add("Parameter DESC nesmie obsahovaù whitespace znaky a nesmie byù dlhöÌ ako 35 znakov.");
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
    StringBuilder sb = new StringBuilder();
    sb.Append(Request["MID"]);
    sb.Append(Request["LNG"]);
    sb.Append(Request["AMT"]);
    sb.Append(Request["VS"]);
    sb.Append(Request["CS"]);
    sb.Append(Request["SS"]);
    sb.Append(Request["DESC"]);

    string signatureBase = sb.ToString();
    PaymentRequestProcessingLogControl.UncryptedSignature = signatureBase;
    string signature = CryptoHelper.GetDesSignatureInHexdec(signatureBase, EPaymentConstants.UCB_UniPlatba_sharedSecret);
    PaymentRequestProcessingLogControl.CryptedSignature = signature;
    PaymentRequestProcessingLogControl.ReceivedSignature = Request["SIGN"];

    bool result = (signature == Request["SIGN"]);
    PaymentRequestProcessingLogControl.IsVerified = result;

    return result;
  }

  private void initResponseForm()
  {
    DictionaryEntry[] responses = new DictionaryEntry[] {
      new DictionaryEntry("OK", "OK"),
      new DictionaryEntry("NO", "NO")
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

    Dictionary<string, string> currentValues = ResponseEditFormControl.Values;
    if (!currentValues.ContainsKey("RURL") || string.IsNullOrEmpty(currentValues["RURL"]))
    {
      responseFields["RURL"] = "";
      if (!string.IsNullOrEmpty((string)ViewState["UCB_Referer"]))
      {
        responseFields["RURL"] = (string)ViewState["UCB_Referer"];
      }
    }
    else
    {
      responseFields["RURL"] = currentValues["RURL"];
    }

    responseFields["VS"] = Request["VS"];
    responseFields["RES"] = response;
    responseFields["SIGN"] = createSignature(responseFields);

    ResponseEditFormControl.InitialValues = responseFields;
    ResponseEditFormControl.DataBind();
  }

  private string createSignature(Dictionary<string, string> fields)
  {
    string signatureBase = string.Format("{0}{1}", fields["VS"], fields["RES"]);
    string signature = CryptoHelper.GetDesSignatureInHexdec(signatureBase, EPaymentConstants.UCB_UniPlatba_sharedSecret);
    return signature;
  }

  protected void ddlResponse_SelectedIndexChanged(Object sender, EventArgs args)
  {
    createResponse(ddlResponse.SelectedValue);
  }

  private string getResponseUrl()
  {
    Dictionary<string, string> values = ResponseEditFormControl.Values;
    string baseUrl = values["RURL"];
    values.Remove("RURL");

    string url = Common.CreateHttpGetResponseUrl(baseUrl, values);
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
