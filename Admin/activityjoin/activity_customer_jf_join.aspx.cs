using System;
using commonlib;
using TJ.DBUtility;
using System.Data;
using System.Text;
using org.in2bits.MyXls;

public partial class Admin_activity_customer_jf_join : AuthorPage
{
    TabExecute tab = new TabExecute();
    public string mMonthString = "";
    public string mResultString = "";
    public int mTotalScore =0;
    public string mResultScore = "";
    DBClass db = new DBClass(DAConfig.Showmode);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            input_start_date.Value = DateTime.Now.ToString("yyyy-MM-01");
            input_end_date.Value = DateTime.Now.ToString("yyyy-MM-dd");
            Display();
        }
    }

    /*
     * 按月、天汇总
     */
    private void Display()
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder mMonthSB = new StringBuilder();
        string mStartDate = input_start_date.Value;
        string mEndDate = Convert.ToDateTime(input_end_date.Value).ToString("yyyy-MM-dd");
       
        DataTable dttemp = null;
        if (ddl_jf_type.Text.Equals("month")) {
            string mMonthSQL = @"  select yearnm,monthnm,SUM(cast(prizevl as bigint)) as total from TJ_Activity_Win_2018 
        where compid=" + GetCookieCompID() + " and awtypeid=2 and (convert(char(10),gettm,120))>='" + mStartDate + "' and(convert(char(10),gettm,120))<='" + mEndDate + @"
        ' group by monthnm,yearnm order by yearnm,monthnm";
            dttemp = tab.ExecuteNonQuery(mMonthSQL);
            DataRow[] rows = dttemp.Select();
            sb.Append("{name: '扫码积分',data:[");
            if (rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    if (i == 0)
                    {
                        sb.Append(rows[i][2]);
                        mTotalScore += Convert.ToInt32(rows[i][2]);
                        mMonthSB.Append("'" + rows[i][0].ToString() + "-" + rows[i][1].ToString() + "'");
                    }
                    else
                    {
                        sb.Append("," + rows[i][2]);
                        mTotalScore += Convert.ToInt32(rows[i][2]);
                        mMonthSB.Append("," + "'" + rows[i][0].ToString() + "-" + rows[i][1].ToString() + "'");
                    }

                }
            }
            mResultScore = "'[" + mStartDate + "至" + mEndDate + "]" + " 积分总额：" + mTotalScore + "'";
        }
        else if (ddl_jf_type.Text.Equals("day"))
        {
            string mDaySQL = @" select yearnm,monthnm,daynum,SUM(cast(prizevl as bigint)) as total from TJ_Activity_Win_2018 
        where compid=" + GetCookieCompID() + " and awtypeid=2 and (convert(char(10),gettm,120))>='" + mStartDate + "' and(convert(char(10),gettm,120))<='" + mEndDate + @"
        ' group by daynum,monthnm,yearnm order by yearnm,monthnm,daynum";
            dttemp = tab.ExecuteNonQuery(mDaySQL);
            DataRow[] rows = dttemp.Select();
            sb.Append("{name: '扫码积分',data:[");
            if (rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    if (i == 0)
                    {
                        sb.Append(rows[i][3]);
                        mTotalScore += Convert.ToInt32(rows[i][3]);
                        mMonthSB.Append("'" + rows[i][0].ToString() + "-" + rows[i][1].ToString()+ "-" + rows[i][2].ToString() +"'");
                    }
                    else
                    {
                        sb.Append("," + rows[i][3]);
                        mTotalScore += Convert.ToInt32(rows[i][3]);
                        mMonthSB.Append("," + "'" + rows[i][0].ToString() + "-" + rows[i][1].ToString() + "-" + rows[i][2].ToString() + "'");
                    }
                }
            }
            mResultScore = "'[" + mStartDate + "至" + mEndDate + "]" + " 积分总额：" + mTotalScore + "'";
        }
        else {
            sb.Append("{name: '无数据',data:[");
        };
                    
        sb.Append("]}");
       
        mMonthString = mMonthSB.ToString();
        mResultString = sb.ToString();
        dttemp.Dispose();
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        Display();
    }

    protected void btn_createexcel_Click(object sender, EventArgs e)
    {
        string mSQL = "";
        string mStartDate = input_start_date.Value;
        string mEndDate = Convert.ToDateTime(input_end_date.Value).ToString("yyyy-MM-dd");
        if (ddl_jf_type.Text.Equals("month"))
        {
            mSQL = @"  select yearnm,monthnm,SUM(cast(prizevl as bigint)) as total from TJ_Activity_Win_2018 
        where compid=" + GetCookieCompID() + " and awtypeid=2 and (convert(char(10),gettm,120))>='" + mStartDate + "' and(convert(char(10),gettm,120))<='" + mEndDate + @"
        ' group by monthnm,yearnm order by yearnm,monthnm";
        }else
        {
            mSQL = @" select yearnm,monthnm,daynum,SUM(cast(prizevl as bigint)) as total from TJ_Activity_Win_2018 
        where compid=" + GetCookieCompID() + " and awtypeid=2 and (convert(char(10),gettm,120))>='" + mStartDate + "' and(convert(char(10),gettm,120))<='" + mEndDate + @"
        ' group by daynum,monthnm,yearnm order by yearnm,monthnm,daynum";
        }
       
        DataTable dt = tab.ExecuteQuery(mSQL, null);
        if (dt.Rows.Count > 0)
        {
            CreateExcel(dt, "消费者积分汇总（"+ ddl_jf_type.SelectedItem+ "）【" + mStartDate + "至" + mEndDate + "】", "消费者积分汇总", ddl_jf_type.Text);
        }
        dt.Dispose();
    }

    public void CreateExcel(DataTable dt, string filename, string sheetnm,string mType)
    {
        XlsDocument xls = new XlsDocument();
        xls.FileName = filename;
        Worksheet sheet = xls.Workbook.Worksheets.Add(sheetnm);//状态栏标题名称  
        ColumnInfo cinfo = new ColumnInfo(xls, sheet);
        cinfo.Collapsed = true;
        //设置列的范围 如 0列-10列 
        cinfo.ColumnIndexStart = 0;//列开始 
        cinfo.ColumnIndexEnd = 6;//列结束 
        cinfo.Collapsed = true;
        cinfo.Width = 80 * 60;
        sheet.AddColumnInfo(cinfo);
        XF cellXF = xls.NewXF();
        cellXF.VerticalAlignment = VerticalAlignments.Centered;
        cellXF.HorizontalAlignment = HorizontalAlignments.Centered;
        cellXF.Font.Height = 18 * 12;
        Cells cells = sheet.Cells;
        //Cell cell1 = cells.Add(1, 1, "编号");
        cells.Add(1, 1, "汇总时间", cellXF);
        cells.Add(1, 2, "总数", cellXF);
        int count = dt.Rows.Count;//统计数量 
        if (count > 62000)
        {
            count = 62000;
        }
        for (int i = 0; i < count; i++)
        {
            int rowIndex = i + 2;
            if (mType.Equals("day")) {
                cells.Add(rowIndex, 1, dt.Rows[i]["yearnm"].ToString()+"-"+dt.Rows[i]["monthnm"].ToString()+"-"+dt.Rows[i]["daynum"].ToString(), cellXF);
            }else
            {
                cells.Add(rowIndex, 1, dt.Rows[i]["yearnm"].ToString() +"-"+dt.Rows[i]["monthnm"].ToString(), cellXF);
            }
            cells.Add(rowIndex, 2, dt.Rows[i]["total"].ToString(), cellXF);
        }
        xls.Send();
    }
}