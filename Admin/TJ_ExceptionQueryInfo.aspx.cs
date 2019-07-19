using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using TJ.Model;
using commonlib;
public partial class Admin_TJ_ExceptionQueryInfo : AuthorPage
{
    BTJ_ExceptionQueryInfo bll = new BTJ_ExceptionQueryInfo();
    MTJ_ExceptionQueryInfo mod = new MTJ_ExceptionQueryInfo();
    TabExecute tab = new TabExecute();
    private int _currentindex = 0; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             _currentindex = 1;
             AspNetPager1.CurrentPageIndex = 1;
             DisplayData(1, AspNetPager1.PageSize);
        }
    }

    private string _temp = "";
    public string ResponseType(string type)
    {
        _temp = string.Empty;
        switch (type)
        {
            case "1":
                _temp = "放行";
                break;
            case "2":
                _temp = "第二天再试";
                break;
        }
        return _temp;
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_ExceptionQueryInfoAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}',600,450,'异常查询处理')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        { 
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
            {
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey["id"].ToString()));
                if (Convert.ToBoolean(dataKey["issolved"]))
                {
                    e.Row.BackColor = Color.DarkGreen;
                    e.Row.ForeColor = Color.White;
                }
                else
                {
                    e.Row.BackColor = Color.Red;
                    e.Row.ForeColor = Color.White;
                }
            } 
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
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from TJ_ExceptionQueryInfo where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_ExceptionQueryInfo", _filtertemp, "id desc", "id", pageSize);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }
}
