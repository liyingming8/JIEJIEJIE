using System;
using System.Data;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
using org.in2bits.MyXls;
using Wuqi.Webdiyer;

public partial class TB_WXHBZJ : AuthorPage
{
    BTJ_User bll = new BTJ_User();
    MTJ_User mod = new MTJ_User();
    BTJ_RoleInfo bllrole = new BTJ_RoleInfo();
    BTJ_RegisterCompanys blrcompany = new BTJ_RegisterCompanys();
    CommonFun comfun = new CommonFun();
    CommonFunWL comwl = new CommonFunWL();
    DBClass db = new DBClass();
    public BTB_Products_Infor bpro = new BTB_Products_Infor();
    TabExecute _tab = new TabExecute();
    private int pageindex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pageindex = 1;
            txtEndDate.Text = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM--dd");
            DisplayData(pageindex, AspNetPager1.PageSize);
            //fillgridview();
        }
    }


    private string _filtertemp = "";
    private void DisplayData(int pageIndex, int pageSize)
    {
        _filtertemp = ReturnFilterString();
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(ID) from TJ_DKWXJPinfo where " + _filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_DKWXJPinfo", _filtertemp, "LQtime desc", "ID", pageSize);
        GridView1.DataBind();
    }

    private void fillgridview()
    {
        DataTable dzj = db.GetDKWXJPinfo(ReturnFilterString());

        Session["dzj"] = dzj;
        GridView1.DataSource = dzj;
        GridView1.DataBind();
    }

    public string proname(string labelcode)
    {
        DataTable dlpro= db.getLabelCodeLJ("CompID="+GetCookieCompID()+" and LabelCode='" + labelcode + "'");
        if (dlpro.Rows.Count > 0)
        {
            string pid = dlpro.Rows[0]["ProID"].ToString();
            return bpro.GetList(int.Parse(pid)).Products_Name;
        }
        else {
            return "";
        
        } 
    }

    string _tempstring = "";
    private string ReturnFilterString()
    {
        _tempstring = "CompID=" + GetCookieCompID();
        if (inputSearchKeyword.Value.Length > 0)
        {
            _tempstring += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
            if (txtStartDate.Text.Length > 0)
            {
                _tempstring += "and LQtime>'" + txtStartDate.Text + "' and LQtime<'" + txtEndDate.Text + "'";  
            }
        } 
        return _tempstring;
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

        xls.FileName = "中奖信息统计";

        Worksheet sheet = xls.Workbook.Worksheets.Add("Sheet1");//状态栏标题名称  
        ColumnInfo cinfo = new ColumnInfo(xls, sheet);

        cinfo.Collapsed = true;

        //设置列的范围 如 0列-10列

        cinfo.ColumnIndexStart = 0;//列开始

        cinfo.ColumnIndexEnd = 7;//列结束

        cinfo.Collapsed = true;
        cinfo.Width = 80 * 60;
        sheet.AddColumnInfo(cinfo);

        XF cellXF = xls.NewXF();

        cellXF.VerticalAlignment = VerticalAlignments.Centered;

        cellXF.HorizontalAlignment = HorizontalAlignments.Centered;

        cellXF.Font.Height = 18 * 12;

        Cells cells = sheet.Cells;

        //Cell cell1 = cells.Add(1, 1, "编号");
        Cell cell1 = cells.Add(1, 1, "标签序号", cellXF);
        Cell cell2 = cells.Add(1, 2, "产品名称", cellXF);
        Cell cell3 = cells.Add(1, 3, "奖项", cellXF);
        Cell cell4 = cells.Add(1, 4, "奖品", cellXF);
        Cell cell5 = cells.Add(1, 5, "中奖人", cellXF);
        Cell cell6 = cells.Add(1, 6, "领取时间", cellXF);

        DataTable db = (DataTable)Session["dzj"];

        int count = db.Rows.Count;

        if (count > 62000)
        {
            count = 62000;
        }

        for (int i = 0; i < count; i++)
        {
            int rowIndex = i + 2;
            int colIndex = i + 1;
            cells.Add(rowIndex, 1, db.Rows[i]["BoxLabel"].ToString(), cellXF);
            cells.Add(rowIndex, 2, "高度柔和", cellXF);
            cells.Add(rowIndex, 3, db.Rows[i]["JXname"].ToString(), cellXF);
            cells.Add(rowIndex, 4, db.Rows[i]["JPInfo"].ToString(), cellXF);
            cells.Add(rowIndex, 5, db.Rows[i]["WXname"].ToString(), cellXF);
            cells.Add(rowIndex, 6, db.Rows[i]["LQtime"].ToString().Substring(0, 10), cellXF);
        }
        xls.Send();
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {

    }
}
