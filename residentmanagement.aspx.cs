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
    public partial class residentmangament : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        static string global_filepath;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            GridView1.DataBind();
        }

        //go button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            getResidentByHouse();
        }

        //add button
        protected void Button1_Click(object sender, EventArgs e)
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
                addNewResident();
            }
            GridView1.DataBind();
        }

        //update button
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkHouseExists())
            {
                updateResidentById(); 
            }
            else
            {
                Response.Write("<script>alert('Resident Id Or House does not exists exists');</script>");
            }
            GridView1.DataBind();
            
        }

        //delete button
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkHouseExists())
            {
                deleteResidentById();
            }
            else
            {
                Response.Write("<script>alert('Resident Id Or House does not exists exists');</script>");
            }
            GridView1.DataBind();
        }

        //user define functions
        void deleteResidentById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("DELETE FROM resident_tbl WHERE house ='" + TextBox1.Text.Trim() + "';", con);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Resident Deleted Successfully');</script>");
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void updateResidentById()
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

                if (filename =="" || filename == null)
                {
                    filepath = global_filepath;
                }
                else
                {
                    FileUpload1.SaveAs(Server.MapPath("imgs/" + filename));
                    filepath = "~/imgs/" + filename;
                }
                

                SqlConnection con = new SqlConnection(strcon);
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd=new SqlCommand("UPDATE resident_tbl SET " +
                    "house=@house,full_name=@full_name,dob=@dob,gender=@gender,contact_no=@contact_no,email=@email,type=@type," +
                    "area=@area,no_of_family=@no_of_family,resident_id=@resident_id,password=@password,img_link=@img_link " +
                    "where house='"+TextBox1.Text.Trim()+"'", con);

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
                Response.Write("<script>alert('Resident Updated Successfully');</script>");
                GridView1.DataBind();

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
                SqlConnection con =new SqlConnection(strcon);
                if(con.State==ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM resident_tbl WHERE house ='" + TextBox1.Text.Trim()+"';", con);
                SqlDataAdapter da=new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count >= 1 )
                {
                    TextBox2.Text = dt.Rows[0]["full_name"].ToString();
                    TextBox3.Text = dt.Rows[0]["dob"].ToString();
                    TextBox4.Text = dt.Rows[0]["contact_no"].ToString();
                    TextBox5.Text = dt.Rows[0]["email"].ToString();
                    TextBox6.Text = dt.Rows[0]["area"].ToString();
                    TextBox7.Text = dt.Rows[0]["no_of_family"].ToString();
                    TextBox8.Text = dt.Rows[0]["resident_id"].ToString();
                    TextBox9.Text = dt.Rows[0]["password"].ToString();
                    DropDownList1.SelectedValue = dt.Rows[0]["type"].ToString().Trim();

                    string gender= dt.Rows[0]["gender"].ToString();
                    RadioButton1.Checked = false; RadioButton2.Checked = false;
                    if(gender.Equals("Male"))
                    {
                        RadioButton1.Checked = true;
                    }
                    else if(gender.Equals("Female"))
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
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
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

                SqlCommand cmd = new SqlCommand("select * from resident_tbl where resident_id='" + TextBox8.Text.Trim() + "';", con);
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

        void addNewResident()
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

                string filepath=string.Empty;
                
                if (FileUpload1.Equals(null))
                {
                    filepath = "~/imgs/loginuser.png";
                }
                else
                {
                    filepath = "~/imgs/apartmentjpg.jpg";
                    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    FileUpload1.SaveAs(Server.MapPath("imgs/" + filename));
                    filepath = "~/imgs/" + filename;
                    
                }

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
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}