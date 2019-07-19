using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_MeshPointImageInfoAddEdit : AuthorPage
{
    private readonly BTJ_MeshPointImageInfo bll = new BTJ_MeshPointImageInfo();
    private MTJ_MeshPointImageInfo mod = new MTJ_MeshPointImageInfo();
    private readonly BTJ_MeshPoint bmeshpoint = new BTJ_MeshPoint();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["MSID"] != null && !Request.QueryString["MSID"].Trim().Equals(""))
            {
                HF_MSID.Value = Request.QueryString["MSID"].Trim();
                Label_MSName.Text = bmeshpoint.GetList(int.Parse(HF_MSID.Value)).MeshPointName;
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
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.MSID = Convert.ToInt32(HF_MSID.Value);
        mod.Show = CheckBoxShow.Checked;
        mod.ImagePathString = HF_ImageURL.Value.Trim();
        if (HF_ImageURL.Value.Contains("/"))
        {
            mod.SMImagePathString = HF_ImageURL.Value.Trim()
                .Insert(HF_ImageURL.Value.Trim().LastIndexOf('/') + 1, "sm_");
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
        Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTJ_MeshPointImageInfo ms = bll.GetList(id);
        HF_ID.Value = ms.MSIMGID.ToString().Trim();
        HF_MSID.Value = ms.MSID.ToString().Trim();
        CheckBoxShow.Checked = ms.Show;
        Image_GoodPic.ImageUrl = ms.ImagePathString.Trim();
        HF_ImageURL.Value = ms.ImagePathString;
        inputRemarks.Value = ms.Remarks.Trim();
    }
}