using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Admin_TJ_WHBuJiang : AuthorPage
{
    public BTJ_User buser = new BTJ_User();
    TabExecute _tab = new TabExecute();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Display(1, AspNetPager1.PageSize);
        }
    }

    protected void Display(int pageIndex, int pageSize)
    {
        
        string Filtertemp = " Compid="+GetCookieCompID();
        if (inputSearchKeyword.Value.Length > 0)
        {
            Filtertemp += " and " + DDLField.SelectedValue + " like  '%" + inputSearchKeyword.Value + "%'";
        }
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TB_SmallBuJiang where " + Filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TB_SmallBuJiang", Filtertemp, "ID", "ID", pageSize);
        GridView1.DataBind();

    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string deleteID= GridView1.DataKeys[e.RowIndex]["ID"].ToString().Trim();
        string mSQL = "delete from TJ_BaseLabelCodeInfo_2019 where ID="+ deleteID;
        _tab.ExecuteNonQuery(mSQL,null);
        Display(1, AspNetPager1.PageSize);
    }

    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        Display(e.NewPageIndex, AspNetPager1.PageSize);
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        Display(1, AspNetPager1.PageSize);
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
        }
    }

    public string XiangXiLinkString(string DanHao)
    {
        if (DanHao.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_WHBuJiangAndEdit.aspx?cmd=edit&DanHao="+ DanHao + "',590,350,'布奖管理')");
        }
        else
        {
            return "";
        }
    }
}