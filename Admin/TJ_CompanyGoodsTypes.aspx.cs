using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_TJ_CompanyGoodsTypes : AuthorPage
{
    private readonly BTJ_CompanyGoodsTypes bll = new BTJ_CompanyGoodsTypes();
    private readonly BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();
    private MTJ_CompanyGoodsTypes mod = new MTJ_CompanyGoodsTypes();
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

    public string GetCompanyName(string CPID)
    {
        return bcompany.GetList(int.Parse(CPID)).CompName;
    }

    //private void fillgridview()
    //{
    //    GridView1.DataSource = bll.GetListsByFilterString(ReturnFilterString());
    //    GridView1.DataBind();
    //}

    //private string tempstring = "";

    //private string ReturnFilterString()
    //{
    //    tempstring = "CompID=" + GetCookieCompID();
    //    if (inputSearchKeyword.Value.Length > 0)
    //    {
    //        tempstring += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
    //    }
    //    return tempstring;
    //}

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["CompGoodsTypeID"].ToString()));
        //fillgridview();
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
        //fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["CompGoodsTypeID"].ToString()));
        mod.GoodsType = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtGoodsType")).Text.Trim();
        mod.Remark = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtRemark")).Text.Trim();
        bll.Modify(mod);
        GridView1.EditIndex = -1;
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        //fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }
   
    private void DisplayData(int pageIndex, int pageSize)
    {
        string comp = GetCookieCompID();
        if (comp == "1")
        {
            string Filtertemp = "1=1";
            // string Filtertemp = "CompID =1";
            if (inputSearchKeyword.Value.Trim().Length > 0)
            {

                Filtertemp = " " + DDLField.SelectedValue +
                                                 " like '%" + inputSearchKeyword.Value.Trim() + "%'";
                //Filtertemp = "CompID = 1 and " + DDLField.SelectedValue +
                //                              " like '%" + inputSearchKeyword.Value.Trim() + "%'";//可以查询全部的
            }

            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_CompanyGoodsTypes where " + Filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_CompanyGoodsTypes", Filtertemp, "CompGoodsTypeID", "CompGoodsTypeID", pageSize);
            GridView1.DataBind();
        }
        else
        {
            string Filtertemp = "CompID=" + comp;
            if (inputSearchKeyword.Value.Trim().Length > 0)
            {

                Filtertemp = "CompID =" + GetCookieCompID() + " and " + DDLField.SelectedValue +
                                             " like '%" + inputSearchKeyword.Value.Trim() + "%'";
                //GridView1.DataSource =
                //    bll.GetListsByFilterString("CompID =" + GetCookieCompID() + " and " + DDLField.SelectedValue +
                //                               " like '%" + inputSearchKeyword.Value.Trim() + "%'");
            }
            //else
            //{
            //    GridView1.DataSource = bll.GetListsByFilterString("CompID =" + GetCookieCompID());
            //}
            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_CompanyGoodsTypes where " + Filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_CompanyGoodsTypes", Filtertemp, "CompGoodsTypeID", "CompGoodsTypeID", pageSize);
            GridView1.DataBind();
        }

        //AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_CompanyGoodsTypes where " + Filtertemp, null).Rows[0][0].ToString());
        //GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_CompanyGoodsTypes", Filtertemp, "CompGoodsTypeID", "CompGoodsTypeID", pageSize);
        //GridView1.DataBind();
    }


    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            //双击后编辑修改方法
            return string.Format("javascript:var win=openWinCenter('TJ_CompanyGoodsTypesAddEdit.aspx?cmd=edit&ID={0}',400,300,'系统角色')", ID);
        }
        else
        {
            return "";
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));

            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton) e.Row.Cells[3].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')");
            }
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

        //if (inputSearchKeyword.Value.Trim().Length > 0)
        //{
        //    GridView1.DataSource =
        //        bll.GetListsByFilterString(DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() +
        //                                   "%' and CompID=" + GetCookieCompID());
        //}
        //else
        //{
        //    GridView1.DataSource = bll.GetListsByFilterString("CompID=" + GetCookieCompID());
        //}
        //GridView1.DataBind();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

        // fillgridview();
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}