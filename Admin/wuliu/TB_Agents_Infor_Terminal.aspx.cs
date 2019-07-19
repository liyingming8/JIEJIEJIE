using System;
using System.Data;
using System.Web.UI.WebControls;
using org.in2bits.MyXls;
using TJ.DBUtility;
using commonlib;
using TJ.BLL;
using Wuqi.Webdiyer;

public partial class Admin_TB_Agents_Infor_Terminal : AuthorPage
{ 
    public CommonFun Commfun = new CommonFun();  
    commfrank comfrank = new commfrank();
    public int Ctid = 0;
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _currentindex = 1;
            FillDDL();
            if (GetCookieRID() != "168")
            {
                if (!IsCompGrade())
                {
                    ddl_jingxiaoshang.DataSource = _tab.ExecuteQuery("select a.CompID,a.CompName from TJ_RegisterCompanys a where a.CompID in (SELECT authorcompid FROM TJ_DepartMent_CompAuthor where departid=" + GetCookieTJDepartID() + ") order by CompName", null);
                    ddl_jingxiaoshang.DataBind();
                } 
            } 
            AspNetPager1.CurrentPageIndex = _currentindex;  
            //Fillgridviewctid();
            DisplayData(_currentindex, AspNetPager1.PageSize);
        }
    }

    private void FillDDL()
    {
        BTJ_DepartMent btjDepart = new BTJ_DepartMent();
        if (IsCompGrade())
        {
            ddl_departid.Visible = true;
            ddl_departid.DataSource = btjDepart.GetListsByFilterString("compid=" + GetCookieCompID() + " and parentid="+GetCookieTJDepartID());
            ddl_departid.DataBind();
            ddl_departid.SelectedIndex = 0;
            ddl_jingxiaoshang.Items.Clear();
            ddl_jingxiaoshang.Items.Add(new ListItem("全部经销商", "0"));
            ddl_jingxiaoshang.DataSource = _tab.ExecuteQuery("select a.CompID,a.CompName from TJ_RegisterCompanys a where a.CompID in (SELECT authorcompid FROM TJ_DepartMent_CompAuthor where departid=" + ddl_departid.SelectedValue + ") order by CompName", null);
            ddl_jingxiaoshang.DataBind();
            ddl_jingxiaoshang.SelectedValue = "0";
        }
        else
        {
            ddl_departid.Visible = false;
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
        if (GetCookieRID() == "168")
        {
            _filtertemp = "ManagerUserID=" + GetCookieUID();
        }
        else
        {
            if (IsCompGrade())
            {
                if (string.IsNullOrEmpty(ddl_jingxiaoshang.SelectedValue) || ddl_jingxiaoshang.SelectedValue.Equals("0"))
                {
                    _filtertemp = "ParentID in (SELECT a.authorcompid FROM TJ_DepartMent_CompAuthor a where a.departid=" + ddl_departid.SelectedValue + ")";
                }
                else
                {
                    _filtertemp = "ParentID=" + ddl_jingxiaoshang.SelectedValue;
                } 
                if (_filtertemp.Length > 0)
                {
                    _filtertemp += " and CompTypeID=486";
                }
                else
                {
                    _filtertemp = "CompTypeID=486";
                }
                string temp = comfrank.GetPermitManagerIDString(GetCookieUID());
                if (!string.IsNullOrEmpty(temp))
                {
                    _filtertemp += " and ManagerUserID in (" + temp + ")";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(ddl_jingxiaoshang.SelectedValue) || ddl_jingxiaoshang.SelectedValue.Equals("0"))
                {
                    _filtertemp = "ParentID in (SELECT a.authorcompid FROM TJ_DepartMent_CompAuthor a where a.departid=" + GetCookieTJDepartID() + ")";
                }
                else
                {
                    _filtertemp = "ParentID=" + ddl_jingxiaoshang.SelectedValue;
                } 
                if (_filtertemp.Length > 0)
                {
                    _filtertemp += " and CompTypeID=486";
                }
                else
                {
                    _filtertemp = "CompTypeID=486";
                }
                string temp = comfrank.GetPermitManagerIDString(GetCookieUID());
                if (!string.IsNullOrEmpty(temp))
                {
                    _filtertemp += " and ManagerUserID in (" + temp + ")";
                }
            } 
        }

        if (inputSearchKeyword.Value.Length > 0)
        {
            if (_filtertemp.Length > 0)
            {
                _filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
            }
            else
            {
                _filtertemp += DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
            }
        }
        string sqlstring ="select count(CompID) as cnt from TJ_RegisterCompanys  where "+_filtertemp;
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery(sqlstring, null).Rows[0][0].ToString()); 
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_RegisterCompanys", _filtertemp,"CompName", "compid", pageSize);
        GridView1.DataBind();
    }  

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#CCFF83'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((Label) e.Row.FindControl("lab_index")).Text =
                ((_currentindex - 1) * AspNetPager1.PageSize + e.Row.RowIndex+1).ToString();
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
                    ((HyperLink)e.Row.FindControl("hyplinkcreateuser")).Attributes.Add("onclick", XiangXiLinkStringForCreatUser(dataKey[0].ToString()));
                }
            } 
        }
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

    public void CreateExcel(DataTable dt)
    {
        XlsDocument xls = new XlsDocument();
        xls.FileName = "终端店信息-" + DateTime.Now.ToString("yyyyMMdd");
        Worksheet sheet = xls.Workbook.Worksheets.Add("终端店");//状态栏标题名称  
        ColumnInfo cinfo = new ColumnInfo(xls, sheet);
        cinfo.Collapsed = true;
        //设置列的范围 如 0列-10列 
        cinfo.ColumnIndexStart = 0;//列开始

        cinfo.ColumnIndexEnd = 9;//列结束

        cinfo.Collapsed = true;
        cinfo.Width = 80 * 60;
        sheet.AddColumnInfo(cinfo);
        XF cellXF = xls.NewXF();
        cellXF.VerticalAlignment = VerticalAlignments.Centered;
        cellXF.HorizontalAlignment = HorizontalAlignments.Centered;
        cellXF.Font.Height = 18 * 12;
        Cells cells = sheet.Cells;
        Cell cell1 = cells.Add(1, 1, "所属战区", cellXF);
        Cell cell2 = cells.Add(1, 2, "父级经销商", cellXF);
        Cell cell3 = cells.Add(1, 3, "终端店", cellXF);
        Cell cell4 = cells.Add(1, 4, "编码", cellXF);
        Cell cell5 = cells.Add(1, 5, "联系人", cellXF);
        Cell cell6 = cells.Add(1, 6, "电话", cellXF);
        Cell cell7 = cells.Add(1, 7, "地址", cellXF);
        Cell cell8 = cells.Add(1, 8, "城市经理", cellXF);
        int count = dt.Rows.Count;//统计数量 
        if (count > 62000)
        {
            count = 62000;
        }
        for (int i = 0; i < count; i++)
        {
            int rowIndex = i + 2;
            cells.Add(rowIndex, 1, dt.Rows[i]["zq"].ToString(), cellXF);
            cells.Add(rowIndex, 2, dt.Rows[i]["fjjxs"].ToString(), cellXF);
            cells.Add(rowIndex, 3, dt.Rows[i]["dm"].ToString(), cellXF);
            cells.Add(rowIndex, 4, dt.Rows[i]["cd"].ToString(), cellXF);
            cells.Add(rowIndex, 5, dt.Rows[i]["lxr"].ToString(), cellXF);
            cells.Add(rowIndex, 6, dt.Rows[i]["ph"].ToString(), cellXF);
            cells.Add(rowIndex, 7, dt.Rows[i]["adds"].ToString(), cellXF);
            cells.Add(rowIndex, 8, dt.Rows[i]["mn"].ToString(), cellXF);
        }
        xls.Send();
    }
    protected void btn_createexcel_Click(object sender, EventArgs e)
    {
        if (GetCookieRID() == "168")
        {
            _filtertemp = "ManagerUserID=" + GetCookieUID();
        }
        else
        {
            if (IsCompGrade())
            {
                if (string.IsNullOrEmpty(ddl_jingxiaoshang.SelectedValue) || ddl_jingxiaoshang.SelectedValue.Equals("0"))
                {
                    _filtertemp = "ParentID in (SELECT a.authorcompid FROM TJ_DepartMent_CompAuthor a where a.departid=" + ddl_departid.SelectedValue + ")";
                }
                else
                {
                    _filtertemp = "ParentID=" + ddl_jingxiaoshang.SelectedValue;
                }
                if (_filtertemp.Length > 0)
                {
                    _filtertemp += " and CompTypeID=486";
                }
                else
                {
                    _filtertemp = "CompTypeID=486";
                }
                string temp = comfrank.GetPermitManagerIDString(GetCookieUID());
                if (!string.IsNullOrEmpty(temp))
                {
                    _filtertemp += " and ManagerUserID in (" + temp + ")";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(ddl_jingxiaoshang.SelectedValue) || ddl_jingxiaoshang.SelectedValue.Equals("0"))
                {
                    _filtertemp = "ParentID in (SELECT a.authorcompid FROM TJ_DepartMent_CompAuthor a where a.departid=" + GetCookieTJDepartID() + ")";
                }
                else
                {
                    _filtertemp = "ParentID=" + ddl_jingxiaoshang.SelectedValue;
                }
                if (_filtertemp.Length > 0)
                {
                    _filtertemp += " and CompTypeID=486";
                }
                else
                {
                    _filtertemp = "CompTypeID=486";
                }
                string temp = comfrank.GetPermitManagerIDString(GetCookieUID());
                if (!string.IsNullOrEmpty(temp))
                {
                    _filtertemp += " and ManagerUserID in (" + temp + ")";
                }
            }
        }

        if (inputSearchKeyword.Value.Length > 0)
        {
            if (_filtertemp.Length > 0)
            {
                _filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
            }
            else
            {
                _filtertemp += DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
            }
        }
        string sqlstring = "select (select top 1 department from TJ_DepartMent_CompAuthor r left join TJ_DepartMent d on r.departid=d.id where authorcompid=b.ParentID) zq ,(select CompName from TJ_RegisterCompanys where CompID=b.ParentID ) fjjxs,CompName dm, Agent_Code cd,LegalPerson lxr,MobilePhoneNumber ph,Address adds,ManagerName mn from TJ_RegisterCompanys b  where " + _filtertemp;
        DataTable dt =  _tab.ExecuteQuery(sqlstring, null);
        if (dt.Rows.Count>0) {
            CreateExcel(dt);
        }
        else
        {
            MessageBox.Show(this, "没有数据！");
        }
        dt.Dispose();
    }

    protected void ddl_departid_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_jingxiaoshang.Items.Clear();
        ddl_jingxiaoshang.Items.Add(new ListItem("全部经销商","0"));
        ddl_jingxiaoshang.DataSource = _tab.ExecuteQuery("select a.CompID,a.CompName from TJ_RegisterCompanys a where a.CompID in (SELECT authorcompid FROM TJ_DepartMent_CompAuthor where departid=" + ddl_departid.SelectedValue + ") order by CompName", null);
        ddl_jingxiaoshang.DataBind();
        ddl_jingxiaoshang.SelectedValue = "0";
        DisplayData(_currentindex > 0 ? _currentindex : 1, AspNetPager1.PageSize);
    }

    protected void ddl_jingxiaoshang_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!ddl_jingxiaoshang.SelectedValue.Equals("0"))
        {
            DisplayData(_currentindex > 0 ? _currentindex : 1, AspNetPager1.PageSize);
        } 
    }
}
