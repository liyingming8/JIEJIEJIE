﻿using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_TJ_SYJGYS : AuthorPage
{
    private readonly BTJ_SYJGYS bll = new BTJ_SYJGYS();
    private MTJ_SYJGYS mod = new MTJ_SYJGYS();
    public BTB_Products_Infor bpro = new BTB_Products_Infor();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //fillgridview();
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_SYJGYSAddEdit.aspx?cmd=edit&ID={0}',600,400)", ID);
        }
        else
        {
            return "";
        }
    }

    private void fillgridview()
    {
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            GridView1.DataSource =
                bll.GetListsByFilterString(" labelcode like '%" + inputSearchKeyword.Value.Trim() + "%'");
        }
        else
        {
            GridView1.DataSource = bll.GetLists();
        }
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["id"].ToString()));
        fillgridview();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgridview();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
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