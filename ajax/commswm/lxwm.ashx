<%@ WebHandler Language="C#" Class="lxwm" %>

using System.Data;
using System.Web;
using Newtonsoft.Json;
using TJ.DBUtility;

public class lxwm : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.Form["comp"]))
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
                DataTable dt = tab.ExecuteQuery("select CompName,CompanyWebSite,LegalPerson,Address,TelNumber,Position from TJ_RegisterCompanys where CompID=" + compid, null);
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
                string companyWebSite = context.Request.Form["WebSite"];
                string address = context.Request.Form["Address"]; 
                string position = context.Request.Form["Position"];
                string telNumber = context.Request.Form["TelNumber"];
                tab.ExecuteNonQuery("update TJ_RegisterCompanys set CompanyWebSite='" + companyWebSite + "',Address='" +
                                    address + "',TelNumber='" + telNumber + "',Position='" + position +
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