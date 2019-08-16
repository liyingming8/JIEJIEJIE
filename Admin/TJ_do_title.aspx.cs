using System;
using commonlib;

public partial class Admin_TJ_do_title : AuthorPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["alert"]))
            {
                lab_reson.Text = Request.QueryString["alert"];
            } 
        }
    }
}