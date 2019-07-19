using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using org.in2bits.MyXls;
using Wuqi.Webdiyer;
using Color = System.Drawing.Color;
using TJ.DBUtility;

public partial class Admin_TJ_JXInfo : AuthorPage
{
    private readonly BTJ_JXInfo bll = new BTJ_JXInfo();
    private MTJ_JXInfo mod = new MTJ_JXInfo();
    public BTJ_LotteryActivity blotteryactivity = new BTJ_LotteryActivity();
    public BTJ_User buser = new BTJ_User();
    private readonly BTJ_DeployJiangXiangInfo bdeployjx = new BTJ_DeployJiangXiangInfo();
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
            }
            else
            {
                fillgridview();
            }
        }
    }
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_JXInfoAddEdit.aspx?cmd=edit&ID={0}',400,300,'奖项设置')", ID);
        }
        else
        {
            return "";
        }
    }

    private void fillgridview()
    {
        GridView1.DataSource = bll.GetListsByFilterString(ReturnFilterString());
        GridView1.DataBind();
    }
      private const string Filtertemp = "1=1"; 
    private void DisplayData(int pageIndex, int pageSize)
    {
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_JXInfo where " + Filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_JXInfo", Filtertemp, "JxID", "JxID", pageSize);
        GridView1.DataBind();
    }
    

    private string tempstring = "";

    private string ReturnFilterString()
    {
        tempstring = "CompID=" + GetCookieCompID();
        if (inputSearchKeyword.Value.Length > 0)
        {
            tempstring += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
        }
        return tempstring;
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["JxID"].ToString()));
        fillgridview();
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

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["JxID"].ToString()));
        mod.OrderNum = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtOrderNum")).Text.Trim());
        mod.Remarks = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtRemarks")).Text;
        bll.Modify(mod);
        GridView1.EditIndex = -1;
        fillgridview();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
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
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                if (CheckIsBeUsed(GridView1.DataKeys[e.Row.RowIndex]["JxID"].ToString()))
                {
                    e.Row.Cells[6].ForeColor = Color.LightGray;
                    e.Row.Cells[6].Enabled = false;
                }
                else
                {
                    ((LinkButton) e.Row.Cells[6].Controls[0]).Attributes.Add("onclick",
                        "javascript:return confirm('你确定要删除当前记录吗?')");
                }
            }
        }
    }

    private bool CheckIsBeUsed(string JXID)
    {
        return bdeployjx.CheckIsExistByFilterString("JxID=" + JXID);
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    protected void BtnSearch2_Click(object sender, EventArgs e)
    {
        DataTable dt;
        DBClass db = new DBClass();
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            dt =
                db.Getstrbyjxneirong(DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'" +
                                     " and CompID=" + GetCookieCompID());

            //GridView1.DataBind();
        }
        else
        {
            dt = db.Getstrbyjxneirong("CompID=" + GetCookieCompID());
        }
        XlsDocument xls = new XlsDocument();
        xls.FileName = "奖项设置信息" + DateTime.Now.ToString("yyyyMMddHHmmssffff", DateTimeFormatInfo.InvariantInfo);
        xls.SummaryInformation.Author = "兑奖"; //填加xls文件作者信息  
        xls.SummaryInformation.NameOfCreatingApplication = "HSLJ"; //填加xls文件创建程序信息  
        xls.SummaryInformation.LastSavedBy = "兑奖"; //填加xls文件最后保存者信息  
        xls.SummaryInformation.Comments = "Comments"; //填加xls文件作者信息  
        xls.SummaryInformation.Title = "兑奖点信息"; //填加xls文件标题信息  
        //xls.SummaryInformation.Subject = "Subject";//填加文件主题信息  
        xls.DocumentSummaryInformation.Company = "兑奖"; //填加文件公司信息  


        Worksheet sheet = xls.Workbook.Worksheets.Add("Sheet1"); //状态栏标题名称  
        Cells cells = sheet.Cells;

        //foreach (DataColumn col in dt.Columns)
        //{
        //    Cell cell = cells.Add(1, col.Ordinal + 1, col.ColumnName);
        //Cell cell1 = cells.Add(1, 1, "编号");
        //Cell cell2 = cells.Add(1, 1, "奖项等级");
        Cell cell3 = cells.Add(1, 1, "奖项名称");
        Cell cell4 = cells.Add(1, 2, "奖项内容");
        //Cell cell5 = cells.Add(1, 4, "布奖人");
        // Cell cell6 = cells.Add(1, 5, "审核人");
        Cell cell7 = cells.Add(1, 3, "创建时间");
        //    cell.Font.FontFamily = FontFamilies.Roman; //字体  
        //    cell.Font.Bold = true;  //字体为粗体    

        //}  

        #region 填充内容

        XF dateStyle = xls.NewXF();
        dateStyle.Format = "yyyy-mm-dd";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            for (int j = 0; j < dt.Columns.Count - 1; j++)
            {
                //sheet.Cells.Add(i + 2, j + 1,dt.Rows[i][j].ToString());
                int rowIndex = i + 2;
                int colIndex = j + 1;
                //string dr = GridView1.Rows[i].Cells[j].ToString();
                string drValue = dt.Rows[i][j].ToString();
                cells.Add(rowIndex, colIndex, drValue);
                //switch (dt.Rows[i][j].GetType().ToString())
                //{
                //    case "System.String"://字符串类型  
                //        cells.Add(rowIndex, colIndex, drValue);
                //        break;
                //    case "System.DateTime"://日期类型  
                //        //DateTime dateV;
                //        //DateTime.TryParse(drValue, out dateV);
                //        //cells.Add(rowIndex, colIndex, dateV, dateStyle);
                //        cells.Add(rowIndex, colIndex, drValue);
                //        break;
                //    case "System.Boolean"://布尔型  
                //        bool boolV = false;
                //        bool.TryParse(drValue, out boolV);
                //        cells.Add(rowIndex, colIndex, boolV);
                //        break;
                //    case "System.Int16"://整型  
                //    case "System.Int32":
                //    case "System.Int64":
                //    case "System.Byte":
                //        int intV = 0;
                //        int.TryParse(drValue, out intV);
                //        cells.Add(rowIndex, colIndex, intV);
                //        break;
                //    case "System.Decimal"://浮点型  
                //    case "System.Double":
                //        double doubV = 0;
                //        double.TryParse(drValue, out doubV);
                //        cells.Add(rowIndex, colIndex, doubV);
                //        break;
                //    case "System.DBNull"://空值处理  
                //        cells.Add(rowIndex, colIndex, null);
                //        break;
                //    default:
                //        cells.Add(rowIndex, colIndex, null);
                //        break;
                //}
            }
        }

        #endregion

        //Server.MapPath("~/DC");
        // xls.Save(Server.MapPath("~/DC"));
        xls.Send();
        Response.Write("<script>alert('导出数据成功！')</script>");
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}