using System; 
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.DBUtility;
using commonlib;
using Wuqi.Webdiyer;

public partial class Admin_TB_Agents_Infor_Terminal_select : AuthorPage
{ 
    public CommonFun commfun = new CommonFun();  
    public int ctid = 0;
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["fr"]))
            {
                hf_fr.Value = Sc.DecryptQueryString(Request.QueryString["fr"]);
                _currentindex = 1; 
                AspNetPager1.CurrentPageIndex = _currentindex;
                DisplayData(_currentindex, AspNetPager1.PageSize);
                Fillgridviewctid();
            }
            else
            {
                Response.End();
            } 
        }
    }

    private void Fillgridviewctid()
    {
        commfun.BindTreeCombox(ComboBox_CID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "选择城市...", true, "-", "1=1");
        ComboBox_CID.SelectedValue = "0";
    }

    private string _filterstring = "";
    private string Getfilterstring()
    {
        if (IsCompGrade())
        {
            _filterstring = "ParentID in (SELECT b.authorcompid FROM TJ_DepartMent_CompAuthor b where b.departid in (select id from  TJ_DepartMent  where compid = " + GetCookieCompID() + " and parentid = " + GetCookieTJDepartID()+"))";
        }
        else
        {
            _filterstring = "ParentID in (SELECT b.authorcompid FROM TJ_DepartMent_CompAuthor b where b.departid=" + GetCookieTJDepartID() + ")";
        } 
        if (!string.IsNullOrEmpty(DDLField.SelectedValue) && !DDLField.SelectedValue.Equals("0"))
        {
            if (inputSearchKeyword.Value.Length > 0)
            {
                _filterstring += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
            } 
        } 
        return _filterstring;
    }
    private string _filtertemp;
    private void DisplayData(int pageIndex, int pageSize)
    {
        _filtertemp = Getfilterstring();
        if (_filterstring.Length > 0)
        {
            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(compid) from TJ_RegisterCompanys where " + _filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_RegisterCompanys", _filtertemp, "compid", "compid", pageSize);
            GridView1.DataBind();
        }
    }

    private string _agentIDString = "";

    public string GetAgentIDStringAuthoredDepartID(string departid)
    {
        _agentIDString = "SELECT authorcompid FROM TJ_DepartMent_CompAuthor where departid="+departid+" and authoruserid<>0";
        return _agentIDString; 
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
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    e.Row.Attributes.Add("onclick", XiangGoBackLinkString(dataKey[0].ToString(), dataKey[1].ToString())); 
                }
            } 
        }
    }

    public string XiangGoBackLinkString(string id, string compName)
    {
        if (id.Length > 0)
        {
            string tempurl = hf_fr.Value;
            if (tempurl.Contains("&agid"))
            {
                tempurl = tempurl.Substring(0, tempurl.IndexOf("&agid", System.StringComparison.Ordinal));
            }
            else
            {
                if (tempurl.Contains("agid"))
                {
                    tempurl = tempurl.Substring(0, tempurl.IndexOf("agid", System.StringComparison.Ordinal));
                }
            }
            //双击后编辑修改方法
            return string.Format("javascript:closemyWindowReloadNewhref('" + (tempurl.Contains("?") ? (tempurl + "&") : (tempurl + "?")) + "agid={0}&agname={1}')", id, compName);
        }
        else
        {
            return "";
        }
    }

    BTJ_DepartMent_CompAuthor btjDepartMentCompAuthor = new BTJ_DepartMent_CompAuthor();
    private Boolean checkisauthored(string agentid)
    {
        return btjDepartMentCompAuthor.CheckIsExistByFilterString("authorcompid=" + agentid);
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TB_Agents_Infor_TerminalAddEdit.aspx?cmd=edit&ID={0}',780,580,'终端店信息')", ID);
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
}
