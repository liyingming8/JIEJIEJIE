using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using System.Data;
using System.Collections.Generic;
using org.in2bits.MyXls;


//using Aspose.Cells ;
public partial class Admin_TJ_ZJLabelCodesmallInfoJGXQ : AuthorPage
{
    private readonly BTJ_ZJLabelCodesmallInfo bll = new BTJ_ZJLabelCodesmallInfo();
    private readonly DBClass db = new DBClass();
    public BTJ_JXInfo bjxinfo = new BTJ_JXInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtEndDate.Text = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd"); //获取今天的时间显示在文本框中

            txtStartDate.Text = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");
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


    private string tempstring = "";

    private string ReturnFilterString()
    {
        tempstring = "CompID=" + GetCookieCompID();
        // tempstring += "and SaleArea between '2017-03-14' and '2017-03-17'";
        tempstring += " and  BJShiJian  between ' " + txtStartDate.Text + "' and '" +
                      Convert.ToDateTime(txtEndDate.Text).AddDays(1) + "'"; //在转换时遇到了错误
        // tempstring += "and SaleArea between '" + txtStartDate.Text +"' and '"+txtEndDate.Text+"'";
        if (inputSearchKeyword.Value.Length > 0)
        {
            if (DDLField.SelectedValue == "BJShiJian")
            {
                IList<MTJ_ZJLabelCodesmallInfo> ipro =
                    bll.GetListsByFilterString("BJShiJian=" + inputSearchKeyword.Value); //写入文本框里的值
                if (ipro.Count > 0)
                {
                    tempstring += " and " + DDLField.SelectedValue + " ='" + ipro[0].LabelCode + "'";
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
                tempstring += "and BJShiJian >'" + txtStartDate.Text + "' and BJShiJian <'" + txtEndDate.Text + "'";
            }
        }
        tempstring += "order by BJShiJian desc";

        return tempstring; //以上是实现模糊查询的条件
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["ZJCDID"].ToString()));
        fillgridview();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
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


    private void fillgridview()
    {
        DataTable dzj = db.getjcsBuJiang(ReturnFilterString());
        Session["dzj"] = dzj;
        GridView1.DataSource = dzj;
        GridView1.DataBind();
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
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    //protected void BtnSearch1_Click(object sender, EventArgs e)
    //{
    //    fillgridview();
    //}

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
        Cell cell1 = cells.Add(1, 1, "二维码连接序号", cellXF);
        Cell cell2 = cells.Add(1, 2, "标签序号", cellXF);
        Cell cell3 = cells.Add(1, 3, "奖项", cellXF);
        Cell cell4 = cells.Add(1, 4, "布奖时间", cellXF);
        //Cell cell5 = cells.Add(1, 5, "中奖人", cellXF);
        //Cell cell6 = cells.Add(1, 6, "领取时间", cellXF);//表头的名称

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
            cells.Add(rowIndex, 1, db.Rows[i]["labelcode"].ToString(), cellXF);
            cells.Add(rowIndex, 2, db.Rows[i]["BoxLabelCode"].ToString(), cellXF);
            string name = bjxinfo.GetList(Convert.ToInt32(db.Rows[i]["JXID"])).JxName;
            cells.Add(rowIndex, 3, name, cellXF);
            cells.Add(rowIndex, 4, db.Rows[i]["BJShiJian"].ToString(), cellXF);
            //cells.Add(rowIndex, 5, db.Rows[i]["WXname"].ToString(), cellXF);
            //cells.Add(rowIndex, 6, db.Rows[i]["LQtime"].ToString().Substring(0, 10), cellXF);
        }
        xls.Send();
    }
}