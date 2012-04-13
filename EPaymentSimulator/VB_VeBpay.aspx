<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VB_VeBpay.aspx.cs" Inherits="VB_VeBpay" MasterPageFile="~/MasterPage.master" %>
<%@ Register Src="~/Controls/PaymentRequestProcessingLog.ascx" TagPrefix="uc" TagName="PaymentRequestProcessingLog" %>
<%@ Register Src="~/Controls/ResponseEditForm.ascx" TagPrefix="uc" TagName="ResponseEditForm" %>

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

<asp:Content ContentPlaceHolderID="MainContentPlace" runat="server">
  <h2>VB VeBpay</h2>
  
  <uc:PaymentRequestProcessingLog runat="server" ID="PaymentRequestProcessingLogControl" />
  
  <hr />
  
  <asp:DropDownList ID="ddlResponse" runat="server" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlResponse_SelectedIndexChanged" /> <br />
  
  <uc:ResponseEditForm runat="server" ID="ResponseEditFormControl" /> <br />
  
  <asp:Button ID="btnGetResponseUrl" runat="server" Enabled="false" Text="Vyrobi odpoveï (url)" OnClick="btnGetResponseUrl_Click" /> <br />
  
  <asp:HyperLink ID="hlResponse" runat="server" Visible="false" />
  
  <br /><br />
  <asp:Button ID="btnSendResponse" runat="server" Enabled="false" Text="Odosla odpoveï" OnClick="btnSendResponse_Click" />
</asp:Content>