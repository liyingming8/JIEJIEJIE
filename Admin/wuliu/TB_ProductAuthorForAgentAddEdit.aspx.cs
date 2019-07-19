using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TB_ProductAuthorForAgentAddEdit : AuthorPage
{
    private readonly BTB_ProductAuthorForAgent bll = new BTB_ProductAuthorForAgent();
    private MTB_ProductAuthorForAgent mod = new MTB_ProductAuthorForAgent();

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
        mod.ATHPID = Convert.ToInt32(inputATHPID.Value.Trim());
        mod.CompID = Convert.ToInt32(inputCompID.Value.Trim());
        mod.AgentID = Convert.ToInt32(inputAgentID.Value.Trim());
        mod.ProdID = Convert.ToInt32(inputProdID.Value.Trim());
        mod.CreateDate = Convert.ToDateTime(inputCreateDate.Value.Trim());
        mod.ActiveDate = Convert.ToDateTime(inputActiveDate.Value.Trim());
        mod.IsActive = Convert.ToBoolean(inputIsActive.Value.Trim());
        mod.UserID = Convert.ToInt32(inputUserID.Value.Trim());
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
        MTB_ProductAuthorForAgent ms = bll.GetList(id);
        inputATHPID.Value = ms.ATHPID.ToString().Trim();
        inputCompID.Value = ms.CompID.ToString().Trim();
        inputAgentID.Value = ms.AgentID.ToString().Trim();
        inputProdID.Value = ms.ProdID.ToString().Trim();
        inputCreateDate.Value = ms.CreateDate.ToString().Trim();
        inputActiveDate.Value = ms.ActiveDate.ToString().Trim();
        inputIsActive.Value = ms.IsActive.ToString().Trim();
        inputUserID.Value = ms.UserID.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}