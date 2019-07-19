<%@ WebHandler Language="C#" Class="xwdt" %>
using System.Data;
using System.Web;
using Newtonsoft.Json;
using TJ.DBUtility;

public class xwdt : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.Form["comp"])&&!string.IsNullOrEmpty(context.Request.Form["tp"]))
        {
            var tab = new TabExecute();
            string compid = context.Request.Form["comp"];
            string type = "1";
            if (string.IsNullOrEmpty(context.Request.Form["tp"]))
            {
                type = context.Request.Form["tp"];
            } 
            if (type.Equals("1"))
            {
                DataTable dt = tab.ExecuteQuery("SELECT IFID,IsHot,CID,Title,LinkURLString,PublishDate FROM TJ_PublishInfo where CompID=" + compid, null);
                if (dt.Rows.Count > 0)
                {
                    context.Response.Write(JsonConvert.SerializeObject(dt));
                }
                else
                {
                    context.Response.Write(0);
                }
                dt.Dispose();
            }
            if (type.Equals("2"))
            {
                string title = context.Request.Form["Title"];
                string linkUrlString = context.Request.Form["LinkURLString"];
                string contents = context.Request.Form["Contents"];
                tab.ExecuteNonQuery("insert into TJ_PublishInfo(Title,LinkURLString,Contents,CID) values('" + title + "','" + linkUrlString + "','" + contents + "',2)");
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