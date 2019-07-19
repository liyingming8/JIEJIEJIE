<%@ WebHandler Language="C#" Class="QueryRegisterCompanyInfo" %>

using System;
using System.Web;
using TJ.DBUtility;
using System.Data;
using Newtonsoft.Json;

public class QueryRegisterCompanyInfo : IHttpHandler
{
    TabExecute tab = new TabExecute();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.Form["filter"]))
        {
            int ctid = 0;
            if (!string.IsNullOrEmpty(context.Request.Form["shi"]))
            {
                ctid = GetCityID(context.Request.Form["shi"].ToString());
            }
            DataTable dt = new DataTable();
            if (ctid > 0)
            {
                dt   = tab.ExecuteQuery("select CompID as id,CompName as nm from TJ_RegisterCompanys where CTID="+ctid+" and " + context.Request.Form["filter"], null);
            }
            else
            {
                dt   = tab.ExecuteQuery("select CompID as id,CompName as nm from TJ_RegisterCompanys where " + context.Request.Form["filter"], null);
            }

            string temp = JsonConvert.SerializeObject(dt);
            context.Response.Write(temp);
            dt.Dispose();
        }
        else
        {
            context.Response.Write("");
        }

    }

    private int GetCityID(string shiname)
    {
        string tempvalue = "0";
        DataTable dttemp =  tab.ExecuteQuery("select CID from TJ_BaseClass where CName like '%"+shiname.Replace("市","").Replace("区","")+"%'",null);
        if (dttemp.Rows.Count > 0)
        {
            tempvalue = dttemp.Rows[0][0].ToString();
        }
        dttemp.Dispose();
        return Convert.ToInt32(tempvalue);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}