<%@ WebHandler Language="C#" Class="yjfk" %>

using System.Web;
using TJ.DBUtility;

public class yjfk : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.Form["advtitle"]) &&
            !string.IsNullOrEmpty(context.Request.Form["advcontent"]) &&
            !string.IsNullOrEmpty(context.Request.Form["phone"]) &&
            !string.IsNullOrEmpty(context.Request.Form["uname"]) && !string.IsNullOrEmpty(context.Request.Form["compid"]))
        {
            string advtitle = context.Request.Form["advtitle"];
            string advcontent = context.Request.Form["advcontent"];
            string customerphone = context.Request.Form["phone"];
            string customername = context.Request.Form["cuname"];
            string compid = context.Request.Form["compid"];
            string uid = string.IsNullOrEmpty(context.Request.Form["uid"]) ? "0" : context.Request.Form["uid"];
            var tab = new TabExecute();
            tab.ExecuteNonQuery("insert into TJ_CustomerAdvice(advtitle,advcontent,customerphone,customername,userid,compid) values('" + advtitle + "','" + advcontent + "','" + customerphone + "','" + customername + "'," + uid + "," + compid + ")", null);
            context.Response.Write(1);
        }
        else
        {
            context.Response.Write(1);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}