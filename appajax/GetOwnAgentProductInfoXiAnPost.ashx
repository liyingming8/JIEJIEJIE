<%@ WebHandler Language="C#" Class="GetOwnAgentProductInfoXiAnPost" %>  
using System.Text;
using System.Web;
using System.Data;
using Newtonsoft.Json;

public class GetOwnAgentProductInfoXiAnPost : IHttpHandler 
{ 
    readonly StringBuilder _sb = new StringBuilder();
    DBClass db = new DBClass();  
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        string strcompid = context.Request.Form["coid"].Trim();
        string stragentid = context.Request.Form["agid"].Trim();
        if (!string.IsNullOrEmpty(stragentid) && !string.IsNullOrEmpty(stragentid))
        {
            DataTable dt = db.GetAuthorProductInfoForJSON(strcompid, stragentid);
            if (dt.Rows.Count > 0)
            {
                _sb.Append(JsonConvert.SerializeObject(dt));
            } 
            dt.Dispose(); 
            if (_sb.ToString().Trim().Length.Equals(0))
            {
                context.Response.Write("没有匹配的记录");
            }
            else
            {
                context.Response.Write(_sb.ToString());
            }
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}