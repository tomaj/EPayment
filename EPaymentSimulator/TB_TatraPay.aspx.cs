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
using System.Text.RegularExpressions;
using System.Text;

public partial class TB_TatraPay : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
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
      validationLog.Add(string.Format("Payment request bol prijatý HTTP metódou {0} namiesto metódy GET.", Request.HttpMethod));
    }

    foreach (string field in new string[] { "MID", "AMT", "CURR", "VS", "CS", "RURL", "SIGN" })
    {
      string value = Request[field];
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
            if (!(new Regex("^.{3,4}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Parameter MID musí byť dlhý 3 alebo 4 znaky.");
            }
            break;

          case "AMT":
            if (!(new Regex(@"^([0-9]{1,13}|[0-9]{0,13}\.[0-9]{1,2})$")).IsMatch(value)) // je dost mozne, ze celkova dlzka moze byt max 15 znakov a nie 16 - specifikacia je nejednoznacna
            {
              validationResult = false;
              validationLog.Add("Parameter AMT musí byčíslo s maximálne dvoma desatinnými miestami a max 13 číslicami pred desatinnou bodkou. Ako oddeľovač desatinných miest sa používa znak \".\" .");
            }
            break;

          case "CURR":
            if (!(value == "978" || value == "703")) // EUR(978), specifikacia hovori, ze do prechodu na EUR je povinna hodnota SKK(703) ale nie, ci je po prechode povinna hodnota EUR
            {
              validationResult = false;
              validationLog.Add("Parameter CURR musí byť \"978\" (kód meny Euro podľa ISO 4217).");
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
            if (!((new Regex(@"^https?\://")).IsMatch(value)) || value.Contains("?"))
            {
              validationResult = false;
              validationLog.Add("Parameter RURL musí byť platná URL adresa bez query stringu");
            }
            break;

          case "SIGN":
            if (!(new Regex("^[0-9A-F]{16}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Parameter sign musí byť dlhý 16 znakov a byť zložený z numerických znakov a znakov A-F .");
            }
            break;
        }
      }
    }

    foreach (string field in new string[] { "PT", "SS", "RSMS", "REM", "DESC", "AREDIR", "LANG" })
    {
      if (Request[field] != null)
      {
        string value = Request[field];
        switch (field)
        {
          case "PT":
            if (value != "TatraPay")
            {
              validationResult = false;
              validationLog.Add("Nepovinný parameter PT môže nadobúdať iba hodnotu \"TatraPay\" .");
            }
            break;

          case "SS":
            if (!(new Regex("^[0-9]{1,10}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Nepovinný parameter SS môže byť zložený iba z maximálne 10 numerických znakov.");
            }
            break;

          case "RSMS":
            if (!(new Regex(@"^(|0|\+421)9([0-9] ?){8}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Nepovinný parameter RSMS musí byť v tvare \"\", \"0\" alebo \"+421\" s následujúcimi ôsmimi numerickými znakmi.");
            }
            break;

          case "REM":
            if (
              !value.Contains("@") ||
              value.Length < 6 ||
              value.Length > 35 ||
              value[0] == '@' ||
              value[value.Length - 1] == '@' ||
              value.Contains("..")
            )
            {
              validationResult = false;
              validationLog.Add("Nepovinný parameter REM musí byť valídna e-mailová adresa o dĺžke minimálne 6 a maximálne 35 znakov.");
            }
            break;

          case "DESC":
            if (!(new Regex("^(.| ){1,20}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Nepovinný parameter DESC nesmie obsahovať diakritiku a byť dlhší ako 20 znakov.");
            }
            break;

          case "AREDIR":
            if (!(value == "1" || value == "0"))
            {
              validationResult = false;
              validationLog.Add("Povolené hodnoty nepovinného parametra AREDIR sú iba \"0\" a \"1\".");
            }
            break;

          case "LANG":
            if (!(value == "sk" || value == "en" || value == "de" || value == "hu"))
            {
              validationResult = false;
              validationLog.Add("Povolené hodnoty nepovinného parametra LANG sú iba \"sk\", \"en\", \"de\" a \"hu\".");
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
    sb.Append(Request["AMT"]);
    sb.Append(Request["CURR"]);
    sb.Append(Request["VS"]);
    if (Request["SS"] != null)
      sb.Append(Request["SS"]);
    sb.Append(Request["CS"]);
    sb.Append(Request["RURL"]);

    string signatureBase = sb.ToString();
    PaymentRequestProcessingLogControl.UncryptedSignature = signatureBase;
    string signature = CryptoHelper.GetDesSignatureInHexdec(signatureBase, EPaymentConstants.TB_TatraPay_sharedSecret);
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
      new DictionaryEntry("FAIL", "Fail"),
      new DictionaryEntry("TOUT", "Timeout")
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

    responseFields["VS"] = Request["VS"];
    if (Request["SS"] != null)
      responseFields["SS"] = Request["SS"];
    responseFields["RES"] = response;

    string signature = createSignature(responseFields);

    responseFields["SIGN"] = signature;

    ResponseEditFormControl.InitialValues = responseFields;
    ResponseEditFormControl.DataBind();
  }

  private string createSignature(Dictionary<string, string> fields)
  {
    string signatureBase = null;
    if (fields.ContainsKey("SS"))
    {
      signatureBase = string.Format("{0}{1}{2}", fields["VS"], fields["SS"], fields["RES"]);
    }
    else
    {
      signatureBase = string.Format("{0}{1}", fields["VS"], fields["RES"]);
    }

    string signature = CryptoHelper.GetDesSignatureInHexdec(signatureBase, EPaymentConstants.TB_TatraPay_sharedSecret);
    return signature;
  }

  protected void ddlResponse_SelectedIndexChanged(Object sender, EventArgs args)
  {
    createResponse(ddlResponse.SelectedValue);
  }

  private string getResponseUrl()
  {
    string url = Common.CreateHttpGetResponseUrl(Request["RURL"], ResponseEditFormControl.Values);
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
