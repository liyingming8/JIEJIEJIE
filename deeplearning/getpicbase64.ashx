<%@ WebHandler Language="C#" Class="getpicbase64" %>

using System.IO;
using System.Web;
using System;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

public class getpicbase64 : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string res = "{\"msg\":\"noupdata\", \"reason\":\"\"}";
        if (context.Request.Form["pic"] != null && context.Request.Form["tpid"] != null)
        {
            try
            {
                string tpid = context.Request.Form["tpid"];
                string temppic = context.Request.Form["pic"];
                string picname = tpid + "_" + DateTime.Now.ToFileTime();
                string dir = context.Request.MapPath("yangben/");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                string filepath = dir + picname;
                Base64StringToImage(temppic, filepath);
                res = "{\"msg\":\"ok\", \"reason\":\"\"}";
            }
            catch (Exception e)
            {
                res = "{\"msg\":\"fail\", \"reason\":\""+e.Message+"\"}";
            }
        }
        else
        {
            log("没有form值");
        }
        //log(res);
        context.Response.ContentType = "text/plain";
        context.Response.Write(res);
        context.Response.End();
    }

    protected void Base64StringToImage(string strbase64,string filestring)
    {
        byte[] arr = Convert.FromBase64String(strbase64);
        MemoryStream ms = new MemoryStream(arr);
        Bitmap bmp = new Bitmap(ms);
        bmp.Save(filestring+".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        ms.Close();
    }

    public static bool IsNumeric(string value)
    {
        return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private void log(string str)
    {
        string filename = HttpContext.Current.Server.MapPath(@"~/Log/xcx/" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
        if(!File.Exists(filename))
        {
            File.Create(filename);
        }
        FileStream fs = new FileStream(filename, FileMode.Append);
        StreamWriter sw = new StreamWriter(fs,Encoding.GetEncoding("utf-8"));
        //开始写入  
        sw.WriteLine(str);
        //关闭流   
        sw.Close();
        fs.Close();
    }
}