<%@ WebHandler Language="C#" Class="GetOwnStoreHouseXiAn" %> 
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class GetOwnStoreHouseXiAn : IHttpHandler 
{
    //readonly BTB_Products_Infor _btbProducts = new BTB_Products_Infor();
    //private IList<MTB_Products_Infor> _prolist;
    readonly StringBuilder _sb = new StringBuilder();

    SqlConnection myConn = null;
    SqlCommand myCmd;
    DataSet ds;
    SqlDataAdapter ad;
    public DataSet Mydataset;
    public SqlDataAdapter ada;

    public SqlConnection GetConnectionWL()
    {
        string str = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString();
        myConn = new SqlConnection(str);
        return myConn;
    }
    
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.QueryString["coid"]))
        {
            string strcompid = context.Request.QueryString["coid"].ToString().Trim();
            string str = "select [STID],[StoreHouseName],[StoreHouseCode] from [TB_StoreHouse] where compid='" + strcompid + "' order by stid";
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
                    _sb.AppendLine("[");

                    for (int i = 0; i <= ds.Tables["StoreHouse"].Rows.Count - 1; i++)
                    {
                        string strid = ds.Tables["StoreHouse"].Rows[i][0].ToString().Trim();
                        string strcode = ds.Tables["StoreHouse"].Rows[i][2].ToString().Trim();
                        string strname = ds.Tables["StoreHouse"].Rows[i][1].ToString().Trim();
                        if (i < ds.Tables["StoreHouse"].Rows.Count - 1)
                        {
                            _sb.AppendLine("{\"sid\":" + strid + ",\"scd\":\"" + strcode + "\",\"snm\":\"" + strname + "\"},");
                        }
                        else
                        {
                            _sb.AppendLine("{\"sid\":" + strid + ",\"scd\":\"" + strcode + "\",\"snm\":\"" + strname + "\"}");
                        }
                    }
                    _sb.AppendLine("]");
                    context.Response.Write(_sb.ToString());
                    _sb.Clear();

                }
                else
                {
                    context.Response.Write("没有匹配的记录");
                }
            }
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}