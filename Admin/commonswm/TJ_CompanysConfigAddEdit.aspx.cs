using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.IO;

public partial class Admin_commonswm_TJ_CompanysConfigAddEdit : AuthorPage
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
                HF_CMD.Value = "edit";
                HF_ID.Value = GetCookieCompID();  
                FillComBox();
                Button1.Text = "修改";
                if (string.IsNullOrEmpty(HF_ID.Value))
                {
                    HF_ID.Value = GetCookieCompID();
                    inputCompName.Disabled = true;
                }
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
        _mod.CompLogo = HF_LogoImage.Value.Trim();
        _mod.Address = inputAddress.Value.Trim(); 
        _mod.DetailDiscription = qiyejieshao.Value;
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(_mod);
                break;
            case "edit":
                bll.Modify(_mod);
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "alert", "alert('修改成功！');", true);
    }

    private void Fillinput(int id)
    {
        MTJ_RegisterCompanys ms = bll.GetList(id);
        ComboBox_CompTypeID.SelectedValue = ms.CompTypeID.ToString().Trim(); 
        ComboBox_CTID.SelectedValue = ms.CTID.ToString().Trim();
        ComboBox_CompAutherID.SelectedValue = ms.CompAutherID.ToString().Trim();
        inputCompName.Value = ms.CompName.Trim();
        if (!string.IsNullOrEmpty(ms.CompLogo.Trim()))
        {
            Image_Logo.ImageUrl = ms.CompLogo.Trim();
            HF_LogoImage.Value = ms.CompLogo;
        } 
        inputLegalPerson.Value = ms.LegalPerson.Trim();
        inputAddress.Value = ms.Address.Trim(); 
        qiyejieshao.Value = ms.DetailDiscription; 
    }

    private void FillComBox()
    {
        _comfun.BindTreeCombox(ComboBox_CompTypeID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.CompanyType,
               "请选择...", true, "-", "CID in(47,48)");
        ComboBox_CompTypeID.SelectedValue = "0";
        ComboBox_CompAutherID.SelectedValue = "0";
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