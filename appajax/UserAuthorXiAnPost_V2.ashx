<%@ WebHandler Language="C#" Class="UserAuthorXiAnPost_V2" %>
using System.Web;
using System.Data; 
using System.Text;
using System.Security.Cryptography;
using System;
using commonlib; 
using TJ.DBUtility;


public class UserAuthorXiAnPost_V2 : IHttpHandler
{
    readonly TabExecute _tab = new TabExecute(); 
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.Form["unm"]) && !string.IsNullOrEmpty(context.Request.Form["upw"]))
        {
            string strusername = context.Request.Form["unm"].Trim();
            string strpwd = context.Request.Form["upw"].Trim();
            if (string.IsNullOrEmpty(strusername) || string.IsNullOrEmpty(strpwd))
            {
                context.Response.Write("{\"rs\":0,\"msg\":\"用户名与密码不匹配!\"}");
            }
            else
            {
                string temp = "select a.UserID,a.CompID,a.LoginName,a.PassWords,a.RID,a.IsActived,b.ParentID,b.CompName from TJ_User a,TJ_RegisterCompanys b where a.RID>0 and  a.CompID=b.CompID and a.LoginName='" + strusername + "' and a.PassWords='" + MD5(strpwd) + "'";
                DataTable dt = _tab.ExecuteQuery(temp, null);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt64(dt.Rows[0]["IsActived"]).Equals(3))
                    {
                        context.Response.Write("{\"rs\":0,\"msg\":\"该用户尚未激活，请稍候再试!\"}");
                    }
                    else
                    {
                        Security sc = new Security();
                        string masterid = Getmasterid(dt.Rows[0]["ParentID"].ToString());
                        context.Response.Write("{\"rs\":1,\"coid\":" + dt.Rows[0]["CompID"].ToString().Trim() + " ,\"uid\":" + dt.Rows[0]["UserID"].ToString().Trim() + ",\"parid\":" + dt.Rows[0]["ParentID"].ToString().Trim() + ",\"masterid\":" + (String.IsNullOrEmpty(masterid)?"0":masterid) + ",\"rid\":" + dt.Rows[0]["RID"] + ",\"unitnm\":\"" + dt.Rows[0]["CompName"] + "\",\"coidnick\":\"" + sc.EncryptQueryString("-" + dt.Rows[0]["CompID"].ToString().Trim()) + "\"}");
                    }
                }
                else
                {
                    context.Response.Write("{\"rs\":0,\"msg\":\"用户名与密码不匹配!\"}");
                }
                dt.Dispose();
            }
        }
        else
        {
            context.Response.Write("{\"rs\":0,\"msg\":\"请输入您的用户名和密码!\"}");
        }
    }

    private string MD5(string encryptString)
    {
        byte[] result = Encoding.Default.GetBytes(encryptString);
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] output = md5.ComputeHash(result);
        string encryptResult = BitConverter.ToString(output).Replace("-", "");
        return encryptResult;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    } 
    private string Getmasterid(string agentid)
    {
        return _tab.ExecuteQueryForSingleValue("select ParentID from TJ_RegisterCompanys where CompID=" + agentid);
    } 
}