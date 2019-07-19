﻿using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_TJ_PayDetail : AuthorPage
{
    private readonly BTJ_PayDetail bll = new BTJ_PayDetail();
    private MTJ_PayDetail mod = new MTJ_PayDetail();

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
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["ID"].ToString()));
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
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["ID"].ToString()));
        mod.CompID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCompID")).Text.Trim());
        mod.OrderIDString = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtOrderIDString")).Text.Trim();
        mod.PayOrderNo = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtPayOrderNo")).Text.Trim();
        mod.InnerOrderNo = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtInnerOrderNo")).Text.Trim();
        mod.ProductType = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtProductType")).Text.Trim();
        mod.PayMethod = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtPayMethod")).Text.Trim();
        mod.PayMoney = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtPayMoney")).Text.Trim();
        mod.Statues = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtStatues")).Text.Trim();
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
        fillgridview();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview();
    }
}