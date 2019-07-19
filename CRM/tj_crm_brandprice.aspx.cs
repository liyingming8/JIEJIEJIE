﻿using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
using Wuqi.Webdiyer;
using System.Web.UI.HtmlControls;

public partial class CRM_tj_crm_brandprice : AuthorPage
{
    Mtj_crm_brandprice mod = new Mtj_crm_brandprice();
    readonly PGTabExecuteCRM tab = new PGTabExecuteCRM(); 
    public BTJ_User BtjUser = new BTJ_User();
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

    public string GetBrandName(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return "";
        } 
        return tab.ExecuteQueryForValue("select brandname from tj_crm_brandinfo where id=" + id).ToString();
    }

    public string GetCustomerGradeName(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return "";
        }
        return tab.ExecuteQueryForValue("select gradename from tj_crm_customergrade where id=" + id).ToString();
    }

    public string GetUnitName(string id)
    {
        return tab.ExecuteQueryForValue("select unitname from tj_crm_unitinfo where id=" + id).ToString();
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('/crm/tj_crm_brandpriceAddEdit.aspx?cmd=" + Sc.EncryptQueryString("edit")+"&ID={0}',580,420,'品牌价格')", Sc.EncryptQueryString(ID));
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
            var key = GridView1.DataKeys[e.RowIndex];
            if (key != null)
                tab.ExecuteQuery("delete from tj_crm_brandprice where id=" + key["id"],
                    null);
        }
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
            ((HtmlImage)e.Row.FindControl("editimg")).Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString()));
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex-1) + e.Row.RowIndex + 1).ToString();
            /*
            if (IsSuperAdmin())
            {
                ((LinkButton)e.Row.Cells[9].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
            }
            else
            {
                 e.Row.Cells[8].Enabled= false;
                 e.Row.Cells[8].ForeColor = Color.LightGray;
            }
            */
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
         _filtertemp = "compid="+GetCookieCompID()+" and "+ DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
       }
       else
       {
           _filtertemp = "compid="+ GetCookieCompID();
       }
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from tj_crm_brandprice where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQuery("select * from tj_crm_brandprice where " + _filtertemp + " order by brandid,cgradeid limit " + pageSize + " offset " + (pageIndex - 1) * pageSize, null);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }
}