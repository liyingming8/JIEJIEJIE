using System;
using commonlib;
using System.Data;

public partial class TB_wuliu_WXSMreport : AuthorPage
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
            txtStartDate.Text = DateTime.Now.Year + "-" + "01";
            txtEndDate.Text = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month).ToString("yyyy-MM");
            string endtime = Convert.ToDateTime(txtEndDate.Text).AddMonths(1).ToString("yyyy-MM-dd");
            string startime = Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd");
            DataTable wxsm = db.GetSMstyle(startime, endtime, true);
            DataRow[] drtemp = null;
            string cs = "|";
            string time = "|";
            string Cmth;
            string Qcs = "|";
            string[] dataspanarray = ReturnDateSpan(DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text));
            foreach (string date in dataspanarray)
            {
                drtemp =
                    wxsm.Select(" ExplorerType like '%micromessenger%' and  CreateTime>= '" + date + "'and CreateTime<'" +
                                Convert.ToDateTime(date).AddMonths(1).ToString("yyyy-MM-dd") + "'");
                cs += "," + drtemp.Length;
                Cmth = Convert.ToDateTime(date).Year.ToString().Substring(2) + "年" + Convert.ToDateTime(date).Month +
                       "月";


                drtemp =
                    wxsm.Select(" ExplorerType not like '%micromessenger%' and  CreateTime>= '" + date +
                                "'and CreateTime<'" + Convert.ToDateTime(date).AddMonths(1).ToString("yyyy-MM-dd") + "'");
                Qcs += "," + drtemp.Length;

                time += "," + Cmth;
            }
            mh = time.Substring(2);
            wxcs = cs.Substring(2);
            qtcs = Qcs.Substring(2);
        }

        //m = new int[] { 1,2,3,4,5,6,7,8,9,10};
    }


    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        //m = new string[] { "1", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        ////s = "三月,四月";

        string endtime = Convert.ToDateTime(txtEndDate.Text).AddMonths(1).ToString("yyyy-MM-dd");
        string startime = Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd");
        DataTable wxsm = db.GetSMstyle(startime, endtime, true);
        DataRow[] drtemp = null;
        string cs = "|";
        string time = "|";
        string Cmth;
        string Qcs = "|";
        string[] dataspanarray = ReturnDateSpan(DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text));
        foreach (string date in dataspanarray)
        {
            drtemp =
                wxsm.Select(" ExplorerType like '%micromessenger%' and  CreateTime>= '" + date + "'and CreateTime<'" +
                            Convert.ToDateTime(date).AddMonths(1).ToString("yyyy-MM-dd") + "'");
            cs += "," + drtemp.Length;
            Cmth = Convert.ToDateTime(date).Year.ToString().Substring(2) + "年" + Convert.ToDateTime(date).Month + "月";


            drtemp =
                wxsm.Select(" ExplorerType not like '%micromessenger%' and  CreateTime>= '" + date + "'and CreateTime<'" +
                            Convert.ToDateTime(date).AddMonths(1).ToString("yyyy-MM-dd") + "'");
            Qcs += "," + drtemp.Length;

            time += "," + Cmth;
        }
        mh = time.Substring(2);
        wxcs = cs.Substring(2);
        qtcs = Qcs.Substring(2);
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
}