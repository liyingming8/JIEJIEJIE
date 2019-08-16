<%@ WebHandler Language="C#" Class="companyregister" %>
using System;
using System.Web;
using commonlib;
using TJ.BLL;
using TJ.Model;

public class companyregister : IHttpHandler { 
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (string.IsNullOrEmpty(context.Request.Form["c_name"]))
        {
            context.Response.Write("{\"rs\":0,\"nr\":\"请正确输入\"}");
        }
        else
        {
            string c_name = context.Request.Form["c_name"];
            var btjcompany = new BTJ_RegisterCompanys();
            var btjuser = new BTJ_User();
            if (btjcompany.CheckIsExistByFilterString("CompName='" + c_name + "'"))
            {
                context.Response.Write("{\"rs\":1,\"nr\":\"公司名称已经存在\"}");
                     context.Response.End();
            }
            else
            {
                string path = context.Server.MapPath(@"..\upload");
                string cType = context.Request.Form["c_type"];
                string cIntro = context.Request.Form["c_intro"];
                string cMaster = context.Request.Form["c_master"];
                string cPhone = context.Request.Form["c_phone"];
                string cAddr = context.Request.Form["c_addr"];
                //string cAddrname = context.Request.Form["c_addrname"];
                string cCity = context.Request.Form["c_city"];
                string lat = context.Request.Form["lat"];
                string lng = context.Request.Form["lng"];
                string openid = context.Request.Form["openid"];
                HttpPostedFile file = context.Request.Files["c_lisenseimg"];
                string filename = DateTime.Now.ToString("yyyyMMdd")+"-"+ DateTime.Now.Ticks + file.ContentType.Replace("image/", ".");
                file.SaveAs(path + "\\swmsh\\" + filename);
                var mtjRegisterCompanys = new MTJ_RegisterCompanys();
                mtjRegisterCompanys.CompName = c_name;
                mtjRegisterCompanys.CompTypeID = int.Parse(cType);
                mtjRegisterCompanys.DetailDiscription = cIntro;
                mtjRegisterCompanys.BusinessLicencePicture = "upload\\swmsh\\" + filename;
                mtjRegisterCompanys.LegalPerson = cMaster;
                mtjRegisterCompanys.RegisterDate = DateTime.Now;
                mtjRegisterCompanys.MobilePhoneNumber = cPhone;
                mtjRegisterCompanys.Address = cCity+" "+cAddr;
                mtjRegisterCompanys.Position = lat + "," + lng;
                mtjRegisterCompanys.CompAutherID = 67;
                mtjRegisterCompanys.IsCompany = true;
                mtjRegisterCompanys.UseSWM = true;
                object compid = btjcompany.Insert(mtjRegisterCompanys);
                var muser = new MTJ_User();
                muser.CompID = int.Parse(compid.ToString());
                muser.WXNumber = openid;
                muser.PassWords = CommonFun.Md5hash_String("123456");
                muser.LoginName = cPhone;
                muser.MobileNumber = cPhone;
                muser.RID = 155;
                muser.AddressInfo = cCity + " " + cAddr;
                //状态
                muser.IsActived = 0;
                //muser.IsActived = 1;
                muser.RegisterDate = DateTime.Now;
                btjuser.Insert(muser);
                HuYi_Info.HY_dxinfoNoYzm("三维码注册待审核，请马上审核！", "18889810919", "天鉴防伪");
                //context.Response.Write("{\"rs\":100,\"nr\":\"您的资料已经提交，请稍候！\"}");
                    context.Response.End();
            }
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}