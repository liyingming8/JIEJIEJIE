﻿using System;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Web.UI;

public partial class Admin_TB_ProductXiangXingAddEdit : AuthorPage
{
    private readonly BTB_ProductXiangXing bll = new BTB_ProductXiangXing();
    private MTB_ProductXiangXing mod = new MTB_ProductXiangXing();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
            }
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim();
            }
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    break;
                case "edit":
                    Button1.Text = "修改";
                    fillinput(int.Parse(HF_ID.Value.Trim()));
                    break;
                default:
                    break;
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.XiangXing = inputXiangXing.Value.Trim();
        mod.Remarks = inputRemarks.Value.Trim();
        //===wyz===20170921==================
        if (mod.XiangXing.Equals(""))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('香型名称不能为空！');", true);
            return;
        }
        //===wyz===20170921==================
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.CompID = Convert.ToInt32(GetCookieCompID());
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        // Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTB_ProductXiangXing ms = bll.GetList(id);
        inputXiangXing.Value = ms.XiangXing.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}