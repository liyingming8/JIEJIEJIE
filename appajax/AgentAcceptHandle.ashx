<%@ WebHandler Language="C#" Class="AgentAcceptHandle" %>

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Newtonsoft.Json.Linq;

public class AgentAcceptHandle : IHttpHandler
{
    private readonly string _totablename = "AgentAcceptInfo_2019";
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.QueryString["strAllInfo"]))
        {
            string strAllInfo = context.Request.QueryString["strAllInfo"];
            JObject obj = JObject.Parse(strAllInfo);
            try
            {
                string sqlstring = "insert into " + _totablename +
                               "(ParentID,AcceptAgentID,BoxLabel,ProID,AcceptUserID) values(" + obj["parentid"] + "," +
                               obj["agentid"] + ",'" + obj["lb"] + "'," + obj["prodid"] + "," + obj["userid"] + ")";
                using (var myConn = GetConnectionWl())
                {
                    if (myConn.State == ConnectionState.Closed)
                    {
                        myConn.Open();
                    }
                    try
                    {
                        var sqlcmd = new SqlCommand(sqlstring, myConn);
                        sqlcmd.ExecuteNonQuery(); 
                        sqlcmd.Dispose();
                        context.Response.Write("1");
                    }
                    catch
                    {
                        context.Response.Write("0");
                    }
                    myConn.Close();
                }
            }
            catch
            {
                context.Response.Write("0");
            }
        }
    }

    private SqlConnection GetConnectionWl()
    {
        string str = ConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString();
        return new SqlConnection(str);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}