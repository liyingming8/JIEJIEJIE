using System;
using System.Data;
using System.Web.UI;
using TJ.DBUtility;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_SWM_Comp_ModulesConfigAddEdit : AuthorPage
{
    BTJ_SWM_Comp_ModulesConfig bll = new BTJ_SWM_Comp_ModulesConfig();
    MTJ_SWM_Comp_ModulesConfig mod = new MTJ_SWM_Comp_ModulesConfig();
    BTJ_SiteMap bSiteMap = new BTJ_SiteMap(); 
    public string LPH = "blue";
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
       {
           if (IsSuperAdmin())
           {
               adminrow.Visible = true;
           }
           else
           {
               adminrow.Visible = false;
           }
           LPH = GetLogoPath(GetCookieCompID());
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
                Fillinput(int.Parse(HF_ID.Value.Trim()));
                break; 
          }
       }
    }

    private string GetLogoPath(string compid)
    {
        string tempreturn = ""; ;
        var tab = new TabExecute();
        DataTable dttemp = tab.ExecuteQuery("select logopath from TJ_CompFrontPage_Config where compid=" + compid, null);
        if (dttemp.Rows.Count > 0)
        {
            tempreturn = dttemp.Rows[0]["logopath"].ToString();
        }
        else
        {
            tempreturn = "blue";
        }
        dttemp.Dispose();
        return tempreturn;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
           mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        } 
        mod.mdnm = inputmdnm.Value.Trim();
        mod.logourl = hd_imgpath.Value.Trim();
        mod.isshow = ckb_show.Checked;
        mod.updatetm = DateTime.Now;
        mod.showorder = int.Parse(string.IsNullOrEmpty(inputshoworder.Value) ? "0" : inputshoworder.Value);
        if (IsSuperAdmin())
        {
            mod.lk = inputlink.Value;
        } 
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_SWM_Comp_ModulesConfigAddEdit.aspx","TJ_SWM_Comp_ModulesConfig","描述",DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_SWM_Comp_ModulesConfigAddEdit.aspx","TJ_SWM_Comp_ModulesConfig","描述",DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void Fillinput(int id)
    {
        MTJ_SWM_Comp_ModulesConfig ms = bll.GetList(id); 
        inputmdnm.Value = ms.mdnm.Trim(); 
        img_logo.Src = ms.logourl.Contains("commup")?ms.logourl:"Images/swmlogo/" + LPH + "/" + ms.logourl;
        hd_imgpath.Value = ms.logourl.Trim();
        ckb_show.Checked = ms.isshow;
        inputshoworder.Value = ms.showorder.ToString();
        inputlink.Value = ms.lk;
    }
    protected void btn_return_Click(object sender, EventArgs e)
    {
        mod = bll.GetList(int.Parse(HF_ID.Value));
        MTJ_SiteMap msite = bSiteMap.GetList(mod.mdid);
        hd_imgpath.Value = msite.LogoName;
        mod.mdnm = msite.PageName;
        mod.logourl = msite.LogoName;
        bll.Modify(mod);
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }
}