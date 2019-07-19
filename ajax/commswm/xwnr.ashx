<%@ WebHandler Language="C#" Class="xwnr" %> 
using System.Data;
using System.Web;
using Newtonsoft.Json;
using TJ.DBUtility;

public class xwnr : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.Form["id"]))
        {
            var tab = new TabExecute();
            string id = context.Request.Form["id"];
            DataTable dt = tab.ExecuteQuery("SELECT Contents,Title,PublishDate,LinkURLString FROM TJMarketingSystemYin.dbo.TJ_PublishInfo where IFID=" + id, null);
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