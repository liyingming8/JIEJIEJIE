using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
using Wuqi.Webdiyer;

public partial class Admin_TJ_RoleInfo : AuthorPage
{
    private readonly BTJ_RoleInfo bll = new BTJ_RoleInfo();
    private MTJ_RoleInfo mod = new MTJ_RoleInfo();
    private readonly BTJ_User buser = new BTJ_User();
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string comp = GetCookieCompID();
            if (comp == "1")
            {
                _currentindex = 1;
                DisplayData(_currentindex, AspNetPager1.PageSize);
            }
            else
            {
                fillgridview();
            }  
      
        }
    }

    private void fillgridview()
    {
        GridView1.DataSource = bll.GetLists();
        GridView1.DataBind();
    }

    private string Filtertemp = "1=1"; 
    private void DisplayData(int pageIndex, int pageSize)
    {
        if (inputSearchKeyword.Value.Length > 0)
        {
            Filtertemp = DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
        }
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_RoleInfo where " + Filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_RoleInfo", Filtertemp, "RID", "RID", pageSize);
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["RID"].ToString()));
        fillgridview();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgridview();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgridview();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        var dataKey = GridView1.DataKeys[e.RowIndex];
        if (dataKey != null)
            mod = bll.GetList(int.Parse(dataKey["RID"].ToString()));
        mod.RoleName = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtRoleName")).Text.Trim();
        mod.Remark = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtRemark")).Text.Trim();
        bll.Modify(mod);
        GridView1.EditIndex = -1;
        fillgridview();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillgridview();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c"); 
            var key = GridView1.DataKeys[e.Row.RowIndex];
            if (key != null)
            {
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(key[0].ToString()));
                ((HyperLink)e.Row.FindControl("hlinkUsers")).Attributes.Add("onclick", XiangXiLinkStringRoleToUser(key[0].ToString(), key[1].ToString()));
                ((HyperLink)e.Row.FindControl("hlinkSystemAuthor")).Attributes.Add("onclick", LinkStringForWindowsShow("SystemAuthorManage.aspx?RID=" + key[0],"800","600","目录授权"));
                ((HyperLink)e.Row.FindControl("hlinkRoleAuthor")).Attributes.Add("onclick", LinkStringForWindowsShow("RoleAuthorManage.aspx?RID=" + key[0], "740", "500", "角色授权"));
                ((HyperLink)e.Row.FindControl("hlinkCompTypeAuthor")).Attributes.Add("onclick", LinkStringForWindowsShow("CompTypeIDAuthorManage.aspx?RID=" + key[0], "600", "400", "单位类型授权"));
                if (!buser.CheckIsExistByFilterString("RID=" + key[0]))
                {
                    ((LinkButton) e.Row.Cells[6].Controls[0]).Attributes.Add("onclick","javascript:return confirm('你确定要删除当前记录吗?')");
                }
                else
                {
                    e.Row.Cells[6].Enabled = false;
                    e.Row.Cells[6].ForeColor = Color.LightGray;
                }
            }
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_RoleInfoAddEdit.aspx?cmd=edit&ID={0}',480,360,'系统角色')", ID);
        }
        else
        {
            return "";
        }
    }

    public string XiangXiLinkStringRoleToUser(string RID, string RoleName)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_RoleToUserInfo.aspx?RID={0}',800,650,'" + "\"" + RoleName + "\"" + " 使用用户信息')", RID);
        }
        else
        {
            return "";
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        DisplayData(_currentindex, AspNetPager1.PageSize);
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        _currentindex = 1;
        DisplayData(_currentindex, AspNetPager1.PageSize);
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}