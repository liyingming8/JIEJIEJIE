using System;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// WebServiceScan 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class WebServiceScan : WebService {

    public WebServiceScan () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    private SqlConnection myConn;
    private DataSet ds;
    private SqlDataAdapter ad;
    public DataSet Mydataset;
    public SqlDataAdapter ada;

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
    /// <summary>
    /// TJMarketYin
    /// </summary>
    /// <returns></returns>
    public SqlConnection GetConnection()
    {
        string str = ConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString();
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
    public string InsertALLFaHuoInfo(string PWD, string strUSERID, string stralling)
    //string boxlabel, string fhdate, string fhpici, string agentid, string pid, string stid, string compid, string userid, string flag, string memo, string otherinfo)
    {
        try
        {
            if (!PWD.Trim().Equals("TianJiankeJi") && (!strUSERID.Trim().Equals("TjUserID")))
            {
                return "false";
            }
            else
            {
                return InsertIntoAllFaHuoInfo(stralling);
                //boxlabel, fhdate, fhpici, agentid, pid, stid, compid, userid, flag, memo, otherinfo);
            }
        }
        catch (Exception eee)
        {
            return eee.Message;
        }
    }
    [WebMethod]
    public string InsertALLRuKuInfo(string PWD, string strUSERID, string stralling)
    //string boxlabel, string fhdate, string fhpici, string agentid, string pid, string stid, string compid, string userid, string flag, string memo, string otherinfo)
    {
        try
        {
            if (!PWD.Trim().Equals("TianJiankeJi") && (!strUSERID.Trim().Equals("TjUserID")))
            {
                return "false";
            }
            else
            {
                return InsertIntoAllRuKuInfo(stralling);
                //boxlabel, fhdate, fhpici, agentid, pid, stid, compid, userid, flag, memo, otherinfo);
            }
        }
        catch (Exception eee)
        {
            return eee.Message;
        }
    }

    private string InsertIntoAllRuKuInfo(string strall)
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
                string str1 ="update ";
                string str2 = "";
                string str3 = "";
                for (int i = 0; i <= strarray.Length - 1; i++)
                {
                    if (!strarray[i].Trim().Equals(""))
                    {
                        string strbox = strarray[i].Split(',')[0].Trim();
                        string strrkdh = strarray[i].Split(',')[1].Trim();//beizhu
                        string strrktype = strarray[i].Split(',')[2].Trim();//rktyid
                        string strrkph = strarray[i].Split(',')[3].Trim();//fhkey
                        string strpid = strarray[i].Split(',')[4].Trim();//pid
                        string strstid = strarray[i].Split(',')[5].Trim();//stid
                        string strcompid = strarray[i].Split(',')[6].Trim();//compid
                        string strshengchanbanzu = strarray[i].Split(',')[7].Trim();//wlid
                        string strjianyanbanzu = strarray[i].Split(',')[8].Trim();//wsid
                        string strshengchanshijian = strarray[i].Split(',')[9].Trim();//shengchanshijian

                        string strfour="W" + strbox.Substring (0,4);
                        Int32 int47=Convert .ToInt32(strbox.Substring (4,7));
                        if (int47>=1 && int47<=2500000)
                        {
                            strfour=strfour + "_00";
                        }
                        else if (int47>=2500001 && int47<=5000000)
                        {
                            strfour=strfour + "_01";
                        }
                        else if (int47>=5000001 && int47<=7500000)
                        {
                            strfour=strfour + "_02";
                        }
                        else if (int47>=7500001 && int47<=9999999)
                        {
                            strfour=strfour + "_03";
                        }

                        str2 = " beizhu='" + strrkdh + "',rktyid='" + strrktype + "',pid='" + strpid + "',fhkey='" + strrkph + "',stid='" + strstid + "',compid='" + strcompid + "',wlid='" + strshengchanbanzu + "',wsid='" + strjianyanbanzu + "',shengchanshijian='" + strshengchanshijian + "'";
                        string str4 = " where boxlabel01='" + strbox + "';";
                        str3 = str3 + str1 + strfour + " set " + str2 + str4;
                        strfour = "";
                        str2 = "";
                        str4 = "";
                    }
                }
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
    private string InsertIntoAllFaHuoInfo(string strall)
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
                string str1 =
                    "insert into TB_ALLInsertFaHuo(boxlabel,fhdate,fhpici,agentid,productid,stid,compid,userid,flag,memo,otherinfo) values(";
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
                        str2 = "'" + strbox + "','" + strfhdate + "','" + strfhpici + "','" + stragentid + "','" +
                               strpid + "','" + strstid + "','" + strcompid + "','" + struserid + "','" + strflag +
                               "','" + strmemo + "','" + strotherinfo + "');";
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
    [WebMethod]
    public DataTable ReturnProduct(string strcompid)
    {
        try
        {
            string str1 = "SELECT t.infor_id,t.products_name,p.standarsdes  FROM [TB_Products_Infor] t,[TB_ProducStandards] p  where t.compid='" + strcompid + "' and t.psid=p.psid and t.compid=p.compid";
            using (var myConn = GetConnectionWL())
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                ds = new DataSet();
                ad = new SqlDataAdapter(str1, myConn);
                ad.Fill(ds, "Product");
                return ds.Tables["Product"];
            }
        }
        catch
        {
            return null;
        }

    }

    [WebMethod]
    public DataTable ReturnShengChanBanZu(string strcompid)
    {
        try
        {
            string str1 = "SELECT WLID,WorkLineName  FROM [TB_WorkLineInfo]  where compid='" + strcompid + "'";
            using (var myConn = GetConnectionWL())
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                ds = new DataSet();
                ad = new SqlDataAdapter(str1, myConn);
                ad.Fill(ds, "SCBZ");
                return ds.Tables["SCBZ"];
            }
        }
        catch
        {
            return null;
        }

    }
    [WebMethod]
    public DataTable ReturnJianYanBanZu(string strcompid)
    {
        try
        {
            string str1 = "SELECT [ZJID],[ZJname]  FROM [TB_ZhiJianYuan]  where compid='" + strcompid + "'";
            using (var myConn = GetConnectionWL())
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                ds = new DataSet();
                ad = new SqlDataAdapter(str1, myConn);
                ad.Fill(ds, "JYBZ");
                return ds.Tables["JYBZ"];
            }
        }
        catch
        {
            return null;
        }

    }
    [WebMethod]
    public  DataTable ReturnCustomer(string strcompid)
    {
        try
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
                if (ds.Tables["Cust"].Rows.Count > 0)
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
                            SqlDataAdapter ad1 = new SqlDataAdapter(str3, myConn1);
                            ad1.Fill(ds1, "Cust11");
                            if (ds1.Tables["Cust11"].Rows.Count > 0)
                            {
                                dt.Rows.Add(str1, str2);
                            }
                        }
                    }
                    if (dt.Rows.Count > 0)
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
        catch 
        {
            return null;
        }
    }

}
