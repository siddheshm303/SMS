using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMS
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            CalculateBalance();
            CalculateTotalDueAmount();
        }

        protected void CalculateBalance()
        {
            try
            {

                // Define SQL query to calculate balance
                string query = @"SELECT 
                                    SUM(CASE WHEN Type = 'Maintenance' THEN AmountPaid ELSE 0 END) AS MaintenanceTotal,
                                    SUM(CASE WHEN Type = 'Service' THEN AmountPaid ELSE 0 END) AS ServiceTotal,
                                    SUM(CASE WHEN Type = 'Balance' THEN AmountPaid ELSE 0 END) AS BalanceTotal,
                                    SUM(CASE WHEN Type IN ('Maintenance', 'Balance') THEN AmountPaid ELSE 0 END) - 
                                    SUM(CASE WHEN Type = 'Service' THEN AmountPaid ELSE 0 END) AS TotalAmount
                                FROM 
                                Payments;";

                // Create SqlConnection
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    // Create SqlCommand
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Open the connection
                        connection.Open();

                        // Execute the command and read the result
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            // Get the balance from the reader
                            decimal balance = Convert.ToDecimal(reader["TotalAmount"]);

                            // Set the balance to the TextBox
                            TextBox2.Text = balance.ToString();
                        }

                        // Close the reader
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        protected void CalculateTotalDueAmount()
        {
            try
            {

                decimal totalDueAmount = 0;
                // Define SQL query to calculate balance
                string query = @"SELECT " +
                "    SUM((s.Rate * r.area) + CASE WHEN r.type = 'Tenant' THEN s.Tcharges ELSE 0 END) AS TotalDueAmount " +
                "FROM " +
                "    [sms].[dbo].[resident_tbl] r " +
                "JOIN " +
                "    [sms].[dbo].[schedule] s ON 1=1 " +
                "WHERE " +
                "    NOT EXISTS (" +
                "       SELECT 1 " +
                "       FROM [sms].[dbo].[Payments] p " +
                "       WHERE s.Month = p.MonthPaid " +
                "       AND p.House = r.house " +
                "    )";

                // Create SqlConnection
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    // Create SqlCommand
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        var result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            totalDueAmount = Convert.ToDecimal(result);
                            TextBox8.Text = totalDueAmount.ToString();
                        }
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