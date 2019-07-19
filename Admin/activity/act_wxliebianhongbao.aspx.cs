using System;
using System.Web.UI;

public partial class Admin_activity_act_wxliebianhongbao : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        string temp = "{\"type\":1,\"minvl\":" + inputminvalue.Value + ",\"maxvl\":" + inputminvalue.Value +",\"maxdaynum\":" + limitnumday.Value + "}";
       
    }
}