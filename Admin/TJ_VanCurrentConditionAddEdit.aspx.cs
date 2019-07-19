using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_VanCurrentConditionAddEdit : AuthorPage
{
    private readonly BTJ_VanCurrentCondition bll = new BTJ_VanCurrentCondition();
    private readonly MTJ_VanCurrentCondition mod = new MTJ_VanCurrentCondition();

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
        mod.VanCurrentConditionID = Convert.ToInt32(inputVanCurrentConditionID.Value.Trim());
        mod.DriverStatuesID = Convert.ToInt32(inputDriverStatuesID.Value.Trim());
        mod.VanID = Convert.ToInt32(inputVanID.Value.Trim());
        mod.FromCTID = Convert.ToInt32(inputFromCTID.Value.Trim());
        mod.TOCTID = Convert.ToInt32(inputTOCTID.Value.Trim());
        mod.UpdateTime = Convert.ToDateTime(inputUpdateTime.Value.Trim());
        mod.StartAfter = Convert.ToInt32(inputStartAfter.Value.Trim());
        mod.WaitForTons = Convert.ToDecimal(inputWaitForTons.Value.Trim());
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
        MTJ_VanCurrentCondition ms = bll.GetList(id);
        inputVanCurrentConditionID.Value = ms.VanCurrentConditionID.ToString().Trim();
        inputDriverStatuesID.Value = ms.DriverStatuesID.ToString().Trim();
        inputVanID.Value = ms.VanID.ToString().Trim();
        inputFromCTID.Value = ms.FromCTID.ToString().Trim();
        inputTOCTID.Value = ms.TOCTID.ToString().Trim();
        inputUpdateTime.Value = ms.UpdateTime.ToString().Trim();
        inputStartAfter.Value = ms.StartAfter.ToString().Trim();
        inputWaitForTons.Value = ms.WaitForTons.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}