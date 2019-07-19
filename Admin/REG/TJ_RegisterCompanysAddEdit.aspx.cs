using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.IO;

public partial class Admin_REG_TJ_RegisterCompanysAddEdit : AuthorPage
{
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();
    private MTJ_RegisterCompanys mod = new MTJ_RegisterCompanys(); 
    private readonly CommonFun comfun = new CommonFun();
    private readonly BTJ_RoleInfo brole = new BTJ_RoleInfo();

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
            if (IsSuperAdmin())
            {
                ComboBox_ParentID.Enabled = true;
            }
            else
            {
                ComboBox_ParentID.Enabled = false;
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
                        ComboBox_CompTypeID.Enabled = false;
                        //I2.Visible = false;
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
        if (ComboBox_CompTypeID.SelectedValue == null || ComboBox_CompTypeID.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('请指定公司类型！');", true);
        }
        else
        {
            if (HF_CMD.Value.ToLower().Trim().Equals("edit"))
            {
                mod = bll.GetList(int.Parse(HF_ID.Value));
            }
            if (HF_ID.Value.Trim() != GetCookieCompID())
            {
                mod.CompTypeID = Convert.ToInt32(ComboBox_CompTypeID.SelectedValue);
            }
            mod.ParentID = Convert.ToInt32(ComboBox_ParentID.SelectedValue);
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
            mod.ZhuCeZiJin = 0;
            mod.AccountNumber = inputAccountNumber.Value.Trim();
            mod.TaxRegisterCode = "";
            mod.BusinessLicencePicture = HF_LectureImage.Value.Trim();
            mod.Position = inputPosition.Value;
            //mod.Remarks = inputRemarks.Text.Trim();
            mod.DetailDiscription = CommonFun.ReplaceHtmlNoImg(CKEditorControl01.Text.Trim());
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
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('操作成功！');", true);
        }
    }

    private void fillinput(int id)
    {
        MTJ_RegisterCompanys ms = bll.GetList(id);
        ComboBox_CompTypeID.SelectedValue = ms.CompTypeID.ToString().Trim();
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
        inputAccountNumber.Value = ms.AccountNumber.Trim();
        //Image_LectureImage.ImageUrl = ms.BusinessLicencePicture.ToString().Trim();
        HF_LectureImage.Value = ms.BusinessLicencePicture.Trim();
        //inputRemarks.Text = ms.Remarks.ToString().Trim();
        CKEditorControl01.Text = ms.DetailDiscription;
        inputPosition.Value = ms.Position;
        ComboBox_ParentID.SelectedValue = ms.ParentID.ToString();
    }

    private void FillComBox()
    {
        string comptypeidstring = brole.GetList(int.Parse(GetCookieRID())).AuthorCompTypeID;
        if (comptypeidstring.Length > 0)
        {
            comfun.BindTreeCombox(ComboBox_CompTypeID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.CompanyType,
                "请选择...", true, "-",
                "CID in(" + (comptypeidstring.StartsWith(",") ? comptypeidstring.Substring(1) : comptypeidstring) + ")");
            ComboBox_CompTypeID.SelectedValue = "0";
        }
        comfun.BindTreeCombox(ComboBox_ParentID, "CompName", "CompID", "ParentID", "TJ_RegisterCompanys", 0, "父级单位...",
            true, "-", "1=1");
        ComboBox_ParentID.SelectedValue = GetCookieCompID();
        //CBLIST_ProductType.DataSource = bcompgoodstype.GetListsByFilterString("CompID=" + GetCookieCompID());
        //CBLIST_ProductType.DataBind();
        //foreach(ListItem item in CBLIST_ProductType.Items)
        //{
        //    item.Selected = true;
        //}
        comfun.BindTreeCombox(ComboBox_AccTypeID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.AccountType,
            "请选择...", true, "-", "");
        ComboBox_AccTypeID.SelectedValue = "0";
        comfun.BindTreeCombox(ComboBox_CompAutherID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.CompStatues,
            "请选择...", true, "-", "");
        ComboBox_CompAutherID.SelectedValue = "0";
        comfun.BindTreeCombox(ComboBox_CTID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "请选择...", true,
            "-", "");
        ComboBox_CTID.SelectedValue = "0";
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