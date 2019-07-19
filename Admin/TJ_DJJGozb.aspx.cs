using System;
using System.Data;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class TJ_DJJGozb : AuthorPage
{
    private readonly BTJ_User bll = new BTJ_User();
    private MTJ_User mod = new MTJ_User(); 
    private readonly DBClass db = new DBClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //fillgridview();       
        }
    }


    private void fillgridview()
    {
        GridView1.DataSource = db.GetDKWXJPinfo(ReturnFilterString());
        GridView1.DataBind();
    }

    private string tempstring = "";

    private string ReturnFilterString()
    {
        tempstring = "CompID=" + GetCookieCompID();
        //if (inputSearchKeyword.Value.Length > 0)
        //{
        //    tempstring += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
        //}
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
        fillgridview();
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


    protected void BtnSearch2_Click(object sender, EventArgs e)
    {
        if (Mphone.Value.Trim().Length == 11)
        {
            DataTable da = db.GetAddressBystr(" CompID=99 and Phone='" + Mphone.Value.Trim() + "'");
            if (da.Rows.Count > 0)
            {
                string uid = da.Rows[0]["UserID"].ToString();
                DataTable dw = db.GetUserBystr("UserID='" + uid + "'");
                if (dw.Rows.Count > 0)
                {
                    string wxid = dw.Rows[0]["WXNumber"].ToString();

                    DataTable dzj = db.GetDKWXJPinfo("CompID=99 and WXid='" + wxid + "' and YZM='欧洲杯'");

                    Button1.Enabled = true;

                    GridView1.DataSource = dzj;
                    GridView1.DataBind();
                }
            }
            else
            {
                Response.Write("<script>alert('你没有中奖！')</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('请输入正确的手机号码！')</script>");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Mphone.Value.Trim().Length == 11)
        {
            DataTable da = db.GetAddressBystr(" CompID=99 and Phone='" + Mphone.Value.Trim() + "'");
            if (da.Rows.Count > 0)
            {
                string uid = da.Rows[0]["UserID"].ToString();
                DataTable dw = db.GetUserBystr("UserID=" + uid + "");
                if (dw.Rows.Count > 0)
                {
                    string wxid = dw.Rows[0]["WXNumber"].ToString();

                    DataTable dzj =
                        db.GetDKWXJPinfo("CompID=99 and WXid='" + wxid + "' and YZM='欧洲杯' and LJflag is null ");
                    if (dzj.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dzj.Rows)
                        {
                            db.updateGetDKWXJPinfo("ID=" + dr["ID"] + "", GetCookieUID());
                        }
                        Response.Write("<script>alert('兑换成功！')</script>");
                        DataTable dcg = db.GetDKWXJPinfo("CompID=99 and WXid='" + wxid + "' and YZM='欧洲杯' and LJflag=1 ");
                        GridView1.DataSource = dcg;
                        GridView1.DataBind();
                    }

                    else
                    {
                        Response.Write("<script>alert('已经兑换过了！')</script>");
                        DataTable dcg = db.GetDKWXJPinfo("CompID=99 and WXid='" + wxid + "' and YZM='欧洲杯' and LJflag=1 ");
                        GridView1.DataSource = dcg;
                        GridView1.DataBind();
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('你没有中奖！')</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('请输入正确的手机号码！')</script>");
        }
    }

    public string ReturnLoginName(string Uid)
    {
        if (!String.IsNullOrEmpty(Uid))
        {
            mod = bll.GetList(int.Parse(Uid));
            return mod.LoginName;
        }
        else
        {
            return "";
        }
    }
}