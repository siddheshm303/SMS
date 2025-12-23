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
    public partial class residentMaintenancePayment : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            getResidentByHouse();
            TotalDueAmount();
            TextBox1.Text = Session["house"].ToString();
            TextBox7.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }

        //Pay Button
        protected void Button2_Click(object sender, EventArgs e)
        {
            InsertPaymentRecord();
        }

        //Load Button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            GetMaintenancePayments();
        }
        //user defined methods
        void getResidentByHouse()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM resident_tbl WHERE house ='" + Session["house"].ToString() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0]["full_name"].ToString();
                    TextBox3.Text = dt.Rows[0]["area"].ToString();
                    TextBox4.Text = dt.Rows[0]["type"].ToString();

                }
                else
                {
                    Response.Write("<script>alert('House Does Not Exists');</script>");
                }
                

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

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
                        "    mc.Month AS DueMonth," +
                        "    mc.Rate AS AreaRate," +
                        "    CASE WHEN r.type = 'Tenant' THEN mc.Tcharges ELSE 0 END AS TenantCharges," +
                        "    mc.StartingDate AS StartingDate," +
                        "    mc.EndingDate AS EndingDate," +
                        "   (r.area * mc.Rate) + CASE WHEN r.type = 'Tenant' THEN mc.Tcharges ELSE 0 END AS TotalCharge  " +
                        "FROM " +
                        "    [sms].[dbo].[schedule] mc  " +
                        "LEFT JOIN " +
                        "    [sms].[dbo].[resident_tbl] r ON 1=1   " +
                        "WHERE " +
                        "    NOT EXISTS (" +
                        "       SELECT 1 " +
                        "       FROM [sms].[dbo].[Payments] p " +
                        "       WHERE mc.Month = p.MonthPaid " +
                        "       AND p.House = r.house " +
                        "    ) AND r.house = @House;";

                    // Execute SQL query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@House", Session["house"].ToString());
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
            CalculateTotalCharge();
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

            // Recalculate the total charge after selecting or deselecting all checkboxes
            CalculateTotalCharge();
        }


        protected void CalculateTotalCharge()
        {
            decimal totalCharge = 0;
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chkBox = row.FindControl("RowCheckBox") as CheckBox;
                if (chkBox.Checked)
                {
                    // Assuming Total Charge column is at index 5
                    totalCharge += Convert.ToDecimal(row.Cells[6].Text);
                }
            }
            // Set the total charge to the appropriate TextBox
            TextBox6.Text = totalCharge.ToString();
        }

        void TotalDueAmount()
        {
            try
            {
                decimal totalCharge = 0;
                foreach (GridViewRow row in GridView1.Rows)
                {
                    totalCharge += Convert.ToDecimal(row.Cells[6].Text);

                }
                // Set the total charge to the appropriate TextBox
                TextBox5.Text = totalCharge.ToString();
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

                foreach (GridViewRow row in GridView1.Rows)
                {
                    CheckBox chkBox = row.FindControl("RowCheckBox") as CheckBox;
                    if (chkBox != null && chkBox.Checked)
                    {
                        // Get the month for the current row
                        string dueMonth = row.Cells[1].Text.ToString(); // Assuming DueMonth is the first column
                        decimal totalCharge = Convert.ToDecimal(row.Cells[6].Text); // TotalCharge column index 5

                        // Insert a payment record for the selected month
                        SqlCommand cmd = new SqlCommand("INSERT INTO Payments " +
                    "(House, MonthPaid, ServiceType, AmountPaid, ModeofPayment, PaymentDate, Type) " +
                    "VALUES (@House, @MonthPaid, @ServiceType, @AmountPaid, @ModeofPayment, @PaymentDate, @Type)", con);

                        cmd.Parameters.AddWithValue("@House", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@MonthPaid", dueMonth);
                        cmd.Parameters.AddWithValue("@AmountPaid", totalCharge);
                        cmd.Parameters.AddWithValue("@ServiceType", "--");
                        cmd.Parameters.AddWithValue("@PaymentDate", TextBox7.Text.Trim());
                        cmd.Parameters.AddWithValue("@ModeOfPayment", DropDownList1.Text.Trim());
                        cmd.Parameters.AddWithValue("@Type", "Maintenance");

                        cmd.ExecuteNonQuery();
                    }
                }
                Response.Write("<script>alert('Payed and Recorded');</script>");
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
                    DateTime sdate = Convert.ToDateTime(e.Row.Cells[4].Text);
                    DateTime edate = Convert.ToDateTime(e.Row.Cells[5].Text);
                    DateTime today = DateTime.Today;
                    if (today > edate)
                    {
                        e.Row.BackColor = System.Drawing.Color.MistyRose;
                    }
                    else if (sdate < today && edate > today)
                    {
                        e.Row.BackColor = System.Drawing.Color.LightYellow;
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