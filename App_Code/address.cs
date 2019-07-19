using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
 
public class address
{ 
    public address(string weidu, string jingdu)
    {
        string url = "https://api.map.baidu.com/geocoder/v2/?coordtype=wgs84ll&location=" + weidu + "," + jingdu + "&output=json&ak=ASl2xmUaYpjwdWy3GPBQbpUK";
        string temp = SendRequest(url);
        JObject j = JObject.Parse(temp);
        if (j != null && j["status"].ToString().Equals("0"))
        {
            status = true; //如果获取到了 状态改为True
            province = j["result"]["addressComponent"]["province"].ToString();
            city = j["result"]["addressComponent"]["city"].ToString();
            district = j["result"]["addressComponent"]["district"].ToString();
            addressInfo = j["result"]["formatted_address"].ToString();
            baidu_jingdu = j["result"]["location"]["lng"].ToString();
            baidu_weidu = j["result"]["location"]["lat"].ToString();
            description = j["result"]["sematic_description"].ToString();
        }
    }
    private bool _status = false;
    private string _province;
    private string _city;
    private string _district;
    private string _addressInfo;
    private string _baidu_jingdu;
    private string _baidu_weidu;
    private string _description;
    /// <summary>
    /// 数据返回状态 有数据为true 否则为false
    /// </summary>
    public bool status
    {
        set { _status = value; }
        get { return _status; }
    }
    /// <summary>
    /// 省
    /// </summary>
    public string province
    {
        set { _province = value; }
        get { return _province; }
    }
    /// <summary>
    /// 市
    /// </summary>
    public string city
    {
        set { _city = value; }
        get { return _city; }
    }
    /// <summary>
    /// 地级市
    /// </summary>
    public string district
    {
        set { _district = value; }
        get { return _district; }
    }
    /// <summary>
    /// 详细地址
    /// </summary>
    public string addressInfo
    {
        set { _addressInfo = value; }
        get { return _addressInfo; }
    }
    /// <summary>
    /// 百度 经度
    /// </summary>
    public string baidu_jingdu
    {
        set { _baidu_jingdu = value; }
        get { return _baidu_jingdu; }
    }
    /// <summary>
    /// 百度 经度
    /// </summary>
    public string baidu_weidu
    {
        set { _baidu_weidu = value; }
        get { return _baidu_weidu; }
    }
    /// <summary>
    /// 更加详细的描述
    /// </summary>
    public string description
    {
        set { _description = value; }
        get { return _description; }
    }
    private string SendRequest(string url)
    {
        HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);
        webRequest.Method = "GET";
        HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
        StreamReader sr = new StreamReader(webResponse.GetResponseStream());
        return sr.ReadToEnd();
    }
}