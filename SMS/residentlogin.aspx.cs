using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace SMS
{
    public partial class userlogin : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        string house;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //resident login
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con= new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd=new SqlCommand("select * from resident_tbl " +
                    "where resident_id='"+TextBox1.Text.Trim()+"' " +
                    "AND password='"+TextBox2.Text.Trim()+"'", con);

                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        Session["id"]=dr.GetValue(9).ToString();
                        Session["fullname"] = dr.GetValue(1).ToString();
                        Session["role"] = "resident";
                        Session["house"]=dr.GetValue(0).ToString();
                        house = Session["house"].ToString();
                    }
                    Response.Redirect("homepage.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Invalid Credential');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}