using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_TJ_BaseClass : AuthorPage
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
            DisplayData(_currentindex, AspNetPager1.PageSize);
        }
    } 
    private string _filtertemp="";
    private void DisplayData(int pageIndex, int pageSize)
    {
        _filtertemp = "";
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
        if (!string.IsNullOrEmpty(DDLField.SelectedValue))
        {
            _filtertemp += " and " + DDLField.Text + " like '%" + inputSearchKeyword.Value + "%'";
        } 
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_BaseClass where " + _filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_BaseClass", _filtertemp, "CID", "CID", pageSize);
        GridView1.DataBind();
    } 
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            //双击后编辑修改方法
            return string.Format("javascript:var win=openWinCenter('TJ_BaseClassAddEdit.aspx?cmd=edit&ID={0}',400,300,'基础类别维护')", ID);
        }
        else
        {
            return "";
        }
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
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
            var key = GridView1.DataKeys[e.Row.RowIndex];
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                if (Checkpermitdel(int.Parse(dataKey[0].ToString())))
                {
                    ((LinkButton)e.Row.Cells[3].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')");
                }
                else
                {
                    e.Row.Cells[3].Enabled = false;
                    ((LinkButton)e.Row.Cells[3].Controls[0]).Attributes.Add("color", "#888888");
                }
            }
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