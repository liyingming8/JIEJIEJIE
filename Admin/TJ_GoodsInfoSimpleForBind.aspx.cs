using System; 
using System.Web.UI.WebControls;
using TJ.BLL; 
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_TJ_GoodsInfoSimpleForBind : AuthorPage
{ 
    private readonly BTJ_RegisterCompanys _bcompany = new BTJ_RegisterCompanys();  
    private readonly BTB_Products_Type _ptype = new BTB_Products_Type();
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
            {
                hf_pid.Value = Sc.DecryptQueryString(Request.QueryString["pid"].Trim());
            }
            else
            {
                hf_pid.Value = GetCookieCompID();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["prodid"]))
            {
                hf_goodid.Value = Sc.DecryptQueryString(Request.QueryString["prodid"]);
            }
            _currentindex = 1;
            AspNetPager1.CurrentPageIndex = _currentindex;
            DisplayData(1, AspNetPager1.PageSize); 
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_GoodsInfoAddEditSimple.aspx?cmd=edit&ID={0}',680,680,'产品信息管理')", ID);
        }
        else
        {
            return "";
        }
    } 
    private void DisplayData(int pageIndex, int pageSize)
    {
        string filtertemp = "CompID=" +GetCookieCompID(); 
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_GoodsInfo where " + filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_GoodsInfo", filtertemp, "GoodsID", "GoodsID", pageSize);
        GridView1.DataBind();
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
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString())); 
            }
            e.Row.Attributes.Add("onclick", "select(" + e.Row.RowIndex + ")");
        }
    }
 
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;//gridview 显示初始页面 
        DisplayData(1, AspNetPager1.PageSize);
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);
    }

    public string ReturnCompanyName(string CPID)
    {
        return _bcompany.GetList(int.Parse(CPID)).CompName;
    }

    public string ReturnGoodsTypeName(string GoodTypeID)
    {
        return _ptype.GetList(int.Parse(GoodTypeID)).TypeName;
        
    } 
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(hf_goodid.Value) && !string.IsNullOrEmpty(hf_pid.Value))
        {
            var internet = new InternetHandle();
            internet.GetUrlData("http://www.china315net.com:35224/zhpt/qr3d/set/product/?pid=" + hf_pid.Value +
                                "&product_id=" + hf_goodid.Value);
            ClientScript.RegisterStartupScript(GetType(), "reload", "closemyWindow();", true);
        }
    }
}