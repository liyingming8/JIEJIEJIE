<%@ WebHandler Language="C#" Class="swmorderpayback" %>

using System.Web;
using Newtonsoft.Json.Linq;
using TJ.BLL;
using TJ.Model;

public class swmorderpayback : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (!string.IsNullOrEmpty(context.Request.Form["data"]))
        {
            try
            {
                BTJ_SWM_OrderInfo btjSwmOrder = new BTJ_SWM_OrderInfo();
                JObject obj = JObject.Parse(context.Request.Form["data"]);
                MTJ_SWM_OrderInfo mswmodmode = btjSwmOrder.GetList(int.Parse(obj["id"].ToString()));
                mswmodmode.ispay = obj["ispay"].ToString().Equals("1");
                mswmodmode.paytype = obj["paytype"].ToString();
                btjSwmOrder.Modify(mswmodmode);
                context.Response.Write(1);
            }
            catch 
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