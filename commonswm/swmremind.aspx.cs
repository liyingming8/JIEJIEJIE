using System;
using System.Data;
using System.Web;
using commonlib;
using Newtonsoft.Json.Linq;
using TJ.BLL;
using TJ.DBUtility;

public partial class commonswm_swmactive : System.Web.UI.Page
{
    DBClass db = new DBClass(); 
    Security sc = new Security();
    public string FromURL = "";
    PGTabExecuteALiGZ tabpg = new PGTabExecuteALiGZ();
    readonly TabExecute _tab = new TabExecute();
    readonly  TabExecutewuliu _tabwuliu = new TabExecutewuliu();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Literal_discription.Text = "友情提示：<br>您的注册信息已提交，我们会尽快审核!";
        }
    } 
}