using System;
using Wuqi.Webdiyer;
using commonlib;
using TJ.DBUtility;
using org.in2bits.MyXls;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_TJ_ChouJiangFlag : AuthorPage
{
    public int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            input_start_date.Value = DateTime.Now.ToString("yyyy-MM-01");
            input_end_date.Value = DateTime.Now.ToString("yyyy-MM-dd");
            DisplayData(_currentindex, AspNetPager1.PageSize);
        }

    }

    private void DisplayData(int pageIndex, int pageSize)
    {
        string _filtertemp = " Compid=" + GetCookieCompID();
        string mEndTime =Convert.ToDateTime(input_end_date.Value.ToString()).AddDays(1).ToString("yyyy-MM-dd");
        _filtertemp += " and LQtime between '" + input_start_date.Value + "' and '" + mEndTime + "'";
        if (inputSearchKeyword.Value.Length>0)
        {
            if (DDLField.SelectedValue.Equals("JXName")) {
                _filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
            }else
            {
                _filtertemp += " and UserID in (select UserID from TJ_User where NickName like '%"+ inputSearchKeyword.Value.Trim() + "%')";
            }
        }
        AspNetPager1.RecordCount = Convert.ToInt32(_tab.ExecuteQuery("select count(id) from TJ_ChouJiangFlag where " + _filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource= _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_ChouJiangFlag", _filtertemp, "id", "id",pageSize);
        GridView1.DataBind();
        
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        DisplayData(1, AspNetPager1.PageSize);
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        DisplayData(_currentindex, AspNetPager1.PageSize);
    }

    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }

    protected string FindUserNameFromUserId(string UserId)
    {
        if (!string.IsNullOrEmpty(UserId))
        {
            return _tab.ExecuteQueryForSingleValue("select NickName from TJ_User where UserID=" + UserId);
        }else
        {
            return "";
        }
    }

    protected void btn_createexcel_Click(object sender, EventArgs e)
    {
        string _filtertemp = " Compid=" + GetCookieCompID();
        string mEndTime = Convert.ToDateTime(input_end_date.Value.ToString()).AddDays(1).ToString("yyyy-MM-dd");
        _filtertemp += " and LQtime between '" + input_start_date.Value + "' and '" + mEndTime + "'";
        if (inputSearchKeyword.Value.Length > 0)
        {
            if (DDLField.SelectedValue.Equals("JXName"))
            {
                _filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
            }
            else
            {
                _filtertemp += " and UserID in (select UserID from TJ_User where NickName like '%" + inputSearchKeyword.Value.Trim() + "%')";
            }
        }
        string sqlstring = " select (select NickName from TJ_User where UserId=a.UserID) as NickName,JXName,LQtime,Rmarks from TJ_ChouJiangFlag a  where " + _filtertemp;
        DataTable dt = _tab.ExecuteQuery(sqlstring, null);
        if (dt.Rows.Count > 0)
        {
            CreateExcel(dt);
            dt.Dispose();
        }else
        {
            MessageBox.Show(this, "没有数据");
        }
    }

    public void CreateExcel(DataTable dt)
    {
        XlsDocument xls = new XlsDocument();
        xls.FileName = "抽奖记录信息表 " + input_start_date.Value + "至" + input_end_date.Value;
        Worksheet sheet = xls.Workbook.Worksheets.Add("抽奖记录信息");//状态栏标题名称  
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
        Cell cell1 = cells.Add(1, 1, "抽奖人昵称", cellXF);
        Cell cell2 = cells.Add(1, 2, "奖品名称", cellXF);
        Cell cell4 = cells.Add(1, 3, "抽奖时间", cellXF);
        Cell cell5 = cells.Add(1, 4, "备注", cellXF);
        int count = dt.Rows.Count;//统计数量 
        if (count > 62000)
        {
            count = 62000;
        }
        for (int i = 0; i < count; i++)
        {
            int rowIndex = i + 2;
            cells.Add(rowIndex, 1, dt.Rows[i]["NickName"].ToString(), cellXF);
            cells.Add(rowIndex, 2, dt.Rows[i]["JXName"].ToString(), cellXF);
            cells.Add(rowIndex, 3, Convert.ToDateTime(dt.Rows[i]["LQtime"]).ToString("yyyy-MM-dd hh:mm:ss"), cellXF);
            cells.Add(rowIndex, 4, dt.Rows[i]["Rmarks"].ToString(), cellXF);           
        }
        xls.Send();
    }
}