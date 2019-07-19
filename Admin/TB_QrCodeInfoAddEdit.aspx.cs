using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TB_QrCodeInfoAddEdit : AuthorPage
{
    private readonly BTB_QrCodeInfo bll = new BTB_QrCodeInfo();
    private MTB_QrCodeInfo mod = new MTB_QrCodeInfo();

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
        mod.LabelCode = inputLabelCode.Value.Trim();
        mod.ExplorerType = inputExplorerType.Value.Trim();
        mod.ComputerORMobile = inputComputerORMobile.Value.Trim();
        mod.CreateTime = Convert.ToDateTime(inputCreateTime.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        //this.Response.Write("<script>alert('操作成功！');</script>");
        Response.Write("<script>location.href='TB_QrCodeInfo.aspx'</script>");
    }

    private void fillinput(int id)
    {
        MTB_QrCodeInfo ms = bll.GetList(id);
        inputID.Value = ms.ID.ToString().Trim();
        inputLabelCode.Value = ms.LabelCode.Trim();
        inputExplorerType.Value = ms.ExplorerType.Trim();
        inputComputerORMobile.Value = ms.ComputerORMobile.Trim();
        inputCreateTime.Value = ms.CreateTime.ToString().Trim();
    }
}