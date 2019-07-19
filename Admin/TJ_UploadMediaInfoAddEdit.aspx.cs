using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_UploadMediaInfoAddEdit : AuthorPage
{
    private readonly BTJ_UploadMediaInfo bll = new BTJ_UploadMediaInfo();
    private MTJ_UploadMediaInfo mod = new MTJ_UploadMediaInfo();

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
        mod.UPLID = Convert.ToInt32(inputUPLID.Value.Trim());
        mod.UID = Convert.ToInt32(inputUID.Value.Trim());
        mod.UploadDate = Convert.ToDateTime(inputUploadDate.Value.Trim());
        mod.PhysicalPath = inputPhysicalPath.Value.Trim();
        mod.LinkURL = inputLinkURL.Value.Trim();
        mod.MediaType = inputMediaType.Value.Trim();
        mod.Introductions = inputIntroductions.Value.Trim();
        mod.Remarks = inputRemarks.Value.Trim();
        mod.Show = Convert.ToBoolean(inputShow.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTJ_UploadMediaInfo ms = bll.GetList(id);
        inputUPLID.Value = ms.UPLID.ToString().Trim();
        inputUID.Value = ms.UID.ToString().Trim();
        inputUploadDate.Value = ms.UploadDate.ToString().Trim();
        inputPhysicalPath.Value = ms.PhysicalPath.Trim();
        inputLinkURL.Value = ms.LinkURL.Trim();
        inputMediaType.Value = ms.MediaType.Trim();
        inputIntroductions.Value = ms.Introductions.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        inputShow.Value = ms.Show.ToString().Trim();
    }
}