using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_TJ_VanInfo : AuthorPage
{
    private readonly BTJ_VanInfo bll = new BTJ_VanInfo();
    private readonly MTJ_VanInfo mod = new MTJ_VanInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgridview();
        }
    }

    private void fillgridview()
    {
        GridView1.DataSource = bll.GetLists();
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["VanID"].ToString()));
        fillgridview();
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
        mod.VanID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtVanID")).Text.Trim());
        mod.VanTypeID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtVanTypeID")).Text.Trim());
        mod.VanBrandID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtVanBrandID")).Text.Trim());
        mod.VanCarryAbID =
            Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtVanCarryAbID")).Text.Trim());
        mod.VanSizeID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtVanSizeID")).Text.Trim());
        mod.VanMaterID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtVanMaterID")).Text.Trim());
        mod.DriverID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtDriverID")).Text.Trim());
        mod.VanCertifID =
            Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtVanCertifID")).Text.Trim());
        mod.NumberPlate = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtNumberPlate")).Text.Trim();
        mod.VanPicture = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtVanPicture")).Text.Trim();
        mod.VanIntructions = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtVanIntructions")).Text.Trim();
        mod.VehicleLicenseCode = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtVehicleLicenseCode")).Text.Trim();
        mod.VehicleLicensePicture =
            ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtVehicleLicensePicture")).Text.Trim();
        mod.OperationCertificateCode =
            ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtOperationCertificateCode")).Text.Trim();
        mod.OperationCertificatePicture =
            ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtOperationCertificatePicture")).Text.Trim();
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
                ((LinkButton) e.Row.Cells[17].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')");
            }
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            GridView1.DataSource =
                bll.GetListsByFilterString(DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'");
        }
        else
        {
            GridView1.DataSource = bll.GetLists();
        }
        GridView1.DataBind();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview();
    }
}