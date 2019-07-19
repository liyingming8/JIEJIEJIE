<%@ WebHandler Language="C#" Class="wxsq_geturl_html" %>

using System.Web;
using WxBaseAPITJ_V2;


public class wxsq_geturl_html : IHttpHandler
{
    WXlogin_api wx = new WXlogin_api("3", "0", "yanlei");
    public void ProcessRequest(HttpContext context)
    {
        string backurl = context.Request.Form["url"].ToString();
        string wxurl = wx.GetWxCodeRedirectString_Byid(backurl,"3","0");

        context.Response.ContentType = "text/plain";
        context.Response.Write(wxurl);
        context.Response.End();

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}