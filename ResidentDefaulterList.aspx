<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ResidentDefaulterList.aspx.cs" Inherits="SMS.ResidentDefaulterList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });

        function SelectAllCheckBox_CheckedChanged(checkbox) {
            var gridView = document.getElementById('<%= GridView1.ClientID %>');
            var rowCheckBoxes = gridView.getElementsByTagName('input');
            for (var i = 0; i < rowCheckBoxes.length; i++) {
                if (rowCheckBoxes[i].type === 'checkbox') {
                    rowCheckBoxes[i].checked = checkbox.checked;
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-7 mx-auto pt-3">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Defaulters List</h4>
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
                                <asp:LinkButton class="btn btn-primary" ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"><i class="fa-solid fa-rotate-right"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="House" HeaderText="House" />
                                        <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                                        <asp:BoundField DataField="ResidentType" HeaderText="Resident Type" />
                                        <asp:BoundField DataField="TotalDueAmount" HeaderText="Total Due Amount" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <a href="homepage.aspx"><< Back to Home</a><br>
        <br>
    </div>
</asp:Content>
