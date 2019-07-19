<%@ WebHandler Language="C#" Class="AgentFaHuoHandle" %>

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web; 
using TJ.DBUtility;

public class AgentFaHuoHandle : IHttpHandler
{
    private readonly string _totablename = "AgentFHInfo_Temp_2019";
    private readonly string _totablenameaccept = "AgentAcceptInfo_2019";
    TabExecutewuliu tabwl = new TabExecutewuliu();
    InternetHandle internet = new InternetHandle();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        //string strAllInfo = context.Request.QueryString["strAllInfo"];
        string parentid = context.Request.Form["parentid"];
        string fragent = context.Request.Form["fragent"];
        string toagent = context.Request.Form["toagent"];
        string lb = context.Request.Form["lb"];
        string prodid = context.Request.Form["prodid"];
        string fhuid = context.Request.Form["fhuid"];
        string guid = context.Request.Form["guid"];
        string storeid = context.Request.Form["storeid"];
        string fhpc = context.Request.Form["fhpc"];
        string qr = context.Request.Form["qr"];
        string compid = context.Request.Form["masterid"];
        if (!string.IsNullOrEmpty(fragent) && !string.IsNullOrEmpty(toagent) && !string.IsNullOrEmpty(lb) &&
            !string.IsNullOrEmpty(prodid))
        {
            try
            {
                string sqlstring = "INSERT INTO " + _totablename + "([ParentID],[FromAgentID],[ToAgentID],[BoxLabel],[ProID], [FHUserID],[GuidString],[StoreID],[FHPici]) VALUES(" + parentid + "," + fragent + "," + toagent + ",'" + lb + "'," + prodid + "," + fhuid + ",'" + guid + "'," + storeid + ",'" + fhpc + "')";
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
                        if (qr == "1")
                        {
                            internet.GetUrlData("http://117.34.70.23:8888/delivery/confirm/?code=" + lb + "&agent_id=" + fragent + "&user_id=" + fhuid + "&pid=" + prodid);
                        }
                        context.Response.Write("{\"rs\":1,\"msg\":\"操作成功\"}");
                        sqlstring = "insert into " + _totablenameaccept + "(ParentID,AcceptAgentID,BoxLabel,ProID,AcceptUserID,KhDDH,CompID) values(" + parentid + "," + fragent + ",'" + lb + "'," + prodid + "," + fhuid + ",''," + compid + ")";
                        tabwl.ExecuteQuery(sqlstring, null);
                    }
                    catch
                    {
                        context.Response.Write("{\"rs\":0,\"msg\":\"系统异常\"}");
                    }
                }
            }
            catch
            {
                context.Response.Write("{\"rs\":0,\"msg\":\"系统异常\"}");
            }
        }
        else
        {
            context.Response.Write("{\"rs\":0,\"msg\":\"参数不完整\"}");
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