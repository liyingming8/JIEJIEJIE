using System;
using System.Web;
using System.Web.UI;
using commonlib;

public partial class _Exit : Page
{
    private readonly DBClass db = new DBClass();
    Security sc = new Security();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["TJOSUID"] != null && Request.Cookies["TJOSCOMPID"] != null)
        {
            string ip = Request.UserHostAddress;
            db.InserLOG(sc.DecryptQueryString(Request.Cookies["TJOSCOMPID"].Value), sc.DecryptQueryString(Request.Cookies["TJOSUID"].Value), "433", ip);
        }

        HttpCookie Ali = new HttpCookie("TJOSUID");
        Ali.Value = "";
        Ali.Expires = DateTime.Now.AddDays(-1);

        HttpCookie Ali1 = new HttpCookie("TJOSRID");
        Ali1.Value = "";
        Ali1.Expires = DateTime.Now.AddDays(-1);

        HttpCookie Ali2 = new HttpCookie("TJOSCOMPID");
        Ali2.Value = "";
        Ali2.Expires = DateTime.Now.AddDays(-1);

        HttpCookie ALi3 = new HttpCookie("TJOSUName");
        ALi3.Value = "";
        ALi3.Expires = DateTime.Now.AddDays(-1);

        Response.Cookies.Add(Ali);
        Response.Cookies.Add(Ali1);
        Response.Cookies.Add(Ali2);
        Response.Cookies.Add(ALi3); 
        Server.Transfer("Login.aspx", true);
    }
}