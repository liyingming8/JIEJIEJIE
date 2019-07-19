<%@ WebHandler Language="C#" Class="GetOwnProductInfoForMymj" %>  
using System; 
using System.Security.Cryptography;
using System.Text;
using System.Web; 
using Newtonsoft.Json.Linq;

public class GetOwnProductInfoForMymj : IHttpHandler 
{  
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        string randomstring = GetRandomString();
        string timestamp = GetTimeStamp();
        string a = "randomString=" + randomstring + "&timestamp=" + timestamp;
        string sigh = md5(a + "&key=qb31ij9wgtlb6cx501").ToUpper();
        string postdata = "{\"randomString\":\"" + randomstring + "\",\"sign\":\"" + sigh + "\",\"timestamp\":\"" + timestamp + "\"}";
        InternetHandle internet = new InternetHandle();
        string temp = internet.PostUrlData("https://super.gzjibei.com/bosnds3/FwmProduct/select", postdata);
        JObject jobj = JObject.Parse(temp);
        JArray jArray = JArray.Parse(jobj["data"].ToString());
        JObject obj;
        if (jArray.Count > 0)
        {
            string[] temparray = new string[jArray.Count];
            int index = 0;
            foreach (JToken token in jArray)
            {
                obj = JObject.Parse(token.ToString());
                temparray[index] = "{\"pid\":" + obj["id"] + ",\"pcd\":\"" + obj["no"] + "\",\"pnm\":\"" + obj["value"] + "/" + obj["color"] + "\"}";
                index++;
            }
            context.Response.Write("[" + String.Join(",", temparray) + "]");
        }
        else
        {
            context.Response.Write("没有匹配的记录");
        }
    }

    public string GetRandomString()
    {
        return Guid.NewGuid().ToString("N");
    }
    
    public string GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }

    public bool IsReusable {
        get {
            return false;
        }
    }
    
     public String md5(String s)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();

            string ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            } 
            return ret.PadLeft(32, '0');
        } 
}