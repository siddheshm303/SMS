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
    public partial class ResidentDefaulterList : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Refresh Button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            GetMaintenancePayments();
        }

        //user define functions
        void GetMaintenancePayments()
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
                        "    NOT EXISTS ( " +
                        "        SELECT 1 " +
                        "        FROM [sms].[dbo].[Payments] p" +
                        "        WHERE p.House = r.house" +
                        "          AND p.MonthPaid = s.[Month]" +
                        "    ) " +
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
    }
}