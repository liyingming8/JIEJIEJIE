using System;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using TJ.BLL;
using commonlib;
using System.Collections.Generic;
using System.Data.SqlClient;
using TJ.Model;
using System.Data;
using org.in2bits.MyXls;
using Wuqi.Webdiyer;
using Color = System.Drawing.Color;
using TJ.DBUtility;

public partial class Admin_TB_SmallBuJiang : AuthorPage
{
    private readonly BTB_SmallBuJiang bll = new BTB_SmallBuJiang();
    private MTB_SmallBuJiang mod = new MTB_SmallBuJiang();
    private readonly DBClass db = new DBClass();

    public BTJ_JXInfo bjx = new BTJ_JXInfo();
    public BTB_Products_Infor tbb = new BTB_Products_Infor();

    private readonly SqlConnection sqlconn =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString());

    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string comp = GetCookieCompID();
            if (comp == "1")
            {
                _currentindex = 1;
                DisplayData(_currentindex, AspNetPager1.PageSize);
                txtEndDate.Text = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd"); //获取今天的时间显示在文本框中
                txtStartDate.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            }
            else
            {
                fillgridview();
                txtEndDate.Text = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd"); //获取今天的时间显示在文本框中
                txtStartDate.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            }
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TB_SmallBuJiangAddEdit.aspx?cmd=edit&ID={0}',600,500,'号码段布奖编辑')", ID);
        }
        else
        {
            return "";
        }
    }

    //我自己写的等待修改
    private string tempstring = ""; //临时存储的就是把值传给了tempstring中

    private string ReturnFilterString()
    {
        tempstring = "CompID=" + GetCookieCompID();
        tempstring += " and BJDate between '" + txtStartDate.Text + "' and '" +
                      txtEndDate.Text+ "'"; //加一天
        if (inputSearchKeyword.Value.Length > 0)
        {
            if (DDLField.SelectedValue == "Remarks1")
            {
                IList<MTB_SmallBuJiang> ipro = bll.GetListsByFilterString("Remarks1=" + inputSearchKeyword.Value);
                    //写入文本框里的值
                if (ipro.Count > 0)
                {
                    tempstring += " and " + DDLField.SelectedValue + " ='" + ipro[0].Remarks1 + "'";
                }

                else
                {
                    tempstring += " and " + DDLField.SelectedValue + " ='null'";
                }
            }
            else
            {
                tempstring += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
            }
            //if (DDLField.SelectedValue == "布奖时间")
            //{

            if (txtStartDate.Text.Length > 0)
            {
                tempstring += "and BJDate>'" + txtStartDate.Text + "' and BJDate<'" + txtEndDate.Text + "'";
            }
            //}
        }
        tempstring += "order by BJDate desc";

        return tempstring; //以上是实现模糊查询的条件
    }

    private void fillgridview()
    {
        DataTable dzj = db.getLJBuJiang(ReturnFilterString());
        Session["dzj"] = dzj;
        GridView1.DataSource = dzj;
        GridView1.DataBind();
    }
    private const string Filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TB_SmallBuJiang where " + Filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TB_SmallBuJiang", Filtertemp, "ID", "ID", pageSize);
        GridView1.DataBind();
    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string startvalue = ((Label) GridView1.Rows[e.RowIndex].FindControl("LabelStartlabelcode")).Text; //根据开始号码段进行删除
        string endvalue = ((Label) GridView1.Rows[e.RowIndex].FindControl("LabelEndlabelcode")).Text; //根据开始号码段进行删除
        DeleteDeployJXDetail(GridView1.DataKeys[e.RowIndex]["ID"].ToString(), startvalue, endvalue);
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["ID"].ToString()));
        fillgridview();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GridView1.PageIndex = e.NewPageIndex;
        //fillgridview();
        // 得到该控件
        GridView theGrid = sender as GridView;
        int newPageIndex = 0;
        if (e.NewPageIndex == -3)
        {
            //点击了Go按钮
            TextBox txtNewPageIndex = null;

            //GridView较DataGrid提供了更多的API，获取分页块可以使用BottomPagerRow 或者TopPagerRow，当然还增加了HeaderRow和FooterRow
            GridViewRow pagerRow = theGrid.BottomPagerRow;

            if (pagerRow != null)
            {
                //得到text控件
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
            }
            if (txtNewPageIndex != null)
            {
                //得到索引
                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
            }
        }
        else
        {
            //点击了其他的按钮
            newPageIndex = e.NewPageIndex;
        }
        //防止新索引溢出
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;

        //得到新的值
        theGrid.PageIndex = newPageIndex;

        //重新绑定
        fillgridview();
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
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
            var key = GridView1.DataKeys[e.Row.RowIndex];
            ((Label) e.Row.FindControl("LabelIndex")).Text =
                (GridView1.PageSize*GridView1.PageIndex + e.Row.RowIndex + 1).ToString();
            if (IsSuperAdmin())
            {
                ((LinkButton) e.Row.Cells[7].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')"); //Cells[9]是说里面所对应的列的位置
            }
            else
            {
                e.Row.Cells[8].Enabled = false;
                e.Row.Cells[8].ForeColor = Color.LightGray;
            }
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        XlsDocument xls = new XlsDocument();

        xls.FileName = "中奖信息统计";

        Worksheet sheet = xls.Workbook.Worksheets.Add("Sheet1"); //状态栏标题名称  
        ColumnInfo cinfo = new ColumnInfo(xls, sheet);

        cinfo.Collapsed = true;

        //设置列的范围 如 0列-10列

        cinfo.ColumnIndexStart = 0; //列开始

        cinfo.ColumnIndexEnd = 8; //列结束

        cinfo.Collapsed = true;
        cinfo.Width = 80*60;
        sheet.AddColumnInfo(cinfo);

        XF cellXF = xls.NewXF();

        cellXF.VerticalAlignment = VerticalAlignments.Centered;

        cellXF.HorizontalAlignment = HorizontalAlignments.Centered;

        cellXF.Font.Height = 18*12;

        Cells cells = sheet.Cells;

        //Cell cell1 = cells.Add(1, 1, "编号");

        Cell cell1 = cells.Add(1, 1, "开始号码", cellXF);
        Cell cell2 = cells.Add(1, 2, "结束号码", cellXF);
        Cell cell3 = cells.Add(1, 3, "奖项", cellXF);
        Cell cell4 = cells.Add(1, 4, "数量", cellXF);
        Cell cell5 = cells.Add(1, 5, "布奖时间", cellXF); //表头的名称

        DataTable db = (DataTable) Session["dzj"];

        int count = db.Rows.Count; //统计数量

        if (count > 62000)
        {
            count = 62000;
        }

        for (int i = 0; i < count; i++)
        {
            int rowIndex = i + 2;
            int colIndex = i + 1;
            cells.Add(rowIndex, 1, db.Rows[i]["Startlabelcode"].ToString(), cellXF);
            cells.Add(rowIndex, 2, db.Rows[i]["Endlabelcode"].ToString(), cellXF);
            string name = bjx.GetList(Convert.ToInt32(db.Rows[i]["JXID"].ToString())).JxName;
            cells.Add(rowIndex, 3, name, cellXF);
            cells.Add(rowIndex, 4, db.Rows[i]["count"].ToString(), cellXF);
            cells.Add(rowIndex, 5, db.Rows[i]["BJDate"].ToString().Substring(0, 10), cellXF);
        }
        xls.Send();
    }

    private void DeleteDeployJXDetail(string DPJXID, string star, string end)
    {
        SqlCommand sqlcommand = new SqlCommand();
        sqlcommand.Connection = sqlconn;
        sqlcommand.CommandText = "delete from TJ_ZJLabelCodesmallInfo  where DPJXID='" + DPJXID + "'";
        if (sqlconn.State != ConnectionState.Open)
        {
            sqlconn.Open();
        }
        sqlcommand.CommandTimeout = 3600; //连接延误的时间
        sqlcommand.ExecuteNonQuery();
        sqlcommand.Dispose();
        sqlconn.Close();
        SqlCommand sqlcommand1 = new SqlCommand();
        sqlcommand1.Connection = sqlconn;
        sqlcommand1.CommandText = "update TB_SmalllabelInfo set ISBJflag=null where ISBJflag=" + DPJXID +
                                  " and DYlabelcode>='" + star + "' and DYlabelcode<='" + end + "'";
        if (sqlconn.State != ConnectionState.Open)
        {
            sqlconn.Open();
        }
        sqlcommand1.CommandTimeout = 3600;
        sqlcommand1.ExecuteNonQuery();
        sqlcommand1.Dispose();
        sqlconn.Close();
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}