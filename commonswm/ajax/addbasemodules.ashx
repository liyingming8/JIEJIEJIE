<%@ WebHandler Language="C#" Class="addbasemodules" %> 
using System.Web;

public class addbasemodules : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (string.IsNullOrEmpty(context.Request.QueryString["compid"]) &&
            string.IsNullOrEmpty(context.Request.QueryString["pid"]))
        {
            DBClass db = new DBClass(); 
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}