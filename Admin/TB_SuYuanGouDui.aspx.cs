using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using TJ.Model;
using commonlib;
using Wuqi.Webdiyer;

public partial class Admin_TB_SuYuanGouDui : AuthorPage
{
    readonly BTB_SuYuanGouDui bll = new BTB_SuYuanGouDui();
    MTB_SuYuanGouDui mod = new MTB_SuYuanGouDui();
    public BTB_SuYuanZhiJianInfo Bzhijian = new BTB_SuYuanZhiJianInfo();
    public BTB_Products_Infor BtbProducts = new BTB_Products_Infor();
    readonly TabExecute tab = new TabExecute();
    readonly BTB_SuYuanZhiQuAndGouDui btbsyzqgd = new BTB_SuYuanZhiQuAndGouDui();
    readonly BTB_SuYuanZhiQu btbsyzq = new BTB_SuYuanZhiQu();
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

    public string ReturnZqpc(string gdid)
    {
         IList<MTB_SuYuanZhiQuAndGouDui> zqgdlist = btbsyzqgd.GetListsByFilterString("GDID=" + gdid);
        if (zqgdlist.Count > 0)
        {
            return btbsyzq.GetList(zqgdlist[0].ZQID).ZhiQuPC;
        }
        return "";
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TB_SuYuanGouDuiAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}',600,400,'勾兑')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    private string LinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TB_SuYuanGouDuiAndZhiJiu.aspx?GDID={0}',700,500,'制酒批次')", Sc.EncryptQueryString(ID));
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
                 bll.Delete(int.Parse(key["ID"].ToString()));
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
            {
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
                ((HyperLink)e.Row.FindControl("hylinkzhijiu")).Attributes.Add("onclick", LinkString(dataKey[0].ToString()));
            } 
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
        _filtertemp = "CompID="+GetCookieCompID(); 
        if (inputSearchKeyword.Value.Trim().Length > 0)
       {
         _filtertemp += " and "+  DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
       } 
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(ID) from TB_SuYuanGouDui where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TB_SuYuanGouDui", _filtertemp, "ID", "ID", pageSize);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }
}
