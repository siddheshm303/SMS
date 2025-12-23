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
    public partial class maintenanceSchedule : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        //go button
        protected void Button1_Click(object sender, EventArgs e)
        {
            getSchedule();
        }

        //schedule button
        protected void Button2_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(TextBox1.Text) && !string.IsNullOrEmpty(TextBox2.Text) && !string.IsNullOrEmpty(TextBox3.Text) && !string.IsNullOrEmpty(TextBox4.Text) && !string.IsNullOrEmpty(TextBox5.Text))
            {
                if (checkScheduleExists())
                {
                    Response.Write("<script>alert('Schedule Exists');</script>");
                }
                else
                {
                    schedule();
                }
            }
            else
            {
                Response.Write("<script>alert('Some Values Are Missing');</script>");
            }
            
        }

        //delete button
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkScheduleExists())
            {
                deleteSchedule();
            }
            else
            {
                Response.Write("<script>alert('Schedule Does Not Exists');</script>");
            }
        }

        //user defined functions
        void deleteSchedule()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("DELETE FROM schedule where Month= '" + TextBox1.Text + "'", con);
                
                int result=cmd.ExecuteNonQuery();
                con.Close();
                if (result > 0)
                {
                    Response.Write("<script>alert('Schedule Deleted Successfully');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Schedule Not Found');</script>");
                }
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        bool checkScheduleExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("select * from schedule where Month= '" + TextBox1.Text + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        void schedule()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO schedule " +
                    "(Month,Rate,Tcharges,StartingDate,EndingDate) values" +
                    "(@Month,@Rate,@Tcharges,@StartingDate,@EndingDate)", con);

                cmd.Parameters.AddWithValue("@Month", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@Rate", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@Tcharges", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@StartingDate", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@EndingDate", TextBox5.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Schedule Added Successfully');</script>");
                GridView1.DataBind();

            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void getSchedule()
        {
            try
            {
                SqlConnection con= new SqlConnection(strcon);
                if(con.State == ConnectionState.Closed )
                {
                    con.Open();
                }

                SqlCommand cmd =new SqlCommand("select * from schedule where Month= '"+TextBox1.Text+"'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if(dt.Rows.Count >= 1 )
                {
                    TextBox2.Text = dt.Rows[0]["Rate"].ToString();
                    TextBox3.Text = dt.Rows[0]["Tcharges"].ToString();
                    TextBox4.Text = dt.Rows[0]["StartingDate"].ToString();
                    TextBox5.Text = dt.Rows[0]["EndingDate"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Schedule Not Found');</script>");
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if(e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime sdate = Convert.ToDateTime(e.Row.Cells[3].Text);
                    DateTime edate = Convert.ToDateTime(e.Row.Cells[4].Text);
                    DateTime today = DateTime.Today;
                    if(today > edate)
                    {
                        e.Row.BackColor = System.Drawing.Color.MistyRose;
                    }
                    else if(sdate < today && edate > today)
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