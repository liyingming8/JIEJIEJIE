﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using commonlib;
using TJ.Model;

public partial class Admin_TJ_Role_Package : AuthorPage
{
    BTJ_Role_Package bll = new BTJ_Role_Package();
    TabExecute tab = new TabExecute();
    BTJ_SiteMap bsitemap = new BTJ_SiteMap(); 
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

    public string ReturnRoleName(string RPID)
    {
        MTJ_Role_Package mtjRolePackage = bll.GetList(int.Parse(RPID));
        string ridstring = mtjRolePackage.ridstring;
        ridstring = ridstring.StartsWith(",") ? ridstring.Substring(1):ridstring;
        if (ridstring.Length > 0)
        {
            IList<MTJ_SiteMap> rolellist = bsitemap.GetListsByFilterString("SiteID in (" + ridstring + ")");
            if (rolellist.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (MTJ_SiteMap info in rolellist)
                {
                    sb.Append("," + info.PageName + (info.Remarks.Length > 0 ? "[" + info.Remarks + "]" : ""));
                }
                return sb.ToString();
            }
        }
        return "";
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_Role_PackageAddEdit.aspx?cmd=" + Sc.EncryptQueryString("edit") + "&ID={0}',600,580,'功能打包')", Sc.EncryptQueryString(ID));
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
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
                ((HyperLink)e.Row.FindControl("hlinkSystemAuthor")).Attributes.Add("onclick", LinkStringForWindowsShow("SystemAuthorManage.aspx?RPID=" + dataKey[0], "800", "600", "功能授权"));
            }

            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex - 1) + e.Row.RowIndex + 1).ToString();

            if (IsSuperAdmin())
            {
                ((LinkButton)e.Row.Cells[6].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除当前记录吗?')");
            }
            else
            {
                e.Row.Cells[6].Enabled = false;
                e.Row.Cells[6].ForeColor = System.Drawing.Color.LightGray;
            }
        }
    }
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        DisplayData(1, AspNetPager1.PageSize);
    }

    private string _filtertemp = "1=1";
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
        AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from TJ_Role_Package where " + _filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_Role_Package", _filtertemp, "showorder", "id", pageSize);
        GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}
