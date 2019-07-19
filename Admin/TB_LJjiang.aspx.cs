using System;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using System.Data.SqlClient;
using System.Data;
using org.in2bits.MyXls;
using Color = System.Drawing.Color;

public partial class TB_LJjiang : AuthorPage
{
    private readonly BTB_SmallBuJiang bll = new BTB_SmallBuJiang();
    private MTB_SmallBuJiang mod = new MTB_SmallBuJiang();
    public BTJ_JXInfo bjx = new BTJ_JXInfo();
    public BTB_Products_Infor bpro = new BTB_Products_Infor();

    private readonly SqlConnection sqlconn =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString());

    private readonly DBClass dht = new DBClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgridview();
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return
                string.Format(
                    "javascript:var win=openWinCenter('TB_SmallBuJiangAddEdit.aspx?cmd=edit&ID={0}',600,400)", ID);
        }
        else
        {
            return "";
        }
    }

    private void fillgridview()
    {
        DataTable dbj = new DataTable();
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            dbj =
                dht.getLJBuJiang(DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'" +
                                 " and  compid=" + GetCookieCompID() + " order by BJDate desc ");
            GridView1.DataSource = dbj;
        }
        else
        {
            dbj = dht.getLJBuJiang("compid=" + GetCookieCompID() + " order by BJDate desc ");
            GridView1.DataSource = dbj;
        }
        Session["dbj"] = dbj;
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string startvalue = ((Label) GridView1.Rows[e.RowIndex].FindControl("LabelStartlabelcode")).Text;
        string endvalue = ((Label) GridView1.Rows[e.RowIndex].FindControl("LabelEndlabelcode")).Text;
        string jpname = ((Label) GridView1.Rows[e.RowIndex].FindControl("LabelRemarks")).Text;


        DeleteDeployJXDetail(GridView1.DataKeys[e.RowIndex]["ID"].ToString(), startvalue, endvalue, jpname);
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["ID"].ToString()));
        fillgridview();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgridview();
    }

    protected void gvwDesignationName_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
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
            ((Label) e.Row.FindControl("LabelIndex")).Text =
                (GridView1.PageSize*GridView1.PageIndex + e.Row.RowIndex + 1).ToString();
            if (IsSuperAdmin())
            {
                ((LinkButton) e.Row.Cells[9].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')");
            }
            else
            {
                e.Row.Cells[9].Enabled = false;
                e.Row.Cells[9].ForeColor = Color.LightGray;
            }
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    private void DeleteDeployJXDetail(string DPJXID, string star, string end, string jpname)
    {
        SqlCommand sqlcommand1 = new SqlCommand();
        sqlcommand1.Connection = sqlconn;
        sqlcommand1.CommandText = "update TB_BuJiangLuJiu set JXID=0 ,JpName='',LQFlag='',JZTime=null where DYCode>='" +
                                  star + "' and DYCode<='" + end + "' and JpName='" + jpname + "' and CompID=" +
                                  GetCookieCompID() + "   ";
        if (sqlconn.State != ConnectionState.Open)
        {
            sqlconn.Open();
        }
        sqlcommand1.CommandTimeout = 3600;
        sqlcommand1.ExecuteNonQuery();
        sqlcommand1.Dispose();
        sqlconn.Close();
    }


    protected void BtDC_ServerClick(object sender, EventArgs e)
    {
        XlsDocument xls = new XlsDocument();

        xls.FileName = "布奖信息统计";

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
        Cell cell1 = cells.Add(1, 1, "开始编码", cellXF);
        Cell cell2 = cells.Add(1, 2, "结束编码", cellXF);
        Cell cell3 = cells.Add(1, 3, "布奖产品", cellXF);
        Cell cell4 = cells.Add(1, 4, "奖项", cellXF);
        Cell cell5 = cells.Add(1, 5, "奖品", cellXF);
        Cell cell6 = cells.Add(1, 6, "数量", cellXF);


        Cell cell7 = cells.Add(1, 7, "布奖时间", cellXF);

        DataTable db = (DataTable) Session["dbj"];
        for (int i = 0; i < db.Rows.Count; i++)
        {
            int rowIndex = i + 2;
            int colIndex = i + 1;

            cells.Add(rowIndex, 1, db.Rows[i]["Startlabelcode"].ToString(), cellXF);
            cells.Add(rowIndex, 2, db.Rows[i]["Endlabelcode"].ToString(), cellXF);
            cells.Add(rowIndex, 3, bpro.GetList(int.Parse(db.Rows[i]["beizhu"].ToString())).Products_Name, cellXF);
            cells.Add(rowIndex, 4, db.Rows[i]["JXID"].ToString().Equals("1") ? "红包" : "实物", cellXF);

            cells.Add(rowIndex, 5, db.Rows[i]["Remarks"].ToString(), cellXF);
            cells.Add(rowIndex, 6, db.Rows[i]["count"].ToString(), cellXF);


            cells.Add(rowIndex, 7, Convert.ToDateTime(db.Rows[i]["BJDate"].ToString()).ToString("yyyy-MM-dd"), cellXF);
        }
        xls.Send();
    }
}