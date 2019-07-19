using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using TJ.BLL;
using TJ.Model;
using System.Text;
using commonlib;

public partial class Admin_Default : AuthorPage
{
    private readonly BTJ_SiteMap bl = new BTJ_SiteMap();
    private readonly BTJ_RoleInfo blrole = new BTJ_RoleInfo();
    private readonly BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MTJ_RegisterCompanys mcopany = bcompany.GetList(int.Parse(GetCookieCompID()));
            literal_title.Text = mcopany.CompName.Trim();
            logo.Src = mcopany.CompLogo;
            logo.Alt = mcopany.CompName;
            //BindCss(mcopany.CSSName);
            string AuthorMenuString =
                blrole.GetList(int.Parse(HttpUtility.UrlDecode(Request.Cookies["TJRID"].Value))).AuthorMenuInfo;
            IList<MTJ_SiteMap> slist =
                bl.GetListsByFilterString("ParentID=0 and SiteID in (" + AuthorMenuString.Substring(1) + ")");
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (MTJ_SiteMap dr in slist)
            {
                sb.Append("<li id=\"Menu" + i + "\" onclick=\"ShowHideLayer('ChannelMenu_Menu" + i +
                          "')\"><a id=\"AChannelMenu_Menu" + i +
                          "\" onclick=\"javascript:main_right.window.location.href='welcome.aspx';\" href=\"LeftMenu.aspx?MID=" +
                          dr.SiteID + "\" target=\"left\"><span id=\"SpanChannelMenu_Menu" + i + "\">" +
                          dr.PageName.Trim() + "</span></a></li>");
                i++;
            }
            PH_TopMenu.Controls.Clear();
            PH_TopMenu.Controls.Add(new LiteralControl(sb.ToString()));
            PH_OperRater.Controls.Clear();
            PH_OperRater.Controls.Add(new LiteralControl(HttpUtility.UrlDecode(Request.Cookies["TJUName"].Value)));
        }
    }

    private void BindCss(string cssname)
    {
        HtmlGenericControl objLink = new HtmlGenericControl("LINK");
        objLink.Attributes["rel"] = "stylesheet";
        objLink.Attributes["type"] = "text/css";
        objLink.Attributes["href"] = "include/" + cssname;
        Page.Header.Controls.Add(objLink);
    }

    //string menuminfo = "";
    //string returnpath = "";
    //private string DefaultMenuLinkePathByRID(string RID,string ParentSiteID)
    //{
    //    returnpath = "";
    //    IList<MTJ_SiteMap> SiteList = bl.GetListsByFilterString("ParentID=" + ParentSiteID);
    //    menuminfo = blrole.GetAuthorMenuString(RID);
    //    foreach (MTJ_SiteMap dr in SiteList)
    //    {
    //        if (menuminfo.Contains("," + dr.SiteID.ToString().Trim() + ","))
    //        {
    //            returnpath = dr.LinkPath;                
    //            break;
    //        }
    //    }
    //    if (returnpath.Trim().Length == 0)
    //    {
    //        returnpath = "Admin/welcome.aspx";
    //    }
    //    return returnpath;
    //}    
}