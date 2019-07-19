using System;
using System.Web.UI.WebControls;
using TJ.BLL; 
using TJ.DBUtility;
using commonlib;
using Wuqi.Webdiyer;

public partial class Admin_TB_Products_Infor_forsingleselect : AuthorPage
{
    private readonly BTB_Products_Infor bll = new BTB_Products_Infor(); 
    public BTB_Products_Type ptype = new BTB_Products_Type();
    public BTB_ProducStandards pstandards = new BTB_ProducStandards();
    public BTB_ProductJingHanLiang pjinghanliang = new BTB_ProductJingHanLiang();
    public BTB_ProductXiangXing Pxiangxing = new BTB_ProductXiangXing();
    public BTB_ProductJiuJingDu Pjiujingdu = new BTB_ProductJiuJingDu();
    public BTB_BiaoZhun Bbiaozhun = new BTB_BiaoZhun();
    public BTB_Metries Byuanliao = new BTB_Metries();
    private int _currentindex = 1; 
    readonly TabExecutewuliu _tab = new TabExecutewuliu(); 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["fr"]))
            {
                hf_fr.Value = Sc.DecryptQueryString(Request.QueryString["fr"]);
                _currentindex = 1;
                AspNetPager1.CurrentPageIndex = _currentindex;
                DisplayData(_currentindex, AspNetPager1.PageSize);
            } 
        }
    }
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return
                string.Format(
                    "javascript:var win=openWinCenter('TB_Products_InforAddEdit.aspx?cmd=edit&ID={0}',800,400,'产品信息编辑')", ID);
        }
        else
        {
            return "";
        }
    }

    public string XiangGoBackLinkString(string ID, string PName)
    {
        if (ID.Length > 0)
        {
            //双击后编辑修改方法
            return string.Format("javascript:closemyWindowReloadNewhref('" + hf_fr.Value + "&proid={0}&proname={1}')", ID, PName);
        }
        else
        {
            return "";
        }
    }
    
    private void DisplayData(int pageIndex, int pageSize)
    { 
        string filtertemp = "CompID=" + GetCookieCompID();
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {

            filtertemp = "CompID =" + GetCookieCompID() + " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";

        }
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TB_Products_Infor where " + filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TB_Products_Infor", filtertemp, "Infor_ID", "Infor_ID", pageSize);
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        var dataKey = GridView1.DataKeys[e.RowIndex];
        if (dataKey != null)
        {
            bll.Delete(int.Parse(dataKey["Infor_ID"].ToString()));
        } 
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize); 
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    } 
   
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    { 
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#CCFF83'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c"); 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
            {
                e.Row.Attributes.Add("onclick", XiangGoBackLinkString(dataKey["Infor_ID"].ToString(), dataKey["Products_Name"].ToString())); 
            } 
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        //fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }

}
