using System;
using System.Web.UI.WebControls;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_TJ_MSjbInfo : AuthorPage
{
    //BTJ_MSjbInfo bll = new BTJ_MSjbInfo();
    //MTJ_MSjbInfo mod = new MTJ_MSjbInfo();
    private readonly DBClass db = new DBClass();
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
            return string.Format("javascript:var win=openWinCenter('TJ_MSjbInfoAddEdit.aspx?cmd=edit&ID={0}',600,400,'基本信息管理')", ID);
        }
        else
        {
            return "";
        }
    }

    //private void fillgridview()
    //{
    //    if (inputSearchKeyword.Value.Trim().Length > 0)
    //    {
    //        GridView1.DataSource =
    //            db.GetListsByFilterStringwl(DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'");
    //    }
    //    else
    //    {
    //        GridView1.DataSource = db.GetLists();
    //    }
    //    GridView1.DataBind();
    //}
    //private const string Filtertemp = "1=1";
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

            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_MSjbInfo where " + Filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_MSjbInfo", Filtertemp, "ID", "ID", pageSize);
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
            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_MSjbInfo where " + Filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_MSjbInfo", Filtertemp, "ID", "ID", pageSize);
            GridView1.DataBind();
        }


        //AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_MSjbInfo where " + Filtertemp, null).Rows[0][0].ToString());
        //GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_MSjbInfo", Filtertemp, "ID", "ID", pageSize);
        //GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        db.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["ID"].ToString()));
        //fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    ((Label)e.Row.FindControl("LabelIndex")).Text = (GridView1.PageSize * GridView1.PageIndex + e.Row.RowIndex+1).ToString();
        //    if (IsSuperAdmin())
        //    {
        //        ((LinkButton)e.Row.Cells[15].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
        //    }
        //    else
        //    {
        //         e.Row.Cells[15].Enabled= false;
        //         e.Row.Cells[15].ForeColor = System.Drawing.Color.LightGray;
        //    }
        //}
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
            var key = GridView1.DataKeys[e.Row.RowIndex];
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton) e.Row.Cells[12].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')");
            }
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        // fillgridview();
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
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}