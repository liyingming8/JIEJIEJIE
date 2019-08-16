using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using commonlib;
using Wuqi.Webdiyer;
using TJ.DBUtility;

public partial class Admin_TJ_WHBiaoQianYanZhengJiLu : AuthorPage
{
    int currentIndex = 1;
    TabExecute tab = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            DisplayData(currentIndex, AspNetPager1.PageSize);
        }
    }

    protected void DisplayData(int pageIndex,int pageSize)
    {
        string _filtertemp = " compid=3831"; //+ GetCookieCompID();
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            _filtertemp += " and "+DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
        }
        
        AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(ID) from TJ_UserScan where " + _filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_UserScan", _filtertemp, "LabelCode", "ID", pageSize);
        GridView1.DataBind();
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        DisplayData(1, AspNetPager1.PageSize);
    }

    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        currentIndex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");           
        }
    }

    protected string returnUserName(string UserID) {
        if (UserID.Length>0)
        {
            return tab.ExecuteQueryForSingleValue("select NickName from tj_user where userid='"+UserID+"'");
        }else
        {
            return "";
        }
    }
}