using System; 
using System.Web.UI; 
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_LogisticsCompanyAddEdit : AuthorPage
{
    BTJ_LogisticsCompany bll = new BTJ_LogisticsCompany();
    MTJ_LogisticsCompany mod = new MTJ_LogisticsCompany();
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
        mod.logisticcompany = inputlogisticcompany.Value.Trim();
        mod.codestr = inputcodestr.Value.Trim();
        mod.queryinterfacestr = inputqueryinterfacestr.Value.Trim();
        mod.lastupdatetm = DateTime.Now;
        mod.updateuserid = Convert.ToInt32(GetCookieUID());
        mod.updateusernm = GetCookieTJUName();
        mod.remarks = inputremarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_LogisticsCompanyAddEdit.aspx","TJ_LogisticsCompany","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_LogisticsCompanyAddEdit.aspx","TJ_LogisticsCompany","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTJ_LogisticsCompany ms = bll.GetList(id); 
        inputlogisticcompany.Value = ms.logisticcompany.Trim();
        inputcodestr.Value = ms.codestr.Trim();
        inputqueryinterfacestr.Value = ms.queryinterfacestr.Trim(); 
        inputremarks.Value = ms.remarks.Trim();
    }
}