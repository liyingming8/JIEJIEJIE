using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using System.Data;
using org.in2bits.MyXls;

public partial class TB_LJzjcode : AuthorPage
{
    private BTJ_ZJLabelCodesmallInfo bll = new BTJ_ZJLabelCodesmallInfo();
    private MTJ_ZJLabelCodesmallInfo mod = new MTJ_ZJLabelCodesmallInfo();
    public BTJ_JXInfo bjxinfo = new BTJ_JXInfo();
    public BTB_Products_Infor bpro = new BTB_Products_Infor();

    private readonly DBClass db = new DBClass();

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
                    "javascript:var win=openWinCenter('TJ_ZJLabelCodesmallInfoAddEdit.aspx?cmd=edit&ID={0}',600,400)",
                    ID);
        }
        else
        {
            return "";
        }
    }

    private void fillgridview()
    {
        DataTable dbj = new DataTable();
        string sqltext = "";

        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            if (DDLField.SelectedValue == "DYCode")
            {
                sqltext = "DYCode >='" + inputSearchKeyword.Value.Trim() + "' and DYCode<='" + jscode.Value.Trim() +
                          "' and JXID!=0";
            }
            else
            {
                sqltext = DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%' and JXID!=0";
            }

            sqltext += "and CompID=" + GetCookieCompID();
            dbj = db.getLabelCodeLJ(sqltext);

            GridView1.DataSource = dbj;
        }
        else
        {
            dbj = db.getLabelCodeLJ("JXID!=0 and CompID=" + GetCookieCompID());
            GridView1.DataSource = dbj;
        }

        Session["dbj"] = dbj;

        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["ZJCDID"].ToString()));
        //fillgridview();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
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
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
            }
        }
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["ID"].ToString()));
        //mod.Remarks = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("LabelJxID")).Text.Trim();
        //bll.Modify(mod);
        //GridView1.EditIndex = -1;
        //fillgridview();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillgridview();
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    protected void BtDC_ServerClick(object sender, EventArgs e)
    {
        XlsDocument xls = new XlsDocument();

        xls.FileName = "布奖号码统计";

        Worksheet sheet = xls.Workbook.Worksheets.Add("Sheet1"); //状态栏标题名称  
        ColumnInfo cinfo = new ColumnInfo(xls, sheet);

        cinfo.Collapsed = true;

        //设置列的范围 如 0列-10列

        cinfo.ColumnIndexStart = 0; //列开始

        cinfo.ColumnIndexEnd = 5; //列结束

        cinfo.Collapsed = true;
        cinfo.Width = 80*60;
        sheet.AddColumnInfo(cinfo);

        XF cellXF = xls.NewXF();

        cellXF.VerticalAlignment = VerticalAlignments.Centered;

        cellXF.HorizontalAlignment = HorizontalAlignments.Centered;

        cellXF.Font.Height = 18*12;


        Cells cells = sheet.Cells;

        //Cell cell1 = cells.Add(1, 1, "编号");
        Cell cell1 = cells.Add(1, 1, "标签序号", cellXF);
        Cell cell2 = cells.Add(1, 2, "顺序号", cellXF);
        Cell cell3 = cells.Add(1, 3, "产品名称", cellXF);
        Cell cell4 = cells.Add(1, 4, "奖项", cellXF);
        Cell cell5 = cells.Add(1, 5, "奖品", cellXF);
        Cell cell6 = cells.Add(1, 6, "截止时间", cellXF);
        Cell cell7 = cells.Add(1, 7, "领取状态", cellXF);

        DataTable db1 = (DataTable) Session["dbj"];

        DataTable dbj = new DataTable();

        if (inputSearchKeyword.Value.Trim().Length > 0 && jscode.Value.Trim().Length > 0)
        {
            dbj =
                db.getLabelCodeLJ60000("DYCode >='" + inputSearchKeyword.Value.Trim() + "' and DYCode<='" +
                                       jscode.Value.Trim() + "' and JXID!=0 and  CompID=" + GetCookieCompID() + " ");
            GridView1.DataSource = dbj;
        }
        else
        {
            dbj = db.getLabelCodeLJ60000("JXID!=0 and  CompID=" + GetCookieCompID() + " ");
            GridView1.DataSource = dbj;
        }

        for (int i = 0; i < dbj.Rows.Count; i++)
        {
            int rowIndex = i + 2;
            int colIndex = i + 1;

            cells.Add(rowIndex, 1, dbj.Rows[i]["LabelCode"].ToString(), cellXF);
            cells.Add(rowIndex, 2, dbj.Rows[i]["DYCode"].ToString(), cellXF);
            cells.Add(rowIndex, 3, bpro.GetList(int.Parse(dbj.Rows[i]["ProID"].ToString())).Products_Name, cellXF);
            cells.Add(rowIndex, 4, dbj.Rows[i]["JXID"].ToString().Equals("1") ? "红包" : "实物", cellXF);
            cells.Add(rowIndex, 5, dbj.Rows[i]["JpName"].ToString(), cellXF);
            cells.Add(rowIndex, 6, dbj.Rows[i]["JZTime"].ToString().Substring(0, 10), cellXF);
            cells.Add(rowIndex, 7, dbj.Rows[i]["LQFlag"].ToString().Equals("1") ? "已领取" : "未领取", cellXF);
        }
        xls.Send();
    }
}