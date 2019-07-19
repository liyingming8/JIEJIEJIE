using System;
using System.Data; 
using System.Web.UI.WebControls; 
using TJ.DBUtility;
using commonlib;
using Wuqi.Webdiyer;

public partial class Admin_TB_Agents_Infor_Terminal_ShenHe : AuthorPage
{ 
    public CommonFun Commfun = new CommonFun();  
    public int Ctid = 0;
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _currentindex = 1;   
            AspNetPager1.CurrentPageIndex = _currentindex;
            DisplayData(_currentindex, AspNetPager1.PageSize);
            //Fillgridviewctid();
        }
    }

    //private void Fillgridviewctid()
    //{
    //    Commfun.BindTreeCombox(ComboBox_CID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "选择城市...", true, "-", "1=1");
    //    ComboBox_CID.SelectedValue = "0";
    //} 
    private string _filtertemp;
    private void DisplayData(int pageIndex, int pageSize)
    {
        _filtertemp = "MasterID="+GetCookieCompID()+" and ParentID=0 and CompTypeID=486";
        string temp = string.Empty;
        if (!IsCompGrade())
        {
            temp = Permitcityidstring(GetCookieTJDepartID());
            if (temp.Length > 0)
            {
                _filtertemp += " and CTID in (" + temp + ")";
            } 
        } 
        //if (!string.IsNullOrEmpty(ComboBox_CID.SelectedValue) && !ComboBox_CID.SelectedValue.Equals("0"))
        //{
        //    if (_filtertemp.Length > 0)
        //    {
        //        _filtertemp += " and CTID=" + ComboBox_CID.SelectedValue;
        //    }
        //    else
        //    {
        //        _filtertemp = " CTID=" + ComboBox_CID.SelectedValue;
        //    }
        //} 
        string sqlstring ="select count(CompID) as cnt from TJ_RegisterCompanys  where "+_filtertemp;
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery(sqlstring, null).Rows[0][0].ToString()); 
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_RegisterCompanys", _filtertemp, "compid desc", "compid", pageSize);
        GridView1.DataBind();
    } 

    private string Permitcityidstring(string departid)
    {
        string sqlstirng = "SELECT CID FROM TJ_BaseClass where ParentID in (select provinceid from TJ_DepartMent_ProvinceAuthor where departid=" + departid + ")";
        DataTable dttemp =  _tab.ExecuteQuery(sqlstirng, null);
        string returntemp = "";
        if(dttemp!=null&&dttemp.Rows.Count>0)
        {
            foreach(DataRow dr in dttemp.Rows)
            {
                if(returntemp.Length.Equals(0))
                {
                    returntemp = dr[0].ToString();
                }
                else
                {
                    returntemp += "," + dr[0];
                }
            }
        } 
        if (dttemp != null) dttemp.Dispose();
        return returntemp;
    } 
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#CCFF83'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
                    ((LinkButton)e.Row.Cells[6].Controls[0]).Attributes.Add("onclick","javascript:return confirm('你确定要删除当前记录吗?')");
                }
            } 
        }
    } 
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TB_Agents_Infor_TerminalAddEdit_ShenHe.aspx?ID={0}',780,560,'终端店信息审核')", ID);
        }
        else
        {
            return "";
        }
    }

    public string XiangXiLinkStringForCreatUser(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('../TJ_User_Terminal.aspx?compid={0}',660,600,'用户信息')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    public string XiangXiLinkForDepartAuthorString(string agentid, string agentnm)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('../TJ_DepartMentForAuthor.aspx?agentid={0}&agentnm={1}',780,560,'职能部门授权')", agentid, agentnm);
        }
        else
        {
            return "";
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataKey dkey = GridView1.DataKeys[e.RowIndex];
        _tab.ExecuteNonQuery("delete  from TJ_RegisterCompanys where CompID=" + dkey["CompID"].ToString(), null);
        _tab.ExecuteQuery("delete  from TJ_User where CompID=" + dkey["CompID"], null);
        DisplayData(_currentindex<1?1:_currentindex,AspNetPager1.PageSize);
    }
}
