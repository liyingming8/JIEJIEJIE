<%@ WebHandler Language="C#" Class="ModifyLoginPassword" %>

using System;
using System.Web;
using commonlib;
using TJ.DBUtility;

public class ModifyLoginPassword : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.Form["uid"]) && !string.IsNullOrEmpty(context.Request.Form["psw"]))
        {
            TabExecute tab = new TabExecute();
            int o = tab.ExecuteNonQuery("update TJ_User set PassWords='" + CommonFun.Md5hash_String(context.Request.Form["psw"]) + "' where UserID=" + context.Request.Form["uid"], null);
            if (o > 0)
            {
                context.Response.Write("{\"rs\":1,\"e\":\"修改成功\"}");
            }
            else
            {
                context.Response.Write("{\"rs\":0,\"e\":\"尚未修改成功\"}");
            }
        }
        else
        {
            context.Response.Write("{\"rs\":0,\"e\":\"参数不完整\"}");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}