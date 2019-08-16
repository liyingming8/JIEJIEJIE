using System.Configuration;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TJ.DBUtility;

/// <summary>
/// WebServiceScan 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class WebServiceScanGetInfo : WebService
{

    public WebServiceScanGetInfo() {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }


    readonly StringBuilder _sb = new StringBuilder();
    SqlConnection myConn;
    DataSet ds;
    SqlDataAdapter ad;
    public DataSet Mydataset;
    public SqlDataAdapter ada;
    string sqlstr = string.Empty;
    string code = string.Empty;
    string ljstr = string.Empty;
    DataTable ss = new DataTable();
    public SqlConnection GetConnectionWL(string flag)
    {
        string str = string.Empty;
        if (flag == "qd")
        {
            str = ConnectionInfo.SqlServerConnString;
        }
        else
        {
            str = ConnectionInfo.SqlServerConnStringWuLiu;
        }

        myConn = new SqlConnection(str);
        return myConn;
    }

    /// <summary>
    /// 将信息插入到整体发货表中的WEB 服务
    /// </summary>
    /// <param name="strcompid"></param>
    /// <param name="PWD"></param>
    /// <param name="USERID"></param>
    /// <returns></returns>
    [WebMethod]
    public DataTable getinfo(string str)
    
    {
        if (!string.IsNullOrEmpty(str))
        {
            code = str;
            switch (code)
            {
                case "dls": sqlstr = "SELECT  [CompID],[CompName] FROM [TJMarketingSystemYin].[dbo].[TJ_RegisterCompanys] where CompID in(SELECT [AgentID] FROM [TianJianWuLiuWebnew].[dbo].[TB_CompAgentInfo] where CompID=17068)"; ljstr = "qd"; break;
                case "cp": sqlstr = " SELECT [Infor_ID] ,[Products_Name]FROM [TianJianWuLiuWebnew].[dbo].[TB_Products_Infor] where CompID='17068'"; ljstr = "wl"; break;
                case "ck": sqlstr = "SELECT STID,StoreHouseName FROM [TianJianWuLiuWebnew].[dbo].TB_StoreHouse where CompID='17068';"; ljstr = "wl"; break;
                case "zj": sqlstr = "SELECT  [ZJID],[ZJname] FROM [TianJianWuLiuWebnew].[dbo].[TB_ZhiJianYuan] where Compid=17068"; ljstr = "wl"; break;
                case "bz": sqlstr = "SELECT [WLID],[WorkLineName] FROM [TianJianWuLiuWebnew].[dbo].[TB_WorkLineInfo] where CompID=17068"; ljstr = "wl"; break;

            }

            using (var myConn = GetConnectionWL(ljstr))
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                ds = new DataSet();
                DataSet ds1 = new DataSet();
                ad = new SqlDataAdapter(sqlstr, myConn);
                ad.Fill(ds, "Cust");
                ss = ds.Tables["Cust"];
                if (ss.Rows.Count > 0)
                {

                    return ss;
                }
                else
                {
                    return ss;
                }
            }
        }
        else
        {
            return ss;
        }
    }
  
   
   

}
