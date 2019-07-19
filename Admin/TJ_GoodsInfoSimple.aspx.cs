using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TJ.BLL; 
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_TJ_GoodsInfoSimple : AuthorPage
{
    private readonly BTJ_GoodsInfo _bll = new BTJ_GoodsInfo(); 
    private readonly BTJ_RegisterCompanys _bcompany = new BTJ_RegisterCompanys(); 
    private readonly BTJ_MeshPoint _bmeshpoint = new BTJ_MeshPoint();  
    private readonly BTB_Products_Type _ptype = new BTB_Products_Type();
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            if (!string.IsNullOrEmpty(Request.QueryString["pcompid"]))
            {
                hf_compid.Value = Request.QueryString["pcompid"].Trim();
            }
            else
            {
                hf_compid.Value = GetCookieCompID();
            }
            _currentindex = 1;
            AspNetPager1.CurrentPageIndex = _currentindex;
            DisplayData(1, AspNetPager1.PageSize); 
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_GoodsInfoAddEditSimple.aspx?cmd=edit&ID={0}',680,680,'产品信息管理')", ID);
        }
        else
        {
            return "";
        }
    } 
    private void DisplayData(int pageIndex, int pageSize)
    {
        string filtertemp = "CompID=" + hf_compid.Value;
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            filtertemp = "CompID =" + GetCookieCompID() + " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
        }
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_GoodsInfo where " + filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_GoodsInfo", filtertemp, "GoodsID", "GoodsID", pageSize);
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        var dataKey = GridView1.DataKeys[e.RowIndex];
        if (dataKey != null)
            _bll.Delete(int.Parse(dataKey["GoodsID"].ToString()));
        //fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);
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
                ((HtmlImage)e.Row.FindControl("editimg")).Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString()));
            } 
            //if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            //{
            //    ((ImageButton) e.Row.Cells[6].Controls[0]).Attributes.Add("onclick",
            //        "javascript:return confirm('你确定要删除当前记录吗?')");
            //} 
        }
    }
 
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;//gridview 显示初始页面 
        DisplayData(1, AspNetPager1.PageSize);
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);
    }

    public string ReturnCompanyName(string CPID)
    {
        return _bcompany.GetList(int.Parse(CPID)).CompName;
    }

    public string ReturnGoodsTypeName(string GoodTypeID)
    {
        return _ptype.GetList(int.Parse(GoodTypeID)).TypeName;
        //return bcompproducttype.GetList(int.Parse(GoodTypeID)).GoodsType;
    }

    public string ReturnMeshPointName(string MeshPointID)
    {
        if (MeshPointID != "" && MeshPointID != "0")
        {
            return _bmeshpoint.GetList(int.Parse(MeshPointID)).MeshPointName;
        }
        else
        {
            return "";
        }
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}