using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_YanLeiCodeActiveInfoAddEdit : AuthorPage
{
    BTJ_YanLeiCodeActiveInfo bll = new BTJ_YanLeiCodeActiveInfo();
    MTJ_YanLeiCodeActiveInfo mod = new MTJ_YanLeiCodeActiveInfo();
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
        mod.tablename = inputtablename.Value.Trim();
        mod.totalnum = Convert.ToInt32(inputtotalnum.Value.Trim());
        mod.activednum = Convert.ToInt32(inputactivednum.Value.Trim());
        mod.acpercent = Convert.ToDecimal(inputacpercent.Value.Trim());
        mod.updatedate = Convert.ToDateTime(inputupdatedate.Value.Trim());
        mod.notactivecodespan = inputnotactivecodespan.Value.Trim();
        mod.remarks = inputremarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_YanLeiCodeActiveInfoAddEdit.aspx","TJ_YanLeiCodeActiveInfo","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_YanLeiCodeActiveInfoAddEdit.aspx","TJ_YanLeiCodeActiveInfo","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTJ_YanLeiCodeActiveInfo ms = bll.GetList(id);
        inputid.Value = ms.id.ToString().Trim();
        inputtablename.Value = ms.tablename.Trim();
        inputtotalnum.Value = ms.totalnum.ToString().Trim();
        inputactivednum.Value = ms.activednum.ToString().Trim();
        inputacpercent.Value = ms.acpercent.ToString().Trim();
        inputupdatedate.Value = ms.updatedate.ToString().Trim();
        inputnotactivecodespan.Value = ms.notactivecodespan.Trim();
        inputremarks.Value = ms.remarks.Trim();
    }
}