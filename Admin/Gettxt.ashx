<%@ WebHandler Language="C#" Class="gettxt" %>

using System.IO;
using System.Web;

public class gettxt : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        string filePath = HttpContext.Current.Server.MapPath("/admin/answer/");
        string picname = context.Request.QueryString["picname"].Trim();
        FileStream file = new FileStream(filePath + picname + ".txt", FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(file);
        StreamReader reader = new StreamReader(context.Request.InputStream);
        sw.WriteLine(reader.ReadToEnd());
        reader.Close();
        sw.Close();
        file.Close();
        file.Dispose();
        HttpContext.Current.Response.Write("ok");
    }

    public bool IsReusable
    {
        get { return false; }
    }
}