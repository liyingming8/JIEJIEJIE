﻿using System;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL; 
using commonlib;
public partial class Admin_TJ_SWM_CommonSuyuan : AuthorPage
{
    BTJ_SWM_CommonSuyuan bll = new BTJ_SWM_CommonSuyuan(); 
    TabExecute tab = new TabExecute();
    private int _currentindex = 0; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["prodid"]) && Request.QueryString["prodid"]!="0")
                {
                    hdpid.Value = Sc.DecryptQueryString(Request.QueryString["pid"]);
                    hdprodid.Value = Sc.DecryptQueryString(Request.QueryString["prodid"]);
                    if (!hdprodid.Value.Equals("0"))
                    {
                        _currentindex = 1;
                        AspNetPager1.CurrentPageIndex = 1;
                        DisplayData(1, AspNetPager1.PageSize);
                    }
                    else
                    {
                        Response.Write("请先绑定产品信息！");
                        topdiv.Visible = false;
                    }
                }
                else
                {
                    Response.Write("请先绑定产品信息！");
                }
            } 
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_SWM_CommonSuyuanAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}&pid={1}',680,600,'溯源信息')", Sc.EncryptQueryString(ID),Sc.EncryptQueryString(hdpid.Value));
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
            ((LinkButton)e.Row.Cells[9].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除当前记录吗?')");
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
        _filtertemp = "pid=" + hdpid.Value;
        if (inputSearchKeyword.Value.Trim().Length > 0)
       {
         _filtertemp = DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
       } 
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from TJ_SWM_CommonSuyuan where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_SWM_CommonSuyuan", _filtertemp, "id", "id", pageSize);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }
}