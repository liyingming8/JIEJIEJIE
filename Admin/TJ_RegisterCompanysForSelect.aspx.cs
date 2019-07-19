using System;
using System.Web.UI.WebControls;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_TJ_RegisterCompanysForSelect : AuthorPage
{
    public CommonFun Comfun = new CommonFun();
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["r"]))
            {
                hf_r.Value = Sc.DecryptQueryString(Request.QueryString["r"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["f"]))
            {
                hf_f.Value = Sc.DecryptQueryString(Request.QueryString["f"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["compid"]))
            {
                hf_objcompid.Value = Request.QueryString["compid"].Trim();
            }
            if (hf_f.Value.Length > 0)
            {
                DisplayData(_currentindex, AspNetPager1.PageSize, ReturnFilterString()); 
            } 
        }
    }

    private string ReturnFilterString()
    {
        if (inputSearchKeyword.Value.Length.Equals(0))
        {
            return hf_f.Value.Length.Equals(0) ? "1=1" : hf_f.Value;
        }
        else
        {
            return (hf_f.Value.Length.Equals(0) ? "" : hf_f.Value + " and ") + " CompName like '%" +inputSearchKeyword.Value + "%'";
        }
    }
 
    private void DisplayData(int pageIndex, int pageSize,string filter)
    {
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(CompID) from TJ_RegisterCompanys where " + filter, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_RegisterCompanys", filter, "CompTypeID", "CompID", pageSize); 
        GridView1.DataBind();
    } 
   
    public string XiangXiLinkString(string compid,string compname)
    {
        if (ID.Length > 0)
        {
            string temp = "";
            if (hf_r.Value.Contains("pcompid"))
            {
                temp =hf_r.Value.Substring(0, hf_r.Value.IndexOf("pcompid", StringComparison.Ordinal)-1);
            }
            else
            {
                temp = hf_r.Value;
            }
            if (temp.Contains("?"))
            {
                return string.Format("javascript:closemyWindowReloadNewhref('" + temp + "&pcompid={0}&pcompnm={1}')", compid, compname);
            }
            return string.Format("javascript:closemyWindowReloadNewhref('" + temp + "?pcompid={0}&pcompnm={1}')", compid, compname);
        }
        return "";
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            var dataKey1 = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey1 != null)
            {
                e.Row.Attributes.Add("onclick", XiangXiLinkString(dataKey1["CompID"].ToString(), dataKey1["CompName"].ToString()));
            }
        }
    } 
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        DisplayData(_currentindex, AspNetPager1.PageSize,ReturnFilterString()); 
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex; 
        DisplayData(_currentindex, AspNetPager1.PageSize,ReturnFilterString()); 
    }
}