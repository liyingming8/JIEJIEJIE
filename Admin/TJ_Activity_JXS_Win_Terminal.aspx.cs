using System;
using System.Data;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL; 
using commonlib;
using org.in2bits.MyXls;

public partial class Admin_TJ_Activity_JXS_Win_Terminal : AuthorPage
{
    private readonly BTJ_Activity_JXS_Win _bll = new BTJ_Activity_JXS_Win();
    readonly TabExecute _tab = new TabExecute();
    public BTJ_RegisterCompanys BtjRegister = new BTJ_RegisterCompanys();
    public BTJ_AwardType BtjAwardType = new BTJ_AwardType();
    private int _currentindex = 1;
    readonly commfrank _comfrank = new commfrank();
    BTJ_DepartMent btjDepart = new BTJ_DepartMent();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //hdcompid.Value = !string.IsNullOrEmpty(Request.QueryString["agid"]) ? Request.QueryString["agid"].Trim() : "";
            //if (!string.IsNullOrEmpty(Request.QueryString["agname"]))
            //{
            //    inputjxs.Value = Request.QueryString["agname"].Trim();
            //}
            if (IsCompGrade())
            {
                ddl_departid.Visible = true;
                ddl_departid.DataSource = btjDepart.GetListsByFilterString("compid=" + GetCookieCompID() + " and parentid=" + GetCookieTJDepartID());
                ddl_departid.DataBind();
                ddl_departid.SelectedIndex = 0;
            }
            else
            {
                ddl_departid.Visible = false;
            }

