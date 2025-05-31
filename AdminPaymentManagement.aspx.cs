using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMS
{
    public partial class AdminPaymentManagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox7.Text = DateTime.Now.ToString("dd-MM-yyyy");
            GetPayments();
            CalculateBalance();
            CalculateTotalDueAmount();
        }

        //Go linkbutton
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            GetPaymentByID();
        }

        //Delete Record Button
        protected void Button1_Click(object sender, EventArgs e)
        {
            DeleteResidentById();
        }

        //Add balance button
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox6.Text))
            {
                InsertPaymentRecord();
            }
            else
            {
                Response.Write("<script>alert('Amount is not entered');</script>");
            }
        }

        //user defined fuctions
        void GetPaymentByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM Payments WHERE PaymentID ='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    TextBox3.Text = dt.Rows[0]["AmountPaid"].ToString();
                    TextBox4.Text = dt.Rows[0]["Type"].ToString();
                    TextBox5.Text = dt.Rows[0]["PaymentDate"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Record With this ID Does Not Exists');</script>");
                }
                GetPayments();
                CalculateBalance();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
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
        void DeleteResidentById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("DELETE FROM Payments WHERE PaymentID ='" + TextBox1.Text.Trim() + "';", con);
                int row = cmd.ExecuteNonQuery();
                if (row > 0)
                {
                    Response.Write("<script>alert('Record Deleted Successfully');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Record With this ID Does Not Exists');</script>");
                }
                con.Close();
                GetPayments();
                CalculateBalance();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

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

        void InsertPaymentRecord()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO Payments " +
                    "(House, MonthPaid, ServiceType, AmountPaid, ModeofPayment, PaymentDate, Type) " +
                    "VALUES (@House, @MonthPaid, @ServiceType, @AmountPaid, @ModeofPayment, @PaymentDate, @Type)", con);

                cmd.Parameters.AddWithValue("@House", "--");
                cmd.Parameters.AddWithValue("@MonthPaid", "--");
                cmd.Parameters.AddWithValue("@ServiceType", "--");
                cmd.Parameters.AddWithValue("@AmountPaid", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@ModeofPayment", "--");
                cmd.Parameters.AddWithValue("@PaymentDate", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@Type", "Balance");

                cmd.ExecuteNonQuery();

                Response.Write("<script>alert('Balance Added');</script>");
                GetPayments();
                CalculateBalance();

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