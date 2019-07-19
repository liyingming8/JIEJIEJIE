<%@ WebHandler Language="C#" Class="GetOwnAgentProductInfoXiAn" %>  
using System.Text;
using System.Web;
using System.Data; 
public class GetOwnAgentProductInfoXiAn : IHttpHandler 
{ 
    readonly StringBuilder _sb = new StringBuilder();
    DBClass db = new DBClass();  
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.QueryString["coid"]))
        {
            string strcompid = context.Request.QueryString["coid"].Trim();
            string stragentid = context.Request.QueryString["agid"].Trim();
            DataTable dt = db.GetAuthorProductInfo(strcompid, stragentid);
            _sb.Append("[");
            foreach (DataRow row in dt.Rows)
            {
                _sb.Append(",{\"pid\":" + row["ProdID"] + ",\"pcd\":\"00\",\"pnm\":\"" + row["Products_Name"] + "\",\"pic\":\"http://os.china315net.com/Admin/wuliu/"+row["pic"]+"\"}");
            }
            _sb.Append("]");
            dt.Dispose();
            
            if (_sb.ToString().Trim().Equals("[]"))
            {
                 context.Response.Write("没有匹配的记录");
            }
            else
            {
                 context.Response.Write(_sb.ToString().Replace("[,{","[{"));
            } 
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}