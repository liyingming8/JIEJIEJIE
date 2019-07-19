using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_LotteryActivityDetailAddEdit : AuthorPage
{
    private readonly BTJ_LotteryActivityDetail bll = new BTJ_LotteryActivityDetail();
    private MTJ_LotteryActivityDetail mod = new MTJ_LotteryActivityDetail();

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
        mod.LADID = Convert.ToInt32(inputLADID.Value.Trim());
        mod.LAID = Convert.ToInt32(inputLAID.Value.Trim());
        mod.GradeName = inputGradeName.Value.Trim();
        mod.GradeID = Convert.ToInt32(inputGradeID.Value.Trim());
        mod.Numbers = Convert.ToInt32(inputNumbers.Value.Trim());
        mod.AwardName = inputAwardName.Value.Trim();
        mod.AwardPictrureURL = inputAwardPictrureURL.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        //ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
         Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTJ_LotteryActivityDetail ms = bll.GetList(id);
        inputLADID.Value = ms.LADID.ToString().Trim();
        inputLAID.Value = ms.LAID.ToString().Trim();
        inputGradeName.Value = ms.GradeName.Trim();
        inputGradeID.Value = ms.GradeID.ToString().Trim();
        inputNumbers.Value = ms.Numbers.ToString().Trim();
        inputAwardName.Value = ms.AwardName.Trim();
        inputAwardPictrureURL.Value = ms.AwardPictrureURL.Trim();
    }
}