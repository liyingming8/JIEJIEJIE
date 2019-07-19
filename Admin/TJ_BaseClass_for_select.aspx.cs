using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_TJ_BaseClass_for_select : AuthorPage
{
    private readonly BTJ_BaseClass _bll = new BTJ_BaseClass();
    private MTJ_BaseClass _mod = new MTJ_BaseClass();
    private readonly CommonFun _comfun = new CommonFun();
    readonly TabExecute _tab = new TabExecute();
    private int _currentindex = 1; 

    protected void Page_Load(object sender, EventArgs e)
    { 
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["r"]))
            {
                hf_r.Value = Sc.DecryptQueryString(Request.QueryString["r"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["f"]))
            {
                hf_f.Value = Sc.DecryptQueryString(Request.QueryString["f"]);
            }
            DisplayData(_currentindex, AspNetPager1.PageSize);
        }
    } 
    private string _filtertemp="";
    private void DisplayData(int pageIndex, int pageSize)
    {
        _filtertemp = "ParentID=2";
        if (IsSuperAdmin())
        {
            if (_filtertemp.Equals(""))
            {
                _filtertemp = "(CompID=0 or CompID=" + GetCookieCompID()+")";
            }
            else
            {
                _filtertemp += " and (CompID=0 or CompID=" + GetCookieCompID()+")";
            }
        } 
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_BaseClass where " + _filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_BaseClass", _filtertemp, "CID", "CID", pageSize);
        GridView1.DataBind();
    }

    public string XiangXiLinkString(string cid, string cname)
    {
        if (ID.Length > 0)
        {
            string temp = "";
            if (hf_r.Value.Contains("pcompid"))
            {
                temp = hf_r.Value.Substring(0, hf_r.Value.IndexOf("pcompid", StringComparison.Ordinal) - 1);
            }
            else
            {
                temp = hf_r.Value;
            }
            if (temp.Contains("?"))
            {
                return string.Format("javascript:closemyWindowReloadNewhref('" + temp + "&cid={0}&cnm={1}')", Sc.EncryptQueryString(cid), Sc.EncryptQueryString(cname));
            }
            return string.Format("javascript:closemyWindowReloadNewhref('" + temp + "?cid={0}&cnm={1}')", Sc.EncryptQueryString(cid),Sc.EncryptQueryString(cname));
        }
        return "";
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        _bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["CID"].ToString()));
        DisplayData(1,20);
        //fillgridview(HF_ParentID.Value.Trim());
    }  
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {//表示对数据进行确认到哪行，添加一个方法事件
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
                e.Row.Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString(),dataKey[1].ToString()));
            var key = GridView1.DataKeys[e.Row.RowIndex]; 
        }
    }

    private bool Checkpermitdel(int cid)
    {
        return !_bll.CheckIsExistByFilterString("ParentID=" + cid);
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    { 
        DisplayData(_currentindex, AspNetPager1.PageSize);
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        DisplayData(_currentindex, AspNetPager1.PageSize);
    }

    public string GetCnameByCID(string cid)
    {
        return _comfun.ReturnBaseClassName(cid, true, false);
    }
   
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}