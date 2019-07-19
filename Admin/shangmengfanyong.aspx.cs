using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_shangmengfanyong : AuthorPage
{
    private readonly CommonFun confun = new CommonFun();
    private readonly BTJ_OrderInfo border = new BTJ_OrderInfo();
    private readonly BTJ_User buser = new BTJ_User();
    private readonly BTJ_GoodsInfo bgoodinfo = new BTJ_GoodsInfo();
    private readonly BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBox_StartDate.Text = DateTime.Now.ToString("yyyy-MM-") + "01";
            TextBox_EndDate.Text =
                Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-") + "01")
                    .AddDays(-1)
                    .ToString("yyyy-MM-dd");
            FillDLL();
        }
    }

    private void FillDLL()
    {
        confun.BindTreeCombox(ComboBox_ShangMeng, "CompName", "CompID", "ParentID", "TJ_RegisterCompanys", 0, "选择商家",
            true, "-", "ParentID=" + GetCookieCompID() + " or CompID=" + GetCookieCompID());
        ComboBox_ShangMeng.SelectedValue = "0";
    }

    public string ReturnOrderStatus(string StatueID)
    {
        return XmlOperation.GetItemNameFromXml("订单状态", "include/ConfigData.xml", Page, StatueID);
    }

    protected void Button_query_Click(object sender, EventArgs e)
    {
        QueryFanYong(ComboBox_ShangMeng.SelectedValue);
    }

    private void QueryFanYong(string ShangMengID)
    {
        if (ShangMengID.Trim().Trim().Length > 0 && ShangMengID != "0")
        {
            GridView1.DataSource =
                border.GetListsByFilterString(
                    "OrderDate between '" + TextBox_StartDate.Text + "' and '" + TextBox_EndDate.Text +
                    "' and IntroCompID=" + ComboBox_ShangMeng.SelectedValue, "OrderDate desc");
        }
        else
        {
            GridView1.DataSource =
                border.GetListsByFilterString(
                    "IntroCompID>0 and OrderDate between '" + TextBox_StartDate.Text + "' and '" + TextBox_EndDate.Text +
                    "'", "OrderDate desc");
        }
        GridView1.DataBind();
    }

    protected void ComboBox_ShangMeng_ComboBoxChanged(object sender, EventArgs e)
    {
        QueryFanYong(ComboBox_ShangMeng.SelectedValue);
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
        return "[" + mgoodinfo.GoodsName + "]" + mgoodinfo.Descriptions;
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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        QueryFanYong(ComboBox_ShangMeng.SelectedValue);
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.PageIndex = -1;
        QueryFanYong(ComboBox_ShangMeng.SelectedValue);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MTJ_User muser = buser.GetList(Convert.ToInt32(GridView1.DataKeys[e.Row.RowIndex]["UserID"].ToString()));
            ((Label) e.Row.FindControl("LabelUserID")).Text = muser.NickName.Equals("")
                ? muser.LoginName
                : muser.NickName;
            //((Label)e.Row.FindControl("LabelUserAddress")).Text = muser.AddressInfo;
            //((Label)e.Row.FindControl("LabelPostStamp")).Text = muser.PostCode;
            MTJ_GoodsInfo mgoodsinfo =
                bgoodinfo.GetList(int.Parse(GridView1.DataKeys[e.Row.RowIndex]["GoodsID"].ToString()));
            PlaceHolder phgoods = (PlaceHolder) e.Row.FindControl("PH_GoodsInfo");
            phgoods.Controls.Add(new LiteralControl("[" + mgoodsinfo.GoodsName + "]" + "</br>" + mgoodsinfo.Descriptions));
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

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
    }
}