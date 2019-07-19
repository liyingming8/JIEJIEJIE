<%@ WebHandler Language="C#" Class="checkneedpzyz" %> 
using System;
using System.Data; 
using System.Web;
using TJ.DBUtility;

public class checkneedpzyz : IHttpHandler
{
    private TabExecute tab;
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.QueryString["compid"]))
        {
            string compid = context.Request.QueryString["compid"];
            context.Response.Write(Getconfiginfo(compid));
        }
        else
        {
            context.Response.Write(1);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    } 

    private object Getconfiginfo(string compid)
    {
        object tempreturn = "";;
        tab = new TabExecute();
        DataTable dttemp = tab.ExecuteQuery(
            "select pzyz from TJ_CompFrontPage_Config where compid=" +
            compid, null);
        if (dttemp.Rows.Count > 0)
        {
            tempreturn = Convert.ToBoolean(dttemp.Rows[0][0])?1:0;
        }
        else
        {
            tempreturn =0;
        }
        dttemp.Dispose();
        return tempreturn;
    }

}