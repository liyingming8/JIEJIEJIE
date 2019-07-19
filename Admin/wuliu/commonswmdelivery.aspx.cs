using System;
using System.Data;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using TJ.BLL;
using TJ.DBUtility;
using commonlib;
using Wuqi.Webdiyer;

public partial class Admin_wuliu_commonswmdelivery : AuthorPage
{
    private readonly BTB_Products_Infor bll = new BTB_Products_Infor(); 
    public BTB_Products_Type ptype = new BTB_Products_Type();   
    readonly TabExecute _tab = new TabExecute();
    private DataTable _dttemp;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            startdate.Text = DateTime.Now.ToString("yyyy-MM-01");
            enddate.Text = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            FillDdl();
        }
    }
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TB_Products_InforAddEdit.aspx?cmd=edit&ID={0}',800,400,'产品信息编辑')", ID);
        }
        else
        {
            return "";
        }
    }
    
    
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        GetDataAndFillGridView(GetCookieCompID(), ddlprodid.SelectedValue, AspNetPager1.PageSize, startdate.Text, enddate.Text, "");
    }

    private void FillDdl()
    {
        ddlprodid.DataSource = bll.GetListsByFilterString("CompID=" + GetCookieCompID());
        ddlprodid.DataBind();
        ddlagent.DataSource = _tab.ExecuteQuery("SELECT b.[CompID],b.CompName FROM [TianJianWuLiuWebnew].[dbo].[TB_CompAgentInfo] a,[TJMarketingSystemYin].dbo.TJ_RegisterCompanys b where a.AgentID=b.CompID and a.CompID="+GetCookieCompID()+" order by b.CompName",null);
        ddlagent.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1) + e.Row.RowIndex + 1).ToString();
        }
    }

    private void GetDataAndFillGridView(string compid, string prodid, int pagesize, string datefrom, string dateto, string page)
    {
        var internet = new InternetHandle();
        string url = "http://www.china315net.com:35224/zhpt/qr3d/stk/delivery/detail/?company_id=" + compid + (prodid.Equals("0") ? "" : "&product_id=" + prodid) + (string.IsNullOrEmpty(page) ? "" : "&page=" + page) +
                     "&records_per_page=" + pagesize + "&date_from=" + datefrom + "&date_to=" + dateto;
        string tempvalue = internet.GetUrlData(url);
        JObject jobject = JObject.Parse(tempvalue);
        int pages = int.Parse(jobject["pages"].ToString());
        if (pages > 0)
        {
            AspNetPager1.RecordCount = pages * AspNetPager1.PageSize;
        }
        JArray jArray = JArray.Parse(jobject["detail"].ToString());
        if (jArray.Count > 0)
        {
            _dttemp = new DataTable();
            _dttemp.Columns.Add("号码");
            _dttemp.Columns.Add("产品");
            _dttemp.Columns.Add("经销商");
            _dttemp.Columns.Add("包装");
            _dttemp.Columns.Add("类型");
            _dttemp.Columns.Add("发货人");
            _dttemp.Columns.Add("时间");
            DataRow dr = _dttemp.NewRow();
            foreach (JObject o in jArray)
            {
                dr["号码"] = o["code"].ToString();
                dr["产品"] = o["product"].ToString();
                dr["经销商"] = o["to_company"].ToString();
                dr["包装"] = o["package"].ToString().Replace(":0:", ":").Replace(":", "托");
                dr["类型"] = o["package_id"].ToString().Equals("11") ? "小标" : (o["package_id"].ToString().Equals("22") ? "中标" : "大标");
                dr["发货人"] = o["user"].ToString();
                dr["时间"] = o["time"].ToString();
                _dttemp.Rows.Add(dr.ItemArray);
            }
            GridView1.DataSource = _dttemp;
            GridView1.DataBind();
        }
    }

    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        GetDataAndFillGridView(GetCookieCompID(), ddlprodid.SelectedValue, AspNetPager1.PageSize, startdate.Text, enddate.Text,
           e.NewPageIndex.ToString());
    }
}
