<%@ WebHandler Language="C#" Class="getsyxxbypid" %>

using System;
using System.Data;
using System.Web;
using commonlib;
using TJ.DBUtility;

public class getsyxxbypid : IHttpHandler {
    Security sc = new Security();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.Form["compid"]) &&!string.IsNullOrEmpty(context.Request.Form["pid"]))
        {
            string sql =
                "SELECT prodname,prodnumber,materials,proddate,checkdate,checkuser,checkreport,outdate,prodaddress,prodcompname  FROM TJ_SWM_CommonSuyuan where compid=" + context.Request.Form["compid"] + " and pid=" + sc.DecryptQueryString(context.Request.Form["pid"]);
            TabExecute tab = new TabExecute();
            DataTable dt =  tab.ExecuteQuery(sql, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                context.Response.Write("{\"pnm\":\"" + dt.Rows[0]["prodname"] + "\",\"pcd\":\"" + dt.Rows[0]["prodnumber"] + "\",\"mt\":\"" + dt.Rows[0]["materials"].ToString().Replace("\n\r","<br/>").Replace("\r","<br/>").Replace("\n","<br/>") + "\",\"ptm\":\"" + Convert.ToDateTime(dt.Rows[0]["proddate"]).ToString("yyyy-MM-dd") + "\",\"ctm\":\"" + Convert.ToDateTime(dt.Rows[0]["checkdate"]).ToString("yyyy-MM-dd") + "\",\"cunm\":\"" + dt.Rows[0]["checkuser"] + "\",\"crpt\":\"http://os.china315net.com/Admin/" + dt.Rows[0]["checkreport"] + "\",\"otm\":\"" + Convert.ToDateTime(dt.Rows[0]["outdate"]).ToString("yyyy-MM-dd") + "\",\"paddress\":\"" + dt.Rows[0]["prodaddress"] + "\",\"pcomp\":\"" + dt.Rows[0]["prodcompname"] + "\"}");
            }
            else
            {
                context.Response.Write(0);
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}