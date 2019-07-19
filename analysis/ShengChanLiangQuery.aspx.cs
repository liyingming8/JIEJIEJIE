using System; 
using System.Data;
using System.Text;
using commonlib;

public partial class analysis_ShengChanLiangQuery : AuthorPage
{ 
    DBClass db = new DBClass(DAConfig.Showmode);
    public string resultstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Fresh();
        }
    }

    private void Fresh()
    {
        DataTable dttemp = db.GetShengChanLiangYearAndMonth(DAConfig.Showmode.Equals("1")?"2":GetCookieCompID());
        //DataTable dttemp = db.GetShengChanLiangYearAndMonth("11168");
        StringBuilder sb = new StringBuilder();
        DataRow[] rows = dttemp.Select("YearNum='2016'", "MonthNum");
        //sb.Append("{name: '2016年',");
        //for (int i = 0; i < 12; i++)
        //{
        //    if (i==0)
        //    {
        //        sb.Append("data: [" + rows[i]["sL"]);
        //    }
        //    else
        //    {
        //        sb.Append("," + rows[i]["sL"]);
        //    } 
        //}
        //sb.Append("]}");

        rows = dttemp.Select("YearNum='2017'", "MonthNum");
        sb.Append("{name: '2017年',");
        for (int i = 0; i < 12; i++)
        {
            if (i == 0)
            {
                sb.Append("data: [" + rows[i]["sL"]);
            }
            else
            { 
                sb.Append("," + rows[i]["sL"]);
            }
        }
        sb.Append("]}");

        rows = dttemp.Select("YearNum='2018'", "MonthNum");
        sb.Append(",{name: '2018年',");
        for (int i = 0; i < 12; i++)
        {
            if (i == 0)
            {
                sb.Append("data: [" + rows[i]["sL"]);
            }
            else
            {
                sb.Append("," + rows[i]["sL"]);
            }
        }
        sb.Append("]}");
        sb.Append(",{name: '2019年',");
        rows = dttemp.Select("YearNum='2019'", "MonthNum");
        if (DateTime.Now.Month.Equals(1))
        {
            sb.Append("data: [0");
        }
        else
        {
            for (int i = 0; i < DateTime.Now.Month - 1; i++)
            {
                if (i == 0)
                {
                    sb.Append("data: [" + rows[i]["sL"]);
                }
                else
                {
                    sb.Append("," + rows[i]["sL"]);
                }
            }
        }
        int num = 12 - DateTime.Now.Month;
        for (int i = 1; i <= num; i++)
        {
            sb.Append(",0");
        } 
        sb.Append("]}");
        resultstring = sb.ToString();
        dttemp.Dispose();
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        Fresh();
    }
}