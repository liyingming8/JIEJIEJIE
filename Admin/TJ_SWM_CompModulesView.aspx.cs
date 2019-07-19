using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using commonlib;
using TJ.BLL;
using TJ.DBUtility;
using TJ.Model;

public partial class Admin_TJ_SWM_CompModulesView : AuthorPage
{
    readonly commfrank _commfun = new commfrank();
    private InternetHandle intenet = new InternetHandle();
    public string LPH = "blue";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
            {
                hdpid.Value = Request.QueryString["pid"];
                LPH = GetLogoPath(GetCookieCompID()); 
                Bindgridview();
            }
        }
    }

    private void Bindgridview()
    {
        string tempstring = intenet.GetUrlData("http://os.china315net.com/ajax/commswm/getpageinfo.ashx?pid=" + hdpid.Value);
        DataTable dttemp = _commfun.ToDataTable(tempstring);
        datalist.DataSource = dttemp;
        datalist.DataBind();
    }

    private string GetLogoPath(string compid)
    {
        string tempreturn = ""; ;
        var tab = new TabExecute();
        DataTable dttemp = tab.ExecuteQuery(
            "select logopath from TJ_CompFrontPage_Config where compid=" +
            compid, null);
        if (dttemp.Rows.Count > 0)
        {
            tempreturn = dttemp.Rows[0]["logopath"].ToString();
        }
        else
        {
            tempreturn = "blue";
        }
        dttemp.Dispose();
        return tempreturn;
    }

    readonly BTJ_SWM_Comp_ModulesConfig _btjSwmCompModules = new BTJ_SWM_Comp_ModulesConfig();
    MTJ_SWM_Comp_ModulesConfig _mtjSwmCompModules = new MTJ_SWM_Comp_ModulesConfig();
    
    protected void btn_setbymyself_Click(object sender, EventArgs e)
    {
        foreach (DataListItem row in datalist.Items)
        {
            if (row.ItemType == ListItemType.Item || row.ItemType == ListItemType.AlternatingItem)
            { 
                _mtjSwmCompModules = new MTJ_SWM_Comp_ModulesConfig(0, int.Parse(GetCookieCompID()), int.Parse(((HtmlInputHidden)row.FindControl("hdid")).Value), ((HtmlInputHidden)row.FindControl("hdpg")).Value, ((HtmlInputHidden)row.FindControl("hdlg")).Value, true, ((HtmlInputHidden)row.FindControl("hdky")).Value, ((HtmlInputHidden)row.FindControl("hdlk")).Value, DateTime.Now,0);
                bool isexist = _btjSwmCompModules.CheckIsExistByFilterString("compid=" + GetCookieCompID() + " and mdid=" + _mtjSwmCompModules.mdid);
                if (!isexist)
                {
                    _btjSwmCompModules.Insert(_mtjSwmCompModules);
                }
            }
        }
        var tab = new TabExecute();
        tab.ExecuteNonQuery("update TJ_RegisterCompanys set CustomerModule=1 where CompID=" + GetCookieCompID(), null);
        Response.Redirect("TJ_SWM_Comp_ModulesConfig.aspx?pid="+hdpid.Value,true);
        //Server.Transfer("TJ_SWM_Comp_ModulesConfig.aspx?pid=" + hdpid.Value,true);
    }
}