using System;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class msite_fajianxx : MAuthorPage
{
    private readonly BTJ_OrderInfo border = new BTJ_OrderInfo();
    private MTJ_OrderInfo morder = new MTJ_OrderInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["orderid"] != null && Request.QueryString["orderid"] != "")
            {
                HF_OrderID.Value = Request.QueryString["orderid"];
            }
            else
            {
                Response.End();
            }
        }
    }

    protected void Button_OK_Click(object sender, EventArgs e)
    {
        morder = border.GetList(int.Parse(HF_OrderID.Value));
        morder.WuLiuCompName = DDL_WuLiuGongSi.SelectedValue;
        morder.WuLiuDanHao = TextBox_YunDanHao.Text;
        border.Modify(morder);
        Response.Redirect("OrderInfoAccepted.aspx");
    }
}