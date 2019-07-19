using System;

public partial class Admin_getpic_piccutter : System.Web.UI.Page
{
    public string BiLv;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["key"]) && !string.IsNullOrEmpty(Request.QueryString["bilv"]) && !string.IsNullOrEmpty(Request.QueryString["hdsv"]))
            { 
                hdkey.Value = Request.QueryString["key"].Trim();
                hdbilv.Value = Request.QueryString["bilv"].Trim();
                hdsv.Value = Request.QueryString["hdsv"].Trim();
            }
            else
            {
                Response.End();
            } 
        }
    } 
}