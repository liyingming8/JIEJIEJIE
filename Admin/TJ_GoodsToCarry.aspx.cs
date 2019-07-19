using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_TJ_GoodsToCarry : AuthorPage
{
    private readonly BTJ_GoodsToCarry bll = new BTJ_GoodsToCarry();
    private readonly MTJ_GoodsToCarry mod = new MTJ_GoodsToCarry();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgridview();
        }
    }

    private void fillgridview()
    {
        GridView1.DataSource = bll.GetLists();
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["GoodsToCarryID"].ToString()));
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
        mod.GoodsToCarryID =
            Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtGoodsToCarryID")).Text.Trim());
        mod.FromCTID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtFromCTID")).Text.Trim());
        mod.ToCTID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtToCTID")).Text.Trim());
        mod.UserID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtUserID")).Text.Trim());
        mod.Weight = Convert.ToDecimal(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtWeight")).Text.Trim());
        mod.Size = Convert.ToDecimal(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtSize")).Text.Trim());
        mod.UpdateTime =
            Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtUpdateTime")).Text.Trim());
        mod.LoadingTime =
            Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtLoadingTime")).Text.Trim());
        mod.NeedIntructions = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtNeedIntructions")).Text.Trim();
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
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton) e.Row.Cells[11].Controls[0]).Attributes.Add("onclick",
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