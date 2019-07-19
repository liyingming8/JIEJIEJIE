using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_TJ_InfoType : AuthorPage
{
    private readonly BTJ_InfoType bll = new BTJ_InfoType();
    private readonly BTJ_PublishInfo bpubinfo = new BTJ_PublishInfo();
    private MTJ_InfoType mod = new MTJ_InfoType();
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();
    commfrank comfrank = new commfrank();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _currentindex = 1;
            AspNetPager1.CurrentPageIndex = _currentindex;
            DisplayData(1, AspNetPager1.PageSize);
        }
    } 

    public string ReturnCompName(string compid)
    {
        return comfrank.GetValueByID("nm", "TJ_RegisterCompanys", "CompID", "CompName", "", compid);
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["IFTypeID"].ToString()));
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize); 
        //fillgridview();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

        //fillgridview();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);
        //fillgridview();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["IFTypeID"].ToString()));
        mod.ParentID = Convert.ToInt32(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtParentID")).Text.Trim());
        //mod.CompID = Convert.ToInt32(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtCompID")).Text.Trim());
        mod.TypeName = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtTypeName")).Text.Trim();
        mod.Remarks = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtRemarks")).Text.Trim();
        bll.Modify(mod);
        GridView1.EditIndex = -1;
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

        //fillgridview();
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
                if (CheckIsUsed(GridView1.DataKeys[e.Row.RowIndex]["IFTypeID"].ToString()))
                {
                    ((LinkButton)e.Row.Cells[4].Controls[0]).Attributes.Add("onclick",
                        "javascript:return confirm('你确定要删除当前记录吗?')");
                }
                else
                {
                    e.Row.Cells[4].ForeColor = Color.Gray;
                    e.Row.Cells[4].Enabled = false;
                }
            }
        }
    }


    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            //双击后编辑修改方法
            return string.Format("javascript:var win=openWinCenter('TJ_InfoTypeAddEdit.aspx?cmd=edit&ID={0}',400,300,'咨询类别编辑')", ID);
        }
        else
        {
            return "";
        }
    }
    private bool CheckIsUsed(string ITPID)
    {
        if (bpubinfo.CheckIsExistByFilterString("CID=" + ITPID))
        {
            if (bll.CheckIsExistByFilterString("ParentID=" + ITPID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    // private const string Filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        if (IsSuperAdmin())
        {
            string Filtertemp = "1=1";
            if (inputSearchKeyword.Value.Trim().Length > 0)
            { 
                Filtertemp = " " + DDLField.SelectedValue +" like '%" + inputSearchKeyword.Value.Trim() + "%'"; 
            } 
            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(IFTypeID) from TJ_InfoType where " + Filtertemp, null).Rows[0]
[0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_InfoType", Filtertemp, "CompID", "IFTypeID", pageSize);
            GridView1.DataBind();
        }
        else
        {
            string Filtertemp = "CompID=" + GetCookieCompID();
            if (inputSearchKeyword.Value.Trim().Length > 0)
            {

                Filtertemp = "CompID =" + GetCookieCompID() + " and " + DDLField.SelectedValue +
                                             " like '%" + inputSearchKeyword.Value.Trim() + "%'";
            }
            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_InfoType where " + Filtertemp, null).Rows[0]
[0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_InfoType", Filtertemp, "IFTypeID", "IFTypeID", pageSize);
            GridView1.DataBind();
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