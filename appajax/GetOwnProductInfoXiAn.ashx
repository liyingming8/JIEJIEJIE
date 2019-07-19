<%@ WebHandler Language="C#" Class="GetOwnProductInfoXiAn" %> 
using System.Configuration;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class GetOwnProductInfoXiAn : IHttpHandler 
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
        string str = ConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString();
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
            string str = "select [Infor_ID],[Products_Name] from [TB_Products_Infor] where compid='" + strcompid + "' order by Infor_ID";
            using (var myConn = GetConnectionWL())
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                ds = new DataSet();
                ad = new SqlDataAdapter(str, myConn);
                ad.Fill(ds, "Product");
                if (ds.Tables["Product"].Rows.Count > 0)
                {
                    _sb.Clear();
                    _sb.AppendLine("[");

                    for (int i = 0; i <= ds.Tables["Product"].Rows.Count - 1; i++)
                    {
                        string strid = ds.Tables["Product"].Rows[i][0].ToString().Trim();
                        string strname = ds.Tables["Product"].Rows[i][1].ToString().Trim();
                        if (i < ds.Tables["Product"].Rows.Count - 1)
                        {
                            _sb.AppendLine("{\"pid\":" + strid + ",\"pcd\":\"" + strid + "\",\"pnm\":\"" + strname + "\"},");
                        }
                        else
                        {
                            _sb.AppendLine("{\"pid\":" + strid + ",\"pcd\":\"" + strid + "\",\"pnm\":\"" + strname + "\"}");
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