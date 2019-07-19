<%@ WebHandler Language="C#" Class="tpsp" %>

using System.Data;
using System.Web;
using Newtonsoft.Json;
using TJ.DBUtility;

public class tpsp : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.Form["comp"]) && !string.IsNullOrEmpty(context.Request.Form["tp"]))
        {
            var tab = new TabExecute();
            string compid = context.Request.Form["comp"];
            string type = context.Request.Form["tp"]; //1:首页顶部；2:兑奖页顶部；3:精品推荐顶部；5:视频；7:会员顶部图片;8:扫码落地页顶部
            string goodsID = context.Request.Form["gid"];
            DataTable dt = tab.ExecuteQuery("select ADID,FilePath,Discriptions,SpecialURLLink from TJ_CompADInfo where IsActive=1 and ADID="+type+" and CompID=" +compid + (string.IsNullOrEmpty(goodsID)?"":" and GoodsID="+goodsID), null);
            if (dt.Rows.Count > 0)
            {
                context.Response.Write(JsonConvert.SerializeObject(dt));
            }
            else
            {
                context.Response.Write(0);
            }
            dt.Dispose();
        }
        else
        {
            context.Response.Write(0);
        } 
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}