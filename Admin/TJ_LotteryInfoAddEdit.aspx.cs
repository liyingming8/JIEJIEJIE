using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_LotteryInfoAddEdit : AuthorPage
{
    private readonly BTJ_LotteryInfo bll = new BTJ_LotteryInfo();
    private MTJ_LotteryInfo mod = new MTJ_LotteryInfo();

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
        mod.LTID = Convert.ToInt32(inputLTID.Value.Trim());
        mod.CompID = Convert.ToInt32(inputCompID.Value.Trim());
        mod.LAID = Convert.ToInt32(inputLAID.Value.Trim());
        mod.LabelCode = inputLabelCode.Value.Trim();
        mod.WinDate = Convert.ToDateTime(inputWinDate.Value.Trim());
        mod.UserID = Convert.ToInt32(inputUserID.Value.Trim());
        mod.LotteryGrade = Convert.ToInt32(inputLotteryGrade.Value.Trim());
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
        MTJ_LotteryInfo ms = bll.GetList(id);
        inputLTID.Value = ms.LTID.ToString().Trim();
        inputCompID.Value = ms.CompID.ToString().Trim();
        inputLAID.Value = ms.LAID.ToString().Trim();
        inputLabelCode.Value = ms.LabelCode.Trim();
        inputWinDate.Value = ms.WinDate.ToString().Trim();
        inputUserID.Value = ms.UserID.ToString().Trim();
        inputLotteryGrade.Value = ms.LotteryGrade.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}