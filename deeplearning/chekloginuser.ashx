<%@ WebHandler Language="C#" Class="chekloginuser" %>

using System;
using System.Web;
using commonlib;
using TJ.DBUtility;

public class chekloginuser : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        if (!string.IsNullOrEmpty(context.Request.QueryString["unm"]) &&!string.IsNullOrEmpty(context.Request.QueryString["upw"]))
        {
            try
            {
                TabExecute tab = new TabExecute();
                string temp = tab.ExecuteQueryForSingleValue(
                    "select id from tj_swm_feedback_userinfo where loginname='" +
                    context.Request.QueryString["unm"] + "' and upsw='" +
                    CommonFun.Md5hash_String(context.Request.QueryString["upw"]) + "'");
                if (!string.IsNullOrEmpty(temp))
                {
                    string lsttm = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    tab.ExecuteNonQuery("update tj_swm_feedback_userinfo set lastlogintm='" + lsttm + "' where id=" +
                                        temp);
                    context.Response.Write("{\"msg\":1,\"tm\":\"" + lsttm + "\"}");
                }
                else
                {
                    context.Response.Write("{\"msg\":0,\"tm\":\"\"}");
                }
            }
            catch
            {
                context.Response.Write("{\"msg\":0,\"tm\":\"\"}");
            }
        }
        else
        {
            context.Response.Write("{\"msg\":0,\"tm\":\"\"}");
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}