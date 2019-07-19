using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

using Color = System.Drawing.Color;

public partial class Admin_TJ_HXInfo : AuthorPage
{
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();
    SqlConnection sqlconcom = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                hfjxid.Value = Request.QueryString["ID"];
            }
            else
            {
                Response.End();
            }
            DisplayData(_currentindex, AspNetPager1.PageSize);
          
        }
    }

    private string Filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            Filtertemp = DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
        }
        Filtertemp = Filtertemp + " and awid="+ hfjxid.Value;

        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_HXInfo where " + Filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_HXInfo", Filtertemp, "id", "id", pageSize);
        GridView1.DataBind();
    }

    public string AwardThingFromAwid(string awid)
    {
        string sqlstring = "SELECT AwardThing from TJ_AwardInfo where awid= "+Convert.ToInt32(awid);
        var sda = new SqlDataAdapter(sqlstring, sqlconcom);
        var dttemp = new DataTable();
        sda.Fill(dttemp);
        dttemp.Dispose();
        sda.Dispose();
        if (string.IsNullOrEmpty(dttemp.Rows[0][0].ToString()))
        {
            return "";
        }
        else
        { return dttemp.Rows[0][0].ToString(); }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        DisplayData(1, AspNetPager1.PageSize);
    }  

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        DisplayData(1, AspNetPager1.PageSize);
    } 

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        DisplayData(_currentindex, AspNetPager1.PageSize);
    }
    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        DisplayData(1, AspNetPager1.PageSize);
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }

}