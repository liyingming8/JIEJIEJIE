<%@ WebHandler Language="C#" Class="queryswmorderinfo" %>
using System.Data;
using System.Web;
using Newtonsoft.Json;
using TJ.DBUtility;

public class queryswmorderinfo : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (!string.IsNullOrEmpty(context.Request.Form["compid"]))
        {
            try
            {
                TabExecute tab = new TabExecute();
                DataTable dttemp = tab.ExecuteQuery(
                    "SELECT id,ordernumber,psid,orderquantity,orderremark,totalprice,ordercompid,orderuserid,ordertm,payconfirm ,isfahuo ,isactive ,orderusernm,orderphonenm,paytype,ispay FROM TJ_SWM_OrderInfo where ordercompid=" +
                    context.Request.Form["compid"], null);
                context.Response.Write(JsonConvert.SerializeObject(dttemp));
                dttemp.Dispose();
            }
            catch
            {
                context.Response.Write("[]");
            }
        }
        else
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