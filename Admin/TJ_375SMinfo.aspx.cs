using System;
using System.Data; 
using System.Web.UI.WebControls;
using org.in2bits.MyXls;
using TJ.BLL;
using TJ.DBUtility;
using commonlib;
public partial class Admin_TJ_375SMinfo : AuthorPage
{
    TabExecute tab = new TabExecute();
    private int _currentindex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _currentindex = 1;
            txt_start.Value = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
            txt_end.Value = DateTime.Now.ToString("yyyy-MM-dd"); ;
            AspNetPager1.CurrentPageIndex = 1;
            DisplayData(1, AspNetPager1.PageSize);
        }
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
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
            {
                ((HyperLink)e.Row.FindControl("hyperlinkuser")).Attributes.Add("onclick", XiangXiLinkStringForUserInfo(dataKey["UserID"].ToString()));
            }
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex - 1) + e.Row.RowIndex + 1).ToString();

        }
    }
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        DisplayData(1, AspNetPager1.PageSize);
    }

    protected void Openmapshowpage(object sender, EventArgs e)
    {
        Response.Redirect("TJ_SaoMaMapShow.aspx?startday="+txt_start.Value+"&endday="+txt_end.Value);
    }

    private string _filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        if (GetCookieRID() == "155")
        {
            _filtertemp = "CompID=" + GetCookieCompID() + " and sm_date between '" + txt_start.Value + "' and '" + txt_end.Value  + "'";
            if (inputSearchKeyword.Value.Trim().Length > 0)
            {
                _filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
            }
            AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(ID) from TJ_SMinfo_2018 where " + _filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_SMinfo_2018", _filtertemp, "ID desc", "ID", pageSize);
            GridView1.DataBind();
        }
        else
        {
            _filtertemp = "CompID=" + GetCookieCompID() + " and sm_date between '" + txt_start.Value + "' and '" + txt_end.Value + "'";
            if (inputSearchKeyword.Value.Trim().Length > 0)
            {
                _filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
            }
            AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(ID) from TJ_375SMinfo where " + _filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_375SMinfo", _filtertemp, "ID desc", "ID", pageSize);
            GridView1.DataBind();
        }

    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
    protected void btn_excel_Click(object sender, EventArgs e)
    {
        string sqlstring =
            "SELECT s.[LabelCode],u.NickName,s.[SMProc],s.[SMsj],s.[SMxj],s.[SMAddress],s.[SMTime] FROM [TJ_SMinfo_2018] s,[TJ_User] u where s.CompID=" +
            GetCookieCompID() + " and s.UserID=u.UserID and SMTime>='" + txt_start.Value + "' and SMTime<'"+Convert.ToDateTime(txt_end.Value).AddDays(1).ToString("yyyy-MM-dd")+"'";
        DataTable dttemp = tab.ExecuteNonQuery(sqlstring);
        CreateExcel(dttemp);
        dttemp.Dispose();
    } 
   
    public void CreateExcel(DataTable dt)
    {
        BTJ_RegisterCompanys btjRegister = new BTJ_RegisterCompanys();
         XlsDocument xls = new XlsDocument(); 
        xls.FileName = "扫码信息-"+btjRegister.GetList(int.Parse(GetCookieCompID())).CompName; 
        Worksheet sheet = xls.Workbook.Worksheets.Add(txt_start.Value+"至"+txt_end.Value);//状态栏标题名称  
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
        Cell cell1 = cells.Add(1, 1, "标签号码", cellXF);
        Cell cell2 = cells.Add(1, 2, "昵称", cellXF);
        Cell cell3 = cells.Add(1, 3, "省份", cellXF);
        Cell cell4 = cells.Add(1, 4, "市(地区)", cellXF);
        Cell cell5 = cells.Add(1, 5, "县(区)", cellXF);
        Cell cell6 = cells.Add(1, 6, "扫码地址", cellXF);
        Cell cell7 = cells.Add(1, 7, "扫码时间", cellXF);
        int count = dt.Rows.Count;//统计数量 
        if (count > 62000)
        {
            count = 62000;
        } 
        for (int i = 0; i < count; i++)
        {
            int rowIndex = i + 2;
            cells.Add(rowIndex, 1, dt.Rows[i]["LabelCode"].ToString(), cellXF);
            cells.Add(rowIndex, 2, dt.Rows[i]["NickName"].ToString(), cellXF);
            cells.Add(rowIndex, 3, dt.Rows[i]["SMProc"].ToString(), cellXF);
            cells.Add(rowIndex, 4, dt.Rows[i]["SMsj"].ToString(), cellXF);
            cells.Add(rowIndex, 5, dt.Rows[i]["SMxj"].ToString(), cellXF);
            cells.Add(rowIndex, 6, dt.Rows[i]["SMAddress"].ToString(), cellXF);
            cells.Add(rowIndex, 7, dt.Rows[i]["SMTime"].ToString(), cellXF);
        }
        xls.Send();
    }
}
