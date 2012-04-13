<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TBTatraPayRequestForm.ascx.cs" Inherits="Controls_TBTatraPayRequestForm" %>

<%--
  Copyright 2009 MONOGRAM Technologies
  
  This file is part of MONOGRAM EPayment libraries
  
  MONOGRAM EPayment libraries is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  MONOGRAM EPayment libraries is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public License
  along with MONOGRAM EPayment libraries.  If not, see <http://www.gnu.org/licenses/>.
--%>

<table>
  <tr>
    <td>mid:</td>
    <td><asp:TextBox ID="tbMid" runat="server" /></td>
  </tr>
  <tr>
    <td>cs:</td>
    <td><asp:TextBox ID="tbCs" runat="server" /></td>
  </tr>
  <tr>
    <td>rurl:</td>
    <td><asp:TextBox ID="tbRurl" runat="server" /></td>
  </tr>
  <tr>
    <td>pt:</td>
    <td><asp:CheckBox ID="chbPt" runat="server" /> <asp:DropDownList ID="ddlPt" runat="server" /></td>
  </tr>
  <tr>
    <td>ss:</td>
    <td><asp:CheckBox ID="chbSs" runat="server" /> <asp:TextBox ID="tbSs" runat="server" /></td>
  </tr>
  <tr>
    <td>rsms:</td>
    <td><asp:CheckBox ID="chbRsms" runat="server" /> <asp:TextBox ID="tbRsms" runat="server" /></td>
  </tr>
  <tr>
    <td>rem:</td>
    <td><asp:CheckBox ID="chbRem" runat="server" /> <asp:TextBox ID="tbRem" runat="server" /></td>
  </tr>
  <tr>
    <td>desc:</td>
    <td><asp:CheckBox ID="chbDesc" runat="server" /> <asp:TextBox ID="tbDesc" runat="server" /></td>
  </tr>
  <tr>
    <td>aredir:</td>
    <td><asp:CheckBox ID="chbAredir" runat="server" /> <asp:DropDownList ID="ddlAredir" runat="server" /></td>
  </tr>
  <tr>
    <td>lang:</td>
    <td><asp:CheckBox ID="chbLang" runat="server" /> <asp:DropDownList ID="ddlLang" runat="server" /></td>
  </tr>
</table>