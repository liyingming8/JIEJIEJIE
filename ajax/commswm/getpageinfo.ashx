<%@ WebHandler Language="C#" Class="getpageinfo" %> 
using System.Web;
using commonlib;

public class getpageinfo : IHttpHandler {  
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        try
        {
            if (!string.IsNullOrEmpty(context.Request.QueryString["pid"]))
            {
                var db = new DBClass();
                var sc = new Security();
                context.Response.Write(db.GetPageInfoByPid(sc.DecryptQueryString(context.Request.QueryString["pid"])));
            }
            else
            {
                context.Response.Write("[]");
            }
        }
        catch
        {
            context.Response.Write("[]");
        } 
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}