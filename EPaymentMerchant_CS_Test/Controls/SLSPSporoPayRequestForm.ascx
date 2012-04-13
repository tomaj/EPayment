<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SLSPSporoPayRequestForm.ascx.cs" Inherits="Controls_SLSPSporoPayRequestForm" %>

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
    <td>pu_predcislo:</td>
    <td><asp:TextBox ID="tbPu_predcislo" runat="server" /></td>
  </tr>
  <tr>
    <td>pu_cislo:</td>
    <td><asp:TextBox ID="tbPu_cislo" runat="server" /></td>
  </tr>
  <tr>
    <td>pu_kbanky:</td>
    <td><asp:TextBox ID="tbPu_kbanky" runat="server" Text="0900" Enabled="false" /></td>
  </tr>
  <tr>
    <td>ss:</td>
    <td><asp:TextBox ID="tbSs" runat="server" /></td>
  </tr>
  <tr>
    <td>param:</td>
    <td><asp:TextBox ID="tbParam" runat="server" /></td>
  </tr>
  <tr>
    <td>url:</td>
    <td><asp:TextBox ID="tbUrl" runat="server" /></td>
  </tr>
  <tr>
    <td>acc_prefix:</td>
    <td><asp:CheckBox ID="chbAcc_prefix" runat="server" /> <asp:TextBox ID="tbAcc_prefix" runat="server" /></td>
  </tr>
  <tr>
    <td>acc_number:</td>
    <td><asp:CheckBox ID="chbAcc_number" runat="server" /> <asp:TextBox ID="tbAcc_number" runat="server" /></td>
  </tr>
  <tr>
    <td>mail_notify_att:</td>
    <td><asp:CheckBox ID="chbMail_notify_att" runat="server" /> <asp:DropDownList ID="ddlMail_notify_att" runat="server" /></td>
  </tr>
  <tr>
    <td>email_adr:</td>
    <td><asp:CheckBox ID="chbEmail_adr" runat="server" /> <asp:TextBox ID="tbEmail_adr" runat="server" /></td>
  </tr>
  <tr>
    <td>client_login:</td>
    <td><asp:CheckBox ID="chbClient_login" runat="server" /> <asp:TextBox ID="tbClient_login" runat="server" /></td>
  </tr>
  <tr>
    <td>auth_tool_type:</td>
    <td><asp:CheckBox ID="chbAuth_tool_type" runat="server" /> <asp:DropDownList ID="ddlAuth_tool_type" runat="server" /></td>
  </tr>
</table>