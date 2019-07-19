using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Collections.Generic;

public partial class Admin_TJ_CompFunctionModleChoose : AuthorPage
{
    private readonly BTJ_CompanyMobileSiteInfo bcompanysite = new BTJ_CompanyMobileSiteInfo();
    private readonly BTJ_SiteMap bll = new BTJ_SiteMap(); 
    private readonly CommonFun commonfun = new CommonFun();
    private IList<MTJ_MobileSite_RoleInfo> mtjMobileSiteRoleList; 
    BTJ_MobileSite_RoleInfo btjMobileSiteRole = new BTJ_MobileSite_RoleInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            mtjMobileSiteRoleList = btjMobileSiteRole.GetListsByFilterString("compid=" + GetCookieCompID());
            FillDataList();
            Fillinput();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        commonfun.DeleteItemsByFilterString("TJ_CompanyMobileSiteInfo", "CompID=" + GetCookieCompID());
        var mcompmobilesite = new MTJ_CompanyMobileSiteInfo();
        HtmlInputText txtShowOrder = null;
        foreach (RepeaterItem item in DataList_FunctionMoudle.Items)
        {
            if (((CheckBox) item.FindControl("ckb_choose")).Checked)
            {
                mcompmobilesite.SiteID = int.Parse(((HiddenField) item.FindControl("HF_SiteID")).Value);
                mcompmobilesite.CompID = int.Parse(GetCookieCompID());
                txtShowOrder = (HtmlInputText) item.FindControl("txtShowOrder");
                if (txtShowOrder.Value.Length > 0)
                {
                    mcompmobilesite.ShowOrder = int.Parse(txtShowOrder.Value);
                }
                else
                {
                    mcompmobilesite.ShowOrder = 0;
                }
                _ckbList = (CheckBoxList) item.FindControl("ckblistridstr");
                mcompmobilesite.RIDStr = GetRIDString(_ckbList);
                bcompanysite.Insert(mcompmobilesite);
            }
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('选取成功!');", true);
    }

    private string returnvlaue = string.Empty;
    private string GetRIDString(CheckBoxList ckblist)
    {
        returnvlaue = string.Empty;
        foreach (ListItem item in ckblist.Items)
        {
            if (item.Selected)
            {
                 if (string.IsNullOrEmpty(returnvlaue))
                 {
                     returnvlaue = item.Value;
                 }
                 else
                 {
                     returnvlaue += "," + item.Value;
                 }
            } 
        }
        return returnvlaue;
    }

    private void Fillinput()
    { 
        var mcompsite = new MTJ_CompanyMobileSiteInfo();
        var msite = new MTJ_SiteMap();
        var bcompany = new BTJ_RegisterCompanys();
        foreach (RepeaterItem item in DataList_FunctionMoudle.Items)
        {
            IList<MTJ_SiteMap> msitelist = bll.GetListsByFilterString("SiteID=" + ((HiddenField) item.FindControl("HF_SiteID")).Value);
            
            if (msitelist.Count > 0)
            {
                msite = msitelist[0];
                ((HtmlImage)item.FindControl("ImageCss")).Src = "Images/mobilemoudle/" + GetCookieCompID() + "/" +
                                                                  bcompany.GetList(int.Parse(GetCookieCompID()))
                                                                      .LogoDirInfo + "/" + msite.LogoName + "";
            }
            IList<MTJ_CompanyMobileSiteInfo> list =
                bcompanysite.GetListsByFilterString("CompID=" + GetCookieCompID() + " and SiteID=" +
                                                    ((HiddenField) item.FindControl("HF_SiteID")).Value);
            if (list.Count > 0)
            {
                mcompsite = list[0];
                ((CheckBox) item.FindControl("ckb_choose")).Checked = true;
                ((HtmlInputText) item.FindControl("txtShowOrder")).Value = mcompsite.ShowOrder.ToString();
            }
        }
    }

    private void FillDataList()
    {
        DataList_FunctionMoudle.DataSource = bll.GetListsByFilterString("ParentID=" + DAConfig.MobileFunctionModleParentID);
        DataList_FunctionMoudle.DataBind();
    }

    private CheckBoxList _ckbList;
    protected void DataList_FunctionMoudle_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            var mod = (MTJ_SiteMap)e.Item.DataItem;
            _ckbList = (CheckBoxList)e.Item.FindControl("ckblistridstr");
            _ckbList.DataSource = mtjMobileSiteRoleList;
            _ckbList.DataBind();
            IList<MTJ_CompanyMobileSiteInfo> list = bcompanysite.GetListsByFilterString("SiteID=" + mod.SiteID + " and CompID="+GetCookieCompID());
            if (list.Count > 0)
            { 
                ckblistselect(_ckbList, list[0].RIDStr);
            } 
        } 
    }

    private void ckblistselect(CheckBoxList ckblist,string ridstring)
    {
        foreach (ListItem item in ckblist.Items)
        {
            if (("," + ridstring + ",").Contains("," + item.Value + ","))
            {
                item.Selected = true;
            }
        }
    }
}