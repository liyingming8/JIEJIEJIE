using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_REG_TJ_RegisterCompanys : AuthorPage
{
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();
    private readonly MTJ_RegisterCompanys mod = new MTJ_RegisterCompanys();
    public CommonFun comfun = new CommonFun();
    private readonly BTJ_User buser = new BTJ_User();
    private CommonFunWL comwl = new CommonFunWL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HF_TempChildCompIDString.Value = comfun.ReturnChildCompIDString(GetCookieCompID(), false);
            fillgridview(HF_TempChildCompIDString.Value);
        }
    }

    private void fillgridview(string tempcomidstring)
    {
        if (IsSuperAdmin())
        {
            if (inputSearchKeyword.Value.Trim().Length > 0)
            {
                GridView1.DataSource =
                    bll.GetListsByFilterString(
                        DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'", "CompTypeID");
            }
            else
            {
                GridView1.DataSource = bll.GetLists("CompTypeID");
            }
        }
        else
        {
            if (tempcomidstring.Length > 0)
            {
                if (inputSearchKeyword.Value.Trim().Length > 0)
                {
                    GridView1.DataSource =
                        bll.GetListsByFilterString(
                            "CompID in (" + tempcomidstring + ") and " + DDLField.SelectedValue + " like '%" +
                            inputSearchKeyword.Value.Trim() + "%'", "CompTypeID");
                }
                else
                {
                    GridView1.DataSource = bll.GetListsByFilterString("CompID in (" + tempcomidstring + ") ",
                        "CompTypeID");
                }
            }
            else
            {
                GridView1.DataSource = null;
            }
        }
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["CompID"].ToString()));
        fillgridview((HF_TempChildCompIDString.Value.Length > 0 ? HF_TempChildCompIDString.Value + "," : "") +
                     HF_TempAgentCompIDString.Value);
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgridview((HF_TempChildCompIDString.Value.Length > 0 ? HF_TempChildCompIDString.Value + "," : "") +
                     HF_TempAgentCompIDString.Value);
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgridview((HF_TempChildCompIDString.Value.Length > 0 ? HF_TempChildCompIDString.Value + "," : "") +
                     HF_TempAgentCompIDString.Value);
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
        fillgridview((HF_TempChildCompIDString.Value.Length > 0 ? HF_TempChildCompIDString.Value + "," : "") +
                     HF_TempAgentCompIDString.Value);
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillgridview((HF_TempChildCompIDString.Value.Length > 0 ? HF_TempChildCompIDString.Value + "," : "") +
                     HF_TempAgentCompIDString.Value);
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((bll.CheckIsExistByFilterString("ParentID=" + GridView1.DataKeys[e.Row.RowIndex][0]) == false) &&
                (buser.CheckIsExistByFilterString("CompID=" + GridView1.DataKeys[e.Row.RowIndex][0]) != false))
            {
                ((LinkButton) e.Row.Cells[13].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')");
            }
            else
            {
                e.Row.Cells[13].Enabled = false;
                e.Row.Cells[13].ForeColor = Color.LightGray;
            }
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        fillgridview((HF_TempChildCompIDString.Value.Length > 0 ? HF_TempChildCompIDString.Value + "," : "") +
                     HF_TempAgentCompIDString.Value);
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview((HF_TempChildCompIDString.Value.Length > 0 ? HF_TempChildCompIDString.Value + "," : "") +
                     HF_TempAgentCompIDString.Value);
    }
}