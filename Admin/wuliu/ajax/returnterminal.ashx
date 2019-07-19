<%@ WebHandler Language="C#" Class="returnterminal" %>

using System; 
using System.Data;
using System.Web;
using TJ.DBUtility; 

public class returnterminal : IHttpHandler {
    TabExecute tab = new TabExecute();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string returnstring=String.Empty;
        if (!string.IsNullOrEmpty(context.Request.QueryString["pid"]))
        {
            string parentid = context.Request.QueryString["pid"];
            returnstring = string.Empty;
            DataTable dt = tab.ExecuteQuery(
                "select CompID,CompName from TJ_RegisterCompanys where ParentID=" + parentid +" order by CompName", null);
            if (dt!=null&&dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (string.IsNullOrEmpty(returnstring))
                    {
                        returnstring="[{name:\""+row["CompName"]+"\",id:"+row["CompID"]+"}";
                    }
                    else
                    {
                        returnstring += ",{name:\"" + row["CompName"] + "\",id:" + row["CompID"] + "}";
                    }
                } 
                returnstring += "]";
            } 
            if (dt != null) dt.Dispose();
            context.Response.Write(returnstring.Length.Equals(0) ? "[]" : returnstring);
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}