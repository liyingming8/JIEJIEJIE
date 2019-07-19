using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_TJ_EffectiveQureyRecordInfo : AuthorPage
{
    private readonly BTJ_EffectiveQureyRecordInfo bll = new BTJ_EffectiveQureyRecordInfo();
    private readonly MTJ_EffectiveQureyRecordInfo mod = new MTJ_EffectiveQureyRecordInfo();
    private readonly BTJ_User blluser = new BTJ_User();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgridview();
        }
    }

    private void fillgridview()
    {
        GridView1.DataSource = bll.GetListsByFilterString(ReturnFilterString());
        GridView1.DataBind();
    }

    private string tempstring = "";

    private string ReturnFilterString()
    {
        tempstring = "CompID=" + GetCookieCompID();
        if (inputSearchKeyword.Value.Length > 0)
        {
            tempstring += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
        }
        return tempstring;
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
        mod.QRID = Convert.ToInt64(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtQRID")).Text.Trim());
        mod.UserID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtUserID")).Text.Trim());
        mod.CompID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCompID")).Text.Trim());
        mod.GoodsID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtGoodsID")).Text.Trim());
        mod.QueryMethod =
            Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtQueryMethod")).Text.Trim());
        mod.QueryDate =
            Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtQueryDate")).Text.Trim());
        mod.QueryPlace = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtQueryPlace")).Text.Trim();
        mod.QueryResult = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtQueryResult")).Text.Trim();
        mod.Remarks = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtRemarks")).Text.Trim();
        bll.Modify(mod);
        GridView1.EditIndex = -1;
        fillgridview();
    }

    public string GetUserName(string UID)
    {
        return blluser.GetList(int.Parse(UID)).NickName;
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
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton) e.Row.Cells[10].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')");
            }
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            GridView1.DataSource =
                bll.GetListsByFilterString(DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'");
        }
        else
        {
            GridView1.DataSource = bll.GetLists();
        }
        GridView1.DataBind();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview();
    }
}