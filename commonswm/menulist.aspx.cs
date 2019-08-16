using System;
using System.Collections.Generic;
using System.Text;
using commonlib;
using TJ.BLL;
using TJ.Model;

public partial class commonswm_menulist : System.Web.UI.Page
{
    public string QuickLingString = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["psid"]))
            {
                QuickLingString = returnkuaijielink(Request.QueryString["psid"]);
            }
        }
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
            IList<MTJ_SiteMap> sitelist = btjSite.GetListsByFilterString("SysTypeID <>" + DAConfig.Huizongtypeid + " and ParentID in(" +
                                                                         authorsiteidstring + ")", "ShowOrder");
            foreach (var mtjSiteMap in sitelist)
            {
                sb.Append("<div class=\"col-3 h16\">\r\n<a href=\"" + mtjSiteMap.LinkPath + "\" class=\"pt4 pb4\"><i class=\"f42 color-primary icon iconfont icon-iconfontliebiao1copy\"></i><p>" +
                          mtjSiteMap.PageName + "</p></a>\r\n</div>");
            }
            return sb.ToString();
        }
        return "";
    }
}