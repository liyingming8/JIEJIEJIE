using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_SWM_Comp_ModulesConfig : AuthorPage
{
    BTJ_SWM_Comp_ModulesConfig bll = new BTJ_SWM_Comp_ModulesConfig(); 
    TabExecute tab = new TabExecute();
    public string LPH = "blue";

    private int _currentindex = 0; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            LPH = GetLogoPath(GetCookieCompID());
             _currentindex = 1;
             AspNetPager1.CurrentPageIndex = 1;
             DisplayData(1, AspNetPager1.PageSize);
        }
    }

    private string GetLogoPath(string compid)
    {
        string tempreturn = ""; ;
        var tab = new TabExecute();
        DataTable dttemp = tab.ExecuteQuery("select logopath from TJ_CompFrontPage_Config where compid=" +compid, null);
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

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_SWM_Comp_ModulesConfigAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}',660,660,'模块信息')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         var dataKey = GridView1.DataKeys[e.RowIndex];
        if (dataKey != null)
        {
            bll.Delete(int.Parse(dataKey["id"].ToString()));
            DisplayData(_currentindex, AspNetPager1.PageSize);
        } 
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
            {
                ((HtmlImage) e.Row.FindControl("img_logo")).Src = (dataKey["logourl"].ToString().Contains("commup")?dataKey["logourl"].ToString():"Images/swmlogo/" + LPH + "/" + dataKey["logourl"]);
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
                ((HtmlImage)e.Row.FindControl("img_edit")).Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString()));  
            } 
        }
    }
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
       _currentindex = 1;
       AspNetPager1.CurrentPageIndex = 1;
       DisplayData(1, AspNetPager1.PageSize);
    }

    private string _filtertemp ="1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        _filtertemp = "compid=" + GetCookieCompID();
        if (inputSearchKeyword.Value.Trim().Length > 0)
       {
         _filtertemp += " and "+  DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
       } 
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from TJ_SWM_Comp_ModulesConfig where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_SWM_Comp_ModulesConfig", _filtertemp, "id", "id", pageSize);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }
}
