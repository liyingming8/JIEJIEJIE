using System;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using TJ.Model;
using commonlib;
public partial class Admin_TJ_DepartMent_ProvinceAuthor : AuthorPage
{
    BTJ_DepartMent_ProvinceAuthor bll = new BTJ_DepartMent_ProvinceAuthor();
    MTJ_DepartMent_ProvinceAuthor mod = new MTJ_DepartMent_ProvinceAuthor();
    TabExecute tab = new TabExecute();
    private int _currentindex = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["departid"]))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["dnm"]))
                {
                    input_dnm.Value = Sc.DecryptQueryString(Request.QueryString["dnm"]);
                }
                input_departid.Value = Sc.DecryptQueryString(Request.QueryString["departid"].ToString());
                if (!string.IsNullOrEmpty(Request.QueryString["cid"]) && !string.IsNullOrEmpty(Request.QueryString["cnm"]))
                {
                    string cid = Sc.DecryptQueryString(Request.QueryString["cid"]);
                    string cnm = Sc.DecryptQueryString(Request.QueryString["cnm"]);
                    if (!bll.CheckIsExistByFilterString("departid=" + input_departid.Value + " and provinceid=" + cid))
                    {
                        bll.Insert(new MTJ_DepartMent_ProvinceAuthor(0, int.Parse(input_departid.Value), int.Parse(cid), DateTime.Now, int.Parse(GetCookieUID()), cnm, ""));
                    }
                }
                _currentindex = 1;
                AspNetPager1.CurrentPageIndex = 1;
                DisplayData(1, AspNetPager1.PageSize);
                add.Attributes.Add("onclick", "openWinCenter('TJ_BaseClass_for_select.aspx?r=" + Sc.EncryptQueryString("TJ_DepartMent_ProvinceAuthor.aspx?departid=" + Sc.EncryptQueryString(input_departid.Value)+ "&dnm="+Sc.EncryptQueryString(input_dnm.Value)) + "', 560,420,'选择省份')");
            }
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_DepartMent_ProvinceAuthorAddEdit.aspx?cmd=" + Sc.EncryptQueryString("edit") + "&ID={0}',600,450,'TJ_DepartMent_ProvinceAuthor')", Sc.EncryptQueryString(ID));
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
            {
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
            }
            ((Label)e.Row.FindControl("Labeldepartid")).Text = input_dnm.Value;
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex - 1) + e.Row.RowIndex + 1).ToString();
            if (IsSuperAdmin())
            {
                ((LinkButton)e.Row.Cells[4].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除当前记录吗?')");
            }
            else
            {
                e.Row.Cells[4].Enabled = false;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.LightGray;
            }
        }
    }
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        DisplayData(1, AspNetPager1.PageSize);
    }

    private string _filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        _filtertemp = "departid=" + input_departid.Value;
        AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from TJ_DepartMent_ProvinceAuthor where " + _filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_DepartMent_ProvinceAuthor", _filtertemp, "id", "id", pageSize);
        GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}
