using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using Npgsql;

/// <summary>
///DBClass 的摘要说明
/// </summary>
public class DBClass
{
    SqlConnection myConn;
    SqlCommand myCmd;
    DataSet ds;
    private DataTable dttemp;
    public DataSet Mydataset;
    public SqlDataAdapter ada;
    private NpgsqlConnection pgconn;
    private NpgsqlCommand pgcommd;
    private readonly string _showmode = "0";
    public DBClass(string sm)
    {
        _showmode = sm;
    }

    public DBClass()
    {
        _showmode = "0";
    }

    public SqlConnection GetConnection(string showmode)
    {
        string str = "";
        if (showmode.Equals("1"))
        {
            str = ConfigurationManager.ConnectionStrings["SqlServerConnStringShowMode"].ToString();
        }
        else
        {
            str = ConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString();
        }

        myConn = new SqlConnection(str);
        return myConn;

    }
    public SqlConnection GetConnectionstrbbe()
    {
        string str = ConfigurationManager.ConnectionStrings["GuanDongBaoBeiEr"].ToString();
        myConn = new SqlConnection(str);
        return myConn;
    }

    public SqlConnection GetConnectionWL(string showmode)
    {
        string str = "";
        if (showmode.Equals("1"))
        {
            str = ConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiuShowMode"].ToString();
        }
        else
        {
            str = ConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString();
        }
        myConn = new SqlConnection(str);
        return myConn;
    }

    public SqlConnection GetConnectionSM
    {
        get
        {
            string str = ConfigurationManager.ConnectionStrings["SqlServerConnStringSM"].ToString();
            myConn = new SqlConnection(str);
            return myConn;
        }
    }
    public SqlConnection GetConnectionJGXQ()
    {
        string str = ConfigurationManager.ConnectionStrings["ConnectionStringJGXQ"].ToString();
        myConn = new SqlConnection(str);
        return myConn;
    }

    public SqlConnection GetConnectionCYJY()
    {
        string str = ConfigurationManager.ConnectionStrings["SqlServerConnStringCYJY"].ToString();
        myConn = new SqlConnection(str);
        return myConn;
    }

    public NpgsqlConnection GetPGConnctionGIS()
    {
        string str = ConfigurationManager.ConnectionStrings["PGConnStringGis"].ToString();
        pgconn = new NpgsqlConnection(str);
        return pgconn;
    }

