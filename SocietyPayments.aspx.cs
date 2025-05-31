using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMS
{
    public partial class PaymentRecord : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            TextBox4.Text = DateTime.Now.ToString("dd-MM-yyyy");
            CalculateBalance();
            GetPayments();
        }
        //Go button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            GetPaymentByID();
        }
        //Pay button
        protected void Button1_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(TextBox3.Text))
            {
                InsertPaymentRecord();
            }
            else
            {
                Response.Write("<script>alert('Amount is not entered');</script>");
            }
        }

        //Delete button
        protected void Button2_Click(object sender, EventArgs e)
        {
            DeleteResidentById();
        }

        //user defined functions
        protected void CalculateBalance()
        {
            try
            {

                // Define SQL query to calculate balance
                string query = @"SELECT 
                            SUM(CASE WHEN Type = 'Maintenance' THEN AmountPaid ELSE 0 END) AS MaintenanceTotal,
                            SUM(CASE WHEN Type = 'Service' THEN AmountPaid ELSE 0 END) AS ServiceTotal,
                            SUM(CASE WHEN Type = 'Maintenance' THEN AmountPaid ELSE 0 END) - SUM(CASE WHEN Type = 'Service' THEN AmountPaid ELSE 0 END) AS Balance
                        FROM 
                            Payments";

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
                            decimal balance = Convert.ToDecimal(reader["Balance"]);

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

        void InsertPaymentRecord()
        {
            try
            {
                decimal amount =Convert.ToDecimal(TextBox3.Text.Trim());
                decimal balance = Convert.ToDecimal(TextBox2.Text.Trim());
                if(amount <= balance)
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
                    cmd.Parameters.AddWithValue("@ServiceType", DropDownList1.Text.Trim());
                    cmd.Parameters.AddWithValue("@AmountPaid", TextBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@PaymentDate", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@ModeOfPayment", DropDownList2.Text.Trim());
                    cmd.Parameters.AddWithValue("@Type", "Service");



                    cmd.ExecuteNonQuery();

                    Response.Write("<script>alert('Payed and Recorded');</script>");
                    GetPayments();
                    CalculateBalance();
                }
                else
                {
                    Response.Write("<script>alert('Amount Paid Cannot Exceed The Balance Amount');</script>");
                }

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
                SqlConnection con= new SqlConnection(strcon);
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT PaymentID, ServiceType, AmountPaid, ModeOfPayment, PaymentDate" +
                    "   FROM Payments" +
                    "   WHERE Type = 'Service';", con);

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
                    TextBox4.Text = dt.Rows[0]["PaymentDate"].ToString();
                    DropDownList1.SelectedValue = dt.Rows[0]["ServiceType"].ToString().Trim();
                    DropDownList2.SelectedValue = dt.Rows[0]["ModeofPayment"].ToString().Trim();
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
    }
}