using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_User_SystemAddEdit : AuthorPage
{
    BTJ_User_System bll = new BTJ_User_System();
    MTJ_User_System mod = new MTJ_User_System();
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
        mod.UserID = Convert.ToInt32(inputUserID.Value.Trim());
        mod.ParentID = Convert.ToInt32(inputParentID.Value.Trim());
        mod.CompID = Convert.ToInt32(inputCompID.Value.Trim());
        mod.RID = Convert.ToInt32(inputRID.Value.Trim());
        mod.IdentityCode = inputIdentityCode.Value.Trim();
        mod.LoginName = inputLoginName.Value.Trim();
        mod.PassWords = inputPassWords.Value.Trim();
        mod.NickName = inputNickName.Value.Trim();
        mod.SexInfo = inputSexInfo.Value.Trim();
        mod.RegisterDate = Convert.ToDateTime(inputRegisterDate.Value.Trim());
        mod.IsActived = Convert.ToInt32(inputIsActived.Value.Trim());
        mod.FromCityID = Convert.ToInt32(inputFromCityID.Value.Trim());
        mod.AddressInfo = inputAddressInfo.Value.Trim();
        mod.PostCode = inputPostCode.Value.Trim();
        mod.SystemPermission = inputSystemPermission.Value.Trim();
        mod.IntegralValue = Convert.ToDecimal(inputIntegralValue.Value.Trim());
        mod.Remarks = inputRemarks.Value.Trim();
        mod.WXGongZhongHao = inputWXGongZhongHao.Value.Trim();
        mod.WXDengLuYouXiang = inputWXDengLuYouXiang.Value.Trim();
        mod.WXYuanShiID = inputWXYuanShiID.Value.Trim();
        mod.WXNumber = inputWXNumber.Value.Trim();
        mod.WXLeiXing = inputWXLeiXing.Value.Trim();
        mod.WXRenZhengQingKuang = inputWXRenZhengQingKuang.Value.Trim();
        mod.WXToken = inputWXToken.Value.Trim();
        mod.WXSignature = inputWXSignature.Value.Trim();
        mod.WXTimesStamp = inputWXTimesStamp.Value.Trim();
        mod.WXOnece = inputWXOnece.Value.Trim();
        mod.WXEchoStrnig = inputWXEchoStrnig.Value.Trim();
        mod.WXIsYanZheng = Convert.ToBoolean(inputWXIsYanZheng.Value.Trim());
        mod.HeaderImageUrl = inputHeaderImageUrl.Value.Trim();
        mod.AuthorDiscount = inputAuthorDiscount.Value.Trim();
        mod.MobileNumber = inputMobileNumber.Value.Trim();
        mod.WX_Province = inputWX_Province.Value.Trim();
        mod.WX_City = inputWX_City.Value.Trim();
        mod.reg_date = inputreg_date.Value.Trim();
        mod.reg_year = Convert.ToInt32(inputreg_year.Value.Trim());
        mod.reg_month = Convert.ToInt32(inputreg_month.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_User_SystemAddEdit.aspx","TJ_User_System","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_User_SystemAddEdit.aspx","TJ_User_System","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTJ_User_System ms = bll.GetList(id);
        inputUserID.Value = ms.UserID.ToString().Trim();
        inputParentID.Value = ms.ParentID.ToString().Trim();
        inputCompID.Value = ms.CompID.ToString().Trim();
        inputRID.Value = ms.RID.ToString().Trim();
        inputIdentityCode.Value = ms.IdentityCode.Trim();
        inputLoginName.Value = ms.LoginName.Trim();
        inputPassWords.Value = ms.PassWords.Trim();
        inputNickName.Value = ms.NickName.Trim();
        inputSexInfo.Value = ms.SexInfo.Trim();
        inputRegisterDate.Value = ms.RegisterDate.ToString().Trim();
        inputIsActived.Value = ms.IsActived.ToString().Trim();
        inputFromCityID.Value = ms.FromCityID.ToString().Trim();
        inputAddressInfo.Value = ms.AddressInfo.Trim();
        inputPostCode.Value = ms.PostCode.Trim();
        inputSystemPermission.Value = ms.SystemPermission.Trim();
        inputIntegralValue.Value = ms.IntegralValue.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        inputWXGongZhongHao.Value = ms.WXGongZhongHao.Trim();
        inputWXDengLuYouXiang.Value = ms.WXDengLuYouXiang.Trim();
        inputWXYuanShiID.Value = ms.WXYuanShiID.Trim();
        inputWXNumber.Value = ms.WXNumber.Trim();
        inputWXLeiXing.Value = ms.WXLeiXing.Trim();
        inputWXRenZhengQingKuang.Value = ms.WXRenZhengQingKuang.Trim();
        inputWXToken.Value = ms.WXToken.Trim();
        inputWXSignature.Value = ms.WXSignature.Trim();
        inputWXTimesStamp.Value = ms.WXTimesStamp.Trim();
        inputWXOnece.Value = ms.WXOnece.Trim();
        inputWXEchoStrnig.Value = ms.WXEchoStrnig.Trim();
        inputWXIsYanZheng.Value = ms.WXIsYanZheng.ToString().Trim();
        inputHeaderImageUrl.Value = ms.HeaderImageUrl.Trim();
        inputAuthorDiscount.Value = ms.AuthorDiscount.Trim();
        inputMobileNumber.Value = ms.MobileNumber.Trim();
        inputWX_Province.Value = ms.WX_Province.Trim();
        inputWX_City.Value = ms.WX_City.Trim();
        inputreg_date.Value = ms.reg_date.Trim();
        inputreg_year.Value = ms.reg_year.ToString().Trim();
        inputreg_month.Value = ms.reg_month.ToString().Trim();
    }
}