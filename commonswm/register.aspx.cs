using System;

public partial class register : System.Web.UI.Page
{  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["openid"]))
            {
                Response.End();
            }
            else
            {
                inputopenid.Value = Request.QueryString["openid"];
                inputpid.Value = Request.QueryString["pid"];
            }
        }
    }
}