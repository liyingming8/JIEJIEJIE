using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// mymj 的摘要说明
/// </summary>
public class mymj
{
    public SqlDataAdapter ada;
    public SqlDataAdapter ada1;
    public SqlCommand cmd, cmddel;
    public DataSet Mydataset;
    public DataSet Mydataset1;
    public DataSet Mydataset2;
    public SqlDataAdapter Pingada;
    public DataSet PingDataSet;
    string sql = string.Empty;

    public mymj()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public SqlConnection GetConnectionWL
    {
        get
        {
            string str = ConfigurationManager.ConnectionStrings["ConnectionStringAccounts"].ToString();
            var myConn = new SqlConnection(str);
            return myConn;
        }
    }
    public SqlConnection GetConnectioBD
    {
        get
        {
            string str = ConfigurationManager.ConnectionStrings["ConnectionStringmymj"].ToString();
            var myConn = new SqlConnection(str);
            return myConn;
        }
    }

    public DataTable ReturnFhInfo(string coid, ref int flag)
    {
        using (var myConn = GetConnectionWL)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            if (coid == "0")
            {
                sql = "SELECT   distinct Dlyndx.NUMBER,Dlystock.btypeid,Btype.typeId ,Btype.FullName,Btype.Name FROM Dlystock LEFT OUTER JOIN ptype ON Dlystock.PtypeId = ptype.typeId LEFT OUTER JOIN  Dlyndx ON Dlystock.Vchcode = Dlyndx.Vchcode LEFT OUTER JOIN[GuanjiaPo].[dbo].[Btype] on Dlystock.btypeid = Btype.typeId ";
                flag = 0;
               
            }
            else
            {
                if (coid.Substring(0, 3) == "CCK")
                {
                    sql = "SELECT     Dlyndx.NUMBER, ptype.UserCode , ptype.FullName, Dlyndx.SaveTime,Dlystock.PtypeId, Dlystock.Qty, Dlystock.Vchcode, Dlystock.btypeid, Dlystock.ktypeid,  Dlystock.comment, Dlystock.usedtype, Dlystock.UserDefined01, Dlystock.UserDefined02, Dlystock.FreeDom01, Dlystock.FreeDom02, Dlystock.FreeDom03 FROM Dlystock LEFT OUTER JOIN ptype ON Dlystock.PtypeId = ptype.typeId LEFT OUTER JOIN Dlyndx ON Dlystock.Vchcode = Dlyndx.Vchcode where dlyndx.NUMBER='" + coid + "'";
                    flag = 1;
                }
                else
                {
                    sql = "SELECT   distinct Dlyndx.NUMBER FROM Dlystock LEFT OUTER JOIN ptype ON Dlystock.PtypeId = ptype.typeId LEFT OUTER JOIN  Dlyndx ON Dlystock.Vchcode = Dlyndx.Vchcode ";
                    flag = 0;
                }
            }

            Mydataset = new DataSet();
            ada = new SqlDataAdapter(sql, myConn);
            ada.Fill(Mydataset, "FhuoInfo");
            return Mydataset.Tables["FhuoInfo"];
        }
    }
    public DataTable ReturnCoidInfo(string coid)
    {
        using (var myConn = GetConnectionWL)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            switch (coid)
            {
                case "AgentID": sql = "select typeId, UserCode,FullName from Btype order by typeId"; break;
                case "Stock": sql = "select typeId, FullName,name from Stock order by typeId"; break;
                case "ProID": sql = "select typeId, UserCode,FullName  from ptype where left(typeid,15)='000010000100001' or left(typeid,15)='000010000100002' order by typeId"; break;
                default: sql = "select typeId, UserCode,FullName from Btype where typeId='11' "; break;//不返回查询信息

            }
            Mydataset = new DataSet();
            ada = new SqlDataAdapter(sql, myConn);
            ada.Fill(Mydataset, "FhuoInfo");
            return Mydataset.Tables["FhuoInfo"];
        }
    }


    /// <summary>
    /// 数据库后台处理基础表格第一步
    /// </summary>
    /// <param name="str">扫码枪出入的基本信息</param>
    /// <returns></returns>
    public string InsertTBProcessDataInfo(string str)//string ProductID, string MadeDate)
    {
        try
        {
            using (var myConn = GetConnectioBD)
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                string str1 = "insert into TB_ProcessDataInfo([CCDanHao],[KuaiDiDanHao],[Guid],[Code],[Czy],[DLS],[CP],[CK],[CompID],[CreateDate]) values(";
                string str2 = "";
                string str3 = "";
                string strBatchNo = DateTime.Now.ToString("yyyyMMddhhmmss");
                if (!str.Trim().Equals(""))
                {
                    string[] sArray = str.Split(',');
                    foreach (string s in sArray)
                    {
                        str2 += "'" + s + "',";
                    }
                    str3 = str1 + str2 + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "');";
                }
                if (!str3.Equals(""))
                {
                    SqlCommand cmd = new SqlCommand(str3, myConn);
                    int intc = cmd.ExecuteNonQuery();
                    if (intc > 0)
                    {
                        cmd.Dispose();
                        myConn.Close();
                        return "1";
                    }
                    else
                    {
                        cmd.Dispose();
                        myConn.Close();
                        return "0";
                    }
                }
                else
                {
                    return "0";

                }
            }
        }
        catch 
        { 
            return "0"; 
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="str">扫码枪出入的基本信息</param>
    /// <returns></returns>
    public string InsertTBProcessDataDealwith(string str)//string ProductID, string MadeDate)
    {
        try
        {
            using (var myConn = GetConnectioBD)
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                string str1 = "insert into TB_ProcessDealWithData([Guid],[CreateDate]) values(";
                string str2 = "";
                string str3 = "";
                if (!str.Trim().Equals(""))
                {
                   
                    string[] guid = str.Split(',');
                    foreach (string ss in guid)
                    {
                        str2 += "'" + ss + "','";
                    }
                    str3 = str1 + str2 + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                }
                if (!str3.Equals(""))
                {
                    SqlCommand cmd = new SqlCommand(str3, myConn);
                    int intc = cmd.ExecuteNonQuery();
                    if (intc > 0)
                    {
                        cmd.Dispose();
                        myConn.Close();

                        return "1";
                    }
                    else
                    {
                        cmd.Dispose();
                        myConn.Close();
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }
        }
        catch
        {
            return "0";

        }
    }
    /// <summary>
    /// 返回标签可能存放的表名，多个用“,”隔开
    /// </summary>
    /// <param name="labelcodestring"></param>
    /// <param name="sqlcon"></param>
    /// <returns></returns>
    public string gettableinfo(string startlabelcodestring, string endlabelstring, SqlConnection sqlcon)
    {
        string returnstring = "";
        try
        {
            using (var myConn = GetConnectioBD)
            {
                SqlDataAdapter sqlda = new SqlDataAdapter("select * from TB_LabelCodeInfo where startfournum='" + startlabelcodestring.Substring(0, 4).Trim() + "' or startfournum='" + endlabelstring.Substring(0, 4) + "'", sqlcon);
                DataTable dt = new DataTable();
                sqlda.Fill(dt);
                string startvalue = "";
                string endvalue = "";
                foreach (DataRow dr in dt.Rows)
                {
                    startvalue = dr["startvalue"].ToString().Trim();
                    endvalue = dr["endvalue"].ToString().Trim();
                    if (!startvalue.Equals("") && !endvalue.Equals(""))
                    {
                        if (((Convert.ToInt64(startlabelcodestring) >= Convert.ToInt64(startvalue)) && (Convert.ToInt64(startlabelcodestring) <= Convert.ToInt64(endvalue))) || ((Convert.ToInt64(endlabelstring) >= Convert.ToInt64(startvalue)) && (Convert.ToInt64(endlabelstring) <= Convert.ToInt64(endvalue))))
                        {
                            returnstring += dr["tablenameinfo"].ToString().Trim() + ",";
                        }
                    }
                }
                dt.Dispose();
                sqlda.Dispose();
                return returnstring;
            }
        }
        catch  
        {
            return "0";

        }
    }

    SqlDataAdapter sqlda = new SqlDataAdapter();
    DataTable dt = new DataTable();
    /// <summary>
    /// 根据返回的表名称确定在哪个表格中
    /// </summary>
    /// <param name="strTableName"></param>
    /// <param name="labelcode"></param>
    /// <param name="Flag"></param>
    /// <returns></returns>
    public string CheckLabelcodeIsOk(string strTableName, string labelcode, string Flag)//string ProductID, string MadeDate)
    {
        try
        {
            using (var myConn = GetConnectioBD)
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                string str ="0";
                string[] tablename = null;
                string sql = string.Empty;
                int tempvalueforcheck = 0;
                string sqlstringforcheckforreplace = string.Empty;
                if (!string.IsNullOrEmpty(strTableName))
                {
                    tablename = strTableName.Split(',');
                    if (tablename.Length > 0)
                    {
                        foreach (string name in tablename)
                        {
                            if (!string.IsNullOrEmpty(name))
                            {
                                switch (Flag.ToUpper())
                                {
                                    case "X":
                                        sql = "select count(*) as num from " + name + " where BoxLabel01='" + labelcode + "' and Flag='" + Flag + "'"; sqlstringforcheckforreplace = "select count(*) as num from " + name + "_BX where BoxLabel='" + labelcode + "'";
                                        sqlda = new SqlDataAdapter(sql, myConn); break;
                                    case "M":
                                        sql = "select count(*) as num from " + name + " where BoxLabel01='" + labelcode + "' and Flag='" + Flag + "'"; sqlstringforcheckforreplace = "select count(*) as num from " + name + "_BX where BoxLabel='" + labelcode + "'";
                                        sqlda = new SqlDataAdapter(sql, myConn); break;
                                    case "P":
                                        sql = "select count(*)  as num from " + name + " where BottleLabel='" + labelcode + "'"; sqlstringforcheckforreplace = "select count(*) as num from " + name + "_BX where BoxLabel='" + labelcode + "'";
                                        sqlda = new SqlDataAdapter(sql, myConn); break;

                                }
                                dt = new DataTable();
                                sqlda.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    tempvalueforcheck = int.Parse(dt.Rows[0]["num"].ToString().Trim());
                                    if (tempvalueforcheck > 0)
                                    {

                                         str = "1";
                                    }
                                    else
                                    {
                                        sqlda = new SqlDataAdapter(sqlstringforcheckforreplace, myConn);
                                        dt = new DataTable();
                                        sqlda.Fill(dt);
                                        if (dt.Rows.Count > 0)
                                        {
                                            tempvalueforcheck = int.Parse(dt.Rows[0]["num"].ToString().Trim());
                                            if (tempvalueforcheck > 0)
                                            {

                                                str = "1";
                                            }
                                            else
                                            {
                                                return "0";
                                            }
                                        }
                                        else
                                        {
                                            return "0";
                                        }
                                    }

                                }
                                else
                                {
                                    return "0";
                                }
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
                return str;
            }
        }
        catch 
        {
            return "0";
        }
    }



    ///// <summary>
    ///// 检测是否发货
    ///// </summary>
    ///// <param name="tablename"></param>
    ///// <param name="BoxLabelString"></param>
    ///// <returns></returns>
    //public bool CheckWfhISok(string tablename, string BoxLabelString)
    //{
    //    string sqlstring = "select count(BoxLabel01) as num from " + tablename + "_FH where BoxLabel01='" + BoxLabelString + "'and left(FHType,1)=3 and CompID=" + GetCookieCompID() + "";
    //    sqlcmdforcheck.CommandText = sqlstring;
    //    sqlcmdforcheck.Connection = sqlconcom;
    //    opensqlconn(sqlconcom);
    //    SqlDataReader sdr = sqlcmdforcheck.ExecuteReader();
    //    if (sdr.Read())
    //    {
    //        string tempvalue = sdr["num"].ToString().Trim();

    //        sdr.Close();
    //        if (Convert.ToInt32(tempvalue) > 0)
    //        {
    //            return true;
    //        }
    //        else
    //        {

    //            return false;
    //        }
    //    }
    //    else
    //    {
    //        sdr.Close();
    //        return false;
    //    }
    //}
   
}