using System;
using commonlib;
using Newtonsoft.Json.Linq;
using TJ.BLL;

public partial class analysis_ShiShiJianKong : AuthorPage
{
    BTJ_RegisterCompanys btjRegisterCompanys = new BTJ_RegisterCompanys();
    public string henzuobaostring = "";
    public string zongzuobiaoshtring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Fresh();
        }
    }

    private void Fresh()
    {
        InternetHandle internet = new InternetHandle();
        string compname = btjRegisterCompanys.GetList(int.Parse(DAConfig.Showmode.Equals("1")?"2":GetCookieCompID())).CompName;
        if (GetCookieCompID() == "11168")
        {
            compname = "湖南浏阳河酒厂有限公司";
        }
        string tempstring =
            "http://www.china315net.com:35222/qry/company/period/?company=" + compname + "&period=day&date=" + DateTime.Now.ToString("yyyy-MM-dd");
        string temp = internet.GetUrlData(tempstring);
        JObject obj = JObject.Parse(temp);
        henzuobaostring = obj["categories"].ToString().Replace("\r\n", "");
        zongzuobiaoshtring = obj["data"].ToString().Replace("\r\n", "");
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        Fresh();
    }
}