            Fillddl();
            _currentindex = 1;
             AspNetPager1.CurrentPageIndex = 1;
             DisplayData(1, AspNetPager1.PageSize);
             //inputjxs.Attributes.Add("onclick", XiangXiLinkStringForTerminal());
   
        }
    }

    private string _filtertemp = string.Empty;
    private void Fillddl()
    {
        ddl_terminal.Items.Clear();
        ddl_terminal.Items.Add(new ListItem("全部终端店", "0"));
        if (IsCompGrade())
        {
            _filtertemp = "ParentID in (SELECT a.authorcompid FROM TJ_DepartMent_CompAuthor a where a.departid=" +
                          ddl_departid.SelectedValue + ")";
        }
        else
        {
            _filtertemp = "ParentID in (SELECT a.authorcompid FROM TJ_DepartMent_CompAuthor a where a.departid=" +
                          GetCookieTJDepartID() + ")";
            string tempterminalidstrin = _comfrank.GetPermitManagerIDString(GetCookieUID());
            if (!string.IsNullOrEmpty(tempterminalidstrin))
            {
                _filtertemp = " ManagerUserID in (" + tempterminalidstrin + ") and " + _filtertemp;
            }
        }
        ddl_terminal.DataSource = _tab.ExecuteQuery("select CompID,CompName from TJ_RegisterCompanys where " + _filtertemp + " order by CompName", null);
        ddl_terminal.DataBind();
        ddl_terminal.SelectedValue = "0";
    }

    public string XiangXiLinkStringForTerminal()
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('../Admin/wuliu/TB_Agents_Infor_Terminal_select.aspx?fr=" + Sc.EncryptQueryString(Request.RawUrl) + "',580,460,'指定终端店')");
        }
        return "";
    }

    public string XiangXiLinkString(string labelcode)
    {
        if (labelcode.Length > 0)
        {
            return "openWinCenter('wuliu/LabelInfoQurey.aspx?labelcode=" + Sc.EncryptQueryString(labelcode)+"',1000,500,'标签发货明细')";
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
         {
             var key = GridView1.DataKeys[e.RowIndex];
             if (key != null)
                 _bll.Delete(int.Parse(key["id"].ToString()));
         }
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
                ((HyperLink)e.Row.FindControl("linkremarks")).Attributes.Add("onclick", XiangXiLinkString(dataKey["remarks"].ToString()));
            }
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex-1) + e.Row.RowIndex + 1).ToString();
            if (IsSuperAdmin())
            {
                ((LinkButton)e.Row.Cells[9].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除当前记录吗?')");
            }
            else
            {
                 e.Row.Cells[9].Enabled= false;
                 e.Row.Cells[9].ForeColor = System.Drawing.Color.LightGray;
            }
        }
    } 
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
       _currentindex = 1;
       AspNetPager1.CurrentPageIndex = 1;
       DisplayData(1, AspNetPager1.PageSize);
    }

    
    private void DisplayData(int pageIndex, int pageSize)
    {
        string filtertemp = "compid=" + GetCookieCompID();
        if (!ddl_gettype.SelectedValue.Equals("0"))
        {
            filtertemp += " and wintypeid=" + ddl_gettype.SelectedValue;
        }
        if (!ddl_terminal.SelectedValue.Equals("0"))
        {
            filtertemp += " and agentid=" + ddl_terminal.SelectedValue;
        }
        else
        {
            if (!IsCompGrade())
            {
                filtertemp += " and agentid in (" + ReturnSubTerminalIdFilter(GetCookieTJDepartID()) + ")";
            }
            else
            {
                filtertemp += " and agentid in (" + ReturnSubTerminalIdFilter(ddl_departid.SelectedValue) + ")";
            }
        } 
       AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(id) from TJ_Activity_JXS_Win where "+filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_Activity_JXS_Win", filtertemp,"id desc", "id", pageSize);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }  
    protected void btn_refresh_Click(object sender, EventArgs e)
    {
        _comfrank.FreshTerminalIntegral(GetCookieCompID(), int.Parse(string.IsNullOrEmpty(hdcompid.Value)?"0":hdcompid.Value));
        DisplayData(1, AspNetPager1.PageSize);
    }

    protected void btn_createexcel_Click(object sender, EventArgs e)
    {
        /*
        string filtertemp = "win.compid=" + GetCookieCompID();
        if (hdcompid.Value.Length > 0)
        {
            filtertemp += " and win.agentid=" + hdcompid.Value;
        }
        filtertemp += " and win.agentid in (" + ReturnSubTerminalIdFilter(GetCookieTJDepartID()) + ")";
        */
        string filtertemp = "and win.compid=" + GetCookieCompID();
        if (!ddl_gettype.SelectedValue.Equals("0"))
        {
            filtertemp += " and wintypeid=" + ddl_gettype.SelectedValue;
        }
        if (!ddl_terminal.SelectedValue.Equals("0"))
        {
            filtertemp += " and agentid=" + ddl_terminal.SelectedValue;
        }
        else
        {
            if (!IsCompGrade())
            {
                filtertemp += " and agentid in (" + ReturnSubTerminalIdFilter(GetCookieTJDepartID()) + ")";
            }
            else
            {
                filtertemp += " and agentid in (" + ReturnSubTerminalIdFilter(ddl_departid.SelectedValue) + ")";
            }
        }
        string sqlstirng = "select rcomp.CompName cnm,awt.awardtype tnm,win.winreason rson,win.prizevl pvl,win.confirmtm tm,win.islq lq from TJ_Activity_JXS_Win win,TJ_RegisterCompanys rcomp,TJ_AwardType awt where win.awtypeid=awt.id and win.agentid=rcomp.CompID  " + filtertemp;
        DataTable dt = _tab.ExecuteQuery(sqlstirng, null);
        if (dt.Rows.Count > 0)
        {
            CreateExcel(dt, "终端奖励信息"+(ddl_terminal.Text.Length.Equals(0)?"":"-"+ddl_terminal.Text.Trim()), "奖励信息");
        }
        dt.Dispose();
    }

    public void CreateExcel(DataTable dt, string filename, string sheetnm)
    {
        XlsDocument xls = new XlsDocument();
        xls.FileName = filename;
        Worksheet sheet = xls.Workbook.Worksheets.Add(sheetnm);//状态栏标题名称  
        ColumnInfo cinfo = new ColumnInfo(xls, sheet);
        cinfo.Collapsed = true;
        //设置列的范围 如 0列-10列 
        cinfo.ColumnIndexStart = 0;//列开始 
        cinfo.ColumnIndexEnd = 6;//列结束 
        cinfo.Collapsed = true;
        cinfo.Width = 80 * 60;
        sheet.AddColumnInfo(cinfo);
        XF cellXF = xls.NewXF();
        cellXF.VerticalAlignment = VerticalAlignments.Centered;
        cellXF.HorizontalAlignment = HorizontalAlignments.Centered;
        cellXF.Font.Height = 18 * 12;
        Cells cells = sheet.Cells;
        //Cell cell1 = cells.Add(1, 1, "编号");
        cells.Add(1, 1, "终端店", cellXF);
        cells.Add(1, 2, "类型", cellXF);
        cells.Add(1, 3, "方式", cellXF);
        cells.Add(1, 4, "积分", cellXF);
        cells.Add(1, 5, "时间", cellXF);
        cells.Add(1, 6, "是否领取", cellXF);
        int count = dt.Rows.Count;//统计数量 
        if (count > 62000)
        {
            count = 62000;
        }
        for (int i = 0; i < count; i++)
        {
            int rowIndex = i + 2;
            cells.Add(rowIndex, 1, dt.Rows[i]["cnm"].ToString(), cellXF);
            cells.Add(rowIndex, 2, dt.Rows[i]["tnm"].ToString(), cellXF);
            cells.Add(rowIndex, 3, dt.Rows[i]["rson"].ToString(), cellXF);
            cells.Add(rowIndex, 4, dt.Rows[i]["pvl"].ToString(), cellXF);
            cells.Add(rowIndex, 5, dt.Rows[i]["tm"].ToString(), cellXF); 
            cells.Add(rowIndex, 6, (Convert.ToBoolean(dt.Rows[i]["lq"].ToString())?"已领取":"未领取"), cellXF);
        }
        xls.Send();
    }

    protected void ddl_departid_SelectedIndexChanged(object sender, EventArgs e)
    {
         Fillddl();
        DisplayData(_currentindex<1?1:_currentindex, AspNetPager1.PageSize);
    }

    protected void ddl_terminal_OnComboBoxChanged(object sender, EventArgs e)
    {
        if (_currentindex < 1)
        {
            _currentindex = 1;
        } 
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);
    }

    protected void ddl_gettype_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayData(_currentindex < 1 ? 1 : _currentindex, AspNetPager1.PageSize);
    }
}
