using System;
using System.Web.UI.WebControls;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;
using TJ.BLL;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class Admin_commonswm_TJ_RegistercommonswmShouQuan : AuthorPage
{
    TabExecute _tab = new TabExecute();
    public CommonFun comfun = new CommonFun();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DisplayData(1, AspNetPager1.PageSize);
        }
    }

    protected void DisplayData(int pageIndex,int pageSize)
    {
        string _filtertemp = "UseSWM=1 and CompAutherID=68 ";
        string fillSQL = "";
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            _filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
            fillSQL= " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
        }
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(CompID) from TJ_RegisterCompanys where " + _filtertemp, null).Rows[0][0].ToString());
        string mSQL = @"select CompID,CompName,CompAutherID,MasterID,Address,AuthoredDate from (SELECT  top " + pageSize + @" * from  (
               SELECT top 100 percent * FROM TJ_RegisterCompanys 
               where useswm= 1 and CompAutherID=68   ) a where CompID not in (SELECT top " + (pageIndex - 1) * pageSize + @" CompID FROM TJ_RegisterCompanys
               where useswm= 1 and CompAutherID=68 ))aa where 1=1 "+ fillSQL + " ORDER BY MasterID"; //where 1=1 "+ fillSQL + " ORDER BY MasterID";
        DataTable mPageData = _tab.ExecuteNonQuery(mSQL);
        GridView1.DataSource = mPageData;
        GridView1.DataBind();
    }

    protected string CompTypeID(string CompTypeID)
    {
        if (!string.IsNullOrEmpty(CompTypeID))
        {
            if (CompTypeID.Equals("484"))
            {
                return "个人";
            }
            else
            {
                return "公司";
            }
        }
        else
        {
            return "";
        }
    }

    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        DisplayData(1, AspNetPager1.PageSize);
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
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString(), dataKey[1].ToString()));
                    ((HtmlImage)e.Row.FindControl("editimg")).Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString(), dataKey[1].ToString()));
                }
            }
            if (!dataKey[1].ToString().Equals("1"))
            {
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    public string XiangXiLinkString(string ID, string MasterID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_RegistercommonswmShouQuanAndEdit.aspx?ID={0}&MasterID={1}',760,520,'详细信息')", ID, MasterID);
        }
        else
        {
            return "";
        }
    }

    protected string ShouQuanInfo(string ID)
    {
        if (ID.Equals("1"))
        {
            return "已授权";
        }else
        {
            return "未授权";
        }
    }

    protected string JiJuoInfo(string ID)
    {
        if (ID.Equals("67"))
        {
            return "未激活";
        }
        else if(ID.Equals("68"))
        {
            return "已激活";
        }else
        {
            return "已冻结";
        }
    }

}