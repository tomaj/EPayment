<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCBUniPlatbaRequestForm.ascx.cs" Inherits="Controls_UCBUniPlatbaRequestForm" %>

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
    <td>lng:</td>
    <td><asp:DropDownList ID="ddlLng" runat="server" /></td>
  </tr>
  <tr>
    <td>cs:</td>
    <td><asp:TextBox ID="tbCs" runat="server" /></td>
  </tr>
  <tr>
    <td>ss:</td>
    <td><asp:CheckBox ID="chbSs" runat="server" /> <asp:TextBox ID="tbSs" runat="server" /></td>
  </tr>
  <tr>
    <td>desc:</td>
    <td><asp:CheckBox ID="chbDesc" runat="server" /> <asp:TextBox ID="tbDesc" runat="server" /></td>
  </tr>
</table>