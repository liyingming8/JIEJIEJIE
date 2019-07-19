<%@ WebHandler Language="C#" Class="AgentFaHuoConfirmPost" %> 
using System.Web; 
public class AgentFaHuoConfirmPost : IHttpHandler {
    readonly InternetHandle _internet = new InternetHandle();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        string guid = context.Request.Form["guid"];
        if (!string.IsNullOrEmpty(guid))
        {
            string temp = _internet.GetUrlData("http://117.34.70.23:8888/perbox/submit/?guid=" + guid);
            context.Response.Write(temp);
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