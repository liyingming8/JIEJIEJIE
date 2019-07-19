<%@ WebHandler Language="C#" Class="getpageinfo_v2" %> 
using System.Web;
using commonlib;

public class getpageinfo_v2 : IHttpHandler
{  
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        try
        {
            if (!string.IsNullOrEmpty(context.Request.QueryString["pid"])&&!string.IsNullOrEmpty(context.Request.QueryString["comp"]))
            {
                var db = new DBClass();
                var sc = new Security();
                string compid = context.Request.QueryString["comp"];
                string pid = sc.DecryptQueryString(context.Request.QueryString["pid"]);
                if (db.GetModuleIsCustomedByCompID(context.Request.QueryString["comp"]))
                {
                    context.Response.Write(db.GetCustomerPageInfoByPid(pid, compid));
                }
                else
                {
                    context.Response.Write(db.GetPageInfoByPid(pid));
                }
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