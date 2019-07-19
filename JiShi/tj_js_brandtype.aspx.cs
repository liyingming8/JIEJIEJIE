using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using commonlib;
using Wuqi.Webdiyer;
using System.Web.UI.HtmlControls;

public partial class Admin_tj_js_brandtype : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM(); 
    private int _currentindex = 1; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             _currentindex = 1;
             AspNetPager1.CurrentPageIndex = 1;
             DisplayData(1, AspNetPager1.PageSize);
        }
    }

    public string ReturnBrandTypeName(string id)
    {
        if (id.Equals("0") || string.IsNullOrEmpty(id))
        {
            return "——";
        }
        else
        {
            return tab.ExecuteQueryForValue("select typename from public.tj_js_brandtype where id=" + id).ToString();
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('tj_js_brandtypeAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}',480,320,'品牌类型')", Sc.EncryptQueryString(ID));
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
            tab.ExecuteQuery("delete from tj_js_brandtype where id=" + dataKey["id"], null);
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
            {
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
                ((HtmlImage)e.Row.FindControl("editimg")).Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString()));
                ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex - 1) + e.Row.RowIndex + 1).ToString();
                /*
                if (IsSuperAdmin() && checkpermitdel(dataKey["id"].ToString()) && !dataKey["parentid"].ToString().Equals("0"))
                {
                    ((LinkButton)e.Row.Cells[3].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
                }
                else
                {
                    e.Row.Cells[3].Enabled = false;
                    e.Row.Cells[3].ForeColor = Color.LightGray;
                }
                */
            } 
        }
    }

    private bool checkpermitdel(string id)
    {
        object vl = tab.ExecuteQueryForValue("select count(id) from public.tj_crm_brandinfo where btypeid=" + id);
        int outvl = 0;
        int.TryParse(vl.ToString(), out outvl);
        if (outvl > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
       _currentindex = 1;
       AspNetPager1.CurrentPageIndex = 1;
       DisplayData(1, AspNetPager1.PageSize);
    }

    private string _filtertemp ="1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        if (inputSearchKeyword.Value.Trim().Length > 0)
       {
         _filtertemp = DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
       }
       else
       {
           _filtertemp = "1=1";
       }
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from tj_js_brandtype where "+_filtertemp, null).Rows[0][0].ToString());
      GridView1.DataSource = tab.ExecuteQuery("select * from tj_js_brandtype where " +_filtertemp+" limit "+pageSize+" offset "+commfrank.nonegertive((pageIndex-1))*pageSize,null);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }
}
