<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="residentmanagement.aspx.cs" Inherits="SMS.residentmangament" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });

        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgview').attr('src', e.target.result);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-5 pt-3">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Resident Details</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img id="imgview" width="100" src="imgs/apartmentjpg.jpg" />
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
                                <asp:FileUpload class="form-control" onChange="readURL(this);" ID="FileUpload1" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 pt-3">
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="House"></asp:TextBox>
                                        <asp:LinkButton class="btn btn-primary" ID="LinkButton1" runat="server" Text="Go" OnClick="LinkButton1_Click" CausesValidation="False"></asp:LinkButton>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="House is required" ForeColor="Red" ControlToValidate="TextBox1">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-8 pt-3">
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Full Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Full Name is required" ForeColor="Red" ControlToValidate="TextBox2">*</asp:RequiredFieldValidator>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 pt-0">
                                <label>Date of Birth</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" TextMode="Date"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Date of Birth is required" ForeColor="Red" ControlToValidate="TextBox3">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Gender</label><br />
                                <div class="form-check-inline">
                                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="gender" CssClass="form-check-input" />
                                    <label class="form-check-label" for="RadioButton1">Male</label>
                                </div>


                                <div class="form-check-inline">
                                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="gender" CssClass="form-check-input" />
                                    <label class="form-check-label" for="RadioButton2">Female</label>
                                </div>


                                <div class="form-check-inline">
                                    <asp:RadioButton ID="RadioButton3" runat="server" GroupName="gender" CssClass="form-check-input" />
                                    <label class="form-check-label" for="RadioButton3">Other</label>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Contact No" TextMode="Number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Contact No is required" ForeColor="Red" ControlToValidate="TextBox4">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="Email ID" TextMode="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Email ID is required" ForeColor="Red" ControlToValidate="TextBox5">*</asp:RequiredFieldValidator>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList class="form-control" ID="DropDownList1" runat="server">
                                        <asp:ListItem Text="Owner" Value="Owner" />
                                        <asp:ListItem Text="Tenant" Value="Tenant" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" placeholder="Area (per sqft)" TextMode="Number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Area is required" ForeColor="Red" ControlToValidate="TextBox6">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" placeholder="No of Family" TextMode="Number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="No of Family is required" ForeColor="Red" ControlToValidate="TextBox7">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <span class="badge badge-pill badge-info">Login Credentials</span>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 pt-3">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TextBox8" runat="server" placeholder="Resident ID"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Resident ID is required" ForeColor="Red" ControlToValidate="TextBox8">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6 pt-3">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TextBox9" runat="server" placeholder="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Password is required" ForeColor="Red" ControlToValidate="TextBox9">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-4">
                                <asp:Button ID="Button1" class="btn btn-lg btn-block btn-success" runat="server" Text="Add" OnClick="Button1_Click" />
                            </div>
                            <div class="col-4">
                                <asp:Button ID="Button2" class="btn btn-lg btn-block btn-warning" runat="server" Text="Update" OnClick="Button2_Click" />
                            </div>
                            <div class="col-4">
                                <asp:Button ID="Button3" class="btn btn-lg btn-block btn-danger" runat="server" Text="Delete" OnClick="Button3_Click" CausesValidation="False" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <asp:ValidationSummary CssClass="pt-3" ID="ValidationSummary1" runat="server" />
                            </div>
                        </div>

                    </div>
            </div>
            <a href="homepage.aspx"><< Back to Home</a><br>
            <br>
        </div>
        <div class="col-md-7 pt-3">
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
                                    <asp:BoundField DataField="house" HeaderText="House" SortExpression="house" >
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

                                                                Sqft</div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-12">

                                                                Gender -
                                                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Text='<%# Eval("gender") %>'></asp:Label>
                                                                &nbsp;| Dob -
                                                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Text='<%# Eval("dob") %>'></asp:Label>
                                                                &nbsp;| Contact No. -
                                                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Text='<%# Eval("contact_no") %>'></asp:Label>
                                                                &nbsp;</div>
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
        </div>
    </div>
  </div>

</asp:Content>
