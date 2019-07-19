<%@ WebHandler Language="C#" Class="GetTerminalSaoMaDetail" %>

using System.Data;
using System.Web; 
using Newtonsoft.Json;
using TJ.DBUtility;

public class GetTerminalSaoMaDetail : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        TabExecutewuliu tab = new TabExecutewuliu();
        if (string.IsNullOrEmpty(context.Request.Form["tid"]))
        {
            context.Response.Write("未找到扫码记录");
        }
        else
        {
            DataTable dttemp = tab.ExecuteQuery(
                "SELECT BoxLabel xm,CONVERT(varchar(50), AcceptDate, 120) as tm FROM AgentAcceptInfo_2019 where AcceptAgentID=" + context.Request.Form["tid"]+" order by ID desc",null); 
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