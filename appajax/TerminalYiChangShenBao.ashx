<%@ WebHandler Language="C#" Class="TerminalYiChangShenBao" %>

using System;
using System.Data;
using System.IO;
using System.Web;
using TJ.DBUtility;

public class TerminalYiChangShenBao : IHttpHandler {
    TabExecute tab = new TabExecute();
    commwl comwl = new commwl();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        try
        {
            if (!string.IsNullOrEmpty(context.Request.Form["masterid"]) &&
                !string.IsNullOrEmpty(context.Request.Form["userid"]) &&
                !string.IsNullOrEmpty(context.Request.Form["username"]) &&
                !string.IsNullOrEmpty(context.Request.Form["labelcode"]) &&
                !string.IsNullOrEmpty(context.Request.Form["ycms"]) && context.Request.Files["file"] != null)
            {
                HttpFileCollection httpfiles = context.Request.Files;
                string masterid = context.Request.Form["masterid"];
                string labelcode = context.Request.Form["labelcode"];
                string userid = context.Request.Form["userid"];
                string username = context.Request.Form["username"];
                string ycms = context.Request.Form["ycms"];
                string parentid = "0";
                if (!string.IsNullOrEmpty(context.Request.Form["parentid"]))
                {
                    parentid = context.Request.Form["parentid"];
                }
                var file = httpfiles[0];
                string picname = masterid + "_" + labelcode + "_" + DateTime.Now.ToFileTime() + ".jpg";
                string dir = context.Request.MapPath("../yichangshenbao/" + masterid + "/");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                string filepath = dir + picname;
                file.SaveAs(filepath); 
                string blabeltabnm = comwl.GetBoxLabelAndTableName(labelcode);
                if (blabeltabnm.Contains(","))
                {
                    string[] blabtabarray = blabeltabnm.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                    TabExecutewuliu tabwl = new TabExecutewuliu();
                    string sql = "select ID,isexception from AgentAcceptInfo_2019 where BoxLabel='" + blabtabarray[0] + "'";
                    DataTable dttemp = tabwl.ExecuteQuery(sql,null);
                    if (dttemp.Rows.Count > 0)
                    {
                        if (dttemp.Rows[0]["isexception"].ToString().Equals("1"))
                        {
                            tab.ExecuteNonQuery(
                                "insert into TJ_YiChangShenBaoInfo(compid,userid,uname,labelcode,imgurl,ycms,parentid) values(" +
                                masterid + "," + userid + ",'" + username + "','" + labelcode + "','yichangshenbao/" +
                                masterid + "/" + picname + "','" + ycms + "',"+parentid+")");
                            context.Response.Write("{\"msg\":\"ok\"}"); 
                        }
                        else
                        {
                            context.Response.Write("{\"msg\":\"该件产品暂无异常情况，请先查询您的积分情况\"}");
                        }
                    }
                    else //未上传
                    {
                        tab.ExecuteNonQuery(
                            "insert into TJ_YiChangShenBaoInfo(compid,userid,uname,labelcode,imgurl,ycms,parentid) values(" +
                            masterid + "," + userid + ",'" + username + "','" + labelcode + "','yichangshenbao/" +
                            masterid + "/" + picname + "','" + ycms + "',"+parentid+")");
                        context.Response.Write("{\"msg\":\"ok\"}");
                    }
                    dttemp.Dispose();
                }
                else
                {
                    tab.ExecuteNonQuery(
                        "insert into TJ_YiChangShenBaoInfo(compid,userid,uname,labelcode,imgurl,ycms,parentid,isconfirm) values(" +
                        masterid + "," + userid + ",'" + username + "','" + labelcode + "','yichangshenbao/" +
                        masterid + "/" + picname + "','" + ycms + "',"+parentid+",1)");
                    context.Response.Write("{\"msg\":\"该标签号码异常\"}");
                }
            }
        }
        catch
        {
            context.Response.Write("{\"msg\":\"网络开小差了\"}");
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}