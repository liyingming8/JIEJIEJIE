using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TB_SuYuanZhiJianInfoAddEdit : AuthorPage
{
    readonly BTB_SuYuanZhiJianInfo bll = new BTB_SuYuanZhiJianInfo();
    MTB_SuYuanZhiJianInfo mod = new MTB_SuYuanZhiJianInfo();
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
        mod.ZhiJianName = inputZhiJianName.Value.Trim();
        mod.Phone = inputPhone.Value.Trim();
        mod.Compid = Convert.ToInt32(GetCookieCompID());
        mod.Remarks = inputRemarks.Value.Trim(); 
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanZhiJianInfoAddEdit.aspx","TB_SuYuanZhiJianInfo","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanZhiJianInfoAddEdit.aspx","TB_SuYuanZhiJianInfo","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTB_SuYuanZhiJianInfo ms = bll.GetList(id); 
        inputZhiJianName.Value = ms.ZhiJianName.Trim();
        inputPhone.Value = ms.Phone.Trim(); 
        inputRemarks.Value = ms.Remarks.Trim(); 
    }
}