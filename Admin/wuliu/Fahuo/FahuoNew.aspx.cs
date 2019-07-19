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

public partial class Admin_wuliu_Fahuo_FahuoNew : AuthorPage
{
    BTJ_RegisterCompanys bagentinfo = new BTJ_RegisterCompanys();
    BTB_Products_Infor bproductinfo = new BTB_Products_Infor();
    BTB_StoreHouse bstorehouse = new BTB_StoreHouse();
    BTB_CompAgentInfo bbagent = new BTB_CompAgentInfo();
    SqlCommand sqlcmd = new SqlCommand();
    BTB_CompAgentInfo bcoma = new BTB_CompAgentInfo();
    SqlCommand sqlcmdforcheck = new SqlCommand();
    private string[] tablenames = new string[0];
    private int repnum = 0;
    private int boxnum = 0;
    private int thnum = 0;
    bool LabelIsNull = false;
    CommonFunWL com = new CommonFunWL();
    commwl comm = new commwl();
    DBClass db = new DBClass();
    //string tempmaxminvalue = "";
    SqlConnection sqlconcom = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());
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
            IList<MTB_CompAgentInfo> mcom = bcoma.GetListsByFilterString("compid='130' and agentid=" + GetCookieCompID());
            if (mcom.Count > 0)
            {
                if (GetCookieCompID() == "0")
                {
                    TextBox_FaHuoTime.Enabled = true;
                    CheckBox_EnterFaHuoDate.Visible = true;
                }
                else
                {
                    TextBox_FaHuoTime.Enabled = false;
                    CheckBox_EnterFaHuoDate.Visible = false;
                }
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
        if (GetCookieCompTypeID() == DAConfig.CompTypeIDChangJia.ToString().Trim() || GetCookieCompID() == "1" || GetCookieCompID() == "2")
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

            if (Session["dttempline"] != null)
            {
                dttempline = (DataTable)Session["dttempline"];
                HFMaxMinValue.Value = Session["strList"].ToString();

                repeatstring = checkcodevalid(dttempline);
                if (!repeatstring.Equals(string.Empty))
                {
                    if (!repeatstring.Trim().Equals("号码不全是数字") && !repeatstring.Trim().Equals("号码长度有误") && !repeatstring.Trim().Equals("入库数据格式不正确"))
                    {
                        Label2.Text = "通过检查,上传数据中有" + Convert.ToString(dttempline.Rows.Count - repnum) + "组有效数据," + repnum + "组重复数据";
                        ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('通过检查," + repeatstring.Substring(0, repeatstring.Length - 2) + "中标签号码重复!');", true);
                        Button_DataCheck.Enabled = false;
                        Button_Insert.Enabled = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('请重新上传ok!');", true);
                    }
                }
                else
                {
                    Label2.Text = "通过检查,上传数据中有" + Convert.ToString(dttempline.Rows.Count) + "组有效数据,无组重复数据";
                    ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('数据检查完毕!');", true);
                    Button_DataCheck.Enabled = false;
                    Button_Insert.Enabled = true;
                }



            }

            else
            {
                ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('请重新上传数据kong!');", true);
            }

            //if (oFile != null)
            //{
            //    //string currentworkdir = Server.MapPath("/");
            //    string topDir = Context.Request["folder"];
            //    //创建顶级目录  
            //    if (!Directory.Exists(HttpContext.Current.Server.MapPath(topDir)))
            //    {
            //        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(topDir));
            //    }

            //    //当天上传的文件放到已当天日期命名的文件夹中  
            //    string dateFolder = HttpContext.Current.Server.MapPath(topDir) + "//files";

            //    if (!Directory.Exists(dateFolder))
            //    {
            //        Directory.CreateDirectory(dateFolder);
            //    }

            //    System.IO.StreamReader sr = new System.IO.StreamReader(dateFolder + "//" + oFile.FileName);
            //    string[] stringSeparators = new string[] { "\r\n" };
            //    codearrytemp = null;
            //    codearrytemp = sr.ReadToEnd().Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            //    if (!string.IsNullOrEmpty(oFile.FileName))
            //    {

            //            //    foreach (string line in codearrytemp)
            //            //    {
            //            //        dttempline.Rows.Add(line.Split(','));
            //            //    }

            //            //    Session.Add("dttempline", dttempline);
            //            //}
            //            //else
            //            //{
            //            //    dttempline.Columns.Add("FHPC");
            //            //    dttempline.Columns.Add("AgentCode");
            //            //    dttempline.Columns.Add("PRCode");
            //            //    dttempline.Columns.Add("LBCode");
            //            //    dttempline.Columns.Add("STCode");
            //            //    foreach (string line in codearrytemp)
            //            //    {
            //            //        dttempline.Rows.Add(line.Split(','));
            //            //    }

            //            //    Session.Add("dttempline", dttempline);
            //            //}
            //            sr.Close();
            //        //  oFile.SaveAs(dateFolder + "//" + oFile.FileName);
            //    }


            //    HFMaxMinValue.Value = com.MaxAndMinValue(codearrytemp, 3);
            //    int S = dttempline.Rows.Count;
            //    repeatstring = checkcodevalid(dttempline);
            //    if (!repeatstring.Equals(string.Empty))
            //    {
            //        if (!repeatstring.Trim().Equals("号码不全是数字") && !repeatstring.Trim().Equals("号码长度有误") && !repeatstring.Trim().Equals("入库数据格式不正确"))
            //        {
            //            Label2.Text = "通过检查,上传数据中有" + Convert.ToString(dttempline.Rows.Count - repnum) + "组有效数据," + repnum.ToString() + "组重复数据";
            //            ScriptManager.RegisterStartupScript(updatepanel, this.GetType(), "info", "alert('通过检查," + repeatstring.Substring(0, repeatstring.Length - 2) + "中标签号码重复!');", true);
            //            Button_DataCheck.Enabled = false;
            //            Button_Insert.Enabled = true;
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(updatepanel, this.GetType(), "info", "alert('请重新上传!');", true);
            //        }
            //    }
            //    else
            //    {
            //        Label2.Text = "通过检查,上传数据中有" + Convert.ToString(dttempline.Rows.Count) + "组有效数据,无组重复数据";
            //        ScriptManager.RegisterStartupScript(updatepanel, this.GetType(), "info", "alert('数据检查完毕!');", true);
            //        Button_DataCheck.Enabled = false;
            //        Button_Insert.Enabled = true;
            //    }
            //    // sr.Close();
            //    //comm comm = new comm();
            //    //comm.clearfile(hf_file.Value.Trim(), false);
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(updatepanel, this.GetType(), "info", "alert('请重新上传数据!');", true);
            //}
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

    private string AgentName = "";
    private string ProductInfoID = "";
    private string StoreHouseID = "";
    private string AgentID = "";
    protected void Button_Insert_Click(object sender, EventArgs e)
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
            bool isok = false;
            bool wlflag = true;
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
                    if (string.IsNullOrEmpty(TextBox_ChuKuDanHao.Value.Trim()))
                    {
                        fahuoinfo[0] = DateTime.Now.ToString("yyyyMMddHHmmss"); //TextBox_ChuKuDanHao.Value.Trim();//发货批次
                    }
                    else
                    {
                        fahuoinfo[0] = TextBox_ChuKuDanHao.Value.Trim();
                    }
                    fahuoinfo[1] = AgentID;//代理商Id
                    fahuoinfo[2] = ProductInfoID;//产品Id
                    fahuoinfo[3] = Guid.NewGuid().ToString();//发货Key
                    fahuoinfo[4] = StoreHouseID;//库房ID
                    //fahuoinfo[5] = "";//涉及表名
                    bool EnterFaHuoDate = false;
                    if (CheckBox_EnterFaHuoDate.Checked)
                    {
                        EnterFaHuoDate = true;
                    }
                    //SqlConnection sqlcon = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());
                    SqlConnection sqlcon1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());
                    SqlConnection sqlcon2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());
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
                    DataRow[] datarows = ((DataTable)Session["dttempline"]).Select("1=1", "AgentCode,PRCode,STCode");
                    if (datarows.Length > 0)
                    {
                        boxnum = datarows.Length;
                        thnum = datarows.Length;
                        string tablenameinfo = string.Empty;
                        tablenames = comm.gettableinfo(HFMaxMinValue.Value.Split(',')[0].Trim(), HFMaxMinValue.Value.Split(',')[1].Trim(), sqlcon1).Split(',');
                        //tablenames = new string[] { "W8060_00", "W8060_01", "W8060_02", "W8060_03", "W8062_00", "W8062_01", "W8062_02", "W8062_03", "W8063_00", "W8063_01", "W8063_02", "W8063_03", "W8061_00", "W8061_01", "W8061_02", "W8061_03" };
                        if (tablenames.Length > 1)
                        {
                            foreach (DataRow dr in datarows)
                            {
                                if (!CheckBox_SelectAgent.Checked)
                                {
                                    if (!AgentCode.Equals(dr[1].ToString().Trim()) || AgentName.Trim().Equals(""))
                                    {
                                        if (oknum > 0 && AgentCode.Trim().Length > 0)
                                        {
                                            FHRecordinfo += fahuoinfo[0] + "|" + fahuoinfo[1] + "|" + fahuoinfo[2] + "|" + fahuoinfo[3] + "|" + fahuoinfo[4] + "|" + (temptablenamestring.StartsWith(",") ? temptablenamestring.Substring(1) : temptablenamestring )+ "|" + oknum + "*";
                                            oknum = 0;
                                            temptablenamestring = "";
                                            fahuoinfo[3] = Guid.NewGuid().ToString();
                                        }
                                        //原有的查询方式
                                        //alist = bagentinfo.GetListsByFilterString("Agent_Code='" + dr[1].ToString().Trim() + "' and CompID=" + GetCookieCompID());
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
                                            FHRecordinfo += fahuoinfo[0] + "|" + fahuoinfo[1] + "|" + fahuoinfo[2] + "|" + fahuoinfo[3] + "|" + fahuoinfo[4] + "|" + (temptablenamestring.StartsWith(",") ? temptablenamestring.Substring(1) : temptablenamestring) + "|" + oknum + "*";
                                            temptablenamestring = "";
                                            oknum = 0;
                                            fahuoinfo[3] = Guid.NewGuid().ToString();
                                        }
                                        //plist = BLPI.GetListsByFilterString("Product_Code='" + dr[2].ToString() + "' and CompID="+GetCookieCompID());
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
                                            FHRecordinfo += fahuoinfo[0] + "|" + fahuoinfo[1] + "|" + fahuoinfo[2] + "|" + fahuoinfo[3] + "|" + fahuoinfo[4] + "|" + (temptablenamestring.StartsWith(",") ? temptablenamestring.Substring(1) : temptablenamestring) + "|" + oknum + "*";
                                            temptablenamestring = "";
                                            oknum = 0;
                                            fahuoinfo[3] = Guid.NewGuid().ToString();
                                        }
                                        //slist = BLSH.GetListsByFilterString("StoreHouseCode='" + dr[4].ToString() + "' and CompID=" + GetCookieCompID());原来的
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

                                if (RadioButtonList_Mode.SelectedValue.Equals("3"))
                                {
                                    foreach (string tbname in tablenames)
                                    {
                                        if (tbname != null && tbname != "")
                                        {
                                            if (!temptablenamestring.Contains("," + tbname))
                                            {
                                                temptablenamestring += "," + tbname;
                                            }
                                            DataTable dt = db.GetFhPiciandFhKey(tbname, dr[3].ToString().Trim(), GetCookieCompID());
                                            if (CheckBoxLabelCodeExist(tbname, dr[3].ToString().Trim()) && dt.Rows.Count > 0)
                                            {
                                                //DataTable dt = db.GetFhPiciandFhKey(tbname, dr[3].ToString().Trim(), GetCookieCompID());
                                                string pici = dt.Rows[0]["FHPici"].ToString();
                                                string key = dt.Rows[0]["FHKey"].ToString();
                                                sqlcmd3.CommandText = GetTuiHuoString(dr[3].ToString().Trim(), tbname, pici, key);
                                                sqlcmd3.ExecuteNonQuery();
                                                isok = true;
                                                break;

                                            }
                                            else
                                            {
                                                if (LabelIsNull)
                                                {
                                                    isok = false;
                                                    break;
                                                }
                                                else
                                                {
                                                    isok = false;
                                                }

                                                //ExceptionString += "\r\n" + dr[3].ToString().Trim() + (ReturnExceptionCause(tablenames, dr[3].ToString().Trim()) ? "号码未发货" : "箱标号码不存在");
                                                //thnum--;
                                                //isok = false;
                                                //break;

                                            }
                                            //if (!isok)
                                            //{
                                            //    ExceptionString += "\r\n" + dr[3].ToString().Trim() + (ReturnExceptionCause(tablenames, dr[3].ToString().Trim()) ? "号码未发货" : "箱标号码不存在");
                                            //    thnum--;  

                                            //}
                                        }
                                    }
                                    if (!isok)
                                    {
                                        ExceptionString += "\r\n" + dr[3].ToString().Trim() + (LabelIsNull ? "号码未发货" : "箱标号码不存在");
                                        thnum--;

                                    }

                                }
                                else
                                {


                                    foreach (string tbname in tablenames)
                                    {
                                        if (tbname != null && tbname != "")
                                        {
                                            if (!temptablenamestring.Contains("," + tbname))
                                            {
                                                temptablenamestring += "," + tbname;
                                            }
                                            if (CheckBoxLabelCodeExist(tbname, dr[3].ToString().Trim()) && !CheckWfhISok(tbname, dr[3].ToString().Trim()))
                                            {
                                                IList<MTB_CompAgentInfo> mAgent = bbagent.GetListsByFilterString("AgentID=" + GetCookieCompID());
                                                if (mAgent.Count > 0)
                                                {
                                                    int coid;
                                                    foreach (MTB_CompAgentInfo ma in mAgent)
                                                    {
                                                        coid = ma.CompID;
                                                        if (CheckIsPrF(tbname, dr[3].ToString().Trim(), coid))
                                                        {
                                                            sqlcmd3.CommandText = GetFaHuoSqlString(dr[3].ToString().Trim(), tbname, AgentName, ProductInfoID, FaHuoPiCi, StoreHouseID, fahuoinfo[3], AgentID, EnterFaHuoDate);
                                                            // sqlcmd3.CommandText = GetFaHuoSqlString(dr[3].ToString().Trim(), tbname, AgentName, ProductInfoID, FaHuoPiCi, StoreHouseID, fahuoinfo[3], AgentID, EnterFaHuoDate);
                                                            // Response.Write("<script>alert('"+ sqlcmd3.CommandText + "')</script>");
                                                            sqlcmd3.ExecuteNonQuery();
                                                            oknum++;
                                                            isok = true;
                                                            wlflag = true;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            wlflag = false;
                                                            isok = true;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                }
                                                else
                                                {
                                                    sqlcmd2.CommandText = GetFaHuoSqlString(dr[3].ToString().Trim(), tbname, AgentName, ProductInfoID, FaHuoPiCi, StoreHouseID, fahuoinfo[3], AgentID, EnterFaHuoDate);
                                                    sqlcmd2.ExecuteNonQuery();
                                                    oknum++;
                                                    isok = true;
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                //在表里找到序号直接跳出
                                                if (LabelIsNull)
                                                {
                                                    isok = false;
                                                    break;
                                                }
                                                else
                                                {
                                                    isok = false;
                                                }
                                                //ExceptionString += "\r\n" + dr[3].ToString().Trim() + (ReturnExceptionCause(tablenames, dr[3].ToString().Trim()) ? "已经发过货" : "箱标号码不存在");
                                                //--;
                                                //break;
                                                //isok = false;

                                            }
                                        }
                                    }

                                    //序号不存在或者已经发过货
                                    if (isok == false)
                                    {
                                        ExceptionString += "\r\n" + dr[3].ToString().Trim() + (LabelIsNull ? "号码已发货" : "箱标号码不存在");
                                        boxnum--;
                                    }
                                    else
                                    {
                                        //下级代理商发货时，序号不是父级发货下来的
                                        if (!wlflag)
                                        {
                                            ExceptionString += "\r\n" + dr[3].ToString().Trim() + "号码不属于正常物流数据";
                                            boxnum--;
                                        }
                                    }
                                }

                                //if (!isok)
                                //{
                                //    ExceptionString += "\r\n" + dr[3].ToString().Trim() + (ReturnExceptionCause(tablenames, dr[3].ToString().Trim()) ? "  已发货" : "  箱标号码不存在");
                                //    boxnum--;
                                //}
                                //isok = false;
                            }
                        }

                        else
                        {
                            ExceptionString += "号码不存在，请先入库！！！";
                            boxnum = 0;
                            thnum = 0;

                        }
                        //if(FHRecordinfo.Length==0)
                        //{
                        //    FHRecordinfo += fahuoinfo[0] + "," + fahuoinfo[1] + "," + fahuoinfo[2] + "," + fahuoinfo[3] + "," + fahuoinfo[4] + "," + (temptablenamestring.StartsWith(",") ? temptablenamestring.Substring(1) : temptablenamestring) + "," + oknum + "*";
                        //}
                        if (oknum > 0)
                        {
                            FHRecordinfo += fahuoinfo[0] + "|" + fahuoinfo[1] + "|" + fahuoinfo[2] + "|" + fahuoinfo[3] + "|" + fahuoinfo[4] + "|" + (temptablenamestring.StartsWith(",") ? temptablenamestring.Substring(1) : temptablenamestring) + "|" + oknum + "*";
                            string[] FaHuoRecordArray = FHRecordinfo.Split('*');
                            foreach (string line in FaHuoRecordArray)
                            {
                                if (!line.Trim().Equals(""))
                                {
                                    string[] temparray = line.Split('|');
                                    string a = string.Empty;
                                    string b = string.Empty;
                                    string c = string.Empty;
                                    string d = string.Empty;
                                    string f = string.Empty;
                                    string g = string.Empty;
                                    string h = string.Empty;
                                    a = temparray[0].Trim();
                                    b = temparray[1].Trim();
                                    c = temparray[2].Trim();
                                    d = temparray[3].Trim();
                                    f = temparray[4].Trim();
                                    g = temparray[5].Trim();
                                    h = temparray[6].Trim();
                                    if (EnterFaHuoDate)
                                    {
                                        RecordFaHuoInfo(temparray[5].Trim(), temparray[0].Trim(), TextBox_FaHuoTime.Text, temparray[2].Trim(), sqlcon1, temparray[3].Trim(), Convert.ToInt32(temparray[6].Trim()), temparray[1].Trim(), temparray[4].Trim());
                                    }
                                    else
                                    {
                                       
                                       
                                        RecordFaHuoInfo(temparray[5].Trim(), temparray[0].Trim(), DateTime.Now.ToString(), temparray[2].Trim(), sqlcon1, temparray[3].Trim(), Convert.ToInt32(temparray[6].Trim()), temparray[1].Trim(), temparray[4].Trim());
                                    }
                                }
                            }
                        }
                        ////FHTans.Commit();
                        // hf_file.Value = "";

                        if (RadioButtonList_Mode.SelectedValue.Equals("3"))
                        {

                            if (ExceptionString.Trim().Equals(""))
                            {
                                ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "divNone()", true);
                                Label2.Text = "";
                                Session.Remove("dttempline");
                                Session.Remove("strList");
                            }
                            else
                            {
                                Label2.Text = "本次退货过程中发生异常!实际退货数量为:" + thnum + "件,异常:" + (datarows.Length - thnum) + "件,请仔细排查!";
                                ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "divNone()", true);
                                Label_Exceptions.Visible = true;
                                TextBox_ExceptionLines.Text = ExceptionString.Trim();
                                TextBox_ExceptionLines.Visible = true;
                                Session.Remove("dttempline");
                                Session.Remove("strList");
                            }
                        }
                        else
                        {
                            if (ExceptionString.Trim().Equals(""))
                            {

                                Label2.Text = "";
                                Session.Remove("dttempline");
                                Session.Remove("strList");
                                ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "divNone()", true);
                            }
                            else
                            {
                                Label2.Text = "本次发货过程中发生异常!实际发货数量为:" + boxnum + "件,异常:" + (datarows.Length - boxnum) + "件,请仔细排查!";
                                ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "divNone()", true);
                                //ScriptManager.RegisterStartupScript(updatepanel, this.GetType(), "info", "alert('本次发货过程中发生异常!实际发货数量为:" + boxnum.ToString() + "件,上传文件中有如下号码存在异常:" + ExceptionString.Trim() + ",请仔细排查!');", true);
                                Label_Exceptions.Visible = true;
                                TextBox_ExceptionLines.Text = ExceptionString.Trim();
                                TextBox_ExceptionLines.Visible = true;
                                Session.Remove("dttempline");
                                Session.Remove("strList");
                            }


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
                        ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('本次发货过程中发生异常!请重新上传数据!');", true);
                        //FHTans.Rollback();
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
    private string GetFaHuoSqlStringByCodeSpan(string tablename, string StartCode, string EndCode, string FaHuoPiCi, string FHKey)
    {
        if (Checkbox_Enforced.Checked && Checkbox_Enforced.Visible)
        {
            return "update " + tablename + " set IsFaHuo='已发货',PinMing=" + ComboBox_ProInfo.SelectedValue.Trim() + ",MuDiDi='" + ComboBox_DaiLiShangID.SelectedItem.Text.Trim() + "',FaHuoCang=" + ComboBox_StoreHouseID.SelectedValue.Trim() + ",FaHuoShiJian='" + (CheckBox_EnterFaHuoDate.Checked ? Convert.ToDateTime(TextBox_FaHuoTime.Text).ToString("yyyy-MM-dd HH:mm") : DateTime.Now.ToString("yyyy-MM-dd HH:mm")) + "',FaHuoPiCi='" + FaHuoPiCi + "',FHKey='" + FHKey + "',AgentID=" + ComboBox_DaiLiShangID.SelectedValue.Trim() + " where (BoxLabel01>='" + ReturnBoxLabelCode(StartCode, tablename) + "' and BoxLabel01<='" + ReturnBoxLabelCode(EndCode, tablename) + "') and BottleLabelNum=" + DropDownList_BiLiGuanXi.SelectedValue;
        }
        else
        {
            return "update " + tablename + " set IsFaHuo='已发货',PinMing=" + ComboBox_ProInfo.SelectedValue.Trim() + ",MuDiDi='" + ComboBox_DaiLiShangID.SelectedItem.Text.Trim() + "',FaHuoCang=" + ComboBox_StoreHouseID.SelectedValue.Trim() + ",FaHuoShiJian='" + (CheckBox_EnterFaHuoDate.Checked ? Convert.ToDateTime(TextBox_FaHuoTime.Text).ToString("yyyy-MM-dd HH:mm") : DateTime.Now.ToString("yyyy-MM-dd HH:mm")) + "',FaHuoPiCi='" + FaHuoPiCi + "',FHKey='" + FHKey + "',AgentID=" + ComboBox_DaiLiShangID.SelectedValue.Trim() + " where (BoxLabel01>='" + ReturnBoxLabelCode(StartCode, tablename) + "' and BoxLabel01<='" + ReturnBoxLabelCode(EndCode, tablename) + "') and (IsFaHuo is null or IsFaHuo<>'已发货') and BottleLabelNum=" + DropDownList_BiLiGuanXi.SelectedValue;
        }
    }

    private string GetFaHuoSqlStringForReplace(string boxstring, string tablename, string MuDiDi, string ProductInfoID, string FaHuoPiCi, string StoreHouseID, string FHKey, string AgengtID, bool InputDateTime)
    {
        string BoxLabel01 = com.ReturnFiledValue(tablename + "_BX", "BoxLabel", boxstring, "BoxLabel01");
        string stringtemp = string.Empty;
        if (InputDateTime)
        {
            stringtemp = "update " + tablename + " set IsFaHuo='已发货',FaHuoShiJian='" + Convert.ToDateTime(TextBox_FaHuoTime.Text.Trim()) + "',MuDiDi='" + MuDiDi + "',PinMing=" + ProductInfoID + ",FaHuoPiCi='" + FaHuoPiCi + "',FaHuoCang=" + StoreHouseID + ",FHKey='" + FHKey + "',AgentID=" + AgengtID + " where (IsFaHuo<>'已发货' or IsFaHuo is null) and BoxLabel01='" + BoxLabel01 + "'";
        }
        else
        {
            stringtemp = "update " + tablename + " set IsFaHuo='已发货',FaHuoShiJian='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "',MuDiDi='" + MuDiDi + "',PinMing=" + ProductInfoID + ",FaHuoPiCi='" + FaHuoPiCi + "',FaHuoCang=" + StoreHouseID + ",FHKey='" + FHKey + "',AgentID=" + AgengtID + " where (IsFaHuo<>'已发货' or IsFaHuo is null) and BoxLabel01='" + BoxLabel01 + "'";
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
    private void RecordFaHuoInfo(string tablename, string FHPiCi, string time, string ProID, SqlConnection sqlconpara, string FHKey, int XiangNumber, string AgentID, string STID)
    { 
        string sqlforinsertrukuinfo = "";
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.Connection = sqlconpara;
        //opensqlconn(sqlconpara);
        //sqlfordeletestring = "delete from TB_FaHuoInfo_" + GetCookieCompID() + " where FHTypeID=3 and AgentID=" + AgentID + " and STID=" + STID + " and ProID=" + ProID + " and  FHPiCi ='" + FHPiCi + "' and TableNameInfo='" + tablename + "'";
        //sqlcmd.CommandText = sqlfordeletestring;
        //opensqlconn(sqlconpara);
        //sqlcmd.ExecuteNonQuery();
        sqlforinsertrukuinfo = "insert into TB_FaHuoInfo_" + GetCookieCompID() + "(FHPiCi,FHTypeID,FHDate,AgentID,FHUserID,TableNameInfo,FHKey,ProID,XiangNumber,CompID,STID) values('" + FHPiCi + "',3,'" + time + "'," + AgentID + "," + GetCookieUID() + ",'" + tablename + "','" + FHKey + "'," + ProID + "," + XiangNumber + "," + GetCookieCompID() + "," + STID + ")";
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
            sqlcmd.CommandText = "select W.BoxLabel01 From " + tablename + " as W," + tablename + "_BX as BX where (BX.BoxLabel='" + LabelCode + "' and BX.BoxLabel01=W.BoxLabel01)";
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
    private string[] returntablenamebyboxcodeforcodespan(string boxcodestringstart, string boxcodestringend, SqlConnection sqlconpara)
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
        stringtemp = "update  TB_FaHuoInfo_" + GetCookieCompID() + " set XiangNumber=XiangNumber-1  where FHPiCi='" + FaHuoPiCi + "' and FHKey='" + FHKey + "' ; delete " + tablename + "_FH where BoxLabel01='" + boxstring + "' and CompID=" + GetCookieCompID() + "and left(FHType,1)=3; delete  TB_FaHuoInfo_" + GetCookieCompID() + " where XiangNumber=0 ";
        //}
        //else
        //{
        //    stringtemp = "update  " + tablename + " set IsFaHuo=1 where BoxLabel01='" + boxstring + "';update  " + tablename + " set IsFaHuo=0 where BoxLabel01='" + boxstring + "';insert into " + tablename + "_FH(BoxLabel01,FromAgentID,FromStorehouseID,ToAgentID,ToStoreHouseID,FHDate,FHType,UserID,FHPici,FHKey,CompID) values('" + boxstring + "'," + GetCookieCompID() + "," + StoreHouseID + "," + AgengtID + ",0,'" + DateTime.Now.ToString() + "',3," + GetCookieUID() + ",'" + FaHuoPiCi + "','" + FHKey + "'," + GetCookieCompID() + ")";
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
    private string GetFaHuoSqlString(string boxstring, string tablename, string MuDiDi, string ProductInfoID, string FaHuoPiCi, string StoreHouseID, string FHKey, string AgengtID, bool InputDateTime)
    {
        string stringtemp = string.Empty;
        //if (RadioButtonList_Mode.SelectedValue.Equals("1"))
        //{
        if (InputDateTime)
        {
            stringtemp = " insert into " + tablename + "_FH(BoxLabel01,FromAgentID,FromStorehouseID,ToAgentID,ToStoreHouseID,FHDate,FHType,UserID,FHPici,FHKey,CompID) values('" + boxstring + "'," + GetCookieCompID() + "," + StoreHouseID + "," + AgengtID + ",0,'" + TextBox_FaHuoTime.Text + "',3," + GetCookieUID() + ",'" + FaHuoPiCi + "','" + FHKey + "'," + GetCookieCompID() + ")";
        }
        else
        {
            stringtemp = " insert into " + tablename + "_FH(BoxLabel01,FromAgentID,FromStorehouseID,ToAgentID,ToStoreHouseID,FHDate,FHType,UserID,FHPici,FHKey,CompID) values('" + boxstring + "'," + GetCookieCompID() + "," + StoreHouseID + "," + AgengtID + ",0,'" + DateTime.Now + "',3," + GetCookieUID() + ",'" + FaHuoPiCi + "','" + FHKey + "'," + GetCookieCompID() + ")";
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
    private string GetTuiHuoSqlStringByCodeSpan(string tablename, string StartCode, string EndCode, string PinMing, string DaiLiShangID, DateTime FaHuoShiJian)
    {
        return "update " + tablename + " set IsFaHuo='已入库' where PinMing=" + PinMing + " and (FaHuoShiJian between '" + Convert.ToDateTime(TextBox_FaHuoTime.Text).ToString("yyyy-MM-dd 00:00") + "' and '" + Convert.ToDateTime(TextBox_FaHuoTime.Text).ToString("yyyy-MM-dd 23:59") + "') and AgentID=" + DaiLiShangID + " and (BoxLabel01>='" + ReturnBoxLabelCode(StartCode, tablename) + "' and BoxLabel01<='" + ReturnBoxLabelCode(EndCode, tablename) + "') and (IsFaHuo='已发货')";
    }

    bool ReturnValue = false;
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
        string sqlstring = "select count(BoxLabel01) as num from " + tablename + "_FH where BoxLabel01='" + BoxLabelString + "'and left(FHType,1)=3 and FromAgentID=" + FromAgentID + " and ToAgentID=" + GetCookieCompID() + "";
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
        string sqlstring = "select count(BoxLabel01) as num from " + tablename + "_FH where BoxLabel01='" + BoxLabelString + "'and left(FHType,1)=3 and CompID=" + GetCookieCompID() + "";
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


    string sqlstringforcheck = "";
    string sqlstringforcheckforreplace = "";
    string tempvalueforcheck = "";
    private bool CheckBoxLabelCodeExist(string tablename, string BoxLabelString)
    {
        sqlstringforcheck = "select count(BoxLabel01) as num from " + tablename + " where BoxLabel01='" + BoxLabelString + "'";
        sqlstringforcheckforreplace = "select count(*) as num from " + tablename + "_BX where BoxLabel='" + BoxLabelString + "'";
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
        ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('清除成功！');window.location.href='FahuoNew.aspx'", true);
    }
}