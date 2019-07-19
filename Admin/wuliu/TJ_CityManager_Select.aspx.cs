using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using commonlib;
using TJ.BLL;
using TJ.DBUtility;

public partial class Admin_wuliu_TJ_CityManager_Select : AuthorPage
{
    private TabExecute tab = new TabExecute();
    public BTJ_DepartMent BtjDepart = new BTJ_DepartMent();
    private int _currentindex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["fr"]))
            {
                fr.Value = Sc.DecryptQueryString(Request.QueryString["fr"]);
                if (!string.IsNullOrEmpty(Request.QueryString["ctmnnm"]))
                {
                    inputSearchKeyword.Value = Sc.DecryptQueryString(Request.QueryString["ctmnnm"]);
                }
                FillDdl();
                DisplayData(1, AspNetPager1.PageSize);
            } 
        }
    }
   
    private void FillDdl()
    {
        if (IsCompGrade())
        {
            string sqlstring = "select id,department from TJ_DepartMent where compid=" + GetCookieCompID() +
                               " and parentid=" + GetCookieCompID();
            ddl_departid.DataSource = tab.ExecuteQuery(sqlstring, null);
            ddl_departid.DataBind();
        }
        else
        {
            ddl_departid.Visible = false;
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        DisplayData(_currentindex > 0 ? _currentindex : 1, AspNetPager1.PageSize);
    }

    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(_currentindex,AspNetPager1.PageSize);
    }

    private string _filtertemp = "";
    private void DisplayData(int pageIndex, int pageSize)
    {
        if (IsCompGrade())
        {
            _filtertemp = "CompID=" + GetCookieCompID() + " and RID=168";
            if (!ddl_departid.SelectedValue.Equals("0"))
            {
                _filtertemp += " and departid=" + ddl_departid.SelectedValue;
            }
            if (inputSearchKeyword.Value.Length > 0)
            {
                _filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
            } 
        }
        else
        {
            _filtertemp = "CompID=" + GetCookieCompID() + " and RID=168 and departid="+GetCookieTJDepartID();
            if (inputSearchKeyword.Value.Length > 0)
            {
                _filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
            }
        }
        AspNetPager1.RecordCount = int.Parse(tab.ExecuteQueryForSingleValue("select count(UserID) from TJ_User where " + _filtertemp));
        GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_User", _filtertemp, "UserID", "UserID", pageSize);
        GridView1.DataBind();
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
                e.Row.Attributes.Add("onclick", XiangXiLinkStringForReturn(dataKey["UserID"].ToString(), dataKey["LoginName"].ToString()));
            }
        }
    }

    public string XiangXiLinkStringForReturn(string userid, string username)
    {
        if (userid.Length > 0)
        {
            return string.Format("javascript:closemyWindowReloadNewhref('" + fr.Value + "&smuid=" +userid + "&smunm=" + username + "')");
        }
        return "";
    }
}