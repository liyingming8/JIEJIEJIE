using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_VanCarryingAbilityAddEdit : AuthorPage
{
    private readonly BTJ_VanCarryingAbility bll = new BTJ_VanCarryingAbility();
    private readonly MTJ_VanCarryingAbility mod = new MTJ_VanCarryingAbility();

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
        mod.VanCarryAbID = Convert.ToInt32(inputVanCarryAbID.Value.Trim());
        mod.CarryAbility = Convert.ToDecimal(inputCarryAbility.Value.Trim());
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
        MTJ_VanCarryingAbility ms = bll.GetList(id);
        inputVanCarryAbID.Value = ms.VanCarryAbID.ToString().Trim();
        inputCarryAbility.Value = ms.CarryAbility.ToString().Trim();
    }
}