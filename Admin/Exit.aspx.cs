using System;
using System.Web;
using System.Web.UI;

public partial class Admin_Exit : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie Ali = new HttpCookie("TJUID");
        Ali.Value = "";
        Ali.Expires = DateTime.Now.AddDays(-1);

        HttpCookie Ali1 = new HttpCookie("TJRID");
        Ali1.Value = "";
        Ali1.Expires = DateTime.Now.AddDays(-1);

        HttpCookie Ali2 = new HttpCookie("TJCOMPID");
        Ali2.Value = "";
        Ali2.Expires = DateTime.Now.AddDays(-1);

        HttpCookie ALi3 = new HttpCookie("TJUName");
        ALi3.Value = "";
        ALi3.Expires = DateTime.Now.AddDays(-1);

        Response.Cookies.Add(Ali);
        Response.Cookies.Add(Ali1);
        Response.Cookies.Add(Ali2);
        Response.Cookies.Add(ALi3);

        Response.Redirect("../Loginbbe.aspx", true);
    }
}