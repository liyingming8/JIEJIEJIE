using System;
using TJ.DBUtility;

public partial class register : System.Web.UI.Page
{
    TabExecute _tab = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["openid"]))
            {
                Response.End();
                //_tab.ExecuteNonQuery("delete from TJ_RegisterCompanys where ");
            }
            else
            {
                inputopenid.Value = Request.QueryString["openid"];
                inputpid.Value = Request.QueryString["pid"];
            }
        }
    }
}