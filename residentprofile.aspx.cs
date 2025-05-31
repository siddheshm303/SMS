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
    public partial class residentprofile : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        static string global_filepath;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["house"].ToString() == "" || Session["house"] == null)
            {
                Response.Redirect("<script>alert('Session Expired Login Again');</script>");
                Response.Redirect("residentlogin.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    getResidentByHouse();
                }
            }
        }
        //Update Button
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["house"].ToString() == "" || Session["house"] == null)
            {
                Response.Redirect("<script>alert('Session Expired Login Again');</script>");
                Response.Redirect("residentlogin.aspx");
            }
            else
            {
                updateResidentDetails();
                
            }
        }

        //user define functions
        void updateResidentDetails()
        {
            try
            {
                string password = "";
                if(TextBox10.Text.Trim() == "")
                {
                    password = TextBox9.Text.Trim();
                }
                else
                {
                    password = TextBox10.Text.Trim();
                }
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

                string filepath = global_filepath;
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);

                if (filename == "" || filename == null)
                {
                    filepath = global_filepath;
                }
                else
                {
                    FileUpload1.SaveAs(Server.MapPath("imgs/" + filename));
                    filepath = "~/imgs/" + filename;
                }
                

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("UPDATE resident_tbl SET " +
                    "house=@house,full_name=@full_name,dob=@dob,gender=@gender,contact_no=@contact_no,email=@email,type=@type," +
                    "area=@area,no_of_family=@no_of_family,resident_id=@resident_id,password=@password,img_link=@img_link " +
                    "where house='" + Session["house"].ToString() + "'", con);

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
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@img_link", filepath);


                int result = cmd.ExecuteNonQuery();
                con.Close();
                if(result > 0)
                {
                    Response.Write("<script>alert('Resident Updated Successfully');</script>");
                    getResidentByHouse();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Entry');</script>");
                }
                
                
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }

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
                    TextBox1.Text = dt.Rows[0]["house"].ToString();
                    TextBox2.Text = dt.Rows[0]["full_name"].ToString();
                    TextBox3.Text = dt.Rows[0]["dob"].ToString();
                    TextBox4.Text = dt.Rows[0]["contact_no"].ToString();
                    TextBox5.Text = dt.Rows[0]["email"].ToString();
                    TextBox6.Text = dt.Rows[0]["area"].ToString();
                    TextBox7.Text = dt.Rows[0]["no_of_family"].ToString();
                    TextBox8.Text = dt.Rows[0]["resident_id"].ToString();
                    TextBox9.Text = dt.Rows[0]["password"].ToString();
                    DropDownList1.SelectedValue = dt.Rows[0]["type"].ToString().Trim();

                    string gender = dt.Rows[0]["gender"].ToString();
                    RadioButton1.Checked = false; RadioButton2.Checked = false;
                    if (gender.Equals("Male"))
                    {
                        RadioButton1.Checked = true;
                    }
                    else if (gender.Equals("Female"))
                    {
                        RadioButton2.Checked = true;
                    }
                    else
                    {
                        RadioButton3.Checked = true;
                    }
                    global_filepath = dt.Rows[0]["img_link"].ToString();
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
    }
}