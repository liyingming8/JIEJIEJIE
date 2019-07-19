<%@ WebHandler Language="C#" Class="gettofeedbackforpython" %>

using System.Data;
using System.Web;
using commonlib;
using Newtonsoft.Json;
using Npgsql;

public class gettofeedbackforpython : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        NpgsqlConnection conn = new NpgsqlConnection(DAConfig.swmfeedbckpgconn);
        string returnstring = "[{\"id\":0}]";
        using (conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            NpgsqlDataAdapter ada =  new NpgsqlDataAdapter("SELECT id, uptime, picfilepath,lbcode FROM public.swm_recognition where fdvalue=0 order by id limit 10;",conn);
            DataTable dttemp = new DataTable();
            ada.Fill(dttemp);
            if (dttemp.Rows.Count > 0)
            {
                returnstring =  JsonConvert.SerializeObject(dttemp);
            }
            dttemp.Dispose();
            ada.Dispose();
        }
        conn.Dispose();
        context.Response.Write(returnstring);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}