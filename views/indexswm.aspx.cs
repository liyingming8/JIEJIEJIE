using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using commonlib;
using TJ.BLL;
using TJ.DBUtility;
using TJ.Model;

public partial class views_indexswm : AuthorPage
{ 
    BTJ_RoleInfo _blrole = new BTJ_RoleInfo();  
    public string UserName = ""; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string menustring = GetMenuString();
            Literal mnliter = new Literal();
            mnliter.Text = menustring;
            UserName = GetCookieTJUName();
            ph_left_menu.Controls.Add(mnliter);
            var httpCookie = Request.Cookies["TJOSCompTypeID"];
            if (httpCookie == null || string.IsNullOrEmpty(httpCookie.Value))
            {
                BTJ_RegisterCompanys btjRegister = new BTJ_RegisterCompanys();
                Security sc = new Security();
                MTJ_RegisterCompanys mcomp = btjRegister.GetList(int.Parse(GetCookieCompID()));
                var tjCompTypeID = new HttpCookie("TJOSCompTypeID");
                tjCompTypeID.Value = sc.EncryptQueryString(mcomp.CompTypeID.ToString());
                tjCompTypeID.Expires.AddDays(1);
                Response.Cookies.Add(tjCompTypeID);
            } 
        }
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

    private string ReturnAuthorMenuString(string authoredstr)
    {
        string authorMenuString = authoredstr;
        if (authorMenuString.Length > 0) 
        {
            var sb = new StringBuilder();
            if (authorMenuString.Length > 0)
            {
                DataTable dt = _tbexe.ExecuteQuery("select  SiteID,PageName from TJ_SiteMap where SysTypeID<>" + DAConfig.Huizongtypeid + " and SysTypeID<>"+DAConfig.luodiyetypeid+" and ParentID=0 and SiteID in (" + (authorMenuString.StartsWith(",") ? authorMenuString.Substring(1) : authorMenuString) + ") order by ShowOrder", null);
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append(
                        "<li data-name=\"app\" class=\"layui-nav-item\"><a href=\"javascript:;\" lay-tips=\"" +
                        dr["PageName"] + "\" lay-direction=\"2\"><i class=\"layui-icon layui-icon-app\"></i><cite>" +
                        dr["PageName"] + "</cite><span class=\"layui-nav-more\"></span></a>");
                    sb.Append(SubSiteLinkString(dr["SiteID"].ToString(), authorMenuString));
                    sb.Append("</li>");
                }
                dt.Dispose();
            }
            return sb.ToString();
        }
        return "";
    }

    string _tempnickname = "";
    private string _linkurlstring = "";
    private string SubSiteLinkString(string pSiteID, string authorMenuString)
    {
        var sb = new StringBuilder();
        DataTable list = _tbexe.ExecuteQuery("select SiteID,LinkPath,PageName from TJ_SiteMap where ParentID=" + pSiteID + " and SysTypeID<>" + DAConfig.Huizongtypeid + " and SysTypeID<>" + DAConfig.luodiyetypeid+" order by  ShowOrder", null);
        if (list.Rows.Count > 0)
        {
            sb.Append("<dl class=\"layui-nav-child\">");
            foreach (DataRow dr in list.Rows)
            {
                if (authorMenuString.Contains("," + dr["SiteID"] + ","))
                {
                    _tempnickname = dr["PageName"].ToString();
                    _linkurlstring ="../"+ ((dr["LinkPath"].ToString().StartsWith("CRM") ||dr["LinkPath"].ToString().StartsWith("JiShi")) ? dr["LinkPath"].ToString() : "Admin/" + dr["LinkPath"]);
                    sb.Append("<dd data-name=\"list\"><a lay-href=\"" + _linkurlstring + "\">" + _tempnickname + "</a></dd>");
                }
            }
            sb.Append("</dl>");
        }
        list.Dispose();
        return sb.ToString().Replace(",]", "]");
    }
}