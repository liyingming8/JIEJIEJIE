using System;
using commonlib;

public partial class Admin_wuliu_Fahuo_Fahuoxinnewtiaozhuan : AuthorPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string getuid = GetCookieUID();
        Response.Redirect("http://117.34.70.23:8888/?uid=" + getuid + "", true);

    }

}