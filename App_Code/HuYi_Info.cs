
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;

/// <summary>
/// PhoneCZ_HYX 的摘要说明
/// </summary>
public class HuYi_Info
{
    public HuYi_Info()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    static string PostUrl = "http://106.ihuyi.cn/webservice/sms.php?method=Submit";
    static string account = "C75540914";//用户名是登录用户中心->验证码、通知短信->帐户及签名设置->APIID
    static string password = "74e762dae2ad1925a27e060bf19fdaad"; //密码是请登录用户中心->验证码、通知短信->帐户及签名设置->APIKEY
    //post 提交
    public static string Web_Post(string data)
    {
        HttpWebRequest request = null;
        HttpWebResponse response = null;
        string result = "";
        try
        {
            request = (HttpWebRequest)WebRequest.Create(PostUrl);
            request.Method = "post";
            request.ContentType= "application/x-www-form-urlencoded";
            StreamWriter swMessages = new StreamWriter(request.GetRequestStream());
            swMessages.Write(data);
            swMessages.Close();
            response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            result = sr.ReadToEnd().Trim();
            sr.Close();
        }
        catch (Exception ex)
        {
           Penglog(ex.Message, "hydx_log");
        }
        finally
        {
            //关闭连接和流
            if (response != null)
            {
                response.Close();
            }
            if (request != null)
            {
                request.Abort();
            }
        }
        return result;
    }

    public static string HY_dxinfo(string yzm, string phone, string sign)
    {
        string content = "您的验证码是：" + yzm + " 。请不要把验证码泄露给其他人。";
        string postStrTpl = "account={0}&password={1}&mobile={2}&content={3}&sign={4}";
        string data = string.Format(postStrTpl, account, password, phone, content, sign);
        string result = Web_Post(data);
        Penglog("互亿短信：" + result, "hydx_log");
        return result;
    }
    public static string HY_dxinfoNoYzm(string content, string phone, string sign)
    { 
        string postStrTpl = "account={0}&password={1}&mobile={2}&content={3}&sign={4}";
        string data = string.Format(postStrTpl, account, password, phone, content, sign);
        string result = Web_Post(data);
        Penglog("互亿短信：" + result, "hydx_log");
        return result;
    }

    public static string HY_dxinfoAutoSign(string content, string phone)
    {
        string postStrTpl = "account={0}&password={1}&mobile={2}&content={3}&sign={4}";
        string data = string.Format(postStrTpl, account, password, phone, content, "海南天鉴防伪科技");
        string result = Web_Post(data);
        Penglog("互亿短信：" + result, "hydx_log");
        return result;
    }

    //MD5
    private static string toMD5(string source)
    {
        return FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5");
    }

    //转64
    private static string Hex_2To16(Byte[] bytes)
    {
        String hexString = String.Empty;
        Int32 iLength = 65535;
        if (bytes != null)
        {
            StringBuilder strB = new StringBuilder();

            if (bytes.Length < iLength)
            {
                iLength = bytes.Length;
            }
            for (int i = 0; i < iLength; i++)
            {
                strB.Append(bytes[i].ToString("X2"));
            }
            hexString = strB.ToString();
        }
        return hexString;
    }

    public static string phonecity(string phone)
    {
        string head1 = phone.Substring(0, 3);
        //string   head2 = phone.Substring(0, 4);
        bool cmcc1 = head1.Equals("134") || head1.Equals("135") || head1.Equals("136")
                    || head1.Equals("137") || head1.Equals("138")
                    || head1.Equals("139") || head1.Equals("147")
                    || head1.Equals("150") || head1.Equals("151")
                    || head1.Equals("152") || head1.Equals("157")
                    || head1.Equals("158") || head1.Equals("159")
                    || head1.Equals("183") || head1.Equals("184")
                    || head1.Equals("182") || head1.Equals("187") || head1.Equals("178")
                    || head1.Equals("188");
        if (cmcc1) return "CMCC";
        //// 移动前4位
        //bool cmcc2 = head2.Equals("1340") || head2.Equals("1341")
        //        || head2.Equals("1342") || head2.Equals("1343")
        //        || head2.Equals("1344") || head2.Equals("1345")
        //        || head2.Equals("1346") || head2.Equals("1347")
        //        || head2.Equals("1348") || head2.Equals("1349");

        //if (cmcc2)  return "CMCC";
        bool unicom = head1.Equals("130") || head1.Equals("131")
                    || head1.Equals("132") || head1.Equals("145") || head1.Equals("176")
                    || head1.Equals("155") || head1.Equals("156") || head1.Equals("175")
                    || head1.Equals("185") || head1.Equals("186");
        if (unicom) return "CUCC";

        bool telecomtemp = head1.Equals("133") || head1.Equals("149")
                    || head1.Equals("153") || head1.Equals("173") || head1.Equals("177")
                    || head1.Equals("180") || head1.Equals("181") || head1.Equals("189");
        if (telecomtemp) return "CTCC";

        return "NULL";
    }

  
    //前端最新修改 
    public static bool Penglog(string str, string logfile)
    {
        str = DateTime.Now.ToString("HH:mm:ss") + " " + str;
        try
        {
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(@"~/Log/" + logfile + "/log" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt"), FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入  
            sw.WriteLine(str);
            //清空缓冲区  
            sw.Flush();
            //关闭流  
            sw.Close();
            fs.Close();
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
}