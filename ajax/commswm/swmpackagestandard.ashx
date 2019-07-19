<%@ WebHandler Language="C#" Class="swmpackagestandard" %> 
using System.Data;
using System.Web;
using Newtonsoft.Json;
using TJ.DBUtility;

public class swmpackagestandard : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        try
        {
            TabExecute tab = new TabExecute();
            DataTable dttemp = tab.ExecuteQuery(
                "SELECT id,unitnm,quantity,singleprice,totalprice,remarks FROM TJ_SWM_Package_Standard", null);
            context.Response.Write(JsonConvert.SerializeObject(dttemp));
            dttemp.Dispose();
        }
        catch
        {
            context.Response.Write("[]");
        } 
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}