using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.IO;

public partial class Admin_TJ_RegisterInnerCompanysAddEdit : AuthorPage
{
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();
    private MTJ_RegisterCompanys mod = new MTJ_RegisterCompanys();
    private readonly BTJ_CompanyGoodsTypes bcomptypes = new BTJ_CompanyGoodsTypes();
    private readonly BTJ_BaseClass bllbase = new BTJ_BaseClass();
    private readonly CommonFun comfun = new CommonFun();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
            }
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim();
            }
            FillComBox();
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    break;
                case "edit":
                    Button1.Text = "修改";
                    if (HF_ID.Value == null || HF_ID.Value == "")
                    {
                        HF_ID.Value = GetCookieCompID();
                        inputCompName.Disabled = true;
                        ComboBox_GoodsTypeID.Enabled = false;
                        I2.Visible = false;
                    }

                    fillinput(int.Parse(HF_ID.Value.Trim()));
                    break;
                default:
                    break;
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.ToLower().Trim().Equals("edit"))
        {
            mod = bll.GetList(int.Parse(HF_ID.Value));
        }
        mod.ParentID = Convert.ToInt32(GetCookieCompID());
        mod.ProductTypeID = Convert.ToInt32(ComboBox_GoodsTypeID.SelectedValue);
        mod.AccTypeID = Convert.ToInt32(ComboBox_AccTypeID.SelectedValue.Trim());
        mod.CTID = Convert.ToInt32(ComboBox_CTID.SelectedValue);
        mod.CompAutherID = Convert.ToInt32(ComboBox_CompAutherID.SelectedValue.Trim());
        mod.CompName = inputCompName.Value.Trim();
        mod.CompLogo = HF_LogoImage.Value.Trim();
        mod.CompanyWebSite = inputCompanyWebSite.Value.Trim();
        mod.LegalPerson = inputLegalPerson.Value.Trim();
        mod.Address = inputAddress.Value.Trim();
        mod.TelNumber = inputTelNumber.Value.Trim();
        mod.FaxNumber = inputFaxNumber.Value.Trim();
        mod.EMail = inputEMail.Value.Trim();
        mod.ZhuCeZiJin = Convert.ToDecimal(inputZhuCeZiJin.Value.Trim());
        mod.AccountNumber = inputAccountNumber.Value.Trim();
        mod.TaxRegisterCode = inputTaxRegisterCode.Value.Trim();
        //mod.BusinessLicencePicture = HF_LectureImage.Value.Trim();
        mod.Remarks = inputRemarks.Text.Trim();
        mod.ReturnDiscount = 100;
        mod.DetailDiscription = textarea.Value.Trim();
        mod.ShowMinPrice = Convert.ToDecimal(TextBox_MinPrice.Text);
        string AuthorDiscountString = "";
        foreach (ListItem item in CheckBoxList_Discout.Items)
        {
            if (item.Selected)
            {
                AuthorDiscountString += "," + item.Value;
            }
        }
        if (AuthorDiscountString.Length > 0)
        {
            AuthorDiscountString += ",";
        }
        mod.AuthorDiscount = AuthorDiscountString;
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.RegisterDate = DateTime.Now;
                mod.AuthoredDate = Convert.ToDateTime("1900-01-01");
                mod.DisAuthorDate = Convert.ToDateTime("1900-01-01");
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        // ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('操作成功');", true);
        //this.Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTJ_RegisterCompanys ms = bll.GetList(id);
        ComboBox_GoodsTypeID.SelectedValue = ms.ProductTypeID.ToString().Trim();
        ComboBox_AccTypeID.SelectedValue = ms.AccTypeID.ToString();
        ComboBox_CTID.SelectedValue = ms.CTID.ToString().Trim();
        ComboBox_CompAutherID.SelectedValue = ms.CompAutherID.ToString().Trim();
        inputCompName.Value = ms.CompName.Trim();
        Image_Logo.ImageUrl = ms.CompLogo.Trim();
        HF_LogoImage.Value = ms.CompLogo.Trim();
        inputCompanyWebSite.Value = ms.CompanyWebSite.Trim();
        inputLegalPerson.Value = ms.LegalPerson.Trim();
        inputAddress.Value = ms.Address.Trim();
        inputTelNumber.Value = ms.TelNumber.Trim();
        inputFaxNumber.Value = ms.FaxNumber.Trim();
        inputEMail.Value = ms.EMail.Trim();
        inputZhuCeZiJin.Value = ms.ZhuCeZiJin.ToString().Trim();
        inputAccountNumber.Value = ms.AccountNumber.Trim();
        inputTaxRegisterCode.Value = ms.TaxRegisterCode.Trim();
        Image_LectureImage.ImageUrl = ms.BusinessLicencePicture.Trim();
        //HF_LectureImage.Value = ms.BusinessLicencePicture.Trim();
        inputRemarks.Text = ms.Remarks.Trim();
        textarea.Value = ms.DetailDiscription;
        TextBox_MinPrice.Text = ms.ShowMinPrice.ToString("0.00");
        foreach (ListItem item in CheckBoxList_Discout.Items)
        {
            if (ms.AuthorDiscount.Contains("," + item.Value + ","))
            {
                item.Selected = true;
            }
            else
            {
                item.Selected = false;
            }
        }
    }

    private void FillComBox()
    {
        ComboBox_GoodsTypeID.DataSource = bcomptypes.GetListsByFilterString("CompID=" + GetCookieCompID());
        ComboBox_GoodsTypeID.DataBind();
        comfun.BindTreeCombox(ComboBox_AccTypeID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.AccountType,
            "请选择...", true, "-", "");
        ComboBox_AccTypeID.SelectedValue = "0";
        comfun.BindTreeCombox(ComboBox_CompAutherID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.CompStatues,
            "请选择...", true, "-", "");
        ComboBox_CompAutherID.SelectedValue = "0";
        comfun.BindTreeCombox(ComboBox_CTID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "请选择...", true,
            "-", "");
        ComboBox_CTID.SelectedValue = "0";
        CheckBoxList_Discout.DataSource = bllbase.GetListsByFilterString("ParentID=" + DAConfig.CompnyDiscount);
        CheckBoxList_Discout.DataBind();
    }

    public bool CheckFileType(string fileName)
    {
        //获取文件的扩展名,前提要用这个方法必须引入命名空间io

        string ext = Path.GetExtension(fileName);

        switch (ext.ToLower())
        {
            case ".gif":

                return true;

            case ".png":

                return true;

            case ".jpeg":

                return true;

            case "jpg":

                return true;

            default:

                return false;
        }
    }
}