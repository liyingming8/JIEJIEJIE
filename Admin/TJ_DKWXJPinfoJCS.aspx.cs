using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using org.in2bits.MyXls;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class TJ_DKWXJPinfoJCS : AuthorPage
{ 
    private readonly DBClass db = new DBClass();
    public BTB_SmallBuJiang bll1 = new BTB_SmallBuJiang();
    public MTB_SmallBuJiang mod1 = new MTB_SmallBuJiang();
    public BTB_Products_Infor bpro = new BTB_Products_Infor();
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取文本框里的时间定位
        if (!IsPostBack)
        {
            string comp = GetCookieCompID();
            if (comp == "1")
            {
                txtEndDate.Text = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
                txtStartDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"); //显示后两天的时间
                _currentindex = 1;
                DisplayData(_currentindex, AspNetPager1.PageSize);
            }
            else
            {
                txtEndDate.Text = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
                txtStartDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"); //显示后两天的时间
                fillgridview();
            }
        }
    }
     
    private void fillgridview()
    {
        DataTable dzj = db.GetDKWXJPinfo(ReturnFilterString());

        Session["dzj"] = dzj;
        GridView1.DataSource = dzj;
        GridView1.DataBind();
    }
    private const string Filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_DKWXJPinfo where " + Filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_DKWXJPinfo", Filtertemp, "ID", "ID", pageSize);
        GridView1.DataBind();
    }

    private string tempstring = "";

    private string ReturnFilterString()
    {
        tempstring = "CompID=" + GetCookieCompID();
        tempstring += " and LQtime between '" + txtStartDate.Text + "' and '" +
                      Convert.ToDateTime(txtEndDate.Text).AddDays(1) + "'";
        if (inputSearchKeyword.Value.Length > 0)
        {
            if (DDLField.SelectedValue == "Remarks1") //应该是产品名称有问题
            {
                IList<MTB_SmallBuJiang> ipro = bll1.GetListsByFilterString("Products_Name=" + inputSearchKeyword.Value);
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

            if (txtStartDate.Text.Length > 0)
            {
                tempstring += "and LQtime>'" + txtStartDate.Text + "' and LQtime<'" + txtEndDate.Text + "'";
            }
        }
        tempstring += "order by LQtime desc";


        return tempstring;
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
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

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    protected void BtnExport_Click(object sender, EventArgs e)
    {
        XlsDocument xls = new XlsDocument();

        xls.FileName = "中奖信息统计";

        Worksheet sheet = xls.Workbook.Worksheets.Add("Sheet1"); //状态栏标题名称  
        ColumnInfo cinfo = new ColumnInfo(xls, sheet);

        cinfo.Collapsed = true;

        //设置列的范围 如 0列-10列

        cinfo.ColumnIndexStart = 0; //列开始

        cinfo.ColumnIndexEnd = 7; //列结束

        cinfo.Collapsed = true;
        cinfo.Width = 80*60;
        sheet.AddColumnInfo(cinfo);

        XF cellXF = xls.NewXF();

        cellXF.VerticalAlignment = VerticalAlignments.Centered;

        cellXF.HorizontalAlignment = HorizontalAlignments.Centered;

        cellXF.Font.Height = 18*12;

        Cells cells = sheet.Cells;

        //Cell cell1 = cells.Add(1, 1, "编号");
        Cell cell1 = cells.Add(1, 1, "中奖号码", cellXF);
        Cell cell2 = cells.Add(1, 2, "验证码", cellXF);
        Cell cell3 = cells.Add(1, 3, "奖项", cellXF);
        Cell cell4 = cells.Add(1, 4, "奖品", cellXF);
        Cell cell5 = cells.Add(1, 5, "中奖人", cellXF);
        Cell cell6 = cells.Add(1, 6, "领取时间", cellXF); //表头的名称

        DataTable db = (DataTable) Session["dzj"];

        int count = db.Rows.Count; //统计数量

        if (count > 62000)
        {
            count = 62000;
        }

        for (int i = 0; i < count; i++)
        {
            int rowIndex = i + 2; 
            cells.Add(rowIndex, 1, db.Rows[i]["BoxLabel"].ToString(), cellXF);
            cells.Add(rowIndex, 2, db.Rows[i]["YZM"].ToString(), cellXF);
            cells.Add(rowIndex, 3, db.Rows[i]["JXname"].ToString(), cellXF);
            cells.Add(rowIndex, 4, db.Rows[i]["JPInfo"].ToString(), cellXF);
            cells.Add(rowIndex, 5, db.Rows[i]["WXname"].ToString(), cellXF);
            cells.Add(rowIndex, 6, db.Rows[i]["LQtime"].ToString().Substring(0, 10), cellXF);
        }
        xls.Send();
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }

}