<%@ WebHandler Language="C#" Class="wx_pay_result" %>

using System.Web;
using System.Text;
using WxBaseAPITJ_V2;
using System.IO;

public class wx_pay_result : IHttpHandler
{
    DBClass db = new DBClass();
    WXData wxdata = new WXData();

    WXpay_api pay = new WXpay_api("wx59969257ee0275da");
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        Stream s = context.Request.InputStream;
        StreamReader sRead = new StreamReader(s, Encoding.UTF8);
        string postContent = sRead.ReadToEnd();
        string result = "";
     
        wxdata.XmlTO_Dic(postContent);
        if (wxdata.IsSet("return_code") && wxdata.IsSet("result_code"))
        {
            if (wxdata.GetValue("return_code").ToString() == "SUCCESS" && wxdata.GetValue("result_code").ToString() == "SUCCESS")
            {
                string out_trade_no = wxdata.GetValue("out_trade_no").ToString();
                db.AddModuleInfo(out_trade_no,out_trade_no.Substring(21));
                result = post_wx("SUCCESS", "OK"); 
            }
            else {
                result = post_wx("FAIL", "失败");
            }
        }
        else
        {
            result = post_wx("FAIL", "失败");
        }
        context.Response.Write(result);
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    private string post_wx(string return_code, string return_msg)
    {
        return "<xml><return_code><![CDATA[" + return_code + "]]></return_code><return_msg><![CDATA[" + return_msg + "]]></return_msg></xml>";
    }

}