﻿using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using commonlib;
using Wuqi.Webdiyer;

public partial class Admin_TJ_WXBaseInfo : AuthorPage
{
    BTJ_WXBaseInfo bll = new BTJ_WXBaseInfo(); 
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
            return string.Format("javascript:var win=openWinCenter('TJ_WXBaseInfoAddEdit.aspx?cmd=" + Sc.EncryptQueryString("edit") + "&ID={0}',580,460,'微信相关')", Sc.EncryptQueryString(ID));
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
         bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["ID"].ToString()));
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
            if (IsSuperAdmin())
            {
                ((LinkButton)e.Row.Cells[8].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除当前记录吗?')");
            }
            else
            {
                 e.Row.Cells[8].Enabled= false;
                 e.Row.Cells[8].ForeColor = Color.LightGray;
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
        if (inputSearchKeyword.Value.Trim().Length > 0)
       {
         _filtertemp = DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
       }
       else
       {
           _filtertemp = "1=1";
       }
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(ID) from TJ_WXBaseInfo where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_WXBaseInfo", _filtertemp, "ID", "ID", pageSize);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }
}