using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_TJ_RegisterInnerAgent : AuthorPage
{
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();
    private readonly MTJ_RegisterCompanys mod = new MTJ_RegisterCompanys();
    public CommonFun comfun = new CommonFun();

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
            GridView1.DataSource =
                bll.GetListsByFilterString("ParentID=" + GetCookieCompID() + " and " + DDLField.SelectedValue +
                                           " like '%" + inputSearchKeyword.Value.Trim() + "%'");
        }
        else
        {
            GridView1.DataSource = bll.GetListsByFilterString("ParentID=" + GetCookieCompID());
        }
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["CompID"].ToString()));
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
        mod.CompID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCompID")).Text.Trim());
        mod.CompTypeID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCompTypeID")).Text.Trim());
        mod.AccTypeID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtAccTypeID")).Text.Trim());
        mod.CTID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCTID")).Text.Trim());
        mod.CompAutherID =
            Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCompAutherID")).Text.Trim());
        mod.CompName = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCompName")).Text.Trim();
        mod.CompLogo = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCompLogo")).Text.Trim();
        mod.CompanyWebSite = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCompanyWebSite")).Text.Trim();
        mod.LegalPerson = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtLegalPerson")).Text.Trim();
        mod.Address = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtAddress")).Text.Trim();
        mod.TelNumber = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtTelNumber")).Text.Trim();
        mod.FaxNumber = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtFaxNumber")).Text.Trim();
        mod.EMail = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtEMail")).Text.Trim();
        mod.ZhuCeZiJin =
            Convert.ToDecimal(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtZhuCeZiJin")).Text.Trim());
        mod.AccountNumber = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtAccountNumber")).Text.Trim();
        mod.RegisterDate =
            Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtRegisterDate")).Text.Trim());
        mod.AuthoredDate =
            Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtAuthoredDate")).Text.Trim());
        mod.DisAuthorDate =
            Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtDisAuthorDate")).Text.Trim());
        mod.CompCode = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCompCode")).Text.Trim();
        mod.TaxRegisterCode = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtTaxRegisterCode")).Text.Trim();
        mod.BusinessLicencePicture =
            ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtBusinessLicencePicture")).Text.Trim();
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
                //((LinkButton)e.Row.Cells[11].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
            }
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview();
    }
}