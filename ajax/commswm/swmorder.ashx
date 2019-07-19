<%@ WebHandler Language="C#" Class="swmorder" %>

using System;
using System.Web;
using Newtonsoft.Json.Linq;
using TJ.BLL; 
using TJ.Model;

public class swmorder : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (!string.IsNullOrEmpty(context.Request.Form["do"]) && !string.IsNullOrEmpty(context.Request.Form["data"]))
        {
            try
            {
                string dokey = context.Request.Form["do"].Trim();
                string data = context.Request.Form["data"].Trim();
                BTJ_SWM_OrderInfo bllorder = new BTJ_SWM_OrderInfo();
                MTJ_SWM_OrderInfo mod;
                JObject obj = JObject.Parse(data);
                switch (dokey)
                {
                    case "1":
                        mod = new MTJ_SWM_OrderInfo();
                        mod.ordernumber = "swm-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + obj["ordercompid"];
                        mod.psid = int.Parse(obj["psid"].ToString());
                        mod.orderquantity = int.Parse(obj["orderquantity"].ToString());
                        mod.orderremark = obj["orderremark"].ToString();
                        mod.totalprice = Convert.ToDecimal(obj["totalprice"]);
                        mod.ordercompid = int.Parse(obj["ordercompid"].ToString());
                        mod.orderuserid = int.Parse(obj["orderuserid"].ToString());
                        mod.ordercompnm = obj["ordercompnm"].ToString(); 
                        mod.orderusernm = obj["orderusernm"].ToString();
                        mod.orderphonenm = obj["orderphonenm"].ToString();
                        bllorder.Insert(mod);
                        context.Response.Write(1);
                        break;
                    case "2":
                        mod = bllorder.GetList(int.Parse(obj["id"].ToString()));
                        mod.ordernumber = "swm-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + obj["ordercompid"];
                        mod.psid = int.Parse(obj["psid"].ToString());
                        mod.orderquantity = int.Parse(obj["orderquantity"].ToString());
                        mod.orderremark = obj["orderremark"].ToString();
                        mod.totalprice = Convert.ToDecimal(obj["totalprice"]);
                        mod.ordercompid = int.Parse(obj["ordercompid"].ToString());
                        mod.orderuserid = int.Parse(obj["orderuserid"].ToString());
                        mod.ordercompnm = obj["ordercompnm"].ToString();
                        mod.orderusernm = obj["orderusernm"].ToString();
                        mod.orderphonenm = obj["orderphonenm"].ToString();
                        bllorder.Modify(mod);
                        context.Response.Write(1);
                        break;
                }
            }
            catch 
            {

                context.Response.Write(0);
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