<%@ WebHandler Language="C#" Class="getairesult" %>

using System;
using System.Data;
using System.Web;
using commonlib;
using Npgsql;

public class getairesult : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (!string.IsNullOrEmpty(context.Request.QueryString["id"]))
        {
            NpgsqlConnection conn = new NpgsqlConnection(DAConfig.swmfeedbckpgconn);
            using (conn)
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                NpgsqlCommand com = new NpgsqlCommand("select fdvalue from public.swm_recognition where id="+context.Request.QueryString["id"],conn);
                object fdvl = com.ExecuteScalar();
                context.Response.Write("{\"fd\":"+fdvl+"}");
                com.Dispose();
                conn.Close();
            }
        }
        else
        {
            context.Response.Write("{\"fd\":-1}");
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}