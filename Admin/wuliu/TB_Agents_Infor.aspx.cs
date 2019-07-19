using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.DBUtility;
using commonlib;
using Wuqi.Webdiyer;
using System.Web.UI.HtmlControls;
using TJ.Model;
using System.Web.UI;


public partial class Admin_TB_Agents_Infor : AuthorPage
{
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();
    public CommonFun commfun = new CommonFun();
    private readonly BTB_CompAgentInfo bcompagent = new BTB_CompAgentInfo();
    private readonly DBClass db = new DBClass();
    public int ctid = 0;
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();
    private MTJ_RegisterCompanys mod = new MTJ_RegisterCompanys();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _currentindex = 1;
            AspNetPager1.CurrentPageIndex = _currentindex;
            DisplayData(_currentindex, AspNetPager1.PageSize);
            Fillgridviewctid();
        }
    }

    private string _agentIDStringForQuery = "";

    private void Fillgridviewctid()
    {
        commfun.BindTreeCombox(ComboBox_CID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "选择城市...", true, "-", "1=1");
        ComboBox_CID.SelectedValue = "0";
    }

    private string _filterstring = "";
    private string Getfilterstring()
    {
        _agentIDStringForQuery = GetAgentIDStringByCompID(GetCookieCompID());
        if (_agentIDStringForQuery.Length > 0)
        {
            if (inputSearchKeyword.Value.Trim().Length > 0)
            {
                if (!string.IsNullOrEmpty(ComboBox_CID.SelectedValue) && ComboBox_CID.SelectedValue != "0")
                {
                    _filterstring = "CompID in (" + _agentIDStringForQuery + ") and CompTypeID in(" +
                                   DAConfig.CompTypeIDJingXiaoShang + "," + DAConfig.CompTypeIDZhongDuanDIan + ") and " + DDLField.SelectedValue +
                                   " like '%" + inputSearchKeyword.Value.Trim() + "%' and CTID=" + ComboBox_CID.SelectedValue;
                }
                else
                {
                    _filterstring = "CompID in (" + _agentIDStringForQuery + ") and CompTypeID in(" +
                                                   DAConfig.CompTypeIDJingXiaoShang + "," + DAConfig.CompTypeIDZhongDuanDIan + ") and " + DDLField.SelectedValue +
                                                   " like '%" + inputSearchKeyword.Value.Trim() + "%'";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(ComboBox_CID.SelectedValue) && ComboBox_CID.SelectedValue != "0")
                {
                    _filterstring = "CompID in (" + _agentIDStringForQuery + ") and CompTypeID in(" +
                                                   DAConfig.CompTypeIDJingXiaoShang + "," + DAConfig.CompTypeIDZhongDuanDIan + ") and CTID=" + ComboBox_CID.SelectedValue;
                }
                else
                {
                    _filterstring = "CompID in (" + _agentIDStringForQuery + ") and CompTypeID in(" + DAConfig.CompTypeIDJingXiaoShang + "," + DAConfig.CompTypeIDZhongDuanDIan + ")";

                }
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

    public string GetAgentIDStringByCompID(string compID)
    {
        if (GetCookieCompID() == "130" && GetCookieRID() == "28")
        {
            _agentIDString = "select AgentID from TianJianWuLiuWebnew.dbo.TB_CompAgentInfo where CompID=" + compID +
                            " and Remarks='" + GetCookieUID() + "'";
        }
        else
        {
            _agentIDString = "select AgentID from TianJianWuLiuWebnew.dbo.TB_CompAgentInfo where CompID=" + compID;
        }
        return _agentIDString;

    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        var dataKey = GridView1.DataKeys[e.RowIndex];
        if (dataKey != null)
        {
            bll.Delete(int.Parse(dataKey["CompID"].ToString()));
            db.DeleteAgent(dataKey["CompID"].ToString());
            db.DeleteFhTable(dataKey["CompID"].ToString());
        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
    }

    private Boolean isauthored = false;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#CCFF83'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        //e.Row.Cells[6].Enabled = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
                    ((HtmlImage)e.Row.FindControl("editimg")).Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString()));
                    ((HyperLink)e.Row.FindControl("hlinkdepartment")).Attributes.Add("onclick", XiangXiLinkForDepartAuthorString(dataKey[0].ToString(), dataKey[1].ToString()));
                    string allowa = dataKey["AllowAreaInfo"].ToString();
                    if (string.IsNullOrEmpty(allowa))
                    {
                        ((HyperLink)e.Row.FindControl("LabelAllowAreaInfo")).Text = "授权";
                    }
                    else
                    {
                        if (allowa.Length > 10)
                        {
                            ((HyperLink)e.Row.FindControl("LabelAllowAreaInfo")).Text = allowa.Substring(0,9)+"...";
                        }
                    }
                    string AllowProduct= dataKey["AllowProduct"].ToString();
                    if (string.IsNullOrEmpty(AllowProduct))
                    {
                        ((HyperLink)e.Row.FindControl("LabelAllowProducts")).Text = "授权";
                    }else
                    {
                        if (AllowProduct.Length > 20)
                        {
                            ((HyperLink)e.Row.FindControl("LabelAllowProducts")).Text = AllowProduct.Substring(0, 19) + "...";
                        }
                    }
                    ((HyperLink)e.Row.FindControl("LabelAllowAreaInfo")).Attributes.Add("onclick", XiangXiLinkForAreaString(dataKey[0].ToString()));
                    ((HyperLink)e.Row.FindControl("LabelAllowProducts")).Attributes.Add("onclick", XiangXiLinkForProductString(dataKey[0].ToString()));
                    isauthored = checkisauthored(dataKey[0].ToString()); 

                    /*
                    if (!bcompagent.CheckIsExistByFilterString("CompID=" + dataKey[0]))
                    {
                        if (GetCookieCompID() == "130")
                        {
                            if (GetCookieRID() == "28" || GetCookieRID() == "15")
                            {
                                ((LinkButton)e.Row.Cells[8].Controls[0]).Attributes.Add("onclick",
                                    "javascript:return confirm('你确定要删除当前记录吗?')");
                            }
                            else
                            {
                                e.Row.Cells[8].Enabled = false;
                                e.Row.Cells[8].ForeColor = Color.LightGray;
                            }
                        }
                        else if ((IsSuperAdmin()))
                        {
                            ((LinkButton)e.Row.Cells[8].Controls[0]).Attributes.Add("onclick",
                                "javascript:return confirm('你确定要删除当前记录吗?')");
                        }
                        else
                        {
                            e.Row.Cells[8].Enabled = false;
                            e.Row.Cells[8].ForeColor = Color.LightGray;
                        }
                    }
                    else
                    {
                        e.Row.Cells[8].Enabled = false;
                        e.Row.Cells[8].ForeColor = Color.LightGray;
                    }
                    */
                }
            }
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
            return string.Format("javascript:var win=openWinCenter('/admin/wuliu/TB_Agents_InforAddEdit.aspx?cmd=edit&ID={0}',780,580,'经销商信息编辑')", ID);
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

    public string XiangXiLinkForAreaString(string agentid)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('AreaAuthor.aspx?agentid={0}&flag=1',780,560,'区域授权')", agentid);
        }
        else
        {
            return "";
        }
    }

    public string XiangXiLinkForProductString(string agentid)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TB_Products_Infor_select.aspx?agentid={0}&flag=1',780,560,'产品授权')", agentid);
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
