using System;
using commonlib;
using TJ.Model;
using TJ.BLL;
using TJ.DBUtility;
using System.Web.UI;

public partial class Admin_commonswm_TJ_RegistercommonswmShouQuanAndEdit : AuthorPage
{
    TabExecute _tab = new TabExecute();
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();
    private MTJ_RegisterCompanys _mod = new MTJ_RegisterCompanys();
    private readonly CommonFun _comfun = new CommonFun();
    private readonly BTJ_RoleInfo _brole = new BTJ_RoleInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                HF_ID.Value = Request.QueryString["ID"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["MasterID"]))
            {
                hf_parentid.Value = Request.QueryString["MasterID"];
            }
            comp.InnerText = "名称";
            intruct.InnerText = "介绍";
            images.InnerText = "上传图片";
            if (hf_parentid.Value.Equals("1"))
            {
                radOn.Checked = true;
                radOn.Enabled = false;
                radOff.Enabled = false;
            }
            else {
                radOff.Checked = true;
            }
            inputCompName.Disabled = true;
            qiyejieshao.Disabled = true;
            inputTelNumber.Disabled = true;
            inputAddress.Disabled = true;
            Fillinput(int.Parse(HF_ID.Value.Trim()));
        }
    }

    protected void Fillinput(int ID)
    {
        MTJ_RegisterCompanys ms = bll.GetList(ID);
        inputCompName.Value = ms.CompName.ToString().Trim();//CompTypeID.ToString().Trim();
        qiyejieshao.Value = ms.DetailDiscription.ToString();
        if (ms.BusinessLicencePicture.Trim().Length > 0)
        {
            Image_Logo.ImageUrl = "http://os.china315net.com/commonswm/" + ms.BusinessLicencePicture.Trim();
        }
        inputAddress.Value = ms.Address.Trim();
        inputTelNumber.Value = ms.MobilePhoneNumber.Trim();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
  
        //授权状态
        if (radOn.Checked)
        {
            MTJ_RegisterCompanys ms = bll.GetList(Convert.ToInt32(HF_ID.Value));
            _tab.ExecuteNonQuery("update TJ_RegisterCompanys set MasterID=1  where CompID=" + HF_ID.Value);
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);

    }
}