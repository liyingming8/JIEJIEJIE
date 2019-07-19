using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using Wuqi.Webdiyer;
using Image = System.Web.UI.WebControls.Image;
using TJ.DBUtility;

public partial class Admin_TJ_OrderInfo : AuthorPage
{
    private readonly BTJ_OrderInfo bll = new BTJ_OrderInfo();
    private MTJ_OrderInfo mod = new MTJ_OrderInfo();
    public BTJ_User buser = new BTJ_User();
    private readonly BTJ_GoodsInfo bgoodinfo = new BTJ_GoodsInfo();
    public BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            _currentindex = 1;
            AspNetPager1.CurrentPageIndex = _currentindex;
            DisplayData(1, AspNetPager1.PageSize);

        }
    }
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_OrderInfoAddEdit.aspx?cmd=edit&ID={0}',600,500,'订单管理')", ID);
        }
        else
        {
            return "";
        }
    }

    //private void fillgridview()
    //{
    //    GridView1.DataSource = bll.GetListsByFilterString("CompID=" + GetCookieCompID(), "OrderDate  desc");
    //    GridView1.DataBind();
    //}
    //private const string Filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        string comp = GetCookieCompID();
        if (comp == "1")
        {
            string Filtertemp = "1=1";
            // string Filtertemp = "CompID =1";
            //if (inputSearchKeyword.Value.Trim().Length > 0)
            //{

            //    Filtertemp = " " + DDLField.SelectedValue +
            //                                     " like '%" + inputSearchKeyword.Value.Trim() + "%'";
            //    //Filtertemp = "CompID = 1 and " + DDLField.SelectedValue +
            //    //                              " like '%" + inputSearchKeyword.Value.Trim() + "%'";//可以查询全部的
            //}

            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(OrderID) from TJ_OrderInfo where " + Filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_OrderInfo", Filtertemp, "OrderID", "OrderID", pageSize);
            GridView1.DataBind();
        }
        else
        {
            string Filtertemp = "CompID=" + comp;
            //if (inputSearchKeyword.Value.Trim().Length > 0)
            //{

            //    Filtertemp = "CompID =" + GetCookieCompID() + " and " + DDLField.SelectedValue +
            //                                 " like '%" + inputSearchKeyword.Value.Trim() + "%'";
            //    //GridView1.DataSource =
            //    //    bll.GetListsByFilterString("CompID =" + GetCookieCompID() + " and " + DDLField.SelectedValue +
            //    //                               " like '%" + inputSearchKeyword.Value.Trim() + "%'");
            //}
            //else
            //{
            //    GridView1.DataSource = bll.GetListsByFilterString("CompID =" + GetCookieCompID());
            //}
            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(OrderID) from TJ_OrderInfo where " + Filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_OrderInfo", Filtertemp, "OrderID", "OrderID", pageSize);
            GridView1.DataBind();
        }
        //AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_OrderInfo where " + Filtertemp, null).Rows[0][0].ToString());
        //GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_OrderInfo", Filtertemp, "OrderID", "OrderID", pageSize);
        //GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["OrderID"].ToString()));
        //  fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        //  fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        //fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    public string ReturnOrderStatus(string StatueID)
    {
        return XmlOperation.GetItemNameFromXml("订单状态", "../include/ConfigData.xml", Page, StatueID);
    }

    public string ReturnOrderUser(string UserID)
    {
        MTJ_User muser = buser.GetList(int.Parse(UserID));
        return muser.NickName + muser.LoginName;
    }

    public string ReturnGoodPicURL(string GoodsID)
    {
        return bgoodinfo.GetList(int.Parse(GoodsID)).GoodsPicURL;
    }

    public string ReturnGoodsInfo(string GoodsID)
    {
        MTJ_GoodsInfo mgoodinfo = bgoodinfo.GetList(int.Parse(GoodsID));
        return "[" + mgoodinfo.GoodsName + "]";
    } 
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        mod = bll.GetList(Convert.ToInt32(GridView1.DataKeys[e.RowIndex]["OrderID"].ToString()));
        // mod.AdditionMoney = Convert.ToDecimal(((TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox_AdditionMoney")).Text.Trim());
        mod.Remarks = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("TextBox_Remarks")).Text.Trim();
        mod.FinalTotalMoney = mod.ShoulPayMoney + mod.AdditionMoney;
        if (mod.IntroCompID > 0)
        {
            mod.ActualReturnMoney = ReturnActualPayMoney(mod.OrderNum*mod.UnitPrice, mod.DisCount/10);
        }
        else
        {
            mod.ActualReturnMoney = 0;
        }
        bll.Modify(mod);
        GridView1.EditIndex = -1;
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#CCFF83'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
            var key = GridView1.DataKeys[e.Row.RowIndex];
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                if (PermitDelete(GridView1.DataKeys[e.Row.RowIndex]["OrderID"].ToString()))
                {
                    ((LinkButton) e.Row.Cells[13].Controls[0]).Attributes.Add("onclick",
                        "javascript:return confirm('你确定要删除当前记录吗?')");
                }
                else
                {
                    e.Row.Cells[13].Enabled = false;
                    e.Row.Cells[13].ForeColor = Color.LightGray;
                }
                MTJ_User muser = buser.GetList(Convert.ToInt32(GridView1.DataKeys[e.Row.RowIndex]["UserID"].ToString()));
                ((Label) e.Row.FindControl("LabelUserID")).Text = muser.NickName.Equals("")
                    ? muser.LoginName
                    : muser.NickName;
                ((Image) e.Row.FindControl("headerlogo")).ImageUrl = muser.HeaderImageUrl;
                //((Label)e.Row.FindControl("LabelUserAddress")).Text = muser.AddressInfo;
                //((Label)e.Row.FindControl("LabelPostStamp")).Text = muser.PostCode;
                MTJ_GoodsInfo mgoodsinfo =
                    bgoodinfo.GetList(int.Parse(GridView1.DataKeys[e.Row.RowIndex]["GoodsID"].ToString()));
                PlaceHolder phgoods = (PlaceHolder) e.Row.FindControl("PH_GoodsInfo");
                phgoods.Controls.Add(new LiteralControl(mgoodsinfo.GoodsName));
                Label LabelOrderStatusID = (Label) e.Row.FindControl("LabelOrderStatusID");
                if (LabelOrderStatusID.Text.Equals("未支付"))
                {
                    LabelOrderStatusID.ForeColor = Color.Red;
                }
                else
                {
                    LabelOrderStatusID.ForeColor = Color.DarkGreen;
                }
            }
        }
    }

    private bool PermitDelete(string ORID)
    {
        if (bll.GetList(int.Parse(ORID)).OrderStatusID != 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public string ReturnMengyouName(string MYID)
    {
        if (MYID != "0" && MYID != "")
        {
            return bcompany.GetList(int.Parse(MYID)).CompName;
        }
        else
        {
            return "---";
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        //if (inputSearchKeyword.Value.Trim().Length > 0)
        //{
        //   // GridView1.DataSource = bll.GetListsByFilterString(DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'");
        //}
        //else
        //{
        //    GridView1.DataSource = bll.GetLists();
        //}
        //GridView1.DataBind();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        //fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("YFH") || e.CommandName.Equals("YWC"))
        {
            MTJ_OrderInfo morder = new MTJ_OrderInfo();
            switch (e.CommandName)
            {
                case "YFH":
                    morder = bll.GetList(int.Parse(e.CommandArgument.ToString()));
                    morder.OrderStatusID = 2;
                    break;
                case "YWC":
                    morder = bll.GetList(int.Parse(e.CommandArgument.ToString()));
                    morder.OrderStatusID = 3;
                    break;
                default:
                    break;
            }
            bll.Modify(morder);
            Response.Write("<script>alert('操作成功！');</script>");
            // fillgridview();
            _currentindex = 1;
            AspNetPager1.CurrentPageIndex = _currentindex;
            DisplayData(1, AspNetPager1.PageSize);

        }
    }

    private decimal ReturnShuldPayMoney(decimal TotalPrice, decimal discount)
    {
        return TotalPrice*discount;
    }

    private decimal ReturnActualPayMoney(decimal TotalPrice, decimal discount)
    {
        return TotalPrice*discount - TotalPrice*decimal.Parse("0.6");
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}