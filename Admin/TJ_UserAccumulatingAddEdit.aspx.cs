using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_UserAccumulatingAddEdit : AuthorPage
{
    private readonly BTJ_UserAccumulating bll = new BTJ_UserAccumulating();
    private readonly MTJ_UserAccumulating mod = new MTJ_UserAccumulating();

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
        mod.UACID = Convert.ToInt32(inputUACID.Value.Trim());
        mod.UID = Convert.ToInt32(inputUID.Value.Trim());
        mod.COMPID = Convert.ToInt32(inputCOMPID.Value.Trim());
        mod.Accumulating = Convert.ToDecimal(inputAccumulating.Value.Trim());
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
        MTJ_UserAccumulating ms = bll.GetList(id);
        inputUACID.Value = ms.UACID.ToString().Trim();
        inputUID.Value = ms.UID.ToString().Trim();
        inputCOMPID.Value = ms.COMPID.ToString().Trim();
        inputAccumulating.Value = ms.Accumulating.ToString().Trim();
    }
}