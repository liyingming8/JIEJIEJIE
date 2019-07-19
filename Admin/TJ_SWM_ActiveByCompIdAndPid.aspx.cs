using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using commonlib;
using Newtonsoft.Json.Linq;
using TJ.BLL;
using TJ.Model;

public partial class Admin_TJ_SWM_ActiveByCompIdAndPid : AuthorPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (IsSuperAdmin())
            {
                foradmin.Visible = true;
                inputcompid.Attributes.Add("onclick", ReturnCompnaySelectScript("所属单位", "0", "UseSWM=1"));
            }
            else
            {
                foradmin.Visible = false;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["pcompid"]))
            {
                hd_compid.Value = Request.QueryString["pcompid"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["pcompnm"]))
            {
                inputcompid.Value = Request.QueryString["pcompnm"];
            }
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        var internet = new InternetHandle();
        string tempvl = internet.GetUrlData("http://www.china315net.com:35224/zhpt/qr3d/pid/from/pkglabel/?label=" + inputactivelabel.Value);
        JObject jo = JObject.Parse(tempvl);
        if (string.IsNullOrEmpty(jo["e"].ToString()))
        {
            DBClass db = new DBClass();
            db.ActivePidAndAuthorAllMoudleInfoFrank(hd_compid.Value, jo["pid"].ToString(), int.Parse(input_totalnm.Value), int.Parse(inputpermitday.Value), inputcompid.Value);
            string result = internet.GetUrlData("http://www.china315net.com:35224/zhpt/active/label/?pid=" + jo["pid"] + "&product_id=0&company_id=" + hd_compid.Value + "&user_id=" + GetCookieUID());
            jo = JObject.Parse(result);
            if (jo["e"].ToString().Trim().Length.Equals(0))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('操作成功');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + jo["e"] + "');", true);
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + jo["e"] + "');", true);
        }
    }
}