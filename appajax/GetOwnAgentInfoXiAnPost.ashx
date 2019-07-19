<%@ WebHandler Language="C#" Class="GetOwnAgentInfoXiAnPost" %>

using System.Configuration;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

public class GetOwnAgentInfoXiAnPost : IHttpHandler 
{ 
    StringBuilder _sb = new StringBuilder(); 
    SqlConnection myConn = null;
    DataTable dt;
    SqlDataAdapter ad;  

    public SqlConnection GetConnectionWLMarket()
    {
        string str = ConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString();
        myConn = new SqlConnection(str);
        return myConn;
    }
    
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");  
        string strcompid = context.Request.Form["coid"].Trim();
        if (!string.IsNullOrEmpty(strcompid))
        {
            string str = "select a.CompID as aid,a.CompName as anm,a.Agent_Code as acd from TJMarketingSystemYin.dbo.TJ_RegisterCompanys a where a.CompID in(select b.AgentID from TianJianWuLiuWebnew.dbo.TB_CompAgentInfo b where b.CompID=" + strcompid + ")";
            using (var myConn = GetConnectionWLMarket())
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                dt = new DataTable();
                ad = new SqlDataAdapter(str, myConn);
                ad.Fill(dt); 
                if (dt.Rows.Count > 0)
                {
                    _sb.Append(JsonConvert.SerializeObject(dt));
                    context.Response.Write(_sb.ToString().Trim());
                }
                else
                {
                    context.Response.Write("");
                }
                dt.Dispose();
                ad.Dispose();
            }
        } 
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }
}