<%@ WebHandler Language="C#" Class="checkismaster" %> 
using System.Data;
using System.Web;
using TJ.DBUtility;

public class checkismaster : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain"; 
        if (!string.IsNullOrEmpty(context.Request.QueryString["openid"]) &&!string.IsNullOrEmpty(context.Request.QueryString["compid"]))
        {
            TabExecute tab = new TabExecute();
            DataTable dttemp = tab.ExecuteNonQuery("SELECT UserID,CompID,RID,LoginName from TJ_User where RID=155 and CompID=" +
                                context.Request.QueryString["compid"] + " and WXNumber='" +
                                context.Request.QueryString["openid"] + "'");
            if (dttemp.Rows.Count > 0)
            {
                string jsonstring = "{\"uid\":" + dttemp.Rows[0]["UserID"] + ",\"compid\":" + dttemp.Rows[0]["CompID"] +
                                    ",\"rid\":" + dttemp.Rows[0]["RID"] + ",\"nm\":\"" + dttemp.Rows[0]["LoginName"] + "\"}";
                dttemp.Dispose();
                context.Response.Write(jsonstring);
            }
            else
            {
                dttemp.Dispose();
                context.Response.Write("0");
            }
        }
        else
        {
            context.Response.Write("0");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}