using System;
using commonlib;
using TJ.DBUtility;
using System.Data;
using System.Text;
using org.in2bits.MyXls;


public partial class Admin_activity_customer_scan_quantity_join : AuthorPage
{
    TabExecute tab = new TabExecute();
    public string mMonthString = "";
    public string mResultString = "";
    public int mTotalScore = 0;
    public int mTotalScoreWin = 0;
    public string mResultScore = "";
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
     * 按天汇总
     */
    private void Display()
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sbWin = new StringBuilder();
        StringBuilder sbTotal = new StringBuilder();
        StringBuilder mMonthSB = new StringBuilder();
        string mStartDate = input_start_date.Value;
        string mEndDate = Convert.ToDateTime(input_end_date.Value.ToString()).AddDays(1).ToString("yyyy-MM-dd");
        //扫码总数
        string mSQL = @" select year(SMTime) 'year',  month(SMTime) 'month',  
                      day(SMTime) 'day',  COUNT(LabelCode) as total
                      from TJ_375SMinfo  
                      where compid=" + GetCookieCompID() + @" and LabelCode is not null and len(LabelCode)<>0 and 
                       LabelCode in (select distinct codestr from TJ_Activity_Win_2018 where compid=" + GetCookieCompID() +
                       " and codestr is not null and len(codestr)<>0 and gettm BETWEEN '" + mStartDate + "' AND '" + mEndDate + "') and SMTime BETWEEN '" + mStartDate + "' AND '" + mEndDate + @"'
                      group by year(SMTime),  month(SMTime),  day(SMTime) order by year(SMTime),  month(SMTime),  day(SMTime)";
        DataTable dttemp = tab.ExecuteNonQuery(mSQL);
        DataRow[] rows = dttemp.Select();
        sb.Append("{name: '有效扫码',data:[");
        if (rows.Length > 0)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                sbTotal.Append("," + rows[i][3]);
                mTotalScore += Convert.ToInt32(rows[i][3]);
                mMonthSB.Append("," + "'" + rows[i][0].ToString() + "-" + rows[i][1].ToString() + "-" + rows[i][2].ToString() + "'");
                //扫码中奖总数
                string mSQLWin = @"   select year(gettm) 'year',  month(gettm) 'month',  
                      day(gettm) 'day',  COUNT(codestr) as total_win
                      from TJ_Activity_Win_2018 
                      where compid=" + GetCookieCompID() + " and convert(char(10),gettm,120)=cast( " + "'" + rows[i][0].ToString() + "-" + rows[i][1].ToString() + "-" + rows[i][2].ToString() + "' as datetime)" + @" and codestr is not null and len(codestr)<>0
                       group by year(gettm),  month(gettm),  day(gettm) order by year(gettm),  month(gettm),  day(gettm)";
                DataTable mDttempWin = tab.ExecuteNonQuery(mSQLWin);
                DataRow[] mRowsWin = mDttempWin.Select();

                if (mRowsWin.Length > 0)
                {
                    sbWin.Append("," + mRowsWin[0][3]);
                    mTotalScoreWin += Convert.ToInt32(mRowsWin[0][3]);
                }
                else
                {
                    sbWin.Append(",0");
                }              
            }
            sb.Append(sbTotal.ToString().Substring(1));
        }
        sb.Append("]},{name: '首次扫码',data:[");      
        if (sbWin.ToString().Length > 0)
        {
            sb.Append(sbWin.ToString().Substring(1));
        }
        sb.Append("]}");
        mResultScore = "'[" + mStartDate + "至" + mEndDate + "]" + " 有效扫码：" + mTotalScore + "人次；首次扫码：" + mTotalScoreWin + "人次'";
        if (mMonthSB.ToString().Length > 0)
        {
            mMonthString = mMonthSB.ToString().Substring(1);
        }
        mResultString = sb.ToString();
        dttemp.Dispose();
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        Display();
    }

    protected void btn_createexcel_Click(object sender, EventArgs e)
    {
        DataTable mDataTable = new DataTable("RoleToUser");
        DataColumn mDataColumn1 = new DataColumn("mTime", Type.GetType("System.String"));
        DataColumn mDataColumn2 = new DataColumn("mSumTotal", Type.GetType("System.String"));
        DataColumn mDataColumn3 = new DataColumn("mWinTotal", Type.GetType("System.String"));
        mDataTable.Columns.Add(mDataColumn1);
        mDataTable.Columns.Add(mDataColumn2);
        mDataTable.Columns.Add(mDataColumn3);
        string mStartDate = input_start_date.Value;
        string mEndDate = Convert.ToDateTime(input_end_date.Value.ToString()).AddDays(1).ToString("yyyy-MM-dd");
        //扫码总数
        string mSQL = @" select year(SMTime) 'year',  month(SMTime) 'month',  
                      day(SMTime) 'day',  COUNT(LabelCode) as total
                      from TJ_375SMinfo  
                      where compid=" + GetCookieCompID() + @" and LabelCode is not null and len(LabelCode)<>0 and 
                       LabelCode in (select distinct codestr from TJ_Activity_Win_2018 where compid=" + GetCookieCompID() +
                       " and codestr is not null and len(codestr)<>0 and gettm BETWEEN '" + mStartDate + "' AND '" + mEndDate + "') and SMTime BETWEEN '" + mStartDate + "' AND '" + mEndDate + @"'
                      group by year(SMTime),  month(SMTime),  day(SMTime) order by year(SMTime),  month(SMTime),  day(SMTime)";
        DataTable dttemp = tab.ExecuteNonQuery(mSQL);
        DataRow[] rows = dttemp.Select();
        if (rows.Length > 0)
        {
            for (int i = 0; i < rows.Length; i++)
            {             
                //扫码中奖总数
                string mSQLWin = @"   select year(gettm) 'year',  month(gettm) 'month',  
                      day(gettm) 'day',  COUNT(codestr) as total_win
                      from TJ_Activity_Win_2018 
                      where compid=" + GetCookieCompID() + " and convert(char(10),gettm,120)=cast( " + "'" + rows[i][0].ToString() + "-" + rows[i][1].ToString() + "-" + rows[i][2].ToString() + "' as datetime)" + @" and codestr is not null and len(codestr)<>0
                       group by year(gettm),  month(gettm),  day(gettm) order by year(gettm),  month(gettm),  day(gettm)";
                DataTable mDttempWin = tab.ExecuteNonQuery(mSQLWin);
                DataRow[] mRowsWin = mDttempWin.Select();
                DataRow mDataRow = mDataTable.NewRow();
                if (mRowsWin.Length > 0)
                {
                    mDataRow["mWinTotal"] = mRowsWin[0][3];
                }
                else
                {
                    mDataRow["mWinTotal"] = 0;
                }
                mDataRow["mTime"] = rows[i][0] + "-" + rows[i][1] + "-" + rows[i][2];
                mDataRow["mSumTotal"] = rows[i][3];
                mDataTable.Rows.Add(mDataRow);
            }
        }
        if (mDataTable.Rows.Count>0)
        {
            CreateExcel(mDataTable, "消费者活动扫码量汇总【" + mStartDate + "至" + mEndDate + "】", "消费者活动扫码量");
        }else
        {
            MessageBox.Show(this, "没有数据");
        }
        mDataTable.Dispose();
    }

    public void CreateExcel(DataTable dt, string filename, string sheetnm)
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
        cells.Add(1, 1, "汇总时间", cellXF);
        cells.Add(1, 2, "有效扫码", cellXF);
        cells.Add(1, 3, "首次扫码", cellXF);
        int count = dt.Rows.Count;//统计数量 
        if (count > 62000)
        {
            count = 62000;
        }
        for (int i = 0; i < count; i++)
        {
            int rowIndex = i + 2;
            cells.Add(rowIndex, 1, dt.Rows[i]["mTime"].ToString(), cellXF);
            cells.Add(rowIndex, 2, dt.Rows[i]["mSumTotal"].ToString(), cellXF);
            cells.Add(rowIndex, 3, dt.Rows[i]["mWinTotal"].ToString(), cellXF);
        }
        xls.Send();
    }
}