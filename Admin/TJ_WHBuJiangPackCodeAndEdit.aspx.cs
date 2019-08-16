using System;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;
using System.Web.UI.WebControls;

public partial class Admin_TJ_WHBuJiangPackCodeAndEdit : AuthorPage
{
    TabExecute _tab = new TabExecute();
    private int currentPage=1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["r"]))
            {
                hf_r.Value = Sc.DecryptQueryString(Request.QueryString["r"]);
            }
            DisplayData(1, AspNetPager1.PageSize);
        }
    }

    protected void DisplayData(int pageIndex, int pageSize)
    {
        string Filtertemp = " compid=" + GetCookieCompID();
        if (inputSearchKeyword.Value.Length>0)
        {
            Filtertemp += " and  DanHao like  '%" + inputSearchKeyword.Value + "%'"; 
        }
       string mSQL = "select distinct DanHao from TJ_BaseLabelCodeInfo_2019 where DanHao is not null and len(DanHao)>0 and (HBJE is null or len(HBJE)=0) and " + Filtertemp;
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(DanHao) from ("+ mSQL + ")aa", null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteNonQuery(@"  
               SELECT  top "+ pageSize + @" DanHao from  (
               SELECT distinct top 100 percent DanHao FROM TJ_BaseLabelCodeInfo_2019 
               where DanHao is not null and len(DanHao)>0 and (HBJE is null or len(HBJE)=0) and " + Filtertemp+@" ORDER BY DanHao desc)a where DanHao 
			   not in (SELECT distinct top "+(pageIndex-1)*pageSize+ @"  DanHao FROM 
			   TJ_BaseLabelCodeInfo_2019 
               where DanHao is not null and len(DanHao)>0 and (HBJE is null or len(HBJE)=0) and " + Filtertemp + @" ORDER BY DanHao desc) ORDER BY DanHao desc");
        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (currentPage - 1) + e.Row.RowIndex + 1).ToString();
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
            {
                e.Row.Attributes.Add("onclick", XiangXiLinkString(dataKey["DanHao"].ToString()));
            }
        }
    }       

    protected string XiangXiLinkString(string BaoBianHao)
    {
        string temp = hf_r.Value.ToString();
        int count = 0;
        foreach (char sigal in temp)
        {
            if (sigal.ToString()=="&")
            {
                count += 1;
            }
        }      
        if (count>0)
        {
            for (int i=0;i<count;i++) {
                temp = temp.Substring(0, temp.LastIndexOf("&"));
            }
        }
        var uid = Guid.NewGuid().ToString();
        return string.Format("javascript:closemyWindowReloadNewhref('" + temp + "&BaoBianHao={0}&uid="+uid+"')", BaoBianHao);
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        DisplayData(1, AspNetPager1.PageSize);
    }

    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        currentPage = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}