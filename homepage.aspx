<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="SMS.homepage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section>
      <img width="1920" src="imgs/crop1wideimg2.jpg" class="img-fluid"/>
   </section>
   <section>
      <div class="container">
         <div class="row">
            <div class="col-12">
               <center>
                  <h2>Our Features</h2>
                  <p><b>Our 3 Primary Features -</b></p>
               </center>
            </div>
         </div>
         <div class="row">
            <div class="col-md-4">
               <center>
                  <img width="150" src="imgs/payment-gateway.png" />
                  <h4>Digital Payments</h4>
                  <p class="text-justify">Seamlessly manage maintenance dues and other payments online, eliminating the hassle of manual transactions.</p>
               </center>
            </div>
            <div class="col-md-4">
               <center>
                  <img width="150" src="imgs/virtualnoticeboard.png"/> 
                  <h4>Virtual Notice Board</h4>
                  <p class="text-justify">Stay informed about society announcements, events, and important notices through our virtual notice board accessible to all residents.</p>
               </center>
            </div>
            <div class="col-md-4">
               <center>
                  <img width="150" src="imgs/due-date.png" />
                  <h4>Defaulter List</h4>
                  <p class="text-justify">Easily identify residents with outstanding dues and send automated reminders, ensuring timely payment and improved financial management.</p>
               </center>
            </div>
         </div>
      </div>
   </section>
   <section>
      <img width="1920"  src="imgs/wideimg2LL.jpg" class="img-fluid" />
   </section>
   

</asp:Content>
