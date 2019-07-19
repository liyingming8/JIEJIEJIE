<%@ WebHandler Language="C#" Class="wxjssdk_all" %>

using System.Web;
using System.Collections;
using WxBaseAPITJ_V2;

public class wxjssdk_all : IHttpHandler
{


    public void ProcessRequest(HttpContext context)
    {
        string res = "fail";
        if (context.Request.Form["url"] != null)
        {
            string url = context.Request.Form["url"];
            string compid = "3"; //context.Request.Form["compid"];
            string sid ="0"; //context.Request.Form["sid"];
            WXjssdk_api wx = new WXjssdk_api(compid, sid);
            Hashtable tb = wx.getSignPackage("http://zynew.china315net.com/sxzy/" + url);
            string appId = tb["appId"].ToString();
            string timestamp = tb["timestamp"].ToString();
            string nonceStr = tb["nonceStr"].ToString();
            string signature = tb["signature"].ToString();
            res = "{\"appId\":\"" + appId + "\",\"timestamp\":\"" + timestamp + "\",\"nonceStr\":\"" + nonceStr + "\",\"signature\":\"" + signature + "\"}";
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(res);
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