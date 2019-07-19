using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TB_SuYuanZhiQuAndGouDuiAddEdit : AuthorPage
{
    readonly BTB_SuYuanZhiQuAndGouDui bll = new BTB_SuYuanZhiQuAndGouDui();
    MTB_SuYuanZhiQuAndGouDui mod = new MTB_SuYuanZhiQuAndGouDui();
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
       
        mod.ZQID = Convert.ToInt32(inputZQID.Value.Trim());
        mod.GDID = Convert.ToInt32(inputGDID.Value.Trim());
        mod.PercentValue = Convert.ToDecimal(inputPercentValue.Value.Trim());
        mod.Remarks = Convert.ToInt32(inputRemarks.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanZhiQuAndGouDuiAddEdit.aspx","TB_SuYuanZhiQuAndGouDui","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanZhiQuAndGouDuiAddEdit.aspx","TB_SuYuanZhiQuAndGouDui","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTB_SuYuanZhiQuAndGouDui ms = bll.GetList(id);
      
        inputZQID.Value = ms.ZQID.ToString().Trim();
        inputGDID.Value = ms.GDID.ToString().Trim();
        inputPercentValue.Value = ms.PercentValue.ToString().Trim();
        inputRemarks.Value = ms.Remarks.ToString().Trim();
    }
}