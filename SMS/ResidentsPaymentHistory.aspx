<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ResidentsPaymentHistory.aspx.cs" Inherits="SMS.ResidentsPaymentHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-8 mx-auto pt-3">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>My Payments</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="PaymentID" HeaderText="ID" />
                                            <asp:BoundField DataField="MonthPaid" HeaderText="Month Paid" />
                                            <asp:BoundField DataField="AmountPaid" HeaderText="Amount Paid" />
                                            <asp:BoundField DataField="ModeofPayment" HeaderText="Mode of Payment" />
                                            <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <a href="homepage.aspx"><< Back to Home</a><br>
                <br>
            </div>
        </div>
    </div>
</asp:Content>
