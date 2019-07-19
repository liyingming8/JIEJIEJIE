using System;
using System.Data;
using System.Web.UI.WebControls;
using TJ.DBUtility; 
using commonlib;
using org.in2bits.MyXls;
using TJ.BLL;

public partial class Admin_TJ_AgentAcceptInfo : AuthorPage
{
     
    TabExecutewuliu tab = new TabExecutewuliu();
    private TabExecute tabmarket = new TabExecute();
    commfrank comfrk = new commfrank();
    BTJ_DepartMent btjDepart = new BTJ_DepartMent();
    private int _currentindex = 1; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            tb_start.Value = DateTime.Now.ToString("yyyy-MM-01");
            tb_end.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
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
        }
    }

    private void Fillddl()
    { 
        ddl_terminal.Items.Clear();
        ddl_terminal.Items.Add(new ListItem("全部终端店","0"));
        if (IsCompGrade())
        {
            _filtertemp = "ParentID in (SELECT a.authorcompid FROM TJ_DepartMent_CompAuthor a where a.departid=" +
                          ddl_departid.SelectedValue + ")"; 
        }
        else
        {
            _filtertemp = "ParentID in (SELECT a.authorcompid FROM TJ_DepartMent_CompAuthor a where a.departid=" +
                          GetCookieTJDepartID() + ")";
            string tempterminalidstrin = comfrk.GetPermitManagerIDString(GetCookieUID());
            if (!string.IsNullOrEmpty(tempterminalidstrin))
            {
                _filtertemp = " ManagerUserID in (" + tempterminalidstrin + ") and " + _filtertemp;
            }
        }
        ddl_terminal.DataSource =tabmarket.ExecuteQuery("select CompID,CompName from TJ_RegisterCompanys where " + _filtertemp+ " order by CompName", null);
        ddl_terminal.DataBind();
        ddl_terminal.SelectedValue = "0";
    } 

    public string Terminalname(string terminalid)
    {
        return tabmarket.ExecuteQueryForSingleValue("select CompName from TJ_RegisterCompanys where CompID=" + terminalid);
    }

    public string ProductName(string prodid)
    {
        return tab.ExecuteQueryForSingleValue("select Products_Name from TB_Products_Infor where Infor_ID=" + prodid);
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_AgentAcceptInfoAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}',600,450,'TJ_AgentAcceptInfo')", Sc.EncryptQueryString(ID));
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
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex-1) + e.Row.RowIndex + 1).ToString();
            DataKey dkey = GridView1.DataKeys[e.Row.RowIndex]; 
            if (dkey != null)
            {
                if (!int.Parse(dkey["isexception"].ToString()).Equals(0))
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                }
            } 
        }
    }
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
       _currentindex = 1;
       AspNetPager1.CurrentPageIndex = 1;
       DisplayData(1, AspNetPager1.PageSize);
    }

    private string _filtertemp ="1=1";
    commfrank comfrank = new commfrank();
    private void DisplayData(int pageIndex, int pageSize)
    { 
        _filtertemp = "AcceptDate between '"+tb_start.Value+"' and '"+Convert.ToDateTime(tb_end.Value).AddDays(1).ToString("yyyy-MM-dd")+"'";
        if (ckb_isexception.Checked)
        {
            _filtertemp += " and isexception=1";
        }
        string tempstring = "";
        if (GetCookieRID()== "168")
        {
            tempstring = comfrank.GetCompIDStringForCityManager(GetCookieUID());
            if (tempstring.Length > 0)
            {
                _filtertemp += " and AcceptAgentID in ("+ tempstring + ")";
                AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(ID) from AgentAcceptInfo_2019 where " + _filtertemp, null).Rows[0][0].ToString());
                GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "AgentAcceptInfo_2019", _filtertemp, "ID desc", "ID", pageSize);
                GridView1.DataBind();
            } 
        }
        else
        {
            if (!string.IsNullOrEmpty(ddl_terminal.SelectedValue) && !ddl_terminal.SelectedValue.Equals("0"))
            {
                _filtertemp += " and CompID=" + GetCookieCompID();
                _filtertemp += " and AcceptAgentID="+ddl_terminal.SelectedValue;
                AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(ID) from AgentAcceptInfo_2019 where " + _filtertemp, null).Rows[0][0].ToString());
                GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "AgentAcceptInfo_2019", _filtertemp, "ID desc", "ID", pageSize);
                GridView1.DataBind();
            }
            else
            {
                tempstring = comfrk.GetPermitManagerIDString(GetCookieUID());
                if (tempstring.Length > 0)
                {
                    tempstring = comfrk.GetCompIDStringForCityManager(tempstring);
                    if (tempstring.Length > 0)
                    {
                        _filtertemp += " and AcceptAgentID in (" + tempstring + ")";
                        AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(ID) from AgentAcceptInfo_2019 where " + _filtertemp, null).Rows[0][0].ToString());
                        GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "AgentAcceptInfo_2019", _filtertemp, "ID desc", "ID", pageSize);
                        GridView1.DataBind();
                    }
                }
                else
                {
                    if (IsCompGrade())
                    {
                        _filtertemp += " and CompID=" + GetCookieCompID();
                        _filtertemp += " and AcceptAgentID in (" + ReturnSubTerminalIdFilter(ddl_departid.SelectedValue) + ")";
                    }
                    else
                    {
                        _filtertemp += " and CompID=" + GetCookieCompID();
                        _filtertemp += " and AcceptAgentID in (" + ReturnSubTerminalIdFilter(GetCookieTJDepartID()) + ")";
                    } 
                    AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(ID) from AgentAcceptInfo_2019 where " + _filtertemp, null).Rows[0][0].ToString());
                    GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "AgentAcceptInfo_2019", _filtertemp, "ID desc", "ID", pageSize);
                    GridView1.DataBind();
                }
            } 
            
        }
    }

    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }

    protected void btn_createexcel_Click(object sender, EventArgs e)
    {
        /*
        string temp = ReturnSubTerminalIdFilter(GetCookieTJDepartID());
        */
        _filtertemp = "AcceptDate between '" + tb_start.Value + "' and '" + Convert.ToDateTime(tb_end.Value).AddDays(1).ToString("yyyy-MM-dd") + "'";
        if (ckb_isexception.Checked)
        {
            _filtertemp += " and isexception=1";
        }
        string tempstring = "";
        if (GetCookieRID() == "168")
        {
            tempstring = comfrank.GetCompIDStringForCityManager(GetCookieUID());
            if (tempstring.Length > 0)
            {
                _filtertemp += " and AcceptAgentID in (" + tempstring + ")";              
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(ddl_terminal.SelectedValue) && !ddl_terminal.SelectedValue.Equals("0"))
            {
                _filtertemp += " and ac.CompID=" + GetCookieCompID();
                _filtertemp += " and AcceptAgentID=" + ddl_terminal.SelectedValue;               
            }
            else
            {
                tempstring = comfrk.GetPermitManagerIDString(GetCookieUID());
                if (tempstring.Length > 0)
                {
                    tempstring = comfrk.GetCompIDStringForCityManager(tempstring);
                    if (tempstring.Length > 0)
                    {
                        _filtertemp += " and AcceptAgentID in (" + tempstring + ")";                     
                    }
                }
                else
                {
                    if (IsCompGrade())
                    {
                        _filtertemp += " and ac.CompID=" + GetCookieCompID();
                        _filtertemp += " and AcceptAgentID in (" + ReturnSubTerminalIdFilter(ddl_departid.SelectedValue) + ")";
                    }
                    else
                    {
                        _filtertemp += " and ac.CompID=" + GetCookieCompID();
                        _filtertemp += " and AcceptAgentID in (" + ReturnSubTerminalIdFilter(GetCookieTJDepartID()) + ")";
                    }                  
                }
            }

        }
        string sqlstring = "select (select CompName from [TJMarketingSystemYin].[dbo].[TJ_RegisterCompanys] where ac.ParentID=CompID ) fjjxs,ac.BoxLabel cd,ac.AcceptDate tm,ac.UploadAddress dizhi,pr.Products_Name pnm,rcomp.CompName cnm from [TianJianWuLiuWebnew].[dbo].[AgentAcceptInfo_2019] ac,[TianJianWuLiuWebnew].[dbo].[TB_Products_Infor] pr,[TJMarketingSystemYin].[dbo].[TJ_RegisterCompanys] rcomp where ac.ProID=pr.Infor_ID and ac.AcceptAgentID=rcomp.CompID and " + _filtertemp;
        DataTable dt = tab.ExecuteQuery(sqlstring, null);
        if (dt.Rows.Count > 0)
        {
            CreateExcel(dt,"终端店扫码-"+tb_start.Value+"至"+tb_end.Value,"扫码信息");
        } 
        dt.Dispose();
    }

    public void CreateExcel(DataTable dt,string filename,string sheetnm)
    {
        XlsDocument xls = new XlsDocument();
        xls.FileName = filename;
        Worksheet sheet = xls.Workbook.Worksheets.Add(sheetnm);//状态栏标题名称  
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
        //Cell cell1 = cells.Add(1, 1, "编号");
        cells.Add(1, 1, "终端店", cellXF);
        cells.Add(1, 2, "产品", cellXF);
        cells.Add(1, 3, "箱码", cellXF);
        cells.Add(1, 4, "时间", cellXF);
        cells.Add(1, 5, "扫码地址", cellXF);
            cells.Add(1, 6, "父级经销商", cellXF);
        int count = dt.Rows.Count;//统计数量 
        if (count > 62000)
        {
            count = 62000;
        }
        for (int i = 0; i < count; i++)
        {
            int rowIndex = i + 2;
            cells.Add(rowIndex, 1, dt.Rows[i]["cnm"].ToString(), cellXF);
            cells.Add(rowIndex, 2, dt.Rows[i]["pnm"].ToString(), cellXF);
            cells.Add(rowIndex, 3, dt.Rows[i]["cd"].ToString(), cellXF);
            cells.Add(rowIndex, 4, dt.Rows[i]["tm"].ToString(), cellXF);
            cells.Add(rowIndex, 5, dt.Rows[i]["dizhi"].ToString(), cellXF);
            cells.Add(rowIndex, 6, dt.Rows[i]["fjjxs"].ToString(), cellXF);
        }
        xls.Send();
    }

    protected void ddl_departid_SelectedIndexChanged(object sender, EventArgs e)
    {
         Fillddl();
        DisplayData((_currentindex <= 0 ? 1 : _currentindex), AspNetPager1.PageSize);
    }

    protected void ddl_terminal_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayData((_currentindex <= 0 ? 1 : _currentindex), AspNetPager1.PageSize);
    }

    protected void ddl_terminal_ComboBoxChanged(object sender, EventArgs e)
    {
        DisplayData((_currentindex <= 0 ? 1 : _currentindex), AspNetPager1.PageSize);
    }
}
