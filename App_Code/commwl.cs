using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Security;
using TJ.Model;
using TJ.BLL;

/// <summary>
/// comm 的摘要说明
/// </summary>
public class commwl
{
    private static string _showmode = "0";
    private readonly SqlCommand sqlcmd = new SqlCommand();

    private readonly SqlConnection sqlconn =
        new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());

    private readonly SqlConnection sqlconnmarketing =
        new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString());

    private readonly DBClass db = new DBClass(_showmode);
    private string tempsqlstring = "";
    private readonly BTB_CompAgentInfo bcompagent = new BTB_CompAgentInfo();

    public commwl(string showmode)
    {
        _showmode = showmode;
    }

    public commwl()
    {
        _showmode = "0";
    }

    public string FHTypeName(string FHTYPE)
    {
        switch (FHTYPE)
        {
            case "1":
                return "入库";
            case "2":
                return "转库";
            case "3":
                return "发货";
            default:
                return "";
        }
    }

    public void opensqlconn(SqlConnection conn)
    {
        if (!conn.State.Equals(ConnectionState.Open))
        {
            conn.Open();
        }
    }

    public string ReturnFiledValue(string TableName, string EquelFiled, string Value, string ReturnFiled)
    {
        string returnvalue = "";
        tempsqlstring = "select " + ReturnFiled + " from " + TableName + " where " + EquelFiled + "='" + Value + "'";
        sqlcmd.CommandText = tempsqlstring;
        sqlcmd.Connection = sqlconn;
        opensqlconn(sqlconn);
        SqlDataReader sdr = sqlcmd.ExecuteReader();
        if (sdr.Read())
        {
            returnvalue = sdr[ReturnFiled].ToString().Trim();
        }
        sdr.Close();
        return returnvalue;
    }

    public string checkcodevalid(string lablecodestring)
    {
        string repnumstring = string.Empty;
        if (IsNumeric(lablecodestring))
        {
            if (!lablecodestring.Length.Equals(12))
            {
                repnumstring = "号码长度有误";
            }
        }
        else
        {
            repnumstring = "号码不全是数字";
        }
        return repnumstring;
    }

    #region 判断字符串是否是数字的

    /// <summary>
    /// 判断是否是数字
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>bool</returns>
    public bool IsNumeric(string str)
    {
        if (str == null || str.Length == 0)
        {
            return false;
        }
        ASCIIEncoding ascii = new ASCIIEncoding();
        byte[] bytestr = ascii.GetBytes(str);
        foreach (byte c in bytestr)
        {
            if (c < 48 || c > 57)
            {
                return false;
            }
        }
        return true;
    }

    #endregion 判断字符串是否是数字的

    public bool clearfile(string filename, bool needsendemail)
    {
        try
        {
            if (needsendemail)
            {
                SendEmailMessage("1215430569@qq.com", "648615839@qq.com", "jiamei2008", "天鉴物流发货数据",
                    "发货方式:使用盘点机文件<br>请在附件夹里打开下载!", filename);
            }
            FileInfo FileInfo = new FileInfo(filename);
            if (FileInfo.Exists)
            {
                FileInfo.Delete();
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    ///   <summary> 
    ///   MD5加密 
    ///   </summary> 
    ///   <param   name= "InputString "> 要加密的字串 </param> 
    ///   <returns> 密文 </returns> 
    public static string Md5hash_String(string InputString)
    {
        InputString = FormsAuthentication.HashPasswordForStoringInConfigFile(InputString, "MD5");
        return InputString;
    }

    public static bool SendEmailMessage(string ToEmailAddress, string FromEmailAddress, string FromEmailPassWord,
        string MailSubject, string MailContent, string AttachFilePath)
    {
        try
        {
            MailMessage message = new MailMessage();
            if (!AttachFilePath.Equals(""))
            {
                Attachment attach = new Attachment(AttachFilePath);
                message.Attachments.Add(attach);
            }
            message.From = new MailAddress(FromEmailAddress); //发送人邮箱地址，与smtp节点中的from值一致
            message.To.Add(new MailAddress(ToEmailAddress)); //接收人邮箱地址
            //message.To.Add(new System.Net.Mail.MailAddress("aswangyang@sina.com")); //多个收件人邮箱地址
            message.Subject = MailSubject;
            message.Body = MailContent;
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            SmtpClient smtpclient = new SmtpClient("smtp.qq.com", 25);
            smtpclient.Credentials = new NetworkCredential(FromEmailAddress, FromEmailPassWord); //参数分别是邮箱用户名和密码
            smtpclient.Send(message);
            return true;
        }
        catch
        {
            return false;
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
        //string returnstring = "";
        //opensqlconn(sqlcon);
        //for (int i = Convert.ToInt16(startlabelcodestring.Substring(0, 4)); i <= Convert.ToInt16(endlabelstring.Substring(0, 4)); i++)
        //{
        //    sqlda = new SqlDataAdapter("select * from TB_LabelCodeInfo where startfournum='" + i + "'", sqlcon);
        //    //sqlda = new SqlDataAdapter("select * from TB_LabelCodeInfo where startfournum='" + startlabelcodestring.Substring(0, 4).Trim() + "' or startfournum='" + endlabelstring.Substring(0, 4) + "'", sqlcon);
        //    dt = new DataTable();
        //    sqlda.Fill(dt);
        //    string startvalue = "";
        //    string endvalue = "";
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        startvalue = dr["startvalue"].ToString().Trim();
        //        endvalue = dr["endvalue"].ToString().Trim();
        //        if (!startvalue.Equals("") && !endvalue.Equals(""))
        //        {
        //            if (((Convert.ToInt64(startlabelcodestring) >= Convert.ToInt64(startvalue)) && (Convert.ToInt64(startlabelcodestring) <= Convert.ToInt64(endvalue))) || ((Convert.ToInt64(endlabelstring) >= Convert.ToInt64(startvalue))))
        //            {
        //                returnstring += dr["tablenameinfo"].ToString().Trim() + ",";
        //            }
        //        }
        //    }
        //}
        //dt.Dispose();
        //sqlda.Dispose();
        //return returnstring;


        string returnstring = "";
        opensqlconn(sqlcon);
        for (int i = Convert.ToInt16(startlabelcodestring.Substring(0, 4));
            i <= Convert.ToInt16(endlabelstring.Substring(0, 4));
            i++)
        {
            sqlda = new SqlDataAdapter("select * from TB_LabelCodeInfo where startfournum='" + i + "'", sqlcon);
            dt = new DataTable();
            sqlda.Fill(dt);
            string startvalue = "";
            string endvalue = "";
            foreach (DataRow dr in dt.Rows)
            {
                startvalue = dr["startvalue"].ToString().Trim();
                endvalue = dr["endvalue"].ToString().Trim();
                if (!startvalue.Equals("") && !endvalue.Equals(""))
                {
                    if (((Convert.ToInt64(startlabelcodestring) >= Convert.ToInt64(startvalue)) &&
                         (Convert.ToInt64(startlabelcodestring) <= Convert.ToInt64(endvalue))) ||
                        ((Convert.ToInt64(endlabelstring) >= Convert.ToInt64(startvalue))) &&
                        ((Convert.ToInt64(startlabelcodestring) <= Convert.ToInt64(startvalue))))
                    {
                        returnstring += dr["tablenameinfo"].ToString().Trim() + ",";
                    }
                }
            }
        }
        dt.Dispose();
        sqlda.Dispose();
        return returnstring;
    }

    /// <summary>
    /// 获取数组最大和最小值
    /// </summary>
    /// <param name="array"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public string MaxAndMinValue(string[] array, int index)
    {
        long max = Convert.ToInt64(array[0].Split(',')[3]);
        long min = Convert.ToInt64(array[0].Split(',')[3]);
        long temp = 0;
        for (int i = 0; i < array.Length; ++i)
        {
            temp = Convert.ToInt64(array[i].Split(',')[3]);
            if (temp > max)
                max = temp;
            else if (temp < min)
                min = temp;
        }
        return min + "," + max;
    }

    private string GetReturnSqlStringForBox(string tablename, string codevalue)
    {
        string returnstring = "select '' as 瓶标,BoxLabel01 as 箱标1,PID as 产品 from " + tablename + " where BoxLabel01='" +
                              codevalue + "'";
        return returnstring;
    }

    private string GetReturnSqlStringForReplaceBox(string tablename, string codevalue)
    {
        string BoxLabel01 = ReturnFiledValue(tablename + "_BX", "BoxLabel", codevalue, "BoxLabel01");
        if (BoxLabel01.Trim().Equals(""))
        {
            return "";
        }
        else
        {
            string returnstring = "select '' as 瓶标,BoxLabel01 as 箱标1,PID as 产品 from " + tablename +
                                  " where BoxLabel01='" + BoxLabel01 + "'";
            return returnstring;
        }
    }

    private string GetReturnSqlStringForBottle(string tablename, string codevalue)
    {
        string returnstring = "select p.BottleLabel as 瓶标,x.BoxLabel01 as 箱标1,PID as 产品 from " + tablename + " as x," +
                              tablename + "_BT as p where p.BottleLabel ='" + codevalue +
                              "' and x.BoxLabel01=p.BoxLabel01";

        return returnstring;
    }

    private SqlDataAdapter sqlda = new SqlDataAdapter();
    private DataTable dt = new DataTable();

    public string CreateAutoID(string strCompID, string strFlag)
    {
        opensqlconn(sqlconn);
        string strsqling = string.Empty;
        int intCode = 0; //将已有的值取出后转换成 INT16 后存放
        int intLen = 0; //从表中取出已有的字段值后的长度
        int intAdd1Len = 0; //增加一个数值后的长度
        string strConvert = string.Empty;
        switch (strFlag.ToUpper())
        {
            case "P":
                strsqling = "select Product_Code from TB_Products_Infor where CompID='" + strCompID +
                            "' order by Product_Code desc";
                sqlda = new SqlDataAdapter(strsqling, sqlconn);
                break;
            case "A":
                strsqling = "select Agent_Code from TJ_RegisterCompanys where CompID in (" +
                            GetAgentIDStringByCompID(strCompID) +
                            ") and (Agent_Code<>''  or Agent_Code is not null) order by Agent_Code desc";
                sqlda = new SqlDataAdapter(strsqling, sqlconnmarketing);
                break;
            case "S":
                strsqling = "select StoreHouseCode from TB_StoreHouse where CompID='" + strCompID +
                            "' order by StoreHouseCode desc";
                sqlda = new SqlDataAdapter(strsqling, sqlconn);
                break;
        }
        dt = new DataTable();
        sqlda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            intCode = Convert.ToInt16(dt.Rows[0][0].ToString());
            intLen = dt.Rows[0][0].ToString().Trim().Length;
            intCode = intCode + 1;
            intAdd1Len = intCode.ToString().Trim().Length;
            if (intAdd1Len < intLen)
            {
                switch (intLen - intAdd1Len) //---将该CODE值的长度定为5位长度以内，如果
                {
                    case 1:
                        strConvert = "0" + intCode.ToString().Trim();
                        break;
                    case 2:
                        strConvert = "00" + intCode.ToString().Trim();
                        break;
                    case 3:
                        strConvert = "000" + intCode.ToString().Trim();
                        break;
                    case 4:
                        strConvert = "0000" + intCode.ToString().Trim();
                        break;
                    case 5:
                        strConvert = "00000" + intCode.ToString().Trim();
                        break;
                }
            }
            else
            {
                strConvert = intCode.ToString().Trim();
            }
        }
        else
        {
            switch (strFlag.ToUpper())
            {
                case "P":
                    strConvert = "1000";
                    break;
                case "A":
                    strConvert = "2000";
                    break;
                case "S":
                    strConvert = "3000";
                    break;
            }
        }
        dt.Dispose();
        sqlda.Dispose();
        return strConvert;
    }

    private IList<MTB_ProductAuthorForAgent> autherprolist;
    private string ProductIDString = "";
    private readonly BTB_ProductAuthorForAgent bproductauthor = new BTB_ProductAuthorForAgent();
    private readonly BTB_Products_Infor bproduct = new BTB_Products_Infor();

    /// <summary>
    /// 根据代理商ID获取授权产品,前面的“,”自动去除，没有代理产品则为“”
    /// </summary>
    /// <param name="AgentID"></param>
    /// <returns></returns>
    public IList<MTB_Products_Infor> ReturnGetAgentAuthorProductInfo(string AgentID)
    {
        autherprolist = bproductauthor.GetListsByFilterString("AgentID=" + AgentID);
        ProductIDString = "";
        foreach (MTB_ProductAuthorForAgent mproauther in autherprolist)
        {
            ProductIDString += "," + mproauther.ProdID;
        }
        if (ProductIDString.StartsWith(","))
        {
            return bproduct.GetListsByFilterString("Infor_ID in (" + ProductIDString.Substring(1) + ")");
        }
        else
        {
            return new List<MTB_Products_Infor>();
        }
    }

    private IList<MTB_CompAgentInfo> mcompagentlist;
    private string AgentIDString = "";

    public string GetAgentIDStringByCompID(string CompID)
    {
        mcompagentlist = bcompagent.GetListsByFilterString("CompID=" + CompID);
        AgentIDString = "";
        if (mcompagentlist.Count > 0)
        {
            foreach (MTB_CompAgentInfo mca in mcompagentlist)
            {
                AgentIDString += "," + mca.AgentID;
            }
            return AgentIDString.Substring(1);
        }
        else
        {
            return "0";
        }
    }

    private DataTable dtforreturn;
    private string checkresult = "";
    private string tableinfo = "";
    private string[] tableinfoarray;
    private bool isfind;

    private DataTable QueryLabelInfoByLabelCode(string LabelCode, ref string Tname)
    {
        dtforreturn = null;
        checkresult = checkcodevalid(LabelCode);
        if (checkresult.Equals(""))
        {
            tableinfo = gettableinfo(LabelCode, LabelCode, sqlconn);
            if (!tableinfo.Trim().Equals(""))
            {
                tableinfoarray = tableinfo.Split(',');
                isfind = false;
                string tablename = "";
                for (int r = 0; r < tableinfoarray.Length; r++)
                {
                    tablename = tableinfoarray[r].Trim();
                    if (!tablename.Equals(""))
                    {
                        string[] sqlstringarray = getquerystringnew(tablename, LabelCode);
                        foreach (string sqlstring in sqlstringarray)
                        {
                            if (!sqlstring.Trim().Equals(""))
                            {
                                SqlDataAdapter sda = new SqlDataAdapter(sqlstring, sqlconn);
                                DataSet ds = new DataSet();
                                sda.Fill(ds, "rs");
                                Tname = "";
                                if (ds.Tables["rs"].Rows.Count > 0)
                                {
                                    dtforreturn = ds.Tables["rs"];
                                    Tname = tablename + "_";
                                    isfind = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (isfind)
                    {
                        break;
                    }
                }
            }
        }
        return dtforreturn;
    }

    private string temptablename = "";

    public string GetRelateTableName(string LabelCode, bool includeBoxLabel1)
    {
        temptablename = "";
        tempsqlstring = gettableinfo(LabelCode, LabelCode, sqlconn);
        foreach (string line in tempsqlstring.Split(','))
        {
            if (line.Length > 0)
            {
                string[] temparray = getquerystringnew(line, LabelCode);
                foreach (string line1 in temparray)
                {
                    sqlda = new SqlDataAdapter(line1, sqlconn);
                    dt = new DataTable();
                    sqlda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        if (includeBoxLabel1)
                        {
                            temptablename = line + "," + dt.Rows[0]["箱标1"];
                        }
                        else
                        {
                            temptablename = line;
                        }
                        break;
                    }
                }
            }
            if (temptablename.Length > 0)
            {
                break;
            }
        }
        return temptablename;
    }

    public string[] getquerystringnew(string tablename, string labelcodestring)
    {
        string[] sqlstringarray = new string[3];
        sqlstringarray.SetValue(GetReturnSqlStringForBox(tablename, labelcodestring), 0);
        sqlstringarray.SetValue(GetReturnSqlStringForBottle(tablename, labelcodestring), 1);
        sqlstringarray.SetValue(GetReturnSqlStringForReplaceBox(tablename, labelcodestring), 2);
        return sqlstringarray;
    }

    /// <summary>
    /// 获取发货信息
    /// </summary>
    /// <param name="strbox"></param>
    /// <param name="TableName"></param>
    /// <returns></returns>
    public DataTable getFaHuoInfo(string strbox, string TableName)
    {
        //string str = "select BottleLabel as 瓶标,BoxLabel01 as 箱标 from " + Tname + " where (BottleLabel='" + strbox + "' or BoxLabel01='" + strbox + "')";
        string str = "select a.* from " + TableName + "FH a  where a.BoxLabel01='" + strbox + "'";
        sqlda = new SqlDataAdapter(str, sqlconn);
        dt = new DataTable();
        sqlda.Fill(dt);
        return dt;
    }

    private string TableName = "";

    /// <summary>
    /// 根据标签获取发货信息。
    /// </summary>
    /// <param name="labelcode"></param>
    /// <returns></returns>
    public DataTable getFaHuoInfo(string labelcode)
    {
        TableName = GetRelateTableName(labelcode, true);
        //string str = "select BottleLabel as 瓶标,BoxLabel01 as 箱标 from " + Tname + " where (BottleLabel='" + strbox + "' or BoxLabel01='" + strbox + "')";
        tempsqlstring = "select a.* from " + TableName.Split(',')[0] + "_FH a  where a.BoxLabel01='" +
                        TableName.Split(',')[1] + "'";
        sqlda = new SqlDataAdapter(tempsqlstring, sqlconn);
        dt = new DataTable();
        sqlda.Fill(dt);
        return dt;
    }


    public string getDailishangkucun(string AgentID, string star, string end)
    {
        if (db.SelectFhTable(AgentID).Rows[0][0].ToString() == "1")
        {
            string str = "select sum(XiangNumber)  XiangNumber from   TB_FaHuoInfo_" + AgentID +
                         " where FHDate  between '" + star + "' and '" + Convert.ToDateTime(end).AddDays(1) + "'";

            sqlda = new SqlDataAdapter(str, sqlconn);
            dt = new DataTable();
            sqlda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string std = dt.Rows[0][0].ToString();

                if (string.IsNullOrEmpty(std))
                {
                    return "0";
                }
                else
                {
                    return std;
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
 
    /// <summary>
    /// 获取批次和数量
    /// </summary>
    /// <param name="fhkey"></param>
    /// <param name="compid"></param>
    /// <returns></returns>
    public DataTable getfhinfobyFhKey(string fhkey, string compid)
    {
        if (!string.IsNullOrEmpty(fhkey))
        {
            string str = "select *   from   TB_FaHuoInfo_" + compid + " where FHKey='" + fhkey + "'";
            sqlda = new SqlDataAdapter(str, sqlconn);
            dt = new DataTable();
            sqlda.Fill(dt);
            return dt;
        }
        return null;
    }

    public string getDailishangkucun(string compid, string AgentID, string star, string end, string flag)
    {
        string str = string.Empty;
        if (flag == "kuncun")
        {
            str = "select sum(XiangNumber)  XiangNumber from   TB_FaHuoInfo_" + compid + "  where AgentID=" + AgentID +
                  " and FHDate>='2016/01/01 0:00:00' and FHDate <'" + Convert.ToDateTime(star).AddDays(-1) + "'";
        }
        else if (flag == "daohuo")
        {
            str = "select sum(XiangNumber)  XiangNumber from   TB_FaHuoInfo_" + compid + " where  AgentID=" + AgentID +
                  " and FHDate  >= '" + star + "' and FHDate< '" + Convert.ToDateTime(end).AddDays(1) + "'";
        }

        else
        {
            str = "select sum(XiangNumber)  XiangNumber from   TB_FaHuoInfo_" + AgentID + " where FHDate  >= '" + star +
                  "' and  FHDate <'" + Convert.ToDateTime(end).AddDays(1) + "'";
        }
        if (db.SelectFhTable(AgentID).Rows[0][0].ToString() == "1")
        {
            sqlda = new SqlDataAdapter(str, sqlconn);
            dt = new DataTable();
            sqlda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string std = dt.Rows[0][0].ToString();

                if (string.IsNullOrEmpty(std))
                {
                    return "0";
                }
                else
                {
                    return std;
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

    public string getcijiDailishangkucun(string compid, string AgentID, string star, string end, string flag)
    {
        string str = string.Empty;
        if (flag == "kuncun")
        {
            str = "select sum(XiangNumber)  XiangNumber from   TB_FaHuoInfo_" + compid +
                  "  where  FHDate>='2016/01/01 0:00:00' and FHDate <'" + Convert.ToDateTime(star).AddDays(-1) + "'";
        }
        else if (flag == "daohuo")
        {
            str = "select sum(XiangNumber)  XiangNumber from   TB_FaHuoInfo_" + compid + " where  AgentID=" + AgentID +
                  " and FHDate  >= '" + star + "' and FHDate< '" + Convert.ToDateTime(end).AddDays(1) + "'";
        }

        else
        {
            str = "select sum(XiangNumber)  XiangNumber from   TB_FaHuoInfo_" + AgentID + " where FHDate  >= '" + star +
                  "' and  FHDate <'" + Convert.ToDateTime(end).AddDays(1) + "'";
        }
        if (db.SelectFhTable(AgentID).Rows[0][0].ToString() == "1")
        {
            sqlda = new SqlDataAdapter(str, sqlconn);
            dt = new DataTable();
            sqlda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string std = dt.Rows[0][0].ToString();

                if (string.IsNullOrEmpty(std))
                {
                    return "0";
                }
                else
                {
                    return std;
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

    public string getproIDbyFhKey(string fhkey, string compid)
    {
        string str = "select *   from   TB_FaHuoInfo_" + compid + " where FHKey='" + fhkey + "'"; 
        sqlda = new SqlDataAdapter(str, sqlconn);
        dt = new DataTable();
        sqlda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            string std = dt.Rows[0]["ProID"].ToString();

            if (string.IsNullOrEmpty(std))
            {
                return "0";
            }
            else
            {
                return std;
            }
        }
        else
        {
            return "0";
        }
    }

    public DataTable GetConfirmInfo(string TableName, string BoxLabel)
    {
        TableName = "AgentAcceptinfo_2019";
        string sqlstring = "select a.AcceptAgentID,MAX(a.AcceptDate) AcceptDate,a.AcceptAgentID AS compname from TianJianWuLiuWebnew.dbo." + TableName + " a where BoxLabel='" + BoxLabel + "'  GROUP BY a.AcceptAgentID ORDER BY  a.AcceptAgentID";
        sqlda = new SqlDataAdapter(sqlstring, sqlconn);
        dt = new DataTable();
        sqlda.Fill(dt);
        return dt;
    }

    /// <summary>
    /// 返回箱标号码和表名，以“,"隔开
    /// </summary>
    /// <param name="cpid">标签号码</param>
    /// <returns></returns>
    public string GetBoxLabelAndTableName(string cpid)
    {
        using (var conn = sqlconn)
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

}