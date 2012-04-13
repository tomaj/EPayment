<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ResponseEditForm.ascx.cs" Inherits="Controls_ResponseEditForm" %>

<%--
  Copyright 2009 MONOGRAM Technologies
  
  This file is part of MONOGRAM EPayment libraries
  
  MONOGRAM EPayment libraries is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  MONOGRAM EPayment libraries is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with MONOGRAM EPayment libraries.  If not, see <http://www.gnu.org/licenses/>.
--%>

<asp:Repeater ID="rResponseFields" runat="server">
  <HeaderTemplate>
    <table>
  </HeaderTemplate>
  <ItemTemplate>
    <tr>
      <td><asp:Label ID="lKey" runat="server" Text="<%# ((System.Collections.Generic.KeyValuePair<string, string>)Container.DataItem).Key %>" />:</td>
      <td><asp:TextBox ID="tbValue" runat="server" Text="<%# ((System.Collections.Generic.KeyValuePair<string, string>)Container.DataItem).Value %>" /></td>
    </tr>
  </ItemTemplate>
  <FooterTemplate>
    </table>
  </FooterTemplate>
</asp:Repeater>