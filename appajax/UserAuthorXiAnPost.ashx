<%@ WebHandler Language="C#" Class="UserAuthorXiAnPost" %>

using System.Configuration;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using System;
using commonlib;
using TJ.BLL;
using TJ.Model;


public class UserAuthorXiAnPost : IHttpHandler
{
 
    SqlConnection myConn = null;
    SqlCommand myCmd;
    DataTable dt;
    SqlDataAdapter ad;
    public DataSet Mydataset;
    public SqlDataAdapter ada; 

    public SqlConnection GetConnectionWLMarket()
    {
        string str = ConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString();
        myConn = new SqlConnection(str);
        return myConn;
    }

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.Form["unm"])&&!string.IsNullOrEmpty(context.Request.Form["upw"]))
        {
            string strusername = context.Request.Form["unm"].Trim();
            string strpwd = context.Request.Form["upw"].Trim();
            if (string.IsNullOrEmpty(strusername) || string.IsNullOrEmpty(strpwd))
            {
                context.Response.Write("{\"rs\":0,\"msg\":\"用户名与密码不匹配!\"}");
            }
            else
            {
                string temp =
                    "select a.[UserID],a.[CompID],a.[LoginName],a.[PassWords],a.[RID],b.ParentID,b.CompName from TJ_User a,TJ_RegisterCompanys b where a.CompID=b.CompID and a.LoginName='" + strusername + "' and a.PassWords='" + MD5(strpwd) + "'";
                //string strtemp = "select a.[UserID],a.[CompID],a.[LoginName],a.[PassWords],a.[RID],(Select b.ParentID from TJ_RegisterCompanys b where b.CompID=a.CompID) ParentID from TJ_User a where LoginName='" + strusername + "' and PassWords='" + MD5(strpwd) + "'";
                using (var myConn = GetConnectionWLMarket())
                {
                    if (myConn.State == ConnectionState.Closed)
                    {
                        myConn.Open();
                    }
                    dt = new DataTable();
                    ad = new SqlDataAdapter(temp, myConn);
                    ad.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Security sc = new Security();
                        string masterid = Getmasterid(dt.Rows[0]["CompID"].ToString());
                        context.Response.Write("{\"rs\":1,\"coid\":" + dt.Rows[0]["CompID"].ToString().Trim() + " ,\"uid\":" + dt.Rows[0]["UserID"].ToString().Trim() + ",\"parid\":" + dt.Rows[0]["ParentID"].ToString().Trim() + ",\"masterid\":" + masterid + ",\"rid\":" + dt.Rows[0]["RID"] + ",\"unitnm\":\"" + dt.Rows[0]["CompName"] + "\",\"coidnick\":\"" + sc.EncryptQueryString("-"+dt.Rows[0]["CompID"].ToString().Trim()) + "\"}");
                    }
                    else
                    {
                        context.Response.Write("{\"rs\":0,\"msg\":\"用户名与密码不匹配!\"}");
                    }
                    dt.Dispose();
                    ad.Dispose();
                }
            }
        }
        else
        {
            context.Response.Write("{\"rs\":0,\"msg\":\"请输入您的用户名和密码!\"}");
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

    BTJ_RegisterCompanys btjRegister = new BTJ_RegisterCompanys();
    MTJ_RegisterCompanys mod = new MTJ_RegisterCompanys();
    private string Getmasterid(string agentid)
    {
        string compid = agentid;
        mod = btjRegister.GetList(int.Parse(agentid));
        if (mod.ParentID == 0)
        {
            return compid;
        }
        return Getmasterid(mod.ParentID.ToString());
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