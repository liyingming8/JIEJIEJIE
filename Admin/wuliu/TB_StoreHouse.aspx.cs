using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using TJ.DBUtility;
using commonlib;
using Wuqi.Webdiyer;
using System.Web.UI.HtmlControls;

public partial class Admin_TB_StoreHouse : AuthorPage
{
    private readonly BTB_StoreHouse bll = new BTB_StoreHouse();
    private MTB_StoreHouse mod = new MTB_StoreHouse();
    public CommonFun commfun = new CommonFun();
    private int _currentindex = 1;
    readonly TabExecutewuliu _tab = new TabExecutewuliu(); 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            _currentindex = 1;
            AspNetPager1.CurrentPageIndex = _currentindex;
            DisplayData(1, AspNetPager1.PageSize);

        }
    }
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return
                string.Format(
                    "javascript:var win=openWinCenter('TB_StoreHouseAddEdit.aspx?cmd=edit&ID={0}',520,500,'库房信息编辑')", ID);
        }
        else
        {
            return "";
        }
    }
     
    private void DisplayData(int pageIndex, int pageSize)
    {
        string comp = GetCookieCompID();
        if (comp == "1")
        {
            string Filtertemp = "1=1"; 
            if (inputSearchKeyword.Value.Trim().Length > 0)
            { 
                Filtertemp = " " + DDLField.SelectedValue +" like '%" + inputSearchKeyword.Value.Trim() + "%'"; 
            }

            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TB_StoreHouse where " + Filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TB_StoreHouse", Filtertemp, "STID", "STID", pageSize);
            GridView1.DataBind();
        }
        else
        {
            string Filtertemp = "CompID=" + GetCookieCompID();
            if (inputSearchKeyword.Value.Trim().Length > 0)
            {
                Filtertemp = "CompID =" + GetCookieCompID() + " and " + DDLField.SelectedValue +" like '%" + inputSearchKeyword.Value.Trim() + "%'"; 
            } 
            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TB_StoreHouse where " + Filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TB_StoreHouse", Filtertemp, "STID", "STID", pageSize);
            GridView1.DataBind();
        }
       
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["STID"].ToString()));
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
        //fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["STID"].ToString()));
        mod.StoreHouseName = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtStoreHouseName")).Text.Trim();
        mod.Remarks = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtRemarks")).Text.Trim();
        mod.AddressString = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtAddressString")).Text.Trim();
        mod.Contractor = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtContractor")).Text.Trim();
        mod.TelPhoneNumber = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtTelPhoneNumber")).Text.Trim();
        mod.MobilePhone = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtMobilePhone")).Text.Trim();
        bll.Modify(mod);
        GridView1.EditIndex = -1;
        // fillgridview();
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
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#CCFF83'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
                
            ((HtmlImage)e.Row.FindControl("editimg")).Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString()));
            e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
            /*
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton) e.Row.Cells[7].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')");
            }
            */
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        //fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }



}