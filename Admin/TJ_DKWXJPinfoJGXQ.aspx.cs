using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using System.Data.SqlClient; 
using commonlib;
using System.IO;
using org.in2bits.MyXls;

public partial class TJ_DKWXJPinfoJGXQ : AuthorPage
{ 
    private readonly DBClass db = new DBClass(); 
    private SqlConnection myConn = new SqlConnection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtEndDate.Text = Convert.ToDateTime(DateTime.Now.AddDays(1)).ToString("yyyy-MM-dd"); //获取今天的时间显示在文本框中 
            txtStartDate.Text = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
            Fillgridview();
        }
    } 

    private void Fillgridview()
    {
        DataTable dzj = db.GetJGXQinfo(ReturnFilterString());
        Session["sml"] = dzj;
        GridView1.DataSource = dzj;
        GridView1.DataBind();
    }

    private string _tempstring = ""; 
    private string ReturnFilterString()
    {
        _tempstring = "CompID=" + GetCookieCompID();
        if (inputSearchKeyword.Value.Length > 0)
        {
            _tempstring += " and LabelCode like '%" + inputSearchKeyword.Value + "%'";
        } 
        if (txtStartDate.Text.Length > 0)
        {
            _tempstring += "and LQDateTime >='" + txtStartDate.Text + "' and  CONVERT(varchar(12) , LQDateTime, 23 ) <='" +
                          txtEndDate.Text + "'";
        }
        _tempstring += "order by LQDateTime desc";
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
        Fillgridview();
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
        Fillgridview();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        Fillgridview();
    }

    private string labelcode = "";
    private string zjm = "";
    private string jxname = "";
    private string nickname = "";
    private string time = "";
    private string zjflag = "";

    protected void BtnExport_Click(object sender, EventArgs e)
    {
        if (Session["sml"] == null)
        {
            Response.Write("<script>alert(请先查询！)</script>");
        }
        XlsDocument xls = new XlsDocument();
        string timestr = DateTime.Now.ToString("yyyyMMddHHmmss");
        xls.FileName = timestr;

        Worksheet sheet = xls.Workbook.Worksheets.Add("Sheet1"); //状态栏标题名称  
        ColumnInfo cinfo = new ColumnInfo(xls, sheet);

        cinfo.Collapsed = true;

        //设置列的范围 如 0列-10列

        cinfo.ColumnIndexStart = 0; //列开始

        cinfo.ColumnIndexEnd = 2; //列结束

        cinfo.Collapsed = true;
        cinfo.Width = 80*60;
        sheet.AddColumnInfo(cinfo);

        XF cellXF = xls.NewXF();

        cellXF.VerticalAlignment = VerticalAlignments.Centered;

        cellXF.HorizontalAlignment = HorizontalAlignments.Centered;

        cellXF.Font.Height = 18*12;

        //cellXF.Font.Bold = true;

        //cellXF.Pattern = 1;//设定单元格填充风格。如果设定为0，则是纯色填充
        cellXF.UseBackground = true;

        cellXF.PatternBackgroundColor = Colors.Red; //填充的背景底色

        cellXF.PatternColor = Colors.Red; //设定填充线条的颜色


        Cells cells = sheet.Cells;

        //Cell cell1 = cells.Add(1, 1, "编号");
        Cell cell1 = cells.Add(1, 2, "中奖号码", cellXF);
        Cell cell2 = cells.Add(1, 3, "兑奖码", cellXF);
        Cell cell3 = cells.Add(1, 4, "所中奖项", cellXF);
        Cell cell4 = cells.Add(1, 5, "中奖人微信昵称", cellXF);
        Cell cell5 = cells.Add(1, 6, "手机号码", cellXF);
        Cell cell6 = cells.Add(1, 1, "中奖时间", cellXF);
        Cell cell7 = cells.Add(1, 10, "兑奖时间", cellXF);
        Cell cell8 = cells.Add(1, 8, "是否领奖", cellXF);
        Cell cell9 = cells.Add(1, 9, "兑奖点", cellXF);
        Cell cell10 = cells.Add(1, 7, "中奖者地址", cellXF);
        Cell cell11 = cells.Add(1, 11, "扫码地址", cellXF); 
        DataTable dsml = (DataTable) Session["sml"];
        if (dsml.Rows.Count > 0)
        {
            XF dateStyle = xls.NewXF();


            for (int i = 0; i < dsml.Rows.Count; i++)
            {
                for (int j = 1; j <= dsml.Columns.Count; j++)
                {
                    int row = i + 2;
                    labelcode = dsml.Rows[i][2].ToString();
                    zjm = dsml.Rows[i][12].ToString();
                    jxname = dsml.Rows[i][6].ToString();
                    nickname = dsml.Rows[i][11].ToString();
                    time = dsml.Rows[i][4].ToString();
                    zjflag = dsml.Rows[i][15].ToString();
                    string phone = dsml.Rows[i][9].ToString();
                    string djtime = dsml.Rows[i][17].ToString();
                    string djaddress = dsml.Rows[i][19].ToString();
                    string zhongjiangaddress = dsml.Rows[i][20].ToString();
                    string saomadizhiadd = dsml.Rows[i][8].ToString();
                    cells.Add(row, 2, labelcode, cellXF);

                    cells.Add(row, 3, Convert.ToDouble(zjm == "" ? "0" : zjm), cellXF);

                    cells.Add(row, 4, jxname, cellXF);

                    cells.Add(row, 5, nickname, cellXF);
                    cells.Add(row, 6, phone, cellXF);
                    cells.Add(row, 1, time, cellXF);
                    cells.Add(row, 10, djtime, cellXF);
                    cells.Add(row, 8, zjflag == "0" ? "未兑奖" : "已兑奖", cellXF);
                    cells.Add(row, 9, djaddress, cellXF);
                    cells.Add(row, 7, zhongjiangaddress, cellXF);
                    cells.Add(row, 11, saomadizhiadd, cellXF);
                }
            }
            xls.Send();
        }
    }

    private void OutPutFile(string ServerFilePath, string strfilename)
    {
        FileInfo file = new FileInfo(ServerFilePath); //用于获得文件信息
        Response.Clear(); //清空输出
        Response.Charset = "GB2312"; //设定编码
        Response.ContentEncoding = Encoding.UTF8;
        // 添加头信息,为"文件下载/另存为"对话框指定默认文件名 
        Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(ServerFilePath));
        // 添加头信息,指定文件大小,让浏览器能够显示下载进度 
        Response.AddHeader("Content-Length", file.Length.ToString());

        // 指定返回的是一个不能被客户端读取的流,必须被下载 
        Response.ContentType = "application/ms-txt";

        // 把文件流发送到客户端 
        Response.WriteFile(file.FullName);
    }

    public SqlConnection GetConnectionWL() //New
    {
        string str = ConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString();
        myConn = new SqlConnection(str);
        return myConn;
    }

    public SqlConnection GetConnection() //Yin
    {
        string str = ConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString();
        myConn = new SqlConnection(str);
        return myConn;
    }

    public SqlConnection GetConnectionHD() //Yin
    {
        string str = ConfigurationManager.ConnectionStrings["ConnectionStringJGXQ"].ToString();
        myConn = new SqlConnection(str);
        return myConn;
    }

    public DataTable Timeselect(string str)
    {
        try
        {
            using (var myConn1 = GetConnectionHD())
            {
                if (myConn1.State == ConnectionState.Closed)
                {
                    myConn1.Open();
                }
                DataTable dt = new DataTable();
                SqlDataAdapter dajk = new SqlDataAdapter("Select* from From TJ_LQJiLu Where '" + str + "'", myConn1);
                DataSet ds = new DataSet();
                dajk.Fill(ds, "ok");
                return ds.Tables["ok"];
            }
        }
        catch
        {
            DataTable dt = new DataTable();
            return dt;
        }
    }

    /// <summary>
    /// 查找经销商名称
    /// </summary>
    /// <param name="strAgentid"></param>
    /// <returns></returns>
    public string queryAgent(string strAgentid)
    {
        string strfour = strAgentid.Trim();
        if (strfour.Equals(""))
        {
            return "";
        }
        DataTable dt1 = new DataTable();
        string str = string.Empty;
        try
        {
            using (var myConn1 = GetConnection())
            {
                if (myConn1.State == ConnectionState.Closed)
                {
                    myConn1.Open();
                }
                str = "Select CompName From TJ_RegisterCompanys Where compid='" + strfour + "'";
                SqlDataAdapter dajk = new SqlDataAdapter(str, myConn1);
                dajk.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    string strIDDDD = dt1.Rows[0][0].ToString().Trim();
                    dt1.Dispose();
                    return strIDDDD;
                }
                else
                {
                    return "";
                }
            }
        }
        catch
        {
            return "Error";
        }
    }

    /// <summary>
    /// 查找产品名称
    /// </summary>
    /// <param name="strProid"></param>
    /// <returns></returns>
    public string queryProDuct(string strProid)
    {
        string strfour = strProid.Trim();
        if (strfour.Equals(""))
        {
            return "";
        }
        DataTable dt1 = new DataTable();
        string str = string.Empty;
        try
        {
            using (var myConn1 = GetConnectionWL())
            {
                if (myConn1.State == ConnectionState.Closed)
                {
                    myConn1.Open();
                }
                str = "Select Products_Name From TB_Products_Infor Where Infor_ID='" + strfour + "'";
                SqlDataAdapter dajk = new SqlDataAdapter(str, myConn1);
                dajk.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    string strIDDDD = dt1.Rows[0][0].ToString().Trim();
                    dt1.Dispose();
                    return strIDDDD;
                }
                else
                {
                    return "";
                }
            }
        }
        catch
        {
            return "Error";
        }
    }

    public string querydls(string labelcode)
    {
        string strfour = labelcode.Substring(0, 4);
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        string str = string.Empty;
        Boolean flagx = false;
        try
        {
            using (var myConn1 = GetConnectionWL())
            {
                if (myConn1.State == ConnectionState.Closed)
                {
                    myConn1.Open();
                }
                for (int k = 0; k <= 3; k++)
                {
                    string strTbname = "W" + strfour + "_0" + k.ToString().Trim() + "_BT";
                    string strFHName = "W" + strfour + "_0" + k.ToString().Trim() + "_FH";
                    string strFHTABLE = "TB_FaHuoInfo_" + GetCookieCompID();

                    str = "Select boxlabel01 From " + strTbname + " Where bottlelabel='" + labelcode.Trim() + "'";
                    SqlDataAdapter dajk = new SqlDataAdapter(str, myConn1);
                    dajk.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        string strboxinfo = dt1.Rows[0][0].ToString().Trim();
                        dt1.Dispose();
                        str = "Select * From " + strFHName + " Where boxlabel01='" + strboxinfo + "'";
                        SqlDataAdapter dajk1 = new SqlDataAdapter(str, myConn1);
                        dajk1.Fill(dt2);
                        if (dt2.Rows.Count > 0)
                        {
                            for (int m = 0; m <= dt2.Rows.Count - 1; m++)
                            {
                                string strAgentID = dt2.Rows[m]["ToAgentID"].ToString().Trim();
                                string strFHKey = dt2.Rows[m]["FhKey"].ToString().Trim();
                                if (!strAgentID.Equals(""))
                                {
                                    str = "Select * From " + strFHTABLE + " Where FHKEY='" + strFHKey + "'";

                                    SqlDataAdapter dajk2 = new SqlDataAdapter(str, myConn1);
                                    dajk2.Fill(dt3);
                                    if (dt3.Rows.Count > 0)
                                    {
                                        for (int n = 0; n <= dt3.Rows.Count - 1; n++)
                                        {
                                            string strProID = dt3.Rows[0]["ProID"].ToString().Trim();
                                            if (!strProID.Equals(""))
                                            {
                                                flagx = true;
                                                return strAgentID + "|" + strProID;
                                            }
                                            else
                                            {
                                                flagx = false;
                                            }
                                        }
                                        dt3.Dispose();
                                        flagx = false;
                                    }
                                    else
                                    {
                                        dt3.Dispose();
                                        flagx = false;
                                    }
                                }
                            }
                            dt2.Dispose();
                            flagx = false;
                        }
                        else
                        {
                            dt2.Dispose();
                            flagx = false;
                        }
                        dt1.Dispose();
                    }
                    else
                    {
                        dt1.Dispose();
                        flagx = false;
                    }
                    //----继续从此查询下去
                }
                if (flagx == false)
                {
                    return "None";
                }
            }
            return "Error";
        }
        catch (Exception)
        {
            return "Error";
        }
    }

    /// <summary>
    /// 导出到电子表格
    /// </summary>
    /// <param name="table"></param>
    /// <param name="file"></param>
    private void dataTableToCsv(DataTable table, string file)
    {
        string title = "";
        FileStream fs = new FileStream(file, FileMode.OpenOrCreate);
        //FileStream fs1 = File.Open(file, FileMode.Open, FileAccess.Read);
        StreamWriter sw = new StreamWriter(new BufferedStream(fs), Encoding.Default);
        for (int i = 0; i < table.Columns.Count; i++)
        {
            title += table.Columns[i].ColumnName + "\t"; //栏位：自动跳到下一单元格
        }
        title = title.Substring(0, title.Length - 1) + "\n";
        sw.Write(title);
        foreach (DataRow row in table.Rows)
        {
            string line = "";
            for (int i = 0; i < table.Columns.Count; i++)
            {
                line += row[i].ToString().Trim() + "\t"; //内容：自动跳到下一单元格
            }
            line = line.Substring(0, line.Length - 1) + "\n";
            sw.Write(line);
        }
        sw.Close();
        fs.Close();
    }

    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //}// end VerifyRenderingInServerForm

    public bool ExportExcelFix(DataTable dt, string path, string FileNameHH)
    {
        return true;
    }
}