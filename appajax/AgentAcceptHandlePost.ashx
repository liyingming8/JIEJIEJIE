<%@ WebHandler Language="C#" Class="AgentAcceptHandle" %> 
using System;
using System.Web;
using TJ.DBUtility;

public class AgentAcceptHandle : IHttpHandler
{ 
    InternetHandle internet = new InternetHandle();
    private readonly string _totablename = "AgentAcceptInfo_2019";
    TabExecutewuliu tabwl = new TabExecutewuliu();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (string.IsNullOrEmpty(context.Request.Form["masterid"]) ||
            string.IsNullOrEmpty(context.Request.Form["parid"]) || string.IsNullOrEmpty(context.Request.Form["lbcode"]) ||
            string.IsNullOrEmpty(context.Request.Form["agid"]) || string.IsNullOrEmpty(context.Request.Form["userid"]) ||
            string.IsNullOrEmpty(context.Request.Form["pid"]))
        {
            context.Response.Write("{\"cnt\": 0, \"e\": \"参数不完整\"}");
        }
        else
        {
            string compid = context.Request.Form["masterid"];
            string parentid = context.Request.Form["parid"];
            string labelcode = context.Request.Form["lbcode"];
            string agent_id = context.Request.Form["agid"];
            string user_id = context.Request.Form["userid"];
            string pid = context.Request.Form["pid"];
            try
            {
                string temp = internet.GetUrlData("http://117.34.70.23:8888/delivery/confirm/?code=" + labelcode + "&agent_id=" +
                                    agent_id + "&user_id=" + user_id + "&pid=" + pid);
                context.Response.Write(temp);
                string sqlstring = "insert into " + _totablename + "(ParentID,AcceptAgentID,BoxLabel,ProID,AcceptUserID,KhDDH,CompID,AgentTypeID) values(" + parentid + "," + agent_id + ",'" + labelcode + "'," + pid + "," + user_id + ",''," + compid + ",3)";
                tabwl.ExecuteQuery(sqlstring, null);
            }
            catch
            {
                context.Response.Write("{\"cnt\": 0, \"e\": \"系统异常\"}");
            }
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