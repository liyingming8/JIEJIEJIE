using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

public partial class testtouyun : Page
{
    InternetHandle internet = new InternetHandle();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = internet.GetTouYunJiuGuiToken();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Label1.Text = internet.Web_RefreshToken_JiuGuiTouYun(Label1.Text);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
         
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Label1.Text = internet.GetUrlDataAlbert("data-service/isv/dictionarys?companyCode=jiuguijiu&category=1&nameOrCode=203164");
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        string temp = printMd5Hex("123456789");
    }

    private string printMd5Hex(string data)
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] dataHash = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
        StringBuilder sb = new StringBuilder();
        foreach (byte b in dataHash)
        {
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }
}