using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using System.Collections.Generic;

public partial class Admin_TJ_TraceInfo : AuthorPage
{
    private readonly BTJ_TraceInfo bll = new BTJ_TraceInfo();
    private MTJ_TraceInfo mod = new MTJ_TraceInfo();
    public CommonFun comfun = new CommonFun();
    public BTB_Products_Infor bproduct = new BTB_Products_Infor();

    private string ProID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgridview();
        }
    }

    private void fillgridview()
    {
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            GridView1.DataSource =
                bll.GetListsByFilterString(DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'");
        }
        else
        {
            IList<MTB_Products_Infor> mtbproductlist = bproduct.GetListsByFilterString("CompID=" + GetCookieCompID());
            if (mtbproductlist.Count > 0)
            {
                foreach (MTB_Products_Infor aa in mtbproductlist)
                {
                    ProID += aa.Infor_ID + ",";
                }
                ProID = ProID.Substring(0, ProID.Length - 1);
            }


            GridView1.DataSource = bll.GetListsByFilterString("WLProID in( " + ProID + ")");
        }
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["TRACEID"].ToString()));
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
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["TRACEID"].ToString()));
        mod.WLProID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtWLProID")).Text.Trim());
        mod.CID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCID")).Text.Trim());
        mod.ShowOrder = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtShowOrder")).Text.Trim());
        mod.Contents = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtContents")).Text.Trim();
        mod.LogoURL = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtLogoURL")).Text.Trim();
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
                ((LinkButton) e.Row.Cells[6].Controls[0]).Attributes.Add("onclick",
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