using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_TJ_LotteryActivityDetail : AuthorPage
{
    private readonly BTJ_LotteryActivityDetail bll = new BTJ_LotteryActivityDetail();
    private readonly MTJ_LotteryActivityDetail mod = new MTJ_LotteryActivityDetail();

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
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["LADID"].ToString()));
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
        mod.LADID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtLADID")).Text.Trim());
        mod.LAID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtLAID")).Text.Trim());
        mod.GradeName = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtGradeName")).Text.Trim();
        mod.GradeID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtGradeID")).Text.Trim());
        mod.Numbers = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtNumbers")).Text.Trim());
        mod.AwardName = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtAwardName")).Text.Trim();
        mod.AwardPictrureURL = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtAwardPictrureURL")).Text.Trim();
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
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton) e.Row.Cells[8].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')");
            }
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview();
    }
}