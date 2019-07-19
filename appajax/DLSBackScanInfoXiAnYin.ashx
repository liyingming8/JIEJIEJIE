<%@ WebHandler Language="C#" Class="DLSBackScanInfoXiAnYin " %>

using System;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
public class DLSBackScanInfoXiAnYin : IHttpHandler 
{
    readonly StringBuilder _sb = new StringBuilder();

    SqlConnection myConn = null;
    SqlCommand myCmd;
    DataSet ds;
    SqlDataAdapter ad;
    public DataSet Mydataset;
    public SqlDataAdapter ada;
    //===TianJianWuLiuWebNew
    public SqlConnection GetConnectionWL()
    {
        string str = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringWuLiu"].ToString();
        myConn = new SqlConnection(str);
        return myConn;
    }
    //===TJMarketingSystemYin
    public SqlConnection GetConnectionMarket()
    {
        string str = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringMarket"].ToString();
        myConn = new SqlConnection(str);
        return myConn;
    }
    
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.QueryString["strAllInfo"]))
        {
            string intBackInfo = BackScanDataProcessDataInfo(context.Request.QueryString["strAllInfo"].Trim());
            if (intBackInfo.Equals ("1"))
            {
                context.Response.Write("1");
            }
            else
            {
                context.Response.Write(intBackInfo);
            }
        }
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

    string _str1 = "";
    string _str2 = "";
    string _str3 = "";
    string _str4 = "";
    
    string _strfhflag = "";
    private string _strBatchNo = "";
    private string _strcode = "";
    private string _strpid = "";
    private string _strdls = "";
    private string[] straa;
    private string strReturn = "";
    private string strFour = "";
    private string _strTB_BX, _strTB_FH, _strTB_00, _strTB_FHInfo2, _strParentID;
    /// <summary>
    /// 根据经销商返回的扫码箱标，判断产品是否有发错的情形，最后的插入及删除操作暂时不用事务处理
    /// </summary>
    /// <param name="strall111"></param>
    /// <returns></returns>
    private string BackScanDataProcessDataInfo(string strall111)//string ProductID, string MadeDate)
    {
        try
        {
            using (var myConn = GetConnectionWL())
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                _str2 = "";
                _str3 = "";
                _strfhflag = "";
                _strBatchNo = DateTime.Now.ToString("yyyyMMddhhmmss");
                if (strall111.Trim().Contains(","))
                {
                    straa = strall111.Split(',');
                    strReturn = straa.Length.ToString();
                    _strcode = strall111.Split(',')[0].Trim();
                    _strdls = strall111.Split(',')[1].Trim();
                    _strpid = strall111.Split(',')[2].Trim();
                    strFour = _strcode.Substring(0, 4);//箱标前四位
                    _strTB_00 = "W" + strFour;
                    //查询父级（上级）CompID
                    _strParentID = BackParentID(_strdls);
                    
                    _strTB_FHInfo2 = "TB_FaHuoInfo_" + _strParentID.ToString ().Trim ();
                    _strfhflag = "";
                    _str3 = "select '00',a.boxlabel01,ToAgentID,b.ProID,a.FHKey  FROM [TianJianWuLiuWebnew].[dbo].[" + _strTB_00 + "_00_FH] a,[TianJianWuLiuWebnew].[dbo].[" + _strTB_FHInfo2 + "] b  where a.boxlabel01='" + _strcode + "' and b.fhkey=a.FHKey  union all  select '01',a.boxlabel01,ToAgentID,b.ProID,a.FHKey  FROM [TianJianWuLiuWebnew].[dbo].[" + _strTB_00 + "_01_FH] a,[TianJianWuLiuWebnew].[dbo].[" + _strTB_FHInfo2 + "] b  where a.boxlabel01='" + _strcode + "' and b.fhkey=a.FHKey  union all SELECT '02',a.boxlabel01,ToAgentID,b.ProID,a.FHKey   FROM [TianJianWuLiuWebnew].[dbo].[" + _strTB_00 + "_02_FH] a,[TianJianWuLiuWebnew].[dbo].[" + _strTB_FHInfo2 + "] b  where a.boxlabel01='" + _strcode + "' and b.fhkey=a.FHKey  union all select '03',a.boxlabel01,ToAgentID,b.ProID,a.FHKey  FROM [TianJianWuLiuWebnew].[dbo].[" + _strTB_00 + "_03_FH] a,[TianJianWuLiuWebnew].[dbo].[" + _strTB_FHInfo2 + "] b  where a.boxlabel01='" + _strcode + "' and b.fhkey=a.FHKey ";
                    if (!_str3.Equals(""))
                    {
                        //从FH表中判断该箱标是否存在
                        DataTable dt = BackSelectInfo(_str3);
                        if (dt != null && dt.Rows.Count > 0)//===存在
                        {
                            string _aid = dt.Rows[0][2].ToString().Trim();//查询到的已发货后的AgentID
                            string _pid = dt.Rows[0][3].ToString().Trim();//查询到的已发货后的ProductID
                            string _w00 = _strTB_00 + "_" + dt.Rows[0][0].ToString().Trim();//查询到所对应的表
                            string _strFHkey = dt.Rows[0][4].ToString().Trim();
                            if (!_aid.Equals(_strdls) || !_pid.Equals(_strpid))//AgentID与Pid有一个不相同时
                            {
                                //插入发货记录
                                string _strpici = "Bu" + System.DateTime.Now.ToString("yyyyMMddhhmmss");
                                string _guid = Guid.NewGuid().ToString();
                                string _strfhdate = System.DateTime.Now.ToString("yyyy-MM-dd");
                                //---插入到TB_FaHuoInfo_2
                                _str1 = "insert into " + _strTB_FHInfo2 + "([FHPiCi],[FHTypeID],[FHDate],[AgentID],[FHUserID],[TableNameInfo],[FHKey],[ProID],[XiangNumber],[CompID],[STID]) values('" + _strpici + "','3','" + _strfhdate + "','" + _strdls + "','112','" + _w00 + "','" + _guid + "','" + _strpid + "','1','2','21')";
                                //---插入到Wxxxx_0X_FH
                                _str2 = "insert into " + _w00 + "_FH" + "([BoxLabel01],[FromAgentID],[FromStorehouseID],[ToAgentID],[ToStoreHouseID],[FHDate],[FHType],[UserID],[FHPici],[FHKey],[CompID],[ProductName],[PID]) values('" + _strcode + "','2','1','" + _strdls + "','21','" + _strfhdate + "','3','112','" + _strpici + "','" + _guid + "','2','XiFengJiu','" + _strpid + "')";
                                //---更新TB_FaHuoInfo_2中的发货数量（减1）
                                _str4 = "update " + _strTB_FHInfo2 + " set XiangNumber=XiangNumber-1 where fhkey='" + _strFHkey + "'";
                                //先插入_str2如果成功再插入_str1再成功则更新总表中原有的发货数量（_str4)
                                int intcount = ExecuteSQLInfo(_str2);
                                if (intcount > 0)
                                {
                                    intcount = ExecuteSQLInfo(_str1);
                                    if (intcount > 0)
                                    {
                                        intcount = ExecuteSQLInfo(_str4);
                                        if (intcount > 0)
                                        {
                                            return "1";
                                        }
                                        else
                                        {
                                            //删除已经插入的记录
                                            _str1 = "delete from " + _strTB_FHInfo2 + " where fhkey='" + _guid + "'";
                                            _str2 = "delete from " + _w00 + "_FH" + " where fhkey='" + _guid + "'";
                                            ExecuteSQLInfo(_str1);
                                            ExecuteSQLInfo(_str2);
                                            return "011";
                                        }
                                    }
                                    else
                                    {
                                        //===删除已插入的记录
                                        _str2 = "delete from " + _w00 + "_FH" + " where fhkey='" + _guid + "'";
                                        ExecuteSQLInfo(_str2);
                                        return "002";
                                    }
                                }
                                else
                                {
                                    return "001";
                                }
                            }
                            else//查询出的PID与AgentID与返回的值是相同的
                            {
                                return "1";
                            }
                        }
                        else//不存在
                        {
                            string _strbucunzai = "select '00',a.boxlabel01  FROM [TianJianWuLiuWebnew].[dbo].[" + _strTB_00 + "_00] a where a.boxlabel01='" + _strcode + "' union all select '00',a.boxlabel01  FROM [TianJianWuLiuWebnew].[dbo].[" + _strTB_00 + "_00] a,[TianJianWuLiuWebnew].[dbo].[" + _strTB_00 + "_00_BX] b where (b.boxlabel01='" + _strcode + "' or b.BoxLabel ='" + _strcode + "') and a.BoxLabel01 =b.BoxLabel01  union all select '01',a.boxlabel01  FROM [TianJianWuLiuWebnew].[dbo].[" + _strTB_00 + "_01] a where a.boxlabel01='" + _strcode + "' union all select '01',a.boxlabel01  FROM [TianJianWuLiuWebnew].[dbo].[" + _strTB_00 + "_01] a,[TianJianWuLiuWebnew].[dbo].[" + _strTB_00 + "_01_BX] b where (b.boxlabel01='" + _strcode + "' or b.BoxLabel ='" + _strcode + "') and a.BoxLabel01 =b.BoxLabel01  union all SELECT '02',a.boxlabel01  FROM [TianJianWuLiuWebnew].[dbo].[" + _strTB_00 + "_02] a where a.boxlabel01='" + _strcode + "' union all select '02',a.boxlabel01  FROM [TianJianWuLiuWebnew].[dbo].[" + _strTB_00 + "_02] a,[TianJianWuLiuWebnew].[dbo].[" + _strTB_00 + "_02_BX] b where (b.boxlabel01='" + _strcode + "' or b.BoxLabel ='" + _strcode + "') and a.BoxLabel01 =b.BoxLabel01  union all select '03',a.boxlabel01  FROM [TianJianWuLiuWebnew].[dbo].[" + _strTB_00 + "_03] a where a.boxlabel01='" + _strcode + "' union all select '03',a.boxlabel01  FROM [TianJianWuLiuWebnew].[dbo].[" + _strTB_00 + "_03] a,[TianJianWuLiuWebnew].[dbo].[" + _strTB_00 + "_03_BX] b where (b.boxlabel01='" + _strcode + "' or b.BoxLabel ='" + _strcode + "') and a.BoxLabel01 =b.BoxLabel01";
                            DataTable dtcz = BackSelectInfo(_strbucunzai);
                            if (dtcz != null && dtcz.Rows.Count > 0)
                            {
                                //===插入记录
                                //插入发货记录
                                string _strpici = "Bu" + System.DateTime.Now.ToString("yyyyMMddhhmmss");
                                string _guid = Guid.NewGuid().ToString();
                                string _strfhdate = System.DateTime.Now.ToString("yyyy-MM-dd");
                                string _strwz = dtcz.Rows[0][0].ToString().Trim();
                                //---TB_FaHuoInfo_2
                                _str1 = "insert into " + _strTB_FHInfo2 + "([FHPiCi],[FHTypeID],[FHDate],[AgentID],[FHUserID],[TableNameInfo],[FHKey],[ProID],[XiangNumber],[CompID],[STID]) values('" + _strpici + "','3','" + _strfhdate + "','" + _strdls + "','112','" + _strTB_00 + "_" + _strwz + "','" + _guid + "','" + _strpid + "','1','2','21')";
                                //---Wxxxx_0X_FH
                                _str2 = "insert into " + _strTB_00 + "_" + _strwz + "_FH" + "([BoxLabel01],[FromAgentID],[FromStorehouseID],[ToAgentID],[ToStoreHouseID],[FHDate],[FHType],[UserID],[FHPici],[FHKey],[CompID],[ProductName],[PID]) values('" + _strcode + "','2','1','" + _strdls + "','21','" + _strfhdate + "','3','112','" + _strpici + "','" + _guid + "','2','XiFengJiu','" + _strpid + "')";
                                //先插入_str2如果成功再插入_str1
                                int intcount = ExecuteSQLInfo(_str2);
                                if (intcount > 0)
                                {
                                    intcount = ExecuteSQLInfo(_str1);
                                    if (intcount > 0)
                                    {
                                        return "1";
                                    }
                                    else
                                    {
                                        //删除已经插入的记录
                                        _str2 = "delete from " + _strTB_00 + "_" + _strwz + "_FH" + " where fhkey='" + _guid + "'";
                                        ExecuteSQLInfo(_str2);
                                        return "004";
                                    }
                                }
                                else
                                {
                                    return "003";
                                }
                            }
                            else
                            {
                                //====基础数据都不存在，因此需要将此箱标插入到表BuDataInfo中，后续找相应的基础数据，同时也需要再插入一条记录其中的两个发货表和总计中
                                string strbudatainfo = "insert into budatainfo(boxlabel,aid,pid) values('" + _strcode + "','" + _strdls + "','" + _strpid + "')";
                                string _strwz = "";
                                Int32 _int7ven = Convert.ToInt32(_strcode.Substring(4, 7));
                                if (_int7ven >= 1 && _int7ven <= 2500000)
                                {
                                    _strwz = "00";
                                }
                                else if (_int7ven >= 2500001 && _int7ven <= 5000000)
                                {
                                    _strwz = "01";
                                }
                                else if (_int7ven >= 5000001 && _int7ven <= 7500000)
                                {
                                    _strwz = "02";
                                }
                                else if (_int7ven >= 7500001 && _int7ven <= 9999999)
                                {
                                    _strwz = "03";
                                }
                                //插入发货记录==批次用Bu开头，以表示是经销商扫描确认返回后的数据（补）
                                string _strpici = "Bu" + System.DateTime.Now.ToString("yyyyMMddhhmmss");
                                string _guid = Guid.NewGuid().ToString();
                                string _strfhdate = System.DateTime.Now.ToString("yyyy-MM-dd");
                                //---TB_FaHuoInfo_2
                                _str1 = "insert into " + _strTB_FHInfo2 + "([FHPiCi],[FHTypeID],[FHDate],[AgentID],[FHUserID],[TableNameInfo],[FHKey],[ProID],[XiangNumber],[CompID],[STID]) values('" + _strpici + "','3','" + _strfhdate + "','" + _strdls + "','112','" + _strTB_00 + "_" + _strwz + "','" + _guid + "','" + _strpid + "','1','2','21')";
                                //---Wxxxx_0X_FH
                                _str2 = "insert into " + _strTB_00 + "_" + _strwz + "_FH" + "([BoxLabel01],[FromAgentID],[FromStorehouseID],[ToAgentID],[ToStoreHouseID],[FHDate],[FHType],[UserID],[FHPici],[FHKey],[CompID],[ProductName],[PID]) values('" + _strcode + "','2','1','" + _strdls + "','21','" + _strfhdate + "','3','112','" + _strpici + "','" + _guid + "','2','XiFengJiu','" + _strpid + "')";
                                //先插入_str2如果成功再插入_str1
                                int intcount = ExecuteSQLInfo(_str2);
                                if (intcount > 0)
                                {
                                    intcount = ExecuteSQLInfo(_str1);
                                    if (intcount > 0)
                                    {
                                        intcount = ExecuteSQLInfo(strbudatainfo);
                                        if (intcount > 0)
                                        {
                                            return "1";
                                        }
                                        else
                                        {
                                            //删除已经插入的记录
                                            _str1 = "delete from " + _strTB_FHInfo2 + " where fhkey='" + _guid + "'";
                                            _str2 = "delete from " + _strTB_00 + "_" + _strwz + "_FH" + " where fhkey='" + _guid + "'";
                                            ExecuteSQLInfo(_str1);
                                            ExecuteSQLInfo(_str2);
                                            return "010";
                                        }
                                    }
                                    else
                                    {
                                        //删除已经插入的记录
                                        _str2 = "delete from " + _strTB_00 + "_" + _strwz + "_FH" + " where fhkey='" + _guid + "'";
                                        ExecuteSQLInfo(_str2);
                                        return "006";
                                    }
                                }
                                else
                                {
                                    return "005";
                                }
                            }
                        }
                    }
                    else
                    {
                        return "007";
                    }
                }
                else
                {
                    return "008";
                }
            }
        }
        catch
        {
            return "009";
        }
    }
    /// <summary>
    /// 以表的形式返回执行的SQL查询语句
    /// </summary>
    /// <param name="strsqlinfo"></param>
    /// <returns></returns>
    private DataTable BackSelectInfo(string strsqlinfo)
    {
        using (var myConn = GetConnectionWL())
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            ds = new DataSet();
            ds.Clear();
            ad = new SqlDataAdapter(strsqlinfo, myConn);
            ad.Fill(ds, "BackInfo");
            return ds.Tables["BackInfo"];
        }
    }
    /// <summary>
    /// 返回执行插入或删除或更新后的影响行数，以判断是否成功
    /// </summary>
    /// <param name="strsqlinfo"></param>
    /// <returns></returns>
    private int  ExecuteSQLInfo(string strsqlinfo)
    {
        try
        {
            using (var myConn = GetConnectionWL())
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                SqlCommand cmd = new SqlCommand(strsqlinfo, myConn);
                int intc = cmd.ExecuteNonQuery();
                cmd.Dispose();
                myConn.Close();
                return intc;
            }
        }
        catch (Exception eee)
        {
            return 0;
        }
    }
    /// <summary>
    /// 返回strcompid对应上级的COMPID
    /// </summary>
    /// <param name="strcompid"></param>
    /// <returns></returns>
    private string BackParentID(string strcompid)
    {
        try
        {
            string str1 = "select [ParentID] from [TJ_RegisterCompanys] where compid='" + strcompid + "'";
            using (var myConn = GetConnectionMarket ())
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                ds = new DataSet();
                ds.Clear();
                ad = new SqlDataAdapter(str1, myConn);
                ad.Fill(ds, "BackInfo");
                if (ds.Tables["BackInfo"] != null && ds.Tables["BackInfo"].Rows.Count > 0)
                {
                    string strparentid = ds.Tables["BackInfo"].Rows[0][0].ToString().Trim();
                    ds.Dispose();
                    ad.Dispose();
                    return strparentid;
                }
                else
                {
                    return "1";//根据客户返回COMPID，找不到对应的上级COMPID时，暂时寄存于我公司comid的表中，以防客户确认扫码中断
                }
            }
        }
        catch (Exception eee)
        {
            return "1";//根据客户返回COMPID，找不到对应的上级COMPID时，暂时寄存于我公司comid的表中，以防客户确认扫码中断
        }
    }
}