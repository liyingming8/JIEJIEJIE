<%@ WebHandler Language="C#" Class="GetTerminalSaoMaSummary" %>

using System.Data;
using System.Web; 
using Newtonsoft.Json;
using TJ.DBUtility;

public class GetTerminalSaoMaSummary : IHttpHandler {
    
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
            DataTable dttemp =
                tab.ExecuteQuery(
                    "SELECT Count(ID) cnt,ProID,CONVERT(varchar(100), AcceptDay, 23) as AcceptDay FROM [TianJianWuLiuWebnew].[dbo].[AgentAcceptInfo_2019] where AcceptAgentID=" +
                    context.Request.Form["tid"] + " Group by ProID,AcceptDay order by AcceptDay desc", null);
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