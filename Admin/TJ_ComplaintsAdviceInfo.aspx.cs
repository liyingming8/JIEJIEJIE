using System;
using System.Web;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_TJ_ComplaintsAdviceInfo : AuthorPage
{
    private readonly BTJ_ComplaintsAdviceInfo bll = new BTJ_ComplaintsAdviceInfo();
    private MTJ_ComplaintsAdviceInfo mod = new MTJ_ComplaintsAdviceInfo();
    private readonly CommonFun confun = new CommonFun();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgridview();
        }
    }

    private void fillgridview()
    {
        GridView1.DataSource = bll.GetListsByFilterString("CompID=" + GetCookieCompID());
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["CAID"].ToString()));
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
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["CAID"].ToString()));
        mod.CompID = int.Parse(HttpUtility.UrlDecode(Request.Cookies["TJCOMPID"].Value));
        mod.CAID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCAID")).Text.Trim());
        mod.CID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCID")).Text.Trim());
        mod.PlaceID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtPlaceID")).Text.Trim());
        mod.ComplainsAdviceContents =
            ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtComplainsAdviceContents")).Text.Trim();
        mod.PublishDate =
            Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtPublishDate")).Text.Trim());
        mod.MobilePhone = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtMobilePhone")).Text.Trim();
        mod.Name = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtName")).Text.Trim();
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
                //((LinkButton)e.Row.Cells[8].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
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

    public string ReturnBaseName(string CID)
    {
        if (CID != "" && CID != "0")
        {
            return confun.ReturnBaseClassName(CID, false, false);
        }
        else
        {
            return "";
        }
    }
}