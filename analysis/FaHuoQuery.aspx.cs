using System;
using System.Data;
using System.Text;
using commonlib;
using TJ.DBUtility;

public partial class analysis_FaHuoQuery : AuthorPage
{
    public StringBuilder Sbfhl = new StringBuilder(); 
    private string mode = "";
    TabExecutewuliu tabexe = new TabExecutewuliu(DAConfig.Showmode);
    Datefrank date  = new Datefrank();
    public int Totalshuliang = 0;
    public  string Dtst = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
    public string Dted = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["md"]))
            {
                mode = Request.QueryString["md"];
            }
            string fahuotablename = "TB_FaHuoInfo_" +(DAConfig.Showmode.Equals("1")?"2":GetCookieCompID());
            string querystring = "";
            DataTable dttemp;
            if (string.IsNullOrEmpty(mode))
            {
                mode = "season";
            }
            ddl_md.SelectedValue = mode;
            switch (mode)
            {
                case "week": 
                    Dtst = date.Weekdays("f");
                    Dted = date.Weekdays("l");   
                    break;
                case "month":
                    Dtst = date.Monthdays("f");
                    Dted = date.Monthdays("l");
                    break;
                case "season":
                    Dtst = date.SeasonDays("f");
                    Dted = date.SeasonDays("l");
                    break;
                case "year":
                    Dtst = date.YearDays("f");
                    Dted = date.YearDays("l");
                    break;
                case "lyear":
                    Dtst = date.LastYearDays("f");
                    Dted = date.LastYearDays("l");
                    break;
            }
            querystring = "SELECT SUM(XiangNumber) as SL,ProID FROM " + fahuotablename + " where  XiangNumber>0 and FHDate>='" + Dtst + "' and FHDate<='" + Convert.ToDateTime(Dted).AddDays(1).ToString("yyyy-MM-dd") + "' group by ProID,Convert(date,FHDate) order by SL desc";
            dttemp = tabexe.ExecuteQuery(querystring, null);
            int cout = 0;
            Totalshuliang = 0;
            int othercout = 0;
            foreach (DataRow row in dttemp.Rows)
            {
                cout++;
                if (cout <= 30)
                {
                    if (string.IsNullOrEmpty(Sbfhl.ToString()))
                    {
                        Totalshuliang += Convert.ToInt32(row["SL"]);
                        if (DAConfig.Showmode.Equals("1"))
                        {
                            Sbfhl.Append("{name: '发货数量',colorByPoint: true,data: [{name: '" + "产品"+cout + "',y:" + row["SL"] + ",sliced: true,selected: true}");
                        }
                        else
                        {
                            Sbfhl.Append("{name: '发货数量',colorByPoint: true,data: [{name: '" + GetProNameByID(row["ProID"].ToString()) + "',y:" + row["SL"] + ",sliced: true,selected: true}");
                        } 
                    }
                    else
                    {
                        Totalshuliang += Convert.ToInt32(row["SL"]);
                        if (DAConfig.Showmode.Equals("1"))
                        {
                            Sbfhl.Append(",{name: '" + "产品"+cout + "',y:" + row["SL"] + "}");
                        }
                        else
                        {
                            Sbfhl.Append(",{name: '" + GetProNameByID(row["ProID"].ToString()) + "',y:" + row["SL"] + "}");
                        } 
                    } 
                }
                else
                { 
                    othercout += Convert.ToInt32(row["SL"]);
                } 
            }
            if (othercout > 0)
            {
                Totalshuliang += Convert.ToInt32(othercout);
                Sbfhl.Append(",{name: '其他',y:" + othercout + "}");
            }
            Sbfhl.Append("]}");
            dttemp.Dispose();
        }
    }

    private string _sqlstring = "";
    private string GetProNameByID(string id)
    {
        _sqlstring = "select Products_Name from TB_Products_Infor where Infor_ID=" + id;
        return  tabexe.ExecuteNonQuery(_sqlstring).Rows[0][0].ToString();
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        Response.Redirect("FaHuoQuery.aspx?md=" + ddl_md.SelectedValue);
    }
}