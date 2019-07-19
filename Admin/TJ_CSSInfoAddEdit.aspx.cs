using System;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Web.UI;

public partial class Admin_TJ_CSSInfoAddEdit : AuthorPage
{
    private readonly BTJ_CSSInfo bll = new BTJ_CSSInfo();
    private MTJ_CSSInfo mod = new MTJ_CSSInfo();

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
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    break;
                case "edit":
                    Button1.Text = "修改";
                    if (inputLogoDirInfo.Value.Length > 0)
                    {
                        inputLogoDirInfo.Disabled = true;
                    }
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
        mod.CSSName = inputCSSName.Value.Trim();
        mod.FileNamePath = HF_CssURL.Value.Trim();
        mod.PicURL = HF_ImageURL.Value.Trim();
        mod.Remarks = inputRemarks.Value.Trim();
        mod.LogoDirInfo = inputLogoDirInfo.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        // Response.Write("<script>location.href='TJ_CSSInfo.aspx'</script>");

        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void fillinput(int id)
    {
        MTJ_CSSInfo ms = bll.GetList(id);
        HF_CssURL.Value = ms.FileNamePath;
        inputCSSName.Value = ms.CSSName.Trim();
        Label_CssFileName.Text = ms.FileNamePath.Trim();
        HF_ImageURL.Value = ms.PicURL.Trim();
        ImageCssLogo.ImageUrl = ms.PicURL.Trim();
        inputLogoDirInfo.Value = ms.LogoDirInfo;
        inputRemarks.Value = ms.Remarks.Trim();
    }
}