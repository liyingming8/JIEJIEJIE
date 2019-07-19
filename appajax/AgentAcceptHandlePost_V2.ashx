<%@ WebHandler Language="C#" Class="AgentAcceptHandlePost_V2" %>
using System;
using System.Data;
using System.Web;
using TJ.DBUtility;
using Newtonsoft.Json.Linq;

public class AgentAcceptHandlePost_V2 : IHttpHandler
{
    InternetHandle internet = new InternetHandle();
    private readonly string _totablename = "AgentAcceptInfo_2019";
    TabExecutewuliu tabwl = new TabExecutewuliu();
    DBClass db = new DBClass();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (string.IsNullOrEmpty(context.Request.Form["masterid"]) ||
            string.IsNullOrEmpty(context.Request.Form["parid"]) || string.IsNullOrEmpty(context.Request.Form["lbcode"]) ||
            string.IsNullOrEmpty(context.Request.Form["agid"]) || string.IsNullOrEmpty(context.Request.Form["userid"]) ||
            string.IsNullOrEmpty(context.Request.Form["pid"]))
        {
            context.Response.Write("{\"cnt\": 0, \"e\": \"参数不完整\"}");
        }
        else
        {
            string compid = context.Request.Form["masterid"];
            string parentid = context.Request.Form["parid"];
            string labelcode = context.Request.Form["lbcode"];
            string agent_id = context.Request.Form["agid"];
            string user_id = context.Request.Form["userid"];
            string pid = context.Request.Form["pid"];
            string smaddress = "";
            if(!string.IsNullOrEmpty(context.Request.Form["smplace"]))
            {
                smaddress = context.Request.Form["smplace"];
                db.Penglog(context.Request.Form["smplace"], "appupload");
            }
            try
            {
                string sqlstring = "select count(id) from " + _totablename + " where BoxLabel='" + labelcode+"'";
                string  cnt = tabwl.ExecuteQueryForSingleValue(sqlstring);
                if (!string.IsNullOrEmpty(cnt) && Convert.ToInt32(cnt).Equals(0))
                {
                    if (int.Parse(cnt).Equals(0))
                    {
                        sqlstring = "insert into " + _totablename + "(ParentID,AcceptAgentID,BoxLabel,ProID,AcceptUserID,KhDDH,CompID,AgentTypeID,UploadAddress) values(" + parentid + "," + agent_id + ",'" + labelcode + "'," + pid + "," + user_id + ",''," + compid + ",3,'"+smaddress+"') select @@identity";
                        DataTable dttemp = tabwl.ExecuteQuery(sqlstring, null);
                        string shangchuanid = dttemp.Rows[0][0].ToString();
                        try
                        {
                            string temp = internet.GetUrlData("http://117.34.70.23:8888/delivery/confirm/?code=" + labelcode + "&agent_id=" +
                               agent_id + "&user_id=" + user_id + "&pid=" + pid + "&achdid=" + shangchuanid + "&jxsid=" + parentid);
                            JObject obj = JObject.Parse(temp);
                            if (!string.IsNullOrEmpty(obj["e"].ToString()))
                            {
                                if (obj["e"].ToString().Equals("窜货嫌疑"))
                                {
                                    tabwl.ExecuteNonQuery("update " + _totablename + " set isexception=1 where id=" + shangchuanid, null);
                                    temp = "{\"cnt\": 0, \"e\": \"\"}";
                                }
                                else
                                {
                                    tabwl.ExecuteNonQuery("delete from " + _totablename + " where id=" + shangchuanid, null);
                                }
                            }
                            dttemp.Dispose();
                            context.Response.Write(temp);
                        }
                        catch
                        {
                            tabwl.ExecuteNonQuery("delete from " + _totablename + " where id=" + shangchuanid, null);
                            context.Response.Write("{\"cnt\": 0, \"e\": \"网络异常\"}");
                        }
                    }
                    else
                    {
                        context.Response.Write("{\"cnt\": 0, \"e\": \"当前序号已经存在\"}");
                    }
                }
                else
                {
                    context.Response.Write("{\"cnt\": 0, \"e\": \"当前序号已经存在\"}");
                }
            }
            catch
            {
                context.Response.Write("{\"cnt\": 0, \"e\": \"系统异常\"}");
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
}