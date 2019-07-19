using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_SysModuleSiteInfoAddEdit : AuthorPage
{
    BTJ_SysModuleSiteInfo bll = new BTJ_SysModuleSiteInfo();
    MTJ_SysModuleSiteInfo mod = new MTJ_SysModuleSiteInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
       {
           if (!string.IsNullOrEmpty(Request.QueryString["mdid"]))
           {
               input_smid.Value = Sc.DecryptQueryString(Request.QueryString["mdid"]);
               if (!string.IsNullOrEmpty(Request.QueryString["cmd"]))
               {
                   HF_CMD.Value = Sc.DecryptQueryString(Request.QueryString["cmd"].Trim());
               }
               if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
               {
                   HF_ID.Value = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
               }
               fillddl();
               switch (HF_CMD.Value)
               {
                   case "add":
                       Button1.Text = "添加";
                       break;
                   case "edit":
                       Button1.Text = "修改";
                       fillinput(int.Parse(HF_ID.Value.Trim()));
                       break;
                   default:
                       break;
               }
           }
           else
           {
               Response.End();
           } 
       }
    }
    CommonFun comm = new CommonFun();
    private void fillddl()
    { 
        comm.BindTreeCombox(ddl_siteid, "PageName","SiteID","ParentID","TJ_SiteMap",0,"指定目录",true,"-","1=1");
    }

    protected void Button1_Click(object sender, EventArgs e)
    { 
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
           mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        } 
        mod.SMID = Convert.ToInt32(input_smid.Value);
        mod.SiteID = Convert.ToInt32(ddl_siteid.SelectedValue.Trim());
        mod.LinkURL = inputLinkURL.Value.Trim();
        mod.SiteName = inputSiteName.Value.Trim();
        mod.ShowOrder = Convert.ToInt32(inputShowOrder.Value.Trim());
        mod.ShowContent = inputShowContent.Value.Trim();
        mod.IsEnd = ck_IsEnd.Checked; 
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
            //     RecordDealLog(new MTJ_DealLog(0,"TJ_SysModuleSiteInfoAddEdit.aspx","TJ_SysModuleSiteInfo","描述",System.DateTime.Now,int.Parse(GetCookieUIDValue()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
               //  RecordDealLog(new MTJ_DealLog(0,"TJ_SysModuleSiteInfoAddEdit.aspx","TJ_SysModuleSiteInfo","描述",System.DateTime.Now,int.Parse(GetCookieUIDValue()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTJ_SysModuleSiteInfo ms = bll.GetList(id);
        input_smid.Value = ms.SMID.ToString(); 
        ddl_siteid.SelectedValue = ms.SiteID.ToString().Trim();
        inputLinkURL.Value = ms.LinkURL.Trim();
        inputSiteName.Value = ms.SiteName.Trim();
        inputShowOrder.Value = ms.ShowOrder.ToString().Trim();
        inputShowContent.Value = ms.ShowContent.Trim();
        ck_IsEnd.Checked = ms.IsEnd; 
    }

    BTJ_SiteMap bsite = new BTJ_SiteMap();
    protected void ddl_siteid_SelectedIndexChanged(object sender, EventArgs e)
    {
        MTJ_SiteMap msite = bsite.GetList(int.Parse(ddl_siteid.SelectedItem.Value));
        inputSiteName.Value = msite.PageName;
        inputLinkURL.Value = msite.LinkPath; 
    }
}