using System;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMS
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            try
            {
                string ses = Session["role"] as string;
                if (string.IsNullOrEmpty(ses))
                {
                    LinkButton1.Visible = true; //resident login button
                    LinkButton2.Visible = false; //resident signup button

                    LinkButton3.Visible = false; //logout button
                    LinkButton4.Visible = false; //Hello resident button

                    LinkButton5.Visible = true; //admin login button
                    LinkButton6.Visible = false; //resident management button

                    LinkButton7.Visible = false; //maintenance schedule button
                    LinkButton8.Visible = false; //admin maintenance payment button
                    LinkButton9.Visible = false; //resident maintenance payment button

                    LinkButton10.Visible = false; //Admin Notice Board
                    LinkButton11.Visible = false; //Resident Notice Board

                    LinkButton12.Visible = false; //Residents View

                    LinkButton13.Visible = false; //Admin Defaulters List
                    LinkButton14.Visible = false; //Resident Defaulters List

                    LinkButton15.Visible = false; //Residents Payment History
                    LinkButton16.Visible = false; //Other Payments
                    LinkButton17.Visible = false; //Societies Transactions

                }
                else if (ses.Equals("admin"))
                {
                    LinkButton1.Visible = false; //resident login button
                    LinkButton2.Visible = false; //resident signup button

                    LinkButton3.Visible = true; //logout button
                    LinkButton4.Visible = true; //Hello resident button
                    LinkButton4.Text = "Hello Admin";

                    LinkButton5.Visible = false; //admin login button
                    LinkButton6.Visible = true; //resident management button

                    LinkButton7.Visible = true; //maintenance schedule button
                    LinkButton8.Visible = true; //admin maintenance payment button
                    LinkButton9.Visible = false; //resident maintenance payment button

                    LinkButton10.Visible = true; //Admin Notice Board
                    LinkButton11.Visible = false; //Resident Notice Board

                    LinkButton12.Visible = false; //Residents View

                    LinkButton13.Visible = true; //Admin Defaulters List
                    LinkButton14.Visible = false; //Resident Defaulters List

                    LinkButton15.Visible = false; //Residents Payment History
                    LinkButton16.Visible = true; //Other Payments
                    LinkButton17.Visible = false; //Societies Transactions
                }
                else
                {
                    LinkButton1.Visible = false; //resident login button
                    LinkButton2.Visible = false; //resident signup button

                    LinkButton3.Visible = true; //logout button
                    LinkButton4.Visible = true; //Hello resident button
                    LinkButton4.Text = "Hello " + Session["fullname"].ToString();

                    LinkButton5.Visible = false; //admin login button
                    LinkButton6.Visible = false; //resident management button

                    LinkButton7.Visible = false; //maintenance schedule button
                    LinkButton8.Visible = false; //admin maintenance payment button
                    LinkButton9.Visible = true; //resident maintenance payment button

                    LinkButton10.Visible = false; //Admin Notice Board
                    LinkButton11.Visible = true; //Resident Notice Board

                    LinkButton12.Visible = true; //Residents View

                    LinkButton13.Visible = false; //Admin Defaulters List
                    LinkButton14.Visible = true; //Resident Defaulters List

                    LinkButton15.Visible = true; //Residents Payment History
                    LinkButton16.Visible = false; //Other Payments
                    LinkButton17.Visible = true; //Societies Transactions
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

        }

        //admin login linkbutton
        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminlogin.aspx");
        }

        //residentmanagement linkbutton
        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Response.Redirect("residentmanagement.aspx");
        }

        //resident login linkbutton
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("residentlogin.aspx");
        }


        //logout button
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Session["id"] = "";
            Session["fullname"] = "";
            Session["role"] = "";

            LinkButton1.Visible = true; //resident login button
            LinkButton2.Visible = true; //resident signup button

            LinkButton3.Visible = false; //logout button
            LinkButton4.Visible = false; //Hello resident button

            LinkButton5.Visible = true; //admin login button
            LinkButton6.Visible = false; //resident management button

            Response.Redirect("homepage.aspx");
        }

        //hello user linkbutton
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Response.Redirect("residentprofile.aspx");
        }

        //maintenance schedule linkbutton
        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            Response.Redirect("maintenanceSchedule.aspx");
        }

        //admin  maintenance payment linkbutton
        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminMaintenancePayment.aspx");
        }

        //resident maintenance payment linkbutton
        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            Response.Redirect("residentMaintenancePayment.aspx");
        }

        //Admin Notice Board
        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminNoticeBoard.aspx");
        }

        //Resident Notice Board
        protected void LinkButton11_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResidentNoticeBoard.aspx");
        }

        //Residents View
        protected void LinkButton12_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResidentsView.aspx");
        }

        //Admin Defaulters List
        protected void LinkButton13_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminDefaulterList.aspx");
        }

        //Resident Defaulters List
        protected void LinkButton14_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResidentDefaulterList.aspx");
        }

        //Residents Payment History
        protected void LinkButton15_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResidentsPaymentHistory.aspx");
        }

        //Other Payments
        protected void LinkButton16_Click(object sender, EventArgs e)
        {
            Response.Redirect("SocietyPayments.aspx");
        }

        protected void LinkButton17_Click(object sender, EventArgs e)
        {
            Response.Redirect("Transactions.aspx");
        }
    }
}