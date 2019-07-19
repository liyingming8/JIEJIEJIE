using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_RoleAuthorManage : AuthorPage
{
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
                HF_AuthorRoleIDString.Value = dt.AuthorRoleIDInfo.Trim();
                foreach (ListItem litem in CheckBoxList_RoleInfo.Items)
                {
                    if ((HF_AuthorRoleIDString.Value + ",").Contains("," + litem.Value + ","))
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
        CheckBoxList_RoleInfo.DataSource = blrole.GetLists();
        CheckBoxList_RoleInfo.DataBind();
    }

    protected void Button_OK_Click(object sender, EventArgs e)
    {
        string RoleIDString = "";
        foreach (ListItem item in CheckBoxList_RoleInfo.Items)
        {
            if (item.Selected)
            {
                RoleIDString += "," + item.Value;
            }
        }
        if (RoleIDString.Length > 0)
        {
            MTJ_RoleInfo mrole = blrole.GetList(int.Parse(DDL_Role.SelectedValue));
            mrole.AuthorRoleIDInfo = RoleIDString;
            blrole.Modify(mrole);
        }
    }
}