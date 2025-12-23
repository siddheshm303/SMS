<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ResidentsView.aspx.cs" Inherits="SMS.ResidentsView" %>

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
                                    <h4>Residents List</h4>
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
                                <asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="resident_id" DataSourceID="SqlDataSource1">
                                    <Columns>
                                        <asp:BoundField DataField="house" HeaderText="House" SortExpression="house">
                                            <ItemStyle Font-Bold="True" />
                                        </asp:BoundField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-lg-9">
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("full_name") %>' Font-Bold="True" Font-Size="X-Large"></asp:Label><asp:Label ID="Label2" runat="server" Text=" (" Font-Bold="True" Font-Size="X-Large"></asp:Label><asp:Label ID="Label4" runat="server" Text='<%# Eval("type") %>' Font-Bold="True" Font-Size="X-Large"></asp:Label><asp:Label ID="Label3" runat="server" Text=")" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-12">

                                                                    <meta charset="utf-8" />
                                                                    <meta charset="utf-8" />
                                                                    ID -
                                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Text='<%# Eval("resident_id") %>'></asp:Label>
                                                                    &nbsp;| Area -&nbsp;
                                                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Text='<%# Eval("area") %>'></asp:Label>

                                                                    Sqft
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    Gender -
                                                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Text='<%# Eval("gender") %>'></asp:Label>
                                                                    &nbsp;| Dob -
                                                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Text='<%# Eval("dob") %>'></asp:Label>
                                                                    &nbsp;| Contact No. -
                                                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Text='<%# Eval("contact_no") %>'></asp:Label>
                                                                    &nbsp;
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    Email -
                                                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Text='<%# Eval("email") %>'></asp:Label>
                                                                    &nbsp;| No of Family -
                                                                <asp:Label ID="Label11" runat="server" Font-Bold="True" Text='<%# Eval("no_of_family") %>'></asp:Label>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3">
                                                            <asp:Image ID="Image1" class="img-fluid" runat="server" ImageUrl='<%# Eval("img_link") %>' />
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=DESKTOP-RLBCU7R\SQLEXPRESS;Initial Catalog=sms;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [resident_tbl]"></asp:SqlDataSource>

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
