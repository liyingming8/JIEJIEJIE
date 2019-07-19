using System;
using System.Data;
using System.Web.UI.WebControls;
using org.in2bits.MyXls;
using TJ.DBUtility;
using TJ.BLL;
using commonlib;
using Color = System.Drawing.Color;
using System.Web.UI.HtmlControls;

public partial class Admin_TJ_OrderInfo_Integral : AuthorPage
{
    BTJ_OrderInfo_Integral bll = new BTJ_OrderInfo_Integral();
    TabExecute tab = new TabExecute();
    public BTJ_AwardInfo BtjAward = new BTJ_AwardInfo();
    private int _currentindex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_start.Value = DateTime.Now.ToString("yyyy-MM-01");
            txt_end.Value = DateTime.Now.ToString("yyyy-MM-dd");
            _currentindex = 1;
            ddl_jiangpin.DataSource = BtjAward.GetListsByFilterString("compid=" + GetCookieCompID());
            ddl_jiangpin.DataBind();
            AspNetPager1.CurrentPageIndex = 1;
            DisplayData(1, AspNetPager1.PageSize);
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_OrderInfo_IntegralAddEdit.aspx?cmd=" + Sc.EncryptQueryString("edit") + "&ID={0}',680,480,'兑奖处理')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        var dataKey = GridView1.DataKeys[e.RowIndex];
        if (dataKey != null)
        {
            bll.Delete(int.Parse(dataKey["OrderID"].ToString()));
            DisplayData(_currentindex, AspNetPager1.PageSize);
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
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
                ((HtmlImage)e.Row.FindControl("editimg")).Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString()));
                if (Convert.ToInt32(dataKey["OrderStatusID"].ToString()) == 2)
                {
                    ((Label)e.Row.FindControl("LabelIsShouHuo")).ForeColor = Color.Green;
                }
                else
                {
                    ((Label)e.Row.FindControl("LabelIsShouHuo")).ForeColor = Color.Red;
                }
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

    private void DisplayData(int pageIndex, int pageSize)
    {
        string filtertemp = "CompID=" + GetCookieCompID() + " and OrderDate between '" + txt_start.Value + "' and '" + Convert.ToDateTime(txt_end.Value).AddDays(1).ToString("yyyy-MM-dd") + "'";
        if (!string.IsNullOrEmpty(ddl_jiangpin.SelectedValue) && !ddl_jiangpin.SelectedValue.Equals("0"))
        {
            filtertemp += " and GoodsID=" + ddl_jiangpin.SelectedValue;
        }
        if (!string.IsNullOrEmpty(ddl_UserType.SelectedValue) && !ddl_UserType.SelectedValue.Equals("0"))
        {
            filtertemp += " and UserType=" + ddl_UserType.SelectedValue;
        }
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
        }
        AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(OrderID) from TJ_OrderInfo_Integral where " + filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_OrderInfo_Integral", filtertemp, "OrderID", "OrderID", pageSize);
        GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }

    public void CreateExcel(DataTable dt)
    { 
        XlsDocument xls = new XlsDocument();
        xls.FileName = "消费者兑奖信息-" +txt_start.Value+"-"+txt_end.Value ;
        Worksheet sheet = xls.Workbook.Worksheets.Add(txt_start.Value + "至" + txt_end.Value);//状态栏标题名称  
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
        Cell cell1 = cells.Add(1, 1, "编号", cellXF);
        Cell cell2 = cells.Add(1, 2, "奖品", cellXF);
        Cell cell3 = cells.Add(1, 3, "数量", cellXF);
        Cell cell4 = cells.Add(1, 4, "申请时间", cellXF);
        Cell cell5 = cells.Add(1, 5, "联系人", cellXF);
        Cell cell6 = cells.Add(1, 6, "电话", cellXF);
        Cell cell7 = cells.Add(1, 7, "地址", cellXF);
        Cell cell8 = cells.Add(1, 8, "备注", cellXF);
        Cell cell9 = cells.Add(1, 9, "状态", cellXF);
        int count = dt.Rows.Count;//统计数量 
        if (count > 62000)
        {
            count = 62000;
        }
        for (int i = 0; i < count; i++)
        {
            int rowIndex = i + 2;
            cells.Add(rowIndex, 1, dt.Rows[i]["bh"].ToString(), cellXF);
            cells.Add(rowIndex, 2, dt.Rows[i]["jp"].ToString(), cellXF);
            cells.Add(rowIndex, 3, dt.Rows[i]["sl"].ToString(), cellXF);
            cells.Add(rowIndex, 4, dt.Rows[i]["tm"].ToString(), cellXF);
            cells.Add(rowIndex, 5, dt.Rows[i]["nm"].ToString(), cellXF);
            cells.Add(rowIndex, 6, dt.Rows[i]["ph"].ToString(), cellXF);
            cells.Add(rowIndex, 7, dt.Rows[i]["addr"].ToString(), cellXF);
            cells.Add(rowIndex, 8, dt.Rows[i]["bz"].ToString(), cellXF);
            cells.Add(rowIndex, 9, Convert.ToInt32(dt.Rows[i]["st"].ToString()).Equals(0)?"未处理":"已处理", cellXF);
        }
        xls.Send();
    }
    protected void btn_excel_out_Click(object sender, EventArgs e)
    {
        string filtertemp = "CompID=" + GetCookieCompID() + " and OrderDate between '" + txt_start.Value + "' and '" + Convert.ToDateTime(txt_end.Value).AddDays(1).ToString("yyyy-MM-dd") + "'";
        if (!string.IsNullOrEmpty(ddl_jiangpin.SelectedValue) && !ddl_jiangpin.SelectedValue.Equals("0"))
        {
            filtertemp += " and GoodsID=" + ddl_jiangpin.SelectedValue;
        }
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
        }
       DataTable dt = tab.ExecuteQuery("select OrderNumber bh,(select AwardThing from TJ_AwardInfo a where a.AWID=GoodsID) as jp,OrderNum sl,Remarks bz ,CustomerName nm,CustomerPhone ph,[Address] addr,OrderStatusID st,OrderDate tm from TJ_OrderInfo_Integral where " + filtertemp, null);
        CreateExcel(dt);
        dt.Dispose();
    }
}
