using System;
using System.Collections.Generic;
using System.Globalization;
using TJ.BLL; 
using commonlib;
using System.Text;
using TJ.Model;

public partial class main : AuthorPage
{
    private readonly BTJ_SiteMap _bl = new BTJ_SiteMap();
    private readonly BTJ_RoleInfo _blrole = new BTJ_RoleInfo(); 
    private readonly BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();
    readonly BTJ_User _btjUser =  new BTJ_User();
    public string CompLogo = "";
    public string UserId = "";
    public string DefaultUrl = "Admin/Welcome.aspx";
    public string MenuString = "";
    public string UserName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserId = GetCookieUID();
            MTJ_RegisterCompanys mcopany = bcompany.GetList(int.Parse(GetCookieCompID()));
            if (!string.IsNullOrEmpty(mcopany.WelcomePage))
            {
                DefaultUrl = mcopany.WelcomePage;
            }
            Literal_title.Text = mcopany.CompName.Trim();
            LiteralCompanyName.Text = mcopany.CompName.Trim();
            if (mcopany.CompLogo.Trim().Length > 0)
            {
                CompLogo = "Admin/"+mcopany.CompLogo;
            }
            else
            {
                CompLogo = "Admin/" + bcompany.GetList(mcopany.ParentID).CompLogo;
            }
            MenuString = GetMenuString(); 
            Literal_BottomInfo.Text = ReturnBottomInfo();
            UserName = _btjUser.GetList(int.Parse(GetCookieUID())).LoginName;
        }
    }

    //private string _tempstring = "";
    private string ReturnBottomInfo()
    { 
        return "技术支持：海南天鉴防伪科技有限公司";
    }
    private string GetMenuString()
    {
        string authorMenuString = _blrole.GetList(int.Parse(GetCookieRID())).AuthorMenuInfo;
        if (authorMenuString.Length > 0)
        {
            //authorMenuString = Sc.DecryptQueryString(authorMenuString);
            var sb = new StringBuilder();
            if (authorMenuString.Length > 0)
            {
                IList<MTJ_SiteMap> slist = _bl.GetListsByFilterString("ParentID=0 and SiteID in (" + (authorMenuString.StartsWith(",") ? authorMenuString.Substring(1) : authorMenuString) + ")", "ShowOrder");//是父级目录
                sb.Append("\"menus\":[");
                //int i = 0;在权限管理中，在这里要先判断该用户所具有的权限目录，只有已授权的菜单目录才可以显示
                foreach (MTJ_SiteMap dr in slist)
                {
                    sb.Append("{\"menuid\":\"" + dr.SiteID + "\",\"icon\":\"icon-sys\",\"menuname\":\"" + dr.PageName + "\"");
                    sb.Append(SubSiteLinkString(dr.SiteID.ToString(CultureInfo.InvariantCulture), authorMenuString));
                    sb.Append("},");
                }
                sb.Append("]");
            }
            return sb.ToString().Replace(",]", "]");
        }
        return "";
    } 
    string _tempnickname = "";
    private string _linkurlstring = "";
    private string SubSiteLinkString(string pSiteID, string authorMenuString)
    {
        var sb = new StringBuilder();
        IList<MTJ_SiteMap> list = _bl.GetListsByFilterString("ParentID=" + pSiteID, "ShowOrder");
        if (list.Count > 0)
        {
            sb.Append(",\"menus\":[");
            foreach (MTJ_SiteMap msite in list)
            {
                if (authorMenuString.Contains("," + msite.SiteID + ","))
                {
                    _tempnickname = msite.PageName;
                    _linkurlstring = (msite.LinkPath.StartsWith("CRM") || msite.LinkPath.StartsWith("JiShi")) ? msite.LinkPath : "Admin/" + msite.LinkPath;
                    sb.Append("{\"menuname\":\"" + (_tempnickname.Equals("") ? msite.PageName : _tempnickname) + "\",\"icon\":\"icon-nav\",\"url\":\"" +_linkurlstring + "\"},");
                }
            }
            sb.Append("]");
        }
        return sb.ToString().Replace(",]", "]"); 
    } 
}