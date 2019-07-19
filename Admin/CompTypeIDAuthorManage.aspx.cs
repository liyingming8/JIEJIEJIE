using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_CompTypeIDAuthorManage : AuthorPage
{
    private readonly BTJ_BaseClass blbaseclass = new BTJ_BaseClass();
    private readonly BTJ_RoleInfo blrole = new BTJ_RoleInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["RID"] != null && Request.QueryString["RID"].Length > 0)
            {
                HF_RID.Value = Request.QueryString["RID"];
            }
            FillDDL();
            BindCheckBoxList();
            if (HF_RID.Value.Length > 0)
            {
                DDL_Role.SelectedValue = HF_RID.Value;
                DDL_Role_SelectedIndexChanged(sender, e);
            }
        }
    }

    private void FillDDL()
    {
        BTJ_RoleInfo BLR = new BTJ_RoleInfo();
        DDL_Role.DataSource = BLR.GetLists();
        DDL_Role.DataBind();
        DDL_Role.Items.Add(new ListItem("请选择角色...", "0"));
        DDL_Role.SelectedValue = "0";
    }

    private MTJ_RoleInfo dt;

    protected void DDL_Role_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!DDL_Role.SelectedValue.Trim().Equals("0"))
        {
            dt = blrole.GetList(int.Parse(DDL_Role.SelectedValue.Trim()));
            if (dt != null)
            {
                HF_AuthorCompTypeIDString.Value = dt.AuthorCompTypeID.Trim();
                foreach (ListItem litem in CheckBoxList_CompanyType.Items)
                {
                    if ((HF_AuthorCompTypeIDString.Value + ",").Contains("," + litem.Value + ","))
                    {
                        litem.Selected = true;
                    }
                    else
                    {
                        litem.Selected = false;
                    }
                }
            }
        }
    }

    private void ClearSelectedCheckListItem(CheckBoxList checklist)
    {
        foreach (ListItem lm in checklist.Items)
        {
            lm.Selected = false;
        }
    }

    private void BindCheckBoxList()
    {
        CheckBoxList_CompanyType.DataSource = blbaseclass.GetListsByFilterString("ParentID=" + DAConfig.CompanyType);
        CheckBoxList_CompanyType.DataBind();
    }

    protected void Button_OK_Click(object sender, EventArgs e)
    {
        string AuthorCompTypeIDString = "";
        foreach (ListItem item in CheckBoxList_CompanyType.Items)
        {
            if (item.Selected)
            {
                AuthorCompTypeIDString += "," + item.Value;
            }
        }
        if (AuthorCompTypeIDString.Length > 0)
        {
            MTJ_RoleInfo mrole = blrole.GetList(int.Parse(DDL_Role.SelectedValue));
            mrole.AuthorCompTypeID = AuthorCompTypeIDString;
            blrole.Modify(mrole);
        }
    }
}