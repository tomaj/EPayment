<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UCB_UniPlatba.aspx.cs" Inherits="UCB_UniPlatba" MasterPageFile="~/PaymentResponse.master" %>
<%@ Register Src="~/Controls/CommonPaymentResponseForm.ascx" TagName="CommonPaymentResponseForm" TagPrefix="uc" %>

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

<asp:Content runat="server" ContentPlaceHolderID="MainPlaceHolder">
  <uc:CommonPaymentResponseForm runat="server" PaymentType="UCB_UniPlatba" />
</asp:Content>