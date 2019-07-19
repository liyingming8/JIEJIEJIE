﻿using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_CompTheme_colorAddEdit : AuthorPage
{
    BTJ_CompTheme_color bll = new BTJ_CompTheme_color();
    MTJ_CompTheme_color mod = new MTJ_CompTheme_color();
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
        mod.layid = Convert.ToInt32(inputlayid.Value.Trim());
        mod.themecolor = inputthemecolor.Value.Trim();
        mod.path = inputpath.Value.Trim();
        mod.remarks = inputremarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_CompTheme_colorAddEdit.aspx","TJ_CompTheme_color","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_CompTheme_colorAddEdit.aspx","TJ_CompTheme_color","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTJ_CompTheme_color ms = bll.GetList(id);
        inputid.Value = ms.id.ToString().Trim();
        inputlayid.Value = ms.layid.ToString().Trim();
        inputthemecolor.Value = ms.themecolor.Trim();
        inputpath.Value = ms.path.Trim();
        inputremarks.Value = ms.remarks.Trim();
    }
}