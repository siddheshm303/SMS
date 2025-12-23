using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SMS
{
    public partial class usersignup : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Sign up button event
       
        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (checkIdExists())
            {
                Response.Write("<script>alert('Resident Id Already exists');</script>");
            }
            
            else if (checkHouseExists())
            {
                Response.Write("<script>alert('House Already exists');</script>");
            }
            else
            {
                residentSignUp();
            }
           
        }

        //user define method

        bool checkHouseExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("select * from resident_tbl where house='" + TextBox1.Text.Trim() + "';", con);
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

        bool checkIdExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("select * from resident_tbl where resident_id='"+ TextBox8.Text.Trim()+"';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt=new DataTable();
                da.Fill(dt);

                if(dt.Rows.Count >= 1)
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

        void residentSignUp()
        {
            try
            {
                string gender = string.Empty;
                if (RadioButton1.Checked)
                {
                    gender = "Male";
                }
                else if (RadioButton2.Checked)
                {
                    gender = "Female";
                }
                else
                {
                    gender = "Other";
                }

                string filepath = "~/imgs/apartmentjpg.jpg";
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(Server.MapPath("imgs/" + filename));
                filepath = "~/imgs/" + filename;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO resident_tbl " +
                    "(house,full_name,dob,gender,contact_no,email,type,area," +
                    "no_of_family,resident_id,password,img_link) values" +
                    "(@house,@full_name,@dob,@gender,@contact_no,@email,@type,@area," +
                    "@no_of_family,@resident_id,@password,@img_link)", con);

                cmd.Parameters.AddWithValue("@house", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@full_name", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@contact_no", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@email", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@type", DropDownList1.Text);
                cmd.Parameters.AddWithValue("@area", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@no_of_family", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@resident_id", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@password", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@img_link", filepath);

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Resident Added Successfully');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}