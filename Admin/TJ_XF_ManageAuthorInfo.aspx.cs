using System;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using TJ.Model;
using commonlib;
public partial class Admin_TJ_XF_ManageAuthorInfo : AuthorPage
{
    BTJ_XF_ManageAuthorInfo bll = new BTJ_XF_ManageAuthorInfo(); 
    TabExecute tab = new TabExecute();
    BTJ_RoleInfo brole = new BTJ_RoleInfo();
    private int _currentindex = 0; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["UID"]))
            {
                uid.Value = Sc.DecryptQueryString(Request.QueryString["UID"]);
                if (!string.IsNullOrEmpty(Request.QueryString["suid"])&&!string.IsNullOrEmpty(Request.QueryString["sunm"])&&!string.IsNullOrEmpty(Request.QueryString["srid"]))
                {
                    bll.Insert(new MTJ_XF_ManageAuthorInfo(0,
                        int.Parse(Sc.DecryptQueryString(Request.QueryString["suid"])),
                        brole.GetList(int.Parse(Sc.DecryptQueryString(Request.QueryString["srid"]))).RoleName,
                        int.Parse(uid.Value), int.Parse(GetCookieUID()), DateTime.Now, Sc.DecryptQueryString(Request.QueryString["sunm"])));
                } 
                _currentindex = 1;
                AspNetPager1.CurrentPageIndex = 1;
                DisplayData(1, AspNetPager1.PageSize);
                add.Attributes.Add("onclick",
                    "openWinCenter('TJ_Depart_UserSelectByRID.aspx?fr=" + Sc.EncryptQueryString(Request.Url.AbsolutePath) +
                    "&RID=" + Sc.EncryptQueryString("168") + "&UID=" + Sc.EncryptQueryString(uid.Value) +
                    "', 600,450,'指定城市经理')");
            }
            else
            {
                Response.End();
            }
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_XF_ManageAuthorInfoAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}',600,450,'TJ_XF_ManageAuthorInfo')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         var dataKey = GridView1.DataKeys[e.RowIndex];
         if (dataKey != null)
         bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["id"].ToString()));
         DisplayData(_currentindex, AspNetPager1.PageSize);
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
            e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex-1) + e.Row.RowIndex + 1).ToString();
            ((LinkButton)e.Row.Cells[5].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除当前记录吗?')");
        }
    }
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
       _currentindex = 1;
       AspNetPager1.CurrentPageIndex = 1;
       DisplayData(1, AspNetPager1.PageSize);
    }

    private string _filtertemp ="1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        _filtertemp = "fathoruserid="+uid.Value;
        if (inputSearchKeyword.Value.Trim().Length > 0)
       {
         _filtertemp += " and "+ DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
       } 
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from TJ_XF_ManageAuthorInfo where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_XF_ManageAuthorInfo", _filtertemp, "id", "id", pageSize);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }
}
