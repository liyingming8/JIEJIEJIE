<%@ WebHandler Language="C#" Class="Editpassword" %>

using System.Web;
using TJ.BLL;
using TJ.Model;
using commonlib;


public class Editpassword : IHttpHandler {
    readonly BTJ_User buser = new BTJ_User(); 
    MTJ_User muser = new MTJ_User();  
    
    public void ProcessRequest (HttpContext context) {
        
        string uid = context.Request.QueryString["uid"];
        string pass = context.Request.QueryString["newpass"];
        muser = buser.GetList(int.Parse(uid));
        muser.PassWords = CommonFun.Md5hash_String(pass);
        buser.Modify(muser); 
        context.Response.Write("ok");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}