using System;
using System.Data; 
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using TJ.DBUtility;
using commonlib;
using org.in2bits.MyXls;
using TJ.BLL;

public partial class Admin_TJ_ActivitySMInfoJoin : AuthorPage
{
    TabExecute tab = new TabExecute();
    private int _currentindex = 1;
    InternetHandle internete = new InternetHandle();
    BTJ_DepartMent btjDepart = new BTJ_DepartMent();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (IsCompGrade())
            {
                ddl_departid.Visible = true;
                ddl_departid.DataSource = btjDepart.GetListsByFilterString("compid=" + GetCookieCompID() + " and parentid=" + GetCookieTJDepartID());
                ddl_departid.DataBind();
                ddl_departid.SelectedIndex = 0; 
            }
            else
            {
                ddl_departid.Visible = false; 
            } 
            _currentindex = 1;
            FillDDL();
            txt_start.Value = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
            txt_end.Value = DateTime.Now.ToString("yyyy-MM-dd");
            AspNetPager1.CurrentPageIndex = 1;
            AspNetPager1.RecordCount = 0;
            DisplayData(1); 
        }
    }

    private void FillDDL()
    {
        ddl_jingxiaoshang.Items.Clear();
        ddl_jingxiaoshang.Items.Add(new ListItem("全部经销商","0")); 
        if (IsCompGrade())
        {
            ddl_jingxiaoshang.DataSource = tab.ExecuteQuery("select a.CompID,a.CompName from TJ_RegisterCompanys a where a.CompID in (SELECT authorcompid FROM TJ_DepartMent_CompAuthor where departid=" + ddl_departid.SelectedValue + ")", null);
        }
        else
        {
            ddl_jingxiaoshang.DataSource = tab.ExecuteQuery("select a.CompID,a.CompName from TJ_RegisterCompanys a where a.CompID in (SELECT authorcompid FROM TJ_DepartMent_CompAuthor where departid=" + GetCookieTJDepartID() + ")", null);
        }
        ddl_jingxiaoshang.DataBind();
        ddl_jingxiaoshang.SelectedValue = "0";
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_375SMinfoAddEdit.aspx?cmd=" + Sc.EncryptQueryString("edit") + "&ID={0}',600,450,'TJ_375SMinfo')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    public string XiangXiLinkStringForUserInfo(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_UserInfoForShow.aspx?ID={0}',500,450,'客户信息')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c"); 
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize*(AspNetPager1.CurrentPageIndex-1)+ e.Row.RowIndex + 1).ToString();
        }
    }
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        AspNetPager1.RecordCount = 0;
        DisplayData(1);
    }

    protected void Openmapshowpage(object sender, EventArgs e)
    {
        Response.Redirect("TJ_SaoMaMapShow.aspx?startday="+txt_start.Value+"&endday="+txt_end.Value);
    }

    private void DisplayData(int newpageid)
    {
        string temp = "";
        if (inputSearchKeyword.Value.Length > 0)
        {
            temp += "&label=" + inputSearchKeyword.Value;
        }
        if (!string.IsNullOrEmpty(ddl_jingxiaoshang.SelectedValue) && !ddl_jingxiaoshang.SelectedValue.Equals("0"))
        {
            temp += "&agent_id=" + ddl_jingxiaoshang.SelectedValue;
        } 
        if (!IsCompGrade())
        {
            temp += "&departid=" + (GetCookieTJDepartID().Equals("3") ? "0" : GetCookieTJDepartID());
        } 
        string datastring = "";
        if (newpageid.Equals(1))
        {
            datastring = internete.GetUrlData("http://117.34.70.23:8888/agent/label/scan/?date_from=" + txt_start.Value + "&date_to=" +
                               txt_end.Value + "&records_per_page=" + AspNetPager1.PageSize + temp);
        }
        else
        {
            datastring = internete.GetUrlData("http://117.34.70.23:8888/agent/label/scan/?date_from=" + txt_start.Value + "&date_to=" +
                               txt_end.Value + "&page=" + newpageid + "&records_per_page=" + AspNetPager1.PageSize + temp);
        }
        JObject obj = JObject.Parse(datastring);
        int record = int.Parse(obj["total_records"].ToString());
        if (record > 0)
        {
            AspNetPager1.RecordCount = record;
        }
       
        JArray jarray = JArray.Parse(obj["details"].ToString());
        if (jarray.Count > 0)
        {
            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("jxs");
            dttemp.Columns.Add("label");
            dttemp.Columns.Add("sm_sheng");
            dttemp.Columns.Add("sm_shi");
            dttemp.Columns.Add("sm_xian");
            dttemp.Columns.Add("xfznknm");
            dttemp.Columns.Add("manger");
            dttemp.Columns.Add("sm_time");
            dttemp.Columns.Add("sm_address");
            dttemp.Columns.Add("AcceptAgent");
            foreach (JObject o in jarray)
            {
                DataRow row = dttemp.NewRow();
                row["jxs"] = o["agent_name"].ToString();
                row["label"] = o["label"].ToString();
                row["sm_sheng"] = o["sm_sheng"].ToString();
                row["sm_shi"] = o["sm_shi"].ToString();
                row["sm_xian"] = o["sm_xian"].ToString();
                row["xfznknm"] = o["sm_user"].ToString();
                row["manger"] = o["manger_name"].ToString();
                row["sm_time"] = o["sm_time"].ToString();
                row["sm_address"] = o["sm_address"].ToString();
                row["AcceptAgent"] = o["AcceptAgent"].ToString();
                dttemp.Rows.Add(row);
            }
            GridView1.DataSource = dttemp;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }

    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex);
    }

    protected void btn_createexcel_Click(object sender, EventArgs e)
    {
        string temp = "";
        if (inputSearchKeyword.Value.Length > 0)
        {
            temp += "&label=" + inputSearchKeyword.Value;
        }

        if (!string.IsNullOrEmpty(ddl_jingxiaoshang.SelectedValue) && !ddl_jingxiaoshang.SelectedValue.Equals("0"))
        {
            temp += "&agent_id=" + ddl_jingxiaoshang.SelectedValue;
        } 
        temp += "&departid=" + (GetCookieTJDepartID().Equals("3") ? "0" : GetCookieTJDepartID());
        string datastring = "";
        datastring = internete.GetUrlData("http://117.34.70.23:8888/agent/label/scan/?date_from=" + txt_start.Value +"&date_to=" +txt_end.Value + "&page=0&records_per_page=" + AspNetPager1.PageSize + temp);
        JObject obj = JObject.Parse(datastring);
        int record = int.Parse(obj["total_records"].ToString());
        if (record > 0)
        {
            AspNetPager1.RecordCount = record;
        }

        JArray jarray = JArray.Parse(obj["details"].ToString());
        DataTable dttemp = new DataTable();
        if (jarray.Count > 0)
        {
            dttemp.Columns.Add("jxs");
            dttemp.Columns.Add("label");
            dttemp.Columns.Add("sm_sheng");
            dttemp.Columns.Add("sm_shi");
            dttemp.Columns.Add("sm_xian");
            dttemp.Columns.Add("xfznknm");
            dttemp.Columns.Add("manger");
            dttemp.Columns.Add("sm_time");
            dttemp.Columns.Add("sm_address");
            dttemp.Columns.Add("AcceptAgent");
            foreach (JObject o in jarray)
            {
                DataRow row = dttemp.NewRow();
                row["jxs"] = o["agent_name"].ToString();
                row["label"] = o["label"].ToString();
                row["sm_sheng"] = o["sm_sheng"].ToString();
                row["sm_shi"] = o["sm_shi"].ToString();
                row["sm_xian"] = o["sm_xian"].ToString();
                row["xfznknm"] = o["sm_user"].ToString();
                row["manger"] = o["manger_name"].ToString();
                row["sm_time"] = o["sm_time"].ToString();
                row["sm_address"] = o["sm_address"].ToString();
                row["AcceptAgent"] = o["AcceptAgent"].ToString();
                dttemp.Rows.Add(row);
            }

            if (dttemp.Rows.Count > 0)
            {
                CreateExcel(dttemp, "消费者扫码-" + txt_start.Value + "至" + txt_end.Value, "扫码信息");
            }

            dttemp.Dispose();
        }
    }

    public void CreateExcel(DataTable dt, string filename, string sheetnm)
    {
        XlsDocument xls = new XlsDocument();
        xls.FileName = filename;
        Worksheet sheet = xls.Workbook.Worksheets.Add(sheetnm);//状态栏标题名称  
        ColumnInfo cinfo = new ColumnInfo(xls, sheet);
        cinfo.Collapsed = true;
        //设置列的范围 如 0列-10列 
        cinfo.ColumnIndexStart = 0;//列开始 
        cinfo.ColumnIndexEnd = 9;//列结束 
        cinfo.Collapsed = true;
        cinfo.Width = 80 * 60;
        sheet.AddColumnInfo(cinfo);
        XF cellXF = xls.NewXF();
        cellXF.VerticalAlignment = VerticalAlignments.Centered;
        cellXF.HorizontalAlignment = HorizontalAlignments.Centered;
        cellXF.Font.Height = 18 * 12;
        Cells cells = sheet.Cells;
        //Cell cell1 = cells.Add(1, 1, "编号");
        cells.Add(1, 1, "标签号码", cellXF);
        cells.Add(1, 2, "省份", cellXF);
        cells.Add(1, 3, "市", cellXF);
        cells.Add(1, 4, "县(区)", cellXF);
        cells.Add(1, 5, "扫码地址", cellXF);
        cells.Add(1, 6, "昵称", cellXF);
        cells.Add(1, 7, "时间", cellXF);
        cells.Add(1, 8, "经销商", cellXF);
        cells.Add(1, 9, "城市经理", cellXF);
        cells.Add(1, 10, "终端店", cellXF);
        int count = dt.Rows.Count;//统计数量 
        if (count > 62000)
        {
            count = 62000;
        }
        for (int i = 0; i < count; i++)
        {
            int rowIndex = i + 2;
            cells.Add(rowIndex, 1, dt.Rows[i]["label"].ToString(), cellXF);
            cells.Add(rowIndex, 2, dt.Rows[i]["sm_sheng"].ToString(), cellXF);
            cells.Add(rowIndex, 3, dt.Rows[i]["sm_shi"].ToString(), cellXF);
            cells.Add(rowIndex, 4, dt.Rows[i]["sm_xian"].ToString(), cellXF);
            cells.Add(rowIndex, 5, dt.Rows[i]["sm_address"].ToString(), cellXF);
            cells.Add(rowIndex, 6, dt.Rows[i]["xfznknm"].ToString(), cellXF);
            cells.Add(rowIndex, 7, dt.Rows[i]["sm_time"].ToString(), cellXF);
            cells.Add(rowIndex, 8, dt.Rows[i]["jxs"].ToString(), cellXF);
            cells.Add(rowIndex, 9, dt.Rows[i]["manger"].ToString(), cellXF);
            cells.Add(rowIndex, 10, dt.Rows[i]["AcceptAgent"].ToString(), cellXF);
        }
        xls.Send();
    }

    protected void ddl_departid_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL();
    }

    protected void ddl_jingxiaoshang_ComboBoxChanged(object sender, EventArgs e)
    {
        DisplayData(_currentindex<1?1:_currentindex);
    }
}
