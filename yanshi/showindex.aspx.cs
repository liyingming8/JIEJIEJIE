using System;

public partial class wx_showindex : System.Web.UI.Page
{
    public string moid = string.Empty;
    public string url = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!string.IsNullOrEmpty(Request.QueryString["sw"]))
        {
            moid = Request.QueryString["sw"].ToString();
        }
        else
        {
            moid = "1";
        }
        switch (moid)
        {
            case "1": url= "../analysis/ChuanHuoYuJing.aspx"; break;
            case "2": url = "../analysis/JiaBiaoQianYuJing.aspx"; break;
            case "3": url = "../yanshi/cpsy.aspx"; break;
            case "4": url = "../yanshi/zxcj.html"; break;
            case "5": Response.Redirect("../yanshi/data/showdata.aspx", true); break;
            //  case "5": url = "../yanshi/smdata.html"; break;
            //case "5": url = "http://wyn.grapecity.com.cn/dashboards/view/5b809a606d502b0031deb3cd?theme=default&lng=zh-CN"; break;
            case "6": url = "../yanshi/djjxs.aspx"; break;
            case "7": url = "../yanshi/showactive.aspx"; break;
            default:break;

        }
    }
    

}