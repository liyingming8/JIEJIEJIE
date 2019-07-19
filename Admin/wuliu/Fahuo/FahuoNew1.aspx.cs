using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;
using TJ.BLL;
using TJ.Model;
using commonlib;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_wuliu_Fahuo_FahuoNewPeng : AuthorPage
{
    private readonly BTJ_RegisterCompanys bagentinfo = new BTJ_RegisterCompanys();
    private readonly BTB_Products_Infor bproductinfo = new BTB_Products_Infor();
    private readonly BTB_StoreHouse bstorehouse = new BTB_StoreHouse();
    private readonly BTB_CompAgentInfo bbagent = new BTB_CompAgentInfo();
    private readonly SqlCommand sqlcmd = new SqlCommand();
    private readonly SqlCommand sqlcmdforcheck = new SqlCommand(); 
    private bool LabelIsNull;
    private readonly CommonFunWL com = new CommonFunWL();
     private readonly DBClass db = new DBClass();
     private readonly DataTable GoodFH = new DataTable(); 
    private readonly DataTable GoodTH = new DataTable();
    //string tempmaxminvalue = "";
    private readonly SqlConnection sqlconcom =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hf_compid.Value = GetCookieCompID();
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
        string tempagentidstring = com.GetAgentIDStringByCompID(hf_compid.Value);
        if (tempagentidstring.Length > 0)
        {
            ComboBox_DaiLiShangID.DataSource = bagentinfo.GetListsByFilterString("CompID in (" + tempagentidstring + ")");
            ComboBox_DaiLiShangID.DataBind();
        }
        if (GetCookieCompTypeID() == DAConfig.CompTypeIDChangJia.ToString().Trim() || hf_compid.Value == "1")
        {
            ComboBox_ProInfo.DataSource = bproductinfo.GetListsByFilterString("CompID=" + hf_compid.Value);
        }
        else
        {
            CommonFunWL comwl = new CommonFunWL();
            ComboBox_ProInfo.DataSource = comwl.ReturnGetAgentAuthorProductInfo(hf_compid.Value);
        }
        ComboBox_ProInfo.DataBind();
        ComboBox_StoreHouseID.DataSource = bstorehouse.GetListsByFilterString("CompID=" + hf_compid.Value);
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
            TextBox_FaHuoTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
        int godnum = 0;
        int cfnum = 0;
        int thnum = 0;

        bool cgflg = false;
        bool notfj = false;
        string Errorsting = "";
        string cpid = "";
        DataTable BadFH = new DataTable();
        SqlConnection sqlcon1 =
            new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());
        SqlConnection sqlcon2 =
            new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());


        //发货
        if (RadioButtonList_Mode.SelectedValue.Equals("1"))
        {
            //oFile = Context.Request.Files["Filedata"];
            string repeatstring = string.Empty;
            //if (!uploadfilepath.Trim().Equals(string.Empty))


            //上传数据成功
            if (Session["dttempline"] != null)
            {
                dttempline = (DataTable) Session["dttempline"];
                //HFMaxMinValue.Value = Session["strList"].ToString();
                //tablenames = comm.gettableinfo(HFMaxMinValue.Value.Split(',')[0].Trim(), HFMaxMinValue.Value.Split(',')[1].Trim(), sqlcon1).Split(',');
                DataRow[] datarows = ((DataTable) Session["dttempline"]).Select("1=1", "AgentCode,PRCode,STCode");

                //逐条 数据检测是否可以发货

                if (datarows.Length > 0)
                {
                    GoodFH.Columns.Add("FHPC");
                    GoodFH.Columns.Add("AgentCode");
                    GoodFH.Columns.Add("PRCode");
                    GoodFH.Columns.Add("LBCode");
                    GoodFH.Columns.Add("STCode");
                    GoodFH.Columns.Add("TName");

                    //循环遍历每一行

                    string sj = "";

                    foreach (DataRow dr in datarows)
                    {
                        //检测是否有重复数据
                        cgflg = false;
                        notfj = false;
                        if (cpid != dr[3].ToString().Trim())
                        {
                            cpid = dr[3].ToString().Trim();
                            foreach (string tbname in db.gettableinfo(dr[3].ToString()).Split(','))
                            {
                                if (tbname != "")
                                {
                                    //检测是不是有效发货标签序号（是否存在基础数据，是否发过货，是否父级发货）

                                    if (CheckBoxLabelCodeNew(tbname, dr[3].ToString().Trim(), hf_compid.Value))
                                    {
                                        IList<MTB_CompAgentInfo> mAgent =
                                            bbagent.GetListsByFilterString("AgentID=" + hf_compid.Value);

                                        //是否经销商发货

                                        if (mAgent.Count > 0)
                                        {
                                            int coid = Convert.ToInt32(hf_compid.Value);
                                            foreach (MTB_CompAgentInfo ma in mAgent)
                                            {
                                                coid = ma.CompID;
                                            }
                                            if (CheckIsPrF(tbname, dr[3].ToString().Trim(), coid))
                                            {
                                                sj = dr[0].ToString().Trim() + "," + dr[1].ToString().Trim() + "," +
                                                     dr[2].ToString().Trim() + "," + dr[3].ToString().Trim() + "," +
                                                     dr[4].ToString().Trim() + "," + tbname;
                                                GoodFH.Rows.Add(sj.Split(','));
                                                godnum++;
                                                cgflg = true;
                                            }
                                            else
                                            {
                                                notfj = true;
                                                //Errorsting += "\r\n" + dr[3].ToString().Trim() + "不是上级正常发货数据";
                                            }
                                        }
                                        else
                                        {
                                            sj = dr[0].ToString().Trim() + "," + dr[1].ToString().Trim() + "," +
                                                 dr[2].ToString().Trim() + "," + dr[3].ToString().Trim() + "," +
                                                 dr[4].ToString().Trim() + "," + tbname;
                                            GoodFH.Rows.Add(sj.Split(','));
                                            godnum++;
                                            cgflg = true;
                                        }
                                    }
                                }
                            } //循环表完毕

                            if (!cgflg)
                            {
                                if (notfj)
                                {
                                    Errorsting += "\r\n" + dr[3].ToString().Trim() + "不是上级正常发货数据";
                                }
                                else
                                {
                                    Errorsting += "\r\n" + dr[3].ToString().Trim() + (LabelIsNull ? "号码已发货" : "箱标号码不存在");
                                }
                            }
                        }

                            //有重复数据
                        else
                        {
                            Errorsting += "\r\n" + dr[3].ToString().Trim() + "是重复数据";
                            cfnum++;
                        }
                    } //循环标签完毕
                }

                //repeatstring = checkcodevalid(dttempline);
                //if (!repeatstring.Equals(string.Empty))
                //{
                //    if (!repeatstring.Trim().Equals("号码不全是数字") && !repeatstring.Trim().Equals("号码长度有误") && !repeatstring.Trim().Equals("入库数据格式不正确"))
                //    {
                Session.Add("GoodFH", GoodFH);
                Label2.Text = "通过检查,上传数据中有" + godnum + "组有效发货数据," + (datarows.Length - godnum) + "组无效发货数据," + cfnum +
                              "组重复数据";
                //ScriptManager.RegisterStartupScript(updatepanel, this.GetType(), "info", "alert('通过检查," + repeatstring.Substring(0, repeatstring.Length - 2) + "中标签号码重复!');", true);

                TextBox_ExceptionLines.Text = Errorsting.Trim();
                TextBox_ExceptionLines.Visible = true;
                if (godnum > 0)
                {
                    Button_DataCheck.Enabled = false;

                    Button_Insert.Enabled = true;
                }


                //    }
                //    else
                //    {
                //        ScriptManager.RegisterStartupScript(updatepanel, this.GetType(), "info", "alert('请重新上传!');", true);
                //    }
                //}
                //else
                //{
                //    Label2.Text = "通过检查,上传数据中有" + Convert.ToString(dttempline.Rows.Count) + "组有效数据,无组重复数据";
                //    ScriptManager.RegisterStartupScript(updatepanel, this.GetType(), "info", "alert('数据检查完毕!');", true);
                //    Button_DataCheck.Enabled = false;
                //    Button_Insert.Enabled = true;
                //}
            }

                //上传数据不成功
            else
            {
                ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('请重新上传数据!');", true);
            }
        }

            //退货
        else if (RadioButtonList_Mode.SelectedValue.Equals("3"))
        {
            string repeatstring = string.Empty;

            //上传数据成功
            if (Session["dttempline"] != null)
            {
                dttempline = (DataTable) Session["dttempline"];
                HFMaxMinValue.Value = Session["strList"].ToString();
                //tablenames = comm.gettableinfo(HFMaxMinValue.Value.Split(',')[0].Trim(), HFMaxMinValue.Value.Split(',')[1].Trim(), sqlcon1).Split(',');
                DataRow[] datarows = ((DataTable) Session["dttempline"]).Select("1=1", "AgentCode,PRCode,STCode");

                //逐条 数据检测是否可以退货

                if (datarows.Length > 0)
                {
                    GoodTH.Columns.Add("FHPC");
                    GoodTH.Columns.Add("AgentCode");
                    GoodTH.Columns.Add("PRCode");
                    GoodTH.Columns.Add("LBCode");
                    GoodTH.Columns.Add("STCode");
                    GoodTH.Columns.Add("TName");

                    //循环遍历每一行

                    string sj = "";

                    foreach (DataRow dr in datarows)
                    {
                        //检测是否有重复数据
                        if (cpid != dr[3].ToString().Trim())
                        {
                            cpid = dr[3].ToString().Trim();
                            foreach (string tbname in db.gettableinfo(dr[3].ToString()).Split(','))
                            {
                                if (tbname != "")
                                {
                                    //检测是不是有效发货标签序号（是否存在基础数据，是否发过货，是否父级发货）
                                    DataTable dt = db.GetFhPiciandFhKey(tbname, dr[3].ToString().Trim(), hf_compid.Value);

                                    if (CheckBoxLabelCodeExist(tbname, dr[3].ToString().Trim()) && dt.Rows.Count > 0)
                                    {
                                        sj = dr[0].ToString().Trim() + "," + dr[1].ToString().Trim() + "," +
                                             dr[2].ToString().Trim() + "," + dr[3].ToString().Trim() + "," +
                                             dr[4].ToString().Trim() + "," + tbname;
                                        GoodTH.Rows.Add(sj.Split(','));
                                        thnum++;
                                        cgflg = true;
                                    }
                                }
                            }

                            if (!cgflg)
                            {
                                Errorsting += "\r\n" + dr[3].ToString().Trim() + (LabelIsNull ? "号码未发货" : "箱标号码不存在");
                            }
                        }

                            //有重复数据
                        else
                        {
                            Errorsting += "\r\n" + dr[3].ToString().Trim() + "是重复数据";
                            cfnum++;
                        }
                    } //循环标签完毕
                }


                Label2.Text = "通过检查,上传数据中有" + thnum + "组有效退货数据," + (datarows.Length - thnum) + "组无效退货数据," + cfnum +
                              "组重复数据";

                TextBox_ExceptionLines.Text = Errorsting.Trim();

                TextBox_ExceptionLines.Visible = true;
                Session.Add("GoodTH", GoodTH);
                if (thnum > 0)
                {
                    Button_DataCheck.Enabled = false;

                    Button_Insert.Enabled = true;
                }
            }

                //上传数据不成功
            else
            {
                ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('请重新上传数据!');", true);
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
        string[] fahuoinfo = new string[5];
        DateTime star = DateTime.Now;
        Button_Insert.Enabled = false;
        if (RadioButtonList_Mode.SelectedValue.Equals("1") || RadioButtonList_Mode.SelectedValue.Equals("3"))
        {  
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
                    StoreHouseID = CheckBox_SelectStoreHouse.Checked ? ComboBox_StoreHouseID.SelectedValue.Trim() : "";
                    AgentID = CheckBox_SelectAgent.Checked ? ComboBox_DaiLiShangID.SelectedValue.Trim() : "";
                    fahuoinfo[0] = DateTime.Now.ToString("yyyy-MM-dd"); //TextBox_ChuKuDanHao.Value.Trim();//发货批次
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

                    string AgentCode = "";
                    //string AgentCodeTemp = "";
                    string ProductInfoCode = "";
                    //string ProductInfoCodeTemp = "";
                    string FaHuoPiCi = fahuoinfo[0];
                    string StoreHouseCode = "";
                    //string StoreHouseCodeTemp = "";
                    //string FHKeyString = "";
                    string FHRecordinfo = "";
                    string temptablenamestring = "";
                    BTB_Products_Infor BLPI = new BTB_Products_Infor();

                    BTB_StoreHouse BLSH = new BTB_StoreHouse();
                    IList<MTJ_RegisterCompanys> alist;
                    IList<MTB_Products_Infor> plist;
                    IList<MTB_StoreHouse> slist;
                    int oknum = 0;

                    DataRow[] datarows = ((DataTable) Session["GoodFH"]).Select("1=1", "AgentCode,PRCode,STCode");

                    if (RadioButtonList_Mode.SelectedValue.Equals("3"))
                    {
                        datarows = ((DataTable) Session["GoodTH"]).Select("1=1", "AgentCode,PRCode,STCode");
                    }


                    //DataRow[] datarows = GoodFH.Select("1=1", "AgentCode,PRCode,STCode");
                    if (datarows.Length > 0)
                    { 
                        //tablenames = comm.gettableinfo(HFMaxMinValue.Value.Split(',')[0].Trim(), HFMaxMinValue.Value.Split(',')[1].Trim(), sqlcon1).Split(',');

                        foreach (DataRow dr in datarows)
                        {
                            if (!CheckBox_SelectAgent.Checked)
                            {
                                if (!AgentCode.Equals(dr[1].ToString().Trim()) || AgentName.Trim().Equals(""))
                                {
                                    if (oknum > 0 && AgentCode.Trim().Length > 0)
                                    {
                                        FHRecordinfo += fahuoinfo[0] + "," + fahuoinfo[1] + "," + fahuoinfo[2] + "," +
                                                        fahuoinfo[3] + "," + fahuoinfo[4] + "," +
                                                        (temptablenamestring.StartsWith(",")
                                                            ? temptablenamestring.Substring(1)
                                                            : temptablenamestring + "," + oknum) + "*";
                                        oknum = 0;
                                        temptablenamestring = "";
                                        fahuoinfo[3] = Guid.NewGuid().ToString();
                                    }
                                    //原有的查询方式
                                    //alist = bagentinfo.GetListsByFilterString("Agent_Code='" + dr[1].ToString().Trim() + "' and CompID=" + hf_compid.Value);
                                    alist = bagentinfo.GetListsByFilterString("CompID=" + dr[1].ToString().Trim());

                                    if (alist.Count > 0)
                                    {
                                        AgentName = alist[0].CompName;
                                        AgentID = alist[0].CompID.ToString();
                                        fahuoinfo[1] = AgentID;
                                    }
                                    AgentCode = dr[1].ToString().Trim();
                                }
                            }
                            if (!CheckBox_SelectProductInfo.Checked)
                            {
                                if (!ProductInfoCode.Equals(dr[2].ToString().Trim()) || ProductInfoID.Trim().Equals(""))
                                {
                                    if (oknum > 0 && ProductInfoCode.Trim().Length > 0)
                                    {
                                        FHRecordinfo += fahuoinfo[0] + "," + fahuoinfo[1] + "," + fahuoinfo[2] + "," +
                                                        fahuoinfo[3] + "," + fahuoinfo[4] + "," +
                                                        (temptablenamestring.StartsWith(",")
                                                            ? temptablenamestring.Substring(1)
                                                            : temptablenamestring) + "," + oknum + "*";
                                        temptablenamestring = "";
                                        oknum = 0;
                                        fahuoinfo[3] = Guid.NewGuid().ToString();
                                    }
                                    //plist = BLPI.GetListsByFilterString("Product_Code='" + dr[2].ToString() + "' and CompID="+hf_compid.Value);
                                    plist = BLPI.GetListsByFilterString("Infor_ID='" + dr[2] + "'");
                                    if (plist.Count > 0)
                                    {
                                        ProductInfoID = plist[0].Infor_ID.ToString();
                                        fahuoinfo[2] = ProductInfoID;
                                    }
                                    ProductInfoCode = dr[2].ToString();
                                }
                            }
                            if (!CheckBox_SelectStoreHouse.Checked)
                            {
                                if (!dr[4].ToString().Trim().Equals(StoreHouseCode) || StoreHouseCode.Trim().Equals(""))
                                {
                                    if (oknum > 0 && StoreHouseCode.Trim().Length > 0)
                                    {
                                        FHRecordinfo += fahuoinfo[0] + "," + fahuoinfo[1] + "," + fahuoinfo[2] + "," +
                                                        fahuoinfo[3] + "," + fahuoinfo[4] + "," +
                                                        (temptablenamestring.StartsWith(",")
                                                            ? temptablenamestring.Substring(1)
                                                            : temptablenamestring) + "," + oknum + "*";
                                        temptablenamestring = "";
                                        oknum = 0;
                                        fahuoinfo[3] = Guid.NewGuid().ToString();
                                    }
                                    //slist = BLSH.GetListsByFilterString("StoreHouseCode='" + dr[4].ToString() + "' and CompID=" + hf_compid.Value);原来的
                                    slist = BLSH.GetListsByFilterString("STID=" + dr[4]);
                                    if (slist.Count > 0)
                                    {
                                        StoreHouseID = slist[0].STID.ToString();
                                        fahuoinfo[4] = StoreHouseID;
                                        StoreHouseCode = dr[4].ToString();
                                    }
                                    //else
                                    //{
                                    //    ScriptManager.RegisterStartupScript(updatepanel, this.GetType(), "info", "alert('请指定货仓!');", true);
                                    //    FHTans.Rollback();
                                    //    break;
                                    //}
                                }
                            }

                            //退货
                            if (RadioButtonList_Mode.SelectedValue.Equals("3"))
                            {
                                if (!temptablenamestring.Contains("|" + dr[5].ToString().Trim()))
                                {
                                    temptablenamestring += "|" + dr[5].ToString().Trim();
                                }

                                DataTable dt = db.GetFhPiciandFhKey(dr[5].ToString().Trim(), dr[3].ToString().Trim(),
                                    hf_compid.Value);

                                //DataTable dt = db.GetFhPiciandFhKey(tbname, dr[3].ToString().Trim(), hf_compid.Value);
                                string pici = dt.Rows[0]["FHPici"].ToString();
                                string key = dt.Rows[0]["FHKey"].ToString();
                                sqlcmd3.CommandText = GetTuiHuoString(dr[3].ToString().Trim(), dr[5].ToString().Trim(),
                                    pici, key);
                                sqlcmd3.ExecuteNonQuery(); 
                            }

                                //发货
                            else
                            {
                                if (!temptablenamestring.Contains("|" + dr[5].ToString().Trim()))
                                {
                                    temptablenamestring += "|" + dr[5].ToString().Trim();
                                }

                                sqlcmd3.CommandText = GetFaHuoSqlString(dr[3].ToString().Trim(), dr[5].ToString().Trim(),
                                    AgentName, ProductInfoID, FaHuoPiCi, StoreHouseID, fahuoinfo[3], AgentID,
                                    EnterFaHuoDate);

                                sqlcmd3.ExecuteNonQuery();


                                oknum++; 
                            } //发货                          
                        }

                        if (oknum > 0)
                        {
                            FHRecordinfo += fahuoinfo[0] + "," + fahuoinfo[1] + "," + fahuoinfo[2] + "," + fahuoinfo[3] +
                                            "," + fahuoinfo[4] + "," +
                                            (temptablenamestring.StartsWith(",")
                                                ? temptablenamestring.Substring(1)
                                                : temptablenamestring) + "," + oknum + "*";
                            string[] FaHuoRecordArray = FHRecordinfo.Split('*');
                            foreach (string line in FaHuoRecordArray)
                            {
                                if (!line.Trim().Equals(""))
                                {
                                    string[] temparray = line.Split(',');
                                    if (EnterFaHuoDate)
                                    {
                                        RecordFaHuoInfo(temparray[5].Trim(), temparray[0].Trim(), TextBox_FaHuoTime.Text,
                                            temparray[2].Trim(), sqlcon1, temparray[3].Trim(),
                                            Convert.ToInt32(temparray[6].Trim()), temparray[1].Trim(),
                                            temparray[4].Trim());
                                    }
                                    else
                                    {
                                        RecordFaHuoInfo(temparray[5].Trim(), temparray[0].Trim(),
                                            DateTime.Now.ToString(), temparray[2].Trim(), sqlcon1, temparray[3].Trim(),
                                            Convert.ToInt32(temparray[6].Trim()), temparray[1].Trim(),
                                            temparray[4].Trim());
                                    }
                                }
                            }
                        }

                        if (RadioButtonList_Mode.SelectedValue.Equals("3"))
                        {
                            //if (ExceptionString.Trim().Equals(""))
                            //{
                            ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('退货完成!')", true);
                            //Label2.Text = "";
                            Session.Remove("dttempline");
                            Session.Remove("strList");
                            Session.Remove("GoodTH");
                            //}
                            //else
                            //{
                            //    Label2.Text = "本次退货过程中发生异常!实际退货数量为:" + thnum.ToString() + "件,异常:" + (datarows.Length - thnum).ToString() + "件,请仔细排查!";
                            //    ScriptManager.RegisterStartupScript(updatepanel, this.GetType(), "info", "alert('本次退货过程中发生异常!实际退货数量为:" + thnum.ToString() + "件,上传文件中有如下号码存在异常:" + ExceptionString.Trim() + ",请仔细排查!');", true);
                            //    Label_Exceptions.Visible = true;
                            //    TextBox_ExceptionLines.Text = ExceptionString.Trim();
                            //    TextBox_ExceptionLines.Visible = true;
                            //    Session.Remove("dttempline");
                            //    Session.Remove("strList");
                            //}
                        }
                        else
                        {
                            //if (ExceptionString.Trim().Equals(""))
                            //{
                            ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('发货完毕!')", true);
                            //Label2.Text = "";
                            Session.Remove("dttempline");
                            Session.Remove("strList");
                            Session.Remove("GoodFH");

                            Label1.Text = (DateTime.Now - star).TotalSeconds.ToString();

                            //}
                            //else
                            //{
                            //    Label2.Text = "本次发货过程中发生异常!实际发货数量为:" + boxnum.ToString() + "件,异常:" + (datarows.Length - boxnum).ToString() + "件,请仔细排查!";
                            //    ScriptManager.RegisterStartupScript(updatepanel, this.GetType(), "info", "alert('本次发货过程中发生异常!实际发货数量为:" + boxnum.ToString() + "件,上传文件中有如下号码存在异常:" + ExceptionString.Trim() + ",请仔细排查!');", true);
                            //    Label_Exceptions.Visible = true;
                            //    TextBox_ExceptionLines.Text = ExceptionString.Trim();
                            //    TextBox_ExceptionLines.Visible = true;
                            //    Session.Remove("dttempline");
                            //    Session.Remove("strList");
                            //}
                        }

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
                    }
                }
            }
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
    /// 返回使用号码段发货时的SQL语句
    /// </summary>
    /// <param name="tablename"></param>
    /// <param name="StartCode"></param>
    /// <param name="EndCode"></param>
    /// <param name="FaHuoPiCi"></param>
    /// <param name="FHKey"></param>
    /// <returns></returns>
    private string GetFaHuoSqlStringByCodeSpan(string tablename, string StartCode, string EndCode, string FaHuoPiCi,
        string FHKey)
    {
        if (Checkbox_Enforced.Checked && Checkbox_Enforced.Visible)
        {
            return "update " + tablename + " set IsFaHuo='已发货',PinMing=" + ComboBox_ProInfo.SelectedValue.Trim() +
                   ",MuDiDi='" + ComboBox_DaiLiShangID.SelectedItem.Text.Trim() + "',FaHuoCang=" +
                   ComboBox_StoreHouseID.SelectedValue.Trim() + ",FaHuoShiJian='" +
                   (CheckBox_EnterFaHuoDate.Checked
                       ? Convert.ToDateTime(TextBox_FaHuoTime.Text).ToString("yyyy-MM-dd HH:mm")
                       : DateTime.Now.ToString("yyyy-MM-dd HH:mm")) + "',FaHuoPiCi='" + FaHuoPiCi + "',FHKey='" + FHKey +
                   "',AgentID=" + ComboBox_DaiLiShangID.SelectedValue.Trim() + " where (BoxLabel01>='" +
                   ReturnBoxLabelCode(StartCode, tablename) + "' and BoxLabel01<='" +
                   ReturnBoxLabelCode(EndCode, tablename) + "') and BottleLabelNum=" +
                   DropDownList_BiLiGuanXi.SelectedValue;
        }
        else
        {
            return "update " + tablename + " set IsFaHuo='已发货',PinMing=" + ComboBox_ProInfo.SelectedValue.Trim() +
                   ",MuDiDi='" + ComboBox_DaiLiShangID.SelectedItem.Text.Trim() + "',FaHuoCang=" +
                   ComboBox_StoreHouseID.SelectedValue.Trim() + ",FaHuoShiJian='" +
                   (CheckBox_EnterFaHuoDate.Checked
                       ? Convert.ToDateTime(TextBox_FaHuoTime.Text).ToString("yyyy-MM-dd HH:mm")
                       : DateTime.Now.ToString("yyyy-MM-dd HH:mm")) + "',FaHuoPiCi='" + FaHuoPiCi + "',FHKey='" + FHKey +
                   "',AgentID=" + ComboBox_DaiLiShangID.SelectedValue.Trim() + " where (BoxLabel01>='" +
                   ReturnBoxLabelCode(StartCode, tablename) + "' and BoxLabel01<='" +
                   ReturnBoxLabelCode(EndCode, tablename) +
                   "') and (IsFaHuo is null or IsFaHuo<>'已发货') and BottleLabelNum=" +
                   DropDownList_BiLiGuanXi.SelectedValue;
        }
    }

    private string GetFaHuoSqlStringForReplace(string boxstring, string tablename, string MuDiDi, string ProductInfoID,
        string FaHuoPiCi, string StoreHouseID, string FHKey, string AgengtID, bool InputDateTime)
    {
        string BoxLabel01 = com.ReturnFiledValue(tablename + "_BX", "BoxLabel", boxstring, "BoxLabel01");
        string stringtemp = string.Empty;
        if (InputDateTime)
        {
            stringtemp = "update " + tablename + " set IsFaHuo='已发货',FaHuoShiJian='" +
                         Convert.ToDateTime(TextBox_FaHuoTime.Text.Trim()) + "',MuDiDi='" + MuDiDi + "',PinMing=" +
                         ProductInfoID + ",FaHuoPiCi='" + FaHuoPiCi + "',FaHuoCang=" + StoreHouseID + ",FHKey='" + FHKey +
                         "',AgentID=" + AgengtID + " where (IsFaHuo<>'已发货' or IsFaHuo is null) and BoxLabel01='" +
                         BoxLabel01 + "'";
        }
        else
        {
            stringtemp = "update " + tablename + " set IsFaHuo='已发货',FaHuoShiJian='" +
                         DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "',MuDiDi='" + MuDiDi + "',PinMing=" +
                         ProductInfoID + ",FaHuoPiCi='" + FaHuoPiCi + "',FaHuoCang=" + StoreHouseID + ",FHKey='" + FHKey +
                         "',AgentID=" + AgengtID + " where (IsFaHuo<>'已发货' or IsFaHuo is null) and BoxLabel01='" +
                         BoxLabel01 + "'";
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
    private void RecordFaHuoInfo(string tablename, string FHPiCi, string time, string ProID, SqlConnection sqlconpara,
        string FHKey, int XiangNumber, string AgentID, string STID)
    { 
        string sqlforinsertrukuinfo = "";
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.Connection = sqlconpara;
        //opensqlconn(sqlconpara);
        //sqlfordeletestring = "delete from TB_FaHuoInfo_" + hf_compid.Value + " where FHTypeID=3 and AgentID=" + AgentID + " and STID=" + STID + " and ProID=" + ProID + " and  FHPiCi ='" + FHPiCi + "' and TableNameInfo='" + tablename + "'";
        //sqlcmd.CommandText = sqlfordeletestring;
        //opensqlconn(sqlconpara);
        //sqlcmd.ExecuteNonQuery();
        sqlforinsertrukuinfo = "insert into TB_FaHuoInfo_" + hf_compid.Value +
                               "(FHPiCi,FHTypeID,FHDate,AgentID,FHUserID,TableNameInfo,FHKey,ProID,XiangNumber,CompID,STID) values('" +
                               FHPiCi + "',3,'" + time + "'," + AgentID + "," + GetCookieUID() + ",'" + tablename +
                               "','" + FHKey + "'," + ProID + "," + XiangNumber + "," + hf_compid.Value + "," + STID +
                               ")";
        sqlcmd.CommandText = sqlforinsertrukuinfo;
        opensqlconn(sqlconpara);
        sqlcmd.ExecuteNonQuery();
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


    /// <summary>
    ///  退货处理
    /// </summary>
    /// <param name="boxstring"></param>
    /// <param name="tablename"></param>
    /// <param name="FaHuoPiCi"></param>
    /// <param name="FHKey"></param>
    /// <returns></returns>
    private string GetTuiHuoString(string boxstring, string tablename, string FaHuoPiCi, string FHKey)
    {
        string stringtemp = string.Empty;

        //if (InputDateTime)
        //{
        stringtemp = "update  TB_FaHuoInfo_" + hf_compid.Value + " set XiangNumber=XiangNumber-1  where FHPiCi='" +
                     FaHuoPiCi + "' and FHKey='" + FHKey + "' ; delete " + tablename + "_FH where BoxLabel01='" +
                     boxstring + "' and CompID=" + hf_compid.Value + "and left(FHType,1)=3; delete  TB_FaHuoInfo_" +
                     hf_compid.Value + " where XiangNumber=0 ";
        //}
        //else
        //{
        //    stringtemp = "update  " + tablename + " set IsFaHuo=1 where BoxLabel01='" + boxstring + "';update  " + tablename + " set IsFaHuo=0 where BoxLabel01='" + boxstring + "';insert into " + tablename + "_FH(BoxLabel01,FromAgentID,FromStorehouseID,ToAgentID,ToStoreHouseID,FHDate,FHType,UserID,FHPici,FHKey,CompID) values('" + boxstring + "'," + hf_compid.Value + "," + StoreHouseID + "," + AgengtID + ",0,'" + DateTime.Now.ToString() + "',3," + GetCookieUID() + ",'" + FaHuoPiCi + "','" + FHKey + "'," + hf_compid.Value + ")";
        //}

        return stringtemp;
    }


    /// <summary>
    /// 返回入库SQL
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
    private string GetFaHuoSqlString(string boxstring, string tablename, string MuDiDi, string ProductInfoID,
        string FaHuoPiCi, string StoreHouseID, string FHKey, string AgengtID, bool InputDateTime)
    {
        string stringtemp = string.Empty;
        //if (RadioButtonList_Mode.SelectedValue.Equals("1"))
        //{
        if (InputDateTime)
        {
            stringtemp = " insert into " + tablename +
                         "_FH(BoxLabel01,FromAgentID,FromStorehouseID,ToAgentID,ToStoreHouseID,FHDate,FHType,UserID,FHPici,FHKey,CompID) values('" +
                         boxstring + "'," + hf_compid.Value + "," + StoreHouseID + "," + AgengtID + ",0,'" +
                         TextBox_FaHuoTime.Text + "',3," + GetCookieUID() + ",'" + FaHuoPiCi + "','" + FHKey + "'," +
                         hf_compid.Value + ")";
        }
        else
        {
            stringtemp = " insert into " + tablename +
                         "_FH(BoxLabel01,FromAgentID,FromStorehouseID,ToAgentID,ToStoreHouseID,FHDate,FHType,UserID,FHPici,FHKey,CompID) values('" +
                         boxstring + "'," + hf_compid.Value + "," + StoreHouseID + "," + AgengtID + ",0,'" +
                         DateTime.Now + "',3," + GetCookieUID() + ",'" + FaHuoPiCi + "','" + FHKey + "'," +
                         hf_compid.Value + ")";
        }
        //}
        //else
        //{
        //    if (RadioButtonList_Mode.SelectedValue.Equals("3"))
        //    {
        //        stringtemp = "update " + tablename + " set IsFaHuo='已入库',MuDiDi='',PinMing=" + ProductInfoID + ",FaHuoCang=" + StoreHouseID + ",FHKey='" + FHKey + "',AgentID=" + AgengtID + " where (IsFaHuo<>'已入库' or IsFaHuo is null) and BoxLabel01='" + boxstring + "'";
        //    }
        //}
        return stringtemp;
    }

    private string GetTuiHuoSqlStringByCodeSpan(string tablename, string StartCode, string EndCode, string PinMing,
        string DaiLiShangID, DateTime FaHuoShiJian)
    {
        return "update " + tablename + " set IsFaHuo='已入库' where PinMing=" + PinMing + " and (FaHuoShiJian between '" +
               Convert.ToDateTime(TextBox_FaHuoTime.Text).ToString("yyyy-MM-dd 00:00") + "' and '" +
               Convert.ToDateTime(TextBox_FaHuoTime.Text).ToString("yyyy-MM-dd 23:59") + "') and AgentID=" +
               DaiLiShangID + " and (BoxLabel01>='" + ReturnBoxLabelCode(StartCode, tablename) + "' and BoxLabel01<='" +
               ReturnBoxLabelCode(EndCode, tablename) + "') and (IsFaHuo='已发货')";
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

    /// <summary>
    /// 检测是否由父级发货
    /// </summary>
    /// <param name="tablename"></param>
    /// <param name="BoxLabelString"></param>
    /// <param name="FromAgentID"></param>
    /// <returns></returns>
    public bool CheckIsPrF(string tablename, string BoxLabelString, int FromAgentID)
    {
        string sqlstring = "select count(BoxLabel01) as num from " + tablename + "_FH where BoxLabel01='" +
                           BoxLabelString + "'and left(FHType,1)=3 and FromAgentID=" + FromAgentID + " and ToAgentID=" +
                           hf_compid.Value + "";
        sqlcmdforcheck.CommandText = sqlstring;
        sqlcmdforcheck.Connection = sqlconcom;
        opensqlconn(sqlconcom);
        SqlDataReader sdr = sqlcmdforcheck.ExecuteReader();
        if (sdr.Read())
        {
            string tempvalue = sdr["num"].ToString().Trim();

            sdr.Close();
            if (Convert.ToInt32(tempvalue) > 0)
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
            sdr.Close();
            return false;
        }
    }


    /// <summary>
    /// 检测是否发货
    /// </summary>
    /// <param name="tablename"></param>
    /// <param name="BoxLabelString"></param>
    /// <returns></returns>
    public bool CheckWfhISok(string tablename, string BoxLabelString)
    {
        string sqlstring = "select count(BoxLabel01) as num from " + tablename + "_FH where BoxLabel01='" +
                           BoxLabelString + "'and left(FHType,1)=3 and CompID=" + hf_compid.Value + "";
        sqlcmdforcheck.CommandText = sqlstring;
        sqlcmdforcheck.Connection = sqlconcom;
        opensqlconn(sqlconcom);
        SqlDataReader sdr = sqlcmdforcheck.ExecuteReader();
        if (sdr.Read())
        {
            string tempvalue = sdr["num"].ToString().Trim();

            sdr.Close();
            if (Convert.ToInt32(tempvalue) > 0)
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
            sdr.Close();
            return false;
        }
    }


    private string _sqlstringforcheck = "";
    private string sqlstringforcheckforreplace = "";
    private string tempvalueforcheck = "";

    private bool CheckBoxLabelCodeExist(string tablename, string BoxLabelString)
    {
        _sqlstringforcheck = "select count(BoxLabel01) as num from " + tablename + " where BoxLabel01='" + BoxLabelString +
                            "'";
        sqlstringforcheckforreplace = "select count(*) as num from " + tablename + "_BX where BoxLabel='" +
                                      BoxLabelString + "'";
        sqlcmdforcheck.CommandText = _sqlstringforcheck;
        sqlcmdforcheck.Connection = sqlconcom;
        opensqlconn(sqlconcom);
        SqlDataReader sdr = sqlcmdforcheck.ExecuteReader();
        if (sdr.Read())
        {
            tempvalueforcheck = sdr["num"].ToString().Trim();
            sdr.Close();
            if (Convert.ToInt32(tempvalueforcheck) > 0)
            {
                LabelIsNull = true;

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
                        LabelIsNull = true;
                        return true;
                    }
                    else
                    {
                        LabelIsNull = false;
                        return false;
                    }
                }
                else
                {
                    LabelIsNull = false;
                    return false;
                }
            }
        }
        else
        {
            sdr.Close();
            LabelIsNull = false;
            return false;
        }
    }


    private bool CheckBoxLabelCodeNew(string tablename, string BoxLabelString, string compid)
    {
        //sqlstringforcheck = "select count(*) as num from " + tablename + " where BoxLabel01='" + BoxLabelString + "'";
        _sqlstringforcheck = "select COUNT(*) as num from " + tablename + " where BoxLabel01='" + BoxLabelString +
                            "' and not EXISTS(select BoxLabel01 from " + tablename + "_FH where BoxLabel01='" +
                            BoxLabelString + "' and FHType='3' and CompID=" + compid + ")";
        //sqlstringforcheckforreplace = "select count(*) as num from " + tablename + "_BX where BoxLabel='" + BoxLabelString + "'";
        sqlstringforcheckforreplace = "select COUNT(*) as num from " + tablename + "_BX where BoxLabel01='" +
                                      BoxLabelString + "' and not EXISTS(select BoxLabel01 from " + tablename +
                                      "_FH where BoxLabel01='" + BoxLabelString + "' and FHType='3' and CompID=" +
                                      compid + ")";
        sqlcmdforcheck.CommandText = _sqlstringforcheck;
        sqlcmdforcheck.Connection = sqlconcom;
        opensqlconn(sqlconcom);
        SqlDataReader sdr = sqlcmdforcheck.ExecuteReader();
        if (sdr.Read())
        {
            tempvalueforcheck = sdr["num"].ToString().Trim();
            sdr.Close();
            if (int.Parse(tempvalueforcheck) > 0)
            {
                LabelIsNull = true;

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
                        LabelIsNull = true;
                        return true;
                    }
                    else
                    {
                        LabelIsNull = false;
                        return false;
                    }
                }
                else
                {
                    LabelIsNull = false;
                    return false;
                }
            }
        }
        else
        {
            sdr.Close();
            LabelIsNull = false;
            return false;
        }
    } 
    protected void Button_upload_Click(object sender, EventArgs e)
    {
        //    Label_Exceptions.Visible = false;
        //    TextBox_ExceptionLines.Text = "";
        //    TextBox_ExceptionLines.Visible = false;
        //    //if (FileUpload1.HasFile)
        //    //{
        //    //    try
        //    //    {
        //    //        System.IO.FileInfo file = new System.IO.FileInfo(FileUpload1.PostedFile.FileName);
        //    //        if (file.Extension.ToLower().Equals(".txt"))
        //    //        {
        //    //            // 保存文件
        //    //            uploadfilepath = Server.MapPath(@"/Admin/wuliu/Fahuo/files/") + DateTime.Now.ToString("yyyyMMddhhmmssss") + ".txt";

        //    //            FileUpload1.SaveAs(uploadfilepath);
        //    //            ScriptManager.RegisterStartupScript(updatepanel, this.GetType(), "info", "alert('上传成功!');", true);
        //    //            Label1.Text = "已上传文件:" + FileUpload1.FileName;
        //    //           // hf_file.Value = uploadfilepath;
        // Button_DataCheck.Enabled = true;
        //Button_DataCheck_Click(sender, e);
        //    //        }
        //    //        else
        //    //        {
        //    //            ScriptManager.RegisterStartupScript(updatepanel, this.GetType(), "info", "alert('请上传格式为(.txt)的盘点机数据库文件');", true);
        //    //        }
        //    //    }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterStartupScript(updatepanel, this.GetType(), "info", "alert('" + ex.Message + "');", true);
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(up datepanel, this.GetType(), "info", "alert('请选取数据文件!');", true);
        //    }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["dttempline"] = null;
        Session.Remove("dttempline");
        Session.Remove("strList");
        ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info",
            "alert('清除成功！');window.location.href='FahuoNewPeng.aspx'", true);
    }
}