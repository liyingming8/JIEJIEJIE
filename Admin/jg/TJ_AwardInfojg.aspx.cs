using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_TJ_AwardInfojg : AuthorPage
{
    private readonly BTJ_AwardInfo bll = new BTJ_AwardInfo();
    private MTJ_AwardInfo mod = new MTJ_AwardInfo();
    private readonly BTJ_Integral bintegral = new BTJ_Integral();

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
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["AWID"].ToString()));
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
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["AWID"].ToString()));
        //mod.AWID = Convert.ToInt32(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtAWID")).Text.Trim());
        //mod.AGID = Convert.ToInt32(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtAGID")).Text.Trim());
        //mod.AwardThing = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtAwardThing")).Text.Trim();
        //mod.ImageURLString = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtImageURLString")).Text.Trim();
        //mod.SImageURLString = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtSImageURLString")).Text.Trim();
        //mod.Contents = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtContents")).Text.Trim();
        //mod.PublishDate = Convert.ToDateTime(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtPublishDate")).Text.Trim());
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
                ((LinkButton) e.Row.Cells[9].Controls[0]).Attributes.Add("onclick",
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

    public string ReturnIntegalName(string ATGID)
    {
        return bintegral.GetList(int.Parse(ATGID)).IntegralName;
    }
}