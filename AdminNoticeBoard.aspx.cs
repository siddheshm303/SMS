using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;

namespace SMS
{
    public partial class AdminNoticeBoard : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox4.Text = DateTime.Now.ToString("dd-MM-yyyy");
            GridView1.DataBind();
        }

        //go linkbutton
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            GetNoticeByID();
        }

        //add button
        protected void Button1_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(TextBox2.Text) && !string.IsNullOrEmpty(TextBox3.Text))
            {
                InsertNotice();
            }
            else
            {
                Response.Write("<script>alert('Some Values Are Missing');</script>");
            }
        }

        //delete button
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkIdExists())
            {
                deleteResidentById();
            }
            else
            {
                Response.Write("<script>alert('Notice with this ID does not exists');</script>");
            }
        }

        //user defined
        void GetNoticeByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM NoticeBoard WHERE NoticeID ='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0]["Title"].ToString();
                    TextBox3.Text = dt.Rows[0]["Description"].ToString();
                    TextBox4.Text = dt.Rows[0]["Date"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Notice with this ID not exists');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        bool checkIdExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("select * from NoticeBoard where NoticeID='" + TextBox1.Text.Trim() + "';", con);
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

                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }


        }

        void InsertNotice()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO NoticeBoard " +
                    "(Title,House,Description,Date) values" +
                    "(@Title,@House,@Description,@Date)", con);

                cmd.Parameters.AddWithValue("@Title", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@House", "--");
                cmd.Parameters.AddWithValue("@Description", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@Date", TextBox4.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Notice Added Successfully');</script>");
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void deleteResidentById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("DELETE FROM NoticeBoard WHERE NoticeID ='" + TextBox1.Text.Trim() + "';", con);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Notice Deleted Successfully');</script>");
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}