using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_VanMasterInfoAddEdit : AuthorPage
{
    private readonly BTJ_VanMasterInfo bll = new BTJ_VanMasterInfo();
    private readonly MTJ_VanMasterInfo mod = new MTJ_VanMasterInfo();

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
        mod.VanMaterID = Convert.ToInt32(inputVanMaterID.Value.Trim());
        mod.UserID = Convert.ToInt32(inputUserID.Value.Trim());
        mod.Name = inputName.Value.Trim();
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
        MTJ_VanMasterInfo ms = bll.GetList(id);
        inputVanMaterID.Value = ms.VanMaterID.ToString().Trim();
        inputUserID.Value = ms.UserID.ToString().Trim();
        inputName.Value = ms.Name.Trim();
        inputPhoneNumber.Value = ms.PhoneNumber.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}