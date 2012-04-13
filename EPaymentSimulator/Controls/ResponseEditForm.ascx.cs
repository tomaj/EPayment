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
using System.Collections.Generic;

public partial class Controls_ResponseEditForm : System.Web.UI.UserControl
{
  //public DictionaryEntry[] InitialValues
  //{
  //  get { return (DictionaryEntry[])ViewState["initialValues"]; }
  //  set { ViewState["initialValues"] = value; }
  //}

  public Dictionary<string, string> InitialValues
  {
    get { return (Dictionary<string, string>)ViewState["initialValues"]; }
    set { ViewState["initialValues"] = value; }
  }

  public Dictionary<string, string> Values
  {
    get { return getValues(); }
  }

  private Dictionary<string, string> getValues()
  {
    Dictionary<string, string> result = new Dictionary<string, string>();

    foreach (RepeaterItem ri in rResponseFields.Items)
    {
      result.Add(((Label)ri.FindControl("lKey")).Text, ((TextBox)ri.FindControl("tbValue")).Text);
    }

    return result;
  }

  #region DataBind event registration
  protected void Page_Init(object sender, EventArgs e)
  {
    DataBinding += new EventHandler(Control_DataBind);
  }
  #endregion

  protected void Control_DataBind(object sender, EventArgs e)
  {
    rResponseFields.DataSource = InitialValues;
  }
}