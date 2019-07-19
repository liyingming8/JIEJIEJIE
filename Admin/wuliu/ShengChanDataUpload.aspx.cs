using System;
using System.IO;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;
using TJ.BLL;
using commonlib;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_wuliu_ShengChanDataUpload : AuthorPage
{
    private readonly BTB_StoreHouse bstorehouse = new BTB_StoreHouse();
    private readonly SqlCommand sqlcmd = new SqlCommand();
    private readonly SqlCommand sqlcmdforcheck = new SqlCommand();
    private string[] codearrytemp = new string[0];
    private string[] tablenames = new string[0];
    private int repnum;
    //private DataTable dttemp;
    private string _currenttablename = string.Empty;
    //string tempmaxminvalue = "";
    private int _boxnum;
    private readonly DataTable _dttemp = new DataTable();
    private readonly CommonFunWL com = new CommonFunWL();
    private readonly DBClass db = new DBClass();

    private readonly SqlConnection sqlconcom =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        ComboBox_StoreHouseID.DataSource = bstorehouse.GetListsByFilterString("CompID=" + GetCookieCompID());
        ComboBox_StoreHouseID.DataBind();
        //CheckBox_SelectStoreHouse.Checked = false;
        //ComboBox_StoreHouseID.DataSource = null;
        //ComboBox_StoreHouseID.DataBind();
        //ComboBox_StoreHouseID.Enabled = false;
    }

    protected void Button_DataCheck_Click(object sender, EventArgs e)
    {
        Label_Exceptions.Visible = false;
        TextBox_ExceptionLines.Text = "";
        TextBox_ExceptionLines.Visible = false;
        if (RadioButtonList_Mode.SelectedValue.Equals("1") || RadioButtonList_Mode.SelectedValue.Equals("3"))
        {
            string repeatstring = string.Empty;
            //if (!uploadfilepath.Trim().Equals(string.Empty))
            if (!hf_file.Value.Trim().Equals(string.Empty))
            {
                //string currentworkdir = Server.MapPath("/");
                StreamReader sr = new StreamReader(hf_file.Value.Trim());
                string[] stringSeparators = new string[] {"\r\n"};
                codearrytemp = null;
                codearrytemp = sr.ReadToEnd().Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

                #region 原始写法

                //dttemp.Columns.Add("FHPC");
                //dttemp.Columns.Add("AgentCode");
                //dttemp.Columns.Add("PRCode");
                //dttemp.Columns.Add("LBCode");
                //dttemp.Columns.Add("STCode");

                #endregion

                _dttemp.Columns.Add("FHPC");
                _dttemp.Columns.Add("PRCode");
                _dttemp.Columns.Add("DateTimeCode");
                _dttemp.Columns.Add("LBCode");
                _dttemp.Columns.Add("ZJCode");
                _dttemp.Columns.Add("STCode");
                _dttemp.Columns.Add("XZCode");

                foreach (string line in codearrytemp)
                {
                    _dttemp.Rows.Add(line.Split(','));
                }
                Session.Add("dttemp", _dttemp);
                repeatstring = checkcodevalid(_dttemp);
                HF_MaxMinValue.Value = com.MaxAndMinValue(codearrytemp, 3);
                if (!repeatstring.Equals(string.Empty))
                {
                    if (!repeatstring.Trim().Equals("号码不全是数字") && !repeatstring.Trim().Equals("号码长度有误") &&
                        !repeatstring.Trim().Equals("入库数据格式不正确"))
                    {
                        Label2.Text = "通过检查,上传数据中有" + Convert.ToString(_dttemp.Rows.Count - repnum) + "组有效数据," + repnum +
                                      "组重复数据";
                        ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info",
                            "alert('通过检查," + repeatstring.Substring(0, repeatstring.Length - 2) + "中标签号码重复!');", true);
                        Button_DataCheck.Enabled = false;
                        Button_Insert.Enabled = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('请重新上传!');", true);
                    }
                }
                else
                {
                    Label2.Text = "通过检查,上传数据中有" + _dttemp.Rows.Count + "组有效数据,无组重复数据";
                    ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('数据检查完毕!');", true);
                    Button_DataCheck.Enabled = false;
                    Button_Insert.Enabled = true;
                }
                sr.Close();
                //comm comm = new comm();
                //comm.clearfile(hf_file.Value.Trim(), false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('请重新上传数据!');", true);
            }
        }
    }

    private string checkcodevalid(DataTable dt)
    {
        repnum = 0;
        string repnumstring = string.Empty;
        string[] temparray1 = new string[5];
        string[] temparray2 = new string[5];
        foreach (DataRow dr in dt.Rows)
        {
            if (IsNumeric(dr[3].ToString().Trim()))
            {
                if (!dr[3].ToString().Trim().Length.Equals(12))
                {
                    repnumstring = "号码长度有误";
                    break;
                }
            }
            else
            {
                repnumstring = "号码不全是数字";
                break;
            }
        }
        return repnumstring;
    }

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
    private string _storeHouseIDString = "";

    protected void Button_Insert_Click(object sender, EventArgs e)
    {
        string ExceptionString = "";
        bool isok = false;


        try
        {
            //WSID = ComboBox_WorkShop.SelectedValue;
            //WLID = ComboBox_workline.SelectedValue;
            //ProductInfoID = CheckBox_SelectProductInfo.Checked ? ComboBox_ProInfo.SelectedValue.Trim() : "";
            if (ComboBox_StoreHouseID.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('请选择仓库!');", true);
            }
            _storeHouseIDString = ComboBox_StoreHouseID.SelectedValue.Trim();
            bool EnterFaHuoDate = false;
            if (CheckBox_EnterFaHuoDate.Checked)
            {
                EnterFaHuoDate = true;
            }
            SqlConnection sqlcon =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());
            SqlConnection sqlcon1 =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());
            sqlcmd.Connection = sqlcon;
            sqlcmd.CommandTimeout = 600;
            opensqlconn(sqlcon);
            string TJUserId = GetCookieUID();
            SqlTransaction FHTans = sqlcon.BeginTransaction(TJUserId);
            sqlcmd.Transaction = FHTans;

            string FaHuoPiCi = "";
            int xiangmuber = 0;
            string FHKeyString = "";
            string FHRecordinfo = "";
            string temptablename = "";
            DataRow[] datarowcode = ((DataTable) Session["dttemp"]).Select("1=1", "FHPC,PRCode");
            //IList<MTB_WorkShopInfo> wslist;
            //IList<MTB_WorkLineInfo> wllist;
            if (datarowcode.Length > 0)
            {
                _boxnum = datarowcode.Length;
                string tablenameinfo = string.Empty;
                // tablenames = db.gettableinfo(datarowcode[3].ToString()).Split(',');
                tablenames =
                    com.gettableinfo(HF_MaxMinValue.Value.Split(',')[0].Trim(),
                        HF_MaxMinValue.Value.Split(',')[1].Trim(), sqlcon1,GetCookieCompID()).Split(',');
                foreach (DataRow dr in datarowcode)
                {
                    if (FaHuoPiCi.Equals(""))
                    {
                        FaHuoPiCi = dr[0].ToString().Trim();
                    }


                    //tablenames = com.gettableinfo(TempCodeArray[3].Trim(), sqlcon1).Split(',');
                    if (!tablenames.Equals(null))
                    {
                        for (int r = 0; r < tablenames.Length; r++)
                        {
                            if (!(tablenames[r] == null))
                            {
                                tablenameinfo = tablenames[r].Trim();
                                if (!tablenameinfo.Equals(""))
                                {
                                    if (!tablenameinfo.Equals(temptablename))
                                    {
                                        if (!FHRecordinfo.Contains(tablenameinfo))
                                        {
                                            FHKeyString = Guid.NewGuid().ToString();
                                            if (FHRecordinfo.Length > 0)
                                            {
                                                FHRecordinfo += "," + xiangmuber + "*";
                                            }
                                            FHRecordinfo += tablenameinfo + "," + FHKeyString + "," +
                                                            dr[1].ToString().Trim();
                                            xiangmuber = 0;
                                        }
                                        else
                                        {
                                            string[] TempArray = FHRecordinfo.Split('*');
                                            foreach (string line in TempArray)
                                            {
                                                if (!line.Trim().Equals(""))
                                                {
                                                    if (line.Split(',')[0].Trim().Equals("tablenameinfo"))
                                                    {
                                                        FHKeyString = line.Split(',')[2].Trim();
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    string[] temarry = tablenameinfo.Split(',');
                                    if (!_currenttablename.Equals(temarry[0].Trim()))
                                    {
                                        _currenttablename = temarry[0].Trim();
                                    }
                                    sqlcmd.CommandTimeout = 3000;
                                    sqlcmd.CommandText = GetFaHuoSqlStringWH(dr[3].ToString().Trim(), temarry[0],
                                        dr[1].ToString().Trim(), FaHuoPiCi, FHKeyString, dr[4].ToString().Trim(),
                                        EnterFaHuoDate, dr[6].ToString().Trim(), dr[5].ToString().Trim());
                                    if (sqlcmd.ExecuteNonQuery() > 0)
                                    {
                                        xiangmuber++;
                                        isok = true;
                                        break;
                                    }
                                    else
                                    {
                                        sqlcmd.CommandText = GetFaHuoSqlStringForReplaceWH(dr[3].ToString().Trim(),
                                            FHKeyString, temarry[0], dr[1].ToString().Trim(), FaHuoPiCi,
                                            dr[4].ToString().Trim(), EnterFaHuoDate, dr[6].ToString().Trim(),
                                            dr[5].ToString().Trim());
                                        if (sqlcmd.ExecuteNonQuery() > 0)
                                        {
                                            xiangmuber++;
                                            isok = true;
                                            break;
                                        }
                                        else
                                        {
                                            isok = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (!isok)
                    {
                        ExceptionString += "\r\n" + dr.ItemArray.ToString().Trim() + dr[3].ToString().Trim() +
                                           (ReturnExceptionCause(tablenames, dr[3].ToString().Trim())
                                               ? "  已导入"
                                               : "  箱标号码不存在");
                        _boxnum--;
                    }
                    isok = false;
                }

                if (!FHRecordinfo.EndsWith("*"))
                {
                    FHRecordinfo += "," + xiangmuber + "*";
                    string[] FaHuoRecordArray = FHRecordinfo.Split('*');
                    foreach (string line in FaHuoRecordArray)
                    {
                        if (!line.Trim().Equals(""))
                        {
                            RecordFaHuoInfo(EnterFaHuoDate, line.Split(',')[0].Trim(), FaHuoPiCi,
                                line.Split(',')[2].Trim(), GetCookieUID(), sqlcon1, line.Split(',')[1].Trim(),
                                int.Parse(line.Split(',')[3].Trim()), _storeHouseIDString);
                        }
                    }
                }


                FHTans.Commit();
                hf_file.Value = "";
                if (ExceptionString.Trim().Equals(""))
                {
                    ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('导入完毕!')", true);
                    Label2.Text = "";
                }
                else
                {
                    Label2.Text = "本次导入过程中发生异常!实际导入数量为:" + _boxnum + "件,异常:" + (datarowcode.Length - _boxnum) + "件,请仔细排查!";
                    ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info",
                        "alert('本次导入过程中发生异常!实际导入数量为:" + _boxnum + "件,上传文件中有如下号码存在异常:" + ExceptionString.Trim() +
                        ",请仔细排查!');", true);
                    Label_Exceptions.Visible = true;
                    TextBox_ExceptionLines.Text = ExceptionString.Trim();
                    TextBox_ExceptionLines.Visible = true;
                }
                sqlcmd.Dispose();
                sqlcon.Close();
                Label1.Text = "";
                Button_DataCheck.Enabled = false;
                Button_Insert.Enabled = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('本次导入过程中发生异常!请重新上传数据!');",
                    true);
                FHTans.Rollback();
            }
        }
        catch (Exception a)
        {
            db.WriteSysTxt(a.ToString());
        }
    }


    private void opensqlconn(SqlConnection sqlconn)
    {
        if (sqlconn.State == ConnectionState.Closed)
        {
            sqlconn.Open();
        }
    }

    //private void filltableinfo(string startfourcharater, SqlConnection sqlcon)
    //{
    //    DataTable dt = new DataTable();
    //    opensqlconn(sqlcon);
    //    SqlDataAdapter sqlda = new SqlDataAdapter("select * from TB_LabelCodeInfo where startfournum='" + startfourcharater + "'", sqlcon);
    //    sqlda.Fill(dt);
    //    //dttemp = dt;
    //}

    private string GetFaHuoSqlStringForReplace(string boxstring, string tablename, string ProductInfoID,
        string FaHuoPiCi, string StoreHouseID, bool InputDateTime, string FHKey, string WSID, string WLID)
    {
        string BoxLabel01 = com.ReturnFiledValue(tablename + "_BX", "BoxLabel", boxstring, "BoxLabel01");
        string stringtemp = string.Empty;
        if (InputDateTime)
        {
            stringtemp = "update " + tablename + " set PID=" + ProductInfoID + ",FHKey='" + FHKey +
                         "', ShengChanShiJian='" + Convert.ToDateTime(TextBox_FaHuoTime.Text.Trim()) + "',STID=" +
                         StoreHouseID + ",WSID=" + WSID + ",WLID=" + WLID + ",CompID=" + GetCookieCompID() +
                         " where BoxLabel01='" + boxstring + "'";
        }
        else
        {
            stringtemp = "update " + tablename + " set PID=" + ProductInfoID + ",FHKey='" + FHKey +
                         "', ShengChanShiJian='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "',STID=" + StoreHouseID +
                         ",WSID=" + WSID + ",WLID=" + WLID + ",CompID=" + GetCookieCompID() + " where BoxLabel01='" +
                         boxstring + "'";
        }
        return stringtemp;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="temtablenamestring"></param>
    /// <param name="FHPiCi"></param>
    /// <param name="AgentName"></param>
    /// <param name="sqlconpara"></param>
    private void RecordFaHuoInfo(bool InputDateTime, string temtablenamestring, string FHPiCi, string ProID,
        string UserID, SqlConnection sqlconpara, string FHKey, int XiangNumber, string STID)
    { 
        string sqlforinsertrukuinfo = "";
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.Connection = sqlconpara;
        opensqlconn(sqlconpara);
        string[] tablenamearray = temtablenamestring.Split(',');
        for (int i = 0; i < tablenamearray.Length; i++)
        {
            if (!tablenamearray[i].Trim().Equals(string.Empty))
            {
                //sqlfordeletestring = "delete from TB_FaHuoInfo_" + GetCookieCompID() + " where FHTypeID=1 and AgentID=0 and ProID=" + ProID + " and  FHPiCi ='" + FHPiCi + "' and TableNameInfo='" + tablenamearray[i].Trim() + "'";
                //sqlcmd.CommandText = sqlfordeletestring;
                //opensqlconn(sqlconpara);
                //sqlcmd.ExecuteNonQuery();
                if (InputDateTime)
                {
                    sqlforinsertrukuinfo = "insert into TB_FaHuoInfo_" + GetCookieCompID() +
                                           "(FHPiCi,FHTypeID,FHDate,AgentID,FHUserID,TableNameInfo,FHKey,ProID,XiangNumber,CompID,STID) values('" +
                                           FHPiCi + "',1,'" + Convert.ToDateTime(TextBox_FaHuoTime.Text.Trim()) + "',0," +
                                           GetCookieUID() + ",'" + tablenamearray[i].Trim() + "','" + FHKey + "'," +
                                           ProID + "," + XiangNumber + "," + GetCookieCompID() + "," + STID + ")";
                    sqlcmd.CommandText = sqlforinsertrukuinfo;
                    opensqlconn(sqlconpara);
                    sqlcmd.ExecuteNonQuery();
                }
                else
                {
                    sqlforinsertrukuinfo = "insert into TB_FaHuoInfo_" + GetCookieCompID() +
                                           "(FHPiCi,FHTypeID,FHDate,AgentID,FHUserID,TableNameInfo,FHKey,ProID,XiangNumber,CompID,STID) values('" +
                                           FHPiCi + "',1,'" + DateTime.Now + "',0," + GetCookieUID() + ",'" +
                                           tablenamearray[i].Trim() + "','" + FHKey + "'," + ProID + "," + XiangNumber +
                                           "," + GetCookieCompID() + "," + STID + ")";
                    sqlcmd.CommandText = sqlforinsertrukuinfo;
                    opensqlconn(sqlconpara);
                    sqlcmd.ExecuteNonQuery();
                }
            }
        }
    }

    private string ReturnBoxLabelCode(string LabelCode, string tablename)
    {
        sqlcmd.CommandText = "select W.BoxLabel01 From " + tablename + " as W where W.BoxLabel01='" + LabelCode + "'";
        sqlcmd.Connection = sqlconcom;
        opensqlconn(sqlconcom);
        SqlDataReader sdr = sqlcmd.ExecuteReader();
        string BoxLabel01 = "";
        if (sdr.Read())
        {
            BoxLabel01 = sdr["BoxLabel01"].ToString().Trim();
            sdr.Close();
        }
        else
        {
            sdr.Close();
            sqlcmd.CommandText = "select W.BoxLabel01 From " + tablename + " as W," + tablename +
                                 "_BX as BX where (BX.BoxLabel='" + LabelCode + "' and BX.BoxLabel01=W.BoxLabel01)";
            SqlDataReader sdr1 = sqlcmd.ExecuteReader();
            if (sdr1.Read())
            {
                BoxLabel01 = sdr1["BoxLabel01"].ToString().Trim();
            }
            sdr1.Close();
        }
        if (BoxLabel01.Equals(""))
        {
            BoxLabel01 = LabelCode;
        }
        return BoxLabel01;
    }

    /// <summary>
    /// 温河替换
    /// </summary>
    /// <param name="boxstring"></param>
    /// <param name="tablename"></param>
    /// <param name="ProductInfoID"></param>
    /// <param name="FaHuoPiCi"></param>
    /// <param name="StoreHouseID"></param>
    /// <param name="InputDateTime"></param>
    /// <param name="FHKey"></param>
    /// <param name="WSID"></param>
    /// <param name="WLID"></param>
    /// <returns></returns>
    private string GetFaHuoSqlStringForReplaceWH(string boxstring, string FHkey, string tablename, string ProductInfoID,
        string FaHuoPiCi, string KuXian, bool InputDateTime, string ZhiJian, string WLID)
    {
        string BoxLabel01 = com.ReturnFiledValue(tablename + "_BX", "BoxLabel", boxstring, "BoxLabel01");
        string stringtemp = string.Empty;
        if (InputDateTime)
        {
            stringtemp = "update " + tablename + " set PID=" + ProductInfoID + ",FHKey='" + FHkey + "',BeiZhu='" +
                         FaHuoPiCi + "',WSID='" + KuXian + "', ShengChanShiJian='" +
                         Convert.ToDateTime(TextBox_FaHuoTime.Text.Trim()) + "',STID='" + ZhiJian + "',WLID='" + WLID +
                         "',CompID=" + GetCookieCompID() + " where BoxLabel01='" + BoxLabel01 + "'";
        }
        else
        {
            stringtemp = "update " + tablename + " set PID=" + ProductInfoID + ",FHKey='" + FHkey + "',BeiZhu='" +
                         FaHuoPiCi + "',WSID='" + KuXian + "', ShengChanShiJian='" +
                         DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "',STID=" + ZhiJian + ",WLID=" + WLID + ",CompID=" +
                         GetCookieCompID() + " where BoxLabel01='" + BoxLabel01 + "'";
        }
        return stringtemp;
    }

    /// <summary>
    /// 返回入库SQL(温河)
    /// </summary>
    /// <param name="boxstring"></param>
    /// <param name="tablename"></param>
    /// <param name="ProductInfoID"></param>
    /// <param name="FaHuoPiCi"></param>
    /// <param name="KuXian"></param>
    /// <param name="InputDateTime"></param>
    /// <param name="ZhiJian"></param>
    /// <param name="?"></param>
    /// <param name="WLID"></param>
    /// <returns></returns>
    private string GetFaHuoSqlStringWH(string boxstring, string tablename, string ProductInfoID, string FaHuoPiCi,
        string FHkey, string KuXian, bool InputDateTime, string ZhiJian, string WLID)
    {
        string stringtemp = string.Empty;
        if (RadioButtonList_Mode.SelectedValue.Equals("1"))
        {
            if (InputDateTime)
            {
                stringtemp = "update " + tablename + " set PID=" + ProductInfoID + ",FHKey='" + FHkey + "',BeiZhu='" +
                             FaHuoPiCi + "',WSID='" + KuXian + "', ShengChanShiJian='" +
                             Convert.ToDateTime(TextBox_FaHuoTime.Text.Trim()) + "',STID='" + ZhiJian + "',WLID='" +
                             WLID + "',CompID=" + GetCookieCompID() + " where BoxLabel01='" + boxstring + "'";
            }
            else
            {
                stringtemp = "update " + tablename + " set PID=" + ProductInfoID + ",FHKey='" + FHkey + "',BeiZhu='" +
                             FaHuoPiCi + "',WSID='" + KuXian + "', ShengChanShiJian='" +
                             DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "',STID=" + ZhiJian + ",WLID=" + WLID +
                             ",CompID=" + GetCookieCompID() + " where BoxLabel01='" + boxstring + "'";
            }
        }
        return stringtemp;
    }

    /// <summary>
    /// 返回入库SQL(原始的方法)
    /// </summary>
    /// <param name="boxstring">箱标号码</param>
    /// <param name="tablename">表名</param>
    /// <param name="boxnum">组箱数量</param>
    /// <param name="storehouseid">货仓ID</param>
    /// <param name="rukupici">入库批次</param>
    /// <param name="userid">入库人ID</param>
    /// <param name="chanpinid">品名ID</param>
    /// <param name="chanpintypeid">产品类别</param>
    /// <param name="chanpinguigeid">产品规格</param>
    /// <returns></returns>
    private string GetFaHuoSqlString(string boxstring, string tablename, string ProductInfoID, string FaHuoPiCi,
        string StoreHouseID, bool InputDateTime, string FHKey, string WSID, string WLID)
    {
        string stringtemp = string.Empty;
        if (RadioButtonList_Mode.SelectedValue.Equals("1"))
        {
            if (InputDateTime)
            {
                stringtemp = "update " + tablename + " set PID=" + ProductInfoID + ",FHKey='" + FHKey +
                             "', ShengChanShiJian='" + Convert.ToDateTime(TextBox_FaHuoTime.Text.Trim()) + "',STID=" +
                             StoreHouseID + ",WSID=" + WSID + ",WLID=" + WLID + ",CompID=" + GetCookieCompID() +
                             " where BoxLabel01='" + boxstring + "'";
            }
            else
            {
                stringtemp = "update " + tablename + " set PID=" + ProductInfoID + ",FHKey='" + FHKey +
                             "', ShengChanShiJian='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "',STID=" +
                             StoreHouseID + ",WSID=" + WSID + ",WLID=" + WLID + ",CompID=" + GetCookieCompID() +
                             " where BoxLabel01='" + boxstring + "'";
            }
        }
        return stringtemp;
    }

    private bool ReturnValue;

    private bool ReturnExceptionCause(string[] tablearray, string BoxLabelString)
    {
        ReturnValue = false;
        foreach (string table in tablearray)
        {
            if (!table.Trim().Equals(""))
            {
                if (CheckBoxLabelCodeExist(table, BoxLabelString))
                {
                    ReturnValue = true;
                    break;
                }
            }
        }
        return ReturnValue;
    }

    private string sqlstringforcheck = "";
    private string sqlstringforcheckforreplace = "";
    private string tempvalueforcheck = "";

    private bool CheckBoxLabelCodeExist(string tablename, string BoxLabelString)
    {
        sqlstringforcheck = "select count(BoxLabel01) as num from " + tablename + " where BoxLabel01='" + BoxLabelString +
                            "'";
        sqlstringforcheckforreplace = "select count(*) as num from " + tablename + "_BX where BoxLabel='" +
                                      BoxLabelString + "'";
        sqlcmdforcheck.CommandText = sqlstringforcheck;
        sqlcmdforcheck.Connection = sqlconcom;
        opensqlconn(sqlconcom);
        SqlDataReader sdr = sqlcmdforcheck.ExecuteReader();
        if (sdr.Read())
        {
            tempvalueforcheck = sdr["num"].ToString().Trim();
            sdr.Close();
            if (Convert.ToInt32(tempvalueforcheck) > 0)
            {
                return true;
            }
            else
            {
                sqlcmdforcheck.CommandText = sqlstringforcheckforreplace;
                sqlcmdforcheck.Connection = sqlconcom;
                opensqlconn(sqlconcom);
                SqlDataReader sdr1 = sqlcmdforcheck.ExecuteReader();
                if (sdr1.Read())
                {
                    tempvalueforcheck = sdr1["num"].ToString().Trim();
                    sdr1.Close();
                    if (Convert.ToInt32(tempvalueforcheck) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            sdr.Close();
            return false;
        }
    }

    private string uploadfilepath = "";

    protected void Button_upload_Click(object sender, EventArgs e)
    {
        Label_Exceptions.Visible = false;
        TextBox_ExceptionLines.Text = "";
        TextBox_ExceptionLines.Visible = false;
        if (FileUpload1.HasFile)
        {
            try
            {
                FileInfo file = new FileInfo(FileUpload1.PostedFile.FileName);
                if (file.Extension.ToLower().Equals(".txt"))
                {
                    // 保存文件
                    uploadfilepath = Server.MapPath(@"/Admin/wuliu/Fahuo/files/") +
                                     DateTime.Now.ToString("yyyyMMddhhmmssss") + ".txt";
                    FileUpload1.SaveAs(uploadfilepath);
                    ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('上传成功!');", true);
                    Label1.Text = "已上传文件:" + FileUpload1.FileName;
                    hf_file.Value = uploadfilepath;
                    Button_DataCheck.Enabled = true;
                    Button_DataCheck_Click(sender, e);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info",
                        "alert('请上传格式为(.txt)的盘点机数据库文件');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('" + ex.Message + "');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('请选取数据文件!');", true);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["dttempline"] = null;
        Session.Remove("dttempline");
        ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info",
            "alert('清除成功！');window.location.href='ShengChanDataUpload.aspx'", true);
    }

    protected void CheckBox_EnterFaHuoDate_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_EnterFaHuoDate.Checked)
        {
            TextBox_FaHuoTime.Enabled = true;
            TextBox_FaHuoTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        else
        {
            TextBox_FaHuoTime.Enabled = false;
        }
    }

    protected void RadioButtonList_Mode_SelectedIndexChanged(object sender, EventArgs e)
    {
        Button_Insert.Visible = true;
    }

    protected void CheckBox_SelectStoreHouse_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_SelectStoreHouse.Checked)
        {
            ComboBox_StoreHouseID.Enabled = true;
        }
        else
        {
            ComboBox_StoreHouseID.Enabled = false;
        }
    }
}