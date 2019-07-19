<%@ WebHandler Language="C#" Class="feedback" %>

using System.Data;
using System.Web;
using commonlib;
using Npgsql;

public class feedback : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (!string.IsNullOrEmpty(context.Request.QueryString["id"]))
        {
            NpgsqlConnection conn = new NpgsqlConnection(DAConfig.swmfeedbckpgconn);
            string id=context.Request.QueryString["id"];
            string vl=context.Request.QueryString["vl"];
            string returnstring = "{\"msg\":\"ok\"}";
            try
            {
                using (conn)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    NpgsqlCommand cmd =
                        new NpgsqlCommand("update public.swm_recognition set fdvalue=" + vl + " where id=" + id + ";",
                            conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }

                conn.Dispose();
            }
            catch
            {
                returnstring = "{\"msg\":\"fail\"}";
            }
            context.Response.Write(returnstring);
        }
        else
        {
            context.Response.Write("{\"msg\":\"fail\"}");
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}