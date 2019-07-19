<%@ WebHandler Language="C#" Class="GetOwnAgentInfoXiAn" %>

using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
public class GetOwnAgentInfoXiAn : IHttpHandler 
{ 
    StringBuilder _sb = new StringBuilder(); 
    SqlConnection myConn = null;
    SqlCommand myCmd;
    DataTable dt;
    SqlDataAdapter ad;  

    public SqlConnection GetConnectionWLMarket()
    {
        string str = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString();
        myConn = new SqlConnection(str);
        return myConn;
    }
    
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*"); 
        if (!string.IsNullOrEmpty(context.Request.QueryString["coid"]))
        { 
            string strcompid = context.Request.QueryString["coid"].Trim(); 
            string str = "select a.CompID,a.CompName,a.Agent_Code from TJMarketingSystemYin.dbo.TJ_RegisterCompanys a where a.CompID in(select b.AgentID from TianJianWuLiuWebnew.dbo.TB_CompAgentInfo b where b.CompID="+strcompid+")"; 
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
                    _sb.Append("[");
                    foreach (DataRow row in dt.Rows)
                    {
                        _sb.Append(",{\"aid\":" + row["CompID"] + ",\"acd\":\"" + row["Agent_Code"] + "\",\"anm\":\"" + row["CompName"] + "\"}");
                    }
                    _sb.Append("]");
                    context.Response.Write(_sb.ToString().Replace("[,{","[{"));
                }
                else
                {
                    context.Response.Write("");
                }
                dt.Dispose();
                
            }
        }
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }
}