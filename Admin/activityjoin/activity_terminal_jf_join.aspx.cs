using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using commonlib;
using TJ.BLL;
using TJ.DBUtility;
using org.in2bits.MyXls;

public partial class Admin_activityjoin_activity_jf_join : AuthorPage
{
    TabExecute tab = new TabExecute();
    public string categories = string.Empty;
    public string category = string.Empty;
    public string seriestring = string.Empty;
    public string subtitlestring = string.Empty;
    //public string ChartTypeString = "column";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            input_start_date.Value = DateTime.Now.ToString("yyyy-MM-01");
            input_end_date.Value = Convert.ToDateTime(input_start_date.Value).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            subtitlestring = input_start_date.Value + "至" + input_end_date.Value;
            BTJ_DepartMent btjDepart = new BTJ_DepartMent();
            if (IsCompGrade())
            {
                ddl_depart.Visible = true;
                ddl_depart.DataSource = btjDepart.GetListsByFilterString("compid=" + GetCookieCompID() + " and parentid=" + GetCookieTJDepartID());
                ddl_depart.DataBind();
                ddl_depart.SelectedIndex = 0;
                ddl_jxs.Items.Clear();
                ddl_jxs.Items.Add(new ListItem("全部经销商", "0"));
                ddl_jxs.DataSource = tab.ExecuteQuery("select a.CompID,a.CompName from TJ_RegisterCompanys a where a.CompID in (SELECT authorcompid FROM TJ_DepartMent_CompAuthor where departid=" + ddl_depart.SelectedValue + ") order by CompName", null);
                ddl_jxs.DataBind();
                ddl_jxs.SelectedValue = "0";
            }
            else
            {
                ddl_depart.Visible = false;
                ddl_jxs.DataSource = tab.ExecuteQuery("select a.CompID,a.CompName from TJ_RegisterCompanys a where a.CompID in (SELECT authorcompid FROM TJ_DepartMent_CompAuthor where departid=" + GetCookieTJDepartID() + ")", null);
                ddl_jxs.DataBind();
            }
            Join();
        }
    }

    private void Join()
    {
        string[] types = new[] { "1", "4" };
        DateTime currentdate = DateTime.Now;
        string filstersing = "";
        if (ddl_jf_type.SelectedValue.Equals("3"))
        {
            ddl_jxs.SelectedValue = "0";
        }
        if (!ddl_jxs.SelectedValue.Equals("0"))
        {
            filstersing = "agentid in (select CompID from TJ_RegisterCompanys where ParentID=" + ddl_jxs.SelectedValue + ")";
        }
        else
        {
            if (!IsCompGrade())
            {
                filstersing = "agentid in (select CompID from TJ_RegisterCompanys where ParentID in (SELECT dc.authorcompid FROM TJ_DepartMent_CompAuthor dc where dc.departid=" + GetCookieTJDepartID() + "))";
            }
            else
            {
                if (!ddl_depart.SelectedValue.Equals("0"))
                {
                    filstersing = "agentid in (select CompID from TJ_RegisterCompanys where ParentID in (SELECT dc.authorcompid FROM TJ_DepartMent_CompAuthor dc where dc.departid=" + ddl_depart.SelectedValue + "))";
                }
            }
        }
        string sql = string.Empty;
        switch (ddl_jf_type.SelectedValue)
        {
            case "1":
                sql = "SELECT sum(prizevl) vl,wintypeid,yearnm y,monthnm m,daynm d FROM TJ_Activity_JXS_Win where " + (filstersing.Length > 0 ? filstersing + " and " : "") + " gettm between '" + input_start_date.Value + "' and '" + input_end_date.Value + "' group by wintypeid,yearnm,monthnm,daynm";
                break;
            case "2":
                sql = "SELECT sum(prizevl) vl,wintypeid,yearnm y,monthnm m FROM TJ_Activity_JXS_Win where " + (filstersing.Length > 0 ? filstersing + " and " : "") + " gettm between '" + input_start_date.Value + "' and '" + input_end_date.Value + "' group by wintypeid,yearnm,monthnm";
                break;
            case "3":
                sql = "SELECT sum(prizevl) vl,wintypeid,(select c.ParentID from TJ_RegisterCompanys c where c.CompID=agentid) as pid FROM TJ_Activity_JXS_Win where " + (filstersing.Length > 0 ? filstersing + " and " : "") + " gettm between '" + input_start_date.Value + "' and '" + input_end_date.Value + "' group by wintypeid,agentid order by pid,wintypeid";
                break;
            case "4":
                sql = "SELECT sum(prizevl) vl,wintypeid,agentid a,(select c.CompName from TJ_RegisterCompanys c where c.CompID=agentid) as anm FROM TJ_Activity_JXS_Win where " + (filstersing.Length > 0 ? filstersing + " and " : "") + " gettm between '" + input_start_date.Value + "' and '" + input_end_date.Value + "' group by wintypeid,agentid order by vl desc";
                break;
        }
        DataTable dt = tab.ExecuteQuery(sql, null);
        DataRow[] rows = null;
        StringBuilder sb = new StringBuilder();
        int count = 0;
        DataRow[] orderrows = null;
        if (dt.Rows.Count > 0)
        {
            int prizevl = 0;
            foreach (DataRow dataRow in dt.Rows)
            {
                prizevl += int.Parse(dataRow["vl"].ToString());
            }
            subtitlestring = (ddl_depart.SelectedValue.Equals("0") ? "" : ddl_depart.SelectedItem.Text+"-")+(ddl_jxs.SelectedValue.Equals("0") ? "" : ddl_jxs.SelectedItem.Text+"-") + "[" + input_start_date.Value + " 至 " + input_end_date.Value + "] 积分总额:" + prizevl;
            switch (ddl_jf_type.SelectedValue)
            {
                case "1":
                    double days = (DateTime.Parse(input_end_date.Value) - DateTime.Parse(input_start_date.Value)).TotalDays;
                    foreach (var type in types)
                    {
                        string temp = ",{name: '" + (type.Equals("1") ? "扫码积分" : "消费者扫码返利") + "',data:[";
                        string tempinner = string.Empty;
                        for (int d = 0; d <= days; d++)
                        {
                            currentdate = Convert.ToDateTime(input_start_date.Value).AddDays(d);
                            categories += ",'" + currentdate.ToString("MM-dd") + "'";
                            rows = dt.Select("y=" + currentdate.Year + " and m=" + currentdate.Month + " and d=" +
                                             currentdate.Day + " and wintypeid=" + type);
                            if (rows.Length > 0)
                            {
                                if (tempinner.Length.Equals(0))
                                {
                                    tempinner = rows[0]["vl"].ToString();
                                }
                                else
                                {
                                    tempinner += "," + rows[0]["vl"].ToString();
                                }
                            }
                            else
                            {
                                if (tempinner.Length.Equals(0))
                                {
                                    tempinner = "0";
                                }
                                else
                                {
                                    tempinner += ",0";
                                }
                            }
                        }
                        temp += tempinner;
                        temp += "]}";
                        sb.AppendLine(temp);
                    }
                    break;
                case "2":
                    break;
                case "3":
                    DataTable dttemp = dt.Clone();
                    string pid = string.Empty;
                    string wintypeid = string.Empty;
                    int jifen = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (string.IsNullOrEmpty(pid))
                        {
                            pid = row["pid"].ToString();
                            wintypeid = row["wintypeid"].ToString();
                            jifen = Convert.ToInt32(row["vl"]);
                        }
                        else
                        {
                            if (pid.Equals(row["pid"].ToString()) && wintypeid.Equals(row["wintypeid"].ToString()))
                            {
                                jifen += Convert.ToInt32(row["vl"]);
                            }
                            else
                            {
                                DataRow dr = dttemp.NewRow();
                                dr["vl"] = jifen;
                                dr["pid"] = pid;
                                dr["wintypeid"] = wintypeid;
                                dttemp.Rows.Add(dr);
                                jifen = Convert.ToInt32(row["vl"]);
                                pid = row["pid"].ToString();
                                wintypeid = row["wintypeid"].ToString();
                            }
                        }
                    }
                    DataRow dr1 = dttemp.NewRow();
                    dr1["vl"] = jifen;
                    dr1["pid"] = pid;
                    dr1["wintypeid"] = wintypeid;
                    dttemp.Rows.Add(dr1);
                    category = String.Empty;
                    foreach (var type in types)
                    {
                        string temp = ",{name: '" + (type.Equals("1") ? "扫码积分" : "消费者扫码返利") + "',data:[";
                        string tempinner = string.Empty;
                        count = 0;
                        if (orderrows == null)
                        {
                            orderrows = dttemp.Select("wintypeid=" + type, "vl desc");
                        }

                        foreach (DataRow orderrow in orderrows)
                        {
                            if (count >= 20)
                            {
                                break;
                            }
                            if (category.Length.Equals(0))
                            {
                                category = tab.ExecuteQueryForSingleValue("select CompName from TJ_RegisterCompanys where CompID=" + orderrow["pid"]);
                            }
                            if (orderrow["wintypeid"].ToString().Equals(type))
                            {
                                if (tempinner.Length.Equals(0))
                                {
                                    tempinner = orderrow["vl"].ToString();
                                }
                                else
                                {
                                    tempinner += "," + orderrow["vl"].ToString();
                                }
                            }
                            else
                            {
                                rows = dt.Select("pid=" + orderrow["pid"] + " and wintypeid=" + type);
                                if (rows.Length > 0)
                                {
                                    if (tempinner.Length.Equals(0))
                                    {
                                        tempinner = rows[0]["vl"].ToString();
                                    }
                                    else
                                    {
                                        tempinner += "," + rows[0]["vl"].ToString();
                                    }
                                }
                                else
                                {
                                    if (tempinner.Length.Equals(0))
                                    {
                                        tempinner = "0";
                                    }
                                    else
                                    {
                                        tempinner += ",0";
                                    }
                                }
                            }
                            categories += ",'" + category + "'";
                            category = string.Empty;
                            count++;
                        }
                        temp += tempinner;
                        temp += "]}";
                        sb.AppendLine(temp);
                    }
                    break;
                case "4":
                    category = String.Empty;
                    foreach (var type in types)
                    {
                        string temp = ",{name: '" + (type.Equals("1") ? "扫码积分" : "消费者扫码返利") + "',data:[";
                        string tempinner = string.Empty;
                        count = 0;
                        if (orderrows == null)
                        {
                            orderrows = dt.Select("wintypeid=" + type);
                        }

                        foreach (DataRow orderrow in orderrows)
                        {
                            if (count >= 30)
                            {
                                break;
                            }
                            if (category.Length.Equals(0))
                            {
                                category = orderrow["anm"].ToString();
                            }
                            if (orderrow["wintypeid"].ToString().Equals(type))
                            {
                                if (tempinner.Length.Equals(0))
                                {
                                    tempinner = orderrow["vl"].ToString();
                                }
                                else
                                {
                                    tempinner += "," + orderrow["vl"].ToString();
                                }
                            }
                            else
                            {
                                rows = dt.Select("a=" + orderrow["a"] + " and wintypeid=" + type);
                                if (rows.Length > 0)
                                {
                                    if (tempinner.Length.Equals(0))
                                    {
                                        tempinner = rows[0]["vl"].ToString();
                                    }
                                    else
                                    {
                                        tempinner += "," + rows[0]["vl"].ToString();
                                    }
                                }
                                else
                                {
                                    if (tempinner.Length.Equals(0))
                                    {
                                        tempinner = "0";
                                    }
                                    else
                                    {
                                        tempinner += ",0";
                                    }
                                }
                            }
                            categories += ",'" + category + "'";
                            category = string.Empty;
                            count++;
                        }
                        temp += tempinner;
                        temp += "]}";
                        sb.AppendLine(temp);
                    }
                    break;
            }
        }
        seriestring = sb.Length > 1 ? sb.ToString().Substring(1) : "";
        categories = categories.Length > 1 ? categories.Substring(1) : "";
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        Join();
    }

    protected void ddl_depart_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!ddl_depart.SelectedValue.Equals("0"))
        {
            ddl_jxs.Items.Clear();
            ddl_jxs.Items.Add(new ListItem("全部经销商", "0"));
            ddl_jxs.DataSource = tab.ExecuteQuery("select a.CompID,a.CompName from TJ_RegisterCompanys a where a.CompID in (SELECT authorcompid FROM TJ_DepartMent_CompAuthor where departid=" + ddl_depart.SelectedValue + ") order by CompName", null);
            ddl_jxs.DataBind();
            ddl_jxs.SelectedValue = "0";
        }
        Join();
    }

    protected void btn_createexcel_Click(object sender, EventArgs e)
    {
        DateTime currentdate = DateTime.Now;
        string filstersing = "";
        if (ddl_jf_type.SelectedValue.Equals("3"))
        {
            ddl_jxs.SelectedValue = "0";
        }
        if (!ddl_jxs.SelectedValue.Equals("0"))
        {
            filstersing = "agentid in (select CompID from TJ_RegisterCompanys where ParentID=" + ddl_jxs.SelectedValue + ")";
        }
        else
        {
            if (!IsCompGrade())
            {
                filstersing = "agentid in (select CompID from TJ_RegisterCompanys where ParentID in (SELECT dc.authorcompid FROM TJ_DepartMent_CompAuthor dc where dc.departid=" + GetCookieTJDepartID() + "))";
            }
            else
            {
                if (!ddl_depart.SelectedValue.Equals("0"))
                {
                    filstersing = "agentid in (select CompID from TJ_RegisterCompanys where ParentID in (SELECT dc.authorcompid FROM TJ_DepartMent_CompAuthor dc where dc.departid=" + ddl_depart.SelectedValue + "))";
                }
            }
        }
        string sql = string.Empty;
        switch (ddl_jf_type.SelectedValue)
        {
            case "1":
                sql = "SELECT sum(prizevl) vl,wintypeid,yearnm y,monthnm m,daynm d FROM TJ_Activity_JXS_Win where " + (filstersing.Length > 0 ? filstersing + " and " : "") + " gettm between '" + input_start_date.Value + "' and '" + input_end_date.Value + "' group by wintypeid,yearnm,monthnm,daynm order by y,m,d";
                break;
            case "2":
                sql = "SELECT sum(prizevl) vl,wintypeid,yearnm y,monthnm m FROM TJ_Activity_JXS_Win where " + (filstersing.Length > 0 ? filstersing + " and " : "") + " gettm between '" + input_start_date.Value + "' and '" + input_end_date.Value + "' group by wintypeid,yearnm,monthnm order by y,m";
                break;
            case "3":
                sql = "select sum(vl) vl,wintypeid,pid from (SELECT sum(prizevl) vl,wintypeid,(select c.ParentID from TJ_RegisterCompanys c where c.CompID=agentid) as pid FROM TJ_Activity_JXS_Win where " + (filstersing.Length > 0 ? filstersing + " and " : "") + " gettm between '" + input_start_date.Value + "' and '" + input_end_date.Value + "' group by wintypeid,agentid )a group by pid,wintypeid order by vl desc,pid asc";
                break;
            case "4":
                sql = "SELECT sum(prizevl) vl,wintypeid,agentid a,(select c.CompName from TJ_RegisterCompanys c where c.CompID=agentid) as anm FROM TJ_Activity_JXS_Win where " + (filstersing.Length > 0 ? filstersing + " and " : "") + " gettm between '" + input_start_date.Value + "' and '" + input_end_date.Value + "' group by wintypeid,agentid order by vl desc ";
                break;
        }
        DataTable dt = tab.ExecuteQuery(sql, null);
        StringBuilder sb = new StringBuilder();
        if (dt.Rows.Count > 0)
        {
            DataTable mDataTable = new DataTable("TJ_Activity_JXS_Win");
            DataColumn mDataColumn1 = null;
            DataColumn mDataColumn2 = null;
            DataColumn mDataColumn3 = null;
            mDataColumn1 = new DataColumn("mType", Type.GetType("System.String"));//汇总类别（天、经销商、终端店）
            mDataColumn2 = new DataColumn("mSMJF", Type.GetType("System.String"));//扫码积分
            mDataColumn3 = new DataColumn("mFL", Type.GetType("System.String"));//返利积分
            mDataTable.Columns.Add(mDataColumn1);
            mDataTable.Columns.Add(mDataColumn2);
            mDataTable.Columns.Add(mDataColumn3);
            DataRow[] mSMJFRows=dt.Select("wintypeid=1");
            for (int i=0;i< mSMJFRows.Length;i++)
            {
                
                DataRow mDataRow = mDataTable.NewRow();
                if (ddl_jf_type.SelectedValue.Equals("1")) {
                    mDataRow["mType"] = mSMJFRows[i][2] + "-" + mSMJFRows[i][3] + "-" + mSMJFRows[i][4];
                    mDataRow["mSMJF"] = mSMJFRows[i][0];
                    DataRow[] mFLRows = dt.Select("y=" + mSMJFRows[i][2] + " and m=" + mSMJFRows[i][3] + " and d=" + mSMJFRows[i][4] + " and wintypeid=4");
                    if (mFLRows.Length > 0)
                    {
                        mDataRow["mFL"] = mFLRows[0][0];
                    } else
                    {
                        mDataRow["mFL"] = 0;
                    }
                }
                else if(ddl_jf_type.SelectedValue.Equals("3"))
                {
                    if (i >= 20)
                    {
                        break;
                    }
                    mDataRow["mType"] = tab.ExecuteQueryForSingleValue("select CompName from TJ_RegisterCompanys where CompID=" + mSMJFRows[i][2]);
                    mDataRow["mSMJF"] = mSMJFRows[i][0];
                    DataRow[] mFLRows = dt.Select("pid=" + mSMJFRows[i][2] + " and wintypeid=4");
                    if (mFLRows.Length > 0)
                    {
                        mDataRow["mFL"] = mFLRows[0][0];
                    }
                    else
                    {
                        mDataRow["mFL"] = 0;
                    }
                }else if (ddl_jf_type.SelectedValue.Equals("4"))
                {
                    if (i >= 30)
                    {
                        break;
                    }
                    mDataRow["mType"] = mSMJFRows[i][3];
                    mDataRow["mSMJF"] = mSMJFRows[i][0];
                    DataRow[] mFLRows = dt.Select("a=" + mSMJFRows[i][2] + " and wintypeid=4");
                    if (mFLRows.Length > 0)
                    {
                        mDataRow["mFL"] = mFLRows[0][0];
                    }
                    else
                    {
                        mDataRow["mFL"] = 0;
                    }
                }
                mDataTable.Rows.Add(mDataRow);
            }
            CreateExcel(mDataTable, ddl_depart.SelectedItem+" 终端积分汇总（"+ ddl_jf_type .SelectedItem+ "）【" + input_start_date.Value+"至"+ input_end_date.Value+"】", "终端积分汇总", ddl_jf_type.SelectedValue);
            mDataTable.Dispose();
            dt.Dispose();
        }
        else
        {
            MessageBox.Show(this, "没有数据！");
        }
    }

    public void CreateExcel(DataTable dt, string filename, string sheetnm,string mType)
    {
        XlsDocument xls = new XlsDocument();
        xls.FileName = filename;
        Worksheet sheet = xls.Workbook.Worksheets.Add(sheetnm);//状态栏标题名称  
        ColumnInfo cinfo = new ColumnInfo(xls, sheet);
        cinfo.Collapsed = true;
        //设置列的范围 如 0列-10列 
        cinfo.ColumnIndexStart = 0;//列开始 
        cinfo.ColumnIndexEnd = 6;//列结束 
        cinfo.Collapsed = true;
        cinfo.Width = 150 * 60;
        sheet.AddColumnInfo(cinfo);
        XF cellXF = xls.NewXF();
        cellXF.VerticalAlignment = VerticalAlignments.Centered;
        cellXF.HorizontalAlignment = HorizontalAlignments.Centered;
        cellXF.Font.Height = 18 * 12;
        Cells cells = sheet.Cells;
        if (mType.Equals("1")) {
            cells.Add(1, 1, "汇总时间", cellXF);
        }else if (mType.Equals("2"))
        {
            cells.Add(1, 1, "经销商名称", cellXF);
        }else if (mType.Equals("3"))
        {
            cells.Add(1, 1, "终端店名称", cellXF);
        }
        cells.Add(1, 2, "扫码积分", cellXF);
        cells.Add(1, 3, "扫码返利积分", cellXF);
        int count = dt.Rows.Count;//统计数量 
        if (count > 62000)
        {
            count = 62000;
        }
        for (int i = 0; i < count; i++)
        {
            int rowIndex = i + 2;
            cells.Add(rowIndex, 1, dt.Rows[i]["mType"].ToString(), cellXF);
            cells.Add(rowIndex, 2, dt.Rows[i]["mSMJF"].ToString(), cellXF);
            cells.Add(rowIndex, 3, dt.Rows[i]["mFL"].ToString(), cellXF);
        }
        xls.Send();
    }
}