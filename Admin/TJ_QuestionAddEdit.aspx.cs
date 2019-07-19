using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_QuestionAddEdit : AuthorPage
{
    private readonly BTJ_Question bll = new BTJ_Question();
    private MTJ_Question mod = new MTJ_Question();

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
        mod.id = Convert.ToInt32(inputid.Value.Trim());
        mod.WJID = Convert.ToInt32(inputWJID.Value.Trim());
        mod.Contents = inputContents.Value.Trim();
        mod.Compid = Convert.ToInt32(inputCompid.Value.Trim());
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
        ClientScript.RegisterStartupScript(GetType(), "reload", "window.opener.location.reload();window.close();", true);
    }

    private void fillinput(int id)
    {
        MTJ_Question ms = bll.GetList(id);
        inputid.Value = ms.id.ToString().Trim();
        inputWJID.Value = ms.WJID.ToString().Trim();
        inputContents.Value = ms.Contents.Trim();
        inputCompid.Value = ms.Compid.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}