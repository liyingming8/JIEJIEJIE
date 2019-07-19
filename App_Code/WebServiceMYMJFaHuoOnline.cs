using System;
using System.Configuration;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// WebServiceMYMJFaHuoOnline 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class WebServiceMYMJFaHuoOnline : WebService {

    SqlConnection myConn;
    DataSet ds;
    SqlDataAdapter ad;
    public DataSet Mydataset;
    public SqlDataAdapter ada;

    public SqlConnection GetConnectionWL()
    {
        string str = ConfigurationManager.ConnectionStrings["ConnectionStringAccounts"].ToString();
        myConn = new SqlConnection(str);
        return myConn;
    }
    public WebServiceMYMJFaHuoOnline () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    
    [WebMethod]
    /// <summary>
    /// 获取客户信息WEB服务
    /// </summary>
    /// <param name="strcompid"></param>
    /// <param name="PWD"></param>
    /// <param name="USERID"></param>
    /// <returns></returns>
    public DataTable GetCustomer(string strcompid, string pwd, string userid)
    {
        try
        {
            if (!pwd.Trim().Equals("TIANJIANKJHAHA") && (!userid.Trim().Equals("TJUSERHAHA")))
            {
                return null;
            }
            else
            {
                return BackCustomer(strcompid);
            }
        }
        catch(Exception)
        {
            return null;
        }
    }
    
    [WebMethod]
    /// <summary>
    /// 获取产品名称WEB服务
    /// </summary>
    /// <param name="strcompid"></param>
    /// <param name="PWD"></param>
    /// <param name="USERID"></param>
    /// <returns></returns>
    public DataTable GetProDuctName(string strcompid, string PWD, string USERID)
    {
        try
        {
            if (!PWD.Trim().Equals("TIANJIANKJHAHA") && (!USERID.Trim().Equals("TJUSERHAHA")))
            {
                return null;
            }
            else
            {
                return BackProduct(strcompid);
            }
        }
        catch
        {
            return null;
        }
    }

    [WebMethod]
    public string GetHuoDanCode(string strcompid, string PWD, string USERID,string strcode)
    {
        try
        {
            if (!PWD.Trim().Equals("TIANJIANKJHAHA") && (!USERID.Trim().Equals("TJUSERHAHA")))
            {
                return "PWD User Error";
            }
            else
            {
                if (BackCode(strcompid,strcode)==true)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
        }
        catch  
        {
            return "Error";
        }
    }
    [WebMethod]
    /// <summary>
    /// 获取仓库信息WEB 服务
    /// </summary>
    /// <param name="strcompid"></param>
    /// <param name="PWD"></param>
    /// <param name="USERID"></param>
    /// <returns></returns>
    public DataTable GetStoreHouseName(string strcompid, string PWD, string USERID)
    {
        try
        {
            if (!PWD.Trim().Equals("TIANJIANKJHAHA") && (!USERID.Trim().Equals("TJUSERHAHA")))
            {
                return null;
            }
            else
            {
                return BackStoreHouseInfo(strcompid);
            }
        }
        catch  
        {
            return null;
        }
    }

    [WebMethod]
    public string InsertALLFaHuoInfo(string PWD, string strUSERID, string stralling)//string boxlabel, string fhdate, string fhpici, string agentid, string pid, string stid, string compid, string userid, string flag, string memo, string otherinfo)
    {
        try
        {
            if (!PWD.Trim().Equals("TIANJIANKJHAHA") && (!strUSERID.Trim().Equals("TJUSERHAHA")))
            {
                return "false";
            }
            else
            {
                return InsertIntoAllFaHuoInfo(stralling);//boxlabel, fhdate, fhpici, agentid, pid, stid, compid, userid, flag, memo, otherinfo);
            }
        }
        catch (Exception eee)
        {
            return eee.Message;
        }
    }

    private string InsertIntoAllFaHuoInfo(string strall)//string boxlabel,string fhdate,string fhpici,string agentid,string pid,string stid,string compid,string userid,string flag,string memo,string otherinfo)
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
                int intjishupid = 0;
                Int32 intpid = 0;
                int intchanpincishu = 1;
                int intsalesid = 1;
                string strpidchar = "";
                Boolean butongpid = false;
                string str11="";//TBLBarcode
                string str22="";//TBSBarcode
                string str33="";//TBSales
                string str44="";//TBSalesDetail
                string str55="";//TBSalesBarCode
                //string str66="";//TBProductBatchNo
                string[] strarray = strall.Split('|');
                string strcode = "";
                string strcid = "";
                string strpid = "";
                string strbox = "";
                string strstid = "";
                for (int i = 0; i <= strarray.Length - 1; i++)
                {
                    if (!strarray[i].Trim().Equals(""))
                    {
                        strcode = strarray[i].Split(',')[0].Trim();//发货单号=操作者ID（UserName）
                        strcid = strarray[i].Split(',')[1].Trim();//代理商ID信息
                        strpid = strarray[i].Split(',')[2].Trim();//产品ID信息
                        strbox = strarray[i].Split(',')[3].Trim();//大箱标信息
                        strstid = strarray[i].Split(',')[4].Trim();//仓库ID信息
                        if (strpidchar .Equals (""))
                        {
                            strpidchar = strpid;
                            intjishupid = intjishupid + 1;
                            InsertTBProductBatchNO(strpidchar);//在更换新的产品名称前，插入一个BatchNo ID 
                            intpid = BackProDuctBatchNoID();
                            intsalesid = BackSalesID() + 1;
                            str11 = str11 + strbox + "," + intpid + "," + strcid + "|";
                            str22 = str22 + strbox + "," + intpid + "," + strcid + "|";
                            str33 = str33 + strcid + "," + strcode.Split('=')[0] + "," + strcode.Split('=')[1];
                            str55=str55 + strbox + ",";
                        }
                        else
                        {
                            intjishupid = intjishupid + 1;
                            if (!strpidchar .Equals (strpid))
                            {
                                updateTBLBarCode(str11);//此时进入到另一个产品名称下,更新TBLBarCode
                                updateTBSBarCode(str22);//此时进入到另一个产品名称下,更新TBSBarCode
                                str33 = str33 + "," + (intjishupid - 1) + "|";
                                InsertTBSales(str33);//将信息插入到表TBSales中（在换新的产品之前）
                                intsalesid = BackSalesID();
                                str44 = str44 + (intpid) + "," + (intjishupid - 1) + "," + intsalesid + "|";
                                InsertTBSalesDetail(str44);//将信息插入到表TBSalesDetail中（在换新的产品之前）
                                str55 = str55 + "|"  + intsalesid ;

                                InsertTBSalesBarCode(str55);//将信息插入到表TBSalesBarCode中（在换新的产品之前）
                                intjishupid = 1; //进入到新的产品后，将计数器恢复到1
                                butongpid = true;
                                str11 = "";
                                str22 = "";
                                str33 = "";
                                str44 = "";
                                str55 = "";
                                //新的产品名称开始前，取之前的值
                                intchanpincishu = intchanpincishu + 1;//第二个产品时，相应的产品数量在原有的INTID上加一
                                intpid = intpid + intchanpincishu;
                                intsalesid = intsalesid + intchanpincishu;
                                strpidchar = strpid;
                                InsertTBProductBatchNO(strpidchar);//在更换新的产品名称前，插入一个BatchNo ID 
                                str11 = str11 + strbox + "," + intpid + "," + strcid + "|";
                                str22 = str22 + strbox + "," + intpid + "," + strcid + "|";
                                str33 = str33 + strcid + "," + strcode.Split('=')[0] + "," + strcode.Split('=')[1];
                                str55 = str55 + strbox + ",";
                            }
                            else
                            {
                                butongpid = false;
                                str11 = str11 + strbox + "," + intpid + "," + strcid + "|";
                                str22 = str22 + strbox + "," + intpid + "," + strcid + "|";
                                str55 = str55 + strbox + ",";
                            }
                        }
                    }
                }
                //====以下处理，则以考虑到扫描完成后，没有出现与之前不一样的PID，保证所有的信息插入到系统中
                if (butongpid==false)
                {
                    updateTBLBarCode(str11);//此时进入到另一个产品名称下,更新TBLBarCode
                    updateTBSBarCode(str22);//此时进入到另一个产品名称下,更新TBSBarCode
                    str33 = str33 + "," + intjishupid + "|";
                    InsertTBSales(str33);//将信息插入到表TBSales中（在换新的产品之前）
                    intsalesid = BackSalesID();
                    str44 = str44 + (intpid) + "," + (intjishupid) + "," + intsalesid + "|";
                    InsertTBSalesDetail(str44);//将信息插入到表TBSalesDetail中（在换新的产品之前）
                    str55 = str55 + "|" + intsalesid;

                    InsertTBSalesBarCode(str55);//将信息插入到表TBSalesBarCode中（在换新的产品之前）

                    
                    //str33 = str33 + "," + (intjishupid - 1).ToString() + "|";
                    //str44 = str44 + (intpid).ToString() + "," + (intjishupid - 1).ToString() + "," + intsalesid.ToString() + "|";
                    //intjishupid = 1;
                    //butongpid = true;
                    //updateTBLBarCode(str11);//此时进入到另一个产品名称下,更新TBLBarCode
                    //updateTBSBarCode(str22);//此时进入到另一个产品名称下,更新TBSBarCode
                    ////InsertTBProductBatchNO(strpidchar);//在更换新的产品名称前，插入一个BatchNo ID 
                    //InsertTBSales(str33);//将信息插入到表TBSales中（在换新的产品之前）
                    //InsertTBSalesDetail(str44);//将信息插入到表TBSalesDetail中（在换新的产品之前）
                    //InsertTBSalesBarCode(str55);//将信息插入到表TBSalesBarCode中（在换新的产品之前）
                    return "2";
                }
                return "1";
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
        catch 
        {
            return null;
        }
    }

    /// <summary>
    /// 返回货单号码的值，以判断是否为不一致的值
    /// </summary>
    /// <returns></returns>
    private bool  BackCode(string compid,string strcode)
    {
        string str = "select * from TBSales where code='" + strcode.Trim () + "'";
        using (var myConn = GetConnectionWL())
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            ds = new DataSet();
            ad = new SqlDataAdapter(str, myConn);
            ad.Fill(ds, "code");
            if ( ds.Tables["code"].Rows.Count >0)
            {
                return true;
            }
            else
            {
                return false;
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
                int int1 =int.Parse ( ds.Tables["lbatchid"].Rows[0][0].ToString ());
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
    private Int32  BackSalesID()
    {
        string str = "select max(id) from TBSales";
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
        string str = "select id from TBLBarcode where LBarcode='" + lbarcode + "'";
        using (var myConn = GetConnectionWL())
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            ds = new DataSet();
            ad = new SqlDataAdapter(str, myConn);
            ad.Fill(ds, "lbarcode");
            if  (ds.Tables["lbarcode"].Rows.Count >0)
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
        string str = "select id from TBSBarcode where LBarcode='" + sbarcode + "'";
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
        string str = "select id from TBSBarcode where LBarcode_ID='" + BackLBarCodeID(LbarCode) + "'";
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
                        str2 = "ProductBatchNo_ID='" + strProDID + "',State='1',WState='2',Customer_ID='" + strcid + "' where LBarcode='" + strbox + "';";
                        str3 = str3 + str1 + str2;
                    }
                }
                //string sql = "";

                //sql = "insert into TB_ALLInsertFaHuo(boxlabel,fhdate,fhpici,agentid,productid,stid,compid,userid,flag,memo,otherinfo) values('" + boxlabel + "','" + fhdate + "','" + fhpici + "','" + agentid + "','" + pid + "','" + stid + "','" + compid + "','" + userid + "','" + flag + "','" + memo + "','" + otherinfo + "')";
                if (!str3.Equals(""))
                {
                    SqlCommand cmd = new SqlCommand(str3, myConn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    myConn.Close();
                    return "true";
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
                        str2 = "ProductBatchNo_ID='" + strProDID + "',State='1',WState='2',Customer_ID='" + strcid + "' where LBarcode_ID='" + strLID + "';";
                        str3 = str3 + str1 + str2;
                    }
                }
                if (!str3.Equals(""))
                {
                    SqlCommand cmd = new SqlCommand(str3, myConn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    myConn.Close();
                    return "true";
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
    /// 插入到 TBSales 
    /// </summary>
    /// <param name="strall"></param>
    /// <returns></returns>
    private string InsertTBSales( string strall)//string CustID, string code, string workdate, string username, string operatorname, string qty, string lbarqty)
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
                        string strworkdate = DateTime.Now.ToShortDateString();
                        string strcreatetime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
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
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    myConn.Close();
                    return "true";
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
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    myConn.Close();
                    return "true";
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
                        dt = BackLBarCodeToSBarCodeID(strLBarCode);
                        if (dt!=null && dt.Rows.Count >0)
                        {
                            for (int hh=0;hh<=dt.Rows.Count -1;hh++)
                            {
                                string strsBarid = dt.Rows[hh][0].ToString().Trim();
                                str2 = "'" + strsid + "','" + strsBarid + "','" + strLBID + "');";
                                str3 = str3 + str1 + str2;
                            }
                        }
                        //string strQty = strarray[i].Split(',')[1].ToString().Trim();
                        //string strLQty = (int.Parse(strQty) * 240).ToString();
                    }
                }
                if (!str3.Equals(""))
                {
                    SqlCommand cmd = new SqlCommand(str3, myConn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    myConn.Close();
                    return "true";
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
    private string InsertTBProductBatchNO(string strall)//string ProductID, string MadeDate)
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
                string str1 = "insert into TBProductBatchNo([Product_ID],[MadeDate]) values(";
                string str2 = "";
                string str3 = "";
                for (int i = 0; i <= strarray.Length - 1; i++)
                {
                    if (!strarray[i].Trim().Equals(""))
                    {
                        string strBatchid = strarray[i].Split(',')[0].Trim();
                        string strMadeDate = DateTime.Now.ToShortDateString();
                        str2 = "'" +  strBatchid + "','" + strMadeDate + "');";
                        str3 = str3 + str1 + str2;
                    }
                }
                if (!str3.Equals(""))
                {
                    SqlCommand cmd = new SqlCommand(str3, myConn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    myConn.Close();
                    return "true";
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
