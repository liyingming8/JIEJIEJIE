<%@ WebHandler Language="C#" Class="GetTerminalJiFenAll" %>

using System.Data;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using TJ.DBUtility;

public class GetTerminalJiFenAll : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        TabExecute tab = new TabExecute();
        commfrank comfrank = new commfrank();
        if (string.IsNullOrEmpty(context.Request.Form["terminalid"]))
        {
            context.Response.Write("未找到积分记录");
        }
        else
        {
            string compid = "0";
            if (!string.IsNullOrEmpty(context.Request.Form["compid"]))
            {
                compid = context.Request.Form["compid"];
            }
            string terminalid = context.Request.Form["terminalid"];
            comfrank.FreshTerminalIntegral(compid.Equals("0") ? "2" : compid, int.Parse(terminalid));
            DataTable dttemp = tab.ExecuteQuery(
                "SELECT top 30 a.id,a.winreason rs,a.prizevl jf,convert(varchar(100), a.gettm, 23) tm FROM TJ_Activity_JXS_Win a where a.agentid=" + terminalid + " and a.islq=0 and a.awtypeid=2 order by a.id desc",null);
            string liststring = JsonConvert.SerializeObject(dttemp);
            string jifeall = tab.ExecuteQueryForSingleValue(
                "select sum(prizevl) from TJ_Activity_JXS_Win where agentid=" +
                terminalid + " and islq=0 and awtypeid=2"); 

            if (string.IsNullOrEmpty(jifeall))
            {
                jifeall = "0";
            }
             
            string UACID = tab.ExecuteQueryForSingleValue("select UACID from TJ_UserAccumulating where COMPID=" + compid + " and UID=-" +terminalid);
            if (string.IsNullOrEmpty(UACID) || UACID.Equals("0"))
            {
                tab.ExecuteNonQuery(
                    "INSERT INTO TJ_UserAccumulating(UID,COMPID,Accumulating) VALUES(-"+terminalid+","+compid+","+jifeall+")", null);
            }
            else
            {
                tab.ExecuteNonQuery(
                    "update  TJ_UserAccumulating set Accumulating=" + jifeall + " where UACID=" + UACID, null);
            }

            string all = "{\"jfall\":" + jifeall + ",\"list\":" + liststring + "}";
            context.Response.Write(all);
            dttemp.Dispose();
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}