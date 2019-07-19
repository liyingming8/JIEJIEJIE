<%@ WebHandler Language="C#" Class="qyjs" %> 
using System.Data;
using System.Web;
using Newtonsoft.Json;
using TJ.DBUtility;

public class qyjs : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.Form["comp"]) && !string.IsNullOrEmpty(context.Request.Form["tp"]))
        {
            var tab = new TabExecute();
            string compid = context.Request.Form["comp"];
            string type = "1";
            if (string.IsNullOrEmpty(context.Request.Form["tp"]))
            {
                type = context.Request.Form["tp"];
            } 
            if (type == "1")
            {
                DataTable dt = tab.ExecuteQuery("select CompName,DetailDiscription from TJ_RegisterCompanys where CompID=" + compid, null);
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
            if (type == "2")
            {
                string detailDiscription = context.Request.Form["Detail"];
                tab.ExecuteNonQuery("update TJ_RegisterCompanys set DetailDiscription='" + detailDiscription +
                                    "' where CompID=" + compid);
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