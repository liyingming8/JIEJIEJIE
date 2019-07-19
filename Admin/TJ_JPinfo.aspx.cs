using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using org.in2bits.MyXls;

public partial class Admin_TJ_JPinfo : AuthorPage
{
    private readonly BTJ_JPinfo bll = new BTJ_JPinfo();
    private MTJ_JPinfo mod = new MTJ_JPinfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgridview();
        }
    }

    private void fillgridview()
    {
        GridView1.DataSource = bll.GetListsByFilterString(ReturnFilterString());
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
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["JPID"].ToString()));
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
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["JPID"].ToString()));
        //mod.JxID = Convert.ToInt32(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtJxID")).Text.Trim());
        mod.DjdName = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtDjdName")).Text.Trim();
        mod.JpName = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtJpName")).Text.Trim();
        mod.PFCount = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtPFCount")).Text.Trim());
        mod.DHCount = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtDHCount")).Text.Trim());
        mod.HXCount = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtHXCount")).Text.Trim());
        mod.SYCount = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtSYCount")).Text.Trim());
        mod.CreatTime =
            Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCreatTime")).Text.Trim());
        //mod.compID = Convert.ToInt32(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtcompID")).Text.Trim());
        //mod.DelFlag = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtDelFlag")).Text.Trim();
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
        ////如果是绑定数据行 
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
        //    {
        //        ((LinkButton)e.Row.Cells[12].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
        //    }
        //}
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
            dt = db.GetJPinfoByStr(DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'");

            //GridView1.DataBind();
        }
        else
        {
            dt = db.GetJPinfo();
        }
        XlsDocument xls = new XlsDocument();
        xls.FileName = "奖品信息" + DateTime.Now.ToString("yyyyMMddHHmmssffff", DateTimeFormatInfo.InvariantInfo);
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
        Cell cell2 = cells.Add(1, 1, "兑奖点");
        Cell cell3 = cells.Add(1, 2, "奖品名称");
        Cell cell4 = cells.Add(1, 3, "配发数量");
        Cell cell5 = cells.Add(1, 4, "兑换数量");
        Cell cell6 = cells.Add(1, 5, "核销数量");
        Cell cell7 = cells.Add(1, 6, "剩余数量");
        Cell cell8 = cells.Add(1, 7, "创建时间");

        //    cell.Font.FontFamily = FontFamilies.Roman; //字体  
        //    cell.Font.Bold = true;  //字体为粗体    

        //}  

        #region 填充内容

        XF dateStyle = xls.NewXF();
        dateStyle.Format = "yyyy-mm-dd";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            for (int j = 2; j < dt.Columns.Count - 2; j++)
            {
                //sheet.Cells.Add(i + 2, j + 1,dt.Rows[i][j].ToString());
                int rowIndex = i + 2;
                int colIndex = j - 1;
                //string dr = GridView1.Rows[i].Cells[j].ToString();
                string drValue = dt.Rows[i][j].ToString();
                cells.Add(rowIndex, colIndex, drValue);
                switch (dt.Rows[i][j].GetType().ToString())
                {
                    case "System.String": //字符串类型  
                        cells.Add(rowIndex, colIndex, drValue);
                        break;
                    case "System.DateTime": //日期类型  
                        //DateTime dateV;
                        //DateTime.TryParse(drValue, out dateV);
                        //cells.Add(rowIndex, colIndex, dateV, dateStyle);
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
}