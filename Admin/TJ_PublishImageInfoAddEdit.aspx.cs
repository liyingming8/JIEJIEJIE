using System;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Web.UI;

public partial class Admin_TJ_PublishImageInfoAddEdit : AuthorPage
{
    private readonly BTJ_PublishInfo bllpublish = new BTJ_PublishInfo();
    private readonly BTJ_PublishImageInfo bll = new BTJ_PublishImageInfo();
    private MTJ_PublishImageInfo mod = new MTJ_PublishImageInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["IFID"] != null && Request.QueryString["IFID"].Trim().Length > 0)
            {
                HF_IFID.Value = Request.QueryString["IFID"];
                Label_IFID.Text = bllpublish.GetList(int.Parse(HF_IFID.Value)).Title;
            }
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
            }
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim();
            }
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
        mod.IFID = int.Parse(HF_IFID.Value);
        mod.PathString = HF_ImageURL.Value;
        if (HF_ImageURL.Value.Contains("/"))
        {
            mod.SMPathString = HF_ImageURL.Value.Trim().Insert(HF_ImageURL.Value.Trim().LastIndexOf('/') + 1, "sm_");
        }
        mod.Remarks = inputRemarks.Value.Trim();
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
        // Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTJ_PublishImageInfo ms = bll.GetList(id);
        HF_ID.Value = ms.IPAID.ToString().Trim();
        HF_IFID.Value = ms.IFID.ToString().Trim();
        HF_ImageURL.Value = ms.PathString.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}