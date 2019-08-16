using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_commonswm_TJ_RegistercommonswmCompanys : AuthorPage
{
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys(); 
    public CommonFun comfun = new CommonFun();
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            string comp = GetCookieCompID();
            if (comp == "1")
            {
                _currentindex = 1;
                DisplayData(_currentindex, AspNetPager1.PageSize);
            } 
        }
    }

    private void fillgridview()
    {
        GridView1.DataSource =
            bll.GetListsByFilterString("ParentID=" + GetCookieCompID() + " or CompID=" + GetCookieCompID());
        GridView1.DataBind();
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            GridView1.DataSource =
                bll.GetListsByFilterString("(ParentID=" + GetCookieCompID() + " or CompID=" + GetCookieCompID() +
                                           ")and " + DDLField.SelectedValue + " like '%" +
                                           inputSearchKeyword.Value.Trim() + "%'");
        }
        else
        {
            GridView1.DataSource =
                bll.GetListsByFilterString("ParentID=" + GetCookieCompID() + " or CompID=" + GetCookieCompID());
        }
        GridView1.DataBind();
    }
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            //双击后编辑修改方法
            return string.Format("javascript:var win=openWinCenter('TJ_RegistercommonswmCompanysAddEdit.aspx?cmd=edit&ID={0}',760,520,'审核信息')", ID);
        }
        else
        {
            return "";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["CompID"].ToString()));
        fillgridview();
    } 
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillgridview();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
            {
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
            }  
        }
    }
    private string _filtertemp ="";
    private void DisplayData(int pageIndex, int pageSize)
    {
        _filtertemp = "UseSWM=1";
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            _filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
        } 
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(CompID) from TJ_RegisterCompanys where " +_filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_RegisterCompanys", _filtertemp, "CompID", "CompID", pageSize);
        GridView1.DataBind();
    }
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        DisplayData(_currentindex, AspNetPager1.PageSize);
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview();
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}