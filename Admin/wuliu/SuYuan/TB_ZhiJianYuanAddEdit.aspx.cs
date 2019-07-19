using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TB_ZhiJianYuanAddEdit : AuthorPage
{
    private readonly BTB_ZhiJianYuan bll = new BTB_ZhiJianYuan();
    private MTB_ZhiJianYuan mod = new MTB_ZhiJianYuan();

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

        mod.ZJname = inputZJname.Value.Trim();

        mod.Remarks = inputRemarks.Value.Trim();
        mod.Phone = inputPhone.Value.Trim();
        mod.Compid = int.Parse(GetCookieCompID());

        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        ClientScript.RegisterStartupScript(GetType(), "reload", "window.opener.location.reload();window.close();", true);
    }

    private void fillinput(int id)
    {
        MTB_ZhiJianYuan ms = bll.GetList(id);

        inputZJname.Value = ms.ZJname.Trim();

        inputRemarks.Value = ms.Remarks.Trim();
        inputPhone.Value = ms.Phone.Trim();
    }
}