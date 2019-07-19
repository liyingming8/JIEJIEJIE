﻿using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_TJ_Integraljg : AuthorPage
{
    private readonly BTJ_Integral bll = new BTJ_Integral();
    private MTJ_Integral mod = new MTJ_Integral();
    private readonly BTJ_RegisterCompanys bllcomp = new BTJ_RegisterCompanys();
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
        GridView1.DataSource = bll.GetListsByFilterString("CompID=" + GetCookieCompID());
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["ITGRID"].ToString()));
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
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["ITGRID"].ToString()));
        //mod.IntegralName = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtIntegralName")).Text.Trim();
        //mod.PublishDate = Convert.ToDateTime(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtPublishDate")).Text.Trim());
        //mod.UserID = Convert.ToInt32(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtUserID")).Text.Trim());
        mod.BeginDate =
            Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtBeginDate")).Text.Trim());
        mod.EndDate = Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtEndDate")).Text.Trim());
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
                ((LinkButton) e.Row.Cells[7].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')");
            }
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            GridView1.DataSource =
                bll.GetListsByFilterString("CompID=" + GetCookieCompID() + " and " + DDLField.SelectedValue + " like '%" +
                                           inputSearchKeyword.Value.Trim() + "%'");
        }
        else
        {
            GridView1.DataSource = bll.GetListsByFilterString("CompID=" + GetCookieCompID());
        }
        GridView1.DataBind();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    public string ReturnCompanyName(string COMPID)
    {
        return bllcomp.GetList(int.Parse(COMPID)).CompName;
    }

    public string ReturnUserName(string UserID)
    {
        return blluser.GetList(int.Parse(UserID)).LoginName;
    }
}