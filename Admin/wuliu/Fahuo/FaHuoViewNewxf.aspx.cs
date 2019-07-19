using System;
using System.Globalization;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using TJ.BLL;
using commonlib;
using System.Data.SqlClient;
using System.Data;
using org.in2bits.MyXls;
using Wuqi.Webdiyer;
using TJ.DBUtility; 
using Color = System.Drawing.Color;

public partial class Admin_wuliu_Fahuo_FaHuoViewNewxf : AuthorPage
{
    readonly TabExecutewuliu _tab = new TabExecutewuliu();
    readonly  TabExecute tabyin = new TabExecute();
    private int _currentindex = 1;
    public BTJ_RegisterCompanys bagent = new BTJ_RegisterCompanys(); 
    public BTB_Products_Infor bpro = new BTB_Products_Infor();
    public BTB_StoreHouse bstorehouse = new BTB_StoreHouse(); 
    //CommonFunWL comwl = new CommonFunWL();
    SqlConnection sqlconcom = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString()); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBox_RukuDateBegin.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            TextBox_RukuDateEnd.Value = DateTime.Now.ToString("yyyy-MM-dd");
            FillDDL();
            _currentindex = 1;
            AspNetPager1.CurrentPageIndex = _currentindex;
            GetFaHuoInfo(1, AspNetPager1.PageSize);
        }
    }
    private void FillDDL()
    {
        ComboBox_JXS.DataSource = tabyin.ExecuteQuery("select CompID,CompName from TJ_RegisterCompanys where ParentID="+GetCookieCompID(),null);
        ComboBox_JXS.DataBind(); 
    }

    private void GetFaHuoInfo(int pageIndex, int pageSize)
    {
        string strnew = string.Empty;
        string filterstring = string.Empty;
        if (string.IsNullOrEmpty(ComboBox_JXS.SelectedValue) || ComboBox_JXS.SelectedValue.Equals("0"))
        {
            strnew = "SELECT count(FHID) from [TB_FaHuoInfo_" + GetCookieCompID() + "]";
            filterstring =
                " where  xiangnumber!=0 and ProID in(select prodid from [TianJianWuLiuWebnew].[dbo].[TB_ProductAuthorForUser ]   where CompID=2 and userid=" + GetCookieUID()+") and " +
                " FHDate between '" + Convert.ToDateTime(TextBox_RukuDateBegin.Value).ToString("yyyy-MM-dd") +
                "' and '" + Convert.ToDateTime(TextBox_RukuDateEnd.Value).AddDays(1).ToString("yyyy-MM-dd") +"'";
        }
        else
        {
            strnew = "SELECT count(FHID) from [TB_FaHuoInfo_" + GetCookieCompID() + "]";
            filterstring = " where AgentID=" + ComboBox_JXS.SelectedValue +
                           " and  xiangnumber!=0 and ProID in(select prodid from [TianJianWuLiuWebnew].[dbo].[TB_ProductAuthorForUser ]   where CompID=2 and userid=" + GetCookieUID()+") and " +
                           " FHDate between '" + Convert.ToDateTime(TextBox_RukuDateBegin.Value).ToString("yyyy-MM-dd") +
                           "' and '" + Convert.ToDateTime(TextBox_RukuDateEnd.Value).AddDays(1).ToString("yyyy-MM-dd") +"'";
        }

        if (!check_all.Checked)
        {
            filterstring += " and (((SELECT sum(receipt_qty) from receipt_normal b where b.fhkey=[TianJianWuLiuWebnew].[dbo].[TB_FaHuoInfo_" + GetCookieCompID()+ "].FHKey)>0) or ((SELECT sum(receipt_qty) from receipt_abnormal b where b.to_fhkey=[TianJianWuLiuWebnew].[dbo].[TB_FaHuoInfo_" + GetCookieCompID() + "].FHKey)>0))";
        }

        string cnt = _tab.ExecuteQueryForSingleValue(strnew + filterstring);
        sqlconcom.Close();
        AspNetPager1.RecordCount = int.Parse(cnt);
        GridView_RukuInfo.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex,
            "[TB_FaHuoInfo_" + GetCookieCompID() + "]  ", filterstring.Substring(7), "fhdate desc", "fhid", pageSize);
        GridView_RukuInfo.DataBind();
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        GetFaHuoInfo(1, AspNetPager1.PageSize);
    } 

    public string XiangXiJiangXiangSheZhiLinkString(string fhkey)
    {
        if (fhkey.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('FaHuoViewNewxf_err.aspx?fhkey={0}',800,700,'异常数据')", Sc.EncryptQueryString(fhkey));
        }
        else
        {
            return "";
        }
    }

    public string ErpOrderInfo(string erpordercode)
    {
        if (erpordercode.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('crm_order_detail.aspx?khddh={0}',700,500,'CRM订单信息')", Sc.EncryptQueryString(erpordercode));
        }
        else
        {
            return "";
        }
    }

    protected void GridView_RukuInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        { 
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c"); 
            var dataKey = GridView_RukuInfo.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
            {
                HyperLink hplinkOrdercode = (HyperLink)e.Row.FindControl("hplink_ordercode");
                if (string.IsNullOrEmpty(dataKey["KhDDH"].ToString()))
                {
                    hplinkOrdercode.Text = "——";
                    //hplink_ordercode.Attributes.Add("onclick", ErpOrderInfo("sx-2018-0090")); 
                }
                else
                {
                    hplinkOrdercode.Attributes.Add("onclick", ErpOrderInfo(dataKey["KhDDH"].ToString()));
                }
                Label labelJxsconfirmnum = (Label)e.Row.FindControl("Label_jxsconfirmnum");
                Label labelJianshu = (Label) e.Row.FindControl("Label_jianshu");
                if (int.Parse(labelJxsconfirmnum.Text).Equals(0))
                {
                    e.Row.ForeColor=Color.Red; 
                }
                else
                {
                    if (int.Parse(labelJxsconfirmnum.Text) < int.Parse(labelJianshu.Text))
                    {
                        e.Row.ForeColor = Color.Orange;
                    }
                    else
                    {
                        e.Row.ForeColor = Color.Green;
                    }
                }
                ((HyperLink)e.Row.FindControl("hlinkerror")).Attributes.Add("onclick", XiangXiJiangXiangSheZhiLinkString(dataKey["fhkey"].ToString()));
            }
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

    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        GetFaHuoInfo(e.NewPageIndex, AspNetPager1.PageSize); 
    }

    public string agentcrim(string fhkey)
    {
        string sqlstring = "SELECT sum(receipt_qty) from receipt_normal where fhkey='" + fhkey + "'";

        var sda = new SqlDataAdapter(sqlstring, sqlconcom);
        var dttemp = new DataTable();
        sda.Fill(dttemp);
        dttemp.Dispose();
        sda.Dispose();
        if (string.IsNullOrEmpty(dttemp.Rows[0][0].ToString()))
        {
            return "0";
        }
        else
        { return dttemp.Rows[0][0].ToString(); } 
    }
    public string error(string fhkey)
    {
        string sqlstring = "SELECT sum(receipt_qty) from receipt_abnormal where    to_fhkey='" + fhkey + "'";

        var sda = new SqlDataAdapter(sqlstring, sqlconcom);
        var dttemp = new DataTable();
        sda.Fill(dttemp);
        dttemp.Dispose();
        sda.Dispose();
        if (string.IsNullOrEmpty(dttemp.Rows[0][0].ToString()))
        {
            return "0";
        }
        else
        { return dttemp.Rows[0][0].ToString(); }


    }

}