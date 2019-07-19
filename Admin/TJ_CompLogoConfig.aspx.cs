using System;
using System.Web.UI;
using commonlib;
using TJ.BLL;
using TJ.Model;

public partial class Admin_TJ_CompLogoConfig : AuthorPage
{
    private MTJ_RegisterCompanys mcompany = new MTJ_RegisterCompanys();
    private readonly BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Image_Logo.ImageUrl = bcompany.GetList(int.Parse(GetCookieCompID())).CompLogo;
            HF_ImageURL.Value = Image_Logo.ImageUrl;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        mcompany = bcompany.GetList(int.Parse(GetCookieCompID()));
        mcompany.CompLogo = HF_ImageURL.Value;
        ScriptManager.RegisterStartupScript(this, GetType(), "info", "alert(设置成功!);", true);
    }
}