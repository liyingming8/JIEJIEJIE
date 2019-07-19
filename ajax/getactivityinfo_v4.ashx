<%@ WebHandler Language="C#" Class="GetactivityinfoV4" %>
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;

public class GetactivityinfoV4 : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string objtable = "AgentAcceptLabelCodeInfo_2019";
        string result = "{\"flag\":\"0\"}";
        try
        {
            if (!string.IsNullOrEmpty(context.Request.QueryString["cd"]) &&
        !string.IsNullOrEmpty(context.Request.QueryString["unid"]) &&
        !string.IsNullOrEmpty(context.Request.QueryString["uid"]))
            {
                string code = context.Request.QueryString["cd"];
                string compid = context.Request.QueryString["unid"];
                string userid = context.Request.QueryString["uid"];
                string insertstring = "insert into TJ_Activity_SMInfo(codenm,compid,userid) values('" + code + "'," +compid + "," + userid + ")";
                using (_myConn = GetConnection)
                {
                    if (_myConn.State != ConnectionState.Open)
                    {
                        _myConn.Open();
                    }
                    _myCmd = new SqlCommand(insertstring,_myConn);
                    _myCmd.ExecuteNonQuery();
                    _myCmd.Dispose();
                }
                string[] array = GetBoxLabelCode(objtable, code);
                if (null != array&&array[1].Length>0)
                {
                    DataTable dttemp = GetShouHuoConfirmInfo(array[0], "AgentAcceptInfo_2019", compid);
                    if (dttemp != null && dttemp.Rows.Count > 0) //终端店确认过
                    {
                        string terminalid = dttemp.Rows[0]["terminalid"].ToString();
                        DataTable dtactivity = CheckActivityInfo(compid, dttemp.Rows[0]["pid"].ToString(), dttemp.Rows[0]["agentid"].ToString(), dttemp.Rows[0]["terminalid"].ToString(), array[2], array[1]);
                        if (dtactivity.Rows.Count > 0)
                        {
                            string temp = GetAcStrategyString(dtactivity.Rows[0]["id"].ToString(), code,
                                Convert.ToDateTime(dtactivity.Rows[0]["cdate"]).Year.ToString(), compid, DateTime.Now,
                                userid, array[0], "");
                            if (temp != "0" && temp.Length > 5)
                            {
                                Rebatetoterminal(dtactivity.Rows[0]["id"].ToString(), compid, terminalid,code);
                                result = "{\"flag\":\"1\",\"acid\":\"" + dtactivity.Rows[0]["id"] + "\",\"jpdata\":" + temp + "}";
                            }
                        } 
                        dtactivity.Dispose();
                    }
                    else
                    {
                        result = "{\"flag\":\"0\"}";
                    }
                    if (dttemp != null) dttemp.Dispose();
                }
                else //两种情况：箱标，终端店没有确认过
                {
                    string tempstring = GetTableAndCpidNew(code);
                    if (tempstring.Contains(","))
                    {
                        string[] array1 = tempstring.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        string boxlabelcode = array1[0];
                        string tablename = array1[1];
                        if (boxlabelcode != code) //不是箱标
                        {
                            DataTable dttemp = Getfhinfo(boxlabelcode, tablename, compid);
                            if (dttemp != null && dttemp.Rows.Count > 0)
                            {
                                DataTable dtactivity = CheckActivityInfo(compid, dttemp.Rows[0]["ProID"].ToString(),
                                    dttemp.Rows[0]["AgentID"].ToString(), dttemp.Rows[0]["KhDDH"].ToString(),
                                    dttemp.Rows[0]["FHDate"].ToString());

                                if (dtactivity.Rows.Count > 0)
                                {
                                    string temp = GetAcStrategyString(dtactivity.Rows[0]["id"].ToString(), code,
                                        Convert.ToDateTime(dtactivity.Rows[0]["cdate"]).Year.ToString(), compid,
                                        DateTime.Now, userid, boxlabelcode, "");
                                    if (temp != "0" && temp.Length > 5)
                                    {
                                        result = "{\"flag\":\"1\",\"acid\":\"" + dtactivity.Rows[0]["id"] +
                                                 "\",\"jpdata\":" + temp + "}";
                                        AddNoConfirmLabelInfo(code, boxlabelcode, objtable,compid,dttemp.Rows[0]["AgentID"].ToString(),"0");
                                    }
                                }
                                dtactivity.Dispose();
                            } //没有发货信息
                            else
                            {
                                result = "{\"flag\":\"0\"}";
                            }
                            if (dttemp != null) dttemp.Dispose();
                        }
                        else //箱标：不参与消费者扫码活动
                        {
                            result = "{\"flag\":\"-1\"}";
                        }
                    }
                    else
                    {
                        result = "{\"flag\":\"0\"}";
                    }
                }
                context.Response.Write(result);
            }
            else
            {
                context.Response.Write("{\"flag\":\"0\"}");
            }
        }
        catch 
        {
            context.Response.Write("{\"flag\":\"0\"}");
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private SqlConnection _myConn;
    private SqlConnection _myConnwuliu;
    public SqlConnection GetConnection
    {
        get
        {
            string str = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString();
            _myConn = new SqlConnection(str);
            return _myConn;
        }
    }
    public SqlConnection GetConnectionWl
    {
        get
        {
            string str = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString();
            _myConnwuliu = new SqlConnection(str);
            return _myConnwuliu;
        }
    }

    private SqlDataAdapter _myAda;
    private SqlCommand _myCmd;
    private DataTable _dtable;
    private string _tempsqlstring = string.Empty;
    private string _returnvaltemp = string.Empty;
    readonly Random _rperson = new Random();

    private void Rebatetoterminal(string acid, string compid, string terminalid,string code)
    {
        using (_myConn = GetConnection)
        {
            if (_myConn.State == ConnectionState.Closed)
            {
                _myConn.Open();
            }
            _myCmd = new SqlCommand("select top 1 feedbacktoparent,feedvalue from TJ_Activity where id=" + acid, _myConn);
            SqlDataReader sdr = _myCmd.ExecuteReader();
            string insertstring = "";
            if (sdr.Read())
            {
                if (Convert.ToBoolean(sdr["feedbacktoparent"]))
                {
                    int jf = Convert.ToInt32(sdr["feedvalue"]);
                    insertstring = "insert into TJ_Activity_JXS_Win(compid,agentid,awtypeid,winreason,prizevl,confirmtm,wintypeid,remarks) values(" + compid + "," + terminalid + ",2,'消费者扫码返利'," + jf + ",'" + DateTime.Now + "',4,'"+code+"')";
                }
            }
            sdr.Close();
            _myCmd.Dispose();
            if (insertstring.Length > 0)
            {
                SqlCommand scmd = new SqlCommand(insertstring, _myConn);
                scmd.ExecuteNonQuery();
                scmd.Dispose();
            }
            _myConn.Close();
            _myConn.Dispose();
        }
    }

    /// <summary>
    /// 返回发货表：TB_FaHuoInfo_[compid]的内容
    /// </summary>
    /// <param name="boxlabelcode"></param>
    /// <param name="tablename"></param>
    /// <param name="compid"></param>
    /// <returns></returns>
    private DataTable Getfhinfo(string boxlabelcode, string tablename, string compid)
    {
        using (var conn = GetConnectionWl)
        {
            _tempsqlstring = "select top 1 FHKey from " + tablename + "_FH where BoxLabel01='" + boxlabelcode + "'  and FromAgentID=" + compid + " and FHKey<>'' order by ID";
            _dtable = new DataTable();
            _myAda = new SqlDataAdapter(_tempsqlstring, conn);
            _myAda.Fill(_dtable);
            _myAda.Dispose();
            if (_dtable.Rows.Count > 0)
            {
                _tempsqlstring = "select FHDate,AgentID,ProID,KhDDH  from TB_FaHuoInfo_" + compid + " where FHKey='" + _dtable.Rows[0][0] + "'";
                _dtable.Dispose();
                _myAda = new SqlDataAdapter(_tempsqlstring, conn);
                _dtable = new DataTable();
                _myAda.Fill(_dtable);
                return _dtable;
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// 根据公司ID、产品ID、经销商ID、终端店ID,以及当前时间返回可能满足消费者的活动ID
    /// </summary>
    /// <param name="compid">公司ID</param>
    /// <param name="prodid">产品ID</param>
    /// <param name="agentid">经销商ID</param>
    /// <param name="terminalid">终端店ID</param>
    /// <param name="khddh">客户订单号</param>
    /// <param name="fhdate">发货时间</param>
    /// <returns>返回0或以”,“隔开的整形字符串</returns>
    public DataTable CheckActivityInfo(string compid, string prodid, string agentid, string terminalid, string khddh, string fhdate)
    {
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select ac.id,ac.CreateDate as cdate from TJ_Activity ac where ac.CompID=" + compid + " and ac.FaceTo='1' and ('" + DateTime.Now.ToString("yyyy-MM-dd") + "' between ac.STM and ac.ETM) and (ac.anyfahuodate>0 or ('" + fhdate + "' between ac.sfahuodate and ac.efahuodate)) and (khddh='' or khddh='" + khddh + "') and  ((ac.prodnum=0 or (select count(id) from TJ_Activity_Product p where p.prodid=" + prodid + " and p.acid=ac.id)>0))  and  ((ac.agentnum=0 or (select count(ag.id) from TJ_Activity_Agent ag where ag.agentid=" + agentid + " and ag.acid=ac.id)>0)) and  ((ac.terminalnum=0 or (select count(at.id) from TJ_Activity_Terminal at where at.terminalid=" + terminalid + " and at.acid=ac.id)>0)) order by ac.priority desc";
            _dtable = new DataTable();
            _myAda = new SqlDataAdapter(_tempsqlstring, conn);
            _myAda.Fill(_dtable);
            return _dtable;
        }
    }

    /// <summary>
    /// 根据公司、产品ID，经销商ID，以及当前时间返回可能满足的活动ID信息
    /// </summary>
    /// <param name="compid">公司ID</param>
    /// <param name="prodid">产品ID</param>
    /// <param name="agentid">经销商ID</param>
    /// <param name="khddh"></param>
    /// <param name="fhdate"></param>
    /// <returns></returns>
    public DataTable CheckActivityInfo(string compid, string prodid, string agentid, string khddh, string fhdate)
    {
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select ac.id,ac.CreateDate as cdate from TJ_Activity ac where ac.CompID=" + compid + " and ac.FaceTo='1' and ('" + DateTime.Now.ToString("yyyy-MM-dd") + "' between ac.STM and ac.ETM) and (ac.anyfahuodate>0 or ('" + fhdate + "' between ac.sfahuodate and ac.efahuodate)) and (khddh='' or khddh='" + khddh + "') and  ((ac.prodnum=0 or (select count(id) from TJ_Activity_Product p where p.prodid=" + prodid + " and p.acid=ac.id)>0))  and  ((ac.agentnum=0 or (select count(ag.id) from TJ_Activity_Agent ag where ag.agentid=" + agentid + " and ag.acid=ac.id)>0)) and  ac.terminalnum=0 order by ac.priority desc";
            _dtable = new DataTable();
            _myAda = new SqlDataAdapter(_tempsqlstring, conn);
            _myAda.Fill(_dtable);
            return _dtable;
        }
    }

    private decimal _tempvalue;
    private string GetAcStrategyString(string acid, string code, string createYear, string compid, DateTime smday, string userid, string boxlabelcode, string tablename)
    {
        _returnvaltemp = string.Empty;
        using (var conn = GetConnection)
        {
            string asid = GetAsidbyScid(acid);//策略ID
            _tempsqlstring = "select timelimit,totalwinrate,toself,maxtimeself,topackage,packagenum from TJ_Activity_Strategy where id=" + asid;
            _myAda = new SqlDataAdapter(_tempsqlstring, conn);
            _dtable = new DataTable();
            _myAda.Fill(_dtable);
            if (_dtable.Rows.Count > 0)
            {
                DataRow row = _dtable.Rows[0];
                int timelimit = GetScanTotalTimes(DateTime.Now, createYear, code, compid); //扫码总数
                if (timelimit <= int.Parse(row["timelimit"].ToString()))
                {
                    if (Convert.ToInt32(row["totalwinrate"]) > 0)
                    {
                        if (Convert.ToInt32(row["totalwinrate"]).Equals(100))
                        {
                            _returnvaltemp = GetAwinfoString(acid);
                        }
                        else
                        {
                            _tempvalue = Totalwinrate_allrate(compid, smday, acid);//不是针对个人所以UserID处置空 
                            if (_tempvalue < (Convert.ToDecimal(row["totalwinrate"])) / 100)
                            {
                                _returnvaltemp = GetAwinfoString(acid);
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToBoolean(row["toself"]))
                        {
                            string day = smday.ToString("yyyy-MM-dd");
                            int smsday = int.Parse(GetSingleData("count(id) as  cnt", "TJ_Activity_SMInfo", "compid=" + compid + " and userid=" + userid + " and smtm>='" + day + "'"));
                            int lqsday = int.Parse(GetSingleData("count(id) as  cnt", "TJ_Activity_Win_" + smday.Year, "compid=" + compid + " and  UserID=" + userid + " and acid=" + acid + " and gettm>='" + day + "'"));
                            int xdcs = int.Parse(row["maxtimeself"].ToString());

                            int lh = smsday / xdcs;
                            int lhint = smsday % xdcs;
                            if (lh == lqsday && lhint > 0)
                            {
                                //有奖
                                int sjbj = _rperson.Next(0, 1);
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
                        else
                        {
                            string labcodestring = LabelCodeString(boxlabelcode, tablename);
                            int wintms = GetWinNumber(createYear, labcodestring);
                            int packagenum = int.Parse(row["packagenum"].ToString());
                            int packgeetotalnum = labcodestring.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length - 1;
                            int scantms = GetScanTotalTimes(DateTime.Now, createYear, labcodestring, compid);
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
            _dtable.Dispose();
            _myAda.Dispose();
        }
        return _returnvaltemp.Equals(string.Empty) ? "0" : _returnvaltemp;
    }

    private string GetAsidbyScid(string acid)
    {
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select ASID from TJ_Activity  where id=" + acid;
            _myAda = new SqlDataAdapter(_tempsqlstring, conn);
            _dtable = new DataTable();
            _myAda.Fill(_dtable);
            _myAda.Dispose();
            _returnvaltemp = "0";
            if (_dtable.Rows.Count > 0)
            {
                _returnvaltemp = _dtable.Rows[0][0].ToString();
            }
            _dtable.Dispose();
            return _returnvaltemp;
        }
    }

    private int GetWinNumber(string acyear, string labcodestring)
    {
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select count(id) from TJ_Activity_Win_" + acyear + " where  iszj=1 and  codestr in (" + labcodestring + ")";
            _myAda = new SqlDataAdapter(_tempsqlstring, conn);
            _dtable = new DataTable();
            _myAda.Fill(_dtable);
            if (_dtable.Rows.Count > 0)
            {
                return int.Parse(_dtable.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }
    }

    private string LabelCodeString(string boxLabelCode01, string tablename)
    {
        using (var conn = GetConnectionWl)
        {
            _tempsqlstring = "SELECT labelcode  FROM " + tablename + "  where blabelcode='" + boxLabelCode01 + "'";
            _dtable = new DataTable();
            _myAda = new SqlDataAdapter(_tempsqlstring, conn);
            _myAda.Fill(_dtable);
            _myAda.Dispose();
            _returnvaltemp = string.Empty;
            if (_dtable.Rows.Count > 0)
            {
                _returnvaltemp = boxLabelCode01;
                foreach (DataRow row in _dtable.Rows)
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
    private DataTable GetShouHuoConfirmInfo(string boxlabelcode, string tablename, string compid)
    {
        using (var conn = GetConnectionWl)
        {
            _tempsqlstring = "select ProID as pid,ParentID as agentid,AcceptAgentID terminalid from " + tablename + " where CompID=" + compid + " and AgentTypeID=3 and BoxLabel='" + boxlabelcode + "'";
            _dtable = new DataTable();
            _myAda = new SqlDataAdapter(_tempsqlstring, conn);
            _myAda.Fill(_dtable);
            _myAda.Dispose();
            if (_dtable.Rows.Count > 0)
            {
                return _dtable;
            }
            return null;
        }
    }

    /// <summary>
    /// 通过B标，查出A标
    /// </summary>
    /// <param name="bcode"></param>
    /// <returns></returns>
    public string GetACodeBY_BCode(string bcode)
    {
        using (var conn = GetConnectionWl)
        {
            _tempsqlstring = "select tablenameinfo  from TB_LabelCodeInfo where startvalue<='" + bcode + "' and  endvalue>='" + bcode + "'";
            _dtable = new DataTable();
            _myAda = new SqlDataAdapter(_tempsqlstring, conn);
            _myAda.Fill(_dtable);
            _myAda.Dispose();
            string acode = "null";
            if (_dtable.Rows.Count > 0)
            {
                foreach (DataRow dr in _dtable.Rows)
                {
                    string tname = dr[0].ToString();
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    _myCmd = new SqlCommand("select ACode  from " + tname + "_BA where BCode='" + bcode + "'", conn);
                    object result = _myCmd.ExecuteScalar();
                    _myCmd.Dispose();
                    if (result != null)
                    {
                        acode = result.ToString();
                    }
                }
                conn.Close();
                _dtable.Dispose();
                return acode;
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
        using (var conn = GetConnectionWl)
        {
            _tempsqlstring = "select tablenameinfo  from TB_LabelCodeInfo where startvalue<='" + cpid + "' and  endvalue>='" + cpid + "'";
            _dtable = new DataTable();
            _myAda = new SqlDataAdapter(_tempsqlstring, conn);
            _myAda.Fill(_dtable);
            _myAda.Dispose();
            string rcpid = "";
            if (_dtable.Rows.Count > 0)
            {

                foreach (DataRow dr in _dtable.Rows)
                {
                    string tname = dr[0].ToString();
                    _myCmd = new SqlCommand("select BoxLabel01  from " + tname + "_BT where BottleLabel='" + cpid + "' UNION select BoxLabel01  from " + tname + "_BT where BoxLabel01='" + cpid + "'", conn);
                    object result = _myCmd.ExecuteScalar();
                    _myCmd.Dispose();
                    if (result != null)
                    {
                        string boxid = result.ToString();
                        _myCmd = new SqlCommand("select BoxLabel01  from " + tname + "_BX where BoxLabel='" + boxid + "'", conn);
                        result = _myCmd.ExecuteScalar();
                        _myCmd.Dispose();
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
                        _myCmd = new SqlCommand("select BoxLabel01  from " + tname + "_BX where BoxLabel01='" + cpid + "'", conn);
                        result = _myCmd.ExecuteScalar();
                        _myCmd.Dispose();
                        if (result != null)
                        {
                            rcpid = result + "," + tname;
                            break;
                        }
                        rcpid = "null";
                    }
                }
                conn.Close();
                _dtable.Dispose();
                return rcpid;
            }
            conn.Close();
            return "null";
        }
    }
    /// <summary>
    /// 返回箱标号码和表名，以“,"隔开
    /// </summary>
    /// <param name="cpid"></param>
    /// <returns></returns>
    public string GetTableAndCpid(string cpid)
    {
        using (var conn = GetConnectionWl)
        {
            _tempsqlstring = "select tablenameinfo  from TB_LabelCodeInfo where startvalue<='" + cpid + "' and  endvalue>='" + cpid + "'";
            _dtable = new DataTable();
            _myAda = new SqlDataAdapter(_tempsqlstring, conn);
            _myAda.Fill(_dtable);
            _myAda.Dispose();
            string rcpid = "";
            if (_dtable.Rows.Count > 0)
            {
                var dbt = new DataTable();
                foreach (DataRow dr in _dtable.Rows)
                {
                    string tname = dr[0].ToString();
                    _myAda = new SqlDataAdapter("select BoxLabel01  from " + tname + "_BT where BottleLabel='" + cpid + "' UNION select BoxLabel01  from " + tname + "_BT where BoxLabel01='" + cpid + "'", conn);
                    dbt = new DataTable();
                    _myAda.Fill(dbt);
                    _myAda.Dispose();
                    if (dbt.Rows.Count > 0)
                    {
                        string boxid = dbt.Rows[0][0].ToString();
                        _myAda = new SqlDataAdapter("select BoxLabel01  from " + tname + "_BX where BoxLabel='" + boxid + "'", conn);
                        var dbx = new DataTable();
                        _myAda.Fill(dbx);
                        _myAda.Dispose();
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
                        _myAda = new SqlDataAdapter("select BoxLabel01  from " + tname + "_BX where BoxLabel01='" + cpid + "'", conn);
                        var dbx = new DataTable();
                        _myAda.Fill(dbx);
                        _myAda.Dispose();
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
                _dtable.Dispose();
                return rcpid;
            }
            conn.Close();
            return "null";
        }
    }

    /// <summary>
    /// 计算总体概率
    /// </summary>
    /// <param name="compid"></param>
    /// <param name="smday"></param>
    /// <param name="acid"></param>
    /// <returns></returns>
    private decimal Totalwinrate_allrate(string compid, DateTime smday, string acid)
    {
        string day = smday.ToString("yyyy-MM-dd");
        string scantimesday = GetSingleData("count(id) as  cnt", "TJ_Activity_SMInfo", "smdt>='" + day + "' and compid=" + compid);
        string lqtimesday = GetSingleData("count(id) as  cnt", "TJ_Activity_Win_" + smday.Year, "gettm>='" + day + "' and acid=" + acid + " and compid=" + compid);
        if (scantimesday != "no" && lqtimesday != "no" && scantimesday != "0")
        {
            return decimal.Parse(lqtimesday.ToString(CultureInfo.InvariantCulture)) / int.Parse(scantimesday);
        }
        else
        {
            return 0;
        }
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
    /// 根据套标查询扫码次数
    /// </summary>
    /// <param name="smday"></param>
    /// <param name="acYear"></param>
    /// <param name="codestring"></param>
    /// <param name="compid"></param>
    /// <returns></returns>
    private int GetScanTotalTimes(DateTime smday, string acYear, string codestring, string compid)
    {
        _returnvaltemp = "0";
        _tempsqlstring = string.Empty;
        using (var conn = GetConnection)
        {
            if (smday.Year.Equals(int.Parse(acYear)))
            {
                if (codestring.Contains(","))
                {
                    //_tempsqlstring = "select count(id) as cnt from TJ_SMinfo_" + smday.Year + "  where CompID=" + compid + " and LabelCode in (" + codestring + ")";
                    _tempsqlstring = "select count(id) as cnt from TJ_Activity_SMInfo where compid=" + compid + " and codenm in (" + codestring + ")";
                }
                else
                {
                    _tempsqlstring = "select count(id) as cnt from TJ_Activity_SMInfo where CompID=" + compid + " and codenm='" + codestring + "'";
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
                            //_tempsqlstring = "select count(id) as cnt from TJ_SMinfo_" + y + "  where CompID=" + compid + " and LabelCode in (" + codestring + ")";
                            _tempsqlstring = "select count(id) as cnt from TJ_Activity_SMInfo where compid=" + compid + " and codenm in (" + codestring + ")";
                        }
                        else
                        {
                            //_tempsqlstring += " UNION select count(id) as cnt from TJ_SMinfo_" + y + "  where CompID=" + compid + " and LabelCode in (" + codestring + ")";
                            _tempsqlstring += " UNION select count(id) as cnt from TJ_Activity_SMInfo  where compid=" + compid + " and codenm in (" + codestring + ")";
                        }
                    }
                    else
                    {
                        if (_tempsqlstring.Equals(string.Empty))
                        {
                            //_tempsqlstring = "select count(id) as cnt from TJ_SMinfo_" + y + "  where CompID=" + compid + " and LabelCode='" + codestring + "'";
                            _tempsqlstring = "select count(id) as cnt from TJ_Activity_SMInfo  where compid=" + compid + " and codenm='" + codestring + "'";
                        }
                        else
                        {
                            //_tempsqlstring += " UNION select count(id) as cnt from TJ_SMinfo_" + y + "  where CompID=" + compid + " and LabelCode='" + codestring + "'";
                            _tempsqlstring += " UNION select count(id) as cnt from TJ_Activity_SMInfo  where compid=" + compid + " and codenm='" + codestring + "'";
                        }
                    }
                }
            }
            _myAda = new SqlDataAdapter(_tempsqlstring, conn);
            DataTable dt = new DataTable();
            _myAda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                _returnvaltemp = dt.Rows[0]["cnt"].ToString();
            }
            dt.Dispose();
            _myAda.Dispose();
        }
        return int.Parse(_returnvaltemp);
    }

    private string Test()
    {
        string[] array = ("sdddddd,ddssss,dddddd,ssssss,dddddd,eeeeeee").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        return String.Join(" UNION ", array);
    } 

    private string GetAwinfoString(string acid)
    {
        _returnvaltemp = string.Empty;
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select awtype,awid,prizevalue,percentvl,awnm from TJ_Activity_Prizes where acid=" + acid;
            _myAda = new SqlDataAdapter(_tempsqlstring, conn);
            _dtable = new DataTable();
            _myAda.Fill(_dtable);
            if (_dtable.Rows.Count > 0)
            {
                foreach (DataRow row in _dtable.Rows)
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
            _myCmd = new SqlCommand(sqlstr, myConn);
            object result = _myCmd.ExecuteScalar();
            _myCmd.Dispose();
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

    /// <summary>
    /// 根据瓶标返回箱标号码、发货时间、客户单号的数组
    /// </summary>
    /// <param name="tablenm"></param>
    /// <param name="cpid"></param>
    /// <returns></returns>
    public string[] GetBoxLabelCode(string tablenm, string cpid)
    {
        using (var conn = GetConnectionWl)
        {
            string s = "select blabelcode,fhdate,khddh from " + tablenm + "  where labelcode='" + cpid + "'";
            string[] rearray = new string[3];
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            //如果conn没有打开,需要conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = s;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            dr = cmd.ExecuteReader();
            if (!dr.HasRows)
            {
                rearray = null;
            }
            else
            {
                dr.Read();
                rearray[0] = dr["blabelcode"].ToString();
                rearray[1] = dr["fhdate"].ToString();
                rearray[2] = dr["khddh"].ToString();
            }
            conn.Close();
            dr.Dispose();
            cmd.Dispose();
            return rearray;
        }
    }

    public bool IsScanedBoxLabelCode(string tablenm, string cpid)
    {
        using (var conn = GetConnectionWl)
        {
            bool isboxlabel;
            string s = "select top 1 labelcode from " + tablenm + "  where blabelcode='" + cpid + "'";
            _myAda = new SqlDataAdapter(s, conn);
            _dtable = new DataTable();
            _myAda.Fill(_dtable);
            if (_dtable != null && _dtable.Rows.Count > 0)
            {
                isboxlabel = true;
            }
            else
            {
                isboxlabel = false;
            }
            _dtable.Dispose();
            _myAda.Dispose();
            return isboxlabel;
        }
    }

    /// <summary>
    /// 返回箱标号码和表名，以“,"隔开
    /// </summary>
    /// <param name="cpid"></param>
    /// <returns></returns>
    public string GetTableAndCpidNew(string cpid)
    {
        using (var conn = GetConnectionWl)
        {
            string prefix = cpid.Substring(0, 4);
            string s = "" +
                "select 'W" + prefix + "_00' tbname,BoxLabel01 from W" + prefix + "_00 where boxlabel01='" + cpid + "' " +
                " union " +
                "select 'W" + prefix + "_01' tbname,BoxLabel01 from W" + prefix + "_01 where boxlabel01='" + cpid + "' " +
                " union " +
                "select 'W" + prefix + "_02' tbname,BoxLabel01 from W" + prefix + "_02 where boxlabel01='" + cpid + "' " +
                " union " +
                "select 'W" + prefix + "_03' tbname,BoxLabel01 from W" + prefix + "_03 where boxlabel01='" + cpid + "' " +
                " union " +
                "select 'W" + prefix + "_00' tbname,BoxLabel01 from W" + prefix + "_00_BX where boxlabel='" + cpid + "' " +
                " union " +
                "select 'W" + prefix + "_01' tbname,BoxLabel01 from W" + prefix + "_01_BX where boxlabel='" + cpid + "' " +
                " union " +
                "select 'W" + prefix + "_02' tbname,BoxLabel01 from W" + prefix + "_02_BX where boxlabel='" + cpid + "' " +
                " union " +
                "select 'W" + prefix + "_03' tbname,BoxLabel01 from W" + prefix + "_03_BX where boxlabel='" + cpid + "' " +
                " union " +
                "select 'W" + prefix + "_00' tbname,BoxLabel01 from W" + prefix + "_00_BT where BottleLabel='" + cpid + "' " +
                " union " +
                "select 'W" + prefix + "_01' tbname,BoxLabel01 from W" + prefix + "_01_BT where BottleLabel='" + cpid + "' " +
                " union " +
                "select 'W" + prefix + "_02' tbname,BoxLabel01 from W" + prefix + "_02_BT where BottleLabel='" + cpid + "' " +
                " union " +
                "select 'W" + prefix + "_03' tbname,BoxLabel01 from W" + prefix + "_03_BT where BottleLabel='" + cpid + "' ";
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            //如果conn没有打开,需要conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = s;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            dr = cmd.ExecuteReader();
            string rcpid;
            if (!dr.HasRows)
            {
                rcpid = "null";
            }
            else
            {
                dr.Read();
                rcpid = dr["BoxLabel01"] + "," + dr["tbname"];
            }
            conn.Close();
            dr.Dispose();
            cmd.Dispose();
            return rcpid;
        }
    }

    private void AddNoConfirmLabelInfo(string labelcode, string boxlablecode, string tablename,string compid,string jxsid,string terminalid)
    {
        using (_myConn = GetConnectionWl)
        {
            if (_myConn.State != ConnectionState.Open)
            {
                _myConn.Open();
            }
            _myCmd = new SqlCommand("insert into "+tablename+"(labelcode,blabelcode,khddh,compid,jxsid,terminalid) values('"+labelcode+"','"+boxlablecode+"','0000',"+compid+","+jxsid+","+terminalid+")",_myConn);
            _myCmd.ExecuteNonQuery();
            _myCmd.Dispose();
        }
    }
}