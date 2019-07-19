using System;
using System.Web.UI.WebControls;
using TJ.BLL; 
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;
using System.Web.UI.HtmlControls;

public partial class Admin_TJ_PublishInfo : AuthorPage
{
    private readonly BTJ_PublishInfo bll = new BTJ_PublishInfo(); 
    private readonly BTJ_InfoType bitemtype = new BTJ_InfoType();
     private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {  
            _currentindex = 1;
            DisplayData(1, AspNetPager1.PageSize);
        }
    }

   
    private void Fillgridview()
    {
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            GridView1.DataSource =
                bll.GetListsByFilterString("CompID=" + GetCookieCompID() +" and " +DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'");
        }
        else
        {
            GridView1.DataSource =
                bll.GetListsByFilterString("CompID=" + GetCookieCompID());

        }
        GridView1.DataBind();
    }
  

     
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            //双击后编辑修改方法
            return string.Format("javascript:var win=openWinCenter('TJ_PublishInfoAddEdit.aspx?cmd=edit&ID={0}',700,700,'内容编辑')", ID);
        }
        else
        {
            return "";
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
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
            ((HtmlImage)e.Row.FindControl("editimg")).Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString()));
            /*
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton) e.Row.Cells[6].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')");
            }
            */
        }
    }
    private string Filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        if (!IsSuperAdmin())
        {
            Filtertemp = "CompID=" + GetCookieCompID();
        }
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_PublishInfo where " + Filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_PublishInfo", Filtertemp, "IFID", "IFID", pageSize);
        GridView1.DataBind();
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        Fillgridview();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        Fillgridview();
    }

    public string ReturnInfoTypeName(string ID)
    {
        return bitemtype.GetList(int.Parse(ID)).TypeName;
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}