<%@ WebHandler Language="C#" Class="show" %>

using System.Collections.Generic;
using System.Web;
using TJ.Model;
using TJ.BLL;

public class show : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string _modid = context.Request.Form["modid"];
        string _showorder = context.Request.Form["showorder"];
        string _endid = context.Request.Form["endid"];
        string result = string.Empty;

        BTJ_SysModuleSiteInfo btj = new BTJ_SysModuleSiteInfo();
        IList<MTJ_SysModuleSiteInfo> mtj = btj.GetListsByFilterString("SMID=" + _modid, "ShowOrder");
        if (_endid == "1")
        {
            if (int.Parse(_showorder) < mtj.Count )
            {
                result = "{\"url\":\"" + mtj[int.Parse(_showorder)+1 ].LinkURL +"\",\"mesg\":\""+ mtj[int.Parse(_showorder)+1 ].ShowContent+ "\",\"flag\":\""+(int.Parse(_showorder)+1)+"\"}";
            }
            else  
            {
                result = "{\"url\":\"no\",\"flag\":\""+(int.Parse(_showorder))+"\"}";//最后一页
            }

        }
        else
        {
            if (int.Parse(_showorder) <  mtj.Count   )
            {
                result = "{\"url\":\"" + mtj[int.Parse(_showorder) - 1].LinkURL +"\",\"mesg\":\""+ mtj[int.Parse(_showorder)-1 ].ShowContent+ "\",\"flag\":\""+(int.Parse(_showorder)-1)+"\"}";
            }
            else
            {
                result = "{\"url\":\"nono\",\"flag\":\"1\"}";;//首页
            }
         
        }
           context.Response.Write(result);
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