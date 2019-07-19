using System;
using TJ.DBUtility;
using System.Data;
using System.Text;
using commonlib;
using org.in2bits.MyXls;


public partial class Admin_activityjoin_activity_address_scan_quantity_join : AuthorPage
{  
    TabExecute tab = new TabExecute();
    public string mSubTitleString = "";
    public string mResultString = "";
    public int mSaoMaTotal = 0;
    public string mProvince = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            input_start_date.Value = DateTime.Now.ToString("yyyy-MM-01");
            input_end_date.Value = DateTime.Now.ToString("yyyy-MM-dd");
            Display();
        }

    }

    private void Display()
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sbSubTitle = new StringBuilder();
        string mStartDate = input_start_date.Value;
        string mEndDate = Convert.ToDateTime(input_end_date.Value.ToString()).AddDays(1).ToString("yyyy-MM-dd");
        sb.Append("{ name:'扫码数量',colorByPoint: true,data: [");
        DataTable dttemp=null;
        //全部省份
        if (Province.SelectedValue.Equals("All"))
        {
            string mSQL = "select SMProc,count(SMProc) as total from TJ_375SMinfo where CompID="+ GetCookieCompID()+ @" and SMTime between '" + mStartDate + "'and '" + mEndDate + @"'  
        and SMProc in ('安徽省','澳门特别行政区','北京市','福建省','甘肃省','广东省','广西壮族自治区','贵州省','海南省','河北省','河南
       省','黑龙江省','湖北省','湖南省','吉林省','江苏省','江西省','辽宁省','内蒙古自治区','宁夏回族自治区','青海省','山东
       省','山西省','陕西省','上海市','四川省','台湾省','天津市','西藏自治区','香港特别行政区','新疆维吾尔自治区','云南省',
       '浙江省','重庆市') and  LabelCode in (select distinct codestr from TJ_Activity_Win_2018 where gettm between '" + mStartDate + "'and '" + mEndDate + @"'
	   and CompID=" + GetCookieCompID() +" and  codestr is not null and len(codestr)<>0) group by SMProc";
            dttemp = tab.ExecuteNonQuery(mSQL);  
        }
        //直辖市、特别行政区
        else if (Province.SelectedValue.Equals("BeiJjing") || Province.SelectedValue.Equals("ShangHai") || Province.SelectedValue.Equals("TianJin")
           || Province.SelectedValue.Equals("ChongQing") || Province.SelectedValue.Equals("XiangGang") || Province.SelectedValue.Equals("AoMen"))
        {
            string mSQL = "select SMxj,count(SMxj) as total from TJ_375SMinfo where CompID=" + GetCookieCompID() + @" and SMTime between '" + mStartDate + "'and '" + mEndDate + @"'  
        and SMProc like '%" + Province.SelectedItem + "%'and  LabelCode in (select distinct codestr from TJ_Activity_Win_2018 where gettm between '" + mStartDate + "'and '" + mEndDate + @"'
	   and CompID=" + GetCookieCompID() + " and  codestr is not null and len(codestr)<>0) group by SMxj";
            dttemp = tab.ExecuteNonQuery(mSQL);
        }
        //单个省
        else
        {
            string mSQL = "select SMsj,count(SMsj) as total from TJ_375SMinfo where CompID=" + GetCookieCompID() + @" and SMTime between '" + mStartDate + "'and '" + mEndDate + @"'  
        and SMProc like '%" + Province.SelectedItem + "%' and LabelCode in (select distinct codestr from TJ_Activity_Win_2018 where gettm between '" + mStartDate + "'and '" + mEndDate + @"'
	   and CompID=" + GetCookieCompID() + " and  codestr is not null and len(codestr)<>0) group by SMsj";
            dttemp = tab.ExecuteNonQuery(mSQL);
        }

        if (dttemp.Rows.Count > 0)
        {
            for (int i = 0; i < dttemp.Rows.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append("{name:'" + dttemp.Rows[i][0] + "',y:" + dttemp.Rows[i][1] + ",sliced: true,selected: true}");
                    mSaoMaTotal += Convert.ToInt32(dttemp.Rows[i][1]);
                }
                else
                {
                    sb.Append(",{name:'" + dttemp.Rows[i][0] + "',y:" + dttemp.Rows[i][1] + "}");
                    mSaoMaTotal += Convert.ToInt32(dttemp.Rows[i][1]);
                }
            }
        }else
        {
            sb.Append("{name:'没有数据',y:0}");
        }
        if (!Province.SelectedValue.Equals("All"))
        {
            mProvince = "'"+Province.SelectedItem.ToString()+"扫码量统计'";
        }else
        {
            mProvince = "'" + "扫码量统计'";
        }
        mSubTitleString =sbSubTitle.Append("'["+ input_start_date.Value+"至"+ input_end_date.Value+"] 扫码总数:"+ mSaoMaTotal+"件'").ToString();
        sb.Append("]}");
        mResultString = sb.ToString();
        dttemp.Dispose();

    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        Display();
    }

    protected void btn_createexcel_Click(object sender, EventArgs e)
    {
        string mStartDate = input_start_date.Value;
        string mEndDate = Convert.ToDateTime(input_end_date.Value.ToString()).AddDays(1).ToString("yyyy-MM-dd");
        string mSQL = "";
        //全部省份
        if (Province.SelectedValue.Equals("All"))
        {
            mSQL = "select SMProc,count(SMProc) as total from TJ_375SMinfo where CompID=" + GetCookieCompID() + @" and SMTime between '" + mStartDate + "'and '" + mEndDate + @"'  
        and SMProc in ('安徽省','澳门特别行政区','北京市','福建省','甘肃省','广东省','广西壮族自治区','贵州省','海南省','河北省','河南
       省','黑龙江省','湖北省','湖南省','吉林省','江苏省','江西省','辽宁省','内蒙古自治区','宁夏回族自治区','青海省','山东
       省','山西省','陕西省','上海市','四川省','台湾省','天津市','西藏自治区','香港特别行政区','新疆维吾尔自治区','云南省',
       '浙江省','重庆市') and  LabelCode in (select distinct codestr from TJ_Activity_Win_2018 where gettm between '" + mStartDate + "'and '" + mEndDate + @"'
	   and CompID=" + GetCookieCompID() + " and  codestr is not null and len(codestr)<>0) group by SMProc";
        }
        //直辖市、特别行政区
        else if (Province.SelectedValue.Equals("BeiJjing") || Province.SelectedValue.Equals("ShangHai") || Province.SelectedValue.Equals("TianJin")
           || Province.SelectedValue.Equals("ChongQing") || Province.SelectedValue.Equals("XiangGang") || Province.SelectedValue.Equals("AoMen"))
        {
            mSQL= "select SMxj as SMProc,count(SMxj) as total from TJ_375SMinfo where CompID=" + GetCookieCompID() + @" and SMTime between '" + mStartDate + "'and '" + mEndDate + @"'  
        and SMProc like '%" + Province.SelectedItem + "%'and  LabelCode in (select distinct codestr from TJ_Activity_Win_2018 where gettm between '" + mStartDate + "'and '" + mEndDate + @"'
	   and CompID=" + GetCookieCompID() + " and  codestr is not null and len(codestr)<>0) group by SMxj";
        }
        //单个省
        else
        {
            mSQL = "select SMsj as SMProc,count(SMsj) as total from TJ_375SMinfo where CompID=" + GetCookieCompID() + @" and SMTime between '" + mStartDate + "'and '" + mEndDate + @"'  
        and SMProc like '%" + Province.SelectedItem + "%' and LabelCode in (select distinct codestr from TJ_Activity_Win_2018 where gettm between '" + mStartDate + "'and '" + mEndDate + @"'
	   and CompID=" + GetCookieCompID() + " and  codestr is not null and len(codestr)<>0) group by SMsj";
        }
      
        DataTable dt = tab.ExecuteNonQuery(mSQL);
        if (dt.Rows.Count > 0)
        {
            CreateExcel(dt, Province.SelectedItem+"扫码量统计【"+ mStartDate+"至"+ mEndDate+"】", "扫码量统计", Province.SelectedValue);
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
        if (mType.Equals("All")) {
            cells.Add(1, 1, "省份", cellXF);
        }else if (mType.Equals("BeiJjing") || mType.Equals("ShangHai") || mType.Equals("TianJin")
           || mType.Equals("ChongQing") || mType.Equals("XiangGang") || mType.Equals("AoMen"))
        {
            cells.Add(1, 1, "区", cellXF);
        }else
        {
            cells.Add(1, 1, "城市", cellXF);           
        }
        cells.Add(1, 2, "总数", cellXF);
        int count = dt.Rows.Count;//统计数量 
        if (count > 62000)
        {
            count = 62000;
        }
        for (int i = 0; i < count; i++)
        {
            int rowIndex = i + 2;
            cells.Add(rowIndex, 1, dt.Rows[i]["SMProc"].ToString(), cellXF);
            cells.Add(rowIndex, 2, dt.Rows[i]["total"].ToString(), cellXF);          
        }
        xls.Send();      
    }
}