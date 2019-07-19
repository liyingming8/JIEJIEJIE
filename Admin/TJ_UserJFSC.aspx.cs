using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class TJ_UserJFSC : AuthorPage
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
            GridView1.DataSource = db.get375SCAD(ReturnFilterString(userid));
            GridView1.DataBind();
        }

        else
        {
            GridView1.DataSource = db.get375SCAD(ReturnFilterStringNuser());
            GridView1.DataBind();
        }
    }

    private string tempstring = "";

    private string ReturnFilterStringNuser()
    {
        tempstring = "a.CompID=" + GetCookieCompID() + " and a.LabelCode=b.LabelCode and a.UserID=b.UserID ";
        if (DateBegin.Text != "" && DateEnd.Text != "")
        {
            tempstring += " and " + "a.SCTime>='" + DateBegin.Text + "' and a.SCTime<'" +
                          Convert.ToDateTime(DateEnd.Text).AddDays(1) + "'";
        }
        if (inputSearchKeyword.Value.Length > 0)
        {
            if (DDLField.SelectedValue == "UserID")
            {
                string uid = "0";
                IList<MTJ_User> muser = bll.GetListsByFilterString("NickName like '%" + inputSearchKeyword.Value + "%'");
                if (muser.Count > 0)
                {
                    uid = muser[0].UserID.ToString();
                }

                tempstring += "and a.UserID=" + uid + "";
            }
            else
            {
                tempstring += "and a." + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
            }
        }

        tempstring += "order by a.SCTime";
        return tempstring;
    }

    private string ReturnFilterString(string userid)
    {
        tempstring = "a.CompID=" + GetCookieCompID() + " and a.UserID=" + userid + "";
        if (DateBegin.Text != "" && DateEnd.Text != "")
        {
            tempstring += " and " + "a.SCTime>='" + DateBegin.Text + "' and a.SCTime<'" +
                          Convert.ToDateTime(DateEnd.Text).AddDays(1) + "'";
        }

        tempstring += "order by a.SCTime";
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
        // 得到该控件
        GridView theGrid = sender as GridView;
        int newPageIndex = 0;
        if (e.NewPageIndex == -3)
        {
            //点击了Go按钮
            TextBox txtNewPageIndex = null;

            //GridView较DataGrid提供了更多的API，获取分页块可以使用BottomPagerRow 或者TopPagerRow，当然还增加了HeaderRow和FooterRow
            GridViewRow pagerRow = theGrid.BottomPagerRow;

            if (pagerRow != null)
            {
                //得到text控件
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
            }
            if (txtNewPageIndex != null)
            {
                //得到索引
                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
            }
        }
        else
        {
            //点击了其他的按钮
            newPageIndex = e.NewPageIndex;
        }
        //防止新索引溢出
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;

        //得到新的值
        theGrid.PageIndex = newPageIndex;

        //重新绑定
        //fillgridview();
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
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#CCFF83'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        fillgridview(HF_UserID.Value, GetCookieCompID());
    }
}