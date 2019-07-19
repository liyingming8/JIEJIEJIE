using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using commonlib; 
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TJ.DBUtility;

/// <summary>
/// InternetHandle 的摘要说明
/// </summary>
public class InternetHandle
{
	public InternetHandle()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private HttpWebRequest _webrequest;
    private StreamWriter _swMessages;
    private HttpWebResponse _webreponse;
    private string _resp=string.Empty;
    private Stream _stream;
    readonly TabExecute _tabexe = new TabExecute();
    //post 提交
    public string Web_Go(string json, string method,string tokenstr,string finalurl)
    {
        string url = DAConfig.Touyunurl + finalurl;
        _webrequest = (HttpWebRequest) WebRequest.Create(url);
        _webrequest.Method = method;
        _webrequest.ContentType = "application/json;charset=UTF-8";
        _webrequest.Accept = "application/json;charset=UTF-8";
        WebHeaderCollection headers = _webrequest.Headers;
        headers.Add("Authorization","Bearer "+ tokenstr);
        _swMessages = new StreamWriter(_webrequest.GetRequestStream());
        _swMessages.Write(json);
        //关闭写入流
        _swMessages.Close();
        _webreponse = (HttpWebResponse) _webrequest.GetResponse();
        _stream = _webreponse.GetResponseStream();
        _resp = string.Empty;
        if (_stream != null)
            using (var reader = new StreamReader(_stream))
            {
                _resp = reader.ReadToEnd();
            }
        return _resp;
    }

    //public string web_post_frank(string method,string json,string tokenstr,string finalurl)
    //{
    //    try
    //    {
    //        string url = DAConfig.Touyunurl + finalurl;
    //        var request = (HttpWebRequest)WebRequest.Create(url);
    //        var postData = json;
    //        var data = Encoding.ASCII.GetBytes(postData);
    //        request.Method = method; 
    //        request.Accept = "application/json;charset=UTF-8";  
    //        request.Headers.Add("Authorization", "Bearer " + tokenstr);
    //        request.ContentLength = data.Length;
    //        using (var stream = request.GetRequestStream())
    //        {
    //            stream.Write(data, 0, data.Length);
    //        }
    //        var response = (HttpWebResponse)request.GetResponse();
    //        return new StreamReader(response.GetResponseStream()).ReadToEnd();
    //    }
    //    catch (Exception ex)
    //    {
    //        return ex.Message;
    //    } 
    //}

