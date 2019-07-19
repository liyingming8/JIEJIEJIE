<%@ WebHandler Language="C#" Class="swmlogin" %>

using System.Web;
using commonlib;
using Newtonsoft.Json.Linq;

public class swmlogin : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (!string.IsNullOrEmpty(context.Request.QueryString["nr"]))
        {
            Security sc = new Security();
            JObject obj = JObject.Parse(context.Request.QueryString["nr"]);
            var userName = new HttpCookie("TJOSUName");
            userName.Value = sc.EncryptQueryString(obj["nm"].ToString().Trim());
            userName.Expires.AddDays(1);
            context.Response.Cookies.Add(userName);

            var userID = new HttpCookie("TJOSUID");
            userID.Value = sc.EncryptQueryString(obj["uid"].ToString().Trim());
            userID.Expires.AddDays(1);
            context.Response.Cookies.Add(userID);

            var tjUserRid = new HttpCookie("TJOSRID");
            tjUserRid.Value = sc.EncryptQueryString(obj["rid"].ToString().Trim());
            tjUserRid.Expires.AddDays(1);
            context.Response.Cookies.Add(tjUserRid);

            var userUnid = new HttpCookie("TJOSCOMPID");
            userUnid.Value = sc.EncryptQueryString(obj["compid"].ToString().Trim());
            userUnid.Expires.AddDays(1);
            context.Response.Cookies.Add(userUnid);
            context.Response.Redirect("views/indexswm.aspx");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}