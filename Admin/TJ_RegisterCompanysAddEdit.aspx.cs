using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.IO;

public partial class Admin_TJ_RegisterCompanysAddEdit : AuthorPage
{
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();
    private MTJ_RegisterCompanys _mod = new MTJ_RegisterCompanys();
    private readonly CommonFun _comfun = new CommonFun();
    private readonly BTJ_RoleInfo _brole = new BTJ_RoleInfo(); 
    public string Temp;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["pcompid"]))
                {
                    hf_parentid.Value = Request.QueryString["pcompid"];
                    inputparentid.Value = bll.GetList(Convert.ToInt32(hf_parentid.Value)).CompName;
                }
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
                        if (string.IsNullOrEmpty(HF_ID.Value))
                        {
                            HF_ID.Value = GetCookieCompID();
                            inputCompName.Disabled = true;
                            ComboBox_CompTypeID.Enabled = false;
                            //I2.Visible = false;
                        }
                        Fillinput(int.Parse(HF_ID.Value.Trim()));
                        break; 
                } 
            }
            catch (Exception a)
            {
                string s = a.ToString();

                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert(" + s + ");", true);
            }
            inputparentid.Attributes.Add("onclick", ReturnCompnaySelectScript("指定父级单位","0",""));
        }
    }
    public string Functionstring()
    {
        return Temp;
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
                _mod = bll.GetList(int.Parse(HF_ID.Value));
            }
            if (HF_ID.Value.Trim() != GetCookieCompID())
            {
                _mod.CompTypeID = Convert.ToInt32(ComboBox_CompTypeID.SelectedValue);
            }
            _mod.ParentID = Convert.ToInt32(hf_parentid.Value.Length.Equals(0) ? "0" : hf_parentid.Value);
            _mod.AccTypeID = Convert.ToInt32(ComboBox_AccTypeID.SelectedValue.Trim());
            _mod.CTID = Convert.ToInt32(ComboBox_CTID.SelectedValue);
            _mod.CompAutherID = Convert.ToInt32(ComboBox_CompAutherID.SelectedValue.Trim());
            _mod.CompName = inputCompName.Value.Trim();
            _mod.CompLogo = HF_LogoImage.Value.Trim();
            _mod.CompanyWebSite = inputCompanyWebSite.Value.Trim();
            _mod.LegalPerson = inputLegalPerson.Value.Trim();
            _mod.Address = inputAddress.Value.Trim();
            _mod.TelNumber = inputTelNumber.Value.Trim();
            _mod.FaxNumber = inputFaxNumber.Value.Trim();
            _mod.EMail = inputEMail.Value.Trim();
            _mod.ZhuCeZiJin = 0;
            _mod.AccountNumber = inputAccountNumber.Value.Trim();
            _mod.TaxRegisterCode = "";
            _mod.BusinessLicencePicture = HF_LectureImage.Value.Trim();
            _mod.Position = inputPosition.Value;
            _mod.DetailDiscription = qiyejieshao.Value;
            _mod.WelcomePage = TextWelcomePage.Value;
            switch (HF_CMD.Value.Trim())
            {
                case "add":
                    _mod.RegisterDate = DateTime.Now;
                    _mod.AuthoredDate = Convert.ToDateTime("1900-01-01");
                    _mod.DisAuthorDate = Convert.ToDateTime("1900-01-01");
                    bll.Insert(_mod);
                    break;
                case "edit":
                    bll.Modify(_mod);
                    break;
            }
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
            //ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('操作成功！');", true);
        }
    }

    private void Fillinput(int id)
    {
        MTJ_RegisterCompanys ms = bll.GetList(id);
        ComboBox_CompTypeID.SelectedValue = ms.CompTypeID.ToString().Trim();
        ComboBox_AccTypeID.SelectedValue = ms.AccTypeID.ToString();
        ComboBox_CTID.SelectedValue = ms.CTID.ToString().Trim();
        ComboBox_CompAutherID.SelectedValue = ms.CompAutherID.ToString().Trim();
        inputCompName.Value = ms.CompName.Trim();
        if (ms.CompLogo.Trim().Length > 0)
        {
            Image_Logo.ImageUrl = ms.CompLogo.Trim();
        }
        HF_LogoImage.Value = ms.CompLogo.Trim();
        inputCompanyWebSite.Value = ms.CompanyWebSite.Trim();
        inputLegalPerson.Value = ms.LegalPerson.Trim();
        inputAddress.Value = ms.Address.Trim();
        inputTelNumber.Value = ms.TelNumber.Trim();
        inputFaxNumber.Value = ms.FaxNumber.Trim();
        inputEMail.Value = ms.EMail.Trim();
        inputAccountNumber.Value = ms.AccountNumber.Trim(); 
        HF_LectureImage.Value = ms.BusinessLicencePicture.Trim();
        qiyejieshao.Value = ms.DetailDiscription;
        inputPosition.Value = ms.Position;
        TextWelcomePage.Value = ms.WelcomePage;
    }

    private void FillComBox()
    {
        string comptypeidstring = _brole.GetList(int.Parse(GetCookieRID())).AuthorCompTypeID;
        if (comptypeidstring.Length > 0)
        {
            _comfun.BindTreeCombox(ComboBox_CompTypeID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.CompanyType,
                "请选择...", true, "-",
                "CID in(" + (comptypeidstring.StartsWith(",") ? comptypeidstring.Substring(1) : comptypeidstring) + ")");
            ComboBox_CompTypeID.SelectedValue = "0";
        }
        _comfun.BindTreeCombox(ComboBox_AccTypeID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.AccountType,
            "请选择...", true, "-", "");
        ComboBox_AccTypeID.SelectedValue = "0";
        _comfun.BindTreeCombox(ComboBox_CompAutherID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.CompStatues,
            "请选择...", true, "-", "");
        ComboBox_CompAutherID.SelectedValue = "0";
        _comfun.BindTreeCombox(ComboBox_CTID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "请选择...", true,
            "-", "");
        ComboBox_CTID.SelectedValue = "0";
    }

    public bool CheckFileType(string fileName)
    {
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