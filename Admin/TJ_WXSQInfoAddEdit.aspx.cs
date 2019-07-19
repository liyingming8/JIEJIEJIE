using System;
using System.Web.UI; 
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_WXSQInfoAddEdit : AuthorPage
{
    BTJ_WXSQInfo bll = new BTJ_WXSQInfo();
    MTJ_WXSQInfo mod = new MTJ_WXSQInfo();
    commfrank comfrank = new commfrank();
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
                inputCompID.Attributes.Add("onclick", ReturnCompnaySelectScript("所属单位","0",""));
                Button1.Text = "添加";
                break;
             case "edit":
                Button1.Text = "修改";
                Fillinput(int.Parse(HF_ID.Value.Trim()));
                break; 
          }
          if (!string.IsNullOrEmpty(Request.QueryString["pcompid"]))
          {
              hf_compid.Value = Request.QueryString["pcompid"];
              inputCompID.Value = comfrank.GetValueByID("nm", "TJ_RegisterCompanys", "CompID", "CompName", "",hf_compid.Value);
          }
       }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.CompID = Convert.ToInt32(inputCompID.Value.Trim());
        mod.Sid = Convert.ToInt32(inputSid.Value.Trim());
        mod.WX_Appid = inputWX_Appid.Value.Trim();
        mod.WX_Appsecret = inputWX_Appsecret.Value.Trim();
        mod.WX_Redirect_url = inputWX_Redirect_url.Value.Trim();
        mod.WX_CL_url = inputWX_CL_url.Value.Trim();
        mod.WX_Scope = RBL_Scope.SelectedValue;
        mod.WX_GZ = CheckBox_GZ.Checked;
        mod.OwnUrl = CheckBoxOwnUrl.Checked;
        mod.Remarkes = input_remarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_WXSQInfoAddEdit.aspx", "TJ_WXSQInfo", "描述", DateTime.Now,
                    int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                bll.Modify(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_WXSQInfoAddEdit.aspx", "TJ_WXSQInfo", "描述", DateTime.Now,
                    int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void Fillinput(int id)
    {
        MTJ_WXSQInfo ms = bll.GetList(id);
        hf_compid.Value = ms.CompID.ToString();
        inputCompID.Value = comfrank.GetValueByID("nm", "TJ_RegisterCompanys", "CompID", "CompName", "",hf_compid.Value); 
        inputSid.Value = ms.Sid.ToString().Trim();
        inputWX_Appid.Value = ms.WX_Appid.Trim();
        inputWX_Appsecret.Value = ms.WX_Appsecret.Trim();
        inputWX_Redirect_url.Value = ms.WX_Redirect_url.Trim();
        inputWX_CL_url.Value = ms.WX_CL_url.Trim();
        RBL_Scope.SelectedValue = ms.WX_Scope.ToLower().Trim();
        CheckBox_GZ.Checked = ms.WX_GZ;
        CheckBoxOwnUrl.Checked = ms.OwnUrl; 
        input_remarks.Value = ms.Remarkes.Trim();
    }
}