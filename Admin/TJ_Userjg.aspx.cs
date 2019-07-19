using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_TJ_Userjg : AuthorPage
{
    private readonly BTJ_User bll = new BTJ_User();
    private MTJ_User mod = new MTJ_User();
    private readonly BTJ_RoleInfo bllrole = new BTJ_RoleInfo();
    private readonly BTJ_RegisterCompanys blrcompany = new BTJ_RegisterCompanys();
    private readonly CommonFun comfun = new CommonFun();
    private readonly CommonFunWL comwl = new CommonFunWL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgridview();
        }
    }

    private string returnvalue = "";

    public string ReturnUserStatu(object IsActive)
    {
        switch (IsActive.ToString())
        {
            case "0":
                returnvalue = "未激活";
                break;
            case "1":
                returnvalue = "已激活";
                break;
            case "2":
                returnvalue = "试用中";
                break;
            case "3":
                returnvalue = "已冻结";
                break;
            default:
                returnvalue = "";
                break;
        }
        return returnvalue;
    }

    private void fillgridview()
    {
        if (IsSuperAdmin())
        {
            string sqlstring = "CompID<>0";
            if (inputSearchKeyword.Value.Length > 0)
            {
                sqlstring += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
            }
            GridView1.DataSource = bll.GetListsByFilterString(sqlstring + "and RID is not null and RID<>44", "CompID ");
        }
        else
        {
            string tempagentid = comwl.GetAgentIDStringByCompID(GetCookieCompID());
            string tempchildcompid = comfun.ReturnChildCompIDString(GetCookieCompID(), true);
            string sqlstring = "CompID<>0 and CompID in (" + tempchildcompid +
                               (tempagentid.Length > 0 ? "," + tempagentid : "") + ")";
            //string tempchildcompid = comfun.ReturnChildCompIDString(GetCookieCompID(), false);
            ////string sqlstring="CompID<>0 and CompID in (" + tempchildcompid + (tempagentid.Length > 0 ? "," + tempagentid : "") + ")";
            //string sqlstring = "CompID<>0 and CompID in (" + tempagentid + ")";
            if (inputSearchKeyword.Value.Length > 0)
            {
                sqlstring += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
            }
            GridView1.DataSource = bll.GetListsByFilterString(sqlstring + "and RID is not null and RID<>44", "CompID ");
        }
        GridView1.DataBind();
    }

    public string ReturnRoleNameByRID(string RID)
    {
        if (RID.Equals("0"))
        {
            return "";
        }
        else
        {
            return bllrole.GetList(int.Parse(RID)).RoleName;
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["UserID"].ToString()));
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

    public string ReturnPlaceByID(string CID)
    {
        return comfun.ReturnBaseClassName(CID, true, false);
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["UserID"].ToString()));
        mod.UserID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtUserID")).Text.Trim());
        mod.ParentID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtParentID")).Text.Trim());
        mod.CompID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCompID")).Text.Trim());
        mod.RID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtRID")).Text.Trim());
        mod.IdentityCode = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtIdentityCode")).Text.Trim();
        mod.LoginName = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtLoginName")).Text.Trim();
        mod.PassWords = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtPassWords")).Text.Trim();
        mod.NickName = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtNickName")).Text.Trim();
        mod.RegisterDate =
            Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtRegisterDate")).Text.Trim());
        mod.IsActived = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtIsActived")).Text.Trim());
        mod.FromCityID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtFromCityID")).Text.Trim());
        mod.SystemPermission = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtSystemPermission")).Text.Trim();
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

    public IList<MTJ_RoleInfo> GetRoleInfo()
    {
        return
            bllrole.GetListsByFilterString("RID in(" + bllrole.GetList(int.Parse(GetCookieRID())).AuthorRoleIDInfo + ")");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                if (GridView1.DataKeys[e.Row.RowIndex]["CompID"].ToString().Equals("0"))
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = Color.Gray;
                }
                else
                {
                    Label labactived = (Label) e.Row.FindControl("LabelIsActived");
                    switch (labactived.Text.Trim())
                    {
                        case "未激活":
                            labactived.ForeColor = Color.Red;
                            break;
                        case "已激活":
                            labactived.ForeColor = Color.Green;
                            break;
                        case "试用中":
                            labactived.ForeColor = Color.Orange;
                            break;
                        case "已冻结":
                            labactived.ForeColor = Color.Gray;
                            break;
                    }
                }
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

    public string GetCompanyNameByID(string CID)
    {
        if (CID == "0")
        {
            return "普通消费者";
        }
        else
        {
            return blrcompany.GetList(int.Parse(CID)).CompName;
        }
    }
}