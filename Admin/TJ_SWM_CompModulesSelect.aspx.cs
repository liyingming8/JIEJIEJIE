using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Admin_TJ_SWM_CompModulesSelect : System.Web.UI.Page
{
    readonly commfrank _commfun = new commfrank();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
            {
                bindgridview();
            }
        }
    }

    private void bindgridview()
    {
        InternetHandle intenet = new InternetHandle();
        string tempstring = intenet.GetUrlData("http://os.china315net.com/ajax/commswm/getpageinfo.ashx?pid=" + Request.QueryString["pid"]);
        DataTable dttemp = _commfun.ToDataTable(tempstring);
        GridView1.DataSource = dttemp;
        GridView1.DataBind();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        bindgridview(); 
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        bindgridview();
    }
}