using System;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;
using TJ.BLL;
using commonlib;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_wuliu_Fahuo_FahuoNewLYH : AuthorPage
{
    private readonly BTJ_RegisterCompanys bagentinfo = new BTJ_RegisterCompanys();
    private readonly BTB_Products_Infor bproductinfo = new BTB_Products_Infor();
    private readonly BTB_StoreHouse bstorehouse = new BTB_StoreHouse();
    private readonly SqlCommand sqlcmdforcheck = new SqlCommand();
    private int boxnum;
    private readonly CommonFunWL com = new CommonFunWL();
    private readonly DBClass db = new DBClass(); 
    //string tempmaxminvalue = "";
    private readonly SqlConnection sqlconcom =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        Button_Insert.Attributes.Add("onclick", "divBlock()");
        if (!IsPostBack)
        {
            //Session.Remove("strList");
            if (GetCookieCompTypeID() == DAConfig.CompTypeIDChangJia.ToString())
            {
                HyperLink_AddProdinfo.Visible = true;
            }
            else
            {
                HyperLink_AddProdinfo.Visible = false;
            }

            FillDDL();
        }
    }

    private void FillDDL()
    {
        string tempagentidstring = com.GetAgentIDStringByCompID(GetCookieCompID());
        if (tempagentidstring.Length > 0)
        {
            ComboBox_DaiLiShangID.DataSource = bagentinfo.GetListsByFilterString("CompID in (" + tempagentidstring + ")");
            ComboBox_DaiLiShangID.DataBind();
        }
        if (GetCookieCompTypeID() == DAConfig.CompTypeIDChangJia.ToString().Trim() || GetCookieCompID() == "1" ||
            GetCookieCompID() == "2")
        {
            ComboBox_ProInfo.DataSource = bproductinfo.GetListsByFilterString("CompID=" + GetCookieCompID());
        }
        else
        {
            CommonFunWL comwl = new CommonFunWL();
            ComboBox_ProInfo.DataSource = comwl.ReturnGetAgentAuthorProductInfo(GetCookieCompID());
        }
        ComboBox_ProInfo.DataBind();
        ComboBox_StoreHouseID.DataSource = bstorehouse.GetListsByFilterString("CompID=" + GetCookieCompID());
        ComboBox_StoreHouseID.DataBind();
    }

    protected void CheckBox_AllSelect_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_AllSelect.Checked)
        {
            CheckBox_SelectAgent.Checked = true;
            ComboBox_DaiLiShangID.Enabled = true;
            CheckBox_SelectProductInfo.Checked = true;
            ComboBox_ProInfo.Enabled = true;
            CheckBox_SelectStoreHouse.Checked = true;
            ComboBox_StoreHouseID.Enabled = true;
            //CheckBox_EnterFaHuoDate.Checked = true;
            TextBox_FaHuoTime.Enabled = true;
            TextBox_FaHuoTime.Text = DateTime.Now.ToString("yyyy-mm-dd");
        }
        else
        {
            CheckBox_SelectAgent.Checked = false;
            ComboBox_DaiLiShangID.DataSource = null;
            ComboBox_DaiLiShangID.DataBind();
            ComboBox_DaiLiShangID.Enabled = false;

            CheckBox_SelectProductInfo.Checked = false;
            ComboBox_ProInfo.DataSource = null;
            ComboBox_ProInfo.DataBind();
            ComboBox_ProInfo.Enabled = false;

            CheckBox_SelectStoreHouse.Checked = false;
            ComboBox_StoreHouseID.DataSource = null;
            ComboBox_StoreHouseID.DataBind();
            ComboBox_StoreHouseID.Enabled = false;

            TextBox_FaHuoTime.Enabled = false;
        }
    }

    protected void CheckBox_SelectAgent_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_SelectAgent.Checked)
        {
            ComboBox_DaiLiShangID.Enabled = true;
        }
        else
        {
            ComboBox_DaiLiShangID.Enabled = false;
        }
    }

    protected void CheckBox_SelectProductInfo_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_SelectProductInfo.Checked)
        {
            ComboBox_ProInfo.Enabled = true;
        }
        else
        {
            ComboBox_ProInfo.Enabled = false;
        }
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
        if (RadioButtonList_Mode.SelectedValue.Equals("1"))
        {
            CheckBox_SelectAgent.Checked = false;
            CheckBox_SelectProductInfo.Checked = false;
            CheckBox_SelectStoreHouse.Checked = false;
            TextBox_StarNum.Visible = false;
            TextBox_EndNum.Visible = false;
            Label_StarNum.Visible = false;
            Label_EndNum.Visible = false;
            //FileUpload1.Enabled = true;
            //Button_upload.Enabled = true;
            Button_Insert.Text = "确定发货";
            Button_Insert.Visible = true;
            Checkbox_Enforced.Visible = false;
            DropDownList_BiLiGuanXi.Visible = false;
            CheckBox_AllSelect.Checked = true;
            CheckBox_CodeSpanTuiHuo.Visible = false;
            CheckBox_AllSelect_CheckedChanged(sender, e);
        }
        else
        {
            if (RadioButtonList_Mode.SelectedValue.Equals("2"))
            {
                TextBox_StarNum.Visible = true;
                TextBox_EndNum.Visible = true;
                Label_StarNum.Visible = true;
                Label_EndNum.Visible = true;
                //FileUpload1.Enabled = false;
                //Button_upload.Enabled = false;
                Button_Insert.Enabled = true;
                Button_Insert.Visible = true;
                DropDownList_BiLiGuanXi.Visible = true;
                ComboBox_DaiLiShangID.Enabled = false;
                ComboBox_ProInfo.Enabled = false;
                Checkbox_Enforced.Visible = true;
                ComboBox_StoreHouseID.Enabled = false;
                CheckBox_AllSelect.Checked = true;
                CheckBox_CodeSpanTuiHuo.Visible = true;
                CheckBox_AllSelect_CheckedChanged(sender, e);
            }
            else
            {
                CheckBox_SelectAgent.Checked = false;
                CheckBox_SelectProductInfo.Checked = false;
                CheckBox_SelectStoreHouse.Checked = false;
                TextBox_StarNum.Visible = false;
                TextBox_EndNum.Visible = false;
                Label_StarNum.Visible = false;
                Label_EndNum.Visible = false;
                //FileUpload1.Enabled = true;
                //Button_upload.Enabled = true;
                Button_Insert.Visible = true;
                Button_Insert.Text = "确定退货";
                Checkbox_Enforced.Visible = false;
                DropDownList_BiLiGuanXi.Visible = false;
                CheckBox_AllSelect.Checked = false;
                CheckBox_CodeSpanTuiHuo.Visible = false;
                //CheckBox_AllSelect_CheckedChanged(sender, e);
            }
        }
    }

    protected void TextBox_ForAgentQuery_TextChanged(object sender, EventArgs e)
    {
    }

    protected void TextBox_ForProductsQuery_TextChanged(object sender, EventArgs e)
    {
    }

    protected void Button_DataCheck_Click(object sender, EventArgs e)
    {
        //Session["dttempline"] = null;
        //Session.Remove("dttempline");
        Label_Exceptions.Visible = false;
        TextBox_ExceptionLines.Text = "";
        TextBox_ExceptionLines.Visible = false;
        DataTable dttempline = new DataTable();
        if (RadioButtonList_Mode.SelectedValue.Equals("1") || RadioButtonList_Mode.SelectedValue.Equals("3"))
        {
            //oFile = Context.Request.Files["Filedata"];
            string repeatstring = string.Empty;
            //if (!uploadfilepath.Trim().Equals(string.Empty))

            if (Session["liuyanghe"] != null)
            {
                dttempline = (DataTable) Session["liuyanghe"];
                // HFMaxMinValue.Value = Session["strList"].ToString();
                Label2.Text = "通过检查,上传数据中有" + Convert.ToString(dttempline.Rows.Count) + "组有效数据,无组重复数据";
                ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('数据检查完毕!');", true);
                Button_DataCheck.Enabled = false;
                Button_Insert.Enabled = true;
            }

            else
            {
                ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('session丢失，请重新上传数据kong!');",
                    true);
            }
        }
    }

    private string checkcodevalid(DataTable dt)
    {
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

    private string AgentName = "";
    private string ProductInfoID = "";
    private string StoreHouseID = "";
    private string AgentID = "";

    protected void Button_Insert_Click(object sender, EventArgs e)
    {
        try
        {
            //Label3.Text = "正在发货中......请勿点击！";
            if (db.SelectFhTable(GetCookieCompID()).Rows[0][0].ToString() != "1")
            {
                db.CreateFhTable(GetCookieCompID());
            }
            string[] fahuoinfo = new string[5];
            Button_Insert.Enabled = false;
            if (RadioButtonList_Mode.SelectedValue.Equals("1") || RadioButtonList_Mode.SelectedValue.Equals("3"))
            {
                string ExceptionString = "";
                if (CheckBox_SelectAgent.Checked && ComboBox_DaiLiShangID.SelectedValue.Trim().Equals("0"))
                {
                    ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('请选择代理商!')", true);
                }
                else
                {
                    if (CheckBox_SelectProductInfo.Checked && ComboBox_ProInfo.SelectedValue.Trim().Equals("0"))
                    {
                        ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('请选择产品!')", true);
                    }
                    else
                    {
                        AgentName = CheckBox_SelectAgent.Checked ? ComboBox_DaiLiShangID.SelectedItem.Text : "";
                        ProductInfoID = CheckBox_SelectProductInfo.Checked ? ComboBox_ProInfo.SelectedValue.Trim() : "";
                        StoreHouseID = CheckBox_SelectStoreHouse.Checked
                            ? ComboBox_StoreHouseID.SelectedValue.Trim()
                            : "";
                        AgentID = CheckBox_SelectAgent.Checked ? ComboBox_DaiLiShangID.SelectedValue.Trim() : "";
                        //if (string.IsNullOrEmpty(TextBox_ChuKuDanHao.Value.Trim()))
                        //{
                        //    fahuoinfo[0] = DateTime.Now.ToString("yyyyMMddHHmmss");
                        //    //TextBox_ChuKuDanHao.Value.Trim();
                        //    //发货批次
                        //}
                        //else
                        //{
                        //    fahuoinfo[0] = TextBox_ChuKuDanHao.Value.Trim();
                        //}
                        fahuoinfo[1] = AgentID; //代理商Id
                        fahuoinfo[2] = ProductInfoID; //产品Id
                        fahuoinfo[3] = Guid.NewGuid().ToString(); //发货Key
                        fahuoinfo[4] = StoreHouseID; //库房ID
                        //fahuoinfo[5] = "";//涉及表名
                        bool EnterFaHuoDate = false;
                        if (CheckBox_EnterFaHuoDate.Checked)
                        {
                            EnterFaHuoDate = true;
                        }
                        //SqlConnection sqlcon = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());
                        SqlConnection sqlcon1 =
                            new SqlConnection(
                                WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());
                        SqlConnection sqlcon2 =
                            new SqlConnection(
                                WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());
                        SqlCommand sqlcmd3 = new SqlCommand();
                        SqlCommand sqlcmd2 = new SqlCommand();
                        sqlcmd3.Connection = sqlcon2;
                        sqlcmd2.Connection = sqlcon2;
                        //sqlcmd.Connection = sqlcon;
                        //sqlcmd.CommandTimeout = 600;
                        //opensqlconn(sqlcon);
                        opensqlconn(sqlcon2);
                        //string TJUserId = GetCookieUID();
                        //SqlTransaction FHTans = sqlcon.BeginTransaction(TJUserId);
                        //sqlcmd.Transaction = FHTans;


                        //string AgentCodeTemp = "";

                        //string ProductInfoCodeTemp = "";
                        string FaHuoPiCi = fahuoinfo[0];

                        //string StoreHouseCodeTemp = "";
                        //string FHKeyString = "";   

                        DataRow[] datarows = ((DataTable) Session["liuyanghe"]).Select("1=1", "AgentCode,PRCode,STCode");
                        if (datarows.Length > 0)
                        {
                            boxnum = datarows.Length;

                            string tablenameinfo = string.Empty;
                            foreach (DataRow dr in datarows)
                            {
                                fahuoinfo[0] = DateTime.Now.ToString("yyyyMMddHHmmss");
                                //TextBox_ChuKuDanHao.Value.Trim();
                                //发货批次
                                FaHuoPiCi = fahuoinfo[0];
                                if (RadioButtonList_Mode.SelectedValue.Equals("1"))
                                {
                                    string[] boxcode = dr[3].ToString().Split('|');
                                    string startcode = Returnlabelcode(boxcode[0]);
                                    string endcode = Returnlabelcode(boxcode[1]);
                                    if (EnterFaHuoDate)
                                    {
                                        RecordFaHuoInfo(startcode, TextBox_FaHuoTime.Text, FaHuoPiCi, dr[1].ToString(),
                                            dr[2].ToString(), dr[4].ToString(), GetCookieCompID(), sqlcon1, endcode, "1");
                                    }
                                    else
                                    {
                                        RecordFaHuoInfo(startcode, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                                            FaHuoPiCi, dr[1].ToString(), dr[2].ToString(), dr[4].ToString(),
                                            GetCookieCompID(), sqlcon1, endcode, "1");
                                    }
                                }
                                if (RadioButtonList_Mode.SelectedValue.Equals("3"))
                                {
                                    string[] boxcode = dr[3].ToString().Split('|');
                                    string startcode = Returnlabelcode(boxcode[0]);
                                    string endcode = Returnlabelcode(boxcode[1]);
                                    if (EnterFaHuoDate)
                                    {
                                        RecordFaHuoInfo(startcode, TextBox_FaHuoTime.Text, FaHuoPiCi, dr[1].ToString(),
                                            dr[2].ToString(), dr[4].ToString(), GetCookieCompID(), sqlcon1, endcode,
                                            "2D");
                                    }
                                    else
                                    {
                                        RecordFaHuoInfo(startcode, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                                            FaHuoPiCi, dr[1].ToString(), dr[2].ToString(), dr[4].ToString(),
                                            GetCookieCompID(), sqlcon1, endcode, "2D");
                                    }
                                }
                            }

                            if (ExceptionString.Trim().Equals(""))
                            {
                                Label2.Text = "";
                                Session.Remove("liuyanghe");
                                // Response.Write("<script>alert('ok!')</script>");
                                ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "divNone()", true);
                            }
                            else
                            {
                                Label2.Text = "本次发货过程中发生异常!实际发货数量为:" + boxnum + "件,异常:" + (datarows.Length - boxnum) +
                                              "件,请仔细排查!";
                                ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "divNone()", true);

                                Label_Exceptions.Visible = true;
                                TextBox_ExceptionLines.Text = ExceptionString.Trim();
                                TextBox_ExceptionLines.Visible = true;
                                Session.Remove("liuyanghe");
                            }


                            //sqlcmd.Dispose();
                            //sqlcon.Close();
                            sqlcmd3.Dispose();
                            sqlcmd2.Dispose();
                            sqlcon2.Close();
                            Label1.Text = "";
                            Button_DataCheck.Enabled = false;
                            Button_Insert.Enabled = false;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info",
                                "alert('本次发货过程中发生异常!请重新上传数据!');", true);
                            //FHTans.Rollback();
                        }
                    }
                }
            }
        }
        catch (Exception aa)
        {
            Response.Write("<script>alert('" + aa + "')</script>");
        }
    }

    private void opensqlconn(SqlConnection sqlconn)
    {
        if (sqlconn.State == ConnectionState.Closed)
        {
            sqlconn.Open();
        }
    }

    /// <summary>
    /// 浏阳河号码段发货
    /// </summary>
    /// <param name="startlabelcode"></param>
    /// <param name="time"></param>
    /// <param name="FHPiCi"></param>
    /// <param name="AgentID"></param>
    /// <param name="ProID"></param>
    /// <param name="STID"></param>
    /// <param name="compid"></param>
    /// <param name="sqlconpara"></param>
    /// <param name="endlabelcode"></param>
    /// <param name="flag"></param>
    private void RecordFaHuoInfo(string startlabelcode, string time, string FHPiCi, string AgentID, string ProID,
        string STID, string compid, SqlConnection sqlconpara, string endlabelcode, string flag)
    {
        string sqlforinsertrukuinfo = "";
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.Connection = sqlconpara;

        sqlforinsertrukuinfo =
            "insert into TB_ALLInsertFaHuo(BoxLabel,FHDate,FHPiCi,AgentID,ProductID,STID,CompID,UserID,Flag,Memo,OperateTime) values('" +
            startlabelcode + "','" + time + "','" + FHPiCi + "','" + AgentID + "','" + ProID + "','" + STID + "'," +
            GetCookieCompID() + "," + GetCookieUID() + ",'" + flag + "'," + endlabelcode + ",'" + DateTime.Now + "')";
        sqlcmd.CommandText = sqlforinsertrukuinfo;
        opensqlconn(sqlconpara);
        sqlcmd.ExecuteNonQuery();
    }


    private string sqlstringforcheck = "";
    private string sqlstringforcheckforreplace = "";
    private string tempvalueforcheck = "";
    private string flag = string.Empty;
    private string islabelcodeisok = string.Empty;

    private bool CheckBoxLabelCodeExist(string tablename, string BoxLabelString, ref string flag,
        ref string islabelcodeisok)
    {
        sqlstringforcheck = "select * from " + tablename + " where BoxLabel01='" + BoxLabelString + "'";
        sqlstringforcheckforreplace = "select * from " + tablename + "_BX where BoxLabel='" + BoxLabelString + "'";
        sqlcmdforcheck.CommandText = sqlstringforcheckforreplace;
        sqlcmdforcheck.Connection = sqlconcom;
        opensqlconn(sqlconcom);
        SqlDataReader sdr = sqlcmdforcheck.ExecuteReader();
        if (sdr.Read())
        {
            tempvalueforcheck = sdr["BoxLabel"].ToString().Trim();
            sdr.Close();
            if (!string.IsNullOrEmpty(tempvalueforcheck))
            {
                flag = "1"; //被替换
                islabelcodeisok = tempvalueforcheck;
                return true;
            }
            else
            {
                sqlcmdforcheck.CommandText = sqlstringforcheck;
                sqlcmdforcheck.Connection = sqlconcom;
                opensqlconn(sqlconcom);
                SqlDataReader sdr1 = sqlcmdforcheck.ExecuteReader();
                if (sdr1.Read())
                {
                    tempvalueforcheck = sdr1["BoxLabel01"].ToString().Trim();
                    sdr1.Close();
                    if (!string.IsNullOrEmpty(tempvalueforcheck))
                    {
                        islabelcodeisok = tempvalueforcheck;
                        flag = "2"; //没有被替换
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

    private string _labelcodeisok = string.Empty; 
    private readonly SqlConnection _sqlcon1 =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());

    public string Returnlabelcode(string cpid)
    {
        string[] strtable = returntablenamebyboxcodeforcodespan(cpid, cpid, _sqlcon1);
        bool _tableok = ReturnExceptionCause(strtable, cpid, ref _labelcodeisok);
        if (_tableok == true && !string.IsNullOrEmpty(_labelcodeisok))
        {
            return _labelcodeisok;
        }
        else
        {
            return cpid;
        }
    }

    /// <summary>
    /// 返回使用号码段发货时涉及到的表的返回
    /// </summary>
    /// <param name="boxcodestringstart"></param>
    /// <param name="boxcodestringend"></param>
    /// <param name="sqlconpara"></param>
    /// <returns></returns>
    private string[] returntablenamebyboxcodeforcodespan(string boxcodestringstart, string boxcodestringend,
        SqlConnection sqlconpara)
    {
        return com.gettableinfo(boxcodestringstart, boxcodestringend, sqlconpara,GetCookieCompID()).Split(',');
    }

    private bool ReturnValue;

    private bool ReturnExceptionCause(string[] tablearray, string BoxLabelString, ref string labelcodeisok)
    {
        ReturnValue = false;
        foreach (string table in tablearray)
        {
            if (!table.Trim().Equals(""))
            {
                if (CheckBoxLabelCodeExist(table, BoxLabelString, ref flag, ref islabelcodeisok))
                {
                    labelcodeisok = islabelcodeisok;
                    ReturnValue = true;
                    break;
                }
            }
        }
        return ReturnValue;
    }

    protected void Button_upload_Click(object sender, EventArgs e)
    {
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["liuyanghe"] = null;
        Session.Remove("liuyanghe");

        ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info",
            "alert('清除成功！');window.location.href='FahuoNewLYH.aspx'", true);
    }
}