<%@ WebHandler Language="C#" Class="UpdateTerminalIntegral" %>

using System;
using System.Data;
using System.Web;
using commonlib;
using TJ.DBUtility;

public class UpdateTerminalIntegral : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain"; 
        TabExecute tab = new TabExecute();
        commfrank com = new commfrank();
        Security sc = new Security();
        if (string.IsNullOrEmpty(context.Request.QueryString["compid"]) || string.IsNullOrEmpty(context.Request.QueryString["terminalid"]) || string.IsNullOrEmpty(context.Request.QueryString["xfjf"]))
        {
            context.Response.Write("{\"rs\":0,\"msg\":\"参数不完整\"}");
        }
        else
        {
            string compid = sc.DecryptQueryString(context.Request.QueryString["compid"]);
            string terminalid = sc.DecryptQueryString(context.Request.QueryString["terminalid"]);
            string xfjf = sc.DecryptQueryString(context.Request.QueryString["xfjf"]);
            com.FreshTerminalIntegral(compid,int.Parse(terminalid));
            if (!string.IsNullOrEmpty(xfjf) && int.Parse(xfjf) > 0)
            {
                DataTable dttemp = tab.ExecuteQuery("select id,prizevl from TJ_Activity_JXS_Win where islq=0 and compid=" + compid + " and agentid=" + terminalid,null);
                if (dttemp != null && dttemp.Rows.Count > 0)
                {
                    string idstring = "";
                    decimal prizevl = 0;
                    foreach (DataRow dr in dttemp.Rows)
                    {
                        if (string.IsNullOrEmpty(idstring))
                        {
                            idstring = dr["id"].ToString();
                        }
                        else
                        {
                            idstring += "," + dr["id"];
                        }
                        prizevl += Convert.ToDecimal(dr["prizevl"]);
                    }
                    tab.ExecuteNonQuery("update TJ_Activity_JXS_Win set  islq=1 where id in (" + idstring + ")");
                    decimal shengyu = prizevl - Convert.ToDecimal(xfjf);
                    tab.ExecuteNonQuery(
                        "insert into TJ_Activity_JXS_Win(compid,agentid,awtypeid,winreason,prizevl,islq,jxstypeid,wintypeid) values(" +
                        compid + "," + terminalid + ",2,'系统结余'," + Convert.ToInt32(shengyu) + ",0,3,0)", null);
                }
                if (dttemp != null) dttemp.Dispose();
            }
            context.Response.Write("{\"rs\":1,\"msg\":\"更新成功\"}");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}