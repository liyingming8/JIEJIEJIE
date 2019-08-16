using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Npgsql;
using System.Data;
using System.Data.SqlClient;
using TJ.BLL;
using TJ.DBUtility;
using TJ.Model;

/// <summary>
/// commfrank 的摘要说明
/// </summary>
public class commfrank
{
    private SqlConnection sqlconn;
    private SqlConnection sqlconnwl;
    private NpgsqlConnection sqlconncrm;
    BTJ_Activity_Strategy btjActivity = new BTJ_Activity_Strategy();
    public commfrank()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        // 
    }
    SqlConnection myConn;
    NpgsqlConnection myConnpg;
    string str = "";
    public SqlConnection GetConnection()
    {
        str = ConnectionInfo.SqlServerConnString;
        myConn = new SqlConnection(str);
        return myConn; 
    }

    public SqlConnection GetConnectionWuLiu()
    {
        str = ConnectionInfo.SqlServerConnStringWuLiu;
        myConn = new SqlConnection(str);
        return myConn;
    }

    public NpgsqlConnection GetConnectionCRM()
    {
        str = ConnectionInfo.PGDBConnStrCRM;
        myConnpg = new NpgsqlConnection(str);
        return myConnpg;
    }

    public string GetPermitManagerIDString(string userid)
    { 
        string tempstring = "";
        DataTable dttemp = _tab.ExecuteQuery("select userid from TJ_XF_ManageAuthorInfo where fathoruserid=" + userid, null);
        if (dttemp.Rows.Count > 0)
        {
            tempstring = dttemp.Rows.Cast<DataRow>().Aggregate(tempstring, (current, row) => current + ("," + row[0]));
        }
        else
        {
            tempstring = "";
        }
        if (tempstring.StartsWith(","))
        {
            tempstring = tempstring.Substring(1);
        }
        dttemp.Dispose();
        return tempstring;
    }

    public string GetCompIDStringForCityManager(string manageridstring)
    {
        string temp = string.Empty;
        DataTable dt =_tab.ExecuteQuery("select CompID from TJ_RegisterCompanys where ManagerUserID in (" + manageridstring+")", null);
        if (dt.Rows.Count > 0)
        {
            foreach(DataRow dr in dt.Rows)
            {
                temp += "," + dr["CompID"].ToString();
            }
        }
        if(temp.StartsWith(","))
        {
            temp = temp.Substring(1); 
        }
        dt.Dispose();
        return temp;
    }

    public static int nonegertive(int invalue)
    {
        if (invalue < 0)
        {
            return 0;
        }
        return invalue;
    }

    string _sqlstring = "";
    SqlDataAdapter _sqlData;
    NpgsqlDataAdapter _pgsqldata;
    DataTable _dttemp;
    string _valuestr = "";
    /// <summary>
    /// 返回数据库表的值
    /// </summary>
    /// <param name="db">数据库</param>
    /// <param name="tablename">表名</param>
    /// <param name="idfield">ID字段名称</param>
    /// <param name="returnfied">返回字段</param>
    /// <param name="otherfilterstring">过滤条件</param>
    /// <param name="idvalue">ID值</param>
    /// <returns></returns>
    public string GetValueByID(string db,string tablename,string idfield,string returnfied,string otherfilterstring,string idvalue)
    {
        _valuestr = "";
        _sqlstring = "select " + returnfied + " from " + tablename + " where " + idfield + "=" + idvalue + (string.IsNullOrEmpty(otherfilterstring) ? "" : " and " + otherfilterstring);
        switch (db.ToLower().Trim())
        {
            case "nm":
                using (sqlconn = GetConnection())
                {
                    if(sqlconn.State!= ConnectionState.Open)
                    {
                        sqlconn.Open();
                    }
                    _sqlData = new SqlDataAdapter(_sqlstring, sqlconn);
                    _dttemp = new DataTable();
                    _sqlData.Fill(_dttemp);
                    if(_dttemp.Rows.Count>0)
                    {
                        _valuestr = _dttemp.Rows[0][0].ToString();
                    }
                    _sqlData.Dispose();
                } 
            break;
            case "wl":
                using (sqlconnwl=GetConnectionWuLiu())
                {
                    if (sqlconnwl.State != ConnectionState.Open)
                    {
                        sqlconnwl.Open();
                    }
                    _sqlData = new SqlDataAdapter(_sqlstring, sqlconnwl);
                    _dttemp = new DataTable();
                    _sqlData.Fill(_dttemp);
                    if (_dttemp.Rows.Count > 0)
                    {
                        _valuestr = _dttemp.Rows[0][0].ToString();
                    }
                    _sqlData.Dispose();
                }
                break;
            case "crm":
                using (sqlconncrm=GetConnectionCRM())
                {
                    if (sqlconncrm.State != ConnectionState.Open)
                    {
                        sqlconncrm.Open();
                    }
                    _pgsqldata =new NpgsqlDataAdapter(_sqlstring,sqlconncrm);
                    _dttemp = new DataTable();
                    _pgsqldata.Fill(_dttemp);
                    if (_dttemp.Rows.Count > 0)
                    {
                        _valuestr = _dttemp.Rows[0][0].ToString();
                    }
                    _pgsqldata.Dispose();
                }
                break;
        }
        _dttemp.Dispose();
        return _valuestr;
    }

    public string GetActivityStrategyDiscription(int id,bool withname)
    {
        MTJ_Activity_Strategy mtjActivityStrategy = btjActivity.GetList(id);
        if (mtjActivityStrategy.totalwinrate > 0)
        {
            if (withname)
            {
                return mtjActivityStrategy.strategyname+"："+"总体中奖率：" + mtjActivityStrategy.totalwinrate + "%";
            }
            return "总体中奖率：" + mtjActivityStrategy.totalwinrate + "%";
        }
        if (mtjActivityStrategy.topackage)
        {
            if (withname)
            {
                return mtjActivityStrategy.strategyname + "：" + "每件包装：" + mtjActivityStrategy.packagenum + "个奖项";
            }
            return "每件包装：" + mtjActivityStrategy.packagenum + "个奖项";
        }
        if (mtjActivityStrategy.toself)
        {
            if (withname)
            {
                return mtjActivityStrategy.strategyname + "："+"消费者：" + mtjActivityStrategy.maxtimeself + "次有效扫码后必中";
            }
            return "消费者：" + mtjActivityStrategy.maxtimeself + "次有效扫码后必中";
        }
        return "";
    }

    #region Json 字符串 转换为 DataTable数据集合
    /// <summary>
    /// Json 字符串 转换为 DataTable数据集合
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public DataTable ToDataTable(string json)
    {
        var dataTable = new DataTable();  //实例化
        DataTable result;
        try
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
            var arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
            if (arrayList.Count > 0)
            {
                foreach (Dictionary<string, object> dictionary in arrayList)
                {
                    if (!dictionary.Keys.Any())
                    {
                        result = dataTable;
                        return result;
                    }
                    if (dataTable.Columns.Count == 0)
                    {
                        foreach (string current in dictionary.Keys)
                        {
                            dataTable.Columns.Add(current, dictionary[current].GetType());
                        }
                    }
                    DataRow dataRow = dataTable.NewRow();
                    foreach (string current in dictionary.Keys)
                    {
                        dataRow[current] = dictionary[current];
                    }
                    dataTable.Rows.Add(dataRow); //循环添加行到DataTable中 
                }
            }
        }
        catch
        {
            return null;
        }
        result = dataTable;
        dataTable.Dispose();
        return result;
    }
    TabExecutewuliu _tabwuliu = new TabExecutewuliu();
    readonly InternetHandle _internet = new InternetHandle();
    TabExecute _tab = new TabExecute();
    /// <summary>
    /// 按天、产品和客户单号取出终端店上传记录之后，去维护经销商的积分表，达到终端店按天存储积分的效果,最后刷新上传记录的状态
    /// </summary>
    /// <param name="compid">公司ID</param>
    /// <param name="terminalid">终端店ID</param>
    public void FreshTerminalIntegral(string compid,int terminalid)
    {
        DataTable dttemp = _tabwuliu.ExecuteNonQuery(
            "SELECT COUNT(ID) cnt,AcceptAgentID,ProID,AcceptDay,KhDDH FROM AgentAcceptInfo_2019 where CompID=" +
            compid + (terminalid.Equals(0) ? "" : " and AcceptAgentID="+terminalid) + " and AgentTypeID=3 and isprized=0 and isexception=0 group by AcceptAgentID,ProID,AcceptDay,KhDDH");
        if (dttemp.Rows.Count > 0)
        {
            foreach (DataRow row in dttemp.Rows)
            {
                string requeststring = "http://os.china315net.com/ajax/getactivityinfoforjingxiaoshang.ashx?unid=" +
                                       compid + "&agentid=" + row["AcceptAgentID"] + "&proid=" + row["ProID"] +
                                       "&faceto=3&fhdate=" +
                                       Convert.ToDateTime(row["AcceptDay"]).ToString("yyyy-MM-dd") +
                                       (row["KhDDH"].ToString().Equals("") ? "" : "&khddh=" + row["KhDDH"]);
                string temp = _internet.GetUrlData(requeststring);
                if (!temp.Equals("0"))
                {
                    JArray jsonarray = JArray.Parse(temp);
                    if (jsonarray.Count > 0)
                    {
                        JObject obj = JObject.Parse(jsonarray[0].ToString());
                        string tempvl = _tab.ExecuteQueryForSingleValue("select id from TJ_Activity_JXS_Win where islq=0 and compid=" + compid + " and agentid=" + row["AcceptAgentID"] + " and awtypeid=" + obj["tpid"] + " and gettm='" + Convert.ToDateTime(row["AcceptDay"]).ToString("yyyy-MM-dd") + "' and wintypeid=1");
                        int t = 0;
                        if (string.IsNullOrEmpty(tempvl) || tempvl.Equals("0"))
                        {
                            t = _tab.ExecuteNonQuery("INSERT INTO TJ_Activity_JXS_Win(compid,agentid,awtypeid,winreason,prizevl,prizeintro,gettm,confirmtm,jxstypeid,yearnm,monthnm,daynm) VALUES(" + compid + "," + row["AcceptAgentID"] + "," + obj["tpid"] + ",'扫码积分'," + Convert.ToDecimal(obj["prvl"].ToString()) * Convert.ToInt32(row["cnt"]) + ",'" + obj["awnm"] + "','" + Convert.ToDateTime(row["AcceptDay"]).ToString("yyyy-MM-dd") + "','" + DateTime.Now + "',3,"+ Convert.ToDateTime(row["AcceptDay"]).Year + ","+ Convert.ToDateTime(row["AcceptDay"]).Month+ "," + Convert.ToDateTime(row["AcceptDay"]).Day + ")", null);
                        }
                        else
                        {
                            t = _tab.ExecuteNonQuery("update TJ_Activity_JXS_Win set prizevl=prizevl+" + Convert.ToDecimal(obj["prvl"].ToString()) * Convert.ToInt32(row["cnt"])+" where id="+tempvl, null);
                        } 
                        if (t > 0)
                        {
                            _tabwuliu.ExecuteQuery(
                                "update AgentAcceptInfo_2019 set isprized=1 where AcceptAgentID=" + row["AcceptAgentID"] +
                                " and  ProID=" + row["ProID"] + " and AcceptDay='" +
                                Convert.ToDateTime(row["AcceptDay"]).ToString("yyyy-MM-dd") + "'" + (row["KhDDH"].ToString().Length > 0 ? " and KhDDH='" +
                                row["KhDDH"] + "'" : ""), null);
                        }
                    }
                }
            }
        }
        dttemp.Dispose();
    } 
    #endregion
}