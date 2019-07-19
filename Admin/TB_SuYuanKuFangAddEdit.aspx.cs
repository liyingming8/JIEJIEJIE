using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TB_SuYuanKuFangAddEdit : AuthorPage
{
    readonly BTB_SuYuanKuFang bll = new BTB_SuYuanKuFang();
    MTB_SuYuanKuFang mod = new MTB_SuYuanKuFang();
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
           FillDDL();
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
        mod.KuFangType = Convert.ToInt32(ComboBoxTypeID.SelectedValue);
        mod.KuFang = inputKuFang.Value.Trim();
        mod.Remarks = inputremarks.Value;
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.Compid = Convert.ToInt32(GetCookieCompID());
                bll.Insert(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanKuFangAddEdit.aspx","TB_SuYuanKuFang","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanKuFangAddEdit.aspx","TB_SuYuanKuFang","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void FillDDL()
    {
        CommonFun common = new CommonFun();
        common.BindTreeCombox(ComboBoxTypeID,"CName","CID","ParentID","TJ_BaseClass",int.Parse(DAConfig.SYCangKuLeiBie),"库房类别...",false,"","1=1");
        ComboBoxTypeID.SelectedValue = "0";
    }

    private void Fillinput(int id)
    {
        MTB_SuYuanKuFang ms = bll.GetList(id);
        ComboBoxTypeID.SelectedValue = ms.KuFangType.ToString(); 
        inputKuFang.Value = ms.KuFang.Trim(); 
        inputremarks.Value = ms.Remarks.Trim();
    }
}