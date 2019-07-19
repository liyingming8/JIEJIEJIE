<%@ WebHandler Language="C#" Class="getshownumforencrptv2" %>

using System;
using System.Web;
using commonlib;

public class getshownumforencrptv2 : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (!string.IsNullOrEmpty(context.Request.QueryString["ecd"]))
        {
            Security sc = new Security();
            context.Response.Write(sc.GetShowNumFromEncryptV2(context.Request.QueryString["ecd"]));
        }
        else
        {
            context.Response.Write("no");
        }
        context.Response.End();
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}