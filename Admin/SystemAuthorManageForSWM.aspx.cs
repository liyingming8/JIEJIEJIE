using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.DBUtility;
using TJ.Model;
using commonlib;

public partial class Admin_SystemAuthorManageForSWM : AuthorPage
{
    private readonly BTJ_SiteMap blsite = new BTJ_SiteMap();
    private readonly BTJ_RoleInfo blrole = new BTJ_RoleInfo();
    private BTJ_Role_Package btjRolePackage = new BTJ_Role_Package();
    TabExecute tab = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["compid"]))
            {
                hf_compid.Value = Sc.DecryptQueryString(Request.QueryString["compid"]);
                DataTable dt = tab.ExecuteQuery("SELECT [mdid] FROM [TJMarketingSystemYin].[dbo].[TJ_SWM_Comp_ModulesConfig] where compid=" + hf_compid.Value, null);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string temp = dt.Rows.Cast<DataRow>().Aggregate(string.Empty, (current, row) => current + (current.Equals("") ? row["mdid"] : "," + row["mdid"]));
                    hf_authorsitedid.Value = temp;
                } 
                BindRepeater(hf_compid.Value);
            }
                
        }
    } 

    private void ClearSelectedCheckListItem(CheckBoxList checklist)
    {
        foreach (ListItem lm in checklist.Items)
        {
            if (("," + hf_authorsitedid.Value + ",").Contains("," + lm.Value + ","))
            {
                lm.Selected = true;
            }
            else
            {
                lm.Selected = false;
            }
        }
    }

    protected void rptSystemMenus_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            CheckBoxList cklist = (CheckBoxList) e.Item.FindControl("CheckList_SubMenus");
            HiddenField hfsiteid = (HiddenField)e.Item.FindControl("HF_SiteID");
            IList<MTJ_SiteMap> list = blsite.GetListsByFilterString("SysTypeID=78 and ParentID<>0 and SiteID in (" + (hfsiteid.Value.StartsWith(",") ? hfsiteid.Value.Substring(1) : hfsiteid.Value) + ")");
            foreach (MTJ_SiteMap mitem in list)
            {
                cklist.Items.Add(new ListItem(mitem.PageName+(mitem.Remarks.Length>0?"["+mitem.Remarks+"]":""), mitem.SiteID.ToString()));
            }
            ClearSelectedCheckListItem(cklist);
        }
    } 
    private void BindRepeater(string compid)
    {
        //rptSystemMenus.DataSource =
        //    tab.ExecuteQuery(
        //        "select id,rpackage,ridstring from [TJMarketingSystemYin].[dbo].TJ_Role_Package rp where rp.id in(SELECT [rpackid] FROM [TJMarketingSystemYin].[dbo].[TJ_Comp_Roles] where compid="+compid+")",
        //        null);
        rptSystemMenus.DataSource = tab.ExecuteQuery("select id,rpackage,ridstring from [TJMarketingSystemYin].[dbo].TJ_Role_Package",null);
        rptSystemMenus.DataBind();
    }

    MTJ_SiteMap modsite = new MTJ_SiteMap();
    protected void Button_OK_Click(object sender, EventArgs e)
    {
        string addstring = "";
        string delstring = "";
        foreach (RepeaterItem ritem in rptSystemMenus.Items)
        { 
            var chklist = (CheckBoxList) ritem.FindControl("CheckList_SubMenus");
            foreach (ListItem item in chklist.Items)
            {
                if (item.Selected)
                {
                    if (!("," + hf_authorsitedid.Value + ",").Contains("," + item.Value + ","))
                    {
                        modsite = blsite.GetList(int.Parse(item.Value));
                        addstring += "insert into TJ_SWM_Comp_ModulesConfig(compid,mdid,mdnm,logourl,ky,lk) values(" +
                                     hf_compid.Value + "," + modsite.SiteID + ",'" + modsite.PageName + "','" +
                                     modsite.LogoName + "','" + modsite.Remarks + "','" + modsite.LinkPath + "');";
                    }
                }
                else
                {
                    if (("," + hf_authorsitedid.Value + ",").Contains("," + item.Value + ","))
                    {
                        delstring += "delete from TJ_SWM_Comp_ModulesConfig where compid=" + hf_compid.Value +
                                     " and mdid=" + item.Value+";";
                    } 
                }
            } 
        }
        if (!string.IsNullOrEmpty(addstring))
        {
            tab.ExecuteNonQuery(addstring, null);
        }
        if (!string.IsNullOrEmpty(delstring))
        {
            tab.ExecuteNonQuery(delstring, null);
        }
        ClientScript.RegisterStartupScript(GetType(), "reload", "closemyWindow();", true); 
    }
}