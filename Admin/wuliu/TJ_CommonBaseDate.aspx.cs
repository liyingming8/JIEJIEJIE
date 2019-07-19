using System;
using System.Data;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using TJ.BLL; 
using commonlib;
using Wuqi.Webdiyer;

public partial class Admin_wuliu_TJ_CommonBaseDate : AuthorPage
{
    private readonly BTB_Products_Infor bll = new BTB_Products_Infor();
    private DataTable _dttemp=new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            startdate.Text = DateTime.Now.ToString("yyyy-MM-01");
            enddate.Text = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd"); 
            FillDdl();
        }
    } 
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        GetDataAndFillGridView(GetCookieCompID(), ddlprodid.SelectedValue, AspNetPager1.PageSize, startdate.Text, enddate.Text, "");
    }

    private void GetDataAndFillGridView(string compid,string prodid,int pagesize, string datefrom, string dateto,string page)
    {
        var internet = new InternetHandle();
        string url = "http://www.china315net.com:35224/zhpt/qr3d/stk/binding/detail/?company_id=" + compid +(prodid.Equals("0")?"":"&product_id="+prodid)+(string.IsNullOrEmpty(page)?"":"&page="+page)+
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
            _dttemp.Columns.Add("仓库");
            _dttemp.Columns.Add("包装");
            _dttemp.Columns.Add("类型");
            _dttemp.Columns.Add("采集人");
            _dttemp.Columns.Add("时间");
            DataRow dr = _dttemp.NewRow();
            foreach (var jToken in jArray)
            {
                var o = (JObject) jToken;
                dr["号码"] = o["code"].ToString();
                dr["产品"] = o["product"].ToString();
                dr["仓库"] = o["storehouse"].ToString();
                dr["包装"] = o["package"].ToString().Replace(":0:",":").Replace(":","托");
                dr["类型"] = o["package_id"].ToString().Equals("11") ? "小标" : (o["package_id"].ToString().Equals("22")?"中标":"大标");
                dr["采集人"] = o["user"].ToString();
                dr["时间"] = o["time"].ToString();
                _dttemp.Rows.Add(dr.ItemArray);
            }
            GridView1.DataSource = _dttemp;
            GridView1.DataBind();
        }
    }

    private void FillDdl()
    {
        ddlprodid.DataSource = bll.GetListsByFilterString("CompID=" + GetCookieCompID());
        ddlprodid.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c"); 
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1) + e.Row.RowIndex + 1).ToString(); 
        }
    }

    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        GetDataAndFillGridView(GetCookieCompID(), ddlprodid.SelectedValue, AspNetPager1.PageSize, startdate.Text, enddate.Text,
            e.NewPageIndex.ToString());
    }
}

