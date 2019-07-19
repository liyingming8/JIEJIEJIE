<%@ WebHandler Language="C#" Class="getactivityinfoforjingxiaoshang" %>
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

public class getactivityinfoforjingxiaoshang : IHttpHandler
{

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (!string.IsNullOrEmpty(context.Request.QueryString["unid"]) && !string.IsNullOrEmpty(context.Request.QueryString["agentid"]) && !string.IsNullOrEmpty(context.Request.QueryString["proid"]) && !string.IsNullOrEmpty(context.Request.QueryString["fhdate"]) && !string.IsNullOrEmpty(context.Request.QueryString["faceto"]))
        {
            string compid = context.Request.QueryString["unid"];
            string agentid = context.Request.QueryString["agentid"];
            string proid = context.Request.QueryString["proid"];
            string fhdate = context.Request.QueryString["fhdate"];
            string faceto = context.Request.QueryString["faceto"];
            string khddh = "";
            if(!string.IsNullOrEmpty(context.Request.QueryString["khddh"]))
            {
                khddh = context.Request.QueryString["khddh"];
            }
            string acid = CheckActivityInfo(compid, faceto,proid,agentid);
            if (acid.Equals("0"))
            {
                context.Response.Write("0");
            }
            else
            {
                string temp = GetAcStrategyString(acid);
                context.Response.Write(temp);
                //string acidDate = GetPermitPrizeActivityID(acidstring, khddh,Convert.ToDateTime(fhdate).ToString("yyyy-MM-dd"));
                //string[] acidcreatearray = acidDate.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //if (acidcreatearray.Length.Equals(2))
                //{
                //    string temp = GetAcStrategyString(acidcreatearray[0]);
                //    context.Response.Write(temp);
                //}
                //else
                //{
                //    context.Response.Write("0");
                //}
            }
        }
        else
        {
            context.Response.Write("0");
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
    public string CheckActivityInfo(string compid,string faceto,string prodid,string agentid)
    {
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select id from TJ_Activity ac where ac.CompID=" + compid + " and (ac.prodnum=0 or (select count(id) from TJ_Activity_Product p where p.prodid=" + prodid + " and p.acid=ac.id)>0) and (ac.agentnum=0 or (select count(ag.id) from TJ_Activity_Agent ag where ag.agentid=" + agentid + " and ag.acid=ac.id)>0) and (ac.terminalnum=0 or (select count(at.id) from TJ_Activity_Terminal at where at.terminalid=" + agentid + " and at.acid=ac.id)>0) and ac.FaceTo=" + faceto + " and '" + DateTime.Now.ToString("yyyy-MM-dd") + "' between ac.STM and ac.ETM order by priority desc";
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

    public string CheckActivityInfo(string compid,string faceto,string prodid,string agentid,string khddh,string fahuodate)
    {
        using (var conn = GetConnection)
        {
            _tempsqlstring = "select id from TJ_Activity ac where ac.CompID=" + compid + " and ((khddh in null or khddh='') or khddh='"+khddh+"') and (anyfahuodate=0 or ('"+fahuodate+"' between sfahuodate and efahuodate)) and (ac.prodnum=0 or (select count(id) from TJ_Activity_Product p where p.prodid=" + prodid + " and p.acid=ac.id)>0) and (ac.agentnum=0 or (select count(ag.id) from TJ_Activity_Agent ag where ag.agentid=" + agentid + " and ag.acid=ac.id)>0) and (ac.terminalnum=0 or (select count(at.id) from TJ_Activity_Terminal at where at.terminalid=" + agentid + " and at.acid=ac.id)>0) and ac.FaceTo=" + faceto + " and '" + DateTime.Now.ToString("yyyy-MM-dd") + "' between ac.STM and ac.ETM";
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

    private string GetAcStrategyString(string acid)
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
                _returnvaltemp = GetAwinfoString(acid);
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