    public string GetUrlDataAlbert(string finalurl)
    { 
        HttpWebRequest request = null;
        HttpWebResponse response = null;
        string result = "";
        try
        {
            string totalurl = DAConfig.Touyunurl + finalurl;
            request = (HttpWebRequest)WebRequest.Create(totalurl);
            request.Method = "GET";
            request.Accept = "application/json;charset=UTF-8";
            request.Headers.Add("Authorization", "Bearer " + GetTouYunJiuGuiToken());
            request.Timeout = 100000;
            response = (HttpWebResponse) request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), encoding: Encoding.UTF8);
            result = sr.ReadToEnd().Trim();
            sr.Close();
        }
        catch
        {
            return "";
        }
        finally
        {
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

    public string HashMD5_String(string filepath)
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        using (var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            return BitConverter.ToString(md5.ComputeHash(fs)).Replace("-", "").ToLower();
        }
    }  

    public string HttpPostData(string url, int timeOut, string fileKeyName,string filePath, NameValueCollection stringDict)
    {
        string responseContent;
        var memStream = new MemoryStream();
        var webRequest = (HttpWebRequest)WebRequest.Create(url);
        webRequest.Headers.Add("Authorization", "Bearer " + GetTouYunJiuGuiToken());
        // 边界符
        var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
        // 边界符
        var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        // 最后的结束符
        var endBoundary = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n"); 
        // 设置属性
        webRequest.Method = "POST";
        webRequest.Accept = "application/json"; 
        webRequest.Timeout = timeOut;
        webRequest.ContentType = "multipart/form-data;charset=UTF-8; boundary=" + boundary; 
        // 写入文件
        const string filePartHeader =
            "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
             "Content-Type: application/octet-stream\r\n\r\n";
        var header = string.Format(filePartHeader, fileKeyName, filePath.Substring(filePath.LastIndexOf('\\')+1));
        var headerbytes = Encoding.UTF8.GetBytes(header); 
        memStream.Write(beginBoundary, 0, beginBoundary.Length);
        memStream.Write(headerbytes, 0, headerbytes.Length); 
        var buffer = new byte[6144];
        int bytesRead; // =0 
        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
        {
            memStream.Write(buffer, 0, bytesRead);
        } 
        // 写入字符串的Key
        var stringKeyHeader = "\r\n--" + boundary +
                               "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                               "\r\n\r\n{1}\r\n";
        foreach (byte[] formitembytes in from string key in stringDict.Keys
            select string.Format(stringKeyHeader, key, stringDict[key])
            into formitem
            select Encoding.UTF8.GetBytes(formitem))
        {
            memStream.Write(formitembytes, 0, formitembytes.Length);
        } 
        // 写入最后的结束边界符
        memStream.Write(endBoundary, 0, endBoundary.Length); 
        webRequest.ContentLength = memStream.Length; 
        var requestStream = webRequest.GetRequestStream(); 
        memStream.Position = 0;
        var tempBuffer = new byte[memStream.Length];
        memStream.Read(tempBuffer, 0, tempBuffer.Length);
        memStream.Close(); 
        requestStream.Write(tempBuffer, 0, tempBuffer.Length);
        requestStream.Close(); 
        var httpWebResponse = (HttpWebResponse)webRequest.GetResponse(); 
        using (var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(),Encoding.GetEncoding("utf-8")))
        {
            responseContent = httpStreamReader.ReadToEnd();
        } 
        fileStream.Close();
        httpWebResponse.Close();
        webRequest.Abort();  
        return responseContent;
    } 

    public string GetTouYunJiuGuiToken()
    {
        string tokenstr;
        DataTable dttemp = _tabexe.ExecuteNonQuery("select * from TJ_OuterTokenInfo where code='" + DAConfig.TyjgtokenCode + "'");
        if (dttemp.Rows.Count > 0)
        {
            double lastupdatetonow = (DateTime.Now - Convert.ToDateTime(dttemp.Rows[0]["lastupdate"])).TotalMinutes;
            if (lastupdatetonow > 20)
            {
                _tabexe.ExecuteNonQuery("delete from TJ_OuterTokenInfo where code='" + DAConfig.TyjgtokenCode + "'");
                tokenstr = Web_GetToken_JiuGuiTouYun();
                _tabexe.ExecuteNonQuery("insert into TJ_OuterTokenInfo([code],[tokenstr],[expiresin]) values('" +
                                        DAConfig.TyjgtokenCode + "','" + tokenstr + "'," +
                                        Convert.ToInt64(GenerateTimeStamp()) + Convert.ToInt64(DAConfig.TouYunTokenExpiresin) + ")");
            }
            else
            {
                tokenstr = dttemp.Rows[0]["tokenstr"].ToString();
            } 
        }
        else
        {
            tokenstr = Web_GetToken_JiuGuiTouYun();
            _tabexe.ExecuteNonQuery("insert into TJ_OuterTokenInfo([code],[tokenstr],[expiresin]) values('" +
                                    DAConfig.TyjgtokenCode + "','" + tokenstr + "'," +
                                    Convert.ToInt64(GenerateTimeStamp()) + Convert.ToInt64(DAConfig.TouYunTokenExpiresin) + ")");
        }
        return tokenstr;
    }

    public string GenerateTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }

    public string Web_GetToken_JiuGuiTouYun()
    {
        _webrequest = (HttpWebRequest)WebRequest.Create(DAConfig.Touyunurl + "sso-service/isv/oauth/token");
        _webrequest.Method = "POST";
        _webrequest.ContentType = "application/json;charset=UTF-8";
        _webrequest.Accept = "application/json;charset=UTF-8";
        _swMessages = new StreamWriter(_webrequest.GetRequestStream());
        _swMessages.Write("{\"clientName\": \"" + DAConfig.Touyunjgname + "\",\"clientPassword\": \"" +DAConfig.Touyunjgpassword + "\"}");
        //关闭写入流
        _swMessages.Close();
        _webreponse = (HttpWebResponse) _webrequest.GetResponse();
        if (_webreponse.StatusCode == HttpStatusCode.OK)
        {
            _stream = _webreponse.GetResponseStream();
            _resp = string.Empty;
            if (_stream != null)
                using (var reader = new StreamReader(_stream))
                {
                    _resp = reader.ReadToEnd();
                }
            JObject jo = (JObject) JsonConvert.DeserializeObject(_resp);
            if (!string.IsNullOrEmpty(jo["accessToken"].ToString()))
            {
                _resp = jo["accessToken"].ToString();
            }
            else
            {
                _resp = "";
            } 
        }
        return _resp;
    }

    public string Web_RefreshToken_JiuGuiTouYun(string oldtoken)
    {
        _webrequest = (HttpWebRequest)WebRequest.Create(DAConfig.Touyunurl + "sso-service/isv/oauth/refreshToken");
        _webrequest.Method = "POST";
        _webrequest.ContentType = "application/json;charset=UTF-8";
        _webrequest.Accept = "application/json;charset=UTF-8";
        _swMessages = new StreamWriter(_webrequest.GetRequestStream());
        _swMessages.Write("{\"accessToken\": \"" + oldtoken + "\"}");
        //关闭写入流
        _swMessages.Close();
        _webreponse = (HttpWebResponse)_webrequest.GetResponse();
        _stream = _webreponse.GetResponseStream();
        _resp = string.Empty;
        if (_stream != null)
            using (var reader = new StreamReader(_stream))
            {
                _resp = reader.ReadToEnd();
                JObject jo = (JObject)JsonConvert.DeserializeObject(_resp);
                if (!string.IsNullOrEmpty(jo["accessToken"].ToString()))
                {
                    _resp = jo["accessToken"].ToString();
                }
                else
                {
                    _resp = "";
                }
            }
        return _resp;
    } 

    private WebClient _wc;
    public string GetUrlData(string url)
    {
        _wc = new WebClient {Credentials = CredentialCache.DefaultCredentials, Encoding = Encoding.UTF8};
        _resp = _wc.DownloadString(url);
        return _resp; 
    }

    public string PostUrlData(string url,string postdata)
    { 
        _wc = new WebClient(); 
        _wc.Headers[HttpRequestHeader.ContentType] = "application/json";
        _wc.Encoding = Encoding.UTF8;
        return _wc.UploadString(url, postdata);
    }
}