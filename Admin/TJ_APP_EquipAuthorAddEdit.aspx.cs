using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_APP_EquipAuthorAddEdit : AuthorPage
{
    BTJ_APP_EquipAuthor bll = new BTJ_APP_EquipAuthor();
    MTJ_APP_EquipAuthor mod = new MTJ_APP_EquipAuthor();
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
        mod.authorkey = CommonFun.Md5hash_String("tjswm"+Label_equipcode.Text).ToLower();
        mod.authoruserid = Convert.ToInt32(GetCookieUID());
        mod.authortm = DateTime.Now;
        mod.rmarks = inputrmarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_APP_EquipAuthorAddEdit.aspx", "TJ_APP_EquipAuthor", "描述",
                    System.DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                bll.Modify(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_APP_EquipAuthorAddEdit.aspx", "TJ_APP_EquipAuthor", "描述",
                    System.DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "reload", "closemyWindow();", true);
    }

    BTJ_RegisterCompanys btjRegister = new BTJ_RegisterCompanys();

    private void fillinput(int id)
    {
        MTJ_APP_EquipAuthor ms = bll.GetList(id);
        Label_compid.Text = btjRegister.GetList(ms.compid).CompName;
        Labelagentid.Text = btjRegister.GetList(ms.compid).CompName;
        Label_equipcode.Text = ms.equipidstr; 
        Label_registerdate.Text = ms.registertm.ToString().Trim(); 
        inputrmarks.Value = ms.rmarks.Trim();
    }
}