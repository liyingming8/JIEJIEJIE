<%@ WebHandler Language="C#" Class="companyregisterswmperson" %> 
using System;
using System.Web;
using commonlib;
using TJ.BLL;
using TJ.Model;

public class companyregisterswmperson : IHttpHandler
{
    //Security sc = new Security();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (string.IsNullOrEmpty(context.Request.Form["p_name"]))
        {
            context.Response.Write("{\"rs\":0,\"nr\":\"请正确输入\"}");
        }
        else
        {
            string pName = context.Request.Form["p_name"];
            string pCode = context.Request.Form["p_code"];
            var btjcompany = new BTJ_RegisterCompanys();
            var btjuser = new BTJ_User();
            if (btjcompany.CheckIsExistByFilterString("CompName='" + pName + "' and CompCode='" + pCode + "'"))
            {
                context.Response.Write("{\"rs\":1,\"nr\":\"该姓名已经存在\"}");
            }
            else
            {
                string path = context.Server.MapPath(@"..\upload");
                string pPhone = context.Request.Form["p_phone"];
                string pAddr = context.Request.Form["p_addr"];
                //string pAddrname = context.Request.Form["p_addrname"];
                string pCity = context.Request.Form["p_city"];
                string lat = context.Request.Form["lat"];
                string lng = context.Request.Form["lng"];
                string openid = context.Request.Form["openid"];
                //openid = sc.DecryptQueryString(openid);
                HttpPostedFile file = context.Request.Files["p_codeimg"];
                string filename = DateTime.Now.ToString("yyyyMMdd") + "-" + DateTime.Now.Ticks + file.ContentType.Replace("image/", ".");
                file.SaveAs(path + "\\swmsh\\" + filename);
                var mtjRegisterCompanys = new MTJ_RegisterCompanys();
                mtjRegisterCompanys.CompName = pName;
                mtjRegisterCompanys.CompTypeID = 484;
                mtjRegisterCompanys.CompCode = pCode;
                mtjRegisterCompanys.RegisterDate = DateTime.Now;
                mtjRegisterCompanys.BusinessLicencePicture = "upload\\swmsh\\" + filename;
                mtjRegisterCompanys.LegalPerson = pName; 
                mtjRegisterCompanys.MobilePhoneNumber = pPhone;
                mtjRegisterCompanys.Address = pCity + " " + pAddr;
                mtjRegisterCompanys.Position = lat + "," + lng;
                mtjRegisterCompanys.IsCompany = true;
                mtjRegisterCompanys.UseSWM = true;
                mtjRegisterCompanys.CompAutherID = 67;
                object compid = btjcompany.Insert(mtjRegisterCompanys);
                var muser = new MTJ_User();
                muser.CompID = int.Parse(compid.ToString());
                muser.WXNumber = openid;
                muser.LoginName = pPhone;
                muser.RID = 155;
                muser.IsActived = 1;
                muser.MobileNumber = pPhone;
                muser.PassWords = CommonFun.Md5hash_String("123456");
                muser.AddressInfo = pCity + " " + pAddr;
                muser.RegisterDate = DateTime.Now;
                btjuser.Insert(muser);
                context.Response.Write("{\"rs\":100,\"nr\":\"您的资料已经提交，请稍候！\"}");
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}