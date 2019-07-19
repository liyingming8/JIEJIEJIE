using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_Comp_ZhengShuAddEdit : AuthorPage
{
    BTJ_Comp_ZhengShu bll = new BTJ_Comp_ZhengShu();
    MTJ_Comp_ZhengShu mod = new MTJ_Comp_ZhengShu();
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
        mod.zsnm = inputzsnm.Value.Trim();
        mod.picurlstring = savefilepath.Value;
        mod.updateuid = Convert.ToInt32(GetCookieUID());
        mod.updatetm = DateTime.Now;
        mod.isshow = ckb_isshow.Checked;
        mod.compid = Convert.ToInt32(GetCookieCompID());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_Comp_ZhengShuAddEdit.aspx","TJ_Comp_ZhengShu","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_Comp_ZhengShuAddEdit.aspx","TJ_Comp_ZhengShu","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTJ_Comp_ZhengShu ms = bll.GetList(id); 
        inputzsnm.Value = ms.zsnm.Trim();
        showimage.Src = ms.picurlstring.Trim();
        savefilepath.Value = ms.picurlstring;
        ckb_isshow.Checked = ms.isshow; 
    }
}