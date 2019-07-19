using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_XF_Contract_ProductAuthorInfoAddEdit : AuthorPage
{
    BTJ_XF_Contract_ProductAuthorInfo bll = new BTJ_XF_Contract_ProductAuthorInfo();
    MTJ_XF_Contract_ProductAuthorInfo mod = new MTJ_XF_Contract_ProductAuthorInfo();
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
        mod.ctrid = Convert.ToInt32(inputctrid.Value.Trim());
        mod.procode = inputprocode.Value.Trim();
        mod.authorizedarea = inputauthorizedarea.Value.Trim();
        mod.areaid = Convert.ToInt32(inputareaid.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_XF_Contract_ProductAuthorInfoAddEdit.aspx", "TJ_XF_Contract_ProductAuthorInfo", "描述", System.DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                bll.Modify(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_XF_Contract_ProductAuthorInfoAddEdit.aspx", "TJ_XF_Contract_ProductAuthorInfo", "描述", System.DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTJ_XF_Contract_ProductAuthorInfo ms = bll.GetList(id);
        inputid.Value = ms.id.ToString().Trim();
        inputctrid.Value = ms.ctrid.ToString().Trim();
        inputprocode.Value = ms.procode.Trim();
        inputauthorizedarea.Value = ms.authorizedarea.Trim();
        inputareaid.Value = ms.areaid.ToString().Trim();
    }
}