<%@ WebHandler Language="C#" Class="TerminalAgentRegister_V3" %>

using System;
using System.Web;
using TJ.Model;
using TJ.BLL;
using TJ.DBUtility;
using System.Data;
using commonlib;

public class TerminalAgentRegister_V3 : IHttpHandler
{
    readonly TabExecute _tab = new TabExecute();
    private readonly CommonFunWL commfunwl = new CommonFunWL();
    readonly BTJ_RegisterCompanys _btjRegister = new BTJ_RegisterCompanys();
    string[] cityanprovince;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        if (!string.IsNullOrEmpty(context.Request.Form["tname"]) && !string.IsNullOrEmpty(context.Request.Form["masterid"]) && !string.IsNullOrEmpty(context.Request.Form["tsheng"]) && !string.IsNullOrEmpty(context.Request.Form["tshi"]) && !string.IsNullOrEmpty(context.Request.Form["taddress"]) && !string.IsNullOrEmpty(context.Request.Form["tfzr"]) && !string.IsNullOrEmpty(context.Request.Form["tfzrphone"]) && context.Request.Form["yyzzpic"] != null && context.Request.Form["pjxsnm"] != null)
        {
            try
            {
                string tname = context.Request.Form["tname"];
                string tsheng = context.Request.Form["tsheng"];
                string tshi = context.Request.Form["tshi"];
                string taddress = context.Request.Form["taddress"];
                string taddressgps = string.Empty;
                if (!string.IsNullOrEmpty(context.Request.Form["taddressgps"]))
                {
                    taddressgps = context.Request.Form["taddressgps"];
                }
                string csjlnm=String.Empty;
                if (!string.IsNullOrEmpty(context.Request.Form["csjlnm"]))
                {
                    csjlnm = context.Request.Form["csjlnm"];
                }
                string masterid = context.Request.Form["masterid"];
                string tfzr = context.Request.Form["tfzr"];
                string tfzrphone = context.Request.Form["tfzrphone"];
                string yyzzpic = context.Request.Form["yyzzpic"];
                string pjxsnm = context.Request.Form["pjxsnm"];
                cityanprovince = GetCityIdAndProvince(tshi).Split(new[] { ','}, StringSplitOptions.RemoveEmptyEntries);
                if ((cityanprovince.Length>0) &&
                    _btjRegister.CheckIsExistByFilterString("CompName='" + tname + "' and CTID=" + cityanprovince[0]))
                {
                    context.Response.Write("{\"re\":0,\"msg\":\"该终端店名已经注册!\"}");
                }
                else
                {
                    MTJ_RegisterCompanys mcomp = new MTJ_RegisterCompanys();
                    mcomp.ParentID = 0;
                    mcomp.CompName = tname;
                    mcomp.CompTypeID = 486;
                    mcomp.Position = taddressgps;
                    mcomp.Address = taddress;
                    mcomp.ManagerName = csjlnm;
                    mcomp.Agent_Code = commfunwl.CreateAutoCode(masterid, "T");
                    mcomp.MasterID = int.Parse(string.IsNullOrEmpty(masterid) ? "0" : masterid);
                    if (cityanprovince.Length > 1)
                    {
                        mcomp.CTID = int.Parse(cityanprovince[0]);
                    }
                    mcomp.BusinessLicencePicture = yyzzpic;
                    mcomp.LegalPerson = tfzr;
                    mcomp.MobilePhoneNumber = tfzrphone;
                    mcomp.DisAuthorDate = DateTime.Now.AddYears(20);
                    mcomp.AuthoredDate = DateTime.Now;
                    mcomp.RegisterDate = DateTime.Now;
                    mcomp.Remarks = pjxsnm;
                    object objid = _btjRegister.Insert(mcomp);
                    BTJ_User buser = new BTJ_User();
                    MTJ_User muser = new MTJ_User();
                    muser.LoginName = tfzrphone;
                    muser.MobileNumber = tfzrphone;
                    muser.NickName = tfzr;
                    muser.CompID = Convert.ToInt32(objid);
                    muser.RID = 160;
                    muser.AddressInfo = taddress;
                    muser.IsActived = 3;
                    muser.RegisterDate = DateTime.Now;
                    muser.PassWords = CommonFun.Md5hash_String(tfzrphone.Substring(tfzrphone.Length - 6));
                    buser.Insert(muser);
                    HuYi_Info.HY_dxinfoAutoSign("尊敬的赵凯,有新终端店【"+tname+"】注册信息,请及时审核!","18889810919");
                    context.Response.Write("{\"re\":1,\"msg\":\"注册成功！信息正在审核中,请留言短信通知!\"}");
                } 
            }
            catch
            {
                context.Response.Write("{\"re\":0,\"msg\":\"抱歉！网络异常,请稍侯再试!\"}");
            }
        }
        else
        {
            context.Response.Write("{\"re\":0,\"msg\":\"参数不全\"}");
        }
    }

    private string getdepartidstring()
    {
        string tempstring = "";
        if (cityanprovince.Length > 1)
        {
            DataTable dt = _tab.ExecuteQuery("select departid from TJ_DepartMent_ProvinceAuthor where provinceid=" + cityanprovince[1], null);
            if (dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    if (tempstring.Length.Equals(0))
                    {
                        tempstring = dr[0].ToString();
                    }
                    else
                    {
                        tempstring += "," + dr[0];
                    }
                }
            }
            dt.Dispose();
        }
        return tempstring;
    }

    private string  GetCityIdAndProvince(string shiname)
    {
        string tempvalue = "0";
        DataTable dttemp = _tab.ExecuteQuery("select CID,ParentID from TJ_BaseClass where CName like '%" + shiname.Replace("市", "").Replace("区", "") + "%'", null);
        if (dttemp.Rows.Count > 0)
        {
            tempvalue = dttemp.Rows[0][0]+","+dttemp.Rows[0][1];
        }
        dttemp.Dispose();
        return tempvalue;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}