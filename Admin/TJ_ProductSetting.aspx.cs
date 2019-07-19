using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.DBUtility;
using commonlib;
using Wuqi.Webdiyer;
using System.Web.UI.HtmlControls;

public partial class Admin_TJ_ProductSetting : AuthorPage
{
    TabExecutewuliu wuliu = new TabExecutewuliu();
    private readonly BTB_Products_Infor bll = new BTB_Products_Infor();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Display(1, AspNetPager1.PageSize);
        }
    }

    protected void Display(int pageIndex, int pageSize)
    {
        string Filtertemp = " CompID="+GetCookieCompID();
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {

            Filtertemp += " and  " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
        }
        AspNetPager1.RecordCount = int.Parse(wuliu.ExecuteQuery("select count(Infor_ID) from TB_Products_Infor where " + Filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = wuliu.ExecuteQueryByProPagerNew(pageIndex, "TB_Products_Infor", Filtertemp, "Infor_ID", "Infor_ID", pageSize);
        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#CCFF83'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString(), dataKey[1].ToString(), dataKey[2].ToString()));
            var key = GridView1.DataKeys[e.Row.RowIndex];
            ((HtmlImage)e.Row.FindControl("editimg")).Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString(), dataKey[1].ToString(), dataKey[2].ToString()));
            /*
            if (IsSuperAdmin())
            {
                ((LinkButton)e.Row.Cells[13].Controls[0]).Attributes.Add("onclick",
                   "javascript:return confirm('你确定要删除当前记录吗?')");
            }
            else
            {
                e.Row.Cells[13].Enabled = false;
                e.Row.Cells[13].ForeColor = Color.LightGray;
            }
            */
            ((LinkButton)e.Row.Cells[5].Controls[0]).Attributes.Add("onclick",
                "javascript:return confirm('你确定要删除当前记录吗?')");
        }
    }

    public string XiangXiLinkString(string Infor_ID,string Product_Code,string Products_Name)
    {
        if (Infor_ID.Length > 0)
        {
            return
                string.Format(
                    "javascript:var win=openWinCenter('TJ_ProductSettingAndEdited.aspx?cmd=edit&Infor_ID={0}&Product_Code={1}&Products_Name={2}',400,350,'产品管理')", Infor_ID, Product_Code, Products_Name);
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
            bll.Delete(int.Parse(dataKey["Infor_ID"].ToString()));
        }
        Display(1, AspNetPager1.PageSize);
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        Display(1,AspNetPager1.PageSize);
    }

    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        Display(e.NewPageIndex, AspNetPager1.PageSize);
    }
}