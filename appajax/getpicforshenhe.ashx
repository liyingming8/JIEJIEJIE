<%@ WebHandler Language="C#" Class="getpicforshenhe" %> 

using System.IO;
using System.Web;
using System; 
using System.Text.RegularExpressions; 

public class getpicforshenhe : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string res = "";
        if (context.Request.Files["file"] != null && context.Request.Form["type"] != null)
        {
            try
            {
                string type = context.Request.Form["type"];
                HttpFileCollection httpfiles = context.Request.Files;
                var file = httpfiles[0];
                string picname = type + "_" + DateTime.Now.ToFileTime() + ".jpg";
                string dir = context.Request.MapPath("../UploadFile/shenhe/");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                string filepath = dir + picname;
                file.SaveAs(filepath);
                res = "{\"msg\":\"ok\", \"path\":\"UploadFile/shenhe/"+picname+"\"}";
            }
            catch (Exception e)
            {
                res = "{\"msg\":\"fail\", \"reason\":\""+e.Message+"\"}";
            }
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(res);
        context.Response.End();
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
}