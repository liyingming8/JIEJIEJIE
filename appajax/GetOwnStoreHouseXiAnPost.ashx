<%@ WebHandler Language="C#" Class="GetOwnStoreHouseXiAnPost" %> 
using System.Configuration;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

public class GetOwnStoreHouseXiAnPost : IHttpHandler
{
    //readonly BTB_Products_Infor _btbProducts = new BTB_Products_Infor();
    //private IList<MTB_Products_Infor> _prolist;
    readonly StringBuilder _sb = new StringBuilder();

    SqlConnection myConn = null;
    DataSet ds;
    SqlDataAdapter ad;
    public DataSet Mydataset;
    public SqlDataAdapter ada;

    public SqlConnection GetConnectionWL()
    {
        string str = ConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString();
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
            string str = "select [STID] as sid,[StoreHouseName] as snm,[StoreHouseCode] as scd from [TB_StoreHouse] where compid='" + strcompid + "' order by stid";
            using (var myConn = GetConnectionWL())
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                ds = new DataSet();
                ad = new SqlDataAdapter(str, myConn);
                ad.Fill(ds, "StoreHouse");
                if (ds.Tables["StoreHouse"].Rows.Count > 0)
                {
                    _sb.Clear();
                    _sb.Append(JsonConvert.SerializeObject(ds.Tables["StoreHouse"]));
                    context.Response.Write(_sb.ToString());
                    ad.Dispose();
                    ds.Dispose();
                }
                else
                {
                    context.Response.Write("没有匹配的记录");
                }
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