<%@ WebHandler Language="C#" Class="GetTerminalJiFenDetail" %>

using System.Data;
using System.Web;
using Newtonsoft.Json;
using TJ.DBUtility;

public class GetTerminalJiFenDetail : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        TabExecute tab = new TabExecute();
        commfrank comfrank = new commfrank();
        if (string.IsNullOrEmpty(context.Request.Form["terminalid"]))
        {
            context.Response.Write("未找到积分记录");
        }
        else
        {
            string compid = "0";
            if (!string.IsNullOrEmpty(context.Request.Form["compid"]))
            {
                compid = context.Request.Form["compid"].ToString();
            }
            comfrank.FreshTerminalIntegral(compid.Equals("0") ? "2" : compid, int.Parse(context.Request.Form["terminalid"]));
            DataTable dttemp = tab.ExecuteQuery(
                "SELECT a.id,a.winreason rs,a.prizevl jf,convert(varchar(100), a.gettm, 23) tm FROM TJ_Activity_JXS_Win a where a.agentid=" + context.Request.Form["terminalid"] + " and a.islq=0 and a.awtypeid=2 order by a.id desc",
                null);
            context.Response.Write(JsonConvert.SerializeObject(dttemp));
            dttemp.Dispose();
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}