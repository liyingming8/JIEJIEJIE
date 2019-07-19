<%@ WebHandler Language="C#" Class="ModifyLoginPasswordRandomAndSendSMS" %>

using System;
using System.Web;
using commonlib;
using TJ.DBUtility;

public class ModifyLoginPasswordRandomAndSendSMS : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.Form["phonenum"]))
        {
            string phonenum = context.Request.Form["phonenum"].ToString();
            TabExecute tab = new TabExecute();
            string sqlstring = "select UserID from TJ_User where LoginName='" + phonenum + "' and RID=160";
            string uid = tab.ExecuteQueryForSingleValue(sqlstring);
            if (!string.IsNullOrEmpty(uid))
            {
                Random rd = new Random();
                string newpsw = rd.Next(100000, 999999).ToString();
                int o = tab.ExecuteNonQuery("update TJ_User set PassWords='" + CommonFun.Md5hash_String(newpsw) + "' where UserID=" +uid, null);
                if (o > 0)
                {
                    HuYi_Info.HY_dxinfoAutoSign("尊敬的用户,您的密码重置为:" + newpsw, phonenum);
                    context.Response.Write("{\"rs\":1,\"e\":\"修改成功\"}");
                }
                else
                {
                    context.Response.Write("{\"rs\":0,\"e\":\"尚未修改成功\"}");
                }
                        
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