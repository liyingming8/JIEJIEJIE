<%@ WebHandler Language="C#" Class="OrperateFaHuoInfoFrank" %>

using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
public class OrperateFaHuoInfoFrank : IHttpHandler 
{
    readonly StringBuilder _sb = new StringBuilder();

    SqlConnection myConn = null;
    SqlCommand myCmd;
    DataSet _ds;
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
        if (!string.IsNullOrEmpty(context.Request.QueryString["dellabel"]))
        {
            string labeltodel = context.Request.QueryString["dellabel"].Trim();
            context.Response.Write(DeleteTbProcessDataInfo(labeltodel));
        }
        if (!string.IsNullOrEmpty(context.Request.QueryString["confirmdeviceid"]))
        {
            context.Response.Write(UpdateForConfirmTbProcessDataInfo(context.Request.QueryString["confirmdeviceid"]));
        }
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

    private string _str1;
    private string DeleteTbProcessDataInfo(string labelcode)//string ProductID, string MadeDate)
    {
        try
        {
            using (var myConn = GetConnectionWL())
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                _str1 = "delete TB_ProcessDataInfo where XH='" + labelcode + "'";
                if (!_str1.Equals(""))
                {
                    SqlCommand cmd = new SqlCommand(_str1, myConn);
                    int intc = cmd.ExecuteNonQuery();
                    if (intc > 0)
                    {
                        cmd.Dispose();
                        myConn.Close(); 
                        return "1";
                    }
                    else
                    {
                        cmd.Dispose();
                        myConn.Close();
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }
        }
        catch
        {
            return "0";
        }
    }

    private string UpdateForConfirmTbProcessDataInfo(string deviceid)//string ProductID, string MadeDate)
    {
        try
        {
            using (var myConn = GetConnectionWL())
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                _str1 = "update TB_ProcessDataInfo set confirmed=1 where [Guid]='" + deviceid + "'";
                if (!_str1.Equals(""))
                {
                    SqlCommand cmd = new SqlCommand(_str1, myConn);
                    int intc = cmd.ExecuteNonQuery();
                    if (intc > 0)
                    {
                        cmd.Dispose();
                        myConn.Close();
                        return "1";
                    }
                    else
                    {
                        cmd.Dispose();
                        myConn.Close();
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }
        }
        catch
        {
            return "0";
        }
    }
}