using System;
using System.Configuration;
using System.Web.UI.WebControls;
using commonlib;

public partial class Admin_TJ_ShowYanLeiData : AuthorPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["lbcode"]))
            {
                string codestr = Request.QueryString["lbcode"].Trim();
                string sqlconnctionstring = ConfigurationManager.ConnectionStrings["SqlServerConnStringYanLei"].ToString();

            }
            else
            {
                Response.End();
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}