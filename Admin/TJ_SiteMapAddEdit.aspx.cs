using System;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Web.UI;

public partial class Admin_TJ_SiteMapAddEdit : AuthorPage
{
    private readonly BTJ_SiteMap bll = new BTJ_SiteMap();
    private readonly MTJ_SiteMap mod = new MTJ_SiteMap();
    private readonly CommonFun comfun = new CommonFun();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
            }
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim();
            }
            fillcombox();
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
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        mod.SiteID = HF_ID.Value.Trim().Equals("") ? 0 : int.Parse(HF_ID.Value.Trim());
        mod.ParentID = Convert.ToInt32(ComboBox_ParentSiteID.SelectedValue.Trim());
        mod.PageName = inputPageName.Value.Trim();
        mod.LinkPath = inputLinkPath.Value.Trim();
        mod.SysTypeID = int.Parse(ComboBox_SystemType.SelectedValue);
        mod.LogoName = inputsystemlogo.Value;
        mod.Remarks = inputRemarks.Value.Trim();
        mod.ShowOrder = int.Parse(inputShowOrder.Value);
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }

        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        //Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTJ_SiteMap ms = bll.GetList(id);
        ComboBox_ParentSiteID.SelectedValue = ms.ParentID.ToString().Trim();
        inputPageName.Value = ms.PageName.Trim();
        inputLinkPath.Value = ms.LinkPath.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        inputShowOrder.Value = ms.ShowOrder.ToString();
        inputsystemlogo.Value = ms.LogoName;
        ComboBox_SystemType.SelectedValue = ms.SysTypeID.ToString();
        ComboBox_SystemType.SelectedValue = ms.SysTypeID.ToString().Trim();
    }

    private void fillcombox()
    {
        comfun.BindTreeCombox(ComboBox_ParentSiteID, "PageName", "SiteID", "ParentID", "TJ_SiteMap", 0, "请选择", true, "—",
            "");
        ComboBox_ParentSiteID.SelectedValue = "0";
        comfun.BindTreeCombox(ComboBox_SystemType, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.SystemTypeID,
            "请选择", true, "—", "");
        ComboBox_SystemType.SelectedValue = "77";
    }
}