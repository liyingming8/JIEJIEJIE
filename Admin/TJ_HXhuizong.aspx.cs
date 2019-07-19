using System;
using commonlib;
using TJ.DBUtility;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class Admin_TJ_HXhuizong : AuthorPage
{
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();
    SqlDataAdapter ada = new SqlDataAdapter();
    SqlConnection myConn = new SqlConnection();
    SqlConnection sqlconcom = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            DisplayData(_currentindex, AspNetPager1.PageSize);

        }

    }
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_HXinfo.aspx?cmd=edit&ID={0}',800,620,'详细信息')", ID);
        }
        else
        {
            return "";
        }
    }

    private string Filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {

        
        string sql = "";
        DataSet ds = new DataSet();
        string sqlnew = "SELECT c.AwardThing as pname ,a.JXID,COUNT(*) as 数量,( select count(*)  FROM [TJMarketingSystemYin].[dbo].[TJ_HXInfo] b where b.awid=a.jxid) as 已核销,(COUNT(*)-(select count(*)  FROM [TJMarketingSystemYin].[dbo].[TJ_HXInfo] b where b.awid=a.jxid)) as 未核销 FROM[TJMarketingSystemYin].[dbo].[TJ_BaseLabelCodeInfo_2019] a,  [TJMarketingSystemYin].[dbo].TJ_AwardInfo c where a.Compid='"+GetCookieCompID()+"' and a.JXID= c.AWID  group by JXID , c.AwardThing";  
        var sda = new SqlDataAdapter(sqlnew, sqlconcom);
        var dttemp = new DataTable();
        sda.Fill(dttemp);
        GridView1.DataSource = dttemp;
        GridView1.DataBind();
        dttemp.Dispose();
        sda.Dispose();
        //AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_HXInfo where " + Filtertemp, null).Rows[0][0].ToString());
        //GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_HXInfo", Filtertemp, "id", "id", pageSize);
        //GridView1.DataSource = _tab.ExecuteNonQuery(" select b.remarks,b.total,isnull(b.hx_total,0) as hx_total,(b.total-isnull(b.hx_total,0))as whx_total,b.pid from(select a.Remarks,a.total,e.hx_total,e.pid from(select compid, Remarks, COUNT(Remarks) as total from[TJMarketingSystemYin].[dbo].[TJ_BaseLabelCodeInfo_2019] where Remarks is not null group by Remarks, Compid)a left join(select pid, COUNT(pid) as hx_total from[TJMarketingSystemYin].[dbo].[TJ_HXInfo] where compid = '23567' group by compid, pid)e on e.pid = a.Remarks)b order by b.remarks");
        //GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                var dataKey = GridView1.DataKeys[e.Row.RowIndex];
                if (dataKey != null)
                {
                   
                    ((HyperLink)e.Row.FindControl("hyplink")).Attributes.Add("onclick", XiangXiLinkString(dataKey["jxid"].ToString()));

                }
            }
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
}