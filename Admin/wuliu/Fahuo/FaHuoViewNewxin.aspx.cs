using System;
using System.Globalization;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using TJ.BLL;
using commonlib;
using System.Data.SqlClient;
using System.Data;
using org.in2bits.MyXls;

public partial class Admin_wuliu_Fahuo_FaHuoViewNewxin : AuthorPage
{
    public BTJ_RegisterCompanys bagent = new BTJ_RegisterCompanys();
    BTB_Products_Infor bpro = new BTB_Products_Infor();
    BTB_StoreHouse bstorehouse = new BTB_StoreHouse();
    BTB_ProductAuthorForAgent bproductforagent = new BTB_ProductAuthorForAgent();
    BTJ_User buser = new BTJ_User();
    CommonFunWL comwl = new CommonFunWL();
    SqlConnection sqlconcom = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBox_RukuDateBegin.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            TextBox_RukuDateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
            FillDDL();
            string sqlstring = "SELECT fh.FHPiCi,fh.FHDate,fh.AgentID,fh.XiangNumber,pr.Products_Name,st.StoreHouseName,fh.FHUserID FROM [TB_FaHuoInfo_" + GetCookieCompID() + "] fh,TB_Products_Infor pr,TB_StoreHouse st where   fh.FHTypeID=3 and fh.ProID=pr.Infor_ID and fh.STID=st.STID";
            sqlstring += " and fh.FHDate between '" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + "' and '" + TextBox_RukuDateEnd.Text + "'order by fh.FHDate desc ";
            if (ComboBox_DaiLiShangID.SelectedValue != "0" && ComboBox_DaiLiShangID.SelectedValue != null)
            {
                sqlstring += " and fh.AgentID=" + ComboBox_DaiLiShangID.SelectedValue;
            }
            if (ComboBox_ProInfo.SelectedValue != "0" && ComboBox_ProInfo.SelectedValue != null)
            {
                sqlstring += " and fh.ProID=" + ComboBox_ProInfo.SelectedValue;
            }
            if (ComboBox_StoreHouse.SelectedValue != "0" && ComboBox_StoreHouse.SelectedValue != null)
            {
                sqlstring += " and fh.STID=" + ComboBox_StoreHouse.SelectedValue;
            }
            SqlDataAdapter sda = new SqlDataAdapter(sqlstring, sqlconcom);
            DataTable dttemp = new DataTable();
            sda.Fill(dttemp);
            GridView_RukuInfo.DataSource = dttemp;
            GridView_RukuInfo.DataBind();
        }
    }
    protected void Button_Search_Click(object sender, EventArgs e)
    {
        string sqlstring = "SELECT fh.FHPiCi,fh.FHDate,fh.AgentID,fh.XiangNumber,pr.Products_Name,st.StoreHouseName,fh.FHUserID FROM [TB_FaHuoInfo_" + GetCookieCompID() + "] fh,TB_Products_Infor pr,TB_StoreHouse st where   fh.FHTypeID=3 and fh.ProID=pr.Infor_ID and fh.STID=st.STID";
        sqlstring += " and fh.FHDate >='" + TextBox_RukuDateBegin.Text + "' and  fh.FHDate < '" + Convert.ToDateTime(TextBox_RukuDateEnd.Text).AddDays(1) + "'";
        if (ComboBox_DaiLiShangID.SelectedValue != "0" && ComboBox_DaiLiShangID.SelectedValue != null)
        {
            sqlstring += " and fh.AgentID=" + ComboBox_DaiLiShangID.SelectedValue;
        }
        if (ComboBox_ProInfo.SelectedValue != "0" && ComboBox_ProInfo.SelectedValue != null)
        {
            sqlstring += " and fh.ProID=" + ComboBox_ProInfo.SelectedValue;
        }
        if (ComboBox_StoreHouse.SelectedValue != "0" && ComboBox_StoreHouse.SelectedValue != null)
        {
            sqlstring += " and fh.STID=" + ComboBox_StoreHouse.SelectedValue;
        }
        SqlDataAdapter sda = new SqlDataAdapter(sqlstring + " order by fh.FHDate desc", sqlconcom);
        DataTable dttemp = new DataTable();
        sda.Fill(dttemp);
        GridView_RukuInfo.DataSource = dttemp;
        GridView_RukuInfo.DataBind();
    }
    protected void GridView_RukuInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    private void FillDDL()
    {
        string tempagentidstring = comwl.GetAgentIDStringByCompID(GetCookieCompID());
        if (tempagentidstring.Length > 0)
        {
            ComboBox_DaiLiShangID.DataSource = bagent.GetListsByFilterString("CompID in (" + tempagentidstring + ")");
            ComboBox_DaiLiShangID.DataBind();
        }
        //ComboBox_DaiLiShangID.DataSource = bagent.GetListsByFilterString("ParentID="+GetCookieCompID()+" and CompTypeID="+DAConfig.CompTypeIDJingXiaoShang);
        //ComboBox_DaiLiShangID.DataBind();
        if (GetCookieCompTypeID() == DAConfig.CompTypeIDJingXiaoShang.ToString().Trim())
        {

            ComboBox_ProInfo.DataSource = comwl.ReturnGetAgentAuthorProductInfo(GetCookieCompID());
        }
        else
        {
            ComboBox_ProInfo.DataSource = bpro.GetListsByFilterString("CompID=" + GetCookieCompID());
        }
        ComboBox_ProInfo.DataBind();
        ComboBox_StoreHouse.DataSource = bstorehouse.GetListsByFilterString("CompID=" + GetCookieCompID());
        ComboBox_StoreHouse.DataBind();
    }
    protected void GridView_RukuInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label labelfhr = (Label)e.Row.FindControl("LabelFHR");
            labelfhr.Text = buser.GetList(int.Parse(labelfhr.Text)).LoginName;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label Label_jianshu_ft = (Label)e.Row.FindControl("Label_jianshu_ft");
            int jianshu = 0;
            foreach (GridViewRow gr in GridView_RukuInfo.Rows)
            {
                jianshu += Convert.ToInt32(((Label)gr.FindControl("Label_jianshu")).Text);
            }
            Label_jianshu_ft.Text = jianshu.ToString();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        DBClass db = new DBClass();
        XlsDocument xls = new XlsDocument();
        xls.FileName = "发货信息表" + DateTime.Now.ToString("yyyyMMddHHmmssffff", DateTimeFormatInfo.InvariantInfo);
        xls.SummaryInformation.Author = "西凤375"; //填加xls文件作者信息  
        xls.SummaryInformation.NameOfCreatingApplication = "375"; //填加xls文件创建程序信息  
        xls.SummaryInformation.LastSavedBy = "西凤375"; //填加xls文件最后保存者信息  
        xls.SummaryInformation.Comments = "Comments"; //填加xls文件作者信息  
        xls.SummaryInformation.Title = "发货信息表"; //填加xls文件标题信息  
                                                //xls.SummaryInformation.Subject = "Subject";//填加文件主题信息  
        xls.DocumentSummaryInformation.Company = "西凤375";//填加文件公司信息  


        Worksheet sheet = xls.Workbook.Worksheets.Add("Sheet1");//状态栏标题名称  
        ColumnInfo cinfo = new ColumnInfo(xls, sheet);

        cinfo.Collapsed = true;

        //设置列的范围 如 0列-10列

        cinfo.ColumnIndexStart = 0;//列开始

        cinfo.ColumnIndexEnd = 8;//列结束

        cinfo.Collapsed = true;
        cinfo.Width = 80 * 60;
        sheet.AddColumnInfo(cinfo);

        XF cellXF = xls.NewXF();

        cellXF.VerticalAlignment = VerticalAlignments.Centered;

        cellXF.HorizontalAlignment = HorizontalAlignments.Centered;

        cellXF.Font.Height = 18 * 12;

        //cellXF.Font.Bold = true;

        //cellXF.Pattern = 1;//设定单元格填充风格。如果设定为0，则是纯色填充
        cellXF.UseBackground = true;

        cellXF.PatternBackgroundColor = Colors.Red;//填充的背景底色

        cellXF.PatternColor = Colors.Red;//设定填充线条的颜色


        Cells cells = sheet.Cells;

        Cell cell2 = cells.Add(1, 1, "批次", cellXF);
        Cell cell3 = cells.Add(1, 2, "发货日期", cellXF);
        Cell cell4 = cells.Add(1, 3, "经销商", cellXF);
        Cell cell6 = cells.Add(1, 4, "发货数量", cellXF);
        Cell cell7 = cells.Add(1, 5, "产品", cellXF);
        Cell cell8 = cells.Add(1, 6, "发货人", cellXF);



        //cell.Font.FontFamily = FontFamilies.Roman; //字体  
        //cell.Font.Bold = true;  //字体为粗体    

        //}  
        #region 填充内容
        //XF dateStyle = xls.NewXF();
        //dateStyle.Format = "yyyy-mm-dd hh-mm-ss";
        if (GridView_RukuInfo.Rows.Count > 0)
        {
            DataTable dt = (DataTable)GridView_RukuInfo.DataSource;
            for (int i = 0; i < GridView_RukuInfo.Rows.Count; i++)
            {
                for (int j = 1; j < GridView_RukuInfo.Columns.Count; j++)
                {
                    //sheet.Cells.Add(i + 2, j + 1,dt.Rows[i][j].ToString());
                    int rowIndex = i + 2;
                    int colIndex = j;
                    //string dr = GridView1.Rows[i].Cells[j].ToString();


                    string agentname = ((Label)GridView_RukuInfo.Rows[i].Cells[j].FindControl("Label1")).Text;
                    string proname = ((Label)GridView_RukuInfo.Rows[i].Cells[j].FindControl("labelAgentID")).Text;
                    string fahuokucun = ((Label)GridView_RukuInfo.Rows[i].Cells[j].FindControl("Label3")).Text;
                     string fhren = ((Label)GridView_RukuInfo.Rows[i].Cells[j].FindControl("Label_jianshu")).Text;
                    string fhnum = ((Label)GridView_RukuInfo.Rows[i].Cells[j].FindControl("Label2")).Text;
                    string dlyfnum = ((Label)GridView_RukuInfo.Rows[i].Cells[j].FindControl("LabelFHR")).Text;
                   // string dlsnum = ((Label)GridView_RukuInfo.Rows[i].Cells[j].FindControl("Label1")).Text;

                    cells.Add(rowIndex, 1, agentname);
                    cells.Add(rowIndex, 3, proname);
                    cells.Add(rowIndex, 2, fahuokucun);
                     cells.Add(rowIndex, 4, fhren);
                    cells.Add(rowIndex, 5, fhnum);
                    cells.Add(rowIndex, 6, dlyfnum);
                   // cells.Add(rowIndex, 6, int.Parse(dlsnum));

                }
            }

            #endregion
            //Server.MapPath("~/DC");
            xls.Send();
            Response.Write("<script>alert('导出数据成功！')</script>");
        }
        else
        {
            Response.Write("");
        }

    }
}