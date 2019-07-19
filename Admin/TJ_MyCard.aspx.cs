using System;
using Wuqi.Webdiyer;
using commonlib;
using TJ.DBUtility;
using System.Web.UI.WebControls;
using org.in2bits.MyXls;
using System.Data;

public partial class Admin_TJ_MyCard : AuthorPage
{
    public int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DisplayData(_currentindex, AspNetPager1.PageSize);
        }
    }

    protected void DisplayData(int pageIndex, int pageSize)
    {
        string _filtertemp = " Compid='" + GetCookieCompID() + "'";
        string mNickName = "";
        string mSQL = "SELECT top " + pageSize + @" * from (SELECT a.userid,(SELECT NickName from TJ_User where UserID=a.userid) as NickName,a.total from (SELECT  top 100 percent userid,count(userid) as total 
         FROM TJ_MyCard where "+ _filtertemp + @" group by userid order by userid)a where userid not in
         (SELECT top " + (pageIndex-1)* pageSize + " userid FROM TJ_MyCard where " + _filtertemp + @" GROUP BY userid  ORDER BY userid))b ";
        if (inputSearchKeyword.Value.Length > 0)
        {
            mSQL+= " where NickName like '%" + inputSearchKeyword.Value + "%'";
            mNickName = inputSearchKeyword.Value;
        }
        AspNetPager1.RecordCount = Convert.ToInt32(_tab.ExecuteQuery("select count(userid) from (select * from(select *,(select NickName from TJ_User where UserId=b.userid) as NickName from (select distinct userid  from TJ_MyCard where " + _filtertemp + @")b)c 

         where  NickName like '%"+ mNickName + "%')a", null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteNonQuery(mSQL);
        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                var dataKey = GridView1.DataKeys[e.Row.RowIndex];
                if (dataKey != null)
                {

                    ((HyperLink)e.Row.FindControl("hyplink")).Attributes.Add("onclick", XiangXiLinkString(dataKey["userid"].ToString()));

                }
            }
        }
    }

    protected string XiangXiLinkString(string userid)
    {
        if (!string.IsNullOrEmpty(userid))
        {
            return string.Format("javascript:var win=openWinCenter('TJ_MyCardAndDetails.aspx?userid={0}',800,620,'卡片详细信息')", userid);
        }
        else
        {
            return "";
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        DisplayData(1, AspNetPager1.PageSize);
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        DisplayData(_currentindex, AspNetPager1.PageSize);
    }

    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }

    protected string FindUserNameFromUserId(string userid)
    {
        if (!string.IsNullOrEmpty(userid))
        {
            return _tab.ExecuteQueryForSingleValue("select NickName from TJ_User where UserID=" + userid);
        }
        else
        {
            return "";
        }
    }
    
    public void btn_createexcel_Click(object sender, EventArgs e)
    {
        string _filtertemp = " Compid='" + GetCookieCompID()+"'";
        string mSQL = string.Format(@"select * from (select (SELECT NickName from TJ_User where UserID=a.userid) as NickName ,count(userid) as total from TJ_MyCard a where 
               "+ _filtertemp + "group by userid )b");
        if (inputSearchKeyword.Value.Length > 0)
        {
            mSQL += " where NickName like '%" + inputSearchKeyword.Value + "%'";
        }
        DataTable dt = _tab.ExecuteQuery(mSQL, null);
        if (dt.Rows.Count>0) {
            CreateExcel(dt);
            dt.Dispose();
        }else
        {
           MessageBox.Show(this,"没有数据！");
        }
    }

    public void CreateExcel(DataTable dt)
    {
        XlsDocument xls = new XlsDocument();
        xls.FileName = "卡片信息表 ";
        Worksheet sheet = xls.Workbook.Worksheets.Add("卡片信息");//状态栏标题名称  
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
        Cell cell1 = cells.Add(1, 1, "昵称", cellXF);
        Cell cell2 = cells.Add(1, 2, "卡片数量", cellXF);
        //Cell cell3 = cells.Add(1, 3, "数量", cellXF);
        //Cell cell4 = cells.Add(1, 4, "类型", cellXF);
        //Cell cell5 = cells.Add(1, 5, "备注", cellXF);
        int count = dt.Rows.Count;//统计数量 
        if (count > 62000)
        {
            count = 62000;
        }
        for (int i = 0; i < count; i++)
        {
            int rowIndex = i + 2;
            cells.Add(rowIndex, 1, dt.Rows[i]["NickName"].ToString(), cellXF);
            cells.Add(rowIndex, 2, dt.Rows[i]["total"].ToString(), cellXF);
            //cells.Add(rowIndex, 3, dt.Rows[i]["num"].ToString(), cellXF);
            //cells.Add(rowIndex, 4, dt.Rows[i]["awardtype"].ToString(), cellXF);
            //cells.Add(rowIndex, 5, dt.Rows[i]["remark"].ToString(), cellXF);
        }
        xls.Send();
    }

    protected string FindAwardThingFromAWID(string awid)
    {
        if (!string.IsNullOrEmpty(awid))
        {
            return _tab.ExecuteQueryForSingleValue("select AwardThing from TJ_AwardInfo where AWID=" + awid);
        }
        else
        {
            return "";
        }
    }

    protected string FindAwardTypeFromAwTypeId(string awtypeid)
    {
        if (!string.IsNullOrEmpty(awtypeid))
        {
            return _tab.ExecuteQueryForSingleValue("select awardtype from TJ_AwardType where id=" + awtypeid);
        }
        else
        {
            return "";
        }
    }
    
}