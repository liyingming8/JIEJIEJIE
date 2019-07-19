using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_InformationAddEdit : AuthorPage
{
    private readonly BTJ_Information bll = new BTJ_Information();
    private MTJ_Information mod = new MTJ_Information();

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
        mod.IFID = Convert.ToInt32(inputIFID.Value.Trim());
        mod.IFTypeID = Convert.ToInt32(inputIFTypeID.Value.Trim());
        mod.CompID = Convert.ToInt32(inputCompID.Value.Trim());
        mod.UserID = Convert.ToInt32(inputUserID.Value.Trim());
        mod.PublishDate = Convert.ToDateTime(inputPublishDate.Value.Trim());
        mod.TitleInfo = inputTitleInfo.Value.Trim();
        mod.ContentInfo = inputContentInfo.Value.Trim();
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
        MTJ_Information ms = bll.GetList(id);
        inputIFID.Value = ms.IFID.ToString().Trim();
        inputIFTypeID.Value = ms.IFTypeID.ToString().Trim();
        inputCompID.Value = ms.CompID.ToString().Trim();
        inputUserID.Value = ms.UserID.ToString().Trim();
        inputPublishDate.Value = ms.PublishDate.ToString().Trim();
        inputTitleInfo.Value = ms.TitleInfo.Trim();
        inputContentInfo.Value = ms.ContentInfo.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}