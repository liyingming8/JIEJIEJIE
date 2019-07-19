using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_QuestionnaireAddEdit : AuthorPage
{
    private readonly BTJ_Questionnaire bll = new BTJ_Questionnaire();
    private MTJ_Questionnaire mod = new MTJ_Questionnaire();

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
        mod.Title = inputTitle.Value.Trim();
        mod.StartTime = Convert.ToDateTime(inputStartTime.Value.Trim());
        mod.EndTime = Convert.ToDateTime(inputEndTime.Value.Trim());
        mod.CreatTime = Convert.ToDateTime(inputCreatTime.Value.Trim());
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
        MTJ_Questionnaire ms = bll.GetList(id);
        inputid.Value = ms.id.ToString().Trim();
        inputTitle.Value = ms.Title.Trim();
        inputStartTime.Value = ms.StartTime.ToString().Trim();
        inputEndTime.Value = ms.EndTime.ToString().Trim();
        inputCreatTime.Value = ms.CreatTime.ToString().Trim();
        inputCompid.Value = ms.Compid.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}