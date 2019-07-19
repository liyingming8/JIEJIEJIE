using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_TJ_RegisterInnerCompanys : AuthorPage
{
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();
    private readonly MTJ_RegisterCompanys mod = new MTJ_RegisterCompanys();
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
            return string.Format("javascript:var win=openWinCenter('TJ_RegisterInnerCompanysAddEdit.aspx?cmd=edit&ID={0}',900,500,'商盟信息编辑')", ID);
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
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));

            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                //((LinkButton)e.Row.Cells[11].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
            }
        }
    }
    private string Filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        Filtertemp = "ParentID=" + GetCookieCompID() + " or CompID=" + GetCookieCompID();
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            Filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
        } 
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(CompID) from TJ_RegisterCompanys where " +Filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_RegisterCompanys", Filtertemp, "CompID", "CompID", pageSize);
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