<%@ WebHandler Language="C#" Class="getcompconfig" %>

using System;
using System.Data;
using System.Text;
using System.Web;
using TJ.DBUtility;

public class getcompconfig : IHttpHandler
{
    private TabExecute tab;
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.QueryString["compid"])&&!string.IsNullOrEmpty(context.Request.QueryString["openid"]))
        {
            string compid = context.Request.QueryString["compid"];
            string openid = context.Request.QueryString["openid"];
           var sb = new StringBuilder();
            sb.Append("{");
            sb.Append(Getismaster(compid,openid));
            sb.Append("," + Getcompshuxing(compid));
            sb.Append("," + Getconfiginfo(compid));
            sb.Append("}");
            context.Response.Write(sb.ToString()); 
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

    private string Getismaster(string compid,string openid)
    {
        tab = new TabExecute();
        string tempstring = string.Empty;
        DataTable dttemp = tab.ExecuteNonQuery("SELECT UserID,CompID,RID,LoginName from TJ_User where RID=155 and CompID=" +compid + " and WXNumber='" +openid + "'");
        if (dttemp.Rows.Count > 0)
        {
            tempstring = "\"ismaster\":1,\"compid\":" + dttemp.Rows[0]["CompID"] + ",\"uid\":" + dttemp.Rows[0]["UserID"] + ",\"rid\":" + dttemp.Rows[0]["RID"] + ",\"nm\":\"" + dttemp.Rows[0]["LoginName"] + "\"";
            dttemp.Dispose(); 
        }
        else
        {
            tempstring = "\"ismaster\":0"; 
        } 
        dttemp.Dispose();
        return tempstring;
    }
    
    private string Getcompshuxing(string compid)
    {
        tab = new TabExecute();
        string tempstring = string.Empty;
        DataTable dttemp = tab.ExecuteQuery("select CompTypeID from TJ_RegisterCompanys where CompID=" + compid, null);
        if (dttemp.Rows.Count > 0)
        {
            if (Convert.ToInt32(dttemp.Rows[0][0]).Equals(484))
            {
                tempstring = "\"isgs\":0";
            }
            else
            {
                tempstring = "\"isgs\":1";
            }
        }
        dttemp.Dispose();
        return tempstring;
    }

    private string Getconfiginfo(string compid)
    {
        string tempreturn = "";;
        tab = new TabExecute();
        DataTable dttemp = tab.ExecuteQuery(
            "select layid,themecolorid,frantbackcolor,logopath,guanzhuweix,guanzhuqrcodeurl,showlogo,updatetime,upuserid,remarks,bigbackgroudimg,wxgzhmc,getnicknmandheader,getweizhi,isshowlogo,showbottom,bottomcontent,pzyz,gotourl from TJ_CompFrontPage_Config where compid=" +
            compid, null);
        if (dttemp.Rows.Count > 0)
        {
            tempreturn = "\"layid\":" + dttemp.Rows[0]["layid"] + ",\"bigbackground\":\"" + dttemp.Rows[0]["bigbackgroudimg"] + "\",\"backcolor\":\"" + dttemp.Rows[0]["frantbackcolor"] + "\",\"logopath\":\"" + dttemp.Rows[0]["logopath"] + "\",\"logo\":\"" + dttemp.Rows[0]["showlogo"] + "\",\"gzwx\":" + (Convert.ToBoolean(dttemp.Rows[0]["guanzhuweix"]) ? 1 : 0) + ",\"gzwxlk\":\"" + dttemp.Rows[0]["guanzhuqrcodeurl"] + "\",\"gzhmc\":\"" + dttemp.Rows[0]["wxgzhmc"] + "\",\"getnicknm\":" + (Convert.ToBoolean(dttemp.Rows[0]["getnicknmandheader"]) ? 1 : 0) + ",\"getplace\":" + (Convert.ToBoolean(dttemp.Rows[0]["getweizhi"]) ? 1 : 0) + ",\"isshowlogo\":" + (Convert.ToBoolean(dttemp.Rows[0]["isshowlogo"]) ? 1 : 0) + ",\"showbottom\":" + (Convert.ToBoolean(dttemp.Rows[0]["showbottom"]) ? 1 : 0) + ",\"bottomcontent\":\"" + dttemp.Rows[0]["bottomcontent"] + "\",\"pzyz\":" + (Convert.ToBoolean(dttemp.Rows[0]["pzyz"]) ? 1 : 0) + ",\"gotourl\":\"" + dttemp.Rows[0]["gotourl"] + "\"";
        }
        else
        {
            tempreturn = "\"layid\":1,\"backcolor\":\"#0099ff\",\"logopath\":\"blue\",\"logo\":\"\",\"gzwx\":0,\"gzwxlk\":\"\"";
        }
        dttemp.Dispose();
        return tempreturn;
    }

}