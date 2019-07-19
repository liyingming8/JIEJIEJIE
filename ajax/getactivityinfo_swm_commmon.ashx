<%@ WebHandler Language="C#" Class="getactivityinfo_swm_commmon" %>
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;

public class getactivityinfo_swm_commmon : IHttpHandler
{ 
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string result = "{\"flag\":\"0\"}";
        //context.Response.Write("Hello World");
        if (!string.IsNullOrEmpty(context.Request.QueryString["cd"]) &&
            !string.IsNullOrEmpty(context.Request.QueryString["unid"]) &&
            !string.IsNullOrEmpty(context.Request.QueryString["uid"]))
        {
            string code = context.Request.QueryString["cd"];
            string compid = context.Request.QueryString["unid"];
            string userid = context.Request.QueryString["uid"];  
            string prodid = context.Request["prodid"];
            string agentid = context.Request["agentid"];
            string fhdate = context.Request["fhdate"];
            string acidstring = CheckActivityInfo(compid, prodid, agentid);
            if (acidstring != "0")
            {
                string acidDate = GetPermitPrizeActivityID(acidstring, "", fhdate);
                string[] acidcreatearray = acidDate.Split(new[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries);
                if (acidcreatearray.Length.Equals(2))
                { 
                    string temp = GetAcStrategyString(acidcreatearray[0], code, Convert.ToDateTime(acidcreatearray[1]).Year.ToString(), compid, DateTime.Now, userid);
                    if (temp != "0" && temp.Length > 5)
                    {
                        result = "{\"flag\":\"1\",\"acid\":\"" + acidcreatearray[0] + "\",\"jpdata\":" + temp + "}";
                    } 
                }
            }
        }
        context.Response.Write(result);
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private SqlConnection myConn;
    private SqlConnection myConnwuliu;
    public SqlConnection GetConnection
    {
        get
        {
            string str = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString();
            myConn = new SqlConnection(str);
            return myConn;
        }

    }
    public SqlConnection GetConnectionWL
    {
        get
        {
            string str = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString();
            myConnwuliu = new SqlConnection(str);
            return myConnwuliu;
        }

    }

    private SqlDataAdapter MyAda;
    private SqlCommand MyCmd = null;
    private DataTable dtable;
    private string _tempsqlstring = string.Empty;
    private string _returnvaltemp = string.Empty;  
    Random rperson = new Random(); 

    private string GetPermitPrizeActivityID(string acidstring, string khddh, string fhdate)
    {
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select acid,creatdate from TJ_Activity_CodeSpan where acid in(" + acidstring +
                             ") and (dingdanbianhao='' or dingdanbianhao='" + khddh +
                             "') and (fhedate='1900-01-01' or ('" + fhdate + "' between fhsdate and fhedate))";
            MyAda = new SqlDataAdapter(_tempsqlstring, conn);
            dtable = new DataTable();
            MyAda.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                return dtable.Rows[0][0] + "," + dtable.Rows[0][1];
            }
            return "0";
        }
    }
    /// <summary>
    /// 根据公司ID判断当前有没有活动
    /// </summary>
    /// <param name="compid">公司ID</param> 
    /// <returns>返回0或以”,“隔开的整形字符串</returns>
    public string CheckActivityInfo(string compid, string prodid,string agentid)
    {
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select ac.id from TJ_Activity ac where ac.CompID=" + compid + " and ac.FaceTo='1' and ('" + DateTime.Now.ToString("yyyy-MM-dd") + "' between ac.STM and ac.ETM) and  ((ac.prodnum=0 or (select count(id) from TJ_Activity_Product p where p.prodid=" + prodid + " and p.acid=ac.id)>0))  and  ((ac.agentnum=0 or (select count(ag.id) from TJ_Activity_Agent ag where ag.agentid=" + agentid + " and ag.acid=ac.id)>0)) and  ((ac.terminalnum=0 or (select count(at.id) from TJ_Activity_Terminal at where at.terminalid=" + agentid + " and at.acid=ac.id)>0))";
            dtable = new DataTable();
            MyAda = new SqlDataAdapter(_tempsqlstring, conn);
            MyAda.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                _returnvaltemp = string.Empty;
                foreach (DataRow row in dtable.Rows)
                {
                    if (_returnvaltemp.Equals(string.Empty))
                    {
                        _returnvaltemp = row[0].ToString();
                    }
                    else
                    {
                        _returnvaltemp += "," + row[0];
                    }
                }
                dtable.Dispose();
                MyAda.Dispose();
                return _returnvaltemp;
            }
            dtable.Dispose();
            return "0";
        }
    }

    private decimal _tempvalue = 0;
    private string GetAcStrategyString(string acid, string code, string createYear, string compid, DateTime smday,string userid)
    {
        _returnvaltemp = string.Empty;
        using (var conn = GetConnection)
        {
            string asid = GetAsidbyScid(acid);//策略ID
            _tempsqlstring = "select timelimit,totalwinrate,toself,maxtimeself,topackage,packagenum from TJ_Activity_Strategy where id=" + asid;
            MyAda = new SqlDataAdapter(_tempsqlstring, conn);
            dtable = new DataTable();
            MyAda.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                DataRow row = dtable.Rows[0];
                int timelimit = GetScanTotalTimes(DateTime.Now, createYear, code); //扫码总数
                if (timelimit <= int.Parse(row["timelimit"].ToString()))
                {
                    if (Convert.ToInt32(row["totalwinrate"]) > 0)
                    {
                        _tempvalue = Totalwinrate_allrate(createYear, compid, smday, acid);//不是针对个人所以UserID处置空
                                                                                           //不是针对个人所以UserID处置空
                        if (_tempvalue < (Convert.ToDecimal(row["totalwinrate"])) / 100)
                        {
                            _returnvaltemp = GetAwinfoString(acid);
                        }
                    }
                    else
                    {
                        if (Convert.ToBoolean(row["toself"]))
                        {
                            string day = smday.ToString("yyyy-MM-dd");
                            int smsday = int.Parse(GetSingleData("count(id) as  cnt", "TJ_SMinfo_" + smday.Year, "SMTime>='" + day + "' and acid=" + acid + " and CompID=" + compid + " and UserID=" + userid));
                            int lqsday = int.Parse(GetSingleData("count(id) as  cnt", "TJ_Activity_Win_" + smday.Year, "gettm>='" + day + "' and acid=" + acid + " and compid=" + compid + " and  UserID=" + userid+" and iszj=1"));
                            int xdcs = int.Parse(row["maxtimeself"].ToString());

                            int lh = smsday / xdcs;
                            int lhint = smsday % xdcs;
                            if (lh == lqsday && lhint > 0)
                            {
                                //有奖
                                int sjbj = rperson.Next(0, 1);
                                if (sjbj == 0)
                                {
                                    _returnvaltemp = GetAwinfoString(acid);
                                }
                            }
                            else if (lh > lqsday && lhint == 0)
                            {
                                _returnvaltemp = GetAwinfoString(acid);
                            } 
                        } 
                    }
                }
            }
            MyAda.Dispose();
        }
        return _returnvaltemp.Equals(string.Empty) ? "0" : _returnvaltemp;
    }

    private string GetAsidbyScid(string acid)
    {
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select ASID from TJ_Activity  where id=" + acid;
            MyAda = new SqlDataAdapter(_tempsqlstring, conn);
            dtable = new DataTable();
            MyAda.Fill(dtable);
            MyAda.Dispose();
            _returnvaltemp = "0";
            if (dtable.Rows.Count > 0)
            {
                _returnvaltemp = dtable.Rows[0][0].ToString();
            }
            dtable.Dispose();
            return _returnvaltemp;
        }
    }

    private int GetWinNumber(string acyear, string labcodestring)
    {
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select count(id) from TJ_Activity_Win_" + acyear + " where  iszj=1 and  codestr in (" + labcodestring + ")";
            MyAda = new SqlDataAdapter(_tempsqlstring, conn);
            dtable = new DataTable();
            MyAda.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                return int.Parse(dtable.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }
    }

    private string LabelCodeString(string boxLabelCode01, string tablename)
    {
        using (var conn = GetConnectionWL)
        {
            _tempsqlstring = "SELECT BottleLabel  FROM " + tablename + "_BT  where BoxLabel01='" + boxLabelCode01 + "'";
            dtable = new DataTable();
            MyAda = new SqlDataAdapter(_tempsqlstring, conn);
            MyAda.Fill(dtable);
            MyAda.Dispose();
            _returnvaltemp = string.Empty;
            if (dtable.Rows.Count > 0)
            {
                _returnvaltemp = boxLabelCode01;
                foreach (DataRow row in dtable.Rows)
                {
                    _returnvaltemp += "," + row[0];
                }
            }
            return _returnvaltemp;
        }
    } 
    /// <summary>
    /// 通过B标，查出A标
    /// </summary>
    /// <param name="Bcode"></param>
    /// <returns></returns>
    public string GetACodeBY_BCode(string Bcode)
    {
        using (var conn = GetConnectionWL)
        {
            _tempsqlstring = "select tablenameinfo  from TB_LabelCodeInfo where startvalue<='" + Bcode + "' and  endvalue>='" + Bcode + "'";
            dtable = new DataTable();
            MyAda = new SqlDataAdapter(_tempsqlstring, conn);
            MyAda.Fill(dtable);
            MyAda.Dispose();
            string Acode = "null";
            if (dtable.Rows.Count > 0)
            {
                foreach (DataRow dr in dtable.Rows)
                {
                    string tname = dr[0].ToString();
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    MyCmd = new SqlCommand("select ACode  from " + tname + "_BA where BCode='" + Bcode + "'", conn);
                    object result = MyCmd.ExecuteScalar();
                    MyCmd.Dispose();
                    if (result != null)
                    {
                        Acode = result.ToString();
                    }

                }
                conn.Close();
                dtable.Dispose();
                return Acode;
            }
            conn.Close();
            return "null";
        }
    }

    /// <summary>
    ///   返回箱标号码和表名，以“,"隔开(新)
    /// </summary>
    /// <param name="cpid"></param>
    /// <returns></returns>
    public string GetTableAndCpid_New(string cpid)
    {
        using (var conn = GetConnectionWL)
        {
            _tempsqlstring = "select tablenameinfo  from TB_LabelCodeInfo where startvalue<='" + cpid + "' and  endvalue>='" + cpid + "'";
            dtable = new DataTable();
            MyAda = new SqlDataAdapter(_tempsqlstring, conn);
            MyAda.Fill(dtable);
            MyAda.Dispose();
            string rcpid = "";
            if (dtable.Rows.Count > 0)
            {

                foreach (DataRow dr in dtable.Rows)
                {
                    string tname = dr[0].ToString();
                    MyCmd = new SqlCommand("select BoxLabel01  from " + tname + "_BT where BottleLabel='" + cpid + "' UNION select BoxLabel01  from " + tname + "_BT where BoxLabel01='" + cpid + "'", conn);
                    object result = MyCmd.ExecuteScalar();
                    MyCmd.Dispose();
                    if (result != null)
                    {
                        string boxid = result.ToString();
                        MyCmd = new SqlCommand("select BoxLabel01  from " + tname + "_BX where BoxLabel='" + boxid + "'", conn);
                        result = MyCmd.ExecuteScalar();
                        MyCmd.Dispose();
                        if (result != null)
                        {
                            rcpid = result + "," + tname;
                            break;
                        }
                        rcpid = boxid + "," + tname;
                        break;
                    }
                    else
                    {
                        MyCmd = new SqlCommand("select BoxLabel01  from " + tname + "_BX where BoxLabel01='" + cpid + "'", conn);
                        result = MyCmd.ExecuteScalar();
                        MyCmd.Dispose();
                        if (result != null)
                        {
                            rcpid = result + "," + tname;
                            break;
                        }
                        rcpid = "null";
                    }
                }
                conn.Close();
                dtable.Dispose();
                return rcpid;
            }
            conn.Close();
            return "null";
        }
    } 

    /// <summary>
    /// 计算总体概率
    /// </summary>
    /// <param name="acyear"></param>
    /// <param name="compid"></param>
    /// <param name="smday"></param>
    /// <param name="prodid"></param>
    /// <param name="agentID"></param>
    /// <param name="ordercode"></param>
    /// <param name="userid"></param>
    /// <param name="sfh"></param>
    /// <param name="efh"></param>
    /// <returns></returns>
    private decimal Totalwinrate_allrate(string acyear, string compid, DateTime smday, string acid)
    {
        string day = smday.ToString("yyyy-MM-dd");
        string scantimesday = GetSingleData("count(id) as  cnt", "TJ_SMinfo_" + smday.Year, "SMTime>='" + day + "' and acid=" + acid + " and CompID=" + compid);
        string lqtimesday = GetSingleData("count(id) as  cnt", "TJ_Activity_Win_" + smday.Year, "gettm>='" + day + "' and acid=" + acid + " and compid=" + compid+" and iszj=1");
        if (scantimesday != "no" && lqtimesday != "no" && scantimesday != "0")
        {
            return decimal.Parse(lqtimesday.ToString(CultureInfo.InvariantCulture)) / int.Parse(scantimesday);
        }
        else
        {
            return 0;
        }
    }

    private decimal Totalwinrate(string acyear, string compid, DateTime smday, string prodid, string agentID, string ordercode, string userid, DateTime sfh, DateTime efh)
    {
        int scantimes = ReturnScanTotalTimes(compid, smday, prodid, agentID, ordercode, userid, sfh, efh);
        int wintimes = ReturnWinTotalTimes(acyear, compid, smday, prodid, agentID, ordercode, userid, sfh, efh);
        if (scantimes.Equals(0))
        {
            return 0;
        }
        return decimal.Parse(wintimes.ToString(CultureInfo.InvariantCulture)) / scantimes;
    }

    private string _filterstring = string.Empty;
    private string ReturnFilterString(string compid, DateTime day, string prodid, string agentID, string ordercode, string userid, string prefix, DateTime sfh, DateTime efh)
    {
        _filterstring = prefix + ".sm_date='" + day.ToString("yyyy-MM-dd") + "'  and " + prefix + ".CompID=" + compid;
        if (!sfh.Equals(DateTime.MinValue) && !efh.Equals(DateTime.MinValue))
        {
            _filterstring += " and sm_date between '" + sfh.ToString("yyyy-MM-dd") + "' and '" + efh.ToString("yyyy-MM-dd") + "'";
        }
        if (!string.IsNullOrEmpty(prodid) && !prodid.Equals("0"))
        {
            _filterstring += " and " + prefix + ".prodid=" + prodid;
        }
        if (!string.IsNullOrEmpty(agentID) && !agentID.Equals("0"))
        {
            _filterstring += " and " + prefix + ".agent_id=" + agentID;
        }
        if (!string.IsNullOrEmpty(ordercode) && !ordercode.Equals("0"))
        {
            _filterstring += " and " + prefix + ".order_code='" + ordercode + "'";
        }
        if (!string.IsNullOrEmpty(userid) && !userid.Equals("0"))
        {
            _filterstring += " and " + prefix + ".UserID=" + userid;
        }
        return _filterstring;
    }


    /// <summary>
    /// 返回扫码次数
    /// </summary>
    /// <param name="compid">公司ID</param>
    /// <param name="day">天</param>
    /// <param name="prodid">产品ID</param>
    /// <param name="agentID">经销商ID</param>
    /// <param name="ordercode">订单编号</param>
    /// <param name="userid">用户ID</param>
    /// <returns></returns>
    private int ReturnScanTotalTimes(string compid, DateTime smday, string prodid, string agentID, string ordercode, string userid, DateTime sfh, DateTime efh)
    {
        _returnvaltemp = "0";
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select count(id) as cnt from TJ_SMinfo_" + smday.Year + " A where " + ReturnFilterString(compid, smday, prodid, agentID, ordercode, userid, "A", sfh, efh);
            MyAda = new SqlDataAdapter(_tempsqlstring, conn);
            dtable = new DataTable();
            MyAda.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                _returnvaltemp = dtable.Rows[0]["cnt"].ToString();
            }
            MyAda.Dispose();
        }
        return int.Parse(_returnvaltemp);
    }

    /// <summary>
    /// 根据套标查询扫码次数
    /// </summary>
    /// <param name="smday"></param>
    /// <param name="codestring"></param>
    /// <returns></returns>
    private int GetScanTotalTimes(DateTime smday, string acYear, string codestring)
    {
        _returnvaltemp = "0";
        _tempsqlstring = string.Empty;
        using (var conn = GetConnection)
        {
            if (smday.Year.Equals(int.Parse(acYear)))
            {
                if (codestring.Contains(","))
                {
                    _tempsqlstring = "select count(id) as cnt from TJ_SMinfo_" + smday.Year + "  where LabelCode in (" + codestring + ")";
                }
                else
                {
                    _tempsqlstring = "select count(id) as cnt from TJ_SMinfo_" + smday.Year + "  where LabelCode='" + codestring + "'";
                }
            }
            else
            {
                for (int y = int.Parse(acYear); y <= smday.Year; y++)
                {
                    if (codestring.Contains(","))
                    {
                        if (_tempsqlstring.Equals(string.Empty))
                        {
                            _tempsqlstring = "select count(id) as cnt from TJ_SMinfo_" + smday.Year + "  where LabelCode in (" + codestring + ")";
                        }
                        else
                        {
                            _tempsqlstring += " UNION select count(id) as cnt from TJ_SMinfo_" + smday.Year + "  where LabelCode in (" + codestring + ")";
                        }
                    }
                    else
                    {
                        if (_tempsqlstring.Equals(string.Empty))
                        {
                            _tempsqlstring = "select count(id) as cnt from TJ_SMinfo_" + smday.Year + "  where LabelCode='" + codestring + "'";
                        }
                        else
                        {
                            _tempsqlstring += " UNION select count(id) as cnt from TJ_SMinfo_" + smday.Year + "  where LabelCode='" + codestring + "'";
                        }
                    }
                }
            }
            MyAda = new SqlDataAdapter(_tempsqlstring, conn);
            dtable = new DataTable();
            MyAda.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                _returnvaltemp = dtable.Rows[0]["cnt"].ToString();
            }
            MyAda.Dispose();
        }
        return int.Parse(_returnvaltemp);
    }

    private string Test()
    {
        string[] array = ("sdddddd,ddssss,dddddd,ssssss,dddddd,eeeeeee").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        return String.Join(" UNION ", array);
    }


    private int ReturnWinTotalTimes(string acyear, string compid, DateTime smday, string prodid, string agentID, string ordercode, string userid, DateTime sfh, DateTime efh)
    {
        _returnvaltemp = "0";
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select count(A.id) as cnt from TJ_Activity_Win_" + acyear +
                             "  A where exists(select id from TJ_SMinfo_" + smday.Year + " B where " +
                             ReturnFilterString(compid, smday, prodid, agentID, ordercode, userid, "B", sfh, efh) +
                             " and  B.LabelCode=A.codestr)";
            MyAda = new SqlDataAdapter(_tempsqlstring, conn);
            dtable = new DataTable();
            MyAda.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                _returnvaltemp = dtable.Rows[0]["cnt"].ToString();
            }
            MyAda.Dispose();
        }
        return int.Parse(_returnvaltemp);
    }

    /// <summary>
    /// 返回单枚标签获奖次数
    /// </summary>
    /// <param name="code">码</param>
    /// <param name="createYear">活动创建年</param>
    /// <returns></returns>
    private int CheckWinTimes(string code, string createYear)
    {
        _returnvaltemp = "0";
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select count(id) as cnt from TJ_Activity_Win_" + createYear + " where codestr='" + code + "' and iszj=1";
            MyAda = new SqlDataAdapter(_tempsqlstring, conn);
            dtable = new DataTable();
            MyAda.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                _returnvaltemp = dtable.Rows[0]["cnt"].ToString();
            }
        }
        dtable.Dispose();
        return int.Parse(_returnvaltemp);
    }

    private string GetAwinfoString(string acid)
    {
        _returnvaltemp = string.Empty;
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select awtype,awid,prizevalue,percentvl,awnm from TJ_Activity_Prizes where acid=" + acid;
            MyAda = new SqlDataAdapter(_tempsqlstring, conn);
            dtable = new DataTable();
            MyAda.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                foreach (DataRow row in dtable.Rows)
                {
                    if (_returnvaltemp.Equals(string.Empty))
                    {
                        _returnvaltemp = "{\"tpid\":\"" + row["awtype"] + "\",\"pervl\":\"" + row["percentvl"] + "\",\"awid\":\"" +
                                         row["awid"] + "\",\"prvl\":\"" + row["prizevalue"] + "\",\"awnm\":\"" + row["awnm"] + "\"}";
                    }
                    else
                    {
                        _returnvaltemp += ",{\"tpid\":\"" + row["awtype"] + "\",\"pervl\":\"" + row["percentvl"] + "\",\"awid\":\"" +
                                         row["awid"] + "\",\"prvl\":\"" + row["prizevalue"] + "\",\"awnm\":\"" + row["awnm"] + "\"}";
                    }
                }
            }
        }
        return _returnvaltemp.Equals(string.Empty) ? "" : "[" + _returnvaltemp + "]";
    }

    #region 通用
    public string GetSingleData(string cstr, string table, string wstr)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string sqlstr = "select " + cstr + "  from  " + table + "  where  " + wstr;
            MyCmd = new SqlCommand(sqlstr, myConn);
            object result = MyCmd.ExecuteScalar();
            MyCmd.Dispose();
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
    #endregion 
}