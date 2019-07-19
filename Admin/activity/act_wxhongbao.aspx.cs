using System;
using System.Web.UI;
using TJ.BLL;

public partial class Admin_activity_act_wxhongbao : Page
{
    BTJ_Activity_BoardInfo btjActivity = new BTJ_Activity_BoardInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillRbList();
        }
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        string temp = "{\"type\":1,\"minvl\":" + inputminvalue.Value + ",\"maxvl\":" + inputminvalue.Value +",\"maxdaynum\":" +inputlimitnumday.Value + "}";
       
    }

    private void FillRbList()
    {
        rbl_activity_board.DataSource = btjActivity.GetListsByFilterString("IsActive=1");
        rbl_activity_board.DataBind();
    }
}