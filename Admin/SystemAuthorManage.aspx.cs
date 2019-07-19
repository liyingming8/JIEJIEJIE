using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_SystemAuthorManage : AuthorPage
{
    private readonly BTJ_SiteMap blsite = new BTJ_SiteMap();
    private readonly BTJ_RoleInfo blrole = new BTJ_RoleInfo();
    private BTJ_Role_Package btjRolePackage = new BTJ_Role_Package();
    BTJ_Role_Package_SiteID btjRolePackageSite = new BTJ_Role_Package_SiteID();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["RID"] != null && Request.QueryString["RID"].Length > 0)
            {
                HF_RID.Value = Request.QueryString["RID"];
                FillDDL(HF_RID.Value.Trim());
                BindRepeater();
                if (HF_RID.Value.Length > 0)
                {
                    DDL_Role.SelectedValue = HF_RID.Value;
                    DDL_Role_SelectedIndexChanged(sender, e);
                }
                HF_RPID.Value = "";
            } 
            if (Request.QueryString["RPID"] != null && Request.QueryString["RPID"].Length > 0)
            {
                DDL_Role.Visible = false;
                HF_RPID.Value = Request.QueryString["RPID"];  
                BindRepeater();
                HF_RID.Value = "";
                SelectCheckBoxItems("", HF_RPID.Value);
            } 
        }
    }


    private void FillDDL(string RID)
    {
        if (RID == "1")
        {
            BTJ_RoleInfo BLR = new BTJ_RoleInfo();
            DDL_Role.DataSource = BLR.GetLists();
            DDL_Role.DataBind();
            DDL_Role.Items.Add(new ListItem("请选择角色...", "0"));
            DDL_Role.SelectedValue = "0";
        }
        else
        {
            BTJ_RoleInfo BLR = new BTJ_RoleInfo();
            DDL_Role.DataSource = BLR.GetListsByFilterString("RID=" + RID);
            DDL_Role.DataBind();
        }
    }

   

    protected void DDL_Role_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!DDL_Role.SelectedValue.Trim().Equals("0") && !string.IsNullOrEmpty(DDL_Role.SelectedValue))
        {
            SelectCheckBoxItems(DDL_Role.SelectedValue, "");
        }
    }

    private void SelectCheckBoxItems(string rid, string rpid)
    {
        if (!string.IsNullOrEmpty(rid))
        {
            MTJ_RoleInfo dt = blrole.GetList(int.Parse(rid));
            if (dt != null)
            {
                HF_AuthorMenuString.Value = dt.AuthorMenuInfo.Trim();
                foreach (RepeaterItem ritem in rptSystemMenus.Items)
                {
                    CheckBoxList chklist = (CheckBoxList)ritem.FindControl("CheckList_SubMenus");
                    foreach (ListItem litem in chklist.Items)
                    {
                        if ((HF_AuthorMenuString.Value + ",").Contains("," + litem.Value + ","))
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
        if (!string.IsNullOrEmpty(rpid))
        {
            MTJ_Role_Package dt = btjRolePackage.GetList(int.Parse(rpid));
            if (dt != null)
            {
                HF_AuthorMenuString.Value = dt.ridstring.Trim();
                foreach (RepeaterItem ritem in rptSystemMenus.Items)
                {
                    CheckBoxList chklist = (CheckBoxList)ritem.FindControl("CheckList_SubMenus");
                    foreach (ListItem litem in chklist.Items)
                    {
                        if ((HF_AuthorMenuString.Value + ",").Contains("," + litem.Value + ","))
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
    }

    private void ClearSelectedCheckListItem(CheckBoxList checklist)
    {
        foreach (ListItem lm in checklist.Items)
        {
            lm.Selected = false;
        }
    }

    protected void rptSystemMenus_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            CheckBoxList cklist = (CheckBoxList) e.Item.FindControl("CheckList_SubMenus");
            HiddenField hfsiteid = (HiddenField) e.Item.FindControl("HF_Parent");
            IList<MTJ_SiteMap> list = blsite.GetListsByFilterString("ParentID=" + hfsiteid.Value);
            foreach (MTJ_SiteMap mitem in list)
            {
                cklist.Items.Add(new ListItem(mitem.PageName+(mitem.Remarks.Length>0?"["+mitem.Remarks+"]":""), mitem.SiteID.ToString()));
            }
        }
    }

    private void BindRepeater()
    {
        string RID = GetCookieRID();
        if (RID == "1")
        {
            rptSystemMenus.DataSource = blsite.GetListsByFilterString("ParentID=0");
            rptSystemMenus.DataBind();
        }
        else
        {
            rptSystemMenus.DataSource = blsite.GetListsByFilterString("ParentID=0 and SiteID not in (1,31,38)");
            rptSystemMenus.DataBind();
        }
    }

    protected void Button_OK_Click(object sender, EventArgs e)
    {
        string roleMenuString = "";
        foreach (RepeaterItem ritem in rptSystemMenus.Items)
        {
            string rowMenuString = "";
            var chklist = (CheckBoxList) ritem.FindControl("CheckList_SubMenus");
            foreach (ListItem item in chklist.Items)
            {
                if (item.Selected)
                {
                    rowMenuString += "," + item.Value;
                }
            }
            if (rowMenuString.Length > 0)
            {
                rowMenuString += "," + ((HiddenField) ritem.FindControl("HF_Parent")).Value;
            }
            roleMenuString += rowMenuString;
        }
        if (roleMenuString.Length > 0)
        {
            if (HF_RID.Value.Length > 0)
            {
                MTJ_RoleInfo mrole = blrole.GetList(int.Parse(DDL_Role.SelectedValue));
                mrole.AuthorMenuInfo = roleMenuString;
                blrole.Modify(mrole);
            }
            if (HF_RPID.Value.Length > 0)
            {
                MTJ_Role_Package mrole = btjRolePackage.GetList(int.Parse(HF_RPID.Value));
                mrole.ridstring = roleMenuString;
                btjRolePackage.Modify(mrole);
                string[] array = roleMenuString.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
                if (array.Length > 0)
                {
                    btjRolePackageSite.Delete("rpid="+HF_RPID.Value+" and siteid not in(" + (roleMenuString.StartsWith(",")?roleMenuString.Substring(1):roleMenuString)+")");
                    foreach (string s in array)
                    {
                        if (!btjRolePackageSite.CheckIsExistByFilterString("rpid=" + HF_RPID.Value + " and siteid=" + s))
                        {
                            btjRolePackageSite.Insert(new MTJ_Role_Package_SiteID(0, int.Parse(HF_RPID.Value),int.Parse(s), ""));
                        }
                    }
                }
            }
        }
    }
}