using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using TJ.DBUtility;
using commonlib;
using Wuqi.Webdiyer;

public partial class Admin_TB_LabelCodeInfo : AuthorPage
{
    private readonly BTB_LabelCodeInfo bll = new BTB_LabelCodeInfo();
    private MTB_LabelCodeInfo mod = new MTB_LabelCodeInfo();
    private int _currentindex = 1;
    readonly TabExecutewuliu _tab = new TabExecutewuliu(); 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            _currentindex = 1;
            AspNetPager1.CurrentPageIndex = _currentindex;
            DisplayData(1, AspNetPager1.PageSize);

        }
    }

    //private void fillgridview()
    //{
    //    if (inputSearchKeyword.Value.Trim().Length > 0)
    //    {
    //        GridView1.DataSource =
    //            bll.GetListsByFilterString(DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'");
    //    }
    //    else
    //    {
    //        GridView1.DataSource = bll.GetLists();
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

            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TB_LabelCodeInfo where " + Filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TB_LabelCodeInfo", Filtertemp, "ID", "ID", pageSize);
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
            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TB_LabelCodeInfo where " + Filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TB_LabelCodeInfo", Filtertemp, "ID", "ID", pageSize);
            GridView1.DataBind();
        }
        //AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TB_LabelCodeInfo where " + Filtertemp, null).Rows[0][0].ToString());
        //GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TB_LabelCodeInfo", Filtertemp, "ID", "ID", pageSize);
        //GridView1.DataBind();
    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["ID"].ToString()));
        //fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        //fillgridview();
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
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["ID"].ToString()));
        mod.startvalue = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtstartvalue")).Text.Trim();
        mod.endvalue = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtendvalue")).Text.Trim();
        mod.xiangnum = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtxiangnum")).Text.Trim());
        mod.pingnum = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtpingnum")).Text.Trim());
        mod.pianyiliang =
            Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtpianyiliang")).Text.Trim());
        mod.remarks = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtremarks")).Text.Trim();
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
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#CCFF83'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        //如果是绑定数据行 
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
        //    {
        //        ((LinkButton)e.Row.Cells[11].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
        //    }
        //}
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton) e.Row.Cells[10].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')");
            }
        }
    }


    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        //fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        // fillgridview();
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