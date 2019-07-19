<%@ WebHandler Language="C#" Class="GetTerminalProductInfoXiAnPostWithPics" %>  
using System.Text;
using System.Web;
using System.Data;
using Newtonsoft.Json;

public class GetTerminalProductInfoXiAnPostWithPics : IHttpHandler 
{ 
    readonly StringBuilder _sb = new StringBuilder();
    DBClass db = new DBClass();  
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (string.IsNullOrEmpty(context.Request.Form["coid"]) || string.IsNullOrEmpty(context.Request.Form["agid"]))
        {
            context.Response.Write("没有匹配的记录");
        }
        else
        {
            string strcompid = context.Request.Form["coid"].Trim();
            string agentid = context.Request.Form["agid"].Trim();
            if (!string.IsNullOrEmpty(strcompid) && !string.IsNullOrEmpty(agentid))
            {
                DataTable dt = db.GetAuthorProductInfoForJsonWithPics(strcompid, agentid);
                if (dt.Rows.Count > 0)
                {
                    _sb.Append(JsonConvert.SerializeObject(dt));
                }
                dt.Dispose();

                if (_sb.ToString().Length == 0)
                {
                    context.Response.Write("没有匹配的记录");
                }
                else
                {
                    context.Response.Write(_sb.ToString());
                }
            }
        }
         
    } 
    public bool IsReusable {
        get {
            return false;
        }
    } 
}