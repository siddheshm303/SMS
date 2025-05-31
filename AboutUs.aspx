<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="SMS.AboutUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="breadcrumb-section" style="background-color: #e9ecef">
        <div class="container p-1 p-sm-3">
            <div class="row">
                <div class="col-12">
                    <h1>About Us</h1>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item">
                            <a href="homepage.aspx">Home</a>
                        </li>
                        <li class="breadcrumb-item active">About Us
                        </li>
                    </ol>
                </div>
            </div>
        </div>
    </section>
    <section class="about-company-section">
        <div class="container p-1 p-sm-3">
            <div class="row">
                <div class="col-12 text-center">
                    <h2>About SMS</h2>
                    <hr />
                </div>

                <div class="col-md-3">
                    <img class="img-fluid" src="imgs/building.png" />
                </div>

                <div class="col-md-9 text-justify">
                    <p>
                        At Society Management System (SMS), we are committed to revolutionizing the way residential societies are managed. Our digital platform is designed to streamline administrative tasks, enhance communication among residents, and promote transparency within the community.
                    </p>
                    <p>
                        Society Management System (SMS) is an innovative web-based platform developed to simplify and modernize residential society management. Our goal is to empower administrators and residents alike by providing user-friendly tools for efficient maintenance operations, transparent financial transactions, and seamless communication. By embracing digital innovation, SMS aims to create a more harmonious and engaging residential community experience.

                    </p>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-6 text-center">
                    <h3>Our Mission</h3>
                </div>
                <div class="col-6 text-center">
                    <h3>Our Vision</h3>
                </div>
            </div>
            <div class="row">
                <div class="col-6 text-justify">
                    <p>
                        At Society Management System, our mission is to modernize residential society management by providing innovative digital solutions that enhance efficiency, transparency, and community engagement.
                    </p>
                </div>
                <div class="col-6 text-justify">
                    <p>
                        We envision a future where society living is seamless and harmonious, enabled by technology that simplifies administrative tasks, fosters communication, and promotes a sense of belonging.
                    </p>
                </div>
            </div>
        </div>
    </section>
    <section class="contactus" style="background-color: #e9ecef">
        <div class="container p-1 p-sm-3">
            <div class="row">
                <div class="col-6">
                    <h3>Contact Us</h3>
                    <p>
                        We are always here to help. If your have requirements/queries about our services; fill up the contact form below and we'll do our best to reply within 24 hours Alternatively simply pickup the phone and give us a call.
                    </p>
                    <a href="tel:+919090909090"><i class="fas fa-phone"></i>+(91) 9090909090 </a>
                    <br>
                    <a href="mailto:sid303@gmail.com"><i class="fas fa-envelope"></i> sid303@gmail.com </a>
                    <br>

                </div>
                <div class="col-6">
                    <div class="embed-responsive embed-responsive-16by9">
                        <iframe class="embed-responsive-item" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d241316.64333236168!2d72.74110193617271!3d19.082522317332813!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3be7c6306644edc1%3A0x5da4ed8f8d648c69!2sMumbai%2C+Maharashtra!5e0!3m2!1sen!2sin!4v1547151374329"
                            frameborder="0"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
