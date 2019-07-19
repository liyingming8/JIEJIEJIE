using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_SWM_Package_StandardAddEdit : AuthorPage
{
    BTJ_SWM_Package_Standard bll = new BTJ_SWM_Package_Standard();
    MTJ_SWM_Package_Standard mod = new MTJ_SWM_Package_Standard();
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
        mod.unitnm = inputunitnm.Value.Trim();
        mod.quantity = Convert.ToInt32(inputquantity.Value.Trim());
        mod.singleprice = Convert.ToDecimal(inputsingleprice.Value.Trim());
        mod.totalprice = Convert.ToDecimal(inputtotalprice.Value.Trim());
        mod.uptm = DateTime.Now;
        mod.upuserid = Convert.ToInt32(GetCookieUID());
        mod.remarks = inputremark.Value;
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_SWM_Package_StandardAddEdit.aspx", "TJ_SWM_Package_Standard", "描述", System.DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                bll.Modify(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_SWM_Package_StandardAddEdit.aspx", "TJ_SWM_Package_Standard", "描述", System.DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTJ_SWM_Package_Standard ms = bll.GetList(id); 
        inputunitnm.Value = ms.unitnm.Trim();
        inputquantity.Value = ms.quantity.ToString().Trim();
        inputsingleprice.Value = ms.singleprice.ToString().Trim();
        inputtotalprice.Value = ms.totalprice.ToString().Trim();
        inputremark.Value = mod.remarks;
    }
}