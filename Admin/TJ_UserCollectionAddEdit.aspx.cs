using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_UserCollectionAddEdit : AuthorPage
{
    private readonly BTJ_UserCollection bll = new BTJ_UserCollection();
    private MTJ_UserCollection mod = new MTJ_UserCollection();

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
        mod.UCID = Convert.ToInt32(inputUCID.Value.Trim());
        mod.UserID = Convert.ToInt32(inputUserID.Value.Trim());
        mod.PBID = Convert.ToInt32(inputPBID.Value.Trim());
        mod.CID = Convert.ToInt32(inputCID.Value.Trim());
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
        MTJ_UserCollection ms = bll.GetList(id);
        inputUCID.Value = ms.UCID.ToString().Trim();
        inputUserID.Value = ms.UserID.ToString().Trim();
        inputPBID.Value = ms.PBID.ToString().Trim();
        inputCID.Value = ms.CID.ToString().Trim();
    }
}