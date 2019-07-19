<%@ WebHandler Language="C#" Class="AgentAcceptHandleV2" %>

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Newtonsoft.Json.Linq;

public class AgentAcceptHandleV2 : IHttpHandler
{
    private readonly string _totablename = "Jxs_AgentAcceptInfo_2019";
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
                string locals = string.Empty;
                if (obj["locals"] != null)
                {
                        locals = obj["locals"].ToString();
                }
                string sqlstring = "insert into " + _totablename + "(ParentID,AcceptAgentID,BoxLabel,ProID,AcceptUserID,KhDDH,CompID,AgentTypeID,UploadAddress) values(" + obj["parentid"] + "," +
                               obj["agentid"] + ",'" + obj["lb"] + "'," + obj["prodid"] + "," + obj["userid"] + ",'" + obj["khddh"] + "'," + obj["parentid"] + ",2,'"+locals +"')";
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