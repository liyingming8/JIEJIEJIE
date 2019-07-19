using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using commonlib;
using TJ.Model;
using WxBaseAPITJ_V2;

public partial class Admin_TJ_Role_Package_Choose_Simple : AuthorPage
{
    BTJ_Role_Package bll = new BTJ_Role_Package();
    BTJ_Comp_Roles btjCompRoles = new BTJ_Comp_Roles();
    DBClass db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string[] datafromactive = Request.QueryString["cnt"].Split('|');
            hf_count.Value = datafromactive[1];
            hf_pid.Value = datafromactive[0];
            hf_proid.Value = datafromactive[2];
            hf_cpid.Value = datafromactive[3];
            string dz = db.GetSingleData("discount", "TJ_SWM_PriceDiscountMode", "shuliang<= " + datafromactive[1] + "  order by id desc");
            if (dz == "no")
            {
                dz = "1";
            }
            else
            {
                dz = (Convert.ToDecimal(dz) / 100).ToString();
            }
            hf_zk.Value = dz;
            if (Request.Cookies["WXnumber_333_1"] != null)
            {
                hf_compid.Value = GetCookieCompID();
                DisplayData();
            }
            else
            {
                WXlogin_api wxlogin = new WXlogin_api("333", "1", "zonghe");
                Response.Redirect(wxlogin.GetWxCodeRedirectString_Byid("Admin/TJ_Role_Package_Choose_Simple.aspx?cnt=" + Request.QueryString["cnt"], "333", "1"), true);
            }
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_Role_PackageAddEdit.aspx?cmd=" + Sc.EncryptQueryString("edit") + "&ID={0}',600,580,'功能打包')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            //if (db.GetSingleData("id", "TJ_Comp_Roles", "compid=" + GetCookieCompID() + " and rpackid=" + ((HiddenField)e.Row.FindControl("hf_id")).Value) != "no")
            //{
            ((Label)e.Row.FindControl("Labelprice")).Text =(Convert.ToDecimal(((Label)e.Row.FindControl("Labelprice")).Text)*Convert.ToDecimal(hf_zk.Value)/100).ToString("#0.000");
            //    ((CheckBox)e.Row.FindControl("CheckBoxSelect")).Enabled = false;
            //}
            //else
            //{
            e.Row.Attributes.Add("onclick", "select(" + e.Row.RowIndex + ")");
            //}

        }
    }
    private void DisplayData()
    {
        GridView1.DataSource = bll.GetListsByFilterString("id<>1");
        GridView1.DataBind();
    }

    private MTJ_Comp_Roles _mtjCompRoles;
    private bool _isexist;
    protected void ButtonYes_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (((CheckBox)row.FindControl("CheckBoxSelect")).Checked)
            {
                string rpid = ((HiddenField)row.FindControl("hf_id")).Value;
                _isexist = btjCompRoles.CheckIsExistByFilterString("CompID=" + GetCookieCompID() + "and rpackid=" + rpid);
                if (!_isexist)
                {
                    _mtjCompRoles = new MTJ_Comp_Roles();
                    _mtjCompRoles.rpackid = int.Parse(rpid);
                    _mtjCompRoles.compid = int.Parse(GetCookieCompID());
                    btjCompRoles.Insert(_mtjCompRoles);
                }
            }
        }
        Response.Redirect("http://tjfnew.china315net.com/common/route.ashx?cpid=" + hf_cpid.Value, true);
    }
}
