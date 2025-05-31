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
    public partial class ResidentNoticeBoard : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
            GetMaintenancePayments();
        }

        void GetMaintenancePayments()
        {
            try
            {
                DataTable maintenancePaymentsTable = new DataTable();

                // Establish connection to the database
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    // SQL query to fetch data from NoticeBoard table based on the provided house value
                    string query = "SELECT * FROM NoticeBoard WHERE House = '" + Session["house"].ToString() +"' OR House='--'";

                    // Execute SQL query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        SqlDataAdapter adapter = new SqlDataAdapter(command);

                        // Fill DataTable with the retrieved data
                        adapter.Fill(maintenancePaymentsTable);
                    }

                    GridView1.DataSource = maintenancePaymentsTable;
                }
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the data item bound to the row
                DataRowView rowView = (DataRowView)e.Row.DataItem;

                if (rowView != null)
                {
                    // Get the index of the "House" column in the data source
                    int houseColumnIndex = rowView.DataView.Table.Columns.IndexOf("House");

                    if (houseColumnIndex != -1)
                    {
                        // Get the value of the "House" column
                        string houseValue = rowView.Row.Field<string>(houseColumnIndex);

                        // Check if House value is '--'
                        if (houseValue == "--")
                        {
                            // If House value is '--', set row background color to red
                            e.Row.BackColor = System.Drawing.Color.AliceBlue;
                        }
                        else
                        {
                            // Otherwise, set row background color to blue
                            e.Row.BackColor = System.Drawing.Color.MistyRose;
                        }
                    }
                }
            }
        }
    }
}