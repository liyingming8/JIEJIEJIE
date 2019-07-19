using System;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using TJ.Model;
using commonlib;
public partial class Admin_TJ_Activity_Product : AuthorPage
{
    BTJ_Activity_Product bll = new BTJ_Activity_Product(); 
    TabExecute tab = new TabExecute();
    DBClass db = new DBClass();
    private int _currentindex = 1; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["acid"]))
            {
                hd_acid.Value = Sc.DecryptQueryString(Request.QueryString["acid"].Trim());
                hd_acnm.Value = Sc.DecryptQueryString(Request.QueryString["acnm"].Trim());
            }
            else
            {
                Response.End();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["proid"]))
            {
                string prodid = Request.QueryString["proid"];
                string prodname = Request.QueryString["proname"];
                if (!bll.CheckIsExistByFilterString("acid=" + hd_acid.Value + " and prodid=" + prodid))
                {
                    bll.Insert(new MTJ_Activity_Product(0, int.Parse(hd_acid.Value), int.Parse(prodid),prodname));
                    db.Updateactivitynum(hd_acid.Value, "pd");
                }
            }
             _currentindex = 1;
             AspNetPager1.CurrentPageIndex = 1;
             DisplayData(1, AspNetPager1.PageSize);
             add.Attributes.Add("onclick", XiangXiLinkStringForProduct());
        }
    } 
    
    public string XiangXiLinkStringForProduct()
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('../Admin/wuliu/TB_Products_Infor_forsingleselect.aspx?fr=" + Sc.EncryptQueryString("/Admin/TJ_Activity_Product.aspx?acid=" + Sc.EncryptQueryString(hd_acid.Value) + "&acnm="+Sc.EncryptQueryString(hd_acnm.Value)) + "',580,460,'指定产品')");
        }
        return "";
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_Activity_ProductAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}',600,450,'TJ_Activity_Product')", Sc.EncryptQueryString(ID));
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
            bll.Delete(int.Parse(dataKey["id"].ToString()));
            db.Updateactivitynum(hd_acid.Value, "pd");
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
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex-1) + e.Row.RowIndex + 1).ToString();
            ((LinkButton)e.Row.Cells[3].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除当前记录吗?')");
        }
    }
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
       _currentindex = 1;
       AspNetPager1.CurrentPageIndex = 1;
       DisplayData(1, AspNetPager1.PageSize);
    }
     
    private void DisplayData(int pageIndex, int pageSize)
    {
        string filtertemp = "acid="+hd_acid.Value;
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
        }
        AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from TJ_Activity_Product where "+filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_Activity_Product", filtertemp, "id", "id", pageSize);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }
}
