<%@ WebHandler Language="C#" Class="AppAuthorRequest" %> 
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

public class AppAuthorRequest : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.QueryString["compid"]) &&
            !string.IsNullOrEmpty(context.Request.QueryString["agentid"]) &&
            !string.IsNullOrEmpty(context.Request.QueryString["dvid"]))
        {
            string cnt = GetRegisterNumByDeviceID(context.Request.QueryString["compid"],context.Request.QueryString["agentid"],context.Request.QueryString["dvid"]);
            if (int.Parse(cnt).Equals(0))
            {
                try
                {
                    int effeccnt = RegisterAppInfo(context.Request.QueryString["compid"], context.Request.QueryString["agentid"], context.Request.QueryString["dvid"]);
                    if (effeccnt > 0)
                    {
                        context.Response.Write("1");
                    }
                    else
                    {
                        context.Response.Write("0");
                    }
                }
                catch 
                {
                    context.Response.Write("0");
                } 
            }
            else
            {
                context.Response.Write("1");
            }
        }
        else
        {
            context.Response.Write("请正确输入");
        }
    }

    private int RegisterAppInfo(string compid, string agentid, string deviceid)
    {
        var sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString());
        using (sqlconn)
        { 
            var sqlcmd = new SqlCommand("insert into TJ_APP_EquipAuthor(compid,agentid,equipidstr) values("+(compid.Equals("0")?agentid:compid)+","+agentid+",'"+deviceid+"')",sqlconn);
            if (sqlconn.State != ConnectionState.Open)
            {
                sqlconn.Open();
            }
            int vl = sqlcmd.ExecuteNonQuery();
            sqlcmd.Dispose();
            sqlconn.Dispose();
            return vl;
        }
    }
    

    private string _tempvl = string.Empty; 
    public string GetRegisterNumByDeviceID(string compid,string agentid,string deviceidstr)
    {
        _tempvl = "";
        string str = ConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString();
        var sda = new SqlDataAdapter("select count(id) as cnt from TJ_APP_EquipAuthor where compid=" + (compid.Equals("0") ? agentid : compid) + " and agentid="+agentid+" and equipidstr='" + deviceidstr + "'",str);
        var dttemp = new DataTable();
        sda.Fill(dttemp);
        if (dttemp.Rows.Count > 0)
        {
            _tempvl = dttemp.Rows[0][0].ToString();
        }
        dttemp.Dispose();
        sda.Dispose();
        return _tempvl;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}