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
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class Common
{
  public static string CreateHttpGetResponseUrl(string baseUrl, Dictionary<string, string> fields)
  {
    StringBuilder result = new StringBuilder(baseUrl);

    bool first = baseUrl.IndexOf('?') < 0;
    foreach (KeyValuePair<string, string> entry in fields)
    {
      result.AppendFormat("{0}{1}={2}",
        (first?'?':'&'),
        HttpUtility.UrlEncode(entry.Key),
        HttpUtility.UrlEncode(entry.Value)
      );
      first = false;
    }
    return result.ToString();
  }

  public static string GetUrl(System.Web.UI.Page control, string relativePath)
  {
    string protocol = control.Page.Request.IsSecureConnection ? "https" : "http";

    string host = control.Page.Request.Url.Host;

    if (!((protocol == "http" && control.Page.Request.Url.Port == 80) || (protocol == "https" && control.Page.Request.Url.Port == 443)))
    {
      host += ":" + control.Page.Request.Url.Port;
    }

    return string.Format("{0}://{1}{2}",
      protocol,
      host,
      control.ResolveUrl(relativePath)
    );
  }
}
