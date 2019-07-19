<%@ WebHandler Language="C#" Class="zszz" %>

using System.Data;
using System.Web;
using Newtonsoft.Json;
using TJ.DBUtility;

public class zszz : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.Form["comp"]) && !string.IsNullOrEmpty(context.Request.Form["tp"]))
        {
            string compid = context.Request.Form["comp"];
            string type = "1";
            if (string.IsNullOrEmpty(context.Request.Form["tp"]))
            {
                type = context.Request.Form["tp"];
            } 
            TabExecute tab = new TabExecute();
            if (type.Equals("1"))
            {
                DataTable dt = tab.ExecuteNonQuery("SELECT zsnm,picurlstring,updateuid,updatetm,id FROM TJ_Comp_ZhengShu where isshow=1 and compid=" +compid);
                if (dt.Rows.Count > 0)
                {
                    context.Response.Write(JsonConvert.SerializeObject(dt));
                }
                else
                {
                    context.Response.Write(0);
                }
            }
            if (type.Equals("2"))
            {
                string zsnm = context.Request.Form["zsnm"];
                string picurlstring = context.Request.Form["picurlstring"];
                tab.ExecuteNonQuery(
                    "insert into TJ_Comp_ZhengShu(zsnm,picurlstring,updateuid) values('" + zsnm + "','" + picurlstring +
                    "',0)", null);
                context.Response.Write(1); 
            }
        }
        else
        {
            context.Response.Write(0);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}