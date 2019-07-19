using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
using Wuqi.Webdiyer;

public partial class CRM_tj_crm_brandpictures : AuthorPage
{
    //Btj_crm_brandpictures bll = new Btj_crm_brandpictures();
    Mtj_crm_brandpictures mod = new Mtj_crm_brandpictures();
    readonly PGTabExecuteCRM tab = new PGTabExecuteCRM();
    //public Btj_crm_brandinfo Brandinfo = new Btj_crm_brandinfo();
    private int _currentindex;
    public string Brandname;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["bid"]))
            {
                hf_bid.Value = Sc.DecryptQueryString(Request.QueryString["bid"]);
                //Brandname = Brandinfo.GetList(int.Parse(hf_bid.Value)).brandname;
            }
            else
            {
                Response.End();
            }
            _currentindex = 1;
            AspNetPager1.CurrentPageIndex = 1;
            DisplayData(1, AspNetPager1.PageSize);
        }
    }

    public string GetBrandName(string id)
    {
        return tab.ExecuteQueryForValue("select brandname from tj_crm_brandinfo where id=" + id).ToString();
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('/crm/tj_crm_brandpicturesAddEdit.aspx?cmd=" + Sc.EncryptQueryString("edit") + "&ID={0}&bid={1}',600,380,'"+Brandname+"')", Sc.EncryptQueryString(ID), Sc.EncryptQueryString(hf_bid.Value));
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
        {
            tab.ExecuteNonQuery("delete from public.tj_crm_brandpictures where id=" + dataKey["id"]);
            DisplayData(_currentindex, AspNetPager1.PageSize);
        } 
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
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex - 1) + e.Row.RowIndex + 1).ToString();
            if (IsSuperAdmin())
            {
                ((LinkButton)e.Row.Cells[4].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
            }
            else
            {
                e.Row.Cells[4].Enabled = false;
                e.Row.Cells[4].ForeColor = Color.LightGray;
            }
        }
    }
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        DisplayData(1, AspNetPager1.PageSize);
    }

    private string _filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        _filtertemp = "brandid=" + hf_bid.Value;
        AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from tj_crm_brandpictures where " + _filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = tab.ExecuteNonQuery("select * from tj_crm_brandpictures where " + _filtertemp + " limit " + pageSize + " offset " + commfrank.nonegertive((pageIndex-1))  * pageSize);
        //GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "tj_crm_brandpictures", _filtertemp, "id", "id", pageSize);
        GridView1.DataBind();
    } 
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}
