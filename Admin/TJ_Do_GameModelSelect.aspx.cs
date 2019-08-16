using System;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_Do_GameModelSelect : AuthorPage
{
    BTJ_BaseGames bll = new BTJ_BaseGames(); 
    TabExecute tab = new TabExecute();
    private int _currentindex = 1; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             _currentindex = 1;
             AspNetPager1.CurrentPageIndex = 1;
             DisplayData(1, AspNetPager1.PageSize);
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_GamesAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}',600,450,'TJ_Games')", Sc.EncryptQueryString(ID));
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
            e.Row.Attributes.Add("onclick", "select(" + e.Row.RowIndex + ")");
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex-1) + e.Row.RowIndex + 1).ToString(); 
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
        _filtertemp = "isactive=1";
       if (inputSearchKeyword.Value.Trim().Length > 0)
       {
         _filtertemp += " and "+ DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
       } 
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from TJ_BaseGames where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_BaseGames", _filtertemp, "id", "id", pageSize);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }
}
