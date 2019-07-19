<%@ WebHandler Language="C#" Class="loginhandle" %>

using System;
using System.Data;
using System.Web;
using commonlib;
using TJ.DBUtility;

public class loginhandle : IHttpHandler
{
    private readonly DBClass db = new DBClass("0");
    TabExecute tabexe = new TabExecute();
    Security sc = new Security();
    private string unm = string.Empty;
    private string upw = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        if (!string.IsNullOrEmpty(context.Request.Form["unm"]) && !string.IsNullOrEmpty(context.Request.Form["upw"]))
        {
            unm = context.Request.Form["unm"].Trim();
            upw = context.Request.Form["upw"].Trim();
            var mulist = tabexe.ExecuteQuery("select RID,IsActived,RegisterDate,LoginName,UserID,CompID,departid from TJ_User where " +
                                    "LoginName is not null and LoginName='" + unm + "' and PassWords='" +
                                    CommonFun.Md5hash_String(upw) + "'", null);
            if (mulist.Rows.Count > 0)
            {
                if (int.Parse(mulist.Rows[0]["RID"].ToString()) == 44)
                {
                    context.Response.Write("{\"rs\":0,\"msg\":\"已到活动结束时间!如有疑问请联系总部!\"}");
                    return;
                }
                if (mulist.Rows[0]["IsActived"].ToString() == "0")
                {
                    context.Response.Write("{\"rs\":0,\"msg\":\"抱歉，该用户尚未激活!\"}");
                    return;
                }
                if (string.IsNullOrEmpty(mulist.Rows[0]["RID"].ToString()) || mulist.Rows[0]["RID"].ToString() == "0")
                {
                    context.Response.Write("{\"rs\":0,\"msg\":\"抱歉，该用户尚未指定角色!\"}");
                    return;
                }
                if (int.Parse(mulist.Rows[0]["IsActived"].ToString()) == 2 && DateTime.Compare(Convert.ToDateTime(mulist.Rows[0]["RegisterDate"].ToString()).AddDays(3), DateTime.Now) > 0)
                {
                    context.Response.Write("{\"rs\":0,\"msg\":\"抱歉，该用户已经超过试用期限!\"}");
                    return;
                }
                if (int.Parse(mulist.Rows[0]["IsActived"].ToString()) == 3)
                {
                    context.Response.Write("{\"rs\":0,\"msg\":\"抱歉，该用户已被系统冻结!\"}");
                    return;
                }
                var userName = new HttpCookie("TJOSUName");
                userName.Value = sc.EncryptQueryString(mulist.Rows[0]["LoginName"].ToString().Trim());
                userName.Expires.AddDays(1);
                context.Response.Cookies.Add(userName);

                var userID = new HttpCookie("TJOSUID");
                userID.Value = sc.EncryptQueryString(mulist.Rows[0]["UserID"].ToString());
                userID.Expires.AddDays(1);
                context.Response.Cookies.Add(userID);

                var tjUserRid = new HttpCookie("TJOSRID")
                {
                    Value = sc.EncryptQueryString(mulist.Rows[0]["RID"].ToString())
                };
                tjUserRid.Expires.AddDays(1);
                context.Response.Cookies.Add(tjUserRid);

                var userUnid = new HttpCookie("TJOSCOMPID");
                userUnid.Value = sc.EncryptQueryString(mulist.Rows[0]["CompID"].ToString().Trim());
                userUnid.Expires.AddDays(1);
                context.Response.Cookies.Add(userUnid);

                var userdepartid = new HttpCookie("TJOSDEPARTID");
                userdepartid.Value = sc.EncryptQueryString(mulist.Rows[0]["departid"].ToString().Trim());
                userdepartid.Expires.AddDays(1);
                context.Response.Cookies.Add(userdepartid);

                var ip = context.Request.UserHostAddress;
                db.InserLOG(mulist.Rows[0]["CompID"].ToString().Trim(), mulist.Rows[0]["UserID"].ToString(), "431", ip);
                var tjCompTypeID = new HttpCookie("TJOSCompTypeID");
                DataTable dt =
                    tabexe.ExecuteQuery(
                        "select CompTypeID from TJ_RegisterCompanys where CompID=" + mulist.Rows[0]["CompID"], null);
                tjCompTypeID.Value = sc.EncryptQueryString(dt.Rows[0]["CompTypeID"].ToString());
                tjCompTypeID.Expires.AddDays(1);
                context.Response.Cookies.Add(tjCompTypeID);
                mulist.Dispose();
                dt.Dispose();
                context.Response.Write("{\"rs\":1,\"msg\":\"通过验证!\"}");
            }
            else
            {
                var mulist1 = tabexe.ExecuteQuery("select count(UserID) from TJ_User where LoginName='" + unm + "'", null);
                if (mulist1.Rows.Count > 0)
                {
                    context.Response.Write("{\"rs\":0,\"msg\":\"密码错误!\"}");
                }
                else
                {
                    context.Response.Write("{\"rs\":0,\"msg\":\"尚未找到相关用户信息!\"}");
                }
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