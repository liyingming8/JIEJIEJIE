﻿<%@ WebHandler Language="C#" Class="AgentAcceptOperationHandle" %> 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

public class AgentAcceptOperationHandle : IHttpHandler
{
    private readonly string _totablename = "AgentFHInfo_Temp_" + DateTime.Now.Year;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.QueryString["delcode"]))
        {
            string delcode = context.Request.QueryString["delcode"];
            string guid = context.Request.QueryString["guid"];
            try
            {
                string sqlstring = "delete from " + _totablename + " where GuidString='" + guid + "' and  BoxLabel='" + delcode + "'"; 
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
                        context.Response.Write("1");
                    }
                    catch
                    {
                        context.Response.Write("0");
                    }

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
        string str = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString();
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