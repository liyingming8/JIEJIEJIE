using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TB_SuYuanZhiQuGongYiAddEdit : AuthorPage
{
    readonly BTB_SuYuanZhiQuGongYi bll = new BTB_SuYuanZhiQuGongYi();
    MTB_SuYuanZhiQuGongYi mod = new MTB_SuYuanZhiQuGongYi();
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
        
        mod.GongYiName = inputGongYiName.Value.Trim();
        mod.ImageUrl = HF_LogoImage.Value;
        mod.ShowerOrder = Convert.ToInt32(inputShowerOrder.Value.Trim());
        mod.Description = inputDescription.Value.Trim();
        mod.Compid = Convert.ToInt32(GetCookieCompID());
        mod.Remarks = inputRemarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanZhiQuGongYiAddEdit.aspx","TB_SuYuanZhiQuGongYi","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanZhiQuGongYiAddEdit.aspx","TB_SuYuanZhiQuGongYi","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTB_SuYuanZhiQuGongYi ms = bll.GetList(id);
      
        inputGongYiName.Value = ms.GongYiName.Trim();
        HF_LogoImage.Value  = ms.ImageUrl.Trim();
        Image_Logo.ImageUrl = ms.ImageUrl.Trim();
        inputShowerOrder.Value = ms.ShowerOrder.ToString().Trim();
        inputDescription.Value = ms.Description.Trim(); 
        inputRemarks.Value = ms.Remarks.Trim();
    }
}