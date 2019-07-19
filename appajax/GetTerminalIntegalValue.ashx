<%@ WebHandler Language="C#" Class="GetTerminalIntegalValue" %> 
using System.Web; 
using TJ.DBUtility;

public class GetTerminalIntegalValue : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        TabExecute tab = new TabExecute();
        commfrank comfrank = new commfrank();
        //Security sc = new Security();
        if (!string.IsNullOrEmpty(context.Request.Form["terminalid"]) && !string.IsNullOrEmpty(context.Request.Form["compid"]))
        {
            string compid = context.Request.Form["compid"];
            string terminalid = context.Request.Form["terminalid"];
            //string compid = "2";
            //string terminalid = "21748";
            comfrank.FreshTerminalIntegral(compid,int.Parse(terminalid));
            string tempvl = tab.ExecuteQueryForSingleValue("SELECT SUM([prizevl]) FROM  TJ_Activity_JXS_Win where agentid=" +terminalid + " and islq=0");
            if (string.IsNullOrEmpty(tempvl))
            {
                tempvl = "0";
            }
            string temp = tab.ExecuteQueryForSingleValue("select UACID from TJ_UserAccumulating where COMPID=" + compid + " and UID=-" +terminalid);
            if (string.IsNullOrEmpty(temp) || temp.Equals("0"))
            {
                tab.ExecuteNonQuery(
                    "INSERT INTO TJ_UserAccumulating(UID,COMPID,Accumulating) VALUES(-"+terminalid+","+compid+","+tempvl+")", null);
            }
            else
            {
                tab.ExecuteNonQuery(
                    "update  TJ_UserAccumulating set Accumulating=" + tempvl + " where UACID=" + temp, null);
            }
            context.Response.Write("{\"vl\":"+tempvl+",\"msg\":\"ok\"}");
        }
        else
        {
            context.Response.Write("{\"vl\":0,\"msg\":\"请上传终端ID\"}");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}