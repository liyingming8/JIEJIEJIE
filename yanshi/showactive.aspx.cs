using System;

public partial class yanshi_showactive : System.Web.UI.Page
{
    public string num = string.Empty;
    public string imurl = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["cpid"] != null)
        {
            num = Request.QueryString["cpid"].ToString();
            switch (num)
            {
                case "1": num = "1"; break;
                case "2": num = "2"; break;
                case "3": num = "3"; break;
                default: num = "1"; break;


            }
        }
        else
        {
            num = "1";
        }

    }
}