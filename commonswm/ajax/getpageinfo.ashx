<%@ WebHandler Language="C#" Class="getpageinfo" %> 
using System.Web; 

public class getpageinfo : IHttpHandler { 
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.QueryString["pid"]))
        {
            DBClass db = new DBClass();
            context.Response.Write(db.GetPageInfoByPid(context.Request.QueryString["pid"]));  
        }
        else
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