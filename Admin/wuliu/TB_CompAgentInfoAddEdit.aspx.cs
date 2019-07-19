using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TB_CompAgentInfoAddEdit : AuthorPage
{
    private readonly BTB_CompAgentInfo bll = new BTB_CompAgentInfo();
    private MTB_CompAgentInfo mod = new MTB_CompAgentInfo();

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
        mod.CAID = Convert.ToInt32(inputCAID.Value.Trim());
        mod.CompID = Convert.ToInt32(inputCompID.Value.Trim());
        mod.AgentID = Convert.ToInt32(inputAgentID.Value.Trim());
        mod.CreateDate = Convert.ToDateTime(inputCreateDate.Value.Trim());
        mod.UserID = Convert.ToInt32(inputUserID.Value.Trim());
        mod.IsActive = Convert.ToBoolean(inputIsActive.Value.Trim());
        mod.Middleman = inputMiddleman.Value.Trim();
        mod.AllowArea = inputAllowArea.Value.Trim();
        mod.PhoneNumber = inputPhoneNumber.Value.Trim();
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
        MTB_CompAgentInfo ms = bll.GetList(id);
        inputCAID.Value = ms.CAID.ToString().Trim();
        inputCompID.Value = ms.CompID.ToString().Trim();
        inputAgentID.Value = ms.AgentID.ToString().Trim();
        inputCreateDate.Value = ms.CreateDate.ToString().Trim();
        inputUserID.Value = ms.UserID.ToString().Trim();
        inputIsActive.Value = ms.IsActive.ToString().Trim();
        inputMiddleman.Value = ms.Middleman.Trim();
        inputAllowArea.Value = ms.AllowArea.Trim();
        inputPhoneNumber.Value = ms.PhoneNumber.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}