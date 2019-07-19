using System;
using System.Web;
using System.Web.UI;

/// <summary>
///AuthorPage 的摘要说明
/// </summary>
public class ConsumerAuthorPage : Page
{
    public ConsumerAuthorPage()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (Request.Cookies["TJUID"] == null)
        {
            HttpContext.Current.Response.Redirect("Login.aspx");
            HttpContext.Current.Response.End();
        }
        if (Request.Cookies["TJCOMPID"] == null && Request.Cookies["TJCOMPID"].Value != "0")
        {
            HttpContext.Current.Response.Redirect("Login.aspx");
            HttpContext.Current.Response.End();
        }
    }
}