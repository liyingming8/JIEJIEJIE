using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;  
using TJ.BLL; 
using commonlib; 
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_TJ_SiteMap : AuthorPage
{
    private readonly BTJ_SiteMap bll = new BTJ_SiteMap();
    private readonly CommonFun comfun = new CommonFun();
    readonly TabExecute _tab = new TabExecute();
    private BTJ_RoleInfo btjrole = new BTJ_RoleInfo();
    private int _currentindex = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            _currentindex = 1;
            FillDDL();
            DisplayData(_currentindex, AspNetPager1.PageSize); 
        }
    }

    private void FillDDL()
    {
        comfun.BindTreeCombox(ddl_parentid, "PageName", "SiteID", "ParentID", "TJ_SiteMap",0,"顶级",true,"-","");
        ddl_parentid.SelectedValue = "0";
    }

    
    private string _filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        if (!string.IsNullOrEmpty(ddl_parentid.SelectedValue))
        {
            _filtertemp = "ParentID=" + ddl_parentid.SelectedValue;
        }
        if (inputSearchKeyword.Value.Length > 0)
        {
            _filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
        }
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_SiteMap where " + _filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_SiteMap", _filtertemp, !ddl_parentid.SelectedValue.Equals("0") ? "ShowOrder" : "ParentID,ShowOrder", "SiteID", pageSize);
        GridView1.DataBind();
    }

    private bool Checkpermitdel(string id)
    {
        return !btjrole.CheckIsExistByFilterString("(AuthorMenuInfo+',') like '%," + id + ",%' ");
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        var dataKey = GridView1.DataKeys[e.RowIndex];
        if (dataKey != null)
        {
            bll.Delete(int.Parse(dataKey["SiteID"].ToString()));
            DisplayData(_currentindex, AspNetPager1.PageSize);
        } 
    } 
    
    public string GetSystemType(string SystemID)
    {
        return comfun.ReturnBaseClassName(SystemID, false, false);
    }

   
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            //双击后编辑修改方法
            return string.Format("javascript:var win=openWinCenter('TJ_SiteMapAddEdit.aspx?cmd=edit&ID={0}',600,500,'系统目录维护')", ID);
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
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
            {
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
                ((HyperLink)e.Row.FindControl("linkshowrole")).Attributes.Add("onclick", XiangXiLinkString_PageName(dataKey[0].ToString(), dataKey[1].ToString()));
                if (IsSuperAdmin() && Checkpermitdel(dataKey["SiteID"].ToString()))
                {
                    ((LinkButton)e.Row.Cells[8].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
                }
                else
                {
                    e.Row.Cells[8].Enabled = false;
                    e.Row.Cells[8].ForeColor = Color.LightGray;
                }
            } 
        }
    }

    public string XiangXiLinkString_PageName(string id, string pageName)
    {
        if (id.Length > 0)
        {
            //双击后编辑修改方法
            return string.Format("javascript:var win=openWinCenter('TJ_SiteMapPageNameAddEdit.aspx?ID={0}',600,500,'" + "\"" + pageName + "\"" + " 使用角色信息')", id);
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
     
    public string GetSiteMapName(string SiteID)
    {
        if (SiteID.Equals("0"))
        {
            return "\\";
        }
        else
        {
            return bll.GetList(int.Parse(SiteID)).PageName;
        }
    }

    public DataTable ReturnMapTreeDataTable()
    {
        return comfun.ReturnTreeDataTable("PageName", "SiteID", "ParentID", "TJ_SiteMap", 0, "请选择", true, "—");
    } 
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}