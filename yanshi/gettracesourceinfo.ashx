<%@ WebHandler Language="C#" Class="gettracesourceinfo" %>

using System.Web;

public class gettracesourceinfo : IHttpHandler {
    // DBClass_PYE dbobj = new DBClass_PYE();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        //string cpid = "";
        //string compid = "";
        //if (!string.IsNullOrEmpty(context.Request.QueryString["cpid"])&&!string.IsNullOrEmpty(context.Request.QueryString["compid"]))
        //{
        //    cpid = context.Request.QueryString["cpid"].Trim();
        //    compid = context.Request.QueryString["compid"].Trim();
        //    string temp = dbobj.GetTraceInfo(cpid);
        //    context.Response.Write(temp);
        //}
        //else
        //{
        //    context.Response.Write("f");
        //} 
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}