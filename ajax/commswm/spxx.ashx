<%@ WebHandler Language="C#" Class="spxx" %> 
using System.Data;
using System.Web;
using Newtonsoft.Json;
using TJ.DBUtility;

public class spxx : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.Form["spid"])&&!string.IsNullOrEmpty(context.Request.Form["comp"]))
        {
            TabExecute tab = new TabExecute();
            string spid = context.Request.Form["spid"];
            string compid = context.Request.Form["comp"];
           DataTable dttemp =  tab.ExecuteQuery("select GoodsName,Descriptions,GoodsPicURL,Price,SaleUnitID from TJ_GoodsInfo where CompID=" + compid + " and GoodsID=" + spid, null);
            if (dttemp.Rows.Count > 0)
            {
                context.Response.Write(JsonConvert.SerializeObject(dttemp));
            }
            else
            {
                context.Response.Write(0);
            }
            dttemp.Dispose(); 
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