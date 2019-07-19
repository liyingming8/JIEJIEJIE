<%@ WebHandler Language="C#" Class="AgentGetOwnOrderCode" %>

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;

public class AgentGetOwnOrderCode : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.QueryString["compid"]) &&!string.IsNullOrEmpty(context.Request.QueryString["masterid"]))
        {
            context.Response.Write(GetOrderCodeByCompIDAndAgentID(context.Request.QueryString["compid"], context.Request.QueryString["masterid"]));
        }
        else
        {
            context.Response.Write("");
        }
    }

    private string GetOrderCodeByCompIDAndAgentID(string compid, string agentid)
    {
        string str = ConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString();
        SqlDataAdapter sda =
            new SqlDataAdapter(
                "select distinct KhDDH,FHDate from TB_FaHuoInfo_" + compid + " where AgentID=" + agentid + " and CompID=" +
                compid + " and (KhDDH<>'' and KhDDH is not null) order by FHDate desc", str);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            bool need = false;
            foreach (DataRow row in dt.Rows)
            {
                if (need)
                {
                    sb.Append(",{\"agentid\":" + agentid + ",\"code\":'" + row[0] + "'}");
                }
                else
                {
                    sb.Append("{\"agentid\":" + agentid + ",\"code\":'" + row[0] + "'}");
                    need = true;
                } 
            }
            sb.Append("]");
            dt.Dispose();
            sda.Dispose();
            return sb.ToString();
        }
        else
        {
            dt.Dispose();
            sda.Dispose();
            return "";
        }
    } 
    public bool IsReusable {
        get {
            return false;
        }
    }

}