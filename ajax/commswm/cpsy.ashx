<%@ WebHandler Language="C#" Class="cpsy" %>

using System.Web;

public class cpsy : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*"); 
        if (!string.IsNullOrEmpty(context.Request.Form["cpid"]))
        { 
            string cpid = context.Request.Form["cpid"];
            string url = "http://www.china315net.com:35224/zhpt/qr3d/label/info/?code="+ cpid;
            InternetHandle internet = new InternetHandle();
            context.Response.Write(internet.GetUrlData(url));  
        }
        else
        {
            context.Response.Write(0);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}