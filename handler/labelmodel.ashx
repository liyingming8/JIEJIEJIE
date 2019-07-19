<%@ WebHandler Language="C#" Class="labelmodel" %>

using System.Web;

public class labelmodel : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.Write("[{\"discriptions\":\"1托6托12\",\"valuestring\":\"1:6:12\"},{\"discriptions\":\"1托2托48\",\"valuestring\":\"1:2:48\"},{\"discriptions\":\"1托4托6\",\"valuestring\":\"1:4:6\"}]");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}