using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_RiChangLogAddEdit : AuthorPage
{
    private readonly BTJ_RiChangLog bll = new BTJ_RiChangLog();
    private MTJ_RiChangLog mod = new MTJ_RiChangLog();

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

        mod.Compid = Convert.ToInt32(GetCookieCompID());
        mod.BiaoTi = inputBiaoTi.Value.Trim();
        mod.NeiRong = txtcontent.Text.Trim();
        mod.TiJiaoTime = Convert.ToDateTime(TijiaoTime.Text.Trim());
        mod.userID = Convert.ToInt32(GetCookieUID());

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
        MTJ_RiChangLog ms = bll.GetList(id);
        inputBiaoTi.Value = ms.BiaoTi.Trim();
        txtcontent.Text = ms.NeiRong.Trim();
    }
}