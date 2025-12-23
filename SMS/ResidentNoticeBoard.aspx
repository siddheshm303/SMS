<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ResidentNoticeBoard.aspx.cs" Inherits="SMS.ResidentNoticeBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
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
                                    <h4>Notices</h4>
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
                                <asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="NoticeID" HeaderText="ID" ReadOnly="True" SortExpression="NoticeID">
                                            <HeaderStyle Font-Bold="True" />
                                            <ItemStyle Font-Bold="True" />
                                        </asp:BoundField>
                                        <asp:TemplateField>

                                            <ItemTemplate>
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-lg-8">
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Text='<%# Eval("Title") %>'></asp:Label>
                                                            <asp:Label ID="Label4" runat="server" Text="("></asp:Label>
                                                            <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("House").ToString() == "--" ? " Everyone" : " Personal" %>'></asp:Literal>
                                                            <asp:Label ID="Label5" runat="server" Text=")"></asp:Label>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Date", "{0:D}") %>' Font-Bold="True" Font-Size="Large"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col">
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Description") %>' Font-Size="Larger"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                            </ItemTemplate>

                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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
