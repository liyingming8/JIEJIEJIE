<%@ WebHandler Language="C#" Class="rmcp" %> 
using System.Data;
using System.Web;
using Newtonsoft.Json;
using TJ.DBUtility;

public class rmcp : IHttpHandler { 
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");  
        if (!string.IsNullOrEmpty(context.Request.Form["comp"]))
        {
            var tab = new TabExecute();
            string compid = context.Request.Form["comp"];
            DataTable dt = tab.ExecuteQuery(
                "select GoodsID,GoodsName,Descriptions,GoodsPicURL,GoodsPicURLSmal,WLProID from TJ_GoodsInfo where CompID=" +
                compid, null);
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