using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_SysModuleInfoAddEdit : AuthorPage
{
    BTJ_SysModuleInfo bll = new BTJ_SysModuleInfo();
    MTJ_SysModuleInfo mod = new MTJ_SysModuleInfo();
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
      //  mod.ID = Convert.ToInt32(inputID.Value.Trim());
        mod.ModuleName = inputModuleName.Value.Trim();
        mod.ShowOrder = Convert.ToInt32(inputShowOrder.Value.Trim());
        mod.ShowConent = inputShowConent.Value.Trim();
        mod.IsShow = ckb_IsShow.Checked;
        mod.Remarks = inputRemarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                // RecordDealLog(new MTJ_DealLog(0,"TJ_SysModuleInfoAddEdit.aspx","TJ_SysModuleInfo","描述",System.DateTime.Now,int.Parse(GetCookieUIDValue()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
              //   RecordDealLog(new MTJ_DealLog(0,"TJ_SysModuleInfoAddEdit.aspx","TJ_SysModuleInfo","描述",System.DateTime.Now,int.Parse(GetCookieUIDValue()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTJ_SysModuleInfo ms = bll.GetList(id);
       // inputID.Value = ms.ID.ToString().Trim();
        inputModuleName.Value = ms.ModuleName.Trim();
        inputShowOrder.Value = ms.ShowOrder.ToString().Trim();
        inputShowConent.Value = ms.ShowConent.Trim();
        ckb_IsShow.Checked = ms.IsShow;
        inputRemarks.Value = ms.Remarks.Trim();
    }
}