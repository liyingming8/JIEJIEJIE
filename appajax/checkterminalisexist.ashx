<%@ WebHandler Language="C#" Class="checkterminalisexist" %>

using System;
using System.Web;
using TJ.DBUtility;

public class checkterminalisexist : IHttpHandler {
    TabExecute tab=new TabExecute();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (!string.IsNullOrEmpty(context.Request.Form["tnm"]) && !string.IsNullOrEmpty(context.Request.Form["cityid"]))
        {
            string temp = tab.ExecuteQueryForSingleValue("select count(CompID) from TJ_RegisterCompanys where CTID=" +
                                                         context.Request.Form["cityid"] + " and CompName='" +
                                                         context.Request.Form["tnm"] + "'");
            context.Response.Write(int.Parse(temp) > 0 ? "{\"res\":1}" : "{\"res\":0}");
        }
        else
        {
            context.Response.Write("{\"res\":-1}");
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}