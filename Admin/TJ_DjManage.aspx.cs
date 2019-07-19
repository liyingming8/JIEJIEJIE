using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using org.in2bits.MyXls;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_TJ_DjManage : AuthorPage
{
    private readonly BTJ_DjManage bll = new BTJ_DjManage();
    private MTJ_DjManage mod = new MTJ_DjManage();
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            _currentindex = 1;
            AspNetPager1.CurrentPageIndex = _currentindex;
            DisplayData(1, AspNetPager1.PageSize);

            //fillgridview();
        }
    }

    //private void fillgridview()
    //{
    //    if (inputSearchKeyword.Value.Trim().Length > 0)
    //    {
    //        GridView1.DataSource =
    //            bll.GetListsByFilterString("CompID=" + GetCookieCompID() + " and " + DDLField.SelectedValue + " like '%" +
    //                                       inputSearchKeyword.Value.Trim() + "%'");
    //    }
    //    else
    //    {
    //        GridView1.DataSource = bll.GetListsByFilterString("CompID=" + GetCookieCompID());
    //    }
    //    GridView1.DataBind();
    //}
    //private const string Filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        string comp = GetCookieCompID();
        if (comp == "1")
        {
            string Filtertemp = "1=1";
            // string Filtertemp = "CompID =1";
            if (inputSearchKeyword.Value.Trim().Length > 0)
            {

                Filtertemp = " " + DDLField.SelectedValue +
                                                 " like '%" + inputSearchKeyword.Value.Trim() + "%'";
                //Filtertemp = "CompID = 1 and " + DDLField.SelectedValue +
                //                              " like '%" + inputSearchKeyword.Value.Trim() + "%'";//可以查询全部的
            }

            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_DjManage where " + Filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_DjManage", Filtertemp, "ZjID", "ZjID", pageSize);
            GridView1.DataBind();
        }
        else
        {
            string Filtertemp = "CompID=" + comp;
            if (inputSearchKeyword.Value.Trim().Length > 0)
            {

                Filtertemp = "CompID =" + GetCookieCompID() + " and " + DDLField.SelectedValue +
                                             " like '%" + inputSearchKeyword.Value.Trim() + "%'";
                //GridView1.DataSource =
                //    bll.GetListsByFilterString("CompID =" + GetCookieCompID() + " and " + DDLField.SelectedValue +
                //                               " like '%" + inputSearchKeyword.Value.Trim() + "%'");
            }
            //else
            //{
            //    GridView1.DataSource = bll.GetListsByFilterString("CompID =" + GetCookieCompID());
            //}
            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_DjManage where " + Filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_DjManage", Filtertemp, "ZjID", "ZjID", pageSize);
            GridView1.DataBind();
        }
        //AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_DjManage where " + Filtertemp, null).Rows[0][0].ToString());
        //GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_DjManage", Filtertemp, "ZjID", "ZjID", pageSize);
        //GridView1.DataBind();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["ZjID"].ToString()));
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["ZjID"].ToString()));
        mod.ZjNumber = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtZjNumber")).Text.Trim();
        mod.ZjTime = Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtZjTime")).Text.Trim());
        mod.JxName = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtJxName")).Text.Trim();
        mod.ZjPhone = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtZjPhone")).Text.Trim();
        mod.CpXS = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCpXS")).Text.Trim();
        mod.CpCX = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCpCX")).Text.Trim();
        mod.DjdName = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtDjdName")).Text.Trim();
        mod.DhTime = Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtDhTime")).Text.Trim());
        mod.DjFlag = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtDjFlag")).Text.Trim();
        mod.LjTime = Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtLjTime")).Text.Trim());
        mod.ZjyzCode = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtZjyzCode")).Text.Trim();
        //mod.DelFlag = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtDelFlag")).Text.Trim();
        bll.Modify(mod);
        GridView1.EditIndex = -1;
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        ////如果是绑定数据行 
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
        //    {
        //        ((LinkButton)e.Row.Cells[14].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
        //    }
        //}
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        //fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void BtnSearch2_Click(object sender, EventArgs e)
    {
        DataTable dt;
        DBClass db = new DBClass();
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            dt = db.GetDjManageByStr(DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'");

            //GridView1.DataBind();
        }
        else
        {
            dt = db.GetDjManage();
        }
        //===wyz-20170921===================================================
        if (dt==null || dt.Rows.Count <=0)
        {
            Response.Write("<script>alert('无数据信息可导出！')</script>");
            return;
        }
        //==================================================================

        XlsDocument xls = new XlsDocument();
        xls.FileName = "兑奖管理信息" + DateTime.Now.ToString("yyyyMMddHHmmssffff", DateTimeFormatInfo.InvariantInfo);
        xls.SummaryInformation.Author = "华山论剑"; //填加xls文件作者信息  
        xls.SummaryInformation.NameOfCreatingApplication = "HSLJ"; //填加xls文件创建程序信息  
        xls.SummaryInformation.LastSavedBy = "华山论剑"; //填加xls文件最后保存者信息  
        xls.SummaryInformation.Comments = "Comments"; //填加xls文件作者信息  
        xls.SummaryInformation.Title = "兑奖点信息"; //填加xls文件标题信息  
        //xls.SummaryInformation.Subject = "Subject";//填加文件主题信息  
        xls.DocumentSummaryInformation.Company = "华山论剑"; //填加文件公司信息  


        Worksheet sheet = xls.Workbook.Worksheets.Add("Sheet1"); //状态栏标题名称  
        Cells cells = sheet.Cells;

        //foreach (DataColumn col in dt.Columns)
        //{
        //    Cell cell = cells.Add(1, col.Ordinal + 1, col.ColumnName);
        //Cell cell1 = cells.Add(1, 1, "编号");
        Cell cell2 = cells.Add(1, 1, "中奖标签号码");
        Cell cell3 = cells.Add(1, 2, "中奖时间");
        Cell cell4 = cells.Add(1, 3, "奖项名称");
        Cell cell5 = cells.Add(1, 4, "中奖手机号");
        Cell cell6 = cells.Add(1, 5, "产品销售地区");
        Cell cell7 = cells.Add(1, 6, "产品查询地区");
        Cell cell8 = cells.Add(1, 7, "兑奖点名称");
        Cell cell9 = cells.Add(1, 8, "兑换时间");
        Cell cell10 = cells.Add(1, 9, "是否领奖");
        Cell cell11 = cells.Add(1, 10, "领奖时间");
        Cell cell12 = cells.Add(1, 11, "中奖验证码");
        //    cell.Font.FontFamily = FontFamilies.Roman; //字体  
        //    cell.Font.Bold = true;  //字体为粗体    

        //}  

        #region 填充内容

        XF dateStyle = xls.NewXF();
        dateStyle.Format = "yyyy-mm-dd";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            for (int j = 1; j < dt.Columns.Count - 1; j++)
            {
                //sheet.Cells.Add(i + 2, j + 1,dt.Rows[i][j].ToString());
                int rowIndex = i + 2;
                int colIndex = j;

                //string dr = GridView1.Rows[i].Cells[j].ToString();
                string drValue = dt.Rows[i][j].ToString();
                cells.Add(rowIndex, colIndex, drValue);

                if (dt.Rows[i][9].ToString().Equals("1"))
                {
                    ////cell10.Rows[rowIndex][9] = "是";
                    cells.Add(rowIndex, 9, "是");
                }
                else
                {
                    cells.Add(rowIndex, 9, "未领奖");
                }
                switch (dt.Rows[i][j].GetType().ToString())
                {
                    case "System.String": //字符串类型  
                        cells.Add(rowIndex, colIndex, drValue);
                        break;
                    case "System.DateTime": //日期类型  
                        //    DateTime dateV;
                        //    DateTime.TryParse(drValue, out dateV);
                        //    cells.Add(rowIndex, colIndex, dateV, dateStyle);
                        //    break;
                        cells.Add(rowIndex, colIndex, drValue);
                        break;
                    case "System.Boolean": //布尔型  
                        bool boolV = false;
                        bool.TryParse(drValue, out boolV);
                        cells.Add(rowIndex, colIndex, boolV);
                        break;
                    case "System.Int16": //整型  
                    case "System.Int32":
                    case "System.Int64":
                    case "System.Byte":
                        int intV = 0;
                        int.TryParse(drValue, out intV);
                        cells.Add(rowIndex, colIndex, intV);
                        break;
                    case "System.Decimal": //浮点型  
                    case "System.Double":
                        double doubV = 0;
                        double.TryParse(drValue, out doubV);
                        cells.Add(rowIndex, colIndex, doubV);
                        break;
                    case "System.DBNull": //空值处理  
                        cells.Add(rowIndex, colIndex, null);
                        break;
                    default:
                        cells.Add(rowIndex, colIndex, null);
                        break;
                }
            }
        }

        #endregion

        //Server.MapPath("~/DC");
        xls.Save(Server.MapPath("~/DC"));
        Response.Write("<script>alert('导出数据成功！')</script>");
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}