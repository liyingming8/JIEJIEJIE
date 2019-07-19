using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_TJ_GoodsInfo : AuthorPage
{
    private readonly BTJ_GoodsInfo _bll = new BTJ_GoodsInfo(); 
    private readonly BTJ_RegisterCompanys _bcompany = new BTJ_RegisterCompanys();
    private MTJ_GoodsInfo _mod = new MTJ_GoodsInfo();
    private readonly BTJ_MeshPoint _bmeshpoint = new BTJ_MeshPoint(); 
    private readonly BTJ_OrderInfo _border = new BTJ_OrderInfo();
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
            return string.Format("javascript:var win=openWinCenter('TJ_GoodsInfoAddEdit.aspx?cmd=edit&ID={0}',880,600,'产品上架管理')", ID);
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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        _mod = _bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["GoodsID"].ToString()));
        _mod.SaleUnitID = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtSaleUnit")).Text.Trim();
        _mod.GoodsName = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtGoodsName")).Text.Trim();
        _mod.Price = Convert.ToDecimal(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtPrice")).Text.Trim());
        _mod.ShowHomePage = ((CheckBox) GridView1.Rows[e.RowIndex].FindControl("eshowhomepage")).Checked;
        _mod.Recmmand = ((CheckBox) GridView1.Rows[e.RowIndex].FindControl("eRecmmand")).Checked;
        _mod.Hot = ((CheckBox) GridView1.Rows[e.RowIndex].FindControl("eHot")).Checked;
        _mod.Remarks = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtRemarks")).Text.Trim();
        _bll.Modify(_mod);
        GridView1.EditIndex = -1;
        //fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
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
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
            var key = GridView1.DataKeys[e.Row.RowIndex];
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton) e.Row.Cells[10].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')");
            }
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (CheckIsUsed(GridView1.DataKeys[e.Row.RowIndex]["GoodsID"].ToString()))
            //    {
            //        e.Row.Cells[14].Enabled = false;
            //        e.Row.Cells[14].ForeColor = System.Drawing.Color.Gray;
            //    }
            //    else
            //    {
            //        ((LinkButton)e.Row.Cells[14].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
            //        e.Row.Cells[14].ForeColor = System.Drawing.Color.Green;
            //    }
            //}
        }
    }

    private bool CheckIsUsed(string GDID)
    {
        return _border.CheckIsExistByFilterString("GoodsID=" + GDID);
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