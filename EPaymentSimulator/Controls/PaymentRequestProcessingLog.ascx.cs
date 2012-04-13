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

public partial class Controls_PaymentRequestProcessingLog : System.Web.UI.UserControl
{
  private bool isValidated = false;
  public bool IsValidated
  {
    get { return isValidated; }
    set { isValidated = value; }
  }

  private string[] validationLog = null;
  public string[] ValidationLog
  {
    get { return validationLog; }
    set { validationLog = value; }
  }

  private bool isVerified = false;
  public bool IsVerified
  {
    get { return isVerified; }
    set { isVerified = value; }
  }

  private string uncryptedSignature = String.Empty;
  public string UncryptedSignature
  {
    get { return uncryptedSignature; }
    set { uncryptedSignature = value; }
  }

  private string cryptedSignature = String.Empty;
  public string CryptedSignature
  {
    get { return cryptedSignature; }
    set { cryptedSignature = value; }
  }

  private string receivedSignature = String.Empty;
  public string ReceivedSignature
  {
    get { return receivedSignature; }
    set { receivedSignature = value; }
  }


  #region DataBind event registration
  protected void Page_Init(object sender, EventArgs e)
  {
    DataBinding += new EventHandler(Control_DataBind);
  }
  #endregion

  protected void Control_DataBind(object sender, EventArgs e)
  {
    if (isValidated)
    {
      lValidationResult.Text = "OK";
    }
    else
    {
      lValidationResult.Text = "Fail";
    }

    if (validationLog != null)
    {
      dValidationFailureMessages.Visible = true;
      rValidationFailureMessages.DataSource = validationLog;
    }
    else
    {
      dValidationFailureMessages.Visible = false;
    }
    
    if (isValidated)
    {
      dVerificationLog.Visible = true;
      if (isVerified)
      {
        lVerificationResult.Text = "OK";
      }
      else
      {
        lVerificationResult.Text = "Fail";
      }

      lVerificationUncryptedSignature.Text = HttpUtility.HtmlEncode(uncryptedSignature);
      lVerificationSignature.Text = HttpUtility.HtmlEncode(cryptedSignature);
      lVerificationReceivedSignature.Text = HttpUtility.HtmlEncode(receivedSignature);
    }
    else
    {
      lVerificationResult.Text = "Správa nebola verifikovaná kvôli zlyhaniu validácie.";
      dVerificationLog.Visible = false;
    }
  }
}