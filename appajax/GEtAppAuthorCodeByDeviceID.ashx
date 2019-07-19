<%@ WebHandler Language="C#" Class="GetAppAuthorCodeByDeviceID" %> 
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

public class GetAppAuthorCodeByDeviceID : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.QueryString["compid"]) &&!string.IsNullOrEmpty(context.Request.QueryString["agentid"]) &&!string.IsNullOrEmpty(context.Request.QueryString["dvid"]))
        {
            context.Response.Write(GetCodeByDeviceID(context.Request.QueryString["compid"], context.Request.QueryString["agentid"], context.Request.QueryString["dvid"]));
        } 
    }

    private string _tempvl = string.Empty;

    public string GetCodeByDeviceID(string compid,string agentid,string deviceidstr)
    {
        _tempvl = "";
        string str = ConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString();
        var sda = new SqlDataAdapter("select authorkey from TJ_APP_EquipAuthor where compid=" + (compid.Equals("0")?agentid:compid) + " and agentid=" + agentid + " and equipidstr='" + deviceidstr + "'", str);
        var dttemp = new DataTable();
        sda.Fill(dttemp);
        if (dttemp.Rows.Count > 0)
        {
            _tempvl = dttemp.Rows[0][0].ToString();
        }
        dttemp.Dispose();
        sda.Dispose();
        return string.IsNullOrEmpty(_tempvl) ? "0" : _tempvl;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}