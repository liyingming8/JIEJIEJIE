using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_TJ_Integral : AuthorPage
{
    private readonly BTJ_Integral bll = new BTJ_Integral();
    private MTJ_Integral mod = new MTJ_Integral();
    private readonly BTJ_RegisterCompanys bllcomp = new BTJ_RegisterCompanys();
    private readonly BTJ_User blluser = new BTJ_User();
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            _currentindex = 1;
            AspNetPager1.CurrentPageIndex = _currentindex;
            DisplayData(1, AspNetPager1.PageSize); 
        }
    }
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_IntegralAddEdit.aspx?cmd=edit&ID={0}',600,420,'积分活动')", ID);
        }
        else
        {
            return "";
        }
    } 

    public string XiangXiLinkStringJifenGuiZe(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_IntegralInfo.aspx?id={0}',620,480,'积分规则')",Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    public string XiangXiLinkStringSelectProduct(string id)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('wuliu/TB_Products_Infor_select.aspx?agentid={0}&ITGRID={1}',620,480,'指定产品')", GetCookieCompID(),Sc.EncryptQueryString(id));
        }
        else
        {
            return "";
        }
    }
    //private void fillgridview()
    //{
    //    GridView1.DataSource = bll.GetListsByFilterString("CompID=" + GetCookieCompID());
    //    GridView1.DataBind();
    //}
    //private const string Filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        string comp = GetCookieCompID();
        //if (comp == "1")
        //{
        //    string Filtertemp = "1=1"; 
        //    if (inputSearchKeyword.Value.Trim().Length > 0)
        //    { 
        //        Filtertemp = " " + DDLField.SelectedValue +" like '%" + inputSearchKeyword.Value.Trim() + "%'"; 
        //    } 
        //    AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(ITGRID) from TJ_Integral where " + Filtertemp, null).Rows[0][0].ToString());
        //    GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_Integral", Filtertemp, "CompID", "ITGRID", pageSize);
        //    GridView1.DataBind();
        //}
        //else
        //{
        //    string Filtertemp = "CompID=" + comp;
        //    if (inputSearchKeyword.Value.Trim().Length > 0)
        //    { 
        //        Filtertemp = "CompID =" + GetCookieCompID() + " and " + DDLField.SelectedValue +" like '%" + inputSearchKeyword.Value.Trim() + "%'"; 
        //    } 
        //    AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_Integral where " + Filtertemp, null).Rows[0][0].ToString());
        //    GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_Integral", Filtertemp, "CompID", "ITGRID", pageSize);
        //    GridView1.DataBind();
        //} 
        string filtertemp = "CompID=" + comp;
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            filtertemp = "CompID =" + GetCookieCompID() + " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
        }
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_Integral where " + filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_Integral", filtertemp, "CompID", "ITGRID", pageSize);
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["ITGRID"].ToString()));
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        //  fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["ITGRID"].ToString())); 
        mod.BeginDate =
            Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtBeginDate")).Text.Trim());
        mod.EndDate = Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtEndDate")).Text.Trim());
        mod.Remarks = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtRemarks")).Text.Trim();
        bll.Modify(mod);
        GridView1.EditIndex = -1;  
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize); 
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

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
                ((HyperLink)e.Row.FindControl("linkjifen")).Attributes.Add("onclick", XiangXiLinkStringJifenGuiZe(dataKey[0].ToString()));
                ((HyperLink)e.Row.FindControl("linkselectproduct")).Attributes.Add("onclick", XiangXiLinkStringSelectProduct(dataKey[0].ToString()));
            } 
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton) e.Row.Cells[7].Controls[0]).Attributes.Add("onclick","javascript:return confirm('你确定要删除当前记录吗?')");
            }
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        //if (inputSearchKeyword.Value.Trim().Length > 0)
        //{
        //    GridView1.DataSource =
        //        bll.GetListsByFilterString("CompID=" + GetCookieCompID() + " and " + DDLField.SelectedValue + " like '%" +
        //                                   inputSearchKeyword.Value.Trim() + "%'");
        //}
        //else
        //{
        //    GridView1.DataSource = bll.GetListsByFilterString("CompID=" + GetCookieCompID());
        //}
        //GridView1.DataBind();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        //fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    public string ReturnCompanyName(string COMPID)
    {
        return bllcomp.GetList(int.Parse(COMPID)).CompName;
    }

    public string ReturnUserName(string UserID)
    {
        return blluser.GetList(int.Parse(UserID)).LoginName;
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}