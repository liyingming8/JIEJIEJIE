using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_TJ_QueryRecorderInfo : AuthorPage
{
    private readonly BTJ_QueryRecorderInfo bll = new BTJ_QueryRecorderInfo();
    private readonly MTJ_QueryRecorderInfo mod = new MTJ_QueryRecorderInfo();
    private readonly BTJ_User buser = new BTJ_User();
    private readonly BTJ_BaseClass bbaseclass = new BTJ_BaseClass();
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
            else
            {
                fillgridview();
            }
        }
    }

    private void fillgridview()
    {
        string sqlstring =  "TJCOMPID=" + GetCookieCompID() + "";
        if (inputSearchKeyword.Value.Length > 0)
        {
            sqlstring += "and  " + DDLField.SelectedValue + " like  '%" + inputSearchKeyword.Value + "%'";
        }
        GridView1.DataSource = bll.GetListsByFilterString(sqlstring);
        GridView1.DataBind();
    }
    private  string Filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            Filtertemp = DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
        }
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_QueryRecorderInfo where " + Filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_QueryRecorderInfo", Filtertemp, "QRID", "QRID", pageSize);
        GridView1.DataBind();
    }

    public string ReturnUserName(string UserID)
    {
        if (UserID.Trim().Equals("0"))
        {
            return "不明确";
        }
        else
        {
            MTJ_User muser = new MTJ_User();
            muser = buser.GetList(int.Parse(UserID));
            return (muser.NickName.Equals("") ? muser.LoginName : muser.NickName + "(" + muser.LoginName + ")") +
                   (muser.FromCityID.Equals(0) ? "" : "来自:" + bbaseclass.GetList(muser.FromCityID).CName);
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["QRID"].ToString()));
        fillgridview();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgridview();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgridview();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        mod.QRID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtQRID")).Text.Trim());
        mod.TJCOMPID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtTJCOMPID")).Text.Trim());
        mod.LabelNumber = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtLabelNumber")).Text.Trim();
        mod.UserID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtUserID")).Text.Trim());
        mod.QueryDate =
            Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtQueryDate")).Text.Trim());
        mod.Remarks = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtRemarks")).Text.Trim();
        bll.Modify(mod);
        GridView1.EditIndex = -1;
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
            ((LinkButton) e.Row.Cells[4].Controls[0]).Attributes.Add("onclick",
                "javascript:return confirm('你确定要删除当前记录吗?')");
        }
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