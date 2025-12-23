using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMS
{
    public partial class ResidentsPaymentHistory : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetPayments();
        }

        void GetPayments()
        {
            try
            {
                DataTable payments= new DataTable();
                SqlConnection con =new SqlConnection(strcon);
                if(con.State != ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("Select PaymentID, MonthPaid, AmountPaid, ModeofPayment, PaymentDate from Payments where House='" + Session["House"].ToString() +"';", con);
                SqlDataAdapter dt= new SqlDataAdapter(cmd);
                dt.Fill(payments);
                GridView1.DataSource = payments;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}