<%@ WebHandler Language="C#" Class="UserAuthorXiAn" %>

using System.Configuration;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using System;
using TJ.DBUtility;


public class UserAuthorXiAn : IHttpHandler { 
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.QueryString["unm"]) &&
            !string.IsNullOrEmpty(context.Request.QueryString["upw"]))
        {
            string strusername = context.Request.QueryString["unm"].Trim();
            string strpwd = context.Request.QueryString["upw"].Trim();
            string strtemp =
                "select a.UserID,a.CompID,a.LoginName,a.PassWords,(Select b.ParentID from TJ_RegisterCompanys b where b.CompID=a.CompID) ParentID from TJ_User a where LoginName='" +
                strusername + "' and PassWords='" + MD5(strpwd) + "'";
            DataTable dt = new DataTable();
            TabExecute tab = new TabExecute();
            dt = tab.ExecuteQuery(strtemp, null);
            if (dt.Rows.Count > 0)
            {
                string masterid = GetMasterIDByAgentID(dt.Rows[0]["CompID"].ToString());
                context.Response.Write("{\"coid\":" + dt.Rows[0]["CompID"].ToString().Trim() + " ,\"uid\":" +
                                       dt.Rows[0]["UserID"].ToString().Trim() + ",\"parid\":" +
                                       dt.Rows[0]["ParentID"].ToString().Trim() + ",\"masterid\":" + masterid + "}");
            }
            else
            {
                context.Response.Write("用户名与密码不匹配!");
            } 
            dt.Dispose(); 
        }
    }

    private  string MD5(string encryptString)
        {
            byte[] result = Encoding.Default.GetBytes(encryptString);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string encryptResult = BitConverter.ToString(output).Replace("-", "");
            return encryptResult;
        }

    public bool IsReusable {
        get {
            return false;
        }
    }

    private string GetMasterIDByAgentID(string agentid)
    {
        string str = ConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString();
        SqlDataAdapter sda = new SqlDataAdapter("SELECT CompID as MarsterID FROM TB_CompAgentInfo where AgentID="+agentid,str);
        DataTable dttemp = new DataTable();
        sda.Fill(dttemp);
        string tempvalue="";
        if (dttemp.Rows.Count > 0)
        {
            tempvalue = dttemp.Rows[0][0].ToString();
        }
        else
        {
            tempvalue = "0";
        }
        dttemp.Dispose();
        sda.Dispose();
        return tempvalue;
    }
}