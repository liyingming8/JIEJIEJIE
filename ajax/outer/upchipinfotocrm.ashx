<%@ WebHandler Language="C#" Class="upchipinfotocrm" %>

using System.Web;

public class upchipinfotocrm : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain"; 
        xifeng_erp_service.CrmToTianJian crm = new xifeng_erp_service.CrmToTianJian();
        crm.CreateLogisticsInfo("");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}