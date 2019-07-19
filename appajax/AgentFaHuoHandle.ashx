<%@ WebHandler Language="C#" Class="AgentFaHuoHandle" %>

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Newtonsoft.Json.Linq;

public class AgentFaHuoHandle : IHttpHandler
{
    //private readonly string _totablename = "AgentFHInfo_Temp_" + DateTime.Now.Year;
    private readonly string _totablename = "AgentFHInfo_Temp_2018";
    DBClass db = new DBClass();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.QueryString["strAllInfo"]))
        {
            string strAllInfo = context.Request.QueryString["strAllInfo"];
            strAllInfo = HttpUtility.UrlDecode(strAllInfo);
            JObject obj = JObject.Parse(strAllInfo);
            try
            {
                string locals = "";
                if (obj["locals"] != null)
                {
                    locals = obj["locals"].ToString();
                }
                string sqlstring = "INSERT INTO " + _totablename + "([ParentID],[FromAgentID],[ToAgentID],[BoxLabel],[ProID], [FHUserID],[GuidString],[StoreID],[FHPici],[scaddress]) VALUES(" + obj["parentid"] + "," + obj["fragent"] + "," + obj["toagent"] + ",'" + obj["lb"] + "'," + obj["prodid"] + "," + obj["fhuid"] + ",'" + obj["guid"] + "'," + obj["storeid"] + ",'" + obj["fhpc"] + "','" + locals + "')";
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