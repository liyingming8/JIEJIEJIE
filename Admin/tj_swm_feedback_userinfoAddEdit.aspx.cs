using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_tj_swm_feedback_userinfoAddEdit : AuthorPage
{
    Btj_swm_feedback_userinfo bll = new Btj_swm_feedback_userinfo();
    Mtj_swm_feedback_userinfo mod = new Mtj_swm_feedback_userinfo();
    protected new void Page_Load(object sender, EventArgs e)
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
        mod.loginname = inputloginname.Value.Trim();
        if (ckb_default_psw.Checked)
        {
            mod.upsw = CommonFun.Md5hash_String("123456");
        }
        else
        {
            mod.upsw = CommonFun.Md5hash_String(inputupsw.Value.Trim());
        } 
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.createdate = DateTime.Now;
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"tj_swm_feedback_userinfoAddEdit.aspx","tj_swm_feedback_userinfo","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"tj_swm_feedback_userinfoAddEdit.aspx","tj_swm_feedback_userinfo","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_swm_feedback_userinfo ms = bll.GetList(id); 
        inputloginname.Value = ms.loginname.Trim();
        inputupsw.Value = ms.upsw.Trim(); 
    }
}