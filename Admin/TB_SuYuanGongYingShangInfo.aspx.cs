using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using TJ.Model;
using commonlib;
using Wuqi.Webdiyer;

public partial class Admin_TB_SuYuanGongYingShangInfo : AuthorPage
{
    readonly BTB_SuYuanGongYingShangInfo bll = new BTB_SuYuanGongYingShangInfo();
    MTB_SuYuanGongYingShangInfo mod = new MTB_SuYuanGongYingShangInfo();
    readonly TabExecute tab = new TabExecute();

    public CommonFun comfun = new CommonFun();
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
            return string.Format("javascript:var win=openWinCenter('TB_SuYuanGongYingShangInfoAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}',600,450,'供应商')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    readonly BTJ_BaseClass _btjBase = new BTJ_BaseClass();
    private string _tempvalue = "";

    public string ReturnCNameByIDString(string idString)
    {
        _tempvalue = "";
        if (idString.Length > 0)
        {
            if (idString.StartsWith(","))
            {
                idString = idString.Substring(1);
            }
            if (idString.EndsWith(","))
            {
                idString = idString.Substring(0, idString.Length - 1);
            }
            IList<MTJ_BaseClass> list = _btjBase.GetListsByFilterString("CID in (" + idString + ")");
            if (list.Count > 0)
            {
                foreach (var mtjBaseClass in list)
                {
                    if (string.IsNullOrEmpty(_tempvalue))
                    {
                        _tempvalue = mtjBaseClass.CName;
                    }
                    else
                    {
                        _tempvalue += "," + mtjBaseClass.CName;
                    }
                }
            }
        }
        return _tempvalue;
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
                ((LinkButton)e.Row.Cells[7].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
            }
            else
            {
                 e.Row.Cells[7].Enabled= false;
                 e.Row.Cells[7].ForeColor = Color.LightGray;
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
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(ID) from TB_SuYuanGongYingShangInfo where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TB_SuYuanGongYingShangInfo", _filtertemp, "ID", "ID", pageSize);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }
}
