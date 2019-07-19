<%@ WebHandler Language="C#" Class="checkphoneisexist" %>

using System;
using System.Web;
using TJ.DBUtility;
using System.Data;

public class checkphoneisexist : IHttpHandler {

    TabExecute tab = new TabExecute();
    public void ProcessRequest (HttpContext context) {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        context.Response.ContentType = "text/plain";
        if (!string.IsNullOrEmpty(context.Request.Form["phone"]))
        {
            DataTable dt = tab.ExecuteQuery("select top 1 UserID from TJ_User where RID is not null and RID>0 and LoginName='" + context.Request.Form["phone"] + "'", null);
            if (dt.Rows.Count > 0)
            {
                context.Response.Write("{\"res\":0,\"msg\":\"已经注册\"}");
            }
            else
            {
               context.Response.Write("{\"res\":1,\"msg\":\"尚未注册\"}");
            }
        }
        else
        {
            context.Response.Write("{\"res\":0,\"msg\":\"请输入手机号码\"}");
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}