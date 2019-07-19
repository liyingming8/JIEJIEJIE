<%@ WebHandler Language="C#" Class="getmodelinfo" %>

using System.Data;
using System.Text;
using System.Web;
using TJ.DBUtility;

public class getmodelinfo : IHttpHandler {
    readonly PGTabExecuteCRM _pgtabexe = new PGTabExecuteCRM();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (!string.IsNullOrEmpty(context.Request["cd"]) && !string.IsNullOrEmpty(context.Request["gd"]))
        {
            string tempsql =
                "SELECT m.pagename, m.logourl, m.linkpathstring FROM public.tj_page_model m,public.tj_page_comp_info cm where m.id=cm.mdid and cm.compid=" + context.Request["cd"] + " and cm.custgid=" + context.Request["gd"];
            DataTable dttemp = _pgtabexe.ExecuteQuery(tempsql, null);
            if (dttemp.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder(); 
                foreach (DataRow row in dttemp.Rows)
                {
                    if (sb.Length.Equals(0))
                    {
                        sb.Append("{\"nm\":\"" + row["pagename"] + "\",\"ic\":\"" + row["logourl"] + "\",\"lk\":\"" +
                                  row["linkpathstring"] + "\"}");
                    }
                    else
                    {
                        sb.Append(",{\"nm\":\"" + row["pagename"] + "\",\"ic\":\"" + row["logourl"] + "\",\"lk\":\"" +
                               row["linkpathstring"] + "\"}");
                    }
                }
                context.Response.Write("["+sb+"]");
            }
            else
            {
                context.Response.Write("{\"rst\":0}");
            }
        }
        else
        {
            context.Response.Write("{\"rst\":0}");
        }
        //context.Response.Write("Hello World");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}