using System; 
using System.Drawing;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
 

public partial class Admin_Join_FenLeiJoinSaoMaLiang : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            txtStartDate.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-01";
            txtEndDate.Text = Convert.ToDateTime(DateTime.Now.Year + "-" + (DateTime.Now.Month ) + "-01").AddDays(1).ToString("yyyy-MM-dd");
            BtnSearch0_Click(sender, e);
        }
    }
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        Series s1 = new Series("扫码量");
        Chart1.Series.Add(s1);
        Title xtitle = new Title();
        Title ytitle = new Title();
        xtitle = new Title(RBL_Item.SelectedItem.Text, Docking.Bottom);
        Chart1.Titles.Add(xtitle);
        ytitle = new Title("扫码量", Docking.Left);
        ytitle.TextOrientation = TextOrientation.Stacked;
        Chart1.Titles.Add(ytitle);
       
        switch(RBL_Item.SelectedValue)
        {
            case "1":
                Chart1.Height = 450;
                Chart1.Width = 600;
                s1.ChartType = SeriesChartType.Doughnut;
                break;
            case "2":
                Chart1.Height = 450;
                Chart1.Width = 600;
                s1.ChartType = SeriesChartType.Doughnut;
                break;
            case "3":
                Chart1.Height = 450;
                Chart1.Width = 600;
                s1.ChartType = SeriesChartType.Pyramid;
                break;
        }
        s1.Color = Color.Orange;
        s1.BorderWidth = 1;
        string[] temparray = GetSaoMaLiang(RBL_Item.SelectedValue).Split('|');
        double[] yValues1 = new double[temparray.Length];
        string[] xValues = new string[temparray.Length];
        for (int i = 0; i < temparray.Length; i++)
        {
            yValues1[i] = double.Parse(temparray[i].Split(',')[1].Trim());
            xValues[i] = temparray[i].Split(',')[0].Trim();
        }
        Chart1.Series["扫码量"].Points.DataBindXY(xValues, yValues1);
    }

    string tempreturnstring = "";
    private string GetSaoMaLiang(string JoinByID)
    {
        tempreturnstring = "";
        switch(int.Parse(RBL_Item.SelectedValue))
        {
            case 1:
                tempreturnstring ="陕西,10000| 内蒙,2000|甘肃,500|北京,900|河北,500|湖南,1000|湖北,900|上海,1300|江苏,600|福建,300|广东,390|广西,400|海南,3000|西藏,100";
                break;
            case 2:
                tempreturnstring = "10-20岁,50|20-30岁,1000|30-40岁,1200|40-50岁,500|50-60岁,300|60-80岁,30";
                break;
            case 3:
                tempreturnstring = "男性,80|女性,20";
                break;
        }
        return tempreturnstring;
    }
}