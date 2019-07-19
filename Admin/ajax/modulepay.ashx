<%@ WebHandler Language="C#" Class="modulepay" %>

using System;
using System.Web;
using TJ.BLL;
using TJ.Model;
using WxBaseAPITJ_V2;

public class modulepay : IHttpHandler
{
    DBClass db = new DBClass();
    BTJ_Role_Package bll = new BTJ_Role_Package();
    BTJ_Comp_Roles btjCompRoles = new BTJ_Comp_Roles();
    private MTJ_Comp_Roles _mtjCompRoles;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string rid = context.Request.Form["rid"];
        string compid = context.Request.Form["compid"];
        string ispay = context.Request.Form["ispay"];
        string paymoney = context.Request.Form["paymoney"];
       string cnt= context.Request.Form["cnt"];
        string pid = context.Request.Form["pid"];
        string proid = context.Request.Form["proid"];
        string result = "";
        string[] rpackid = rid.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
        bool _isexist;
        foreach (string myrid in rpackid)
        {
            _isexist = btjCompRoles.CheckIsExistByFilterString("CompID=" + compid + "and rpackid=" + myrid);
            if (!_isexist)
            {
                _mtjCompRoles = new MTJ_Comp_Roles();
                _mtjCompRoles.rpackid = int.Parse(myrid);
                _mtjCompRoles.compid = int.Parse(compid);
                _mtjCompRoles.isactive = false;
                btjCompRoles.Insert(_mtjCompRoles);
            }
        }
        db.WriteSysTxt("------------支付金额"+paymoney);
        if (ispay == "1" && int.Parse(paymoney) > 0)
        {
            WXpay_api pay = new WXpay_api("wx59969257ee0275da");
            string mch_billno = "tswm" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + compid;
            string openid = context.Request.Cookies["WXnumber_333_1"].Value;
            int money = int.Parse(paymoney);
            WXData back = pay.GetGZHPay(mch_billno, money, "激活-订单", "http://os.china315net.com/Admin/ajax/wx_pay_result.ashx", openid);
            result = "{\"msg\":\"ok\",\"jsapi\":" + pay.GetJsApiParameters() + "}";
            db.RecordActiveInfo(pid, proid, rid, cnt, paymoney, "0", mch_billno,compid);
        }
        else
        {
            db.RecordActiveInfo(pid, proid, rid, cnt, paymoney, "1", "no",compid);
            result = "{\"msg\":\"nopay\"}";
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