<%@ WebHandler Language="C#" Class="xfagentcontractupdate" %> 
using System.Data;
using System.Web;
using Newtonsoft.Json.Linq;
using TJ.DBUtility;

public class xfagentcontractupdate : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.Form["data"]))
        { 
            string jsonstring = HttpUtility.UrlDecode(context.Request.Form["data"]);
            JObject obj = JObject.Parse(jsonstring);
            TabExecute tab = new TabExecute();
            string insertstring = "INSERT INTO TJ_XF_AgentUpdate(compid,agentname,agentCode,datafrom) VALUES(2,'" +
                                  obj["Agentname"] + "','" + obj["AgentCode"] + "','汉德接口') select @@identity";
            DataTable dtid = tab.ExecuteQuery(insertstring, null);
            JArray array = JArray.Parse(obj["Contract"].ToString());
            if (array.Count > 0)
            {
                bool isok = true;
                foreach (JObject o in array)
                {
                    insertstring = "INSERT INTO TJ_XF_Contract(updtid,starttime,endtime) VALUES(" + dtid.Rows[0][0] + ",'" +
                                   o["starttime"] + "','" + o["endtime"] + "') select @@identity";
                    dtid.Dispose();
                    tab = new TabExecute();
                    DataTable dtcid = tab.ExecuteQuery(insertstring,null);
                    JArray arrayc = JArray.Parse(o["Product"].ToString());
                    if (arrayc.Count > 0)
                    {
                        foreach (JObject po in arrayc)
                        {
                            insertstring =
                                "INSERT INTO TJ_XF_Contract_ProductAuthorInfo(ctrid,procode,authorizedarea,proname) VALUES(" +
                                dtcid.Rows[0][0] + ",'" + po["procode"] + "','" + po["Authorizedarea"] + "','" + po["pronm"] + "')";
                            tab = new TabExecute();
                            tab.ExecuteNonQuery(insertstring, null);
                        }
                    }
                    else
                    {
                        isok = false;
                        break;
                    }
                    dtcid.Dispose();
                }
                if (isok)
                {
                    context.Response.Write("{\"re\":1,\"msg\":\"更新成功\"}");
                }
                else
                {
                    context.Response.Write("{\"re\":0,\"msg\":\"产品授权信息异常\"}");
                }
            }
            else
            {
                context.Response.Write("{\"re\":0,\"msg\":\"合同内容不存在\"}");
            }
        }
        else
        {
            context.Response.Write("{\"re\":0,\"msg\":\"尚未接收到数据\"}");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}