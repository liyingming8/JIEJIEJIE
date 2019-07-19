using System;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Web.UI;

public partial class Admin_TB_AllowPhoneNumAddEdit : AuthorPage
{
    private readonly BTB_AllowPhoneNum bll = new BTB_AllowPhoneNum();
    private MTB_AllowPhoneNum mod = new MTB_AllowPhoneNum();

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
        mod.Phone_Num = inputPhone_Num.Value.Trim();
        mod.Master = inputMaster.Value.Trim();
        mod.CompID = int.Parse(GetCookieCompID());
        if (RadioButtonok.Checked == true)
        {
            mod.isactive = 1;
        }
        else
        {
            mod.isactive = 0;
        }

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
        // ClientScript.RegisterStartupScript(GetType(), "reload", "window.opener.location.reload();window.close();", true);
        // Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTB_AllowPhoneNum ms = bll.GetList(id);

        inputPhone_Num.Value = ms.Phone_Num.Trim();
        inputMaster.Value = ms.Master.Trim();

        if (mod.isactive == 0)
        {
            RadioButtonno.Checked = true;
        }
        else
        {
            RadioButtonok.Checked = true;
        }
    }
}