using System;
using commonlib;
using System.Data;
using org.in2bits.MyXls;

public partial class TB_WXSMreportDay : AuthorPage
{
    private readonly DBClass db = new DBClass();
    public string[] m;
    public string wxcs;
    public string mh;
    public string qtcs;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStartDate.Text =
                Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-01").ToString("yyyy-MM-dd");
            ;
            txtEndDate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            string endtime = txtEndDate.Text;
            string startime = txtStartDate.Text;

            DataTable sml = db.getsmcsDay(startime, endtime);
            Session.Add("sml", sml);

            string cs = "|";
            string time = "|";

            foreach (DataRow row in sml.Rows)
            {
                cs += "," + row["num"];
                time += "," + row["time"].ToString().Substring(8);
            }

            mh = time.Substring(2);
            wxcs = cs.Substring(2);
        }
    }


    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        string endtime = Convert.ToDateTime(txtEndDate.Text).AddDays(1).ToString("yyyy-MM-dd");
        string startime = txtStartDate.Text;

        DataTable sml = db.getsmcsDay(startime, endtime);
        Session.Add("sml", sml);

        string cs = "|";
        string time = "|";
        if (sml.Rows.Count > 0)
        {
            foreach (DataRow row in sml.Rows)
            {
                cs += "," + row["num"];
                time += "," + row["time"].ToString().Substring(8);
            }

            mh = time.Substring(2);
            wxcs = cs.Substring(2);
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

        xls.FileName = "扫码量统计";

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
        Cell cell2 = cells.Add(1, 1, "扫码量", cellXF);
        Cell cell3 = cells.Add(1, 2, "扫码时间", cellXF);

        string drValue = "";
        DataTable dsml = (DataTable) Session["sml"];
        if (dsml.Rows.Count > 0)
        {
            XF dateStyle = xls.NewXF();


            for (int i = 0; i < dsml.Rows.Count; i++)
            {
                for (int j = 0; j < dsml.Columns.Count; j++)
                {
                    int rowIndex = i + 2;
                    int colIndex = j + 1;
                    if (j == 0)
                    {
                        int sl = int.Parse(dsml.Rows[i][j].ToString());
                        cells.Add(rowIndex, colIndex, sl, cellXF);
                    }

                    else
                    {
                        drValue = dsml.Rows[i][j].ToString();
                        cells.Add(rowIndex, colIndex, drValue, cellXF);
                    }
                }
            }
            xls.Send();
        }
    }
}