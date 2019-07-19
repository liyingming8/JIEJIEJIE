using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
using Wuqi.Webdiyer;

public partial class Admin_TJ_Depart_UserInfo : AuthorPage
{
    private readonly BTJ_User bll = new BTJ_User(); 
    private readonly BTJ_RoleInfo bllrole = new BTJ_RoleInfo();
    private BTJ_DepartMent btjDepart = new BTJ_DepartMent();
    private readonly CommonFun comfun = new CommonFun();
    private readonly CommonFunWL comwl = new CommonFunWL();
    readonly TabExecute _tab = new TabExecute();
    private int _currentindex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            DisplayData(1, AspNetPager1.PageSize);
        }
    }

    private string returnvalue = "";

    public string ReturnUserStatu(object IsActive)
    {
        switch (IsActive.ToString())
        {
            case "0":
                returnvalue = "未激活";
                break;
            case "1":
                returnvalue = "已激活";
                break;
            case "2":
                returnvalue = "试用中";
                break;
            case "3":
                returnvalue = "已冻结";
                break;
            default:
                returnvalue = "";
                break;
        }
        return returnvalue;
    }

    private void Fillgridview()
    {
        if (IsSuperAdmin())
        {
            string sqlstring = "CompID<>0";
            if (inputSearchKeyword.Value.Length > 0)
            {
                sqlstring += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
            }
            GridView1.DataSource = bll.GetListsByFilterString(sqlstring + "and RID is not null", "CompID ");
        }
        else
        {
            string tempagentid = comwl.GetAgentIDStringByCompID(GetCookieCompID());
            string tempchildcompid = comfun.ReturnChildCompIDString(GetCookieCompID(), true);
            string sqlstring = "CompID<>0 and CompID in (" + tempchildcompid +
                               (tempagentid.Length > 0 ? "," + tempagentid : "") + ")";
            if (inputSearchKeyword.Value.Length > 0)
            {
                sqlstring += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
            }
            GridView1.DataSource = bll.GetListsByFilterString(sqlstring + "and RID is not null", "CompID ");
        }
        GridView1.DataBind();
    }

    private string _filtertemp = "";

    private void DisplayData(int pageIndex, int pageSize)
    {
        if (IsSuperAdmin())
        {
            _filtertemp = "departid=" + GetCookieTJDepartID() + " and RID is not null and CompID<>0";
            if (inputSearchKeyword.Value.Length > 0)
            {
                _filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
            }
        }
        else
        { 
            _filtertemp = "CompID=" + GetCookieCompID() +" and departid=" + GetCookieTJDepartID() + " and RID is not null";
            if (inputSearchKeyword.Value.Length > 0)
            {
                _filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
            }
        }
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(UserID) from TJ_User where " + _filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_User", _filtertemp, "UserID", "UserID",pageSize);
        GridView1.DataBind();
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            //双击后编辑修改方法
            return string.Format("javascript:var win=openWinCenter('TJ_UserAddEdit.aspx?cmd=edit&ID={0}',680,580,' 用户编辑')", ID);
        }
        else
        {
            return "";
        }
    }

    public string XiangXiLinkStringForManagerAuthor(string ID)
    {
        if (ID.Length > 0)
        {
            //双击后编辑修改方法
            return string.Format("javascript:var win=openWinCenter('TJ_XF_ManageAuthorInfo.aspx?UID={0}',680,580,' 授权')", ID);
        }
        else
        {
            return "";
        }
    }

    public string XiangXiLinkStringForYeWuDaiBiaoSelect(string ID)
    {
        if (ID.Length > 0)
        {
            //双击后编辑修改方法
            return string.Format("javascript:var win=openWinCenter('TJ_XF_CityManageAndYewu.aspx?UID={0}',680,580,' 指定')", ID);
        }
        else
        {
            return "";
        }
    }

    private string temp = "";
    public string ReturnRoleNameByRid(string RID)
    { 
        temp = "";
        if (!string.IsNullOrEmpty(RID))
        {
            DataTable dttemp = _tab.ExecuteQuery("select RoleName from TJ_RoleInfo where RID=" + RID, null);
            if (dttemp.Rows.Count > 0)
            {
                temp = dttemp.Rows[0][0].ToString();
            }
            dttemp.Dispose();
        } 
        return temp; 
    }
   

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        var dataKey = GridView1.DataKeys[e.RowIndex];
        if (dataKey != null)
            bll.Delete(int.Parse(dataKey["UserID"].ToString()));
        Fillgridview();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        Fillgridview();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Fillgridview();
    }

    public string ReturnPlaceByID(string CID)
    {
        if (string.IsNullOrEmpty(CID))
        {
            return "";
        }
        return comfun.ReturnBaseClassName(CID, true, false);
        
    } 
    public IList<MTJ_RoleInfo> GetRoleInfo()
    {
        return
            bllrole.GetListsByFilterString("RID in(" + bllrole.GetList(int.Parse(GetCookieRID())).AuthorRoleIDInfo + ")");
    }

    private int tempnum = 0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
            {
                if (!dataKey["RID"].ToString().Equals("168"))
                {
                    ((HyperLink)e.Row.FindControl("linkmanagerauthor")).Attributes.Add("onclick", XiangXiLinkStringForManagerAuthor(Sc.EncryptQueryString(dataKey[0].ToString())));
                    ((HyperLink) e.Row.FindControl("linkmanagerauthor")).ForeColor = Color.Red; 
                    tempnum = AuthoredNum(dataKey["UserID"].ToString());
                    if (tempnum.Equals(0))
                    {
                        ((Literal)e.Row.FindControl("literauthornum")).Text = "【全部】";
                    }
                    else
                    {
                        ((Literal)e.Row.FindControl("literauthornum")).Text = "【" + tempnum + "】";
                    }
                    //((HyperLink)e.Row.FindControl("linkyewudaibiaoauthor")).Visible = false;
                    //((HyperLink)e.Row.FindControl("linkyewudaibiaoauthor")).Visible = false;
                }
                else
                {
                    //((HyperLink)e.Row.FindControl("linkyewudaibiaoauthor")).Attributes.Add("onclick", XiangXiLinkStringForYeWuDaiBiaoSelect(Sc.EncryptQueryString(dataKey[0].ToString())));
                    //((HyperLink)e.Row.FindControl("linkyewudaibiaoauthor")).ForeColor = Color.Red;
                    e.Row.FindControl("linkmanagerauthor").Visible = false;
                    e.Row.FindControl("literauthornum").Visible = false;

                    //tempnum = OrderedYewuDaibiaoNum(dataKey["UserID"].ToString());
                    //if (tempnum.Equals(0))
                    //{
                    //    ((Literal)e.Row.FindControl("literorderornum")).Text = "【无】";
                    //}
                    //else
                    //{
                    //    ((Literal)e.Row.FindControl("literorderornum")).Text = "【" + tempnum + "】";
                    //}
                }
                if (dataKey["CompID"].ToString().Equals("0"))
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = Color.Gray;
                }
                else
                {
                    Label labactived = (Label)e.Row.FindControl("LabelIsActived");
                    switch (labactived.Text.Trim())
                    {
                        case "未激活":
                            labactived.ForeColor = Color.Red;
                            break;
                        case "已激活":
                            labactived.ForeColor = Color.Green;
                            break;
                        case "试用中":
                            labactived.ForeColor = Color.Orange;
                            break;
                        case "已冻结":
                            labactived.ForeColor = Color.Gray;
                            break;
                    }
                }
            }   
        }
    }

    TabExecute tabexe = new TabExecute();
    private int AuthoredNum(string userid)
    {
         return int.Parse(tabexe.ExecuteQueryForSingleValue("select count(id) from TJ_XF_ManageAuthorInfo where fathoruserid=" + userid));
    }

    private int OrderedYewuDaibiaoNum(string userid)
    {
        MTJ_XF_CityManageAndYewu m = new MTJ_XF_CityManageAndYewu(); 
        return int.Parse(tabexe.ExecuteQueryForSingleValue("select count(id) from TJ_XF_CityManageAndYewu where citymanageid=" + userid));
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        DisplayData(_currentindex, AspNetPager1.PageSize);
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        Fillgridview();
    }

    public string GetCompanyNameByID(string CID)
    {
        if (CID == "0")
        {
            return "普通消费者";
        }
        if (string.IsNullOrEmpty(CID))
        {
            return "---";
        }
        return btjDepart.GetList(int.Parse(CID)).department;
    }

    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}