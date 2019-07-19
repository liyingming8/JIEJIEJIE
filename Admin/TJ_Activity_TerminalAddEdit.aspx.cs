using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_Activity_TerminalAddEdit : AuthorPage
{
    BTJ_Activity_Terminal bll = new BTJ_Activity_Terminal();
    MTJ_Activity_Terminal mod = new MTJ_Activity_Terminal();
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
        mod.acid = Convert.ToInt32(inputacid.Value.Trim());
        mod.terminalid = Convert.ToInt32(inputterminalid.Value.Trim());
        mod.terminalnm = inputterminalnm.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_Activity_TerminalAddEdit.aspx","TJ_Activity_Terminal","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_Activity_TerminalAddEdit.aspx","TJ_Activity_Terminal","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTJ_Activity_Terminal ms = bll.GetList(id);
        inputid.Value = ms.id.ToString().Trim();
        inputacid.Value = ms.acid.ToString().Trim();
        inputterminalid.Value = ms.terminalid.ToString().Trim();
        inputterminalnm.Value = ms.terminalnm.Trim();
    }
}