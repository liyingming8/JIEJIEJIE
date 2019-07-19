using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_RoleInfoAddEdit : AuthorPage
{
    private readonly BTJ_RoleInfo bll = new BTJ_RoleInfo();
    private MTJ_RoleInfo mod = new MTJ_RoleInfo();

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
            mod = bll.GetList(int.Parse(HF_ID.Value.Trim()));
        }
        mod.RoleName = inputRoleName.Value.Trim();
        mod.CompGrade = ckb_comp_grade.Checked ? 1 : 0;
        mod.Remark = inputRemark.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        } 
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void fillinput(int id)
    {
        MTJ_RoleInfo ms = bll.GetList(id);
        inputRoleName.Value = ms.RoleName.Trim();
        ckb_comp_grade.Checked = ms.CompGrade.Equals(1);
        inputRemark.Value = ms.Remark.Trim();
    }
}