using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TB_ReplaceInfoAddEdit : AuthorPage
{
    private readonly BTB_ReplaceInfo bll = new BTB_ReplaceInfo();
    private MTB_ReplaceInfo mod = new MTB_ReplaceInfo();

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
        mod.ID = Convert.ToInt32(inputID.Value.Trim());
        mod.NewLabelCode = inputNewLabelCode.Value.Trim();
        mod.OldLabelCode = inputOldLabelCode.Value.Trim();
        mod.UpDateTime = Convert.ToDateTime(inputUpDateTime.Value.Trim());
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
        MTB_ReplaceInfo ms = bll.GetList(id);
        inputID.Value = ms.ID.ToString().Trim();
        inputNewLabelCode.Value = ms.NewLabelCode.Trim();
        inputOldLabelCode.Value = ms.OldLabelCode.Trim();
        inputUpDateTime.Value = ms.UpDateTime.ToString().Trim();
    }
}