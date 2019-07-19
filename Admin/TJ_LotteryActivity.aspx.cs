using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using System.Collections.Generic;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_TJ_LotteryActivity : AuthorPage
{
    private readonly BTJ_LotteryActivity bll = new BTJ_LotteryActivity();
    private MTJ_LotteryActivity mod = new MTJ_LotteryActivity();
    private readonly BTJ_JXInfo bjx = new BTJ_JXInfo();
    public BTJ_User buser = new BTJ_User();
    private readonly BTB_Products_Infor bproduct = new BTB_Products_Infor();
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string comp = GetCookieCompID();
            if (comp == "1")
            {
                _currentindex = 1;
                DisplayData(_currentindex, AspNetPager1.PageSize);
            }
            else
            {
                fillgridview();
            }
        }
    }
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_LotteryActivityAddEdit.aspx?cmd=edit&ID={0}',600,500,'有奖活动管理')", ID);
        }
        else
        {
            return "";
        }
    }


    private string tempproductnamestring = "";

    public string GetProductName(string ProductIDString)
    {
        if (ProductIDString.Trim().Length == 0)
        {
            return "不限";
        }
        else
        {
            tempproductnamestring = "";
            //---wyz---20160720---xiugai
            if (!ProductIDString.Equals(""))
            {
                string strHouDouHao = ProductIDString.Substring(ProductIDString.Length - 1, 1);
                if (strHouDouHao.Equals(","))
                {
                    ProductIDString = ProductIDString.Substring(0, ProductIDString.Length - 1);
                }
            }
            //---wyz---20160720---xiugai
            IList<MTB_Products_Infor> productlist =
                bproduct.GetListsByFilterString("Infor_ID in (" + ProductIDString + ")");
            if (productlist.Count > 0)
            {
                foreach (MTB_Products_Infor mp in productlist)
                {
                    tempproductnamestring += "," + mp.Products_Name;
                }
            }
            return tempproductnamestring.StartsWith(",") ? tempproductnamestring.Substring(1) : tempproductnamestring;
        }
    }

    private void fillgridview()
    {
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            GridView1.DataSource =
                bll.GetListsByFilterString("CompID=" + GetCookieCompID() + " and " + DDLField.SelectedValue + " like '%" +
                                           inputSearchKeyword.Value.Trim() + "%'");
        }
        else
        {
            GridView1.DataSource = bll.GetListsByFilterString("CompID=" + GetCookieCompID());
        }
        GridView1.DataBind();
    }
    private const string Filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_LotteryActivity where " + Filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_LotteryActivity", Filtertemp, "LAID", "LAID", pageSize);
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["LAID"].ToString()));
        fillgridview();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgridview();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgridview();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        mod = bll.GetList(Convert.ToInt32(GridView1.DataKeys[e.RowIndex]["LAID"].ToString()));
        mod.LotteryActivityName =
            ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtLotteryActivityName")).Text.Trim();
        mod.Remarks = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtRemarks")).Text.Trim();
        bll.Modify(mod);
        GridView1.EditIndex = -1;
        fillgridview();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillgridview();
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
            var key = GridView1.DataKeys[e.Row.RowIndex];
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                if (CheckIsUsed(GridView1.DataKeys[e.Row.RowIndex]["LAID"].ToString()))
                {
                    e.Row.Cells[6].ForeColor = Color.LightGray;
                    e.Row.Cells[6].Enabled = false;
                }
                else
                {
                    ((LinkButton) e.Row.Cells[6].Controls[0]).Attributes.Add("onclick",
                        "javascript:return confirm('你确定要删除当前记录吗?')");
                }
            }
        }
    }

    private bool CheckIsUsed(string LAID)
    {
        return bjx.CheckIsExistByFilterString("LAID=" + LAID);
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview();
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}