using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using TJ.Model;
using commonlib;
public partial class Admin_TJ_YanLeiCodeActiveInfo : AuthorPage
{
    BTJ_YanLeiCodeActiveInfo bll = new BTJ_YanLeiCodeActiveInfo();
    MTJ_YanLeiCodeActiveInfo mod = new MTJ_YanLeiCodeActiveInfo();
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
            return string.Format("javascript:var win=openWinCenter('TJ_YanLeiCodeActiveInfoAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}',600,450,'TJ_YanLeiCodeActiveInfo')", Sc.EncryptQueryString(ID));
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
         bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["id"].ToString()));
         DisplayData(_currentindex, AspNetPager1.PageSize);
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
            e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
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
        if (inputSearchKeyword.Value.Trim().Length > 0)
       {
         _filtertemp = DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
       }
       else
       {
           _filtertemp = "1=1";
       }
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from TJ_YanLeiCodeActiveInfo where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_YanLeiCodeActiveInfo", _filtertemp, "id", "id", pageSize);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }
    protected void btn_insert_Click(object sender, EventArgs e)
    { 
        string tempsqlstring =
            "select name from sysobjects where xtype='u' and (LEFT(name,1)='T' and LEFT(name,2)<>'TJ' and LEFT(name,2)<>'TB' and LEFT(name,2)<>'TC')";
        var sqldata = new SqlDataAdapter(tempsqlstring, ConfigurationManager.ConnectionStrings["SqlServerConnStringYanLei"].ToString());
        DataTable dt = new DataTable();
        sqldata.Fill(dt);
        foreach (DataRow row in dt.Rows)
        {
            if (!bll.CheckIsExistByFilterString("tablename='" + row[0] + "'"))
            {
                mod = new MTJ_YanLeiCodeActiveInfo();
                mod.tablename = row[0].ToString();
                mod.updatedate = DateTime.Now;
                bll.Insert(mod);
            } 
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToInt32(e.CommandArgument.ToString()) > 0)
        {
            mod = bll.GetList(int.Parse(e.CommandArgument.ToString()));
            string tempsqlstring =
           "select count(id) from "+mod.tablename;
            var sqldata = new SqlDataAdapter(tempsqlstring, ConfigurationManager.ConnectionStrings["SqlServerConnStringYanLei"].ToString());
            DataTable dt = new DataTable();
            sqldata.Fill(dt);
            int totalnum = int.Parse(dt.Rows[0][0].ToString()); 
            tempsqlstring = "select count(id) from " + mod.tablename + " where is_active=1";
            sqldata = new SqlDataAdapter(tempsqlstring, ConfigurationManager.ConnectionStrings["SqlServerConnStringYanLei"].ToString());
            dt = new DataTable();
            sqldata.Fill(dt);
            int activednum = int.Parse(dt.Rows[0][0].ToString());
            mod.totalnum = totalnum;
            mod.activednum = activednum;
            mod.updatedate = DateTime.Now;
            mod.acpercent = Convert.ToDecimal(activednum)/totalnum;
            bll.Modify(mod);
            DisplayData(AspNetPager1.CurrentPageIndex,AspNetPager1.PageSize);
        }
    }
}
