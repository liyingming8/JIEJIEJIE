using System;
using commonlib;
using System.Data;
using org.in2bits.MyXls;

public partial class TB_WXHBJGinfoDay : AuthorPage
{

    DBClass db = new DBClass();
    public string[] m;
    public string gdhb;
    public string trhb;
    public string xqhb;
    public string mh;
    public string qtcs;


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            txtStartDate.Text = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-01").ToString("yyyy-MM-dd"); ;
            txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            BindHC(txtStartDate.Text, txtEndDate.Text, HBselect.Value);
        }

    }

    public void BindHC(string stardate, string enddate, string type)
    {
        string endtime = Convert.ToDateTime(enddate).AddDays(1).ToString("yyyy-MM-dd");
        string startime = Convert.ToDateTime(stardate).ToString("yyyy-MM-dd");

        DataTable gdrhnum = db.GetWXHBnum(startime, endtime, GetCookieCompID(), "高度柔和", "55", "day");
        DataTable trnum = db.GetWXHBnum(startime, endtime, GetCookieCompID(), "恬柔", "55", "day");
        DataTable xqnum = db.GetWXHBnum_JGXQ(startime, endtime, GetCookieCompID(), "湘泉", "112", "day");

        Session.Add("gdrhnum", gdrhnum);
        Session.Add("trnum", trnum);
        Session.Add("xqnum", xqnum);

       
        DataRow[] drtemp = null;

        string gdcs = "|";
        string trcs = "|";
        string xqcs = "|";
        string time = "|";
        foreach (DataRow row in gdrhnum.Rows)
        {
            if (type == "HB_Num")
            {
                gdcs += "," + row["num"];
            }
            else
            {
                gdcs += "," + Convert.ToInt32(row["num"].ToString()) * 19.9;
            }
            time += "," + row["time"];
        }
        mh = time.Substring(2);

        foreach (string date in mh.Split(','))
        {
            drtemp = trnum.Select("time = '" + date + "'");
            string num = "0";
            if (drtemp.Length > 0)
            {
                num = drtemp[0]["num"].ToString();
            }
            if (type == "HB_Num")
            {
                trcs += "," + num;
            }
            else
            {
                trcs += "," + Convert.ToInt32(num) * 19.9;
            }

            drtemp = xqnum.Select("time = '" + date + "'");

            if (drtemp.Length > 0)
            {
                num = drtemp[0]["num"].ToString();
            }

            if (type == "HB_Num")
            {
                xqcs += "," + num;
            }
            else
            {
                xqcs += "," + Convert.ToInt32(num) * 5;
            }
        }
        gdhb = gdcs.Length > 2 ? gdcs.Substring(2) : "0";
        trhb = trcs.Length > 2 ? trcs.Substring(2) : "0";
        xqhb = xqcs.Length > 2 ? xqcs.Substring(2) : "0";

    }


    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        string endtime = Convert.ToDateTime(txtEndDate.Text).ToString("yyyy-MM-dd");
        string startime = Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd");

        BindHC(startime, endtime, HBselect.Value);

    }



    private string[] ReturnDateSpan(DateTime sd, DateTime ed)
    {
        string tempstring = "";
        int kd = ed.Month - sd.Month;

        int tj;
        tj = (ed.Year - sd.Year) * 12 + kd;
        if (tj != 0)
        {
            for (int i = 0; i <= tj; i++)
            {
                tempstring += "|" + sd.AddMonths(i).ToString("yyyy-MM");
            }
            tempstring = tempstring.Substring(1);
            return tempstring.Split('|');
        }
        else
        {
            string[] s = { sd.ToString("yyyy-MM") };
            return s;
        }
    }

    protected void BtDC_ServerClick(object sender, EventArgs e)
    {
        string type = HBselect.Value;
        XlsDocument xls = new XlsDocument();

        if (type == "HB_Num")
        {
            xls.FileName = "天红包数量统计";
        }
        else
        {
            xls.FileName = "天红包金额统计";
        }
        XF cellXF = xls.NewXF();
        cellXF.VerticalAlignment = VerticalAlignments.Centered;
        cellXF.HorizontalAlignment = HorizontalAlignments.Centered;
        cellXF.Font.Height = 18 * 12;

        Worksheet sheet = xls.Workbook.Worksheets.Add("红包统计");//状态栏标题名称  
        ColumnInfo cinfo = new ColumnInfo(xls, sheet);
        cinfo.Collapsed = true;
        //设置列的范围 如 0列-10列
        cinfo.ColumnIndexStart = 0;//列开始
        cinfo.ColumnIndexEnd = 3;//列结束
        cinfo.Width = 80 * 60;
        sheet.AddColumnInfo(cinfo);
        Cells cells = sheet.Cells;
        cells.Add(1, 1, "活动产品", cellXF);
        if (type == "HB_Num")
        {
            cells.Add(1, 2, "红包领取数量", cellXF);
        }
        else
        {
            cells.Add(1, 2, "红包领取金额", cellXF);
        }
        cells.Add(1, 3, "领取时间", cellXF);

        // Worksheet sheettr = xls.Workbook.Worksheets.Add("恬柔");//状态栏标题名称  
        // ColumnInfo cinfotr = new ColumnInfo(xls, sheettr);
        // cinfotr.Collapsed = true;
        // //设置列的范围 如 0列-10列
        // cinfotr.ColumnIndexStart = 0;//列开始
        // cinfotr.ColumnIndexEnd = 3;//列结束
        // cinfotr.Width = 80 * 60;
        // sheettr.AddColumnInfo(cinfotr);
        // Cells celltr = sheettr.Cells;
        // cell1 = celltr.Add(1, 1, "活动产品", cellXF);
        // if (type == "HB_Num")
        // {
        //     Cell cell2 = celltr.Add(1, 2, "红包领取数量", cellXF);
        // }
        // else
        // {
        //     Cell cell2 = celltr.Add(1, 2, "红包领取金额", cellXF);
        // }
        //cell3 = celltr.Add(1, 3, "领取时间", cellXF);


        DataTable gdrhnum = (DataTable)Session["gdrhnum"];
        DataTable trnum = (DataTable)Session["trnum"];
        DataTable xqnum = (DataTable)Session["xqnum"];
        int gdrow = gdrhnum.Rows.Count;
        int trrow = trnum.Rows.Count;


        XF dateStyle = xls.NewXF();

        //高度柔和
        if (gdrow > 0)
        {
            for (int i = 0; i < gdrow; i++)
            {
                int rowIndex = i + 2;
                cells.Add(rowIndex, 1, "高度柔和", cellXF);
                if (type == "HB_Num")
                {
                    cells.Add(rowIndex, 2, int.Parse(gdrhnum.Rows[i]["num"].ToString()), cellXF);
                }
                else
                {
                    cells.Add(rowIndex, 2, int.Parse(gdrhnum.Rows[i]["num"].ToString()) * 19.9, cellXF);
                }
                cells.Add(rowIndex, 3, gdrhnum.Rows[i]["time"].ToString(), cellXF);
            }
        }
        //恬柔
        if (trrow > 0)
        {
            for (int i = 0; i < trrow; i++)
            {
                int rowIndex = i + 2 + gdrow+1;

                cells.Add(rowIndex, 1, "恬柔拾伍", cellXF);
                if (type == "HB_Num")
                {
                    cells.Add(rowIndex, 2, int.Parse(trnum.Rows[i]["num"].ToString()), cellXF);
                }
                else
                {
                    cells.Add(rowIndex, 2, int.Parse(trnum.Rows[i]["num"].ToString()) * 19.9, cellXF);
                }
                cells.Add(rowIndex, 3, trnum.Rows[i]["time"].ToString(), cellXF);
            }
        }
        //湘泉
        if (trrow > 0)
        {
            for (int i = 0; i < xqnum.Rows.Count; i++)
            {
                int rowIndex = i + 2 + gdrow + trrow+2;
                cells.Add(rowIndex, 1, "湘泉系列", cellXF);
                if (type == "HB_Num")
                {
                    cells.Add(rowIndex, 2, int.Parse(xqnum.Rows[i]["num"].ToString()), cellXF);
                }
                else
                {
                    cells.Add(rowIndex, 2, int.Parse(xqnum.Rows[i]["num"].ToString()) * 5, cellXF);
                }
                cells.Add(rowIndex, 3, xqnum.Rows[i]["time"].ToString(), cellXF);
            }
        }
        xls.Send();





    }

}