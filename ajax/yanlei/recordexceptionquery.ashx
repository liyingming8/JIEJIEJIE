<%@ WebHandler Language="C#" Class="recordexceptionquery" %> 
using System.Web;
using TJ.DBUtility;

public class recordexceptionquery : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.QueryString["lbcode"]) &&
            !string.IsNullOrEmpty(context.Request.QueryString["extype"]) &&
            !string.IsNullOrEmpty(context.Request.QueryString["restype"])&&
            !string.IsNullOrEmpty(context.Request.QueryString["address"]))
        {
            try
            {
                string code = context.Request.QueryString["lbcode"];
                string extype = context.Request.QueryString["extype"];
                string restype = context.Request.QueryString["restype"];
                string userid = string.IsNullOrEmpty(context.Request.QueryString["userid"]) ? "0" : context.Request.QueryString["userid"];
                string queryaddress = context.Request.QueryString["address"];
                var tab = new TabExecute();
                tab.ExecuteNonQuery("INSERT INTO TJ_ExceptionQueryInfo(lbcode,extype,restype,userid,platform,queryaddress) VALUES('" + code + "','" + extype + "'," + restype + "," + userid + ",'yanlei','"+queryaddress+"')", null);
                context.Response.Write(1);
            }
            catch  
            {
                context.Response.Write(0);
            } 
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