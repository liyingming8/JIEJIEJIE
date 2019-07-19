using System;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
public partial class CRM_tj_crm_backmoneylist : AuthorPage
{
    Mtj_crm_backmoneylist mod = new Mtj_crm_backmoneylist();
    PGTabExecuteCRM tab = new PGTabExecuteCRM(); 
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
            return string.Format("javascript:var win=openWinCenter('tj_crm_backmoneylistAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}',600,450,'tj_crm_backmoneylist')", Sc.EncryptQueryString(ID));
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
         tab.ExecuteQuery("delete from tj_crm_backmoneylist where id=" + dataKey["id"],null);
         DisplayData(_currentindex, AspNetPager1.PageSize);
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");  
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex-1) + e.Row.RowIndex + 1).ToString();
            if (IsSuperAdmin())
            {
                ((LinkButton)e.Row.Cells[7].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除当前记录吗?')");
            }
            else
            {
                 e.Row.Cells[7].Enabled= false;
                 e.Row.Cells[7].ForeColor = System.Drawing.Color.LightGray;
            }
        }
    } 
    public string GetUserName(string uid)
    {
        return tab.ExecuteQueryForValue("select customername from public.tj_crm_customerinfo where id=" + uid).ToString();
    }

    public string GetRewardType(string id)
    {
        return tab.ExecuteQueryForValue("select name from public.tj_crm_rewardtype where id=" + id).ToString();
    }

    public string GetOrderNameCode(string id)
    {
        return tab.ExecuteQueryForValue("select brandname,ordercustomername from public.tj_crm_customerorderinfo where id=" + id).ToString();
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
         _filtertemp = DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
       } 
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from tj_crm_backmoneylist where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQuery("SELECT id,(select username from tj_crm_customerinfo where id=fromuid) frnm,(select username from tj_crm_customerinfo where id=touid) tonm,(select ordercustomername||'-'||brandname||'-'||ordernumber from tj_crm_customerorderinfo where orderid=id) ornm,(select name from tj_crm_rewardtype where id=rewardtype) rwnm,bdate,moneynum FROM public.tj_crm_backmoneylist where " + _filtertemp + " limit " + pageSize + " offset " + (pageIndex - 1) * pageSize, null);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }
}