    /// <summary>
    /// 得到扫码地址(县级)
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable Getsmadxjafter(string startime, string endtime)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "SELECT SMsj as address ,COUNT(*) as num  FROM [TJMarketingSystemYin].[dbo].[TJ_375SMinfo] where CompID=130 and SMProc like '%陕西省%' and SMTime>='" + startime + "' and SMTime<'" + endtime + "' group by SMsj order by SMsj desc ";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "CS");
            return Mydataset.Tables["CS"];
        }
    }

    ///<summary>
    ///得到扫码地址（省级）
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable GetsmadxjafterProvince(string startime, string endtime)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string myCmd = "SELECT SMProc as address ,COUNT(*) as num  FROM [TJMarketingSystemYin].[dbo].[TJ_375SMinfo] where CompID=130 and SMTime>='" + startime + "' and SMTime<'" + endtime + "' group by SMProc order by SMProc desc ";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "CS");
            return Mydataset.Tables["CS"];
        }
    }


    /// <summary>
    /// 根据条件查询user 表
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable Getcyjyuserinfo(string str)
    {
        myConn = GetConnectionCYJY();
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from CY_User where " + str + " order by RegistDate desc ", myConn);
        ada.Fill(ds, "userinfo");
        return ds.Tables["userinfo"];

    }


    /// <summary>
    /// 更新潞酒截止日期
    /// </summary>
    /// <param name="add"></param>
    /// <param name="str"></param>
    /// <returns></returns>
    public int UpdateLJinfo(string str, string gstr)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string sql = "";

            sql = "update TB_BuJiangLuJiu  set " + gstr + " where " + str + "";
            SqlCommand cmd = new SqlCommand(sql, myConn);
            int c = cmd.ExecuteNonQuery();
            cmd.Dispose();
            myConn.Close();
            return c;

        }
    }


    #region
    /// <summary>
    /// 得到扫码性别,compid
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable getsmsex(string startime, string endtime, string compid)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "SELECT  xb,COUNT(*) as num,SUBSTRING(CONVERT(varchar(10),SMTime , 23),0,8) as time FROM  (select sm.SMTime as SMTime , sm.SMAddress, xb.SexInfo as xb from TJ_375SMinfo  sm, TJ_User  xb where sm.UserID=xb.UserID and sm.CompID=" + compid + " ) as sm where SMTime>='" + startime + "' and SMTime<'" + endtime + "' group by SUBSTRING(CONVERT(varchar(10),SMTime , 23),0,8),xb order by time";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "CS");
            return Mydataset.Tables["CS"];
        }
    }

    public DataTable getsmad(string startime, string endtime, string compid)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "SELECT SUBSTRING(SMAddress,0,4) as address ,COUNT(*) as num  FROM TJ_375SMinfo  where CompID=" + compid + " and SMTime>='" + startime + "' and SMTime<'" + endtime + "' group by SUBSTRING(SMAddress,0,4) ";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "CS");
            return Mydataset.Tables["CS"];
        }
    }

    public DataTable GetsmcsForAllDay(string startime, string endtime, string compid)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string myCmd = "SELECT COUNT(*) as num,sm_date as time FROM TJ_375SMinfo  where CompID=" + compid + " and sm_date>='" + startime + "' and sm_date<'" + endtime + "'  group by sm_date order by time ";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "CS");
            return Mydataset.Tables["CS"];
        }
    }

    public DataTable GetsmcsForAllMonth(string startime, string endtime, string compid, string showmode)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string myCmd = "";
            if (showmode == "1")
            {
                myCmd = "SELECT COUNT(ID) as num,(Convert(varchar,sm_year)+RIGHT('0'+Convert(varchar,sm_month),2)) as time FROM TJ_375SMinfo  where sm_date>='" + startime + "' and sm_date<'" + endtime + "'  group by sm_year,sm_month order by time ";
            }
            else
            {
                myCmd = "SELECT COUNT(ID) as num,(Convert(varchar,sm_year)+RIGHT('0'+Convert(varchar,sm_month),2)) as time FROM TJ_375SMinfo  where CompID=" + compid + " and sm_date>='" + startime + "' and sm_date<'" + endtime + "'  group by sm_year,sm_month order by time ";
            }
            dttemp = new DataTable();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(dttemp);
            return dttemp;
        }
    }

    public DataTable GetsmcsForAllMonthForSWM(string startime, string endtime, string compid)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string myCmd = "";
            if (Convert.ToDateTime(startime).Year.Equals(Convert.ToDateTime(endtime).Year))
            {
                myCmd = "SELECT COUNT(ID) as num,(Convert(varchar,sm_year)+RIGHT('0'+Convert(varchar,sm_month),2)) as time FROM TJ_SMinfo_" + Convert.ToDateTime(startime).Year + "  where CompID=" + compid + " and sm_date>='" + startime + "' and sm_date<='" + endtime + "'  group by sm_year,sm_month order by time ";
            }
            else
            {
                for (int i = Convert.ToDateTime(startime).Year; i <= Convert.ToDateTime(endtime).Year; i++)
                {
                    if (myCmd == "")
                    {
                        myCmd = "SELECT COUNT(ID) as num,(Convert(varchar,sm_year)+RIGHT('0'+Convert(varchar,sm_month),2)) as time FROM TJ_SMinfo_" + i + "  where CompID=" + compid + " and sm_date>='" + startime + "' and sm_date<='" + endtime + "'  group by sm_year,sm_month order by time ";
                    }
                    else
                    {
                        myCmd = "UNION SELECT COUNT(ID) as num,(Convert(varchar,sm_year)+RIGHT('0'+Convert(varchar,sm_month),2)) as time FROM TJ_SMinfo_" + i + "  where CompID=" + compid + " and sm_date>='" + startime + "' and sm_date<='" + endtime + "'  group by sm_year,sm_month order by time ";
                    }
                }
            }
            dttemp = new DataTable();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(dttemp);
            return dttemp;
        }
    }

    public int GetFanCountBefore(string endtime, string compid)
    {
        using (var mycon = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                mycon.Open();
            }
            string sqlcmd = "Select count(UserID) as num from TJ_User where CompID=" + compid + " and reg_date<='" + endtime + "'";
            dttemp = new DataTable();
            ada = new SqlDataAdapter(sqlcmd, myConn);
            ada.Fill(dttemp);
            return Convert.ToInt32(dttemp.Rows[0][0]);
        }
    }

    public DataTable GetFanByMonth(string startdaime, string endtime, string compid)
    {
        using (var mycon = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                mycon.Open();
            }
            string sqlcmd = "";
            //上个月最后一天
            string endtimeLastMonuth = Convert.ToDateTime(Convert.ToDateTime(endtime).ToString("yyyy-MM-01")).AddDays(-1).ToString("yyyy-MM-dd");
            /*
            if (_showmode.Equals("1"))
            {
                sqlcmd = "Select count(UserID) as num, (Convert(varchar,reg_year)+RIGHT('0'+Convert(varchar,reg_month),2)) as time from TJ_User where  RID is null and reg_date>='" +
                          startdaime + "' and reg_date<='" + endtime + "' group by reg_year,reg_month order by time";
            }
            else
            {
            */
            dttemp = new DataTable();
            string sqlcmdLastMonth = "select top 1 ynm,mnm from TJ_Fans_RaiseInfo where CompId=" + compid+ " order by ynm desc,mnm desc";
            ada = new SqlDataAdapter(sqlcmdLastMonth, myConn);
            ada.Fill(dttemp);
            if (dttemp.Rows.Count>0)
            {
                //新增从最大月份开始
                string mMaxMonth = dttemp.Rows[0][0].ToString()+"-"+ dttemp.Rows[0][1].ToString();
                if (!mMaxMonth.Equals(Convert.ToDateTime(endtimeLastMonuth).ToString("yyyy-M")))
                {
                    dttemp.Clear();
                    sqlcmd = "Select count(UserID) as num, (Convert(varchar,reg_year)+RIGHT('0'+Convert(varchar,reg_month),2)) as time from TJ_User where CompID=" + compid + " and RID is null and reg_date>='" +
                          Convert.ToDateTime(Convert.ToDateTime(mMaxMonth).ToString("yyyy-MM-dd")).AddMonths(1).ToString("yyyy-MM-dd") + "' and reg_date<='" + endtimeLastMonuth + "' group by reg_year,reg_month order by time";
                    ada = new SqlDataAdapter(sqlcmd, myConn);
                    ada.Fill(dttemp);
                    if (dttemp.Rows.Count>0)
                    {                       
                        for (int i = 0; i < dttemp.Rows.Count; i++)
                        {
                            string insertSQL = "insert into TJ_Fans_RaiseInfo(compid,ynm,mnm,sumvl,updatetm) values(" + compid + "," + dttemp.Rows[i]["time"].ToString().Substring(0, 4)
                           + "," + dttemp.Rows[i]["time"].ToString().Substring(4) + "," + dttemp.Rows[i]["num"].ToString() + ",getdate())";
                            SqlCommand cmd = new SqlCommand(insertSQL, myConn);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            //新增全部
            else { 
                sqlcmd = "Select count(UserID) as num, (Convert(varchar,reg_year)+RIGHT('0'+Convert(varchar,reg_month),2)) as time from TJ_User where CompID=" + compid + " and RID is null and reg_date>='" +
                           startdaime + "' and reg_date<='" + endtimeLastMonuth + "' group by reg_year,reg_month order by time";
                ada = new SqlDataAdapter(sqlcmd, myConn);
                ada.Fill(dttemp);
                if (dttemp.Rows.Count>0) {
                    for (int i=0; i< dttemp.Rows.Count;i++) {
                        string insertSQL = "insert into TJ_Fans_RaiseInfo(compid,ynm,mnm,sumvl,updatetm) values("+ compid+","+ dttemp.Rows[i]["time"].ToString().Substring(0,4)
                            +","+ dttemp.Rows[i]["time"].ToString().Substring(4)+","+ dttemp.Rows[i]["num"].ToString()+",getdate())";
                        SqlCommand cmd = new SqlCommand(insertSQL, myConn);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            //从TJ_Fans_RaiseInfo查询数据
            string QuerySQL = @"select * from(select sumvl as num,(CAST(ynm AS varchar)+CAST((case when mnm <= 9 then '0'+CAST(mnm AS varchar) else CAST(mnm AS varchar) end) as varchar)) 
               as time from TJ_Fans_RaiseInfo where CompID=" + compid + ")a "+
              "  where time>='" + Convert.ToDateTime(startdaime).ToString("yyyyMM")+ "' and time<='" + Convert.ToDateTime(endtimeLastMonuth).ToString("yyyyMM") +"'";
            ada = new SqlDataAdapter(QuerySQL, myConn);
            dttemp.Clear();
            ada.Fill(dttemp);
            return dttemp;
        }
    }

    public string GetMinRegdate(string compid)
    {
        string sqlcmd = "select min(reg_date) from TJ_User where CompID=" + compid;
        using (var mycon = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                mycon.Open();
            }
            dttemp = new DataTable();
            ada = new SqlDataAdapter(sqlcmd, myConn);
            ada.Fill(dttemp);
            return Convert.ToDateTime(dttemp.Rows[0][0]).ToString("yyyy-MM-dd");
        }
    }

    public DataTable getsmcsDay(string startime, string endtime, string compid)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "SELECT COUNT(*) as num,SUBSTRING(CONVERT(varchar(15),SMTime , 23),0,11) as time FROM  TJ_375SMinfo  where CompID=" + compid + " and SMTime>='" + startime + "' and SMTime<'" + endtime + "'  group by SUBSTRING(CONVERT(varchar(15),SMTime , 23),0,11) order by time ";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "CS");
            return Mydataset.Tables["CS"];
        }
    }

    //public DataTable GetShengChanLiangYearAndMonth(string compid)
    //{
    //    using (var myConn = GetConnection(_showmode))
    //    {
    //        if (myConn.State == ConnectionState.Closed)
    //        {
    //            myConn.Open();
    //        }
    //        string myCmd =
    //            "SELECT SUM([BoxNumber]) as sL,[YearNum],[MonthNum]  FROM [TJ_HuiZong].[dbo].[TB_ShengChanLiang] where CompID=" +
    //            compid + " group by [YearNum],[MonthNum] order by [YearNum],[MonthNum]";
    //        DataTable dt = new DataTable();
    //        ada = new SqlDataAdapter(myCmd, myConn);
    //        ada.Fill(dt);
    //        return dt;
    //    }
    //}

    public DataTable GetShengChanLiangYearAndMonth(string compid)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            //获取上个月份
            DateTime currentTime = Convert.ToDateTime(DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-1).ToShortDateString());
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string sqlQuery = "";
            string sqlInsert = "";
            int month_result = 0;
            DateTime bigTime = new DateTime();

            //最大年月
            sqlQuery = "SELECT top 1 [YearNum],[MonthNum] FROM[TJ_HuiZong].[dbo].[TB_ShengChanLiang] where CompID = " + compid + " order by YearNum desc, MonthNum desc";
            ada = new SqlDataAdapter(sqlQuery, myConn);
            ada.Fill(ds, "max_time");
            if (ds.Tables["max_time"].Rows.Count > 0)
            {
                bigTime = Convert.ToDateTime(ds.Tables["max_time"].Rows[0][0].ToString() + "-" + ds.Tables["max_time"].Rows[0][1].ToString());
            }
            //月份差
            if (ds.Tables["max_time"].Rows.Count > 0)
            {
                month_result = currentTime.Year * 12 + currentTime.Month - bigTime.Year * 12 - bigTime.Month;
            }

            if (month_result > 0)
            {
                //循环月份差数据到汇总表，
                for (int i = 0; i < month_result; i++)
                {
                    DateTime current = Convert.ToDateTime(DateTime.Parse(currentTime.ToString("yyyy-MM-01")).AddMonths(-i).ToShortDateString());
                    int currentYear = current.Year;
                    int currentMonth = current.Month;
                    sqlQuery = "SELECT [CompID],[AgentID],[ProID],SUM([XiangNumber]) as BoxNumber,year(FHDate) as YearNum,MONTH(FHDate) as MonthNum FROM " +
                        " [TianJianWuLiuWebnew].[dbo].[TB_FaHuoInfo_" + compid + "] where CompID=" + compid + " and YEAR(FHDate)=" + currentYear +
                        " and MONTH(FHDate) in (" + currentMonth + ") group by [CompID],[AgentID],[ProID],[XiangNumber],FHDate order by YearNum,MonthNum";
                    ada = new SqlDataAdapter(sqlQuery, myConn);
                    //重置dataset对象
                    DataSet ds2 = new DataSet();
                    ada.Fill(ds2, "table");
                    if (ds2.Tables["table"].Rows.Count > 0)
                    {
                        for (int sing_row = 0; sing_row < ds2.Tables["table"].Rows.Count; sing_row++)
                        {
                            sqlInsert = "Insert into [TJ_HuiZong].[dbo].[TB_ShengChanLiang]([CompID],[AgentID],[ProID],[BoxNumber],[YearNum],[MonthNum])values(" + ds2.Tables["table"].Rows[sing_row][0] + "," + ds2.Tables["table"].Rows[sing_row][1] + "," + ds2.Tables["table"].Rows[sing_row][2] + "," + ds2.Tables["table"].Rows[sing_row][3] + "," + ds2.Tables["table"].Rows[sing_row][4] + "," + ds2.Tables["table"].Rows[sing_row][5] + ")";
                            SqlCommand cmd = new SqlCommand(sqlInsert, myConn);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    //TB_FaHuoInfo_生产量表没有数据，则在TB_ShengChanLiang为0
                    else
                    {
                        sqlInsert = "Insert into [TJ_HuiZong].[dbo].[TB_ShengChanLiang]([CompID],[BoxNumber],[YearNum],[MonthNum])values(" + compid + "," + 0 + "," + currentYear + "," + currentMonth + ")";
                        SqlCommand cmd = new SqlCommand(sqlInsert, myConn);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            string myCmd =
                "SELECT SUM([BoxNumber]) as sL,[YearNum],[MonthNum]  FROM [TJ_HuiZong].[dbo].[TB_ShengChanLiang] where CompID=" +
                compid + " group by [YearNum],[MonthNum] order by [YearNum],[MonthNum]";
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(dt);
            return dt;
        }
    }

    public DataTable getsmadxj(string startime, string endtime, string compid)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "SELECT SUBSTRING(SMAddress,0,10) as address ,COUNT(*) as num  FROM  TJ_375SMinfo  where CompID=" + compid
                + " and SMTime>='" + startime + "' and SMTime<'" + endtime + "' group by SUBSTRING(SMAddress,0,10) order by num desc  ";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "CS");
            return Mydataset.Tables["CS"];
        }
    }
    #endregion

    /// <summary>
    /// 得到扫码性别
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable getsmsex(string startime, string endtime)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "SELECT xb,COUNT(*) as num,SUBSTRING(CONVERT(varchar(10),SMTime , 23),0,8) as time FROM  (select sm.SMTime as SMTime , sm.SMAddress, xb.SexInfo as xb from [TJMarketingSystemYin].[dbo].[TJ_375SMinfo] sm,[TJMarketingSystemYin].[dbo].[TJ_User] xb where sm.UserID=xb.UserID and sm.CompID=130 ) as sm where SMTime>='" + startime + "' and SMTime<'" + endtime + "' group by SUBSTRING(CONVERT(varchar(10),SMTime , 23),0,8),xb order by time";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "CS");
            return Mydataset.Tables["CS"];
        }
    }

    /// <summary>
    /// 得到扫码次数
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable Getsmcs(string startime, string endtime)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "SELECT COUNT(*) as num,SUBSTRING(CONVERT(varchar(10),SMTime , 23),0,8) as time FROM [TJMarketingSystemYin].[dbo].[TJ_375SMinfo] where CompID=130 and SMTime>='" + startime + "' and SMTime<'" + endtime + "'  group by SUBSTRING(CONVERT(varchar(10),SMTime , 23),0,8) order by time ";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "CS");
            return Mydataset.Tables["CS"];
        }
    }

    /// <summary>
    /// 得到天扫码量
    /// </summary>
    /// <param name="startime"></param>
    /// <param name="endtime"></param>
    /// <returns></returns>
    public DataTable getsmcsDay(string startime, string endtime)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "SELECT COUNT(*) as num,SUBSTRING(CONVERT(varchar(15),SMTime , 23),0,11) as time FROM [TJMarketingSystemYin].[dbo].[TJ_375SMinfo] where CompID=130 and SMTime>='" + startime + "' and SMTime<'" + endtime + "'  group by SUBSTRING(CONVERT(varchar(15),SMTime , 23),0,11) order by time ";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "CS");
            return Mydataset.Tables["CS"];
        }
    }

    /// <summary>
    /// 得到扫码地址
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable getsmad(string startime, string endtime)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "SELECT SUBSTRING(SMAddress,0,4) as address ,COUNT(*) as num  FROM [TJMarketingSystemYin].[dbo].[TJ_375SMinfo] where CompID=130 and SMTime>='" + startime + "' and SMTime<'" + endtime + "' group by SUBSTRING(SMAddress,0,4) ";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "CS");
            return Mydataset.Tables["CS"];
        }
    }


    /// <summary>
    /// 得到扫码地址(县级)
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable getsmadxj(string startime, string endtime)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "SELECT SUBSTRING(SMAddress,0,10) as address ,COUNT(*) as num  FROM [TJMarketingSystemYin].[dbo].[TJ_375SMinfo] where CompID=130 and SMTime>='" + startime + "' and SMTime<'" + endtime + "' group by SUBSTRING(SMAddress,0,10) order by num desc ";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "CS");
            return Mydataset.Tables["CS"];
        }
    }



    /// <summary>
    /// 根据条件查询user 表
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable GetPZinfoBystr(string str)
    {
        myConn = GetConnectionstrbbe();
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from BB_PZinfo where " + str + " ", myConn);
        ada.Fill(ds, "TJ_PZinfo");
        return ds.Tables["TJ_PZinfo"];

    }

    /// <summary>
    /// 路酒
    /// </summary>
    /// <param name="labelcode"></param>
    /// <param name="compid"></param>
    /// <returns></returns>

    public DataTable getLabelCodeLJ(string str)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from TB_BuJiangLuJiu  where " + str + "";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            myConn.Close();
            ada.Fill(Mydataset, "ob");
            return Mydataset.Tables["ob"];
        }

    }

    public DataTable getLJBuJiang(string str)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from TB_SmallBuJiang  where " + str + "";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            myConn.Close();
            ada.Fill(Mydataset, "ob");
            return Mydataset.Tables["ob"];
        }

    }

    public DataTable getLabelCodeLJ60000(string str)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select Top 62000 * from TB_BuJiangLuJiu  where " + str + "";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "TB_BuJiangLuJiu");
            myConn.Close();
            return Mydataset.Tables["TB_BuJiangLuJiu"];
        }

    }


    #region 积分

    public DataTable get375SCAD(string str)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select a.ID, a.LabelCode,a.UserID,a.ImgUrl,a.SCTime,b.SMAddress from TJ_375PZinfo a, TJ_375SMinfo b where " + str + " ";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "PZ");


            return Mydataset.Tables["PZ"];
        }
    }
    public DataTable get375SCADCHYJ(string str)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from  TJ_375SMinfo b where " + str + " ";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "PZ");


            return Mydataset.Tables["PZ"];
        }
    }

    /// <summary>
    /// 查询上传记录
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable get375SC(string str)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from TJ_375PZinfo  where " + str + " ";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "PZ");


            return Mydataset.Tables["PZ"];
        }
    }

    /// <summary>
    /// 查询积分记录
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable getJFlab(string str)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from TJ_JFLab  where " + str + " ";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "jflab");
            return Mydataset.Tables["jflab"];
        }
    }

    /// <summary>
    ///获取用户积分信息
    /// </summary>
    /// <param name="UserID">用户ID</param>
    /// <param name="CompID">公司ID</param>
    /// <returns></returns>
    public DataTable GetJiFenInfo(string UserID, string CompID)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string myCmd = "select * from TJ_UserAccumulating where UID=" + UserID + " and COMPID=" + CompID;
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "ob");
            return Mydataset.Tables["ob"];
        }
    }


    /// <summary>
    /// 积分兑换
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable getJFDh(string str)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from TJ_IntegralUseDetail  where " + str + " ";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "JL");
            return Mydataset.Tables["JL"];
        }
    }





    #endregion

    /// <summary>
    /// 查询发货
    /// </summary>
    /// <param name="compid"></param>
    /// <returns></returns>

    public DataTable GetFH(string compid, string type)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {


            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            ds = new DataSet();
            ada = new SqlDataAdapter("select  * from TB_FaHuoInfo_" + compid + " where FHTypeID=" + type + " and  FHDate>='" + DateTime.Now + "' and FHDate<'" + DateTime.Now.AddDays(1) + "'  order by FHDate DESC", myConn);
            ada.Fill(ds, "TJ_User");
            return ds.Tables["TJ_User"];
        }

    }



    /// <summary>
    /// 删除FHinfo
    /// </summary>
    /// <param name="AgentID"></param>
    /// <returns></returns>
    public int DLFH()
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            SqlCommand myCmd = new SqlCommand("Delete from TB_FaHuoInfo ", myConn);

            int c = myCmd.ExecuteNonQuery();
            myCmd.Dispose();
            myConn.Close();
            return c;
        }

    }


    /// <summary>
    /// 插入代理商发货库存数据
    /// </summary>
    /// <param name="LabelCodeOld"></param>
    /// <param name="LabelCodeNew"></param>
    /// <param name="compid"></param>
    /// <param name="Remarks"></param>
    public void InserFH(string compid, string star, string end)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string sql = "";

            sql = "insert into TB_FaHuoInfo select * from TB_FaHuoInfo_" + compid + " where FHDate>='" + star + "' and FHDate<'" + Convert.ToDateTime(end).AddDays(1) + "'";
            SqlCommand cmd = new SqlCommand(sql, myConn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            myConn.Close();
            return;
        }
    }

    #region  替换标签

    /// <summary>
    /// 根据条件找到替换数据
    /// </summary>
    /// <param name="cxstr"></param>
    /// <returns></returns>

    public DataTable GetBQLabelTH(string cxstr)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            ds = new DataSet();
            ada = new SqlDataAdapter("select * from TB_BQLabelTH where " + cxstr + " ", myConn);
            ada.Fill(ds, "TB_BQLabelTH");
            return ds.Tables["TB_BQLabelTH"];
        }

    }


    /// <summary>
    /// 插入替换数据
    /// </summary>
    /// <param name="LabelCodeOld"></param>
    /// <param name="LabelCodeNew"></param>
    /// <param name="compid"></param>
    /// <param name="Remarks"></param>
    public void InserBQLabelTH(string LabelCodeOld, string LabelCodeNew, string Flag, string compid, string Remarks)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string sql = "";

            sql = "insert into  TB_BQLabelTH(LabelCodeOld,LabelCodeNew,Flag,CompID,Remarks) values('" + LabelCodeOld + "','" + LabelCodeNew + "','" + Flag + "'," + compid + ",'" + Remarks + "')";

            SqlCommand cmd = new SqlCommand(sql, myConn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            myConn.Close();
            return;
        }
    }


    /// <summary>
    /// 删除替换数据
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public int DeleteBQLabelTH(string ID)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            SqlCommand myCmd = new SqlCommand("Delete TB_BQLabelTH where ID=" + ID, myConn);

            int c = myCmd.ExecuteNonQuery();
            myConn.Close();
            return c;
        }

    }


    /// <summary>
    /// 更新替换数据
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="LabelCodeOld"></param>
    /// <param name="LabelCodeNew"></param>
    /// <param name="Remarks"></param>
    /// <returns></returns>
    public int updateBQLabelTH(string ID, string LabelCodeOld, string LabelCodeNew, string Remarks)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            SqlCommand myCmd = new SqlCommand("update TB_BQLabelTH set LabelCodeOld='" + LabelCodeOld + "',LabelCodeNew='" + LabelCodeNew + "',Remarks='" + Remarks + "'  where ID=" + ID, myConn);

            int c = myCmd.ExecuteNonQuery();
            myConn.Close();
            return c;
        }

    }


    #endregion


    #region 喵上 农事信息


    /// <summary>
    /// 查询农事信息
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable GetMsNsInfoBystr(string str)
    {
        myConn = GetConnection(_showmode);
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TJ_MSnsInfo where " + str + " ", myConn);
        ada.Fill(ds, "TJ_MSnsInfo");
        return ds.Tables["TJ_MSnsInfo"];
    }


    /// <summary>
    ///     插入农事
    /// </summary>
    /// <param name="FHpici"></param>
    /// <param name="DTime"></param>
    /// <param name="UseYW"></param>
    /// <param name="UserFL"></param>
    /// <param name="StyleFZ"></param>
    /// <param name="StyleXG"></param>
    /// <param name="CompID"></param>
    /// <param name="Remarke"></param>

    public void InserMSnsInfo(string FHpici, string DTime, string UseYW, string UserFL, string StyleFZ, string StyleXG, string CompID, string Remarke)
    {
        string sql = "";

        sql = "insert into  TJ_MSnsInfo(FHpici,DTime,UseYW,UserFL,StyleFZ,StyleXG,CompID,Remarke) values('" + FHpici + "','" + DTime + "','" + UseYW + "','" + UserFL + "','" + StyleFZ + "','" + StyleXG + "','" + CompID + "','" + Remarke + "')";
        myConn = GetConnection(_showmode);
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }
        SqlCommand cmd = new SqlCommand(sql, myConn);
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        myConn.Close();
        return;


    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public int DeleteMSnsInfo(string str)
    {
        myConn = GetConnection(_showmode);
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }

        SqlCommand myCmd = new SqlCommand("Delete TJ_MSnsInfo where " + str, myConn);

        int c = myCmd.ExecuteNonQuery();
        myConn.Close();
        return c;


    }

    /// <summary>
    /// 
    /// 更新农事
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="FHpici"></param>
    /// <param name="DTime"></param>
    /// <param name="UseYW"></param>
    /// <param name="UserFL"></param>
    /// <param name="StyleFZ"></param>
    /// <param name="StyleXG"></param>
    /// <param name="CompID"></param>
    /// <param name="Remarke"></param>
    /// <returns></returns>

    public int updateMSnsInfo(string ID, string FHpici, string DTime, string UseYW, string UserFL, string StyleFZ, string StyleXG, string CompID, string Remarke)
    {
        myConn = GetConnection(_showmode);
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }

        SqlCommand myCmd = new SqlCommand("update TJ_MSnsInfo set FHpici='" + FHpici + "',DTime='" + DTime + "',UseYW='" + UseYW + "',UserFL='" + UserFL + "' ,StyleFZ='" + StyleFZ + "' ,StyleXG='" + StyleXG + "' ,CompID='" + CompID + "',Remarke='" + Remarke + "'   where ID=" + ID, myConn);

        int c = myCmd.ExecuteNonQuery();
        myConn.Close();
        return c;


    }

    #endregion

    /// <summary>
    /// 通过序号找到 确定的就、基础数据表
    /// </summary>
    /// <param name="startlabelcodestring"></param>
    /// <returns></returns>

    public string gettableinfo(string startlabelcodestring)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string returnstring = "";
            ada = new SqlDataAdapter("select * from TB_LabelCodeInfo where startfournum='" + Convert.ToInt16(startlabelcodestring.Substring(0, 4)) + "'", myConn);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            string startvalue = "";
            string endvalue = "";
            foreach (DataRow dr in dt.Rows)
            {
                startvalue = dr["startvalue"].ToString().Trim();
                endvalue = dr["endvalue"].ToString().Trim();
                if (!startvalue.Equals("") && !endvalue.Equals(""))
                {
                    if (((Convert.ToInt64(startlabelcodestring) >= Convert.ToInt64(startvalue)) && (Convert.ToInt64(startlabelcodestring) <= Convert.ToInt64(endvalue))))
                    {
                        returnstring += dr["tablenameinfo"].ToString().Trim() + ",";
                    }
                }
            }

            dt.Dispose();
            ada.Dispose();
            return returnstring;
        }
    }


    /// <summary>
    /// 根据条件查询user 表
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable GetUserBystr(string str)
    {
        myConn = GetConnection(_showmode);
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TJ_User where " + str + " ", myConn);
        ada.Fill(ds, "TJ_User");
        return ds.Tables["TJ_User"];

    }


    /// <summary>
    /// 根据条件查询地址
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>

    public DataTable GetAddressBystr(string str)
    {
        myConn = GetConnection(_showmode);
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TJ_Address where " + str + " ", myConn);
        ada.Fill(ds, "TJ_Address");
        return ds.Tables["TJ_Address"];

    }



    /// <summary>
    ///   根据公司ID，得到杜康微信红包，卡券领取记录(酒鬼领奖 )
    /// </summary>
    /// <returns></returns>


    public DataTable GetDKWXJPinfo(string cxstr)
    {
        myConn = GetConnection(_showmode);
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TJ_DKWXJPinfo where " + cxstr + " ", myConn);
        ada.Fill(ds, "DKWXJPinfo");
        return ds.Tables["DKWXJPinfo"];
    }


    /// <summary>
    /// 更新兑奖表
    /// </summary>
    /// <param name="str"></param>
    /// <param name="UserID"></param>
    public void updateGetDKWXJPinfo(string str, string UserID)
    {
        myConn = GetConnection(_showmode);
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }
        ds = new DataSet();
        SqlCommand myCmd = new SqlCommand(" update TJ_DKWXJPinfo set LJflag=1 , DHUserID=" + UserID + " where " + str + " ", myConn);

        myCmd.ExecuteNonQuery();
        myCmd.Dispose();
        myConn.Close();

    }

    /// <summary>
    /// 根据标签序号，判定是否领奖
    /// </summary>
    /// <param name="BoxLabel"></param>
    /// <returns></returns>

    public DataTable GetDKWXJPinfoByBox(string BoxLabel)
    {
        myConn = GetConnection(_showmode);
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TJ_DKWXJPinfo where BoxLabel='" + BoxLabel + "'", myConn);
        ada.Fill(ds, "DKWXJPinfo");
        return ds.Tables["DKWXJPinfo"];

    }

    public DataTable GetFhPiciandFhKey(string tablename, string BoxLabelString, string CompID)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from " + tablename + "_FH where BoxLabel01='" + BoxLabelString + "'and FHType=3 and CompID=" + CompID + "";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "ob");
            ada.Dispose();
            myConn.Close();
            Mydataset.Dispose();
            return Mydataset.Tables["ob"];
        }
    }



    //查询是否微信扫描

    public DataTable GetSMstyle(string startime, string endtime, bool wx)
    {
        using (var myConn = GetConnectionSM)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            //string myCmd;
            //if (wx)
            //{
            string myCmd = "select * from TB_QrCodeInfo where LEFT(labelcode,3)='806'and CreateTime >='" + startime + "'and CreateTime <'" + endtime + "'";

            //}
            //else
            //{
            //     myCmd = "select * from TB_QrCodeInfo where  ExplorerType not like '%micromessenger%' and LEFT(labelcode,3)='806'and CreateTime >='" + startime + "'and CreateTime <='" + endtime + "'";           
            //}

            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "ob");
            ada.Dispose();
            myConn.Close();
            Mydataset.Dispose();
            return Mydataset.Tables["ob"];

        }
    }
    public DataTable Getstrbyjxneirong(string str)
    {
        myConn = GetConnection(_showmode);
        ds = new DataSet();
        ada = new SqlDataAdapter("select JxName,JxContent,CJtime,OrderNum from TJ_JXInfo where " + str + "", myConn);
        ada.Fill(ds, "JXInfo");
        return ds.Tables["JXInfo"];

    }
    /// <summary>
    ///   删除代理商 与主公司之间的联系
    /// </summary>
    /// <param name="AgentID"></param>
    /// <returns></returns>
    public int DeleteAgent(string AgentID)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            SqlCommand myCmd = new SqlCommand("Delete TB_CompAgentInfo where AgentID=" + AgentID, myConn);

            int c = myCmd.ExecuteNonQuery();
            myConn.Close();
            return c;
        }

    }

    /// <summary>
    ///   删除代理商的FH表
    /// </summary>
    /// <param name="CompID"></param>
    /// <returns></returns>
    public int DeleteFhTable(string CompID)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            int cid = Convert.ToInt16(CompID);
            SqlCommand myCmd = new SqlCommand("DROP TABLE [dbo].[TB_FaHuoInfo_" + cid + "]", myConn);

            int c = myCmd.ExecuteNonQuery();
            myConn.Close();
            return c;
        }

    }


    /// <summary>
    /// 创建FH表
    /// </summary>
    /// <param name="CompID"></param>
    /// <returns></returns>
    public int CreateFhTable(string CompID)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            int cid = Convert.ToInt16(CompID);

            string tempstring = "CREATE TABLE [dbo].[TB_FaHuoInfo_" + cid + "](	[FHID] [int] IDENTITY(1,1) NOT NULL,	[FHPiCi] [varchar](30) NULL,	[FHTypeID] [int] NULL,	[FHDate] [datetime] NULL,	[AgentID] [int] NULL,	[FHUserID] [int] NULL,	[TableNameInfo] [varchar](200) NULL,	[FHKey] [varchar](50) NULL,	[ProID] [int] NULL,	[XiangNumber] [int] NULL,	[CompID] [int] NULL,	[STID] [int] NULL,	[Remarks] [varchar](50) NULL, CONSTRAINT [PK_TB_FaHuoInfo_" + cid + "] PRIMARY KEY CLUSTERED (	[FHID] ASC) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";

            SqlCommand myCmd = new SqlCommand(tempstring, myConn);

            int c = myCmd.ExecuteNonQuery();
            myConn.Close();
            return c;
        }
    }

    /// <summary>
    /// 查询FH 表是否存在
    /// </summary>
    /// <param name="CompID"></param>
    /// <returns></returns>
    public DataTable SelectFhTable(string CompID)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            int cid = Convert.ToInt16(CompID);

            string tempstring = " select count(1) from sys.objects where name = 'TB_FaHuoInfo_" + cid + "'";

            SqlCommand myCmd = new SqlCommand(tempstring, myConn);

            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd);
            ada.Fill(Mydataset, "fh");
            ada.Dispose();
            myConn.Close();
            Mydataset.Dispose();
            return Mydataset.Tables["fh"];
        }
    }


    /// <summary>
    /// 检测是否由父级发货
    /// </summary>
    /// <param name="tablename"></param>
    /// <param name="BoxLabelString"></param>
    /// <param name="FromAgentID"></param>
    /// <returns></returns>
    public bool CheckIsPrF(string tablename, string BoxLabelString, string FromAgentID, string ToAgentID)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from " + tablename + "_FH where BoxLabel01='" + BoxLabelString + "'and FHType=3 and FromAgentID=" + FromAgentID + " and ToAgentID=" + ToAgentID + "";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "ob");
            if (Mydataset.Tables["ob"].Rows.Count > 0)
            {
                ada.Dispose();
                Mydataset.Dispose();
                myConn.Close();
                return true;

            }
            else
            {
                ada.Dispose();
                Mydataset.Dispose();
                myConn.Close();
                return false;

            }


            //sqlcmdforcheck.CommandText = sqlstring;
            //sqlcmdforcheck.Connection = sqlconcom;
            //opensqlconn(sqlconcom);
            //SqlDataReader sdr = sqlcmdforcheck.ExecuteReader();
            //if (sdr.Read())
            //{
            //    string tempvalue = sdr["num"].ToString().Trim();

            //    sdr.Close();
            //    if (Convert.ToInt32(tempvalue) > 0)
            //    {
            //        return true;
            //    }
            //    else
            //    {

            //        return false;
            //    }
            //}
            //else
            //{
            //    sdr.Close();
            //    return false;
            //}
        }


    }


    /// <summary>
    /// 检测是否发货
    /// </summary>
    /// <param name="tablename"></param>
    /// <param name="BoxLabelString"></param>
    /// <returns></returns>
    public bool CheckWfhISok(string tablename, string BoxLabelString, string CompID)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string myCmd = "select * from " + tablename + "_FH where BoxLabel01='" + BoxLabelString + "'and FHType=3 and CompID=" + CompID + "";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "ui");
            if (Mydataset.Tables[0].Rows.Count > 0)
            {

                ada.Dispose();
                Mydataset.Dispose();
                myConn.Close();
                return true;

            }
            else
            {
                ada.Dispose();
                Mydataset.Dispose();
                myConn.Close();
                return false;

            }

            //return Mydataset.Tables["ui"];

            //sqlcmdforcheck.CommandText = sqlstring;
            //sqlcmdforcheck.Connection = sqlconcom;
            //opensqlconn(sqlconcom);
            //SqlDataReader sdr = sqlcmdforcheck.ExecuteReader();
            //if (sdr.Read())
            //{
            //    string tempvalue = sdr["num"].ToString().Trim();

            //    sdr.Close();
            //    if (Convert.ToInt32(tempvalue) > 0)
            //    {
            //        return true;
            //    }
            //    else
            //    {

            //        return false;
            //    }
            //}
            //else
            //{
            //    sdr.Close();
            //    return false;
            //}
        }
    }


    /// <summary>
    /// 兑奖点信息
    /// </summary>
    /// <returns></returns>
    public DataTable GetDjPlace()
    {
        myConn = GetConnection(_showmode);
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TJ_DJPlace ", myConn);
        ada.Fill(ds, "DJPlace");
        return ds.Tables["DJPlace"];

    }
    public DataTable GetDjPlaceByStr(string str)
    {
        myConn = GetConnection(_showmode);
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TJ_DJPlace where " + str + "", myConn);
        ada.Fill(ds, "DJPlace");
        return ds.Tables["DJPlace"];

    }

    /// <summary>
    /// 兑奖管理
    /// </summary>
    /// <returns></returns>
    public DataTable GetDjManage()
    {
        myConn = GetConnection(_showmode);
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TJ_DjManage  ", myConn);
        ada.Fill(ds, "DjManage");
        return ds.Tables["DjManage"];

    }
    public DataTable GetDjManageByStr(string str)
    {
        myConn = GetConnection(_showmode);
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TJ_DjManage where  " + str + " order by DjFlag Desc", myConn);
        ada.Fill(ds, "DjManage");
        return ds.Tables["DjManage"];

    }

    /// <summary>
    /// 
    ///奖项信息
    /// </summary>
    /// <returns></returns>
    public DataTable GetJXInfo()
    {
        myConn = GetConnection(_showmode);
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TJ_JXInfo ", myConn);
        ada.Fill(ds, "JXInfo");
        return ds.Tables["JXInfo"];

    }
    public DataTable GetJXInfoByStr(string str)
    {
        myConn = GetConnection(_showmode);
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TJ_JXInfo where " + str + "", myConn);
        ada.Fill(ds, "JXInfo");
        return ds.Tables["JXInfo"];

    }

    public DataTable GetLAIDbyComID(int comid)
    {
        myConn = GetConnection(_showmode);
        ds = new DataSet();
        ada = new SqlDataAdapter("select LAID from TJ_LotteryActivity where CompID=" + comid + "", myConn);
        ada.Fill(ds, "LAID");
        return ds.Tables["LAID"];
    }



    /// <summary>
    /// 奖品情况信息
    /// </summary>
    /// <returns></returns>
    public DataTable GetJPinfo()
    {
        myConn = GetConnection(_showmode);
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TJ_JPinfo ", myConn);
        ada.Fill(ds, "VipManage");
        return ds.Tables["VipManage"];

    }

    public DataTable GetJPinfoByStr(string str)
    {
        myConn = GetConnection(_showmode);
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TJ_JPinfo where  " + str + "", myConn);
        ada.Fill(ds, "JPinfo");
        return ds.Tables["JPinfo"];
    }



    /// <summary>
    /// 会员管理
    /// </summary>
    /// <returns></returns>
    public DataTable GetVipManage()
    {
        myConn = GetConnection(_showmode);
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TJ_VipManage ", myConn);
        ada.Fill(ds, "VipManage");
        return ds.Tables["VipManage"];

    }

    public DataTable GetVipByStr(string str)
    {
        myConn = GetConnection(_showmode);
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TJ_VipManage where " + str + "", myConn);
        ada.Fill(ds, "VipManage");
        return ds.Tables["VipManage"];
    }
    /// <summary>
    /// 获取兑奖管理表
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable GetDuiJiang(string phone, string YZCode)
    {
        myConn = GetConnection(_showmode);
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TJ_DjManage  where ZjPhone='" + phone + "'and ZjyzCode= '" + YZCode + "'", myConn);
        ada.Fill(ds, "DJManage");
        return ds.Tables["DJManage"];
    }
    /// <summary>
    /// 领奖确认
    /// </summary>
    /// <param name="ZjPhone"></param>
    /// <param name="ZjyzCode"></param>
    /// <returns></returns>
    public int updateDjFlag(string ZjPhone, string ZjyzCode)
    {

        myConn = GetConnection(_showmode);
        myCmd = new SqlCommand("update TJ_DjManage set DjFlag=1,LjTime='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where ZjPhone=@ZjPhone  and ZjyzCode= @ZjyzCode and DjFlag=0", myConn);
        myCmd.Parameters.Add(new SqlParameter("@ZjPhone", SqlDbType.VarChar));
        myCmd.Parameters["@ZjPhone"].Value = ZjPhone;
        myCmd.Parameters.Add(new SqlParameter("@ZjyzCode ", SqlDbType.VarChar));
        myCmd.Parameters["@ZjyzCode "].Value = ZjyzCode;
        myConn.Open();
        int count = myCmd.ExecuteNonQuery();
        myConn.Close();
        return count;


    }





    /// <summary>
    /// 显示是否有重复的号码
    /// </summary>
    /// <param name="ZjPhone"></param>
    /// <param name="ZjYzCode"></param>
    /// <returns></returns>
    public SqlDataReader DJSeleManager(string ZjPhone, string ZjYzCode)
    {
        myConn = GetConnection(_showmode);
        SqlCommand myCmd = new SqlCommand("select * from TJ_DjManage where ZjPhone=@ZjPhone and ZjYzCode=@ZjYzCode ", myConn);
        myCmd.Parameters.Add(new SqlParameter("@ZjPhone", SqlDbType.VarChar));
        myCmd.Parameters["@ZjPhone"].Value = ZjPhone;
        myCmd.Parameters.Add(new SqlParameter("@ZjYzCode", SqlDbType.VarChar));
        myCmd.Parameters["@ZjYzCode"].Value = ZjYzCode;
        myConn.Open();
        SqlDataReader sdr = myCmd.ExecuteReader(CommandBehavior.CloseConnection);
        return sdr;
    }

    public SqlDataReader DJselectPlace(string zhanghao)
    {
        myConn = GetConnection(_showmode);
        SqlCommand myCmd = new SqlCommand("select * from TJ_DJPlace where DjZH=@zhanghao ", myConn);
        myCmd.Parameters.Add(new SqlParameter("@zhanghao", SqlDbType.VarChar));
        myCmd.Parameters["@zhanghao"].Value = zhanghao;
        myConn.Open();
        SqlDataReader sdr = myCmd.ExecuteReader(CommandBehavior.CloseConnection);
        return sdr;
    }

    public int UpdateDJManager(string ZjPhone, string ZjYzCode, string JxName, string CpXS, string CpCX, string DjdName, DateTime DhTime, string DjFlag, DateTime LjTime)
    {
        myConn = GetConnection(_showmode);
        SqlCommand myCmd = new SqlCommand("Update TJ_DjManage set CpXS=@CpXS,JxName=@JxName,CpCX=@CpCX,DjdName=@DjdName,DhTime=@DhTime,DjFlag=@DjFlag,LjTime=@LjTime where ZjPhone=@ZjPhone and ZjYzCode=@ZjYzCode  ", myConn);
        myCmd.Parameters.Add(new SqlParameter("@CpXS ", SqlDbType.VarChar));
        myCmd.Parameters["@CpXS "].Value = CpXS;
        myCmd.Parameters.Add(new SqlParameter("@ZjYzCode ", SqlDbType.VarChar));
        myCmd.Parameters["@ZjYzCode "].Value = ZjYzCode;
        myCmd.Parameters.Add(new SqlParameter("@ZjPhone ", SqlDbType.VarChar));
        myCmd.Parameters["@ZjPhone "].Value = ZjPhone;
        myCmd.Parameters.Add(new SqlParameter("@JxName ", SqlDbType.VarChar));
        myCmd.Parameters["@JxName "].Value = JxName;
        myCmd.Parameters.Add(new SqlParameter("@CpCX", SqlDbType.VarChar));
        myCmd.Parameters["@CpCX"].Value = CpCX;
        myCmd.Parameters.Add(new SqlParameter("@DjdName", SqlDbType.VarChar));
        myCmd.Parameters["@DjdName"].Value = DjdName;
        myCmd.Parameters.Add(new SqlParameter("@DhTime", SqlDbType.DateTime));
        myCmd.Parameters["@DhTime"].Value = DhTime;
        myCmd.Parameters.Add(new SqlParameter("@DjFlag", SqlDbType.VarChar));
        myCmd.Parameters["@DjFlag"].Value = DjFlag;
        myCmd.Parameters.Add(new SqlParameter("@LjTime", SqlDbType.DateTime));
        myCmd.Parameters["@LjTime"].Value = LjTime;
        myConn.Open();
        int count = myCmd.ExecuteNonQuery();
        myConn.Close();
        return count;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="DJplace"></param>
    /// <param name="JxName"></param>
    /// <returns></returns>
    public SqlDataReader DJselectJPinfo(string DJplace, string JxName)
    {
        myConn = GetConnection(_showmode);
        SqlCommand myCmd = new SqlCommand("select * from TJ_JPinfo where DjdName=@DJplace and  JpName=@JxName ", myConn);
        myCmd.Parameters.Add(new SqlParameter("@DJplace", SqlDbType.VarChar));
        myCmd.Parameters["@DJplace"].Value = DJplace;
        myCmd.Parameters.Add(new SqlParameter("@JxName", SqlDbType.VarChar));
        myCmd.Parameters["@JxName"].Value = JxName;
        myConn.Open();
        SqlDataReader sdr = myCmd.ExecuteReader(CommandBehavior.CloseConnection);
        return sdr;
    }
    /// <summary>
    /// 奖品情况信息表
    /// </summary>
    /// <param name="DJplace"></param>
    /// <param name="s"></param>
    /// <param name="jxname"></param>
    /// <param name="dhcount"></param>
    /// <returns></returns>
    public int UpdateJPinfo(string DJplace, int s, string jxname, int dhcount)
    {
        myConn = GetConnection(_showmode);
        if (s == 2)
        {
            SqlCommand myCmd = new SqlCommand("Update TJ_JPinfo set DHCount=DHCount+1,SYCount=PFCount-@dhcount-1 where  DjdName=@DJplace   ", myConn);
            myCmd.Parameters.Add(new SqlParameter("@dhcount ", SqlDbType.Int));
            myCmd.Parameters["@dhcount "].Value = dhcount;
            myCmd.Parameters.Add(new SqlParameter("@DJplace ", SqlDbType.VarChar));
            myCmd.Parameters["@DJplace "].Value = DJplace;
            myConn.Open();
            int count = myCmd.ExecuteNonQuery();
            myConn.Close();
            return count;
        }
        else
        {
            SqlCommand myCmd = new SqlCommand("Update TJ_JPinfo set HXCount=HXCount+1,SYCount=PFCount-@dhcount-1 ", myConn);
            myCmd.Parameters.Add(new SqlParameter("@dhcount ", SqlDbType.Int));
            myCmd.Parameters["@dhcount "].Value = dhcount;
            myCmd.Parameters.Add(new SqlParameter("@DJplace ", SqlDbType.VarChar));
            myCmd.Parameters["@DJplace "].Value = DJplace;
            myConn.Open();
            int count = myCmd.ExecuteNonQuery();
            myConn.Close();
            return count;
        }




    }
    public bool WriteSysTxt(string str)
    {
        str = DateTime.Now.ToString("HH:mm:ss") + " " + str;
        try
        {
            FileStream fs = new FileStream((@"~/SysLog/log" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt"), FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入  
            sw.WriteLine(str);
            //清空缓冲区  
            sw.Flush();
            //关闭流  
            sw.Close();
            fs.Close();
        }
        catch (Exception)
        {
            return false;
        }
        return true;



    }

    #region 喵上餐厅的类库部分
    /// <summary>
    /// 根据传入的参数返回相应的结果。
    /// </summary>
    /// <param name="labelcode"></param>
    /// <returns></returns>
    public DataTable GetListsByFilterStringwl(string FilterString)
    {

        myConn = GetConnection(_showmode);
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }
        string tempstring = "Select * From TJ_MSjbInfo Where " + FilterString;

        SqlCommand myCmd = new SqlCommand(tempstring, myConn);

        Mydataset = new DataSet();
        ada = new SqlDataAdapter(myCmd);
        ada.Fill(Mydataset, "fh");
        ada.Dispose();
        myConn.Close();
        Mydataset.Dispose();
        return Mydataset.Tables["fh"];


    }
    /// <summary>
    /// 返回所有信息。
    /// </summary>
    /// <param name="labelcode"></param>
    /// <returns></returns>
    public DataTable GetLists()
    {

        myConn = GetConnection(_showmode);
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }
        string tempstring = "Select * From TJ_MSjbInfo ";

        SqlCommand myCmd = new SqlCommand(tempstring, myConn);

        Mydataset = new DataSet();
        ada = new SqlDataAdapter(myCmd);
        ada.Fill(Mydataset, "fh");
        ada.Dispose();
        myConn.Close();
        Mydataset.Dispose();
        return Mydataset.Tables["fh"];


    }


    public void Delete(int ID)
    {
        string DeleteSqlstr = "DELETE FROM TJ_MSjbInfo Where ID=@ID";

        myConn = GetConnection(_showmode);
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }
        SqlCommand myCmd = new SqlCommand(DeleteSqlstr, myConn);

        myCmd.Parameters.Add(new SqlParameter("@ID ", SqlDbType.Int));
        myCmd.Parameters["@ID "].Value = ID;
        myCmd.ExecuteNonQuery();
        myConn.Close();
    }
    public object insert(string FHPC, string SHNum, string HZSName, string NHName, string SSQuYu, string ChanPinPiCi, string ChanPinDengJi, string ZhiJianYuan, string LianXiRen, string Phone, string ChanPinShuoMing)
    {

        string InsertSqlstr = "INSERT INTO TJ_MSjbInfo(FHPC,SHNum,HZSName,NHName,SSQuYu,ChanPinPiCi,ChanPinDengJi,ZhiJianYuan,LianXiRen,Phone,ChanPinShuoMing) VALUES (@FHPC,@SHNum,@HZSName,@NHName,@SSQuYu,@ChanPinPiCi,@ChanPinDengJi,@ZhiJianYuan,@LianXiRen,@Phone,@ChanPinShuoMing) select @@identity";
        myConn = GetConnection(_showmode);
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }
        SqlCommand myCmd = new SqlCommand(InsertSqlstr, myConn);
        myCmd.Parameters.Add(new SqlParameter("@FHPC ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@SHNum ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@HZSName ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@NHName ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@SSQuYu ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@ChanPinPiCi ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@ChanPinDengJi ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@ZhiJianYuan ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@LianXiRen ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@Phone ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@ChanPinShuoMing ", SqlDbType.VarChar));

        myCmd.Parameters["@FHPC "].Value = FHPC;
        myCmd.Parameters["@SHNum "].Value = SHNum;
        myCmd.Parameters["@HZSName "].Value = HZSName;
        myCmd.Parameters["@NHName "].Value = NHName;
        myCmd.Parameters["@SSQuYu "].Value = SSQuYu;
        myCmd.Parameters["@ChanPinPiCi "].Value = ChanPinPiCi;
        myCmd.Parameters["@ChanPinDengJi "].Value = ChanPinDengJi;
        myCmd.Parameters["@ZhiJianYuan "].Value = ZhiJianYuan;
        myCmd.Parameters["@LianXiRen "].Value = LianXiRen;
        myCmd.Parameters["@Phone "].Value = Phone;
        myCmd.Parameters["@ChanPinShuoMing "].Value = ChanPinShuoMing;



        int count = myCmd.ExecuteNonQuery();
        myConn.Close();
        return count;

    }
    /// <summary>
    /// 修改一条记录
    ///</summary>
    public void Modify(string FHPC, string SHNum, string HZSName, string NHName, string SSQuYu, string ChanPinPiCi, string ChanPinDengJi, string ZhiJianYuan, string LianXiRen, string Phone, string ChanPinShuoMing, int id)
    {

        string ModifySqlstr = "UPDATE TJ_MSjbInfo SET FHPC=@FHPC,SHNum=@SHNum,HZSName=@HZSName,NHName=@NHName,SSQuYu=@SSQuYu,ChanPinPiCi=@ChanPinPiCi,ChanPinDengJi=@ChanPinDengJi,ZhiJianYuan=@ZhiJianYuan,LianXiRen=@LianXiRen,Phone=@Phone,ChanPinShuoMing=@ChanPinShuoMing where ID=@ID";
        myConn = GetConnection(_showmode);
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }
        SqlCommand myCmd = new SqlCommand(ModifySqlstr, myConn);
        myCmd.Parameters.Add(new SqlParameter("@ID ", SqlDbType.Int));
        myCmd.Parameters.Add(new SqlParameter("@FHPC ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@SHNum ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@HZSName ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@NHName ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@SSQuYu ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@ChanPinPiCi ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@ChanPinDengJi ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@ZhiJianYuan ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@LianXiRen ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@Phone ", SqlDbType.VarChar));
        myCmd.Parameters.Add(new SqlParameter("@ChanPinShuoMing ", SqlDbType.VarChar));
        myCmd.Parameters["@ID "].Value = id;
        myCmd.Parameters["@FHPC "].Value = FHPC;
        myCmd.Parameters["@SHNum "].Value = SHNum;
        myCmd.Parameters["@HZSName "].Value = HZSName;
        myCmd.Parameters["@NHName "].Value = NHName;
        myCmd.Parameters["@SSQuYu "].Value = SSQuYu;
        myCmd.Parameters["@ChanPinPiCi "].Value = ChanPinPiCi;
        myCmd.Parameters["@ChanPinDengJi "].Value = ChanPinDengJi;
        myCmd.Parameters["@ZhiJianYuan "].Value = ZhiJianYuan;
        myCmd.Parameters["@LianXiRen "].Value = LianXiRen;
        myCmd.Parameters["@Phone "].Value = Phone;
        myCmd.Parameters["@ChanPinShuoMing "].Value = ChanPinShuoMing;


        int count = myCmd.ExecuteNonQuery();
        myConn.Close();

    }

    private static SqlParameter[] GetOrderParameters()
    {
        string ModifySqlstr = "UPDATE TJ_MSjbInfo SET FHPC=@FHPC,SHNum=@SHNum,HZSName=@HZSName,NHName=@NHName,SSQuYu=@SSQuYu,ChanPinPiCi=@ChanPinPiCi,ChanPinDengJi=@ChanPinDengJi,ZhiJianYuan=@ZhiJianYuan,LianXiRen=@LianXiRen,Phone=@Phone,ChanPinShuoMing=@ChanPinShuoMing where ID=@ID";

        string PARM_FHPC = "@FHPC";
        string PARM_SHNum = "@SHNum";
        string PARM_HZSName = "@HZSName";
        string PARM_NHName = "@NHName";
        string PARM_SSQuYu = "@SSQuYu";
        string PARM_ChanPinPiCi = "@ChanPinPiCi";
        string PARM_ChanPinDengJi = "@ChanPinDengJi";
        string PARM_ZhiJianYuan = "@ZhiJianYuan";
        string PARM_LianXiRen = "@LianXiRen";
        string PARM_Phone = "@Phone";
        string PARM_ChanPinShuoMing = "@ChanPinShuoMing";

        SqlParameter[] parms = SqlHelper.GetCachedParameters(ModifySqlstr);
        if (parms == null)
        {
            parms = new SqlParameter[] {
                new SqlParameter(PARM_FHPC,SqlDbType.VarChar,100),
                new SqlParameter(PARM_SHNum,SqlDbType.VarChar,100),
                new SqlParameter(PARM_HZSName,SqlDbType.VarChar,100),
                new SqlParameter(PARM_NHName,SqlDbType.VarChar,100),
                new SqlParameter(PARM_SSQuYu,SqlDbType.VarChar,100),
                new SqlParameter(PARM_ChanPinPiCi,SqlDbType.VarChar,100),
                new SqlParameter(PARM_ChanPinDengJi,SqlDbType.VarChar,100),
                new SqlParameter(PARM_ZhiJianYuan,SqlDbType.VarChar,100),
                new SqlParameter(PARM_LianXiRen,SqlDbType.VarChar,100),
                new SqlParameter(PARM_Phone,SqlDbType.VarChar,100),
                new SqlParameter(PARM_ChanPinShuoMing,SqlDbType.VarChar,1000) };
            SqlHelper.CacheParameters(ModifySqlstr, parms);
        }
        return parms;
    }

    #endregion

    /// <summary>
    /// 返回执行标准。
    /// </summary>
    /// <param name="compid"></param>
    /// <returns></returns>
    public DataTable returnbiaozhun(string compid)
    {

        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from TB_BiaoZhun where  CompID=" + compid;
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "ob");
            ada.Dispose();
            myConn.Close();
            Mydataset.Dispose();
            return Mydataset.Tables["ob"];

        }


    }
    /// <summary>
    /// 返回原材料
    /// </summary>
    /// <param name="compid"></param>
    /// <returns></returns>
    public DataTable returnyuanliao(string compid)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from TB_Metries where  CompID=" + compid;
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "ob");
            ada.Dispose();
            myConn.Close();
            Mydataset.Dispose();
            return Mydataset.Tables["ob"];

        }



    }
    /// <summary>
    /// 巴黎之花
    /// </summary>
    /// <param name="BoxLabelString"></param>
    /// <param name="CompID"></param>
    /// <returns></returns>
    public DataTable GetFhPiciandFhKeyNOXB(string BoxLabelString, string CompID)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from TB_FaHuoInfo_NOXB_" + CompID + " where BoxLabel01='" + BoxLabelString + "'and FHTypeID=3 and CompID=" + CompID + "";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(Mydataset, "ob");
            ada.Dispose();
            myConn.Close();
            Mydataset.Dispose();
            return Mydataset.Tables["ob"];

        }
    }
    /// <summary>
    /// 金彩山返回对应的标签序号
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable returnlabelcode(string str)
    {
        myConn = GetConnection(_showmode);
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TB_SmalllabelInfo where " + str + " ", myConn);
        ada.Fill(ds, "TJ_small");
        return ds.Tables["TJ_small"];

    }
    /// <summary>
    /// 插入操作日志
    /// </summary>
    /// <param name="LabelCodeOld"></param>
    /// <param name="LabelCodeNew"></param>
    /// <param name="compid"></param>
    /// <param name="Remarks"></param>
    public void InserLOG(string compid, string Uid, string typeid, string ip)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string sql = "";

            sql = "insert into TJ_Log (uid,IPAddress,DoType,DoTime,Compid ) values(" + Uid + ",'" + ip + "'," + typeid + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + compid + ")";
            SqlCommand cmd = new SqlCommand(sql, myConn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            myConn.Close();
            return;
        }
    }

    //金彩山 xzl
    public DataTable getjcsBuJiang(string str)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from TJ_ZJLabelCodesmallInfo  where " + str + "";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            myConn.Close();
            ada.Fill(Mydataset, "ob");
            return Mydataset.Tables["ob"];
        }

    }

    #region 酒鬼酒湘泉
    /// <summary>
    /// 获取兑奖管理表
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable GetDuiJiangJGXQ(string phone, string zjCode)
    {
        myConn = GetConnectionJGXQ();
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TJ_LQJiLu  where Phone='" + phone + "'and ZJMa= '" + zjCode + "'", myConn);
        ada.Fill(ds, "DJManage");
        return ds.Tables["DJManage"];
    }
    /// <summary>
    /// 领奖确认
    /// </summary>
    /// <param name="ZjPhone"></param>
    /// <param name="ZjyzCode"></param>
    /// <returns></returns>
    public int updateDjFlagJG(string ZjPhone, string ZJMa, string DJDian, string DJDianName, string compid)
    {

        myConn = GetConnectionJGXQ();
        myCmd = new SqlCommand("update TJ_LQJiLu set DJDian=@DJDian,DJDianName=@DJDianName,Remarks=@compid,DjFlag=1,DHtime='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where Phone=@Phone  and ZJMa= @ZJMa and DJflag=0", myConn);
        myCmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.VarChar));
        myCmd.Parameters["@Phone"].Value = ZjPhone;
        myCmd.Parameters.Add(new SqlParameter("@ZJMa", SqlDbType.VarChar));
        myCmd.Parameters["@ZJMa"].Value = ZJMa;
        myCmd.Parameters.Add(new SqlParameter("@DJDian", SqlDbType.VarChar));
        myCmd.Parameters["@DJDian"].Value = DJDian;
        myCmd.Parameters.Add(new SqlParameter("@DJDianName", SqlDbType.VarChar));
        myCmd.Parameters["@DJDianName"].Value = DJDianName;
        myCmd.Parameters.Add(new SqlParameter("@compid", SqlDbType.VarChar));
        myCmd.Parameters["@compid"].Value = compid;
        myConn.Open();
        int count = myCmd.ExecuteNonQuery();
        myConn.Close();
        return count;


    }
    public DataTable GetJGXQinfo(string cxstr)
    {
        myConn = GetConnectionJGXQ();
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }
        ds = new DataSet();
        ada = new SqlDataAdapter("select * from TJ_LQJiLu where " + cxstr + " ", myConn);
        ada.Fill(ds, "DKWXJPinfo");
        return ds.Tables["DKWXJPinfo"];
    }
    #endregion
    public DataTable returnload(string compid, string flagstr, string str)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string myCmd = "select " + flagstr + " from TJ_375SMinfo  where Compid= " + compid + str + "";
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(myCmd, myConn);
            myConn.Close();
            ada.Fill(Mydataset, "ob");
            return Mydataset.Tables["ob"];
        }
    }
    /// <summary>
    /// 返回经销商授权产品(ProdID，Products_Name)
    /// </summary>
    /// <param name="compid"></param>
    /// <param name="agentid"></param>
    /// <returns>ProdID，Products_Name</returns>
    public DataTable GetAuthorProductInfo(string compid, string agentid)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string myCmd = string.Empty;
            if (compid.Equals(agentid))
            {
                myCmd = "Select a.Infor_ID as ProdID,a.Products_Name,a.Remarks as pic From TianJianWuLiuWebnew.dbo.TB_Products_Infor a where a.CompID=" + compid;
            }
            else
            {
                myCmd = "Select a.ProdID,(select b.Products_Name from TianJianWuLiuWebnew.dbo.TB_Products_Infor b where a.ProdID=b.Infor_ID) Products_Name,(select b.Remarks from TianJianWuLiuWebnew.dbo.TB_Products_Infor b where a.ProdID=b.Infor_ID) pic From TianJianWuLiuWebnew.dbo.TB_ProductAuthorForAgent a Where AgentID=" + agentid + " and CompID=" + compid;
            }
            dttemp = new DataTable();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(dttemp);
            return dttemp;
        }
    }

    public DataTable GetAuthorProductInfoForJSON(string compid, string agentid)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string myCmd = string.Empty;
            if (compid.Equals(agentid))
            {
                myCmd = "Select a.Infor_ID as pid, a.Infor_ID as pcd,a.Products_Name as pnm From TianJianWuLiuWebnew.dbo.TB_Products_Infor a where a.CompID=" + compid;
            }
            else
            {
                myCmd = "Select a.ProdID as pid,a.ProdID as pcd,(select b.Products_Name from TianJianWuLiuWebnew.dbo.TB_Products_Infor b where a.ProdID=b.Infor_ID)  pnm From TianJianWuLiuWebnew.dbo.TB_ProductAuthorForAgent a Where AgentID=" + agentid + " and CompID=" + compid;
            }
            dttemp = new DataTable();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(dttemp);
            return dttemp;
        }
    }

    public DataTable GetAuthorProductInfoForJsonWithPics(string compid, string agentid)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string myCmd = string.Empty;
            if (compid.Equals(agentid))
            {
                myCmd = "Select a.Infor_ID as pid, a.Infor_ID as pcd,a.Products_Name as pnm,'http://os.china315net.com/Admin/wuliu/'+a.Remarks as pic From TianJianWuLiuWebnew.dbo.TB_Products_Infor a where a.CompID=" + compid;
            }
            else
            {
                myCmd = "Select a.ProdID as pid,a.ProdID as pcd,(select b.Products_Name from TianJianWuLiuWebnew.dbo.TB_Products_Infor b where a.ProdID=b.Infor_ID)  pnm,(select 'http://os.china315net.com/Admin/wuliu/'+b.Remarks from TianJianWuLiuWebnew.dbo.TB_Products_Infor b where a.ProdID=b.Infor_ID)  pic From TianJianWuLiuWebnew.dbo.TB_ProductAuthorForAgent a Where AgentID=" + agentid + " and CompID=" + compid;
            }
            dttemp = new DataTable();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(dttemp);
            return dttemp;
        }
    }

    public DataTable GetAuthorProductInfoForJsonWithPicsForXFXCX(string compid, string agentid)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string myCmd = string.Empty;
            if (compid.Equals(agentid))
            {
                myCmd = "Select a.Infor_ID as pid, a.Infor_ID as pcd,a.Products_Name as pnm,'http://os.china315net.com/Admin/wuliu/'+a.Remarks as pic From TianJianWuLiuWebnew.dbo.TB_Products_Infor a where a.ProdID in(3959,5220,5732,5758) and a.CompID=" + compid;
            }
            else
            {
                myCmd = "Select a.ProdID as pid,a.ProdID as pcd,(select b.Products_Name from TianJianWuLiuWebnew.dbo.TB_Products_Infor b where a.ProdID=b.Infor_ID)  pnm,(select 'http://os.china315net.com/Admin/wuliu/'+b.Remarks from TianJianWuLiuWebnew.dbo.TB_Products_Infor b where a.ProdID=b.Infor_ID)  pic From TianJianWuLiuWebnew.dbo.TB_ProductAuthorForAgent a Where a.ProdID in(3959,5220,5732,5758) and AgentID=" + agentid + " and CompID=" + compid;
            }
            dttemp = new DataTable();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(dttemp);
            return dttemp;
        }
    }

    public int PggisSqlExecute(string sqlstring)
    {
        using (var myconn = GetPGConnctionGIS())
        {
            if (myconn.State == ConnectionState.Closed)
            {
                myconn.Open();
            }
            pgcommd = new NpgsqlCommand(sqlstring, myconn);
            return pgcommd.ExecuteNonQuery();
        }
    }

    #region 酒鬼酒红包统计
    /// <summary>
    /// 查询月红包量（高度柔和，恬柔15）
    /// </summary>
    /// <param name="startime"></param>
    /// <param name="endtime"></param>
    /// <param name="compid"></param>
    /// <param name="pro"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public DataTable GetWXHBnum(string startime, string endtime, string compid, string pro, string type, string datetype)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string cpstr = "";
            string Fldate = "SUBSTRING(CONVERT(varchar(10),LQtime , 23),0,8)";

            if (pro == "恬柔")
            {
                cpstr = "and BoxLabel>='206328571615' and BoxLabel<='206330171604' ";
            }
            if (datetype == "day")
            {
                Fldate = " SUBSTRING(CONVERT(varchar(15), LQtime, 23), 0, 11)";
            }
            string myCmd = "SELECT COUNT(*) as num," + Fldate + " as time FROM TJ_DKWXJPinfo  where CompID=" + compid + " and LQtime>='" + startime + "' and LQtime<'" + endtime + "' and   LEFT(BoxLabel,2)='20' and JPType=" + type + " " + cpstr + "  group by  " + Fldate + " order by time ";
            DataTable dtj = new DataTable();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(dtj);
            ada.Dispose();
            myConn.Close();
            return dtj;
        }
    }
    public DataTable GetWXHBnum_JGXQ(string startime, string endtime, string compid, string pro, string type, string datetype)
    {
        using (var myConn = GetConnectionJGXQ())
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string Fldate = "SUBSTRING(CONVERT(varchar(10),LQDateTime , 23),0,8)";
            if (datetype == "day")
            {
                Fldate = " SUBSTRING(CONVERT(varchar(15), LQDateTime, 23), 0, 11)";
            }
            string myCmd = "SELECT COUNT(*) as num, " + Fldate + " as time FROM  TJ_LQJiLu  where CompID=" + compid + " and LQDateTime>='" + startime + "' and LQDateTime<'" + endtime + "' and JXID=" + type + "   group by  " + Fldate + "  order by time ";
            DataTable dtj = new DataTable();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(dtj);
            ada.Dispose();
            myConn.Close();
            return dtj;
        }
    }
    #endregion

    /// <summary>
    /// 得到扫码地址(2018/8/13)_PYE
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable getsmad_all(string startime, string endtime, string compid, string showmode)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string myCmd = "";
            if (showmode.Equals("1"))
            {
                myCmd = "SELECT SMProc as address ,COUNT(*) as num  FROM TJ_375SMinfo  where SMTime>='" + startime + "' and SMTime<'" + endtime + "' and SMProc is not null group by SMProc ";
            }
            else
            {
                myCmd = "SELECT SMProc as address ,COUNT(*) as num  FROM TJ_375SMinfo  where CompID=" + compid + " and SMTime>='" + startime + "' and SMTime<'" + endtime + "' and SMProc is not null group by SMProc ";
            }
            dttemp = new DataTable();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(dttemp);
            ada.Dispose();
            myConn.Close();
            return dttemp;
        }
    }

    public DataTable GetMyAgentInfo(string compid)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string myCmd = "select a.CompID,a.CompName,a.Agent_Code from TJMarketingSystemYin.dbo.TJ_RegisterCompanys a where a.CompID in(select b.AgentID from TianJianWuLiuWebnew.dbo.TB_CompAgentInfo b where b.CompID=" + compid + ")  order by  a.CompName";
            dttemp = new DataTable();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(dttemp);
            ada.Dispose();
            myConn.Close();
            return dttemp;
        }
    }

    public DataTable GetFaHuoPicInfo(string compid, string agentid, string sdate, string edate)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string myCmd = "select a.FHID,a.FHPiCi from TianJianWuLiuWebnew.dbo.TB_FaHuoInfo_" + compid + " a where a.AgentID=" + agentid + " and a.FHDate between '" + sdate + "' and '" + edate + "'";
            dttemp = new DataTable();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(dttemp);
            ada.Dispose();
            myConn.Close();
            return dttemp;
        }
    }

    public DataTable GetFaHuoInfoByFHID(string compid, string FHID)
    {
        using (var myConn = GetConnectionWL(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string myCmd = "select a.AgentID,a.FHKey,a.TableNameInfo from TianJianWuLiuWebnew.dbo.TB_FaHuoInfo_" + compid + " a where a.FHID=" + FHID;
            dttemp = new DataTable();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(dttemp);
            ada.Dispose();
            myConn.Close();
            return dttemp;
        }
    }

    public DataTable GetFahuoDetailByTableAndKey(string tbname, string fhkey)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string temp = "SELECT a.[ProductName] '产品',a.[BoxLabel01] '箱码',(select b.CompName from TJMarketingSystemYin.dbo.TJ_RegisterCompanys b where b.CompID=a.FromAgentID) '从',(select b.CompName from TJMarketingSystemYin.dbo.TJ_RegisterCompanys b where b.CompID=a.[ToAgentID]) '至',[FHDate] '时间',(select COUNT(*)  from (SELECT DISTINCT c.BoxLabel,c.AcceptAgentID FROM TianJianWuLiuWebnew.dbo.AgentAcceptInfo_2019 c where c.BoxLabel=a.BoxLabel01) as cnt) as cs  FROM [TianJianWuLiuWebnew].[dbo].[" + tbname + "_FH] a where a.FHKey='" + fhkey + "'";
            dttemp = new DataTable();
            ada = new SqlDataAdapter(temp, myConn);
            ada.Fill(dttemp);
            ada.Dispose();
            myConn.Close();
            return dttemp;
        }
    }

    #region 通用
    public string GetSingleData(string cstr, string table, string wstr)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string sqlstr = "select " + cstr + "  from  " + table + "  where  " + wstr;
            myCmd = new SqlCommand(sqlstr, myConn);
            object result = myCmd.ExecuteScalar();
            myCmd.Dispose();
            myConn.Close();
            if (result != null)
            {
                return result.ToString();
            }
            else
            {
                return "no";
            }
        }
    }

    public DataTable GetTableData(string cstr, string table, string wstr)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string myCmd = "select  " + cstr + "  from  " + table + "  where  " + wstr;
            DataTable dtable = new DataTable();
            ada = new SqlDataAdapter(myCmd, myConn);
            ada.Fill(dtable);
            ada.Dispose();
            myConn.Close();
            return dtable;
        }
    }

    public int Update_SingleData(string tabel, string wstr, string cstr)
    {
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string sql = "update  " + tabel + "  set  " + cstr + "  where  " + wstr + " ";
            SqlCommand cmd = new SqlCommand(sql, myConn);
            int c = cmd.ExecuteNonQuery();
            cmd.Dispose();
            myConn.Close();
            return c;
        }
    }

    public int Deletedata(string tabel, string str)
    {
        myConn = GetConnection(_showmode);
        if (myConn.State == ConnectionState.Closed)
        {
            myConn.Open();
        }
        SqlCommand myCmd = new SqlCommand("Delete  " + tabel + "   where " + str, myConn);
        int c = myCmd.ExecuteNonQuery();
        myConn.Close();
        return c;
    }
    #endregion

    private string _returnvl = string.Empty;

    /// <summary>
    /// 返回公司ID,用户ID,是否激活码
    /// </summary>
    /// <param name="swmopenid"></param>
    /// <returns></returns>
    public string GetCompidBySwmOpenid(string swmopenid)
    {
        _returnvl = string.Empty;
        myConn = GetConnection(_showmode);
        ada = new SqlDataAdapter("select CompID,UserID,IsActived from TJ_User where RID=155 and WXNumber='" + swmopenid + "'", myConn);
        dttemp = new DataTable();
        ada.Fill(dttemp);
        if (dttemp.Rows.Count > 0)
        {
            _returnvl = dttemp.Rows[0]["CompID"] + "|" + dttemp.Rows[0]["UserID"] + "|" + dttemp.Rows[0]["IsActived"];
        }
        dttemp.Dispose();
        ada.Dispose();
        myConn.Close();
        return _returnvl;
    }

    public Boolean IsHaveModle(string compid)
    {
        bool re = false;
        myConn = GetConnection(_showmode);
        ada = new SqlDataAdapter("select COUNT(id) cnt from TJ_Comp_Roles where compid=" + compid + " and isactive=1", myConn);
        dttemp = new DataTable();
        ada.Fill(dttemp);
        if (dttemp.Rows.Count > 0)
        {
            if (int.Parse(dttemp.Rows[0]["cnt"].ToString()) > 0)
            {
                re = true;
            }
            else
            {
                re = false;
            }
        }
        else
        {
            re = false;
        }
        dttemp.Dispose();
        ada.Dispose();
        return re;
    }

    private string tempsql = "";

    /// <summary>
    /// 记录三维码激活功能选择
    /// </summary>
    /// <param name="pid">批次ID</param>
    /// <param name="prodid">产品ID</param>
    /// <param name="rpidstr">没有功能设为空，有多个功能包，ID之间用”,“隔开</param>
    /// <param name="number">数量</param>
    /// <param name="jiage">价格</param>
    /// <param name="active">1或0</param>
    public void RecordActiveInfo(string pid, string prodid, string rpidstr, string number, string jiage, string active, string mchbillno, string compid)
    {
        tempsql = "insert into TJ_SWM_PID_Active(pid,prodid,rpidstring,isactive,number,pricevl,mchbillno,compid) values (" + pid +
                         "," + prodid + ",'" + rpidstr + "'," + active + "," + number + "," + jiage + ",'" + mchbillno + "'," + compid + ")";
        myConn = GetConnection(_showmode);
        myCmd = new SqlCommand(tempsql, myConn);
        if (myConn.State != ConnectionState.Open)
        {
            myConn.Open();
        }
        myCmd.ExecuteNonQuery();
    }

    public void AddModuleInfo(string mchbillno, string compid)
    {
        using (myConn = GetConnection(_showmode))
        {
            ada = new SqlDataAdapter("select pid,rpidstring from TJ_SWM_PID_Active where mchbillno='" + mchbillno + "' and compid=" + compid, myConn);
            dttemp = new DataTable();
            ada.Fill(dttemp);
            if (dttemp.Rows.Count > 0)
            {
                string rpidstr = dttemp.Rows[0]["rpidstring"].ToString();
                string pid = dttemp.Rows[0]["pid"].ToString();
                if (rpidstr.Length > 0)
                {
                    string[] rpidarray = rpidstr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var s in rpidarray)
                    {
                        if (s.Length > 0)
                        {
                            if (tempsql.Length.Equals(0))
                            {
                                tempsql = "insert into TJ_SWM_PID_RPID(pid,rpid) values(" + pid + "," + s + ")";
                            }
                            else
                            {
                                tempsql += ";insert into TJ_SWM_PID_RPID(pid,rpid) values(" + pid + "," + s + ")";
                            }
                        }
                    }
                    if (tempsql.Length > 0)
                    {
                        tempsql += ";update TJ_SWM_PID_Active set isactive=1 where mchbillno='" + mchbillno + "'";
                    }
                    else
                    {
                        tempsql = "update TJ_SWM_PID_Active set isactive=1 where mchbillno='" + mchbillno + "'";
                    }
                    myCmd = new SqlCommand(tempsql, myConn);
                    if (myConn.State != ConnectionState.Open)
                    {
                        myConn.Open();
                    }
                    myCmd.ExecuteNonQuery();
                    myConn.Close();
                }
            }
            dttemp.Dispose();
            ada.Dispose();
            myCmd.Dispose();
        }
    }

    private string _basemoduleidstring = "";
    public string GetPageInfoByPid(string pid)
    {
        _basemoduleidstring = GetSWMBaseSiteID();
        tempsql = "select (stuff((select '' + ridstring  from TJ_Role_Package where id in (SELECT rpid FROM TJ_SWM_PID_RPID where pid=" + pid + ") for xml path('')),1,1,''))";
        myConn = GetConnection(_showmode);
        ada = new SqlDataAdapter(tempsql, myConn);
        dttemp = new DataTable();
        ada.Fill(dttemp);
        if (dttemp.Rows.Count > 0)
        {
            string temp = dttemp.Rows[0][0].ToString();
            if (temp.Length > 0)
            {
                temp += "," + _basemoduleidstring;
            }
            else
            {
                temp = _basemoduleidstring;
            }
            temp = temp.StartsWith(",") ? temp.Substring(1) : temp;
            tempsql = "select PageName pg,LinkPath lk,LogoName lg,Remarks ky,SiteID id from TJ_SiteMap where SysTypeID=78 and ParentID=38 and SiteID in (" + temp + ") order by ShowOrder";
            ada = new SqlDataAdapter(tempsql, myConn);
            dttemp = new DataTable();
            ada.Fill(dttemp);
            if (dttemp.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dttemp);
            }
        }
        dttemp.Dispose();
        myConn.Close();
        ada.Dispose();
        return "[]";
    }
    public string GetCustomerPageInfoByPid(string pid, string compid)
    {
        _basemoduleidstring = GetSWMBaseSiteID();
        tempsql = "select (stuff((select '' + ridstring  from TJ_Role_Package where id in (SELECT rpid FROM TJ_SWM_PID_RPID where pid=" + pid + ") for xml path('')),1,1,''))";
        myConn = GetConnection(_showmode);
        ada = new SqlDataAdapter(tempsql, myConn);
        dttemp = new DataTable();
        ada.Fill(dttemp);
        if (dttemp.Rows.Count > 0)
        {
            string temp = dttemp.Rows[0][0].ToString();
            if (temp.Length > 0)
            {
                temp += "," + _basemoduleidstring;
            }
            else
            {
                temp = _basemoduleidstring;
            }
            temp = temp.StartsWith(",") ? temp.Substring(1) : temp;
            tempsql = "select mdnm pg,lk,logourl lg,ky,mdid id from TJ_SWM_Comp_ModulesConfig where compid=" + compid + " and isshow=1 and mdid in (" + temp + ") order by showorder";
            ada = new SqlDataAdapter(tempsql, myConn);
            dttemp = new DataTable();
            ada.Fill(dttemp);
            if (dttemp.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dttemp);
            }
        }
        dttemp.Dispose();
        myConn.Close();
        ada.Dispose();
        return "[]";
    }

    public string GetSWMCustomerPageInfoByPid(string pid, string compid)
    {
        _basemoduleidstring = GetSWMBaseSiteID();
        tempsql = "select (stuff((select '' + ridstring  from TJ_Role_Package where id in (SELECT rpid FROM TJ_SWM_PID_RPID where pid=" + pid + ") for xml path('')),1,1,''))";
        myConn = GetConnection(_showmode);
        ada = new SqlDataAdapter(tempsql, myConn);
        dttemp = new DataTable();
        ada.Fill(dttemp);
        if (dttemp.Rows.Count > 0)
        {
            string temp = dttemp.Rows[0][0].ToString();
            if (temp.Length > 0)
            {
                temp += "," + _basemoduleidstring;
            }
            else
            {
                temp = _basemoduleidstring;
            }
            temp = temp.StartsWith(",") ? temp.Substring(1) : temp;
            tempsql = "select mdnm pg,lk,logourl lg,ky,mdid id from TJ_SWM_Comp_ModulesConfig where compid=" + compid + " and isshow=1 and mdid in (" + temp + ") order by showorder";
            ada = new SqlDataAdapter(tempsql, myConn);
            dttemp = new DataTable();
            ada.Fill(dttemp);
            if (dttemp.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dttemp);
            }
        }
        dttemp.Dispose();
        myConn.Close();
        ada.Dispose();
        return "[]";
    }

    private string GetSWMBaseSiteID()
    {
        tempsql = "select ridstring from TJ_Role_Package where id=1";
        dttemp = new DataTable();
        myConn = GetConnection(_showmode);
        ada = new SqlDataAdapter(tempsql, myConn);
        ada.Fill(dttemp);
        string tempvalue = "";
        if (dttemp.Rows.Count > 0)
        {
            tempvalue = dttemp.Rows[0][0].ToString();
        }
        dttemp.Dispose();
        ada.Dispose();
        return tempvalue.StartsWith(",") ? tempvalue.Substring(1) : tempvalue;
    }
    /// <summary>
    /// 更新活动范围限定数量
    /// </summary>
    /// <param name="acid">活动ID</param>
    /// <param name="type">对象类型（pd:产品；ag:经销商；tm:终端店）</param>
    public void Updateactivitynum(string acid, string type)
    {
        myConn = GetConnection(_showmode);
        switch (type.ToLower())
        {
            case "pd":
                ada = new SqlDataAdapter("select count(id) from TJ_Activity_Product where acid=" + acid, myConn);
                dttemp = new DataTable();
                ada.Fill(dttemp);
                if (dttemp.Rows.Count > 0)
                {
                    myCmd = new SqlCommand("update TJ_Activity set prodnum=" + dttemp.Rows[0][0] + " where id=" + acid, myConn);
                    if (myConn.State != ConnectionState.Open)
                    {
                        myConn.Open();
                    }
                    myCmd.ExecuteNonQuery();
                }
                break;
            case "ag":
                ada = new SqlDataAdapter("select count(id) from TJ_Activity_Agent where acid=" + acid, myConn);
                dttemp = new DataTable();
                ada.Fill(dttemp);
                if (dttemp.Rows.Count > 0)
                {
                    myCmd = new SqlCommand("update TJ_Activity set agentnum=" + dttemp.Rows[0][0] + " where id=" + acid, myConn);
                    if (myConn.State != ConnectionState.Open)
                    {
                        myConn.Open();
                    }
                    myCmd.ExecuteNonQuery();
                }
                break;
            case "tm":
                ada = new SqlDataAdapter("select count(id) from TJ_Activity_Terminal where acid=" + acid, myConn);
                dttemp = new DataTable();
                ada.Fill(dttemp);
                if (dttemp.Rows.Count > 0)
                {
                    myCmd = new SqlCommand("update TJ_Activity set terminalnum=" + dttemp.Rows[0][0] + " where id=" + acid, myConn);
                    if (myConn.State != ConnectionState.Open)
                    {
                        myConn.Open();
                    }
                    myCmd.ExecuteNonQuery();
                }
                break;
        }
        myConn.Close();
        dttemp.Dispose();
        ada.Dispose();
        myCmd.Dispose();
    }

    public bool GetModuleIsCustomedByCompID(string compid)
    {
        tempsql = "select CustomerModule from TJ_RegisterCompanys where CompID=" + compid;
        dttemp = new DataTable();
        myConn = GetConnection(_showmode);
        ada = new SqlDataAdapter(tempsql, myConn);
        ada.Fill(dttemp);
        bool tempvalue = false;
        if (dttemp.Rows.Count > 0)
        {
            tempvalue = Convert.ToBoolean(dttemp.Rows[0][0].ToString());
        }
        dttemp.Dispose();
        ada.Dispose();
        return tempvalue;
    }

    public bool Penglog(string str, string logfile)
    {
        str = DateTime.Now.ToString("HH:mm:ss") + " " + str;
        try
        {
            FileStream fs =
                new FileStream(
                    HttpContext.Current.Server.MapPath(@"~/Log/" + logfile + "/log" +
                                                       DateTime.Now.ToString("yyyy-MM-dd") + ".txt"), FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入  
            sw.WriteLine(str);
            //清空缓冲区  
            sw.Flush();
            //关闭流  
            sw.Close();
            fs.Close();
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 三维码批量激活
    /// </summary>
    /// <param name="compid">单位ID</param>
    /// <param name="pid">卷标ID</param>
    /// <param name="activenum">激活数量</param>
    /// <param name="freeday">试用期限(天)</param>
    /// <param name="unitname">单位名称</param>
    /// <returns></returns>
    public string ActivePidAndAuthorAllMoudleInfoFrank(string compid, string pid, int activenum, int freeday, string unitname)
    {
        string sqlstring = "";
        DataTable dttemp = null;
        string prodid = "0";
        using (var myconn = GetConnectionWL(_showmode))
        {
            if (myconn.State != ConnectionState.Open)
            {
                myconn.Open();
            }
            sqlstring = "select top 1 Infor_ID from TB_Products_Infor where CompID=" + compid + " order by Infor_ID";
            ada = new SqlDataAdapter(sqlstring, myconn);
            dttemp = new DataTable();
            ada.Fill(dttemp);
            if (dttemp.Rows.Count > 0)
            {
                prodid = dttemp.Rows[0][0].ToString();
            }
            else
            {
                sqlstring =
                    "insert into TB_Products_Infor(TypeId,PSID,Product_Code,Products_Name,Products_Summary,Products_Price,Products_date,CompID) values(0,0,'0001','产品名称','我的产品介绍',0,'" +
                    DateTime.Now + "'," + compid + ")  select @@identity";
                myCmd = new SqlCommand(sqlstring, myconn);
                prodid = myCmd.ExecuteScalar().ToString();
                myCmd.Dispose();
            }
            dttemp.Dispose();
            ada.Dispose();
            myconn.Close();
        }
        using (var myConn = GetConnection(_showmode))
        {
            if (myConn.State != ConnectionState.Open)
            {
                myConn.Open();
            }
            sqlstring = "select GoodsID from TJ_GoodsInfo  where WLProID=" + prodid;
            myCmd = new SqlCommand(sqlstring, myConn);
            string goodsid = string.Empty;
            Object gid = myCmd.ExecuteScalar();
            if (gid != null)
            {
                goodsid = gid.ToString();
            }
            if (string.IsNullOrEmpty(goodsid))
            {
                sqlstring = "insert into TJ_GoodsInfo(CompID,GoodsTypeID,SaleUnitID,GoodsName,Descriptions,GoodsPicURL,GoodsPicURLSmal,Price,BeginSaleDate,EndSaleDate,DisplayDate,WLProID) values(" + compid + ",0,'件','产品名称','我的产品介绍','Images/upload/1/goodsdasheng1.jpg','Images/upload/1/goodsdasheng1.jpg',1,'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.AddYears(3).ToString("yyyy-MM-dd") + "','" + DateTime.Now + "'," + prodid + ") select @@identity";
                myCmd = new SqlCommand(sqlstring, myConn);
                goodsid = myCmd.ExecuteScalar().ToString();
            }

            sqlstring = "insert into TJ_SWM_PID_Active(pid,prodid,rpidstring,isactive,number,remarks,compid) values(" +
                        pid + "," + prodid + ",0,1," + activenum + ",'system'," + compid + ")";
            myCmd = new SqlCommand(sqlstring, myConn);
            myCmd.ExecuteNonQuery();
            sqlstring = "insert into TJ_SWM_PID_RPID(pid,rpid,starttm,endtm) values(" + pid + ",1,'" +
                        DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.MaxValue +
                        "');insert into TJ_SWM_PID_RPID(pid,rpid,starttm,endtm) values(" + pid + ",2,'" +
                        DateTime.Now.ToString("yyyy-MM-dd") + "','" +
                        (freeday.Equals(0)
                            ? DateTime.MaxValue.ToString("yyyy-MM-dd")
                            : DateTime.Now.AddDays(freeday + 1).ToString("yyyy-MM-dd")) +
                        "');insert into TJ_SWM_PID_RPID(pid,rpid,starttm,endtm) values(" + pid + ",3,'" +
                        DateTime.Now.ToString("yyyy-MM-dd") + "','" +
                        (freeday.Equals(0)
                            ? DateTime.MaxValue.ToString("yyyy-MM-dd")
                            : DateTime.Now.AddDays(freeday + 1).ToString("yyyy-MM-dd")) +
                        "');insert into TJ_SWM_PID_RPID(pid,rpid,starttm,endtm) values(" + pid + ",4,'" +
                        DateTime.Now.ToString("yyyy-MM-dd") + "','" +
                        (freeday.Equals(0)
                            ? DateTime.MaxValue.ToString("yyyy-MM-dd")
                            : DateTime.Now.AddDays(freeday + 1).ToString("yyyy-MM-dd")) +
                        "');insert into TJ_SWM_PID_RPID(pid,rpid,starttm,endtm) values(" + pid + ",5,'" +
                        DateTime.Now.ToString("yyyy-MM-dd") + "','" +
                        (freeday.Equals(0)
                            ? DateTime.MaxValue.ToString("yyyy-MM-dd")
                            : DateTime.Now.AddDays(freeday + 1).ToString("yyyy-MM-dd")) +
                        "');insert into TJ_SWM_PID_RPID(pid,rpid,starttm,endtm) values(" + pid + ",6,'" +
                        DateTime.Now.ToString("yyyy-MM-dd") + "','" + (freeday.Equals(0)
                            ? DateTime.MaxValue.ToString("yyyy-MM-dd")
                            : DateTime.Now.AddDays(freeday + 1).ToString("yyyy-MM-dd")) + "');";
            myCmd = new SqlCommand(sqlstring, myConn);
            myCmd.ExecuteNonQuery();

            sqlstring =
                "select distinct rpid from TJ_SWM_PID_RPID where pid in (select ta.pid from TJ_SWM_PID_Active ta where ta.compid=" +
                compid + ")";
            ada = new SqlDataAdapter(sqlstring, myConn);
            dttemp = new DataTable();
            ada.Fill(dttemp);
            int nm = 0;
            if (dttemp.Rows.Count > 0)
            {
                foreach (DataRow row in dttemp.Rows)
                {
                    sqlstring = "select count(id) from TJ_Comp_Roles where compid=" + compid + " and rpackid=" + row[0];
                    myCmd = new SqlCommand(sqlstring, myConn);
                    nm = int.Parse(myCmd.ExecuteScalar().ToString());
                    if (nm.Equals(0))
                    {
                        sqlstring =
                            "insert into TJ_Comp_Roles(compid,rpackid,authoruserid,authordate,isactive,remarks) values(" +
                            compid + "," + row[0] + ",0,'" + DateTime.Now + "',1,'system')";
                        myCmd = new SqlCommand(sqlstring, myConn);
                        myCmd.ExecuteNonQuery();
                    }
                }
            }

            sqlstring = "select count(IFID) from TJ_PublishInfo where CompID=" + compid;
            myCmd = new SqlCommand(sqlstring, myConn);
            nm = int.Parse(myCmd.ExecuteScalar().ToString());
            if (nm.Equals(0))
            {
                sqlstring = "insert into TJ_PublishInfo(CompID,IsHot,CID,LinkURLString,Title,Contents,PublishDate) values(" + compid +
                            ",1,2,'Images/commup/1/reliezhuhe.jpg','" + unitname + "开始启用结构三维码','热烈祝贺:" + unitname + "于" +
                            DateTime.Now.ToString("yyyy-MM-dd") + "开始启用三维码，从此走上新征程！','" + DateTime.Now + "')";
                myCmd = new SqlCommand(sqlstring, myConn);
                myCmd.ExecuteNonQuery();
            }
            dttemp.Dispose();
            ada.Dispose();
            myCmd.Dispose();
            myConn.Close();
        }
        return "";
    }

}



