using System;
using System.Web.UI;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_TJ_ModifySinglePassword : AuthorPage
{
    private readonly BTJ_User bluser = new BTJ_User();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HF_UID.Value = GetCookieUID();
            FillUserOrigalInfo();
        }
    }

    protected void Button_Ok_Click(object sender, EventArgs e)
    {
        if (TextBox_OldPassword.Text .Trim () .Equals (TextBox_NewPassWord.Text.Trim () ))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('修改密码时新旧密码不能相同!');", true);
            return;
        }
        if (!TextBox_NewPassWord.Text.Trim () .Equals (TextBox_NewPassWordRepeater.Text.Trim () ))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('修改密码时新密码两次输入不相同!');", true);
            return;
        }
        if (
            bluser.CheckIsExistByFilterString("LoginName='" + Label_UserName.Text + "' and PassWords='" +
                                              CommonFun.Md5hash_String(TextBox_OldPassword.Text) + "'"))
        {
            MTJ_User muser = bluser.GetList(int.Parse(HF_UID.Value));
            muser.PassWords = CommonFun.Md5hash_String(TextBox_NewPassWord.Text);
            bluser.Modify(muser);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('操作成功!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('原密码输入不正确!');", true);
        }
    }

    private void FillUserOrigalInfo()
    {
        MTJ_User muser = bluser.GetList(Convert.ToInt32(HF_UID.Value));
        Label_UserName.Text = muser.LoginName.Trim();
    }
}