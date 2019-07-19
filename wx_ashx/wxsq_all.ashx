<%@ WebHandler Language="C#" Class="wxsq_all" %>

using System.Web;
using WxBaseAPITJ_V2;
using LitJson;
using System.Collections;
using System;
public class wxsq_all : IHttpHandler
{
    private string compid = "";
    private string sid = "";
    private string[] url;
    private string reurl = "";
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string code = "";
        if (context.Request.QueryString["code"] != null && context.Request.QueryString["code"] != "")
        {
            code = context.Request.QueryString["code"];
            url = context.Request.QueryString["back"].Split(',');
            reurl = url[0];
            compid = url[1];
            sid = url[2];
            WXlogin_api wxl = new WXlogin_api(compid, sid, "zonghe");
            if (context.Request.Cookies["TJWXuserid_" + compid + "_" + sid] != null && context.Request.Cookies["WXnumber_" + compid + "_" + sid] != null)
            {
                context.Response.Redirect("../" + reurl);
            }
            JsonData userinfo = wxl.GetWxtjUserinfo_OS(compid, code, sid, "TJ_User");
            if (((IDictionary)userinfo).Contains("openid"))
            {
                context.Response.Cookies["WXnumber_" + compid + "_" + sid].Value = userinfo["openid"].ToString();
                context.Response.Cookies["WXnumber_" + compid + "_" + sid].Expires = DateTime.Now.AddDays(4);
                context.Response.Redirect("../" + reurl, true);
            }

            else
            {
                WXData.Penglog("微信授权出错：无openid", "wxsqlog");
                context.Response.Redirect("../../error.html", true);
            }
        }
        else //未获得openid，
        {
            WXData.Penglog("微信授权出错：微信未返回code", "wxsqlog");
            context.Response.Redirect("../../error.html", true);
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}