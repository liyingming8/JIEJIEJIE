<%@ WebHandler Language="C#" Class="getactivityinfo" %> 
using System; 
using System.Data;
using System.Data.SqlClient;
using System.Globalization; 
using System.Web;
using commonlib;

public class getactivityinfo : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        Security sc = new Security();
        //context.Response.Write("Hello World");
        if (!string.IsNullOrEmpty(context.Request.QueryString["cd"]) && !string.IsNullOrEmpty(context.Request.QueryString["unid"]) && !string.IsNullOrEmpty(context.Request.QueryString["uid"]))
        {
            string code = context.Request.QueryString["cd"];
            string compid = context.Request.QueryString["unid"];
            string userid = context.Request.QueryString["uid"]; 
            string acidstring = CheckActivityInfo(compid);
            if (acidstring.Equals("0"))
            {
                context.Response.Write("0");
            }
            else
            {
                string boxlabelandtable =  GetTableAndCpid(code);
                if (boxlabelandtable.Equals("null"))
                {
                    context.Response.Write("0");
                }
                else
                {
                    string[] boxtablearray = boxlabelandtable.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                    if (boxtablearray.Length > 0)
                    {
                        string boxlabelcode = boxtablearray[0];
                        string tablename = boxtablearray[1];
                        DataTable dttemp = Getfhinfo(boxlabelcode, tablename, compid);
                        if (dttemp.Rows.Count > 0)
                        {
                            string ACIDDate = GetPermitPrizeActivityID(acidstring, dttemp.Rows[0]["ProID"].ToString(),
                                dttemp.Rows[0]["AgentID"].ToString(), dttemp.Rows[0]["KhDDH"].ToString(),
                                Convert.ToDateTime(dttemp.Rows[0]["FHDate"]).ToString("yyyy-MM-dd"));
                            string[] acidcreatearray = ACIDDate.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                            if (acidcreatearray.Length.Equals(2))
                            {
                               string temp = GetAcStrategyString(acidcreatearray[0], code, Convert.ToDateTime(acidcreatearray[1]).Year.ToString(), compid, DateTime.Now,dttemp.Rows[0]["ProID"].ToString(), dttemp.Rows[0]["AgentID"].ToString(),
                                    dttemp.Rows[0]["KhDDH"].ToString(), userid, DateTime.MinValue, DateTime.MinValue,
                                    boxlabelcode, tablename);
                                context.Response.Write(temp);
                            }
                            else
                            {
                                context.Response.Write("0");
                            }
                        }
                        else
                        {
                            context.Response.Write("0");
                        }
                    }
                    else
                    {
                        context.Response.Write("0");
                    }
                }
            }
        }
        else
        {
            context.Response.Write("0");
            //context.Response.Write(Test());
        }
    }

    private string GetPermitPrizeActivityID(string acidstring,string proid,string agentid,string khddh,string fhdate)
    {
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select acid,creatdate from TJ_Activity_CodeSpan where acid in(" + acidstring +
                             ") and (prodid=0 or prodid=" + proid + ") and (agentid=0 or agentid=" + agentid +
                             ") and (dingdanbianhao='' or dingdanbianhao='" + khddh +
                             "') and (fhedate='1900-01-01' or ('" + fhdate + "' between fhsdate and fhedate))";
            MyAda = new SqlDataAdapter(_tempsqlstring,conn);
            dtable = new DataTable();
            MyAda.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                return dtable.Rows[0][0] + "," + dtable.Rows[0][1];
            }
            return "0";
        }
    }
 
    public bool IsReusable {
        get {
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
    private DataTable dtable;
    private string _tempsqlstring = string.Empty;
    private string _returnvaltemp = string.Empty;
    Random random = new Random(1);
    /// <summary>
    /// 根据公司ID判断当前有没有活动
    /// </summary>
    /// <param name="compid">公司ID</param> 
    /// <returns>返回0或以”,“隔开的整形字符串</returns>
    public string CheckActivityInfo(string compid)
    {
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select id from TJ_Activity where CompID="+compid+" and '" + DateTime.Now.ToString("yyyy-MM-dd") + "' between STM and ETM";
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

    private decimal _tempvalue=0;
    private string GetAcStrategyString(string acid,string code, string createYear,string compid,DateTime smday,string proid,string agentid,string ordercode,string userid,DateTime sfh,DateTime efh,string boxlabelcode,string tablename)
    {
        _returnvaltemp = string.Empty;
        using (var conn = GetConnection)
        {
            string asid = GetAsidbyScid(acid);
            _tempsqlstring = "select timelimit,totalwinrate,toself,maxtimeself,topackage,packagenum from TJ_Activity_Strategy where id=" + asid;
            MyAda = new SqlDataAdapter(_tempsqlstring, conn);
            dtable = new DataTable();
            MyAda.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                DataRow row = dtable.Rows[0];
                int timelimit = GetScanTotalTimes(DateTime.Now, createYear,code); 
                if (timelimit < int.Parse(row["timelimit"].ToString()))
                {
                    if (Convert.ToInt32(row["totalwinrate"]) > 0)
                    {
                        _tempvalue = Totalwinrate(createYear, compid, smday, proid, agentid, ordercode, "", sfh, efh);//不是针对个人所以UserID处置空
                            //不是针对个人所以UserID处置空
                        if (_tempvalue < (Convert.ToDecimal(row["totalwinrate"]))/100)
                        {
                            _returnvaltemp = GetAwinfoString(acid);
                        }
                    }
                    else
                    {
                        if (Convert.ToBoolean(row["toself"]))
                        {
                            _tempvalue = Totalwinrate(createYear, compid, smday, proid, agentid, ordercode, userid,sfh, efh); 
                            if (_tempvalue < (1/decimal.Parse(row["maxtimeself"].ToString())))
                            {
                                if (DateTime.Now.Millisecond%2==0)
                                {
                                    _returnvaltemp = GetAwinfoString(acid);
                                } 
                            }
                            else
                            {
                                _returnvaltemp = GetAwinfoString(acid);
                            }
                        }
                        else
                        { 
                            string labcodestring = LabelCodeString(boxlabelcode, tablename);
                            int wintms = GetWinNumber(createYear, labcodestring);
                            int packagenum = int.Parse(row["packagenum"].ToString());
                            int packgeetotalnum = labcodestring.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Length - 1;
                            int scantms = GetScanTotalTimes(DateTime.Now, createYear, labcodestring);
                            if (scantms.Equals(0))
                            {
                                _returnvaltemp = GetAwinfoString(acid);
                            }
                            else
                            {
                                if (decimal.Parse(wintms.ToString(CultureInfo.InvariantCulture)) / scantms < decimal.Parse(packagenum.ToString(CultureInfo.InvariantCulture)) / packgeetotalnum)
                                {
                                    _returnvaltemp = GetAwinfoString(acid);
                                }
                                else
                                {
                                    _returnvaltemp = string.Empty;
                                }
                            } 
                        }
                    }
                }
            }
            MyAda.Dispose();
        }
        return _returnvaltemp.Equals(string.Empty)?"0":_returnvaltemp;
    }

    private string GetAsidbyScid(string acid)
    {
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select ASID from TJ_Activity  where id=" + acid;
            MyAda = new SqlDataAdapter(_tempsqlstring,conn);
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
            _tempsqlstring = "select count(id) from TJ_Activity_Win_" + acyear + " where codestr in (" + labcodestring +")";
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
            MyAda = new SqlDataAdapter(_tempsqlstring,conn);
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
    /// 返回发货表：TB_FaHuoInfo_[compid]的内容
    /// </summary>
    /// <param name="boxlabelcode"></param>
    /// <param name="tablename"></param>
    /// <param name="compid"></param>
    /// <returns></returns>
    private DataTable Getfhinfo(string boxlabelcode, string tablename,string compid)
    {
        using (var conn = GetConnectionWL)
        {
            _tempsqlstring = "select FHKey from " + tablename + "_FH where BoxLabel01='" + boxlabelcode + "'";
            dtable = new DataTable();
            MyAda = new SqlDataAdapter(_tempsqlstring,conn);
            MyAda.Fill(dtable);
            MyAda.Dispose();
            if (dtable.Rows.Count > 0)
            {
                _tempsqlstring = "select * from TB_FaHuoInfo_" + compid + " where FHKey='" + dtable.Rows[0][0] + "'";
                dtable.Dispose();
                MyAda = new SqlDataAdapter(_tempsqlstring,conn);
                dtable = new DataTable();
                MyAda.Fill(dtable);
                return dtable;
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 返回箱标号码和表名，以“,"隔开
    /// </summary>
    /// <param name="cpid"></param>
    /// <returns></returns>
    public string GetTableAndCpid(string cpid)
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
                var dbt=new DataTable();
                foreach (DataRow dr in dtable.Rows)
                {
                    string tname = dr[0].ToString();
                    MyAda = new SqlDataAdapter("select BoxLabel01  from " + tname + "_BT where BottleLabel='" + cpid + "' UNION select BoxLabel01  from " + tname + "_BT where BoxLabel01='" + cpid + "'", conn);
                    dbt = new DataTable();
                    MyAda.Fill(dbt);
                    MyAda.Dispose();
                    if (dbt.Rows.Count > 0)
                    {
                        string boxid = dbt.Rows[0][0].ToString();
                        MyAda = new SqlDataAdapter("select BoxLabel01  from " + tname + "_BX where BoxLabel='" + boxid + "'", conn);
                        var dbx = new DataTable();
                        MyAda.Fill(dbx);
                        MyAda.Dispose();
                        if (dbx.Rows.Count > 0)
                        {
                            rcpid = dbx.Rows[0][0] + "," + tname;
                            dbx.Dispose();
                            break;
                        }
                        rcpid = boxid + "," + tname;
                        dbx.Dispose();
                        break;
                    }
                    else
                    {
                        MyAda = new SqlDataAdapter("select BoxLabel01  from " + tname + "_BX where BoxLabel01='" + cpid + "'", conn);
                        var dbx = new DataTable();
                        MyAda.Fill(dbx);
                        MyAda.Dispose();
                        if (dbx.Rows.Count > 0)
                        {
                            rcpid = dbx.Rows[0][0] + "," + tname;
                            dbx.Dispose();
                            break;
                        }
                        dbx.Dispose();
                        rcpid = "null";
                    }
                }
                dbt.Dispose();
                conn.Close();
                dtable.Dispose();
                return rcpid;
            }
            conn.Close();
            return "null";
        }
    }

    private decimal Totalwinrate(string acyear,string compid, DateTime smday, string prodid, string agentID, string ordercode,string userid,DateTime sfh,DateTime efh)
    {
        int scantimes = ReturnScanTotalTimes(compid, smday, prodid, agentID, ordercode, userid,sfh,efh);
        int wintimes = ReturnWinTotalTimes(acyear, compid, smday, prodid, agentID, ordercode, userid,sfh,efh);
        if (scantimes.Equals(0))
        {
            return 0;
        }
        return decimal.Parse(wintimes.ToString(CultureInfo.InvariantCulture)) / scantimes;
    }

    private string _filterstring = string.Empty;
    private string ReturnFilterString(string compid, DateTime day, string prodid, string agentID, string ordercode,string userid,string prefix,DateTime sfh,DateTime efh)
    {
        _filterstring = prefix+".sm_date='" + day.ToString("yyyy-MM-dd") + "'  and "+prefix+".CompID=" + compid;
        if (!sfh.Equals(DateTime.MinValue)&& !efh.Equals(DateTime.MinValue))
        {
            _filterstring += " and sm_date between '"+sfh.ToString("yyyy-MM-dd")+"' and '"+efh.ToString("yyyy-MM-dd")+"'";
        }
        if (!string.IsNullOrEmpty(prodid) && !prodid.Equals("0"))
        {
            _filterstring += " and "+prefix+".prodid=" + prodid;
        }
        if (!string.IsNullOrEmpty(agentID) && !agentID.Equals("0"))
        {
            _filterstring += " and "+prefix+".agent_id=" + agentID;
        }
        if (!string.IsNullOrEmpty(ordercode) && !ordercode.Equals("0"))
        {
            _filterstring += " and "+prefix+".order_code='" + ordercode + "'";
        }
        if (!string.IsNullOrEmpty(userid) && !userid.Equals("0"))
        {
            _filterstring += " and "+prefix+".UserID=" + userid;
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
    private int ReturnScanTotalTimes(string compid, DateTime smday, string prodid, string agentID, string ordercode, string userid,DateTime sfh,DateTime efh)
    {
        _returnvaltemp = "0";
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select count(id) as cnt from TJ_SMinfo_" + smday.Year + " A where " + ReturnFilterString(compid, smday, prodid, agentID, ordercode, userid,"A",sfh,efh);
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
    private int GetScanTotalTimes(DateTime smday,string acYear,string codestring)
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
        string[] array =("sdddddd,ddssss,dddddd,ssssss,dddddd,eeeeeee").Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
        return String.Join(" UNION ",array);
    }
    

    private int ReturnWinTotalTimes(string acyear,string compid, DateTime smday, string prodid, string agentID, string ordercode,string userid,DateTime sfh,DateTime efh)
    {
        _returnvaltemp = "0";
        using (var conn = GetConnection)
        { 
            _tempsqlstring = "select count(A.id) as cnt from TJ_Activity_Win_" + acyear +
                             "  A where exists(select id from TJ_SMinfo_" + smday.Year + " B where " +
                             ReturnFilterString(compid, smday, prodid, agentID, ordercode, userid, "B",sfh,efh) +
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
            _tempsqlstring = "select count(id) as cnt from TJ_Activity_Win_" + createYear + " where codestr='" + code + "'";
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
                        _returnvaltemp = "{tpid:" + row["awtype"] + ",pervl:" + row["percentvl"] + ",awid:" +
                                         row["awid"] + ",prvl:" + row["prizevalue"] + ",awnm:'"+row["awnm"]+"'}";
                    }
                    else
                    {
                        _returnvaltemp += ",{tpid:" + row["awtype"] + ",pervl:" + row["percentvl"] + ",awid:" +
                                         row["awid"] + ",prvl:" + row["prizevalue"] + ",awnm:'"+row["awnm"]+"'}";
                    }
                }
            }
        }
        return _returnvaltemp.Equals(string.Empty) ? "" : "[" + _returnvaltemp + "]";
    } 

}