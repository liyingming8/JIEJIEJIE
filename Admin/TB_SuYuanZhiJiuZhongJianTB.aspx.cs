using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using TJ.Model;
using commonlib;
using Wuqi.Webdiyer;

public partial class Admin_TB_SuYuanZhiJiuZhongJianTB : AuthorPage
{
    readonly BTB_SuYuanZhiJiuZhongJianTB bll = new BTB_SuYuanZhiJiuZhongJianTB();
    MTB_SuYuanZhiJiuZhongJianTB mod = new MTB_SuYuanZhiJiuZhongJianTB();
    readonly TabExecute tab = new TabExecute();
    public BTB_SuYuanZhiJiu BtbSuYuanZhiJiu = new BTB_SuYuanZhiJiu();
    public BTB_SuYuanGrain BtbSuYuanGrain= new BTB_SuYuanGrain();
    public BTB_SuYuanChengQuTB BtbSuYuanChengQu=new BTB_SuYuanChengQuTB(); 
    private int _currentindex = 1; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ZJID"]))
            {
                HF_ZJID.Value = Sc.DecryptQueryString(Request.QueryString["ZJID"]); 
            }
            else
            {
                Response.End();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["YL"]))
            {
                HF_YL.Value = Request.QueryString["YL"];
            }
            _currentindex = 1;
            AspNetPager1.CurrentPageIndex = 1;
            DisplayData(1, AspNetPager1.PageSize);
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TB_SuYuanZhiJiuZhongJianTBAddEdit.aspx?YL="+HF_YL.Value+"&ZJID=" + Sc.EncryptQueryString(HF_ZJID.Value) + "&cmd=" + Sc.EncryptQueryString("edit") + "&ID={0}',600,360,'成曲配料')", Sc.EncryptQueryString(ID));
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
         bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["ID"].ToString()));
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
            e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex-1) + e.Row.RowIndex + 1).ToString();
            if (IsSuperAdmin())
            {
                ((LinkButton)e.Row.Cells[6].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
            }
            else
            {
                 e.Row.Cells[6].Enabled= false;
                 e.Row.Cells[6].ForeColor = Color.LightGray;
            }
        }
    }
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
       _currentindex = 1;
       AspNetPager1.CurrentPageIndex = 1;
       DisplayData(1, AspNetPager1.PageSize);
    }

    private string _filtertemp = "";
    private void DisplayData(int pageIndex, int pageSize)
    {
        _filtertemp = "ZJID=" + HF_ZJID.Value;
        if (HF_YL.Value.Equals("1"))
        {
            _filtertemp += " and YLID>0";
        }
        else
        {
            _filtertemp += " and CQID>0";
        }
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(ID) from TB_SuYuanZhiJiuZhongJianTB where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TB_SuYuanZhiJiuZhongJianTB", _filtertemp, "ID", "ID", pageSize);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }
}
