using System;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Web.UI;

public partial class Admin_TJ_AdInfoAddEdit : AuthorPage
{
    private readonly BTJ_AdInfo bll = new BTJ_AdInfo();
    private MTJ_AdInfo mod = new MTJ_AdInfo();
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
            FillDLL();
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
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.ADName = inputADName.Value.Trim();
        mod.MediaType = Convert.ToInt32(ComboBox_MediaType.SelectedValue);
        mod.MWidth = Convert.ToInt32(inputMWidth.Value.Trim());
        mod.MHeight = Convert.ToInt32(inputMHeight.Value.Trim());
        mod.SiteID = Convert.ToInt32(ComboBox_Site.SelectedValue);
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        //Response.Write("<script>location.href='TJ_AdInfo.aspx'</script>");
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void FillDLL()
    {
        comfun.BindTreeCombox(ComboBox_MediaType, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.AdFileTypeID,
            "请选择...", true, "-", "");
        ComboBox_MediaType.SelectedValue = "0";
        comfun.BindTreeCombox(ComboBox_Site, "PageName", "SiteID", "ParentID", "TJ_SiteMap", DAConfig.MobileMoudlePID,
            "首页", true, "-", "");
        ComboBox_Site.SelectedValue = "0";
    }

    private void fillinput(int id)
    {
        MTJ_AdInfo ms = bll.GetList(id);
        inputADName.Value = ms.ADName.Trim();
        ComboBox_MediaType.SelectedValue = ms.MediaType.ToString().Trim();
        inputMWidth.Value = ms.MWidth.ToString().Trim();
        inputMHeight.Value = ms.MHeight.ToString().Trim();
        ComboBox_Site.SelectedValue = ms.SiteID.ToString().Trim();
    }
}