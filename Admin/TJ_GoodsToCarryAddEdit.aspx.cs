using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_GoodsToCarryAddEdit : AuthorPage
{
    private readonly BTJ_GoodsToCarry bll = new BTJ_GoodsToCarry();
    private readonly MTJ_GoodsToCarry mod = new MTJ_GoodsToCarry();

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
        mod.GoodsToCarryID = Convert.ToInt32(inputGoodsToCarryID.Value.Trim());
        mod.FromCTID = Convert.ToInt32(inputFromCTID.Value.Trim());
        mod.ToCTID = Convert.ToInt32(inputToCTID.Value.Trim());
        mod.UserID = Convert.ToInt32(inputUserID.Value.Trim());
        mod.Weight = Convert.ToDecimal(inputWeight.Value.Trim());
        mod.Size = Convert.ToDecimal(inputSize.Value.Trim());
        mod.UpdateTime = Convert.ToDateTime(inputUpdateTime.Value.Trim());
        mod.LoadingTime = Convert.ToDateTime(inputLoadingTime.Value.Trim());
        mod.NeedIntructions = inputNeedIntructions.Value.Trim();
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
        MTJ_GoodsToCarry ms = bll.GetList(id);
        inputGoodsToCarryID.Value = ms.GoodsToCarryID.ToString().Trim();
        inputFromCTID.Value = ms.FromCTID.ToString().Trim();
        inputToCTID.Value = ms.ToCTID.ToString().Trim();
        inputUserID.Value = ms.UserID.ToString().Trim();
        inputWeight.Value = ms.Weight.ToString().Trim();
        inputSize.Value = ms.Size.ToString().Trim();
        inputUpdateTime.Value = ms.UpdateTime.ToString().Trim();
        inputLoadingTime.Value = ms.LoadingTime.ToString().Trim();
        inputNeedIntructions.Value = ms.NeedIntructions.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}