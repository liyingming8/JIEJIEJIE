#region Using Directives 
using System;
using System.Data;
using System.Web;
using System.Web.UI; 
using commonlib;
using TJ.DBUtility;

#endregion

public partial class Login : Page
{
    private readonly DBClass db = new DBClass("0");
    TabExecute tabexe = new TabExecute();
    Security sc = new Security();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            isshow.Value = DAConfig.Showmode; 
            if (Request.QueryString["flag"] != null)
            {
                string flag = Request.QueryString["flag"];
                string name = flag.Substring(0, 7);
                int timestamp = int.Parse(flag.Substring(7, flag.Length - 7));
                if (name == "tourist")
                {
                    int ts = ConvertDateTimeInt(DateTime.Now);
                    if (ts - timestamp < 10)
                    {
                        var httpCookie = Response.Cookies["TJCOMPID"];
                        if (httpCookie != null) httpCookie.Value = "1";
                        var cookie = Response.Cookies["TJCOMPID"];
                        if (cookie != null)
                            cookie.Expires = DateTime.Now.AddHours(1);

                        var httpCookie1 = Response.Cookies["TJCompTypeID"];
                        if (httpCookie1 != null) httpCookie1.Value = "47";
                        var cookie1 = Response.Cookies["TJCompTypeID"];
                        if (cookie1 != null)
                            cookie1.Expires = DateTime.Now.AddHours(1);

                        var httpCookie2 = Response.Cookies["TJRID"];
                        if (httpCookie2 != null) httpCookie2.Value = "152";
                        var cookie2 = Response.Cookies["TJRID"];
                        if (cookie2 != null) cookie2.Expires = DateTime.Now.AddHours(1);

                        var httpCookie3 = Response.Cookies["TJUID"];
                        if (httpCookie3 != null) httpCookie3.Value = "5552020";
                        Response.Cookies["TJUID"].Expires = DateTime.Now.AddHours(1);
                        Response.Cookies["TJUName"].Value = "%e6%b8%b8%e5%ae%a2";
                        Response.Cookies["TJUName"].Expires = DateTime.Now.AddHours(1);
                        Server.Transfer("~/views/index.aspx", true);
                    }
                }
            }
        }

    }

    public int ConvertDateTimeInt(DateTime time)
    {
        int intResult = 0;
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        intResult = Convert.ToInt32((time - startTime).TotalSeconds);
        return intResult;
    }
    protected void Button_Login_Click(object sender, EventArgs e)
    {
        var httpCookie = Request.Cookies["tjyzm"];
        if (httpCookie != null && (httpCookie.Value != null &&
                                                 !httpCookie.Value.ToLower().Trim().Equals(loginvercode.Value.ToLower().Trim())))
        {
            MessageBox.Show(Page, "输入验证码不正确，请重新输入!");
            return;
        }
        if (username.Value.Trim().Equals("") || userpassword.Value.Trim().Equals(""))
        {
            MessageBox.Show(Page, "请输入用户名和密码信息!");
        }
        else
        { 
            //IList<MTJ_User> mulist = bluser.GetListsByFilterString("IsActived=1 and RID<>0 and LoginName='" + TextBox_UserName.Text.Trim() + "' and PassWords='" + commonlib.CommonFun.Md5hash_String(TextBox_PassWord.Text.Trim()) + "'");
            var mulist = tabexe.ExecuteQuery("select RID,IsActived,RegisterDate,LoginName,UserID,CompID,departid from TJ_User where " +
                                    "LoginName is not null and LoginName='" + username.Value + "' and PassWords='" +
                                    CommonFun.Md5hash_String(userpassword.Value) + "'", null);
            if (mulist.Rows.Count > 0)
            {
                if (int.Parse(mulist.Rows[0]["RID"].ToString()) == 44)
                {
                    MessageBox.Show(Page, "已到活动结束时间!如有疑问请联系总部!");
                    return;
                }
                if (mulist.Rows[0]["IsActived"].ToString() == "0")
                {
                    username.Value = "";
                    userpassword.Value = "";
                    MessageBox.Show(Page, "抱歉，该用户尚未激活!");
                    return;
                }
                if (mulist.Rows[0]["RID"].ToString() == "0")
                {
                    username.Value = "";
                    userpassword.Value = "";
                    MessageBox.Show(Page, "抱歉，该用户尚未指定角色!");
                    return;
                }
                if (int.Parse(mulist.Rows[0]["IsActived"].ToString()) == 2 && DateTime.Compare(Convert.ToDateTime(mulist.Rows[0]["RegisterDate"].ToString()).AddDays(3), DateTime.Now) > 0)
                {
                    username.Value = "";
                    userpassword.Value = "";
                    MessageBox.Show(Page, "抱歉，该用户已经超过试用期限!");
                    return;
                }
                if (int.Parse(mulist.Rows[0]["IsActived"].ToString()) == 3)
                {
                    username.Value = "";
                    userpassword.Value = "";
                    MessageBox.Show(Page, "抱歉，该用户已被系统冻结!");
                    return;
                }
                var userName = new HttpCookie("TJOSUName");
                userName.Value = sc.EncryptQueryString(mulist.Rows[0]["LoginName"].ToString().Trim());
                userName.Expires.AddDays(1);
                Response.Cookies.Add(userName);

                var userID = new HttpCookie("TJOSUID");
                userID.Value = sc.EncryptQueryString(mulist.Rows[0]["UserID"].ToString());
                userID.Expires.AddDays(1);
                Response.Cookies.Add(userID);

                var tjUserRid = new HttpCookie("TJOSRID");
                tjUserRid.Value = sc.EncryptQueryString(mulist.Rows[0]["RID"].ToString());
                tjUserRid.Expires.AddDays(1);
                Response.Cookies.Add(tjUserRid);

                string compgrade =  tabexe.ExecuteQueryForSingleValue(
                    "select CompGrade from TJ_RoleInfo where RID=" + mulist.Rows[0]["RID"]);

                var iscompgrade = new HttpCookie("TJOSISCOMPGRADE");
                iscompgrade.Value = sc.EncryptQueryString(compgrade);
                iscompgrade.Expires.AddDays(1);
                Response.Cookies.Add(iscompgrade);


                var userUnid = new HttpCookie("TJOSCOMPID");
                userUnid.Value = sc.EncryptQueryString(mulist.Rows[0]["CompID"].ToString().Trim());
                userUnid.Expires.AddDays(1);
                Response.Cookies.Add(userUnid);

                var userdepartid = new HttpCookie("TJOSDEPARTID");
                userdepartid.Value = sc.EncryptQueryString(mulist.Rows[0]["departid"].ToString().Trim());
                userdepartid.Expires.AddDays(1);
                Response.Cookies.Add(userdepartid); 

                var ip = Request.UserHostAddress;
                db.InserLOG(mulist.Rows[0]["CompID"].ToString().Trim(), mulist.Rows[0]["UserID"].ToString(), "431", ip);
                DataTable dttemp =
                    tabexe.ExecuteQuery(
                        "select CompTypeID,CompName from TJ_RegisterCompanys where CompID=" + mulist.Rows[0]["CompID"],
                        null); 
                if (dttemp != null && dttemp.Rows.Count > 0)
                {
                    var tjCompTypeId = new HttpCookie("TJOSCompTypeID");
                    tjCompTypeId.Value = sc.EncryptQueryString(dttemp.Rows[0]["CompTypeID"].ToString());
                    tjCompTypeId.Expires.AddDays(1);
                    Response.Cookies.Add(tjCompTypeId);

                    var tjCompCompNM = new HttpCookie("TJOSCompName");
                    tjCompCompNM.Value = sc.EncryptQueryString(dttemp.Rows[0]["CompName"].ToString());
                    tjCompCompNM.Expires.AddDays(1);
                    Response.Cookies.Add(tjCompCompNM);
                }

                mulist.Dispose(); 
                //if (iszc.Value == "1" && mulist.Rows[0]["CompID"].ToString() == "2")
                //{
                //    db.Update_SingleData("TJ_User", "UserID=" + mulist.Rows[0]["UserID"].ToString(), "WXNumber = '" + Request.Cookies["WXnumber_2_11"].Value + "'");
                //}

                //Server.Transfer("~/views/index.aspx", true);
                if (mulist.Rows[0]["LoginName"].ToString().Trim() == "demo")
                {
                    Server.Transfer("yanshi/showyanshi.aspx", true);

                }
                else
                {
                    Server.Transfer("~/views/index.aspx", true);
                }
            }
            else
            {
                var mulist1 = tabexe.ExecuteQuery("select count(UserID) from TJ_User where LoginName='" + username.Value + "'", null); 
                if (mulist1.Rows.Count > 0)
                {
                    username.Value = "";
                    userpassword.Value = "";
                    MessageBox.Show(Page, "密码错误!");
                }
                else
                {
                    username.Value = "";
                    userpassword.Value = "";
                    MessageBox.Show(Page, "尚未找到相关用户信息!");
                }
            }
        }
    }
}