using System;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using TJ.Model;
using commonlib;
public partial class Admin_TJ_AwardInfo_Images : AuthorPage
{
    BTJ_AwardInfo_Images bll = new BTJ_AwardInfo_Images();
    MTJ_AwardInfo_Images mod = new MTJ_AwardInfo_Images();
    TabExecute tab = new TabExecute();
    private int _currentindex = 0; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["awid"]))
            {
                hd_awid.Value = Request.QueryString["awid"];
                hd_awnm.Value = Request.QueryString["awnm"];
                _currentindex = 1;
                AspNetPager1.CurrentPageIndex = 1;
                DisplayData(1, AspNetPager1.PageSize);
                add.Attributes.Add("onclick", "openWinCenter('TJ_AwardInfo_ImagesAddEdit.aspx?awid="+hd_awid.Value+"&cmd="+Sc.EncryptQueryString("add")+"', 500, 430, '图像编辑')");
            } 
        }
    }

    public string XiangXiLinkString(string idstr)
    {
        if (idstr.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_AwardInfo_ImagesAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+ "&ID={0}&awid={1}',500,430,'图像编辑')", Sc.EncryptQueryString(idstr),hd_awid.Value);
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
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex-1) + e.Row.RowIndex + 1).ToString();
            DataKey key = GridView1.DataKeys[e.Row.RowIndex];
            if (key != null)
            {
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(key[0].ToString()));
            }
            if (IsSuperAdmin())
            {
                ((LinkButton)e.Row.Cells[5].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除当前记录吗?')");
            }
            else
            {
                 e.Row.Cells[5].Enabled= false;
                 e.Row.Cells[5].ForeColor = System.Drawing.Color.LightGray;
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
        _filtertemp = "awid="+hd_awid.Value;
        if (inputSearchKeyword.Value.Trim().Length > 0)
       {
         _filtertemp += " and "+ DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
       } 
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from TJ_AwardInfo_Images where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_AwardInfo_Images", _filtertemp, "id", "id", pageSize);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }
}
