using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_CustomerAdviceAddEdit : AuthorPage
{
    BTJ_CustomerAdvice bll = new BTJ_CustomerAdvice();
    MTJ_CustomerAdvice mod = new MTJ_CustomerAdvice();
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
        mod.advtitle = inputadvtitle.Value.Trim();
        mod.advcontent = inputadvcontent.Value.Trim();
        mod.customerphone = inputcustomerphone.Value.Trim();
        mod.customername = inputcustomername.Value.Trim();
        mod.adtime = Convert.ToDateTime(inputadtime.Value.Trim());
        mod.userid = Convert.ToInt32(inputuserid.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_CustomerAdviceAddEdit.aspx","TJ_CustomerAdvice","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_CustomerAdviceAddEdit.aspx","TJ_CustomerAdvice","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTJ_CustomerAdvice ms = bll.GetList(id);
        inputid.Value = ms.id.ToString().Trim();
        inputadvtitle.Value = ms.advtitle.Trim();
        inputadvcontent.Value = ms.advcontent.Trim();
        inputcustomerphone.Value = ms.customerphone.Trim();
        inputcustomername.Value = ms.customername.Trim();
        inputadtime.Value = ms.adtime.ToString().Trim();
        inputuserid.Value = ms.userid.ToString().Trim();
    }
}