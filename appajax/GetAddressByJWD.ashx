<%@ WebHandler Language="C#" Class="GetAddressByJWD" %>

using System;
using System.Data;
using System.Web;
using TJ.DBUtility;

public class GetAddressByJWD : IHttpHandler {
    TabExecute _tab = new TabExecute();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        try
        {
            if (!string.IsNullOrEmpty(context.Request.Form["jd"]) && !string.IsNullOrEmpty(context.Request.Form["wd"]))
            {
                string jd = context.Request.Form["jd"];
                string wd = context.Request.Form["wd"];
                address maddrss = new address(wd, jd);
                string ctid = GetCityIdAndProvince(maddrss.city,maddrss.province);
                context.Response.Write("{\"res\":1,\"dz\":\""+maddrss.addressInfo+"\",\"sheng\":\""+maddrss.province+"\",\"shi\":\""+maddrss.city+"\",\"ctid\":\""+ctid+"\"}");
            }
            else
            {
                context.Response.Write("{\"res\":0,\"msg\":\"网络异常\"}");
            }
        }
        catch
        {
            context.Response.Write("{\"res\":0,\"msg\":\"网络异常\"}");
        }
    }

    private string  GetCityIdAndProvince(string shiname,string shengname)
    {
        string tempvalue = "0";
        if (!string.IsNullOrEmpty(shiname))
        {
            DataTable dttemp = _tab.ExecuteQuery("select CID,ParentID from TJ_BaseClass where CName like '%" + shiname.Replace("市", "").Replace("区", "") + "%'", null);
            if (dttemp.Rows.Count > 0)
            {
                tempvalue = dttemp.Rows[0][0].ToString();
            }
            else
            {
                if (!string.IsNullOrEmpty(shengname))
                {
                    dttemp = _tab.ExecuteQuery("select CID,ParentID from TJ_BaseClass where CName like '%" + ReplaceSheng(shengname) + "%'", null);
                    if (dttemp.Rows.Count > 0)
                    {
                        string shenid = dttemp.Rows[0][0].ToString();
                        string insertstring = "insert into TJ_BaseClass(ParentID,CName) values(" + shenid + ",'" +shiname.Replace("市", "").Replace("区", "") + "') select @@identity;";
                        tempvalue = _tab.ExecuteQueryForSingleValue(insertstring);
                    }
                } 
            }
            dttemp.Dispose();
        }
        return tempvalue;
    }

    private string ReplaceSheng(string shengnm)
    {
        if (shengnm.Contains("省"))
        {
            return shengnm.Replace("省", "");
        }
        if (shengnm.Contains("市"))
        {
            return shengnm.Replace("市", "");
        }
        if (shengnm.Contains("广西壮族自治区"))
        {
            return "广西";
        }
        if (shengnm.Contains("宁夏回族自治区"))
        {
            return "宁夏";
        }
        if (shengnm.Contains("新疆维吾尔自治区"))
        {
            return "新疆";
        }

        if (shengnm.Contains("香港"))
        {
            return "香港";
        }
        if (shengnm.Contains("澳门"))
        {
            return "澳门";
        }
        return shengnm.Replace("自治区", "");
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}