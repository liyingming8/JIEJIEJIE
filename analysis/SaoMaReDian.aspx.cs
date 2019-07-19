using commonlib;
using System;
using TJ.BLL;

public partial class Admin_SaoMaReDian : AuthorPage
{
    BTJ_RegisterCompanys _RegisterCompanys = new BTJ_RegisterCompanys();

    protected void Page_Load(object sender, EventArgs e)
    {
        string comp = DAConfig.Showmode.Equals("1") ? "2" : GetCookieCompID();
        string compName = _RegisterCompanys.GetList(int.Parse(comp)).CompName;
        TComp.Value = compName;
    }
}