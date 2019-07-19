using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_Activity_JXS_WinAddEdit : AuthorPage
{
    BTJ_Activity_JXS_Win bll = new BTJ_Activity_JXS_Win();
    MTJ_Activity_JXS_Win mod = new MTJ_Activity_JXS_Win();
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
        mod.compid = Convert.ToInt32(inputcompid.Value.Trim());
        mod.agentid = Convert.ToInt32(inputagentid.Value.Trim());
        mod.awtypeid = Convert.ToInt32(inputawtypeid.Value.Trim());
        mod.winreason = inputwinreason.Value.Trim();
        mod.prizevl = Convert.ToDecimal(inputprizevl.Value.Trim());
        mod.prizeintro = inputprizeintro.Value.Trim();
        mod.gettm = Convert.ToDateTime(inputgettm.Value.Trim());
        mod.confirmtm = Convert.ToDateTime(inputconfirmtm.Value.Trim());
        mod.remarks = inputremarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_Activity_JXS_WinAddEdit.aspx", "TJ_Activity_JXS_Win", "描述",
                    System.DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                bll.Modify(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_Activity_JXS_WinAddEdit.aspx", "TJ_Activity_JXS_Win", "描述",
                    System.DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "reload", "closemyWindow();", true);
    }

    private void fillinput(int id)
    {
        MTJ_Activity_JXS_Win ms = bll.GetList(id);
        inputid.Value = ms.id.ToString().Trim();
        inputcompid.Value = ms.compid.ToString().Trim();
        inputagentid.Value = ms.agentid.ToString().Trim();
        inputawtypeid.Value = ms.awtypeid.ToString().Trim();
        inputwinreason.Value = ms.winreason.Trim();
        inputprizevl.Value = ms.prizevl.ToString();
        inputprizeintro.Value = ms.prizeintro.Trim();
        inputgettm.Value = ms.gettm.ToString().Trim();
        inputconfirmtm.Value = ms.confirmtm.ToString().Trim();
        inputremarks.Value = ms.remarks.Trim();
        inputislq.Value = ms.islq.ToString().Trim();
    }
}