﻿using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_XF_ManageAuthorInfoAddEdit : AuthorPage
{
    BTJ_XF_ManageAuthorInfo bll = new BTJ_XF_ManageAuthorInfo();
    MTJ_XF_ManageAuthorInfo mod = new MTJ_XF_ManageAuthorInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
       {
          if (!string.IsNullOrEmpty(Request.QueryString["cmd"]))
          {
             HF_CMD.Value = Sc.DecryptQueryString(Request.QueryString["cmd"].Trim());
          }
          if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
          {
             HF_ID.Value = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
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
        mod.id = Convert.ToInt32(inputid.Value.Trim());
        mod.userid = Convert.ToInt32(inputuserid.Value.Trim());
        mod.rolename = inputrolename.Value.Trim();
        mod.fathoruserid = Convert.ToInt32(inputfathoruserid.Value.Trim());
        mod.authoruserid = Convert.ToInt32(inputauthoruserid.Value.Trim());
        mod.authordate = Convert.ToDateTime(inputauthordate.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_XF_ManageAuthorInfoAddEdit.aspx","TJ_XF_ManageAuthorInfo","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_XF_ManageAuthorInfoAddEdit.aspx","TJ_XF_ManageAuthorInfo","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTJ_XF_ManageAuthorInfo ms = bll.GetList(id);
        inputid.Value = ms.id.ToString().Trim();
        inputuserid.Value = ms.userid.ToString().Trim();
        inputrolename.Value = ms.rolename.ToString().Trim();
        inputfathoruserid.Value = ms.fathoruserid.ToString().Trim();
        inputauthoruserid.Value = ms.authoruserid.ToString().Trim();
        inputauthordate.Value = ms.authordate.ToString().Trim();
    }
}