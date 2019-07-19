using System;
using System.Web.UI.WebControls;
using commonlib;
using System.Data;
using org.in2bits.MyXls;

public partial class TB_SWMWXSMreport : AuthorPage
{
    private readonly DBClass _db = new DBClass(DAConfig.Showmode);
    public string[] m;
    public string wxcs;
    public string mh;
    public string qtcs;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddl_year.Items.Add(new ListItem("去年",(DateTime.Now.Year-1).ToString()));
            ddl_year.Items.Add(new ListItem("今年", (DateTime.Now.Year.ToString())));
            ddl_year.SelectedValue = DateTime.Now.Year.ToString();
            LoadData();
        }
    }


    private void LoadData()
    {
        string startime = Convert.ToDateTime(ddl_year.SelectedValue + "-01-01").ToString("yyyy-MM-dd");
        string endtime = Convert.ToDateTime(ddl_year.SelectedValue + "-12-31").ToString("yyyy-MM-dd");
        DataTable sml = _db.GetsmcsForAllMonthForSWM(startime, endtime, GetCookieCompID());
        Session.Add("sml", sml); 
        string[] mharray = new string[12];
        string[] wxcsarray = new string[12];
        if (sml.Rows.Count > 0)
        {
            foreach (DataRow row in sml.Rows)
            {
                int index = int.Parse(row["time"].ToString().Replace(ddl_year.SelectedValue, "")) - 1;
                mharray[index] = row["time"].ToString();
                wxcsarray[index] = row["num"].ToString();
            }
            for (int i = 0; i < 12; i++)
            {
                if (string.IsNullOrEmpty(mharray[i]))
                {
                    mharray[i] = ddl_year.SelectedValue + (100 + i + 1).ToString().Substring(1);
                    wxcsarray[i] = "0";
                }
            }
            mh = String.Join(",", mharray);
            wxcs = String.Join(",", wxcsarray);
        }
    }


    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        LoadData();
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