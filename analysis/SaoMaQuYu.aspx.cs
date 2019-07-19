using System;
using commonlib;
using System.Data;
using org.in2bits.MyXls;

public partial class SaoMaQuYu : AuthorPage
{
    private readonly DBClass db = new DBClass(DAConfig.Showmode);
    public string[] m;
    public string wxcs;
    public string add;
    public int sum;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStartDate.Text = "2018-01-01";
            txtEndDate.Text =DateTime.Now.ToString("yyyy-MM-dd");
            SMdata(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text),GetCookieCompID(),DAConfig.Showmode);
        }
    }


    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        string endtime = Convert.ToDateTime(txtEndDate.Text).AddMonths(1).ToString("yyyy-MM-dd");
        string startime = Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd");
        SMdata(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), GetCookieCompID(),DAConfig.Showmode);
    }

    protected  void SMdata(DateTime sd, DateTime ed,string compid,string showmode) {

        TimeSpan ts = ed - sd;
        double days = ts.TotalDays;
        int lestnum = 1;
        if (days > 3) {
            lestnum = 70;
        }
        string endtime = ed.AddMonths(1).ToString("yyyy-MM-dd");
        string startime =sd.ToString("yyyy-MM-dd");
        DataTable sml = db.getsmad_all(startime, endtime, compid, showmode);
        string cs = "|";
        string ad = "|";
        int zl = 0;
        if (sml.Rows.Count > 0)
        {
            foreach (DataRow row in sml.Rows)
            {
                if (int.Parse(row["num"].ToString()) > lestnum)
                {
                    cs += "," + row["num"];
                    ad += "," + row["address"];
                    zl += int.Parse(row["num"].ToString());
                }
            }
            sum = zl;
            add = ad.Substring(2);
            wxcs = cs.Substring(2);
            Session["add"] = add;
            Session["cs"] = wxcs;
        } 
    }

    public string[] b()
    {
        string[] m = {"1", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};
        return m;
    }

    private string[] ReturnDateSpan(DateTime sd, DateTime ed)
    {
        string tempstring = "";


        if ((ed - sd).Days >= 32)
        {
            int tj;
            if (ed.Year == sd.Year)
            {
                tj = ed.Month - sd.Month;
            }
            else
            {
                tj = ed.Month + 12 - sd.Month;
            }
            for (int i = 0; i <= tj; i++)
            {
                tempstring += "|" + sd.AddMonths(i).ToString("yyyy-MM-dd");
            }
        }
        if (tempstring.StartsWith("|"))
        {
            tempstring = tempstring.Substring(1);
        }
        if (tempstring.Length > 0)
        {
            return tempstring.Split('|');
        }
        else
        {
            return null;
        }
    }

    protected void BtDC_ServerClick(object sender, EventArgs e)
    {
        XlsDocument xls = new XlsDocument();

        xls.FileName = "地址扫码量统计（省级）";

        Worksheet sheet = xls.Workbook.Worksheets.Add("Sheet1"); //状态栏标题名称  
        ColumnInfo cinfo = new ColumnInfo(xls, sheet);

        cinfo.Collapsed = true;

        //设置列的范围 如 0列-10列

        cinfo.ColumnIndexStart = 0; //列开始

        cinfo.ColumnIndexEnd = 5; //列结束

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
        Cell cell1 = cells.Add(1, 1, "地址（省级）", cellXF);
        Cell cell2 = cells.Add(1, 2, "扫码量", cellXF);


        string[] add = Session["add"].ToString().Split(',');
        string[] cs = Session["cs"].ToString().Split(',');

        string ad = "";
        string scs = "";


        for (int i = 0; i < add.Length; i++)
        {
            int rowIndex = i + 2;
            int colIndex = i + 1;
            ad = add[i];
            scs = cs[i];

            cells.Add(rowIndex, 1, ad, cellXF);
            cells.Add(rowIndex, 2, int.Parse(scs), cellXF);
        }

        xls.Send();
    }


    protected void BtXj_ServerClick(object sender, EventArgs e)
    {
        string endtime = Convert.ToDateTime(txtEndDate.Text).AddMonths(1).ToString("yyyy-MM-dd");
        string startime = Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd");
        DataTable sml = db.getsmadxj(startime, endtime);
        XlsDocument xls = new XlsDocument();

        xls.FileName = "地址扫码量统计(县级)";

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


        Cells cells = sheet.Cells;

        Cell cell1 = cells.Add(1, 1, "省", cellXF);
        Cell cell2 = cells.Add(1, 2, "市", cellXF);
        Cell cell3 = cells.Add(1, 3, "县", cellXF);
        Cell cell4 = cells.Add(1, 4, "扫码量", cellXF);


        string ad = "";
        string scs = "";
        string sj = "";
        string s = "";
        string x = "";
        if (sml.Rows.Count > 0)
        {
            XF dateStyle = xls.NewXF();

            for (int i = 0; i < sml.Rows.Count; i++)
            {
                int rowIndex = i + 2;
                int colIndex = i + 1;
                ad = sml.Rows[i]["address"].ToString();

                if (ad.Length > 7)
                {
                    sj = ad.Substring(0, 3);
                    s = ad.Substring(3, 3);

                    if (ad.Length < 9)
                    {
                        x = ad.Substring(6, 2);
                    }
                    else
                    {
                        x = ad.Substring(6, 3);
                    }
                }

                else
                {
                    sj = "未允许";
                    s = "未允许";
                    x = "未允许";
                }

                scs = sml.Rows[i]["num"].ToString();

                cells.Add(rowIndex, 1, sj, cellXF);
                cells.Add(rowIndex, 2, s, cellXF);
                cells.Add(rowIndex, 3, x, cellXF);
                cells.Add(rowIndex, 4, int.Parse(scs), cellXF);
            }
        }


        xls.Send();
    }
}