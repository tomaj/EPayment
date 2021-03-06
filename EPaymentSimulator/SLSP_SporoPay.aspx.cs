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
using System.Collections.Specialized;
using System.Collections.Generic;

public partial class SLSP_SporoPay : System.Web.UI.Page
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

    if (Request.HttpMethod != "GET")
    {
      validationResult = false;
      validationLog.Add(string.Format("Payment request bol prijatý HTTP metódou {0} namiesto metódy GET.", Request.HttpMethod));
    }

    foreach (string field in new string[] { "pu_predcislo", "pu_cislo", "pu_kbanky", "suma", "mena", "vs", "ss", "url", "param", "sign1" })
    {
      if (Request[field] == null)
      {
        validationResult = false;
        validationLog.Add("Chýba parameter " + field + " .");
      }
      else
      {
        string value = Request[field];
        switch (field)
        {
          case "pu_predcislo":
            if (!(new Regex("^(|[0-9]{6})$")).IsMatch(value)) // pod¾a dokumentácie je možné odovzda aj prázdny parameter
            {
              validationResult = false;
              validationLog.Add("Parameter pu_predcislo musí byť 6 numerických znakov alebo prázdny.");
            }
            break;

          case "pu_cislo":
            if (!(new Regex("^[0-9]{10}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Parameter pu_cislo musí byť 10 numerických znakov.");
            }
            break;

          case "pu_kbanky":
            if (!(new Regex("^[0-9]{4}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Parameter pu_kbanky musí byť 4 numerické znaky");
            }
            break;

          case "suma":
            if (!(new Regex(@"^[0-9]+\.[0-9]{2}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Parameter suma musí byť desatinné číslo s desatinnou bodkou, dvoma desatinnými miestami a aspoň jednou cifrou pred desatinnou bodkou.");
            }
            break;

          case "mena":
            if (!(value == "EUR"))
            {
              validationResult = false;
              validationLog.Add("Parameter mena musí byť \"EUR\".");
            }
            break;

          case "vs":
            if (!(new Regex("^[0-9]{10}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Parameter vs musí byť 10 numerických znakov.");
            }
            break;

          case "ss":
            if (!(new Regex("^[0-9]{10}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Parameter ss musí byť 10 numerických znakov.");
            }
            break;

          case "url":
            if (value.IndexOfAny(new char[] { ';', '?', '&' }) != -1)
            {
              validationResult = false;
              validationLog.Add("Parameter url nesmie obsahovať zakázané znaky \";\" \"?\" ani \"&\".");
            }
            if (!(new Regex(@"^https?\://.+$", RegexOptions.IgnoreCase)).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Zlý formát url parametra. Url parameter musí byť valídna URL bez query stringu.");
            }
            break;

          case "param":
            if (value.IndexOfAny(new char[] { ';', '?', '&' }) != -1)
            {
              validationResult = false;
              validationLog.Add("Parameter param nesmie obsahovať zakázané znaky \";\" \"?\" ani \"&\".");
            }
            if (!(new Regex("^.+=.+$")).IsMatch(value)) // v praxi systém akceptuje iba hodnoty vo formáte abc=def
            {
              validationResult = false;
              validationLog.Add("Zlý formát param parametra. Parameter param musí byť string vo formáte kluc=hodnota . Pozor, túto hodnotu je nutné pred odoslaním zaenkódovať na prenos cez URL.");
            }
            break;

          case "sign1":
            if (!(new Regex(@"^[a-zA-Z0-9\+/=]{32}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Zlý formát sign1 parametra. Parameter musí mať hodnotu 32 ASCII znakov znamenajúcich 24 bajtov zakódovaných Base64 encodingom. Pozor, parameter je nutné pred odoslaním zaenkódovať na prenos cez URL.");
            }
            break;
        }
      }
    }

    foreach (string field in new string[] { "acc_prefix", "acc_number", "mail_notify_att", "email_adr", "client_login", "auth_tool_type" })
    {
      if (Request[field] != null)
      {
        string value = Request[field];
        switch (field)
        {
          case "acc_prefix":
            if (!(new Regex("^[0-9]{6}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Nepovinný parameter acc_prefix musí byť šesť numerických znakov.");
            }
            break;

          case "acc_number":
            if (!(new Regex("^[0-9]{10}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Nepovinný parameter acc_number musí byť 10 numerických znakov.");
            }
            break;

          case "mail_notify_att":
            if (!(value == "1" || value == "2" || value == "3"))
            {
              validationResult = false;
              validationLog.Add("Povolené hodnoty nepovinného parametra mail_notify_att sú iba \"1\", \"2\" a \"3\".");
            }
            break;

          case "email_adr":
            if (!(new Regex("^.+@.+$")).IsMatch(value)) // email hádam nieje nutné toľko validovať
            {
              validationResult = false;
              validationLog.Add("Nepovinný parameter email_adr musí byť valídna e-mailová adresa.");
            }
            break;

          case "client_login":
            if (!(new Regex("^[0-9]{9}$")).IsMatch(value))
            {
              validationResult = false;
              validationLog.Add("Nepovinný parameter client_login musí byť 9 numerických znakov.");
            }
            break;

          case "auth_tool_type":
            if (!(value == "1" || value == "2"))
            {
              validationResult = false;
              validationLog.Add("Povolené hodnoty nepovinného parametra auth_tool_type sú iba \"1\" a \"2\".");
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
    string signatureBase = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}",
      Request["pu_predcislo"],
      Request["pu_cislo"],
      Request["pu_kbanky"],
      Request["suma"],
      Request["mena"],
      Request["vs"],
      Request["ss"],
      Request["url"],
      Request["param"]
    );

    PaymentRequestProcessingLogControl.UncryptedSignature = signatureBase;
    string signature = CryptoHelper.Get3DesSignatureInBase64(signatureBase, EPaymentConstants.SLSP_SporoPay_sharedSecret);
    PaymentRequestProcessingLogControl.CryptedSignature = signature;
    PaymentRequestProcessingLogControl.ReceivedSignature = Request["sign1"];

    bool result = (signature == Request["sign1"]);

    PaymentRequestProcessingLogControl.IsVerified = result;

    return result;
  }

  private void initResponseForm()
  {
    DictionaryEntry[] responses = new DictionaryEntry[] {
      new DictionaryEntry("ok", "OK"),
      new DictionaryEntry("tout", "Timeout"),
      new DictionaryEntry("fail", "Fail")
    };

    ddlResponse.DataSource = responses;
    ddlResponse.DataValueField = "Key";
    ddlResponse.DataTextField = "Value";
    ddlResponse.DataBind();

    ddlResponse.Enabled = true;
    btnSendResponse.Enabled = true;
    btnGetResponseUrl.Enabled = true;
    hlResponse.Visible = true;

    createResponse("ok");
  }

  private void createResponse(string response)
  {
    string result = "OK";
    string real = "OK";

    switch (response)
    {
      case "ok":
        result = "OK";
        real = "OK";
        break;

      case "fail":
        result = "NOK";
        real = "NOK";
        break;

      case "tout":
        result = "OK";
        real = "NOK";
        break;
    }
    
    Dictionary<string, string> responseFields = new Dictionary<string, string>();

    if (Request["acc_prefix"] != null) { responseFields.Add("u_predcislo", Request["acc_prefix"]); }
    else { responseFields.Add("u_predcislo", "123456"); }

    if (Request["acc_number"] != null) { responseFields.Add("u_cislo", Request["acc_number"]); }
    else { responseFields.Add("u_cislo", "9876543210"); }

    responseFields.Add("u_kbanky", "0900");
    responseFields.Add("pu_predcislo", EPaymentConstants.SLSP_SporoPay_pu_predcislo);
    responseFields.Add("pu_cislo", EPaymentConstants.SLSP_SporoPay_pu_cislo);
    responseFields.Add("pu_kbanky", EPaymentConstants.SLSP_SporoPay_pu_kbanky);
    responseFields.Add("suma", Request["suma"]);
    responseFields.Add("mena", Request["mena"]);
    responseFields.Add("vs", Request["vs"]);
    responseFields.Add("ss", Request["ss"]);
    responseFields.Add("url", Request["url"]);
    responseFields.Add("param", Request["param"]);
    responseFields.Add("result", result);
    responseFields.Add("real", real);

    string signature = createSignature(responseFields);

    responseFields.Add("SIGN2", signature);

    ResponseEditFormControl.InitialValues = responseFields;
    ResponseEditFormControl.DataBind();
  }
  
  private string createSignature(Dictionary<string, string> fields)
  {
    string signatureBase = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13}",
      fields["u_predcislo"],
      fields["u_cislo"],
      fields["u_kbanky"],
      fields["pu_predcislo"],
      fields["pu_cislo"],
      fields["pu_kbanky"],
      fields["suma"],
      fields["mena"],
      fields["vs"],
      fields["ss"],
      fields["url"],
      fields["param"],
      fields["result"],
      fields["real"]
    );
    string signature = CryptoHelper.Get3DesSignatureInBase64(signatureBase, EPaymentConstants.SLSP_SporoPay_sharedSecret);
    return signature;
  }

  protected void ddlResponse_SelectedIndexChanged(Object sender, EventArgs args)
  {
    createResponse(ddlResponse.SelectedValue);
  }

  private string getResponseUrl()
  {
    string url = Common.CreateHttpGetResponseUrl(Request["url"], ResponseEditFormControl.Values);
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
