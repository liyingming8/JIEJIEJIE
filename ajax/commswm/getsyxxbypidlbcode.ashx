<%@ WebHandler Language="C#" Class="getsyxxbypidlbcode" %>

using System;
using System.Data;
using System.Web;
using commonlib;
using TJ.DBUtility;
using System.Text;

public class getsyxxbypidlbcode : IHttpHandler
{
    Security sc = new Security();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.Form["compid"]) &&
            !string.IsNullOrEmpty(context.Request.Form["pid"]) && !string.IsNullOrEmpty(context.Request.Form["cpid"]))
        {
            string label = context.Request.Form["cpid"];
            string sql =
                "SELECT prodname,prodnumber,materials,proddate,checkdate,checkuser,checkreport,outdate,prodaddress,prodcompname  FROM TJ_SWM_CommonSuyuan where compid=" +
                context.Request.Form["compid"] + " and pid=" + sc.DecryptQueryString(context.Request.Form["pid"]);
            TabExecute tab = new TabExecute();
            DataTable dt = tab.ExecuteQuery(sql, null);
            if (dt != null && dt.Rows.Count > 0)
            {

                StringBuilder str = new StringBuilder();
                str.Append("<table class='gridtable'>");
                str.Append("<tr><td class='lefttd'>产品名称：</td><td>" + dt.Rows[0]["prodname"] + "</td></tr>");
                str.Append("<tr><td class='lefttd'>产品编码：</td><td>" + (IsNumberic(label) ? label : sc.GetShowNumFromEncryptV2(label)) + "</td></tr>");
                str.Append("<tr><td class='lefttd'>原    料：</td><td>" + dt.Rows[0]["materials"].ToString().Replace("\n\r", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>") + "</td></tr>");
                str.Append("<tr><td class='lefttd'>产    地：</td><td>" + dt.Rows[0]["prodaddress"] + "</td></tr>");
                str.Append("<tr><td class='lefttd'>生产商：</td><td>" + dt.Rows[0]["prodcompname"] + "</td></tr>");
                str.Append("<tr><td class='lefttd'>生产日期：</td><td>" + Convert.ToDateTime(dt.Rows[0]["proddate"]).ToString("yyyy-MM-dd") + "</td></tr>");
                str.Append("<tr><td class='lefttd'>出厂日期：</td><td>" + Convert.ToDateTime(dt.Rows[0]["outdate"]).ToString("yyyy-MM-dd") + "</td></tr>");
                str.Append("<tr><td class='lefttd'>检验日期：</td><td>" + Convert.ToDateTime(dt.Rows[0]["checkdate"]).ToString("yyyy-MM-dd") + "</td></tr>");
                str.Append("<tr><td class='lefttd'>质检员：</td><td>" + dt.Rows[0]["checkuser"] + "</td></tr>");
                str.Append("<tr><td class='lefttd'>质检报告：</td><td><img src='http://os.china315net.com/Admin/" + dt.Rows[0]["checkreport"] + "'</td></tr>");
                str.Append("<table>"); 

                context.Response.Write("{\"sysetdata\":\"" + str.ToString().Trim() +"\"}");

            }
            else
            {
                context.Response.Write(0);
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    protected bool IsNumberic(string message)
    {
        System.Text.RegularExpressions.Regex rex =
            new System.Text.RegularExpressions.Regex(@"^\d+$");
        if (rex.IsMatch(message))
        {
            return true;
        }
        return false;
    }

}