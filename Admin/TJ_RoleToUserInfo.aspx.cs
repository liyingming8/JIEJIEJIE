using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using TJ.DBUtility;
using System.Web.UI.WebControls;
using commonlib;
using Wuqi.Webdiyer;


public partial class Admin_TJ_RoleToUserInfo : AuthorPage
{
    readonly TabExecute _tab = new TabExecute();
    private int _currentindex = 1;
    SqlConnection sqlcon = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["RID"]))
            {
                mUserRID.Value = Request.QueryString["RID"];
                DisplayData(mUserRID.Value, _currentindex, AspNetPager1.PageSize);
            }
        }
    }

    private void DisplayData(string mUserRID, int pageIndex, int pageSize)
    {
        string Filtertemp = mUserRID;
        string mUserName = "";
        DataTable mDataTable = new DataTable("RoleToUser");
        DataColumn mDataColumn1 = new DataColumn("CompName", Type.GetType("System.String"));
        DataColumn mDataColumn2 = new DataColumn("UserName", Type.GetType("System.String"));
        mDataTable.Columns.Add(mDataColumn1);
        mDataTable.Columns.Add(mDataColumn2);
        string mSQL = @"SELECT  top " + pageSize + @" CompID from  (
               SELECT top 100 percent CompID FROM [TJMarketingSystemYin].[dbo].[TJ_User] 
               where RID= " + mUserRID + @" GROUP BY CompID  ORDER BY CompID ) a where CompID not in (SELECT top " + (pageIndex - 1) * pageSize + @" CompID FROM [TJMarketingSystemYin].[dbo].[TJ_User] 
               where RID= " + mUserRID + @" GROUP BY CompID  ORDER BY CompID)";
        DataTable mPageData = _tab.ExecuteNonQuery(mSQL);
        if (mPageData.Rows.Count > 0)
        {
            for (int i = 0; i < mPageData.Rows.Count; i++)
            {
                DataTable mUserInfo = _tab.ExecuteNonQuery("SELECT CompID,LoginName FROM [TJMarketingSystemYin].[dbo].[TJ_User] where RID=" + mUserRID + " and CompID=" + mPageData.Rows[i][0]);
                if (mUserInfo.Rows.Count > 0)
                {
                    //公司名称
                    string mCompName = _tab.ExecuteQueryForSingleValue("select [CompName] from [TJ_RegisterCompanys] where [CompID]=" + mPageData.Rows[i][0]);
                    //用户信息
                    for (int j = 0; j < mUserInfo.Rows.Count; j++)
                    {
                        mUserName += "," + mUserInfo.Rows[j][1];
                    }
                    if (mUserName.Length > 0)
                    {
                        mUserName = mUserName.Substring(1);
                    }
                    DataRow mDataRow = mDataTable.NewRow();
                    mDataRow["CompName"] = mCompName;
                    mDataRow["UserName"] = mUserName;
                    mDataTable.Rows.Add(mDataRow);
                }
                mUserName = "";
            }
            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from (SELECT CompID FROM [TJMarketingSystemYin].[dbo].[TJ_User] where RID=" + mUserRID + " GROUP BY CompID) a", null).Rows[0][0].ToString());
            GridView1.DataSource = mDataTable;
            GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        }
    }

    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(mUserRID.Value, e.NewPageIndex, AspNetPager1.PageSize);
    }
}