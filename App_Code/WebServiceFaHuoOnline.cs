using System;
using System.Configuration;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// WebServiceFaHuoOnline 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class WebServiceFaHuoOnline : WebService {

    SqlConnection myConn;
    DataSet ds;
    SqlDataAdapter ad;
    public DataSet Mydataset;
    public SqlDataAdapter ada;

    /// <summary>
    /// TJMarketingSystemYin
    /// </summary>
    /// <returns></returns>
    public SqlConnection GetConnection()
    {
        string str = ConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString();
        myConn = new SqlConnection(str);
        return myConn;
    }
    /// <summary>
    /// TianJianWuLiuWebNew
    /// </summary>
    /// <returns></returns>
    public SqlConnection GetConnectionWL()
    {
        string str = ConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString();
        myConn = new SqlConnection(str);
        return myConn;
    }
    public WebServiceFaHuoOnline () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    /// <summary>
    /// 获取客户信息WEB服务
    /// </summary>
    /// <param name="strcompid"></param>
    /// <param name="PWD"></param>
    /// <param name="USERID"></param>
    /// <returns></returns>
    [WebMethod]
    public DataTable GetCustomer(string strcompid,string PWD,string USERID)
    {
        try
        {
            if (!PWD.Trim().Equals("TianJiankeJi") && (!USERID.Trim().Equals("TjUserID")))
            {
                return null;
            }
            else
            {
                return BackCustomer(strcompid);
            }
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// 获取产品名称WEB服务
    /// </summary>
    /// <param name="strcompid"></param>
    /// <param name="PWD"></param>
    /// <param name="USERID"></param>
    /// <returns></returns>
    [WebMethod]
    public DataTable GetProDuctName(string strcompid,string PWD,string USERID)
    {
        try
        {
            if (!PWD.Trim().Equals("TianJiankeJi") && (!USERID.Trim().Equals("TjUserID")))
            {
                return null;
            }
            else
            {
                return BackProduct(strcompid);
            }
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 获取仓库信息WEB 服务
    /// </summary>
    /// <param name="strcompid"></param>
    /// <param name="PWD"></param>
    /// <param name="USERID"></param>
    /// <returns></returns>
    [WebMethod]
    
    public DataTable GetStoreHouseName(string strcompid, string PWD, string USERID)
    {
        try
        {
            if (!PWD.Trim().Equals("TianJiankeJi") && (!USERID.Trim().Equals("TjUserID")))
            {
                return null;
            }
            else
            {
                return BackStoreHouseInfo(strcompid);
            }
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// 将信息插入到整体发货表中的WEB 服务
    /// </summary>
    /// <param name="strcompid"></param>
    /// <param name="PWD"></param>
    /// <param name="USERID"></param>
    /// <returns></returns>
    [WebMethod]

    public string InsertALLFaHuoInfo(string PWD,string strUSERID,string stralling)//string boxlabel, string fhdate, string fhpici, string agentid, string pid, string stid, string compid, string userid, string flag, string memo, string otherinfo)
    {
        try
        {
            if (!PWD.Trim().Equals("TianJiankeJi") && (!strUSERID.Trim().Equals("TjUserID")))
            {
                return "false";
            }
            else
            {
                return InsertIntoAllFaHuoInfo(stralling);//boxlabel, fhdate, fhpici, agentid, pid, stid, compid, userid, flag, memo, otherinfo);
            }
        }
        catch (Exception eee)
        {
            return eee.Message;
        }
    }
    /// <summary>
    /// 通过CompID 返回产品名称和对应的ID
    /// </summary>
    /// <param name="strcompid"></param>
    /// <returns></returns>
    private DataTable BackProduct(string strcompid)
    {
        string str = "select infor_id,Products_name from tb_products_infor where compid='" + strcompid + "'";
        using (var myConn = GetConnectionWL())
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            ds = new DataSet();
            ad = new SqlDataAdapter(str, myConn);
            ad.Fill(ds, "Product");
            return ds.Tables["Product"];
        }
    }
    private DataTable BackStoreHouseInfo(string strcompid)
    {
        string str = "select stid,storehousename from tb_storehouse where compid='" + strcompid + "'";
        using (var myConn = GetConnectionWL())
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            ds = new DataSet();
            ad = new SqlDataAdapter(str, myConn);
            ad.Fill(ds, "StoreHouse");
            return ds.Tables["StoreHouse"];
        }
    }

    private DataTable BackCustomer(string strcompid)
    {
        string str = "select compid,compname from tj_registercompanys where parentid='" + strcompid + "'";
        DataTable dt = new DataTable();
        dt.TableName = "TempTable";
        using (var myConn = GetConnection())
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            ds = new DataSet();
            DataSet ds1 = new DataSet();
            ad = new SqlDataAdapter(str, myConn);
            ad.Fill(ds, "Cust");
            if (ds.Tables["Cust"].Rows.Count>0)
            {
                dt.Columns.Add("ID");
                dt.Columns.Add("CustName");
                for (int i = 0; i <= ds.Tables["Cust"].Rows.Count - 1; i++)
                {
                    string str1 = ds.Tables["Cust"].Rows[i][0].ToString().Trim();
                    string str2 = ds.Tables["Cust"].Rows[i][1].ToString().Trim();
                    string str3 = "select * from tb_compagentinfo where compid='" + strcompid + "' and agentid='" + str1 + "'";
                    using (var myConn1 = GetConnectionWL())
                    {
                        if (myConn1.State == ConnectionState.Closed)
                        {
                            myConn1.Open();
                        }
                        SqlDataAdapter  ad1 = new SqlDataAdapter(str3, myConn1);
                        ad1.Fill(ds1, "Cust11");
                        if (ds1.Tables ["Cust11"].Rows .Count >0)
                        {
                            dt.Rows.Add (str1,str2);
                        }
                    }
                }
                if (dt.Rows.Count >0 )
                {
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
    private string InsertIntoAllFaHuoInfo(string strall)//string boxlabel,string fhdate,string fhpici,string agentid,string pid,string stid,string compid,string userid,string flag,string memo,string otherinfo)
    {
        //-----用竖线隔开数据，然后在此进行分解再做处理
        try
        {
            using (var myConn = GetConnectionWL())
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                string[] strarray = strall.Split('|');
                string str1 = "insert into TB_ALLInsertFaHuo(boxlabel,fhdate,fhpici,agentid,productid,stid,compid,userid,flag,memo,otherinfo) values(";
                string str2 = "";
                string str3 = "";
                for (int i = 0; i <= strarray.Length - 1; i++)
                {
                    if (!strarray[i].Trim().Equals(""))
                    {
                        string strbox = strarray[i].Split(',')[0].Trim();
                        string strfhdate = strarray[i].Split(',')[1].Trim();
                        string strfhpici = strarray[i].Split(',')[2].Trim();
                        string stragentid = strarray[i].Split(',')[3].Trim();
                        string strpid = strarray[i].Split(',')[4].Trim();
                        string strstid = strarray[i].Split(',')[5].Trim();
                        string strcompid = strarray[i].Split(',')[6].Trim();
                        string struserid = strarray[i].Split(',')[7].Trim();
                        string strflag = strarray[i].Split(',')[8].Trim();
                        string strmemo = strarray[i].Split(',')[9].Trim();
                        string strotherinfo = strarray[i].Split(',')[10].Trim();
                        str2 = "'" + strbox + "','" + strfhdate + "','" + strfhpici + "','" + stragentid + "','" + strpid + "','" + strstid + "','" + strcompid + "','" + struserid + "','" + strflag + "','" + strmemo + "','" + strotherinfo + "');";
                        str3 = str3 + str1 + str2;
                    }
                }
                //string sql = "";

                //sql = "insert into TB_ALLInsertFaHuo(boxlabel,fhdate,fhpici,agentid,productid,stid,compid,userid,flag,memo,otherinfo) values('" + boxlabel + "','" + fhdate + "','" + fhpici + "','" + agentid + "','" + pid + "','" + stid + "','" + compid + "','" + userid + "','" + flag + "','" + memo + "','" + otherinfo + "')";
                if (!str3.Equals(""))
                {
                    SqlCommand cmd = new SqlCommand(str3, myConn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    myConn.Close();
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
        }
        catch (Exception eee)
        {
            return eee.Message;
        }
    }
}
