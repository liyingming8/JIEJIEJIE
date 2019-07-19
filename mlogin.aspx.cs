using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class mlogin : Page
{
    private readonly BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ln"] != null && Request.QueryString["ln"].Trim().Length > 0)
            {
                txtLoginName.Text = Server.HtmlDecode(Request.QueryString["ln"].Trim());
            }
        }
    }

    protected void botton_login_Click(object sender, EventArgs e)
    {
        if (txtLoginName.Text.Trim().Equals("") || txtPassword.Text.Trim().Equals(""))
        {
            MessageBox.Show(Page, "请输入用户名和密码信息!");
        }
        else
        {
            BTJ_User bluser = new BTJ_User();
            IList<MTJ_User> mulist =
                bluser.GetListsByFilterString("RID<>0 and LoginName='" + txtLoginName.Text.Trim() + "' and PassWords='" +
                                              CommonFun.Md5hash_String(txtPassword.Text.Trim()) + "'");

            if (mulist.Count > 0)
            {
                HttpCookie UserName = new HttpCookie("TJUName");
                UserName.Value = HttpUtility.UrlEncode(mulist[0].LoginName.Trim());
                UserName.Expires.AddDays(1);
                Response.Cookies.Add(UserName);

                HttpCookie UserID = new HttpCookie("TJUID");
                UserID.Value = HttpUtility.UrlEncode(mulist[0].UserID.ToString());
                UserID.Expires.AddDays(1);
                Response.Cookies.Add(UserID);

                HttpCookie TJUserRID = new HttpCookie("TJRID");
                TJUserRID.Value = HttpUtility.UrlEncode(mulist[0].RID.ToString());
                TJUserRID.Expires.AddDays(1);
                Response.Cookies.Add(TJUserRID);

                HttpCookie UserUNID = new HttpCookie("TJCOMPID");
                UserUNID.Value = HttpUtility.UrlEncode(mulist[0].CompID.ToString().Trim());
                UserUNID.Expires.AddDays(1);
                Response.Cookies.Add(UserUNID);

                HttpCookie TJCompTypeID = new HttpCookie("TJCompTypeID");
                TJCompTypeID.Value =
                    HttpUtility.UrlEncode(bcompany.GetList(mulist[0].CompID).CompTypeID.ToString().Trim());
                TJCompTypeID.Expires.AddDays(1);
                Response.Cookies.Add(TJCompTypeID);

                //HF_CID.Value = UserUNID.Value;
                Response.Redirect("msite/OrderInfoNoAccept.aspx");
            }
            else
            {
                txtLoginName.Text = "";
                txtPassword.Text = "";
                MessageBox.Show(Page, "尚未找到相关用户信息!");
            }
        }
    }
}