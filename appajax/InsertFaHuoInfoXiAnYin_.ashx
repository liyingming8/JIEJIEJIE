<%@ WebHandler Language="C#" Class="InsertFaHuoInfoXiAnYin " %>

using System;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
public class InsertFaHuoInfoXiAnYin : IHttpHandler 
{
    readonly StringBuilder _sb = new StringBuilder();

    SqlConnection myConn = null;
    SqlCommand myCmd;
    DataSet ds;
    SqlDataAdapter ad;
    public DataSet Mydataset;
    public SqlDataAdapter ada;

    public SqlConnection GetConnectionWL()
    {
        string str = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString();
        myConn = new SqlConnection(str);
        return myConn;
    }
    
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.QueryString["strAllInfo"]))
        {
            string intBackInfo = InsertTbProcessDataInfo(context.Request.QueryString["strAllInfo"].Trim());
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
    private string _strBatchNo = "";
    private string _strcode = "";
    private string _strczy = "";
    private string _strdls = "";
    private string _strcp = "";
    private string _strxh = "";
    private string _strck = "";
    private string _strguid = "";

    private   string  _strlabelcode = "";
    private  string  _strfhdate ="";
    private  string  _strfhpici = "";
    private  string   _straid= "";
    private  string _strpid = "";
    private  string _strstid = "";
    private  string _strcompid = "";
    private  string _struserid = "";
    private  string _strflag = "";

    private string InsertTbProcessDataInfo(string strall111)//string ProductID, string MadeDate)
    {
        try
        {
            using (var myConn = GetConnectionWL())
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                _str1 = "insert into TB_ALLInsertFaHuoTest([BoxLabel],[FHDate],[FHPICI],[AgentID],[ProductID],[STID],[CompID],[UserID],[Flag]) values(";
                _str2 = "";
                _str3 = "";
                _strBatchNo = DateTime.Now.ToString("yyyyMMddhhmmss");
                if (strall111.Trim().Contains(","))
                {
                    _strlabelcode = strall111.Split(',')[0].Trim();//标签号码
                    _strfhdate = strall111.Split(',')[1].Trim();//发货时间
                    _strfhpici = strall111.Split(',')[2].Trim();//发货批次
                    _straid= strall111.Split(',')[3].Trim();//代理商ID
                    _strpid = strall111.Split(',')[4].Trim();//产品ID
                    _strstid = strall111.Split(',')[5].Trim();//发货仓库ID
                    _strcompid = strall111.Split(',')[6].Trim();//登录对应CompID
                    _struserid = strall111.Split(',')[7].Trim();//登录对应UserID
                    _strflag = strall111.Split(',')[8].Trim();//发货标志

                    _str2 = "'" + _strlabelcode + "','" + _strfhdate + "','" + _strfhpici + "','" + _straid + "','" + _strpid + "','" + _strstid + "','" + _strcompid + "','" + _struserid + "','" + _strflag + "');";
                    _str3 = _str3 + _str1 + _str2;
                }
                if (!_str3.Equals(""))
                {
                    SqlCommand cmd = new SqlCommand(_str3, myConn);
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

    private string InsertIntoAllFaHuoInfoNew(string strall)//string boxlabel,string fhdate,string fhpici,string agentid,string pid,string stid,string compid,string userid,string flag,string memo,string otherinfo)
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
                Int32 intpid = 0;
                int intsalesid = 1;
                string str11 = "";//TBLBarcode
                string str22 = "";//TBSBarcode
                string str33 = "";//TBSales
                string str44 = "";//TBSalesDetail
                string str55 = "";//TBSalesBarCode

                //string[] strarray = strall.Split('^');//区分数量与扫描信息
                //string[] strarray1 = strarray[0].Split('%');//区分扫描产品的数量与产品ID
                //string[] strarray2 = strarray[1].Split('|');//区分扫描后的每条详细记录
                //string[] strarray3 = strarray1[1].Split(',');//区分产品

                string strcode = "";
                string strcid = "";
                string strpid = "";
                string strbox = "";
                string strstid = "";
                string strczr = "";
                string strxhhaha = "";
                //int intchanpinshuliang = Convert.ToInt16(strarray1[0].ToString().Trim());
                string strcpinfo = "";
                Boolean boolsalesinsert = false;

                Boolean boosalesINS = false;
                int intscanshuliang = 0;
                int intTotalShuLiang = 0;//统计总共的扫描数量值
                string strBackInfo = "";

                SqlDataAdapter adda11 = new SqlDataAdapter();
                SqlDataAdapter adda22 = new SqlDataAdapter();
                DataSet dssd11 = new DataSet();
                DataSet dssd22 = new DataSet();

                string strcpgeshu = "";
                string strsqling = "select distinct cp,code from TB_ProcessDataInfo";
                dssd22 = new DataSet();
                adda22 = new SqlDataAdapter(strsqling, myConn);
                adda22.Fill(dssd22, "tbprocess");
                if (dssd22.Tables["tbprocess"].Rows.Count > 0)
                {
                    int intchanpinshuliang = dssd22.Tables["tbprocess"].Rows.Count;
                    for (int jl = 0; jl <= dssd22.Tables["tbprocess"].Rows.Count - 1; jl++)
                    {
                        string strcphahaha = dssd22.Tables["tbprocess"].Rows[jl][0].ToString().Trim();
                        string strcodehahaha = dssd22.Tables["tbprocess"].Rows[jl][1].ToString().Trim();
                        if (jl < dssd22.Tables["tbprocess"].Rows.Count - 1)
                        {
                            strcpgeshu = strcpgeshu + strcphahaha + "|" + strcodehahaha + ",";
                        }
                        else
                        {
                            strcpgeshu = strcpgeshu + strcphahaha + "|" + strcodehahaha;
                        }
                    }

                    string[] strarray3 = strcpgeshu.Split(',');//区分产品
                    string strcodeinfo = "";
                    dssd22.Clear();
                    dssd22.Dispose();
                    adda22.Dispose();
                    for (int i = 1; i <= intchanpinshuliang; i++)
                    {
                        strcpinfo = strarray3[i - 1].Split('|')[0].Trim();
                        strcodeinfo = strarray3[i - 1].Split('|')[1].Trim();

                        strsqling = "select * from TB_ProcessDataInfo where cp='" + strcpinfo + "' and code='" + strcodeinfo + "'";
                        strBackInfo = InsertTBProductBatchNO(strcpinfo);
                        if (strBackInfo.Equals("true"))//插入一个ProDuctBatchNoID 
                        {
                            intpid = BackProDuctBatchNoID();//获取插入后ProDuctBatchNoID 的值
                            adda11 = new SqlDataAdapter(strsqling, myConn);
                            adda11.Fill(dssd11, "tbp");
                            if (dssd11.Tables["tbp"].Rows.Count <= 0)
                            {
                                return "0";
                            }
                            for (int j = 0; j <= dssd11.Tables["tbp"].Rows.Count - 1; j++)
                            {
                                strcode = dssd11.Tables["tbp"].Rows[j]["code"].ToString().Trim();//发货单号=操作者ID（UserName）
                                strczr = dssd11.Tables["tbp"].Rows[j]["czy"].ToString().Trim();
                                strcid = dssd11.Tables["tbp"].Rows[j]["dls"].ToString().Trim();//代理商ID信息
                                strpid = dssd11.Tables["tbp"].Rows[j]["cp"].ToString().Trim();//产品ID信息
                                strbox = dssd11.Tables["tbp"].Rows[j]["xh"].ToString().Trim();//大箱标信息
                                strstid = dssd11.Tables["tbp"].Rows[j]["ck"].ToString().Trim();//仓库ID信息

                                if (strpid.Equals(strcpinfo) && strcode.Equals(strcodeinfo))
                                {
                                    intscanshuliang = intscanshuliang + 1;//统计满足条件的箱标数量 
                                    intTotalShuLiang = intTotalShuLiang + 1;//统计所有的扫描数量
                                    str11 = str11 + strbox + "," + intpid + "," + strcid + "|";
                                    str22 = str22 + strbox + "," + intpid + "," + strcid + "|";
                                    str33 = str33 + strcid + "," + strcode + "," + strczr + "," + intscanshuliang.ToString().Trim();

                                    str55 = str55 + strbox + ",";

                                    strxhhaha = strxhhaha + strbox + ",";

                                    if (boolsalesinsert == false)
                                    {
                                        boolsalesinsert = true;
                                        strBackInfo = BackCode("111", strcode);
                                        if (strBackInfo.Equals("0"))//判断SALES里CODE是否存在，“0”为不存在，否则返回对应的ID值
                                        {
                                            strBackInfo = InsertTBSales(str33);//将信息插入到表TBSales中（在换新的产品之前）
                                            if (!strBackInfo.Equals("true"))
                                            {
                                                return "InsertTBSales：" + strBackInfo;
                                            }
                                            intsalesid = BackSalesIDNew(strcode);//返回 SalesID
                                            boosalesINS = true;
                                        }
                                        else
                                        {
                                            boosalesINS = false;
                                            intsalesid = Convert.ToInt32(strBackInfo);
                                        }
                                    }

                                }
                            }
                            str44 = str44 + intpid + "," + intscanshuliang + "," + intsalesid + "|";

                            strBackInfo = updateTBLBarCode(str11);
                            if (!strBackInfo.Equals("true"))//此时进入到另一个产品名称下,更新TBLBarCode
                            {
                                return "updateTBLBarCode:" + strBackInfo;
                            }

                            strBackInfo = updateTBSBarCode(str22);//此时进入到另一个产品名称下,更新TBSBarCode
                            if (!strBackInfo.Equals("true"))
                            {
                                return "updateTBSBarCode：" + strBackInfo;
                            }

                            strBackInfo = InsertTBSalesDetail(str44);//将信息插入到表TBSalesDetail中（在换新的产品之前）
                            if (!strBackInfo.Equals("true"))
                            {
                                return "InsertTBSalesDetail：" + strBackInfo;
                            }
                            str55 = str55 + "|" + intsalesid;

                            strBackInfo = InsertTBSalesBarCode(str55);//将信息插入到表TBSalesBarCode中（在换新的产品之前）
                            if (!strBackInfo.Equals("true"))
                            {
                                return "InsertTBSalesBarCode：" + strBackInfo;
                            }
                            //===删除TBProcessDataInfo 中的数据================================
                            string[] arrayxh = strxhhaha.Split(',');
                            int intarrlength = arrayxh.Length;
                            string stror = "xh=";
                            strxhhaha = "";
                            for (int intxh = 0; intxh <= intarrlength - 1; intxh++)
                            {
                                if (!arrayxh[intxh].Equals(""))
                                {
                                    if (intxh < intarrlength - 1)
                                    {
                                        strxhhaha = strxhhaha + stror + "'" + arrayxh[intxh] + "'" + " or ";
                                    }
                                    else
                                    {
                                        strxhhaha = strxhhaha + stror + "'" + arrayxh[intxh] + "'";
                                    }
                                }
                            }
                            if (!strxhhaha.Equals(""))
                            {
                                strxhhaha = strxhhaha.Substring(0, strxhhaha.Length - 3);
                                strBackInfo = DeleteCodeXh(strcode, strxhhaha);
                                if (!strBackInfo.Equals("true"))
                                {
                                    return "Delete ProcessDataInfo：" + strBackInfo;
                                }
                            }
                            strxhhaha = "";
                            stror = "";
                            //==================================================================
                            //===Update Sales 里的数量
                            if (boosalesINS == true)
                            {
                                strBackInfo = UpdateTBSalesInfo(strcode, (intTotalShuLiang - 1).ToString().Trim(), intsalesid.ToString().Trim());
                            }
                            else
                            {
                                strBackInfo = UpdateTBSalesInfo(strcode, intTotalShuLiang.ToString().Trim(), intsalesid.ToString().Trim());
                            }
                            if (!strBackInfo.Equals("true"))
                            {
                                return "UpdateTBSales：" + strBackInfo;
                            }
                            boolsalesinsert = false;
                            intscanshuliang = 0;
                            intTotalShuLiang = 0;
                            boosalesINS = false;
                            str11 = "";
                            str22 = "";
                            str33 = "";
                            str44 = "";
                            str55 = "";
                            strBackInfo = "";
                            dssd11.Dispose();
                            dssd11.Clear();
                            adda11.Dispose();
                        }
                        else
                        {
                            return "InsertTBProductBatchNO：" + strBackInfo;
                        }
                    }
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
        }
        catch (Exception eee)
        {
            return eee.Message;
        }
    }


    private string DeleteCodeXh(string strcode, string strxh)
    {
        string strsqling = "delete from TB_ProcessDataInfo where code='" + strcode.Trim() + "' and (" + strxh + ")";
        try
        {
            using (var myConn = GetConnectionWL())
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                SqlCommand cmd = new SqlCommand(strsqling, myConn);
                int intc = cmd.ExecuteNonQuery();
                if (intc > 0)
                {
                    cmd.Dispose();
                    myConn.Close();
                    return "true";
                }
                else
                {
                    cmd.Dispose();
                    myConn.Close();
                    return "false: " + intc.ToString().Trim();
                }
            }
        }
        catch (Exception eee)
        {
            return eee.Message;
        }
    }
    private DataTable BackProduct(string strcompid)
    {
        //string str = "select top 20 b.id,t.Brand,t.Name,t.Type,t.Code from TBProduct t,TBProductBatchNo b where t.id=b.Product_ID order by b.MadeDate desc";
        string str = "select id,Name from TBProduct order by id";
        using (var myConn = GetConnectionWL())
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            ds = new DataSet();
            ad = new SqlDataAdapter(str, myConn);
            ad.Fill(ds, "Product");
            return ds.Tables["Product"];
        }
    }
    private DataTable BackStoreHouseInfo(string strcompid)
    {
        string str = "select id,Description,code from TBWarehouse order by id";
        try
        {
            using (var myConn = GetConnectionWL())
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                ds = new DataSet();
                ad = new SqlDataAdapter(str, myConn);
                ad.Fill(ds, "StoreHouse");
                return ds.Tables["StoreHouse"];
            }
        }
        catch (Exception eee)
        {
            return null;
        }
    }

    /// <summary>
    /// 返回货单号码的值，以判断是否为不一致的值
    /// </summary>
    /// <returns></returns>
    private string BackCode(string compid, string strcode)
    {
        string str = "select * from TBSales where code='" + strcode.Trim() + "'";
        using (var myConn = GetConnectionWL())
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            DataSet ds11 = new DataSet();
            SqlDataAdapter ad11 = new SqlDataAdapter(str, myConn);
            ad11.Fill(ds11, "code");
            if (ds11.Tables["code"].Rows.Count > 0)
            {
                return ds11.Tables["code"].Rows[0]["id"].ToString().Trim();
            }
            else
            {
                return "0";
            }
        }
    }

    private DataTable BackCustomer(string strcompid)
    {
        string str = "select id,name from TBCustomer order by id";
        using (var myConn = GetConnectionWL())
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            ds = new DataSet();
            DataSet ds1 = new DataSet();
            ad = new SqlDataAdapter(str, myConn);
            ad.Fill(ds, "Cust");
            return ds.Tables["Cust"];
        }
    }
    /// <summary>
    /// 返回 TBProduct Batch No表中的ID最大值，因ID自动增加，所以，获取后ID加1就是将来要增加的ID值
    /// </summary>
    /// <returns></returns>
    private int BackProDuctBatchNoID()
    {
        string str = "select max(id) from TBProductBatchNo";
        using (var myConn = GetConnectionWL())
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            ds = new DataSet();
            ad = new SqlDataAdapter(str, myConn);
            ad.Fill(ds, "lbatchid");
            if (ds.Tables["lbatchid"].Rows.Count > 0)
            {
                int int1 = int.Parse(ds.Tables["lbatchid"].Rows[0][0].ToString());
                return int1;
            }
            else
            {
                return 0;
            }
        }
    }
    /// <summary>
    /// 返回TBSales 表中最大的ID，然后加1，即为即将增加的值
    /// </summary>
    /// <returns></returns>
    private Int32 BackSalesIDNew(string strcode)
    {
        string str = "select id from TBSales where code='" + strcode + "'";
        using (var myConn = GetConnectionWL())
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            ds = new DataSet();
            ad = new SqlDataAdapter(str, myConn);
            ad.Fill(ds, "salesid");
            if (ds.Tables["salesid"].Rows.Count > 0)
            {
                Int32 int1 = int.Parse(ds.Tables["salesid"].Rows[0][0].ToString());
                return int1;
            }
            else
            {
                return 0;
            }
        }
    }

    //---返回LBarCODE　ID
    private string BackLBarCodeID(string lbarcode)
    {
        string str = "select id from TBLBarcode where LBarcode='" + lbarcode + "' or BoxLabelCode like '%" + lbarcode + "%'";
        using (var myConn = GetConnectionWL())
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            ds = new DataSet();
            ad = new SqlDataAdapter(str, myConn);
            ad.Fill(ds, "lbarcode");
            if (ds.Tables["lbarcode"].Rows.Count > 0)
            {
                string strback = ds.Tables["lbarcode"].Rows[0][0].ToString().Trim();
                return strback;
            }
            else
            {
                return "0";
            }
        }
    }
    //---返回SBar Code ID
    private string BackSBarCodeID(string sbarcode)
    {
        string str = "select id from TBSBarcode where Barcode='" + sbarcode + "'";
        using (var myConn = GetConnectionWL())
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            ds = new DataSet();
            ad = new SqlDataAdapter(str, myConn);
            ad.Fill(ds, "sbarcode");
            if (ds.Tables["sbarcode"].Rows.Count > 0)
            {
                string strback = ds.Tables["sbarcode"].Rows[0][0].ToString().Trim();
                return strback;
            }
            else
            {
                return "0";
            }
        }
    }
    //通过 LBar Code获取其对应的所有的小Bar Code ID列表
    private DataTable BackLBarCodeToSBarCodeID(string LbarCode)
    {
        string strlid = BackLBarCodeID(LbarCode);
        if (!strlid.Equals("0"))
        {
            string str = "select id from TBSBarcode where LBarcode_ID='" + strlid + "'";
            using (var myConn = GetConnectionWL())
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                ds = new DataSet();
                ad = new SqlDataAdapter(str, myConn);
                ad.Fill(ds, "ALLbarcode");
                return ds.Tables["ALLbarcode"];
            }
        }
        else
        {
            return null;
        }
    }
    //更新TBLBarCode
    private string updateTBLBarCode(string strall)//string Barcode,string PBatchID,string CustID)
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
                string str1 = "update TBLBarCode set ";
                string str2 = "";
                string str3 = "";
                for (int i = 0; i <= strarray.Length - 1; i++)
                {
                    if (!strarray[i].Trim().Equals(""))
                    {
                        string strbox = strarray[i].Split(',')[0].Trim();
                        string strProDID = strarray[i].Split(',')[1].Trim();
                        string strcid = strarray[i].Split(',')[2].Trim();
                        str2 = "ProductBatchNo_ID='" + strProDID + "',State='1',WState='2',Customer_ID='" + strcid + "' where LBarcode='" + strbox + "' or BoxLabelCode like '%" + strbox + "%';";
                        str3 = str3 + str1 + str2;
                    }
                }
                //string sql = "";

                //sql = "insert into TB_ALLInsertFaHuo(boxlabel,fhdate,fhpici,agentid,productid,stid,compid,userid,flag,memo,otherinfo) values('" + boxlabel + "','" + fhdate + "','" + fhpici + "','" + agentid + "','" + pid + "','" + stid + "','" + compid + "','" + userid + "','" + flag + "','" + memo + "','" + otherinfo + "')";
                if (!str3.Equals(""))
                {
                    SqlCommand cmd = new SqlCommand(str3, myConn);
                    int intc = cmd.ExecuteNonQuery();
                    if (intc > 0)
                    {
                        cmd.Dispose();
                        myConn.Close();
                        return "true";
                    }
                    else
                    {
                        cmd.Dispose();
                        myConn.Close();
                        return "false:" + intc.ToString().Trim();
                    }
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
    //更新 TBSBar Code
    /// <summary>
    /// 正常情况下应该返回布尔型，为了方便调试，看出错的信息更清楚，返回字符串
    /// </summary>
    /// <param name="strall"></param>
    /// <returns></returns>
    private string updateTBSBarCode(string strall)//string BarCode,string PBatchID, string CustID
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
                string str1 = "update TBSBarCode set ";
                string str2 = "";
                string str3 = "";
                for (int i = 0; i <= strarray.Length - 1; i++)
                {
                    if (!strarray[i].Trim().Equals(""))
                    {
                        string strbox = strarray[i].Split(',')[0].Trim();
                        string strProDID = strarray[i].Split(',')[1].Trim();
                        string strcid = strarray[i].Split(',')[2].Trim();
                        string strLID = BackLBarCodeID(strbox);
                        if (!strLID.Equals("0"))
                        {
                            str2 = "ProductBatchNo_ID='" + strProDID + "',State='1',WState='2',Customer_ID='" + strcid + "' where LBarcode_ID='" + strLID + "';";
                            str3 = str3 + str1 + str2;
                        }
                        else
                        {
                            return "false0";
                        }
                    }
                }
                if (!str3.Equals(""))
                {
                    SqlCommand cmd = new SqlCommand(str3, myConn);
                    int intc = cmd.ExecuteNonQuery();
                    if (intc > 0)
                    {
                        cmd.Dispose();
                        myConn.Close();
                        return "true";
                    }
                    else
                    {
                        cmd.Dispose();
                        myConn.Close();
                        return "false:" + intc.ToString().Trim();
                    }
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

    private string UpdateTBSalesInfo(string code, string strqty, string strsalesid)//string CustID, string code, string workdate, string username, string operatorname, string qty, string lbarqty)
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
                int strQtyL = int.Parse(strqty);
                int strLQty = strQtyL * 240;
                string str1 = "update TBSales set Qty=Qty + " + strLQty + ",LBarcodeQty=LBarcodeQty + " + strQtyL + " where id='" + strsalesid + "' and code='" + code + "'";
                SqlCommand cmd = new SqlCommand(str1, myConn);
                int intc = cmd.ExecuteNonQuery();
                if (intc > 0)
                {
                    cmd.Dispose();
                    myConn.Close();
                    return "true";
                }
                else
                {
                    cmd.Dispose();
                    myConn.Close();
                    return "false:" + intc.ToString().Trim();
                }
            }
        }
        catch (Exception eee)
        {
            return eee.Message;
        }
    }


    /// <summary>
    /// 插入到 TBSales 
    /// </summary>
    /// <param name="strall"></param>
    /// <returns></returns>
    private string InsertTBSales(string strall)//string CustID, string code, string workdate, string username, string operatorname, string qty, string lbarqty)
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
                string str1 = "insert into TBSales([Customer_ID],[Code],[WorkDate],[toCustomer_ID],[CreateTime],[UserName],[State],[OperatorName],[Qty],[LBarcodeQty]) values(";
                string str2 = "";
                string str3 = "";
                for (int i = 0; i <= strarray.Length - 1; i++)
                {
                    if (!strarray[i].Trim().Equals(""))
                    {
                        string strcid = strarray[i].Split(',')[0].Trim();
                        string strcode = strarray[i].Split(',')[1].Trim();
                        string strworkdate = System.DateTime.Now.ToShortDateString();
                        string strcreatetime = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        string strusername = strarray[i].Split(',')[2].Trim();
                        string strQty = strarray[i].Split(',')[3].Trim();
                        string strLQty = (int.Parse(strQty) * 240).ToString();

                        str2 = "'1','" + strcode + "','" + strworkdate + "','" + strcid + "','" + strcreatetime + "','" + strusername + "','1','','" + strLQty + "','" + strQty + "');";
                        str3 = str3 + str1 + str2;
                    }
                }
                if (!str3.Equals(""))
                {
                    SqlCommand cmd = new SqlCommand(str3, myConn);
                    int intc = cmd.ExecuteNonQuery();
                    if (intc > 0)
                    {
                        cmd.Dispose();
                        myConn.Close();
                        return "true";
                    }
                    else
                    {
                        cmd.Dispose();
                        myConn.Close();
                        return "false:" + intc.ToString().Trim();
                    }
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
    /// <summary>
    /// 插入到表 TBSalesDetail
    /// </summary>
    /// <param name="strall"></param>
    /// <returns></returns>
    private string InsertTBSalesDetail(string strall)//string SalesID,string probatchid,string qty, string lbarqty)
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
                string str1 = "insert into TBSalesDetail([Sales_ID],[ProductBatchNo_ID],[Qty],[LBarcodeQty]) values(";
                string str2 = "";
                string str3 = "";
                for (int i = 0; i <= strarray.Length - 1; i++)
                {
                    if (!strarray[i].Trim().Equals(""))
                    {
                        string strBatchid = strarray[i].Split(',')[0].Trim();
                        string strQty = strarray[i].Split(',')[1].Trim();
                        string strLQty = (int.Parse(strQty) * 240).ToString();
                        string strsid = strarray[i].Split(',')[2].Trim();
                        str2 = "'" + strsid + "','" + strBatchid + "','" + strLQty + "','" + strQty + "');";
                        str3 = str3 + str1 + str2;
                    }
                }
                if (!str3.Equals(""))
                {
                    SqlCommand cmd = new SqlCommand(str3, myConn);
                    int intc = cmd.ExecuteNonQuery();
                    if (intc > 0)
                    {
                        cmd.Dispose();
                        myConn.Close();
                        return "true";
                    }
                    else
                    {
                        cmd.Dispose();
                        myConn.Close();
                        return "false:" + intc.ToString().Trim();
                    }
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
    /// <summary>
    /// 插入到TBSalesBarcode
    /// </summary>
    /// <param name="strall"></param>
    /// <returns></returns>
    private string InsertTBSalesBarCode(string strall)//string SalesID, string sbarcodeid, string Lbarcodeid)
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
                DataTable dt = new DataTable();
                string[] strarray1 = strall.Split('|');
                string str1 = "insert into TBSalesBarcode([Sales_ID],[SBarcode_ID],[LBarcode_ID]) values(";
                string str2 = "";
                string str3 = "";
                string[] strarray = strarray1[0].Split(',');
                string strsid = strarray1[1].Trim();
                for (int i = 0; i <= strarray.Length - 1; i++)
                {
                    if (!strarray[i].Trim().Equals(""))
                    {
                        string strLBarCode = strarray[i].Trim();
                        string strLBID = BackLBarCodeID(strLBarCode);
                        if (!strLBID.Equals("0"))
                        {
                            dt = BackLBarCodeToSBarCodeID(strLBarCode);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                for (int hh = 0; hh <= dt.Rows.Count - 1; hh++)
                                {
                                    string strsBarid = dt.Rows[hh][0].ToString().Trim();
                                    str2 = "'" + strsid + "','" + strsBarid + "','" + strLBID + "');";
                                    str3 = str3 + str1 + str2;
                                }
                            }
                        }
                        else
                        {
                            return "false0";
                        }
                        //string strQty = strarray[i].Split(',')[1].ToString().Trim();
                        //string strLQty = (int.Parse(strQty) * 240).ToString();
                    }
                }
                if (!str3.Equals(""))
                {
                    SqlCommand cmd = new SqlCommand(str3, myConn);
                    int intc = cmd.ExecuteNonQuery();
                    if (intc > 0)
                    {
                        cmd.Dispose();
                        myConn.Close();
                        return "true";
                    }
                    else
                    {
                        cmd.Dispose();
                        myConn.Close();
                        return "false:" + intc.ToString().Trim();
                    }
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
    /// <summary>
    /// 插入到 TBProductBatchNo
    /// </summary>
    /// <param name="strall"></param>
    /// <returns></returns>
    private string InsertTBProductBatchNO(string strall111)//string ProductID, string MadeDate)
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
                //string[] strarray = strall.Split('|');
                string str1 = "insert into TBProductBatchNo([Product_ID],BatchNo,[MadeDate]) values(";
                string str2 = "";
                string str3 = "";
                //for (int i = 0; i <= strarray.Length - 1; i++)
                //{
                //if (!strarray[i].ToString().Trim().Equals(""))
                //{
                //string strBatchid = strarray[i].Split(',')[0].ToString().Trim();
                string strBatchid = strall111.Trim();
                string strMadeDate = System.DateTime.Now.ToShortDateString();
                string strBatchNo = System.DateTime.Now.ToString("yyyyMMddhhmmss");
                str2 = "'" + strBatchid + "','" + strBatchNo + "','" + strMadeDate + "');";
                str3 = str3 + str1 + str2;
                //}
                //}
                if (!str3.Equals(""))
                {
                    SqlCommand cmd = new SqlCommand(str3, myConn);
                    int intc = cmd.ExecuteNonQuery();
                    if (intc > 0)
                    {
                        cmd.Dispose();
                        myConn.Close();
                        return "true";
                    }
                    else
                    {
                        cmd.Dispose();
                        myConn.Close();
                        return "false:" + intc.ToString().Trim();
                    }
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
}