using System;
using System.Collections.Generic;
using System.Text;
using commonlib;
using TJ.BLL;
using TJ.Model;

public partial class views_home_consoleswm : AuthorPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string RID = GetCookieRID();
            if (GetCookieCompTypeID().EndsWith("484"))
            {
                LabelQyjs.Text = "自我介绍";
            }
            div_daiban.Visible = false;
            div_swmconfig.Visible = true;
            var btjCompRoles = new BTJ_Comp_Roles();
            IList<MTJ_Comp_Roles> comroleist = btjCompRoles.GetListsByFilterString("CompID=" + GetCookieCompID());
            string authormenuidstring = ReturnSwmCompanyAuthorMunuString(comroleist);
            literal_kuaijiefangshi.Text = returnkuaijielink(authormenuidstring);
        }
    }

    readonly BTJ_Role_Package _btjRolePackage = new BTJ_Role_Package();
    private string ReturnSwmCompanyAuthorMunuString(IEnumerable<MTJ_Comp_Roles> comroleist)
    {
        var sb = new StringBuilder();
        foreach (MTJ_Comp_Roles mtjCompRoles in comroleist)
        {
            if (mtjCompRoles.isactive)
            {
                sb.Append(_btjRolePackage.GetList(mtjCompRoles.rpackid).ridstring);
            }
        }
        return sb.ToString();
    }

    BTJ_SiteMap btjSite = new BTJ_SiteMap();
    private string returnkuaijielink(string authorsiteidstring)
    {
       authorsiteidstring = authorsiteidstring.StartsWith(",")
                ? authorsiteidstring.Substring(1)
                : authorsiteidstring;
            authorsiteidstring = authorsiteidstring.EndsWith(",")
                ? authorsiteidstring.Substring(0, authorsiteidstring.Length - 1)
                : authorsiteidstring;
        if (authorsiteidstring.Length > 0)
        {
            StringBuilder sb = new StringBuilder();
            IList<MTJ_SiteMap> sitelist = btjSite.GetListsByFilterString("SysTypeID=" + DAConfig.Huizongtypeid + " and ParentID<>0 and SiteID in(" +
                                       authorsiteidstring + ")","ShowOrder");
            foreach (var mtjSiteMap in sitelist)
            {
                sb.Append("<li class=\"layui-col-xs3\"><a lay-href=\"" + mtjSiteMap.LinkPath +
                          "\"><i class=\""+mtjSiteMap.LogoName+"\"></i><cite>" + mtjSiteMap.PageName +
                          "</cite></a></li>");
            }
            return sb.ToString();
        }
        return "";
    }
   
}