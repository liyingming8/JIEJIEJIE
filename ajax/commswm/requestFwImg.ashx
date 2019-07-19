<%@ WebHandler Language="C#" Class="requestFwImg" %>

using System;
using System.Web;
using TJ.DBUtility;
using System.Data;

public class requestFwImg : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        string res = "";

        //判断传值
        if (context.Request.Form["uid"] != null && context.Request.Form["cpid"] != null)
        {
            string uid = context.Request.Form["uid"];
            string cpid = context.Request.Form["cpid"];
            InternetHandle internet = new InternetHandle();
            string returnJson =
                internet.GetUrlData("http://www.china315net.com:35207/tjfw/get/?fwcode=" + cpid +
                                    "&history=1&record=1&uid=" + uid);
            TabExecute tab = new TabExecute();


            DataTable dt = tab.ExecuteQuery("select SMTime from TJ_SMinfo_2018 where LabelCode = '" + cpid + "' order by id  ", null);
            string count = dt.Rows.Count.ToString();
            string tm;
            if (dt.Rows.Count > 0)
                tm = dt.Rows[0]["smtime"].ToString();
            else
                tm = DateTime.Now.ToString();


            string warn = "";
            if (!string.IsNullOrEmpty(context.Request.Form["commontype"]))
            {

                string commontype = context.Request.Form["commontype"];
                if(commontype == "tangjinswm")
                {
                    warn = "请认真核对实物标签上的图案特征是否与上图相符，否则谨防假冒！";
                }
            }


            res = "{\"msg\":\"ok\",\"reason\":\"成功！\",\"info\":" + returnJson + ",\"count\":" + count + ",\"tm\":\"" + tm + "\",\"warn\":\"" + warn + "\"}";
        }
        else
        {
            res = "{\"msg\":\"fail\",\"reason\":\"数据丢失！\"}";
        }
        context.Response.Write(res);
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}