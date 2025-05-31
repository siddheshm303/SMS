using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMS
{
    public partial class DefaulterList : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox3.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }
        
        //Refresh Button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            GetDefaulterList();
        }

        //Send Button
        protected void Button1_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(TextBox1.Text.Trim()) && !string.IsNullOrEmpty(TextBox2.Text.Trim()))
            {
                SendNotices();
            }
            else
            {
                Response.Write("<script>alert('Some Values Are Empty');</script>");
            }
            
        }

        //user define functions
        void GetDefaulterList()
        {
            try
            {
                DataTable maintenancePaymentsTable = new DataTable();

                // Establish connection to the database
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    // SQL query to fetch data from maintenance schedule, payments, and resident tables
                    string query = "SELECT " +
                        "    r.house AS House," +
                        "    r.full_name AS FullName," +
                        "    r.type AS ResidentType," +
                        "    SUM((s.Rate * r.area) + CASE WHEN r.type = 'Tenant' THEN s.Tcharges ELSE 0 END) AS TotalDueAmount " +
                        "FROM " +
                        "    [sms].[dbo].[resident_tbl] r " +
                        "JOIN" +
                        "    [sms].[dbo].[schedule] s ON 1=1 " +
                        "WHERE " +
                        "    NOT EXISTS (" +
                        "        SELECT 1 " +
                        "        FROM [sms].[dbo].[Payments] p" +
                        "        WHERE p.House = r.house" +
                        "          AND p.MonthPaid = s.[Month]" +
                        "    ) " +
                        "    AND s.EndingDate < GETDATE() " +
                        "GROUP BY " +
                        "    r.house, r.full_name, r.type;";
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

        protected void RowCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = sender as CheckBox;
            GridViewRow row = chkBox.Parent.Parent as GridViewRow;
        }

        protected void SelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox selectAllCheckBox = sender as CheckBox;

            // Loop through each row in the GridView
            foreach (GridViewRow row in GridView1.Rows)
            {
                // Find the RowCheckBox control in the current row
                CheckBox rowCheckBox = row.FindControl("RowCheckBox") as CheckBox;

                // Set the Checked property of the RowCheckBox based on the state of the SelectAllCheckBox
                rowCheckBox.Checked = selectAllCheckBox.Checked;
            }
        }

        void SendNotices()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                bool residentseleted = false;
                foreach (GridViewRow row in GridView1.Rows)
                {
                    CheckBox chkBox = row.FindControl("RowCheckBox") as CheckBox;
                    if (chkBox != null && chkBox.Checked)
                    {
                        residentseleted = true;

                        string house = row.Cells[1].Text.ToString();

                        SqlCommand cmd = new SqlCommand("INSERT INTO NoticeBoard " +
                    "(Title,House,Description,Date) values" +
                    "(@Title,@House,@Description,@Date)", con);

                        cmd.Parameters.AddWithValue("@Title", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@House", house);
                        cmd.Parameters.AddWithValue("@Description", TextBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@Date", TextBox3.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }
                }
                if(residentseleted)
                {
                    Response.Write("<script>alert('Notice Sent');</script>");
                    GridView1.DataBind();
                }
                else
                {
                    Response.Write("<script>alert('Please select at least one resident');</script>");
                }
                
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

    }
}