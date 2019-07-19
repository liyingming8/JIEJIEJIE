<%@ WebHandler Language="C#" Class="checkcompanyisexist" %>

using System;
using System.Data;
using System.Web;
using TJ.DBUtility;

public class checkcompanyisexist : IHttpHandler { 
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (!string.IsNullOrEmpty(context.Request.QueryString["compnm"]))
        {
            var tab = new TabExecute();
            DataTable dttemp = tab.ExecuteQuery(
                "select count(CompID) as cnt from TJ_RegisterCompanys where CompName='" +
                context.Request.QueryString["compnm"] + "'", null);
            if (dttemp.Rows.Count > 0)
            {
                if (Convert.ToInt32(dttemp.Rows[0][0]) > 0)
                {
                    context.Response.Write(1);
                }
                else
                {
                    context.Response.Write(0);
                }
            }
            else
            {
                context.Response.Write(0);
            }
            dttemp.Dispose();
        }
        else
        {
            context.Response.Write(-1);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}