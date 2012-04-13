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

/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
  public static Random Random = new Random();

  public static string GetUrl(System.Web.UI.UserControl control, string relativePath)
  {
    string protocol = control.Page.Request.IsSecureConnection ? "https" : "http";

    string host = control.Page.Request.Url.Host;
    
    if (!((protocol == "http" && control.Page.Request.Url.Port == 80) || (protocol=="https" && control.Page.Request.Url.Port == 443)))
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
