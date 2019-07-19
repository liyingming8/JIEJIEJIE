using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.IO;

public partial class Admin_TJ_RegisterCompanysLXWM : AuthorPage
{
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();
    private MTJ_RegisterCompanys _mod = new MTJ_RegisterCompanys(); 
    public string Temp;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                HF_ID.Value = GetCookieCompID();
                HF_CMD.Value = "edit";
                Button1.Text = "确定";
                Fillinput(int.Parse(HF_ID.Value.Trim()));
            }
            catch (Exception a)
            {
                string s = a.ToString();

                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert(" + s + ");", true);
            } 
        }
    }
    public string Functionstring()
    {
        return Temp;
    } 
     
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.ToLower().Trim().Equals("edit"))
        {
            _mod = bll.GetList(int.Parse(HF_ID.Value));
        }

        _mod.ParentID = Convert.ToInt32(hf_parentid.Value.Length.Equals(0) ? "0" : hf_parentid.Value); 
        _mod.CompName = inputCompName.Value.Trim();
        _mod.CompLogo = HF_LogoImage.Value.Trim();
        _mod.CompanyWebSite = inputCompanyWebSite.Value.Trim();
        _mod.LegalPerson = inputLegalPerson.Value.Trim();
        _mod.Address = inputAddress.Value.Trim();
        _mod.TelNumber = inputTelNumber.Value.Trim(); 
        _mod.EMail = inputEMail.Value.Trim();
        _mod.ZhuCeZiJin = 0; 
        _mod.TaxRegisterCode = "";
        _mod.BusinessLicencePicture = HF_LectureImage.Value.Trim(); 
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
    }

    private void Fillinput(int id)
    {
        MTJ_RegisterCompanys ms = bll.GetList(id); 
        inputCompName.Value = ms.CompName.Trim(); 
        HF_LogoImage.Value = ms.CompLogo.Trim();
        inputCompanyWebSite.Value = ms.CompanyWebSite.Trim();
        inputLegalPerson.Value = ms.LegalPerson.Trim();
        inputAddress.Value = ms.Address.Trim();
        inputTelNumber.Value = ms.TelNumber.Trim(); 
        inputEMail.Value = ms.EMail.Trim(); 
        HF_LectureImage.Value = ms.BusinessLicencePicture.Trim(); 
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