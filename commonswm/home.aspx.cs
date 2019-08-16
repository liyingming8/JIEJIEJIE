using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using commonlib;
using TJ.BLL;
using TJ.DBUtility;
using TJ.Model;

public partial class commonswm_home : AuthorPage
{
    BTJ_RoleInfo _blrole = new BTJ_RoleInfo();
    public string CompanyName = "Frank Bar";
    public string LeftMenuString = ""; 
    public string HeaderImageURL =
        "http://thirdwx.qlogo.cn/mmopen/vi_32/fYRojK0MYGYZDZS2Vvmaw1pWH6oS3eCbaTHZMWGucOdzlDxtlfZS7j4UyMe0hLEtpD7TTfZL4odMIsv0wqh9QA/132";

    public string QuickLingString = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            CompanyName = GetCompName();   
            var httpCookie = Request.Cookies["TJOSCompTypeID"];
            if (httpCookie == null || string.IsNullOrEmpty(httpCookie.Value))
            {
                LeftMenuString = GetMenuString();
                BTJ_RegisterCompanys btjRegister = new BTJ_RegisterCompanys();
                Security sc = new Security();
                MTJ_RegisterCompanys mcomp = btjRegister.GetList(int.Parse(GetCookieCompID()));
                var tjCompTypeID = new HttpCookie("TJOSCompTypeID");
                tjCompTypeID.Value = sc.EncryptQueryString(mcomp.CompTypeID.ToString());
                tjCompTypeID.Expires.AddDays(1);
                Response.Cookies.Add(tjCompTypeID);  
            }
            LeftMenuString = GetMenuString();
            QuickLingString = LinkString();
        }
    }

    private string LinkString()
    {
        var btjCompRoles = new BTJ_Comp_Roles();
        IList<MTJ_Comp_Roles> comroleist = btjCompRoles.GetListsByFilterString("CompID=" + GetCookieCompID());
        string authormenuidstring = ReturnSwmCompanyAuthorMenuIDString(comroleist);
        return returnkuaijielink(authormenuidstring);
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
            IList<MTJ_SiteMap> sitelist = btjSite.GetListsByFilterString("SysTypeID <>" + DAConfig.Huizongtypeid + " and ParentID<>0 and SiteID in(" +
                                                                         authorsiteidstring + ")", "ShowOrder");
            foreach (var mtjSiteMap in sitelist)
            {
                sb.Append("<div class=\"col-3 h19\">\r\n   <a href=\"" + mtjSiteMap.LinkPath + "\" class=\"pt4 pb4\"><i class=\"f42 color-primary icon iconfont icon-iconfontliebiao1copy\"></i><p>" +
                          mtjSiteMap.PageName + "</p></a>\r\n</div>");
            }
            return sb.ToString();
        }
        return "";
    }

    readonly TabExecute _tbexe = new TabExecute();
    private int _rid = 0;
    private string GetMenuString()
    {
        _rid = int.Parse(GetCookieRID());
        if (!_rid.Equals(155))
        {
            string authorMenuString = _blrole.GetList(_rid).AuthorMenuInfo;
            return ReturnAuthorMenuString(authorMenuString);
        }
        var btjCompRoles = new BTJ_Comp_Roles();
        IList<MTJ_Comp_Roles> comroleist = btjCompRoles.GetListsByFilterString("CompID=" + GetCookieCompID());
        if (comroleist.Count > 0)
        {
            return ReturnSwmCompanyAuthorMunuString(comroleist);
        }
        return "";
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
        return ReturnAuthorMenuString(sb.ToString());
    }

    private string ReturnSwmCompanyAuthorMenuIDString(IEnumerable<MTJ_Comp_Roles> comroleist)
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

    private string ReturnAuthorMenuString(string authoredstr)
    {
        string authorMenuString = authoredstr;
        if (authorMenuString.Length > 0)
        {
            var sb = new StringBuilder();
            if (authorMenuString.Length > 0)
            {
                DataTable dt = _tbexe.ExecuteQuery("select  SiteID,PageName from TJ_SiteMap where SysTypeID<>" + DAConfig.Huizongtypeid + " and SysTypeID<>" + DAConfig.luodiyetypeid + " and ParentID=0 and SiteID in (" + (authorMenuString.StartsWith(",") ? authorMenuString.Substring(1) : authorMenuString) + ") order by ShowOrder", null);
                foreach (DataRow dr in dt.Rows)
                {
                    string temp = "<li><a href =\"menulist.aspx?psid=" + dr["SiteID"] + "\" class=\"pl3 color8\">" + dr["PageName"] +"</a></li>";
                    sb.Append(temp); 
                }
                dt.Dispose();
            }
            return sb.ToString();
        }
        return "";
    } 
}