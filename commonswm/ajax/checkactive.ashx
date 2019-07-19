<%@ WebHandler Language="C#" Class="checkactive" %>

using System;
using System.Web;

public class checkactive : IHttpHandler {
    DBClass db = new DBClass();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        string openid = context.Request.Form["openid"];
        string result = "";
        string compidanduserid = db.GetCompidBySwmOpenid(openid);
        string[] temparray = compidanduserid.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

        if (temparray[2].Equals("0"))
        {
            result = "0";
        }
        else {
            result = "1";
        }
        context.Response.Write(result);

        context.Response.End();
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}