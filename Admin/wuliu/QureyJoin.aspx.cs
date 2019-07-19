using System;
using System.Linq;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using commonlib;
using DataAccess;
using System.Data;
using System.Text;

public partial class Admin_wuliu_QureyJoin : AuthorPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStartDate.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-01";
            txtEndDate.Text =
                Convert.ToDateTime(DateTime.Now.Year + "-" + (DateTime.Now.Month + 1) + "-01")
                    .AddDays(-1)
                    .ToString("yyyy-MM-dd");
            BtnSearch0_Click(sender, e);
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        Series s1 = new Series("电话查询");
        Title xtitle = new Title();
        Title ytitle = new Title();
        xtitle = new Title("时段", Docking.Bottom);
        Chart1.Titles.Add(xtitle);
        ytitle = new Title("人次", Docking.Left);
        ytitle.TextOrientation = TextOrientation.Stacked;
        Chart1.Titles.Add(ytitle);
        Chart1.Height = 400;
        Chart1.Width = 900;
        s1.ChartType = SeriesChartType.Line;
        s1.Color = Color.Orange;
        s1.BorderWidth = 2;
        if (CBL_QueryMethod.Items[0].Selected)
        {
            Chart1.Series.Add(s1);
        }
        Series s2 = new Series("短信查询");
        s2.ChartType = SeriesChartType.Line;
        s2.BorderWidth = 2;
        s2.Color = Color.Blue;
        if (CBL_QueryMethod.Items[1].Selected)
        {
            Chart1.Series.Add(s2);
        }
        Series s3 = new Series("互联网查询");
        s3.ChartType = SeriesChartType.Line;
        s3.BorderWidth = 2;
        s3.Color = Color.Red;
        if (CBL_QueryMethod.Items[2].Selected)
        {
            Chart1.Series.Add(s3);
        }

        int index = 0;
        double joinenlarge = DAConfig.joinenlarge;
        string[] temparray = QueryInfoByPhone("1").Split('|');
        double[] yValues1 = new double[temparray.Length];
        double[] yValues2 = new double[temparray.Length];
        double[] yValues3 = new double[temparray.Length];
        string[] xValues = new string[temparray.Length];
        foreach (string line in temparray)
        {
            if (line.Contains('*'))
            {
                string[] leixingarray = line.Split('*');
                for (int i = 0; i < leixingarray.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (leixingarray[i].Contains(","))
                            {
                                yValues1.SetValue(int.Parse(leixingarray[i].Split(',')[0])*joinenlarge, index);
                                xValues.SetValue(leixingarray[i].Split(',')[1], index);
                            }
                            else
                            {
                                yValues1.SetValue(0, index);
                                xValues.SetValue(leixingarray[i].Split(',')[1], index);
                            }
                            break;
                        case 1:
                            if (leixingarray[i].Contains(","))
                            {
                                yValues2.SetValue(int.Parse(leixingarray[i].Split(',')[0])*joinenlarge, index);
                            }
                            else
                            {
                                yValues2.SetValue(0, index);
                            }
                            break;
                        case 2:
                            if (leixingarray[i].Contains(","))
                            {
                                yValues3.SetValue(int.Parse(leixingarray[i].Split(',')[0])*joinenlarge, index);
                            }
                            else
                            {
                                yValues3.SetValue(0, index);
                            }
                            break;
                    }
                }
                index++;
            }
        }
        if (CBL_QueryMethod.Items[0].Selected)
        {
            Chart1.Series["电话查询"].Points.DataBindXY(xValues, yValues1);
        }
        if (CBL_QueryMethod.Items[1].Selected)
        {
            Chart1.Series["短信查询"].Points.DataBindXY(xValues, yValues2);
        }
        if (CBL_QueryMethod.Items[2].Selected)
        {
            Chart1.Series["互联网查询"].Points.DataBindXY(xValues, yValues3);
        }
    }

    private DataTable dttemp = new DataTable();
    private DataRow[] drtemp;
    private int tempcounter;
    private string tempstring = "";
    private readonly StringBuilder sb = new StringBuilder();

    private string QueryInfoByPhone(string ByFieldName)
    {
        DataAccessClass dacob = new DataAccessClass();
        dttemp = dacob.getCountALL("XF000001", txtStartDate.Text,
            Convert.ToDateTime(txtEndDate.Text).AddDays(1).ToString("yyyy-MM-dd"));
        switch (ByFieldName)
        {
            case "1":
                string[] dataspanarray = ReturnDateSpan(DateTime.Parse(txtStartDate.Text),
                    DateTime.Parse(txtEndDate.Text));
                sb.Clear();
                foreach (string date in dataspanarray)
                {
                    drtemp =
                        dttemp.Select(
                            "查询方式='001' and 开始时间 >= '" + date + "' and 开始时间<'" +
                            Convert.ToDateTime(date).AddDays(1).ToString("yyyy-MM-dd") + "'", "开始时间");
                    sb.Append("|" + drtemp.Length + "," + date.Substring(date.IndexOf("-") + 1));
                    drtemp =
                        dttemp.Select(
                            "查询方式='002' and 开始时间 >= '" + date + "' and 开始时间<'" +
                            DateTime.Parse(date).AddDays(1).ToString("yyyy-MM-dd") + "'", "开始时间");
                    sb.Append("*" + drtemp.Length + "," + date.Substring(date.IndexOf("-") + 1));
                    drtemp =
                        dttemp.Select(
                            "查询方式='003' and 开始时间 >= '" + date + "' and 开始时间<'" +
                            DateTime.Parse(date).AddDays(1).ToString("yyyy-MM-dd") + "'", "开始时间");
                    sb.Append("*" + drtemp.Length + "," + date.Substring(date.IndexOf("-") + 1));
                }
                break;
            case "2":
                drtemp = dttemp.Select("", "查询地区");
                sb.Clear();
                tempcounter = 0;
                tempstring = "";
                foreach (DataRow dr in drtemp)
                {
                    if (!dr["查询地区"].Equals(DBNull.Value) && dr["查询地区"].ToString().Trim().Length > 0)
                    {
                        if (tempstring == "")
                        {
                            tempcounter = 1;
                            tempstring = dr["查询地区"].ToString();
                        }
                        else
                        {
                            if (tempstring != dr["查询地区"].ToString().Trim())
                            {
                                sb.Append("|" + tempcounter + "," + tempstring.Replace(",", "").Replace("|", ""));
                                tempcounter = 1;
                                tempstring = dr["查询地区"].ToString();
                            }
                            else
                            {
                                tempcounter += 1;
                            }
                        }
                    }
                }
                break;
            default:
                break;
        }
        return sb.ToString().StartsWith("|") ? sb.ToString().Substring(1) : sb.ToString();
    }

    private string[] ReturnDateSpan(DateTime sd, DateTime ed)
    {
        string tempstring = "";
        if ((ed - sd).Days < 62)
        {
            for (int i = 0; i <= (ed - sd).Days; i++)
            {
                tempstring += "|" + sd.AddDays(i).ToString("yyyy-MM-dd");
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
}