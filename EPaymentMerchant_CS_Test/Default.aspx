<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Src="~/Controls/SLSPSporoPayRequestForm.ascx" TagName="SLSPSporoPayRequestForm" TagPrefix="uc" %>
<%@ Register Src="~/Controls/TBTatraPayRequestForm.ascx" TagName="TBTatraPayRequestForm" TagPrefix="uc" %>
<%@ Register Src="~/Controls/TBCardPayRequestForm.ascx" TagName="TBCardPayRequestForm" TagPrefix="uc" %>
<%@ Register Src="~/Controls/VUBEPlatbaRequestForm.ascx" TagName="VUBEPlatbaRequestForm" TagPrefix="uc" %>
<%@ Register Src="~/Controls/UCBUniPlatbaRequestForm.ascx" TagName="UCBUniPlatbaRequestForm" TagPrefix="uc" %>
<%@ Register Src="~/Controls/PaymentRequestCreationLog.ascx" TagName="PaymentRequestCreationLog" TagPrefix="uc" %>
<%@ Register Src="~/Controls/VUBEPlatba2HMACRequestForm.ascx" TagName="VUBEPlatba2HMACRequestForm" TagPrefix="uc" %>

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

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
  <title>EPayment C# merchant test application</title>
</head>
<body>
  <form id="form1" runat="server">
  <div>
    <table>
      <tr>
        <td>Payment type:</td>
        <td><asp:DropDownList ID="ddlPaymentType" runat="server" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged" AutoPostBack="true" /></td>
      </tr>
      <tr>
        <td>Amount:</td>
        <td>&euro; <asp:TextBox ID="tbAmount" runat="server" /></td>
      </tr>
      <tr>
        <td>VariableSymbol:</td>
        <td><asp:TextBox ID="tbVariableSymbol" runat="server" /></td>
      </tr>
    </table>
    <uc:SLSPSporoPayRequestForm ID="SLSPSporoPayRequestFormControl" runat="server" Visible="false" />
    <uc:TBTatraPayRequestForm ID="TBTatraPayRequestFormControl" runat="server" Visible="false" />
    <uc:TBCardPayRequestForm ID="TBCardPayRequestFormControl" runat="server" Visible="false" />
    <uc:VUBEPlatbaRequestForm ID="VUBEPlatbaRequestFormControl" runat="server" Visible="false" />
    <uc:UCBUniPlatbaRequestForm ID="UCBUniPlatbaRequestFormControl" runat="server" Visible="false" />
    <uc:VUBEPlatba2HMACRequestForm ID="VUBEPlatba2HMACRequestFormControl" runat="server" Visible="false" />
    <table>
      <tr>
        <td>Shared secret:</td>
        <td><asp:TextBox ID="tbSharedSecret" runat="server" /></td>
      </tr>
    </table>
    <asp:Button ID="btnSendRequest" runat="server" OnClick="btnSendRequest_Click" Text="Create payment request" />
    <br />
    <uc:PaymentRequestCreationLog ID="PaymentRequestCreationLogControl" runat="server" />
    <br />
    <asp:HyperLink ID="hlPaymentRequest" runat="server" Visible="false" />
  </div>
  </form>
  
  <p><% renderSendFormIfNecessary(Response.Output); %></p>
</body>
</html>
