using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TB_SuYuanPaiAddEdit : AuthorPage
{
    readonly BTB_SuYuanPai bll = new BTB_SuYuanPai();
    MTB_SuYuanPai mod = new MTB_SuYuanPai();
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
                Fillinput(int.Parse(HF_ID.Value.Trim()));
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
        mod.BianMa = inputBianMa.Value.Trim();
        mod.Name = inputName.Value.Trim();
        mod.Compid = Convert.ToInt32(GetCookieCompID());
        mod.Remarks = inputRemarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanPaiAddEdit.aspx","TB_SuYuanPai","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanPaiAddEdit.aspx","TB_SuYuanPai","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void Fillinput(int id)
    {
        MTB_SuYuanPai ms = bll.GetList(id); 
        inputBianMa.Value = ms.BianMa.Trim();
        inputName.Value = ms.Name.Trim(); 
        inputRemarks.Value = ms.Remarks.Trim();
    }
}