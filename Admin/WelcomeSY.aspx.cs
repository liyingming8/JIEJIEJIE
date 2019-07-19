using System;
using System.Web;
using System.Web.UI;
using TJ.BLL;

public partial class WelcomeSY : Page
{
    private DBClass db = new DBClass();
    private BTJ_RiChangLog bwt = new BTJ_RiChangLog();
    private BTJ_QuerLabelCodeJG bcm = new BTJ_QuerLabelCodeJG();
    public BTJ_RegisterCompanys bagent = new BTJ_RegisterCompanys();
    public BTJ_User buser = new BTJ_User();
    public BTB_Products_Infor bpro = new BTB_Products_Infor();

    protected void Page_Load(object sender, EventArgs e)
    {
        //fillgridviewWT();
        //fillgridviewFH();
        //fillgridviewCM();
        //fillgridviewRK();
    }

    public string GetCookieCompID()
    {
        if (Request.Cookies["TJCOMPID"] != null)
        {
            return HttpUtility.UrlDecode(Request.Cookies["TJCOMPID"].Value.Trim());
        }
        else
        {
            return "99";
        }
    }

    //private void fillgridviewWT()
    //{
    //    GridView_WT.DataSource = bwt.GetListsByFilterString("CompID=" + GetCookieCompID() + " and TiJiaoTime>='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and TiJiaoTime<'" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "'");
    //    GridView_WT.DataBind();
    //}
    //private void fillgridviewFH()
    //{
    //    GridView_FH.DataSource = db.GetFH(GetCookieCompID(),"3");
    //    GridView_FH.DataBind();
    //}

    //private void fillgridviewCM()
    //{
    //    GridView_CM.DataSource = bcm.GetListsByFilterString("CompID=" + GetCookieCompID() + "  and firshangchuantime>='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and firshangchuantime<'" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "' ");
    //    GridView_CM.DataBind();
    //}
    //private void fillgridviewRK()
    //{
    //    GridView_RK.DataSource = db.GetFH(GetCookieCompID(), "1");
    //    GridView_RK.DataBind();
    //}
    //protected void GridView_WT_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridView_WT.PageIndex = e.NewPageIndex;
    //    fillgridviewWT();

    //}
    //protected void GridView_FH_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridView_FH.PageIndex = e.NewPageIndex;
    //    fillgridviewFH();

    //}
    //protected void GridView_RK_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridView_RK.PageIndex = e.NewPageIndex;

    //    fillgridviewRK();
    //}
    //protected void GridView_CM_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridView_CM.PageIndex = e.NewPageIndex;
    //    fillgridviewCM();
    //}
}