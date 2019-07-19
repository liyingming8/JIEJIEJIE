<%@ WebHandler Language="C#" Class="sendCode" %>

using System.Collections.Generic;
using System.Web;
using commonlib;
using TJ.BLL;
using TJ.Model;

public class sendCode : IHttpHandler
{
    BTB_AllowPhoneNum bTB = new BTB_AllowPhoneNum();
    MTB_AllowPhoneNum mtb = new MTB_AllowPhoneNum();
    BTJ_RegisterCompanys registerCompanys = new BTJ_RegisterCompanys();
    MTJ_RegisterCompanys MTJ_RegisterCompanys = new MTJ_RegisterCompanys();
    Security sc = new Security();
    public void ProcessRequest(HttpContext context)
    {
        string res = "fail";
        //if (context.Request.Url.Host == context.Request.UrlReferrer.Host)
        //{
        //    if (context.Request.Form["agtid"] != null &&context.Request.Form["code"] != null && context.Request.Form["product"] != null && context.Request.Form["time"] != null && context.Request.Form["address"] != null)
        //    {
        //        string agtid = context.Request.Form["agtid"];
        //        MTJ_RegisterCompanys = registerCompanys.GetList(int.Parse(agtid));
        //        string code = context.Request.Form["code"];
        //        string product = context.Request.Form["product"];
        //        string time = context.Request.Form["time"];
        //        string address = context.Request.Form["address"];
        //        mtb = bTB.GetListsByFilterString("compid = "+sc.DecryptQueryString(context.Request.Cookies["TJOSCOMPID"].Value)+" and master like '%稽查员' ")[0];
        //        string info = "您好，亲爱的 "+mtb.Master+"，现公司发现在" + address + "有一条可疑窜货信息，标签序号为：" + code + "，产品是：" + product + "， 发往经销商："+MTJ_RegisterCompanys.CompName+"，系统侦察时间为：" + time + "，请注意核查！";
        //        string duanxin = HuYi_Info.HY_dxinfoNoYzm(info, mtb.Phone_Num, "海南天鉴防伪科技");
        //        res = "ok";
        //    }
        //}
        if (context.Request.Form["agtid"] != null &&context.Request.Form["code"] != null && context.Request.Form["product"] != null && context.Request.Form["time"] != null && context.Request.Form["address"] != null)
        {
            string agtid = context.Request.Form["agtid"];
            MTJ_RegisterCompanys = registerCompanys.GetList(int.Parse(agtid));
            string code = context.Request.Form["code"];
            string product = context.Request.Form["product"];
            string time = context.Request.Form["time"];
            string address = context.Request.Form["address"];
            IList<MTB_AllowPhoneNum> alist = bTB.GetListsByFilterString("compid = "+sc.DecryptQueryString(context.Request.Cookies["TJOSCOMPID"].Value)+" and master like '%稽查员' ");
            if (alist.Count > 0)
            {
                MTB_AllowPhoneNum mtb = alist[0];
                string info = "您好，亲爱的 "+mtb.Master+"，现公司发现在" + address + "有一条可疑窜货信息，标签序号为：" + code + "，产品是：" + product + "， 发往经销商："+MTJ_RegisterCompanys.CompName+"，系统侦察时间为：" + time + "，请注意核查！";
                string duanxin = HuYi_Info.HY_dxinfoNoYzm(info, mtb.Phone_Num, "海南天鉴防伪科技");
                res = "ok";
            } 
        }
        context.Response.ContentType = "text/plain";
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