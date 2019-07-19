using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.BLL;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_TJ_RegisterCompanys : AuthorPage
{
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys(); 
    public CommonFun comfun = new CommonFun();
    private readonly BTJ_User buser = new BTJ_User(); 
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HF_TempChildCompIDString.Value = comfun.ReturnChildCompIDString(GetCookieCompID(), false);
            DisplayData(_currentindex, AspNetPager1.PageSize, (HF_TempChildCompIDString.Value.Length > 0 ? HF_TempChildCompIDString.Value : "") + HF_TempAgentCompIDString.Value); 
        }
    }

    private string _filtertemp = "";
    private void DisplayData(int pageIndex, int pageSize,string tempcomidstring)
    {
        _filtertemp = string.Empty;
        if (IsSuperAdmin())
        {
            if (inputSearchKeyword.Value.Trim().Length > 0)
            {
                _filtertemp = DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'"; 
            }
            else
            {
                _filtertemp = "1=1";
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(tempcomidstring))
            {
                if (!string.IsNullOrEmpty(inputSearchKeyword.Value))
                {
                    _filtertemp = "CompID in (" + tempcomidstring + ") and "+DDLField.SelectedValue+" like '%"+inputSearchKeyword.Value+"%'";
                }
                else
                {
                    _filtertemp = "CompID in (" + tempcomidstring + ") ";
                } 
            }
        }
        if (!string.IsNullOrEmpty(_filtertemp))
        {
            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_RegisterCompanys where " + _filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_RegisterCompanys", _filtertemp, "CompTypeID", "CompID", pageSize); 
        }
        else
        {
            GridView1.DataSource = null;
        }
        GridView1.DataBind();
    } 
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            //双击后编辑修改方法
            return string.Format("javascript:var win=openWinCenter('TJ_RegisterCompanysAddEdit.aspx?cmd=edit&ID={0}',680,700,' 公司信息编辑')", ID);
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
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
            var key = GridView1.DataKeys[e.Row.RowIndex];
            var dataKey1 = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey1 != null && (!bll.CheckIsExistByFilterString("ParentID=" + dataKey1[0]) &&!buser.CheckIsExistByFilterString("CompID=" + dataKey1[0])))
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

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        DisplayData(_currentindex, AspNetPager1.PageSize,(HF_TempChildCompIDString.Value.Length > 0 ? HF_TempChildCompIDString.Value + "," : "") +HF_TempAgentCompIDString.Value); 
    } 
 
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        HF_TempChildCompIDString.Value = comfun.ReturnChildCompIDString(GetCookieCompID(), false);
        DisplayData(_currentindex, AspNetPager1.PageSize, (HF_TempChildCompIDString.Value.Length > 0 ? HF_TempChildCompIDString.Value : "") + HF_TempAgentCompIDString.Value); 
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (GridView1.DataKeys[e.RowIndex] != null)
        {
            _tab.ExecuteNonQuery("delete from TJ_RegisterCompanys where CompID=" + GridView1.DataKeys[e.RowIndex]["CompID"],null); 
            DisplayData(_currentindex, AspNetPager1.PageSize, (HF_TempChildCompIDString.Value.Length > 0 ? HF_TempChildCompIDString.Value : "") + HF_TempAgentCompIDString.Value);
        } 
    }
}