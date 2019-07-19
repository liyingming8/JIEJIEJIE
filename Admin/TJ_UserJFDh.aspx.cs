using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class TJ_UserJFDh : AuthorPage
{
    private readonly BTJ_User bll = new BTJ_User();
    private MTJ_User mod = new MTJ_User();
    private BTJ_RoleInfo bllrole = new BTJ_RoleInfo();
    private BTJ_RegisterCompanys blrcompany = new BTJ_RegisterCompanys();
    private CommonFun comfun = new CommonFun();
    private CommonFunWL comwl = new CommonFunWL();
    private readonly DBClass db = new DBClass();
    public BTJ_User buser = new BTJ_User();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
            if (Request.QueryString["UserID"] != null)
            {
                Cuser.Style["display"] = "none";
                HF_UserID.Value = Request.QueryString["UserID"].Trim();


                fillgridview(HF_UserID.Value, GetCookieCompID());
            }

            else
            {
                HF_UserID.Value = "";
                fillgridview(HF_UserID.Value, GetCookieCompID());
            }
        }
    }

    private void fillgridview(string userid, string compid)
    {
        if (userid != "")
        {
            GridView1.DataSource = db.getJFDh(ReturnFilterString(userid));
            GridView1.DataBind();
        }

        else
        {
            GridView1.DataSource = db.getJFDh(ReturnFilterStringNuser());
            GridView1.DataBind();
        }
    }

    private string tempstring = "";

    private string ReturnFilterStringNuser()
    {
        tempstring = "OwnCompID=" + GetCookieCompID() + "";
        if (DateBegin.Text != "" && DateEnd.Text != "")
        {
            tempstring += " and " + "UseDate>='" + DateBegin.Text + "' and UseDate<'" +
                          Convert.ToDateTime(DateEnd.Text).AddDays(1) + "'";
        }
        if (inputSearchKeyword.Value.Length > 0)
        {
            string uid = "0";
            IList<MTJ_User> muser = bll.GetListsByFilterString("NickName like '%" + inputSearchKeyword.Value + "%'");
            if (muser.Count > 0)
            {
                uid = muser[0].UserID.ToString();
            }

            tempstring += "and OwnUserID=" + uid + "";
        }

        tempstring += "order by UseDate";
        return tempstring;
    }

    private string ReturnFilterString(string userid)
    {
        tempstring = "OwnCompID=" + GetCookieCompID() + " and OwnUserID=" + userid + "";
        if (DateBegin.Text != "" && DateEnd.Text != "")
        {
            tempstring += " and " + "UseDate>='" + DateBegin.Text + "' and UseDate<'" +
                          Convert.ToDateTime(DateEnd.Text).AddDays(1) + "'";
        }

        tempstring += "order by UseDate";
        return tempstring;
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgridview(HF_UserID.Value, GetCookieCompID());
    }


    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        fillgridview(HF_UserID.Value, GetCookieCompID());
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview(HF_UserID.Value, GetCookieCompID());
    }
}