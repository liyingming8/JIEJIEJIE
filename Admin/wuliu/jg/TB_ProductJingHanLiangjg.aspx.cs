﻿using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_TB_ProductJingHanLiangjg : AuthorPage
{
    private readonly BTB_ProductJingHanLiang bll = new BTB_ProductJingHanLiang();
    private MTB_ProductJingHanLiang mod = new MTB_ProductJingHanLiang();

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
                bll.GetListsByFilterString("CompID=" + GetCookieCompID() + " and " + DDLField.SelectedValue + " like '%" +
                                           inputSearchKeyword.Value.Trim() + "%'");
        }
        else
        {
            GridView1.DataSource = bll.GetListsByFilterString("CompID=" + GetCookieCompID());
        }
        GridView1.DataBind();
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
        mod.JingHanLiang = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtJingHanLiang")).Text.Trim();
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

    private readonly BTB_Products_Infor bproduct = new BTB_Products_Infor();

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!bproduct.CheckIsExistByFilterString("ProductJingHanLiang=" + GridView1.DataKeys[e.Row.RowIndex][0]))
            {
                ((LinkButton) e.Row.Cells[3].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')");
            }
            else
            {
                e.Row.Cells[3].Enabled = false;
                e.Row.Cells[3].ForeColor = Color.LightGray;
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