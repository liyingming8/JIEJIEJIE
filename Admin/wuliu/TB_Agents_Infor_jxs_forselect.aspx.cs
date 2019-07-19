﻿using System;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using commonlib;
using Wuqi.Webdiyer;

public partial class Admin_wuliu_TB_Agents_Infor_jxs_forselect : AuthorPage
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
                if(!string.IsNullOrEmpty(Request.QueryString["pnm"]))
                {
                    inputSearchKeyword.Value = Request.QueryString["pnm"].ToString();
                }
                hf_fr.Value = Sc.DecryptQueryString(Request.QueryString["fr"]);
                _currentindex = 1;
                AspNetPager1.CurrentPageIndex = _currentindex;
                DisplayData(_currentindex, AspNetPager1.PageSize);              
            }
            else
            {
                Response.End();
            }
        }
    }


    private string _filtertemp;
    private void DisplayData(int pageIndex, int pageSize)
    {
        if (IsCompGrade())
        {
            _filtertemp = "ParentID=" + GetCookieCompID() + " and CompTypeID=48";
        }
        else
        {
            _filtertemp = "CompID in (SELECT authorcompid FROM TJ_DepartMent_CompAuthor where departid=" + GetCookieTJDepartID() + ")";
        } 
        if (inputSearchKeyword.Value.Length>0)
        {
            _filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
        } 
        if (_filtertemp.Length > 0)
        {
            string cnt =
                _tab.ExecuteQueryForSingleValue("select count(compid) from TJ_RegisterCompanys where " + _filtertemp);
            AspNetPager1.RecordCount = int.Parse(string.IsNullOrEmpty(cnt)?"0":cnt);
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_RegisterCompanys", _filtertemp, "compid", "compid", pageSize);
            GridView1.DataBind();
        }
    }

    private string AgentIDString = "";

    public string GetAgentIDStringByCompID(string compID)
    {
        if (GetCookieCompID() == "130" && GetCookieRID() == "28")
        {
            AgentIDString = "select AgentID from TianJianWuLiuWebnew.dbo.TB_CompAgentInfo where CompID=" + compID +
                            " and Remarks='" + GetCookieUID() + "'";
        }
        else
        {
            AgentIDString = "select AgentID from TianJianWuLiuWebnew.dbo.TB_CompAgentInfo where CompID=" + compID;
        }
        return AgentIDString;

    } 
 
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#CCFF83'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
            {
                e.Row.Attributes.Add("onclick", XiangGoBackLinkString(dataKey[0].ToString(), dataKey[1].ToString()));
            } 
        } 
    }

    public string XiangGoBackLinkString(string ID, string CompName)
    {
        if (ID.Length > 0)
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
            return string.Format("javascript:closemyWindowReloadNewhref('" + (tempurl.Contains("?")?(tempurl+"&"):(tempurl+"?")) + "agid={0}&agname={1}')", ID, CompName);
        }
        else
        {
            return "";
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TB_Agents_InforAddEdit.aspx?cmd=edit&ID={0}',780,500,'经销商信息编辑')", ID);
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
