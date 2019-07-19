<%@ WebHandler Language="C#" Class="getpic" %>

using System.IO;
using System.Web;
using System;
using System.Data;
using System.Text.RegularExpressions;
using commonlib;
using Npgsql;

public class getpic : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string res = "";
        if (context.Request.Files["file"] != null && context.Request.Form["tpid"] != null)
        {
            try
            {
                string tpid = context.Request.Form["tpid"];
                string lbcode = "";
                if (!string.IsNullOrEmpty(context.Request.Form["cpid"]))
                {
                    lbcode = context.Request.Form["cpid"];
                }

                HttpFileCollection httpfiles = context.Request.Files;
                var file = httpfiles[0];
                string picname = tpid + "_" + DateTime.Now.ToFileTime() + ".jpg";
                string dir = context.Request.MapPath("yangben/");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                string filepath = dir + picname;
                file.SaveAs(filepath);
                //Image img = Image.FromStream(file.InputStream);
                //img.Save(filepath, ImageFormat.Jpeg); 
                //img.Dispose();
                string rst = Record(lbcode, picname);
                res = "{\"msg\":\"ok\", \"reason\":\"\",\"id\":" + rst + "}";
            }
            catch (Exception e)
            {
                res = "{\"msg\":\"fail\", \"reason\":\"" + e.Message + "\",\"id\":0}";
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

    private string Record(string lbcode,string picfilepath)
    {
        try
        {
            NpgsqlConnection conn = new NpgsqlConnection(DAConfig.swmfeedbckpgconn);
            using (conn)
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                NpgsqlCommand npgcmd =
                    new NpgsqlCommand(
                        "INSERT INTO public.swm_recognition(lbcode,uptime, picfilepath, fdvalue) VALUES ('" + lbcode +
                        "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 'deeplearning/yangben/" +
                        picfilepath + "', 0) returning id", conn);
                object o = npgcmd.ExecuteScalar();
                conn.Close();
                npgcmd.Dispose();
                return o.ToString();
            }
        }
        catch
        {
            return "0";
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}