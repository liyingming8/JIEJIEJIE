﻿using System;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using TJ.Model;
using commonlib;
using System.Data;

public partial class Admin_TJ_DepartMent : AuthorPage
{
    BTJ_DepartMent bll = new BTJ_DepartMent();
    MTJ_DepartMent mod = new MTJ_DepartMent();
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

    private string GetAuthorProvinceInfo(string departid)
    {
        string tempstring = "";
        DataTable dt = tab.ExecuteQuery("select authorprovincenm from TJ_DepartMent_ProvinceAuthor where departid=" + departid, null);
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach(DataRow dr in dt.Rows)
            {
                if (string.IsNullOrEmpty(tempstring))
                {
                    tempstring = dr[0].ToString();
                }
                else
                {
                    tempstring += "," + dr[0].ToString();
                }
            }
        }
        dt.Dispose();
        return tempstring;
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_DepartMentAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}',680,520,'组织结构')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    public string XiangXiLinkStringForProvinceAuther(string ID,string dnm)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_DepartMent_ProvinceAuthor.aspx?departid={0}&dnm={1}',680,520,'管辖省份')", Sc.EncryptQueryString(ID),Sc.EncryptQueryString(dnm));
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
            {
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
                HyperLink lk_proviceauther = (HyperLink)e.Row.FindControl("lk_proviceauther");
                lk_proviceauther.Attributes.Add("onclick", XiangXiLinkStringForProvinceAuther(dataKey[0].ToString(),dataKey[1].ToString()));
                string temp = GetAuthorProvinceInfo(dataKey[0].ToString());
                if (temp.Length > 0)
                {
                    lk_proviceauther.Text = "已授权";
                    lk_proviceauther.ForeColor = System.Drawing.Color.Red;
                    lk_proviceauther.ToolTip = temp;
                }
            } 
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex-1) + e.Row.RowIndex + 1).ToString();
            if (IsSuperAdmin())
            {
                ((LinkButton)e.Row.Cells[8].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除当前记录吗?')");
            }
            else
            {
                 e.Row.Cells[8].Enabled= false;
                 e.Row.Cells[8].ForeColor = System.Drawing.Color.LightGray;
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
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from TJ_DepartMent where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_DepartMent", _filtertemp, "id", "id", pageSize);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }
}
