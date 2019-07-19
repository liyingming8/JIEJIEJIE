using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SendMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_sendmsg_Click(object sender, EventArgs e)
    {
        string temp = HuYi_Info.HY_dxinfoAutoSign(input_terminal.Value.Trim() + "您好!您的资料已经通过审核,请用当前手机号码登录,初始密码为本手机号码后6位,谢谢!", input_phone.Value);
    }
}