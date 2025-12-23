using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace SMS
{
    public partial class Transactions : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetPayments();
        }

        //user define methods
        void GetPayments()
        {
            try
            {
                DataTable maintenancePaymentsTable = new DataTable();
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from Payments", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(maintenancePaymentsTable);
                GridView1.DataSource = maintenancePaymentsTable;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string type = e.Row.Cells[7].Text;
                    string balanace = "Balance";
                    string service = "Service";
                    if (type == service)
                    {
                        e.Row.BackColor = System.Drawing.Color.MistyRose;
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.Color.AliceBlue;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}