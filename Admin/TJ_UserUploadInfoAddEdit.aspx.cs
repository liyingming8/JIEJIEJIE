using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_UserUploadInfoAddEdit : AuthorPage
{
    private readonly BTJ_UserUploadInfo bll = new BTJ_UserUploadInfo();
    private MTJ_UserUploadInfo mod = new MTJ_UserUploadInfo();

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
        mod.UULID = Convert.ToInt32(inputUULID.Value.Trim());
        mod.UserID = Convert.ToInt32(inputUserID.Value.Trim());
        mod.CID = Convert.ToInt32(inputCID.Value.Trim());
        mod.PathString = inputPathString.Value.Trim();
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
        MTJ_UserUploadInfo ms = bll.GetList(id);
        inputUULID.Value = ms.UULID.ToString().Trim();
        inputUserID.Value = ms.UserID.ToString().Trim();
        inputCID.Value = ms.CID.ToString().Trim();
        inputPathString.Value = ms.PathString.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}