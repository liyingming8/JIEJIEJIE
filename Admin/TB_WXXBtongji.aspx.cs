using System;
using commonlib;
using System.Data;
using org.in2bits.MyXls;

public partial class TB_WXXBtongji : AuthorPage
{
    private readonly DBClass db = new DBClass();
    public string[] m;
    public string man;
    public string mh;
    public string women;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStartDate.Text = "2016-10";
            txtEndDate.Text = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month).ToString("yyyy-MM");
            string endtime = Convert.ToDateTime(txtEndDate.Text).AddMonths(1).ToString("yyyy-MM-dd");
            string startime = Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd");
            DataTable sml = db.getsmsex(startime, endtime);
            Session.Add("sml", sml);

            DataRow[] drtemp = null;
            string nan = "|";
            string time = "|";
            string nv = "|";
            string[] dataspanarray = ReturnDateSpan(DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text));
            foreach (string date in dataspanarray)
            {
                drtemp = sml.Select(" xb='1' and time = '" + date + "'");
                if (drtemp != null && drtemp.Length>0)
                {
                    nan += "," + drtemp[0]["num"];
                }
                drtemp = sml.Select(" xb='2' and time = '" + date + "'");
                if (drtemp != null && drtemp.Length >0)
                {
                    nv += "," + drtemp[0]["num"];
                }
                time += "," + date;
            }
            mh = time.Substring(2);
            man = nan.Substring(2);
            women = nv.Substring(2);
            Session["msm"] = man;
            Session["wmsm"] = women;
            Session["time"] = mh;
        }

        //m = new int[] { 1,2,3,4,5,6,7,8,9,10};
    }


    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        string endtime = Convert.ToDateTime(txtEndDate.Text).AddMonths(1).ToString("yyyy-MM-dd");
        string startime = Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd");
        DataTable sml = db.getsmsex(startime, endtime);
        Session.Add("sml", sml);

        DataRow[] drtemp = null;
        string nan = "|";
        string time = "|";
        string nv = "|";
        string[] dataspanarray = ReturnDateSpan(DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text));
        foreach (string date in dataspanarray)
        {
            drtemp = sml.Select(" xb='1' and time = '" + date + "'");
            if (drtemp != null && drtemp.Length > 0)
            {
                nan += "," + drtemp[0]["num"];
            }
            drtemp = sml.Select(" xb='2' and time = '" + date + "'");
            if (drtemp != null && drtemp.Length > 0)
            {
                nv += "," + drtemp[0]["num"];
            }

            time += "," + date;
        }
        mh = time.Substring(2);
        man = nan.Substring(2);
        women = nv.Substring(2);
        Session["msm"] = man;
        Session["wmsm"] = women;
        Session["time"] = mh;
    }

    public string[] b()
    {
        string[] m = {"1", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};
        return m;
    }

    private string[] ReturnDateSpan(DateTime sd, DateTime ed)
    {
        string tempstring = "";
        int kd = ed.Month - sd.Month;


        if (kd != 0)
        {
            int tj;
            tj = (ed.Year - sd.Year)*12 + kd;


            for (int i = 0; i <= tj; i++)
            {
                tempstring += "|" + sd.AddMonths(i).ToString("yyyy-MM");
            }

            tempstring = tempstring.Substring(1);

            return tempstring.Split('|');
        }

        else
        {
            Response.Write("<script>alert('请至少选择超过一个月')</script>");
            return tempstring.Split('|');
        }
    }

    protected void BtDC_ServerClick(object sender, EventArgs e)
    {
        XlsDocument xls = new XlsDocument();

        xls.FileName = "扫码性别统计";

        Worksheet sheet = xls.Workbook.Worksheets.Add("Sheet1"); //状态栏标题名称  
        ColumnInfo cinfo = new ColumnInfo(xls, sheet);

        cinfo.Collapsed = true;

        //设置列的范围 如 0列-10列

        cinfo.ColumnIndexStart = 0; //列开始

        cinfo.ColumnIndexEnd = 4; //列结束

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
        Cell cell1 = cells.Add(1, 1, "男性扫码量", cellXF);
        Cell cell2 = cells.Add(1, 2, "女性扫码量", cellXF);
        Cell cell3 = cells.Add(1, 3, "扫码时间", cellXF);

        string[] msm = Session["msm"].ToString().Split(',');
        string[] wmsm = Session["wmsm"].ToString().Split(',');
        string[] tsm = Session["time"].ToString().Split(',');
        string m = "";
        string w = "";
        string t = "";


        for (int i = 0; i < msm.Length; i++)
        {
            int rowIndex = i + 2;
            int colIndex = i + 1;
            m = msm[i];
            w = wmsm[i];
            t = tsm[i];
            cells.Add(rowIndex, 1, int.Parse(m), cellXF);
            cells.Add(rowIndex, 2, int.Parse(w), cellXF);
            cells.Add(rowIndex, 3, t, cellXF);
        }

        xls.Send();
    }
}