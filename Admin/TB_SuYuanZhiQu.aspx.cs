﻿using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using commonlib;
using Wuqi.Webdiyer;

public partial class Admin_TB_SuYuanZhiQu : AuthorPage
{
    readonly BTB_SuYuanZhiQu bll = new BTB_SuYuanZhiQu(); 
    readonly TabExecute tab = new TabExecute();
    public BTB_SuYuanZhiJianInfo bsyzjy = new BTB_SuYuanZhiJianInfo();
    private int _currentindex = 1; 

    public BTB_SuYuanKuFang BtbSuYuanKuFang = new BTB_SuYuanKuFang();
    public BTB_SuYuanPai BtbSuYuanPai = new BTB_SuYuanPai();
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
            return string.Format("javascript:var win=openWinCenter('TB_SuYuanZhiQuAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}',600,450,'原曲信息')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    public string XiangXiLinkStringZhiQuPeiLiao(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TB_SuYuanZhiQuAndGain.aspx?ZQID={0}',700,500,'原曲配料')", Sc.EncryptQueryString(ID));
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
            ((HyperLink)e.Row.FindControl("hyplink")).Attributes.Add("onclick", XiangXiLinkStringZhiQuPeiLiao(dataKey[0].ToString()));
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex-1) + e.Row.RowIndex + 1).ToString();
            if (IsSuperAdmin())
            {
                ((LinkButton)e.Row.Cells[9].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
            }
            else
            {
                 e.Row.Cells[9].Enabled= false;
                 e.Row.Cells[9].ForeColor = Color.LightGray;
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
        _filtertemp = "Compid=" + GetCookieCompID();
        if (inputSearchKeyword.Value.Trim().Length > 0)
       {
         _filtertemp += " and "+ DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
       } 
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(ID) from TB_SuYuanZhiQu where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TB_SuYuanZhiQu", _filtertemp, "ID desc", "ID", pageSize);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }
}
