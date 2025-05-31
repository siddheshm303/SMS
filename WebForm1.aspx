<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SMS.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container p-5">
        <div class="progress">
            <%-- Progress bar for total due amount --%>
            <%
                // Calculate the percentages
                decimal totalDueAmount = string.IsNullOrEmpty(TextBox8.Text) ? 0 : Convert.ToDecimal(TextBox8.Text);
                decimal totalPaidAmount = string.IsNullOrEmpty(TextBox2.Text) ? 0 : Convert.ToDecimal(TextBox2.Text);
                decimal totalAmount = totalDueAmount + totalPaidAmount;

                // Calculate percentages
                decimal dueAmountPercentage = totalAmount == 0 ? 0 : (totalDueAmount / totalAmount) * 100;
                decimal paidAmountPercentage = totalAmount == 0 ? 0 : (totalPaidAmount / totalAmount) * 100;
            %>
            <div class="progress-bar bg-danger" role="progressbar" style="width: <%=dueAmountPercentage.ToString("0")%>%" aria-valuemin="0" aria-valuemax="100"></div>
            <div class="progress-bar" role="progressbar" style="width: <%=paidAmountPercentage.ToString("0")%>%" aria-valuemin="0" aria-valuemax="100"></div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <label>Total Due Amount</label>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="TextBox8" runat="server" ReadOnly="True" ForeColor="Red" BorderColor="Red"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6">
                <label>Balance Amount</label>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" ReadOnly="True"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
