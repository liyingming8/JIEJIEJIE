<%@ WebHandler Language="C#" Class="GetTerminalSaoMaDetailDay" %>

using System.Data;
using System.Web; 
using Newtonsoft.Json;
using TJ.DBUtility;

public class GetTerminalSaoMaDetailDay : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        TabExecutewuliu tab = new TabExecutewuliu();
        if (string.IsNullOrEmpty(context.Request.Form["tid"])||string.IsNullOrEmpty(context.Request.Form["day"]))
        {
            context.Response.Write("未找到扫码记录");
        }
        else
        {
            DataTable dttemp = tab.ExecuteQuery(
                "SELECT BoxLabel xm,CONVERT(varchar(50), AcceptDate, 8) as tm FROM AgentAcceptInfo_2019 where AcceptDay='"+context.Request.Form["day"]+"' and  AcceptAgentID=" + context.Request.Form["tid"]+" order by ID desc",null); 
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