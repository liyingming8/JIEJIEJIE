<%@ WebHandler Language="C#" Class="GetOwnAgentInfoXiAn" %>

using System;
using System.Configuration;
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
        string str = ConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString();
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
            string str = "select a.* from TJMarketingSystemYin.dbo.TJ_App_LabelModel_Info a where a.CompID=" + strcompid; 
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
                    string[] temparray = new string[dt.Rows.Count];
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        temparray[i] = "{\"discriptions\":\"" + row["labelmodeldiscription"] + "\",\"valuestring\":\"" + row["labelmodelvalue"] + "\"}";
                        i++;
                    } 
                    context.Response.Write("["+String.Join(",",temparray)+"]");
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