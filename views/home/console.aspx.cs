using System;
using System.Collections.Generic;
using System.Text;
using commonlib;
using TJ.BLL;
using TJ.Model;

public partial class views_home_console : AuthorPage
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
            if(RID=="155")
            {
                div_daiban.Visible = false;
                div_swmconfig.Visible = true;
                var btjCompRoles = new BTJ_Comp_Roles();
                IList<MTJ_Comp_Roles> comroleist = btjCompRoles.GetListsByFilterString("CompID=" + GetCookieCompID());
                string authormenuidstring = ReturnSwmCompanyAuthorMunuString(comroleist);
                literal_kuaijiefangshi.Text = returnkuaijielink(authormenuidstring);
            }
            else
            {
                div_daiban.Visible = true;
                div_swmconfig.Visible = false;
                literal_kuaijiefangshi.Text =
                    "<li class=\"layui-col-xs3\"><a lay-href=\"../analysis/ShengChanLiangQuery.aspx\"><i class=\"layui-icon layui-icon-flag\"></i><cite>生产量</cite></a></li><li class=\"layui-col-xs3\"><a lay-href=\"../analysis/FaHuoQuery.aspx\"><i class=\"layui-icon layui-icon-chart\"></i><cite>发货量</cite></a></li><li class=\"layui-col-xs3\"><a lay-href=\"../Admin/TB_WXSMreport.aspx\"><i class=\"layui-icon layui-icon-chart-screen\" style=\"color:#009688;\"></i><cite>扫码量</cite></a></li><li class=\"layui-col-xs3\"><a lay-href=\"../analysis/FansAddedReport.aspx\"><i class=\"layui-icon layui-icon-face-smile\"></i><cite>粉丝</cite></a></li><li class=\"layui-col-xs3\"><a lay-href=\"../analysis/ShiShiJianKong.aspx\"><i class=\"layui-icon layui-icon-log\"></i><cite>实时监控</cite></a></li><li class=\"layui-col-xs3\"><a lay-href=\"../analysis/SaoMaReDian.aspx\"><i class=\"layui-icon layui-icon-fire\"></i><cite>扫码热点</cite></a></li><li class=\"layui-col-xs3\"><a lay-href=\"../analysis/ChuanHuoYuJing.aspx\"><i class=\"layui-icon layui-icon-circle-dot shink-red\"></i><cite>窜货预警</cite></a><li class=\"layui-col-xs3\"><a lay-href=\"../analysis/JiaBiaoQianYuJing.aspx\"><i class=\"layui-icon layui-icon-circle-dot shink-red\"></i><cite>假货预警</cite></a></li>";
            }
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