using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_TJ_jifenjilu : AuthorPage
{
    private readonly BTJ_GoodsInfo bll = new BTJ_GoodsInfo(); 
    private readonly BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();
    private MTJ_GoodsInfo mod = new MTJ_GoodsInfo();
    private readonly BTJ_MeshPoint bmeshpoint = new BTJ_MeshPoint(); 
    private readonly BTJ_OrderInfo border = new BTJ_OrderInfo();
    private readonly BTB_Products_Type ptype = new BTB_Products_Type();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgridview();
        }
    }

    private void fillgridview()
    {
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            //GridView1.DataSource = bll.GetListsByFilterString("CompID in(" + comfun.ReturnChildCompIDString(GetCookieCompID(),true) + ") and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'");
            GridView1.DataSource =
                bll.GetListsByFilterString("CompID =" + GetCookieCompID() + " and " + DDLField.SelectedValue +
                                           " like '%" + inputSearchKeyword.Value.Trim() + "%'");
        }
        else
        {
            GridView1.DataSource = bll.GetListsByFilterString("CompID =" + GetCookieCompID());
        }
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        var dataKey = GridView1.DataKeys[e.RowIndex];
        if (dataKey != null)
        {
            bll.Delete(int.Parse(dataKey["GoodsID"].ToString()));
            fillgridview();
        } 
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgridview();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgridview();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        var dataKey = GridView1.DataKeys[e.RowIndex];
        if (dataKey != null)
            mod = bll.GetList(int.Parse(dataKey["GoodsID"].ToString()));
        mod.SaleUnitID = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtSaleUnit")).Text.Trim();
        mod.GoodsName = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtGoodsName")).Text.Trim();
        mod.Price = Convert.ToDecimal(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtPrice")).Text.Trim());
        mod.ShowHomePage = ((CheckBox) GridView1.Rows[e.RowIndex].FindControl("eshowhomepage")).Checked;
        mod.Recmmand = ((CheckBox) GridView1.Rows[e.RowIndex].FindControl("eRecmmand")).Checked;
        mod.Hot = ((CheckBox) GridView1.Rows[e.RowIndex].FindControl("eHot")).Checked;
        mod.Remarks = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtRemarks")).Text.Trim();
        bll.Modify(mod);
        GridView1.EditIndex = -1;
        fillgridview();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillgridview();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton) e.Row.Cells[14].Controls[0]).Attributes.Add("onclick",
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
        return border.CheckIsExistByFilterString("GoodsID=" + GDID);
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    public string ReturnCompanyName(string CPID)
    {
        return bcompany.GetList(int.Parse(CPID)).CompName;
    }

    public string ReturnGoodsTypeName(string GoodTypeID)
    {
        return ptype.GetList(int.Parse(GoodTypeID)).TypeName;
        //return bcompproducttype.GetList(int.Parse(GoodTypeID)).GoodsType;
    }

    public string ReturnMeshPointName(string MeshPointID)
    {
        if (MeshPointID != "" && MeshPointID != "0")
        {
            return bmeshpoint.GetList(int.Parse(MeshPointID)).MeshPointName;
        }
        else
        {
            return "";
        }
    }
}