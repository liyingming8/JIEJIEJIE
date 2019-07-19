using System;
using System.Collections.Generic;
using commonlib;
using TJ.Model;
using TJ.BLL;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class msite_OrderInfoAcceptIng : MAuthorPage
{
    private readonly BTJ_OrderInfo borderinfo = new BTJ_OrderInfo();
    private readonly CommonFun comfun = new CommonFun();
    private readonly BTJ_GoodsInfo bgoodinfo = new BTJ_GoodsInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetOrderInfo();
        }
    }

    private IList<MTJ_OrderInfo> orderlist;
    private string authorgoodsidstring = "";
    private readonly StringBuilder sb = new StringBuilder();
    private MTJ_GoodsInfo mgood = new MTJ_GoodsInfo();
    private string name = string.Empty;
    private string phone = string.Empty;
    private string address = string.Empty;

    private void GetOrderInfo()
    {
        authorgoodsidstring = comfun.GetSaleAuthoredGoodsIDForSubComps(GetCookieCompID());
        if (authorgoodsidstring.Length > 0)
        {
            orderlist =
                borderinfo.GetListsByFilterString(
                    "OrderStatusID=0  and (DeliveryCompID is not null and DeliveryCompID<>0 and WuLiuCompName='' and WuLiuDanHao='') and (CompID=" +
                    GetCookieCompID() + " or GoodsID in(" + authorgoodsidstring + "))");
        }
        else
        {
            orderlist =
                borderinfo.GetListsByFilterString(
                    "OrderStatusID=0 and (DeliveryCompID is not null and DeliveryCompID<>0 and WuLiuCompName='' and WuLiuDanHao='') and CompID=" +
                    GetCookieCompID());
        }
        foreach (MTJ_OrderInfo morder in orderlist)
        {
            mgood = bgoodinfo.GetList(morder.GoodsID);
            mgood = bgoodinfo.GetList(morder.GoodsID);
            string[] m = morder.Remarks.Split(',');
            name = m[0];
            phone = m[1];
            address = m[2];
            sb.Append("        <div class=\"row orderitem\">");
            sb.Append("             <div class=\"col-xs-3 prodpic\"><img src=\"../Admin/" + mgood.GoodsPicURLSmal +
                      "\" /></div>");
            sb.Append("             <div class=\"col-xs-5 goodsname\"><div>" + mgood.GoodsName + "</div><div>数量：" +
                      morder.OrderNum + ";" + mgood.SaleUnitID + "</div></div>");
            sb.Append("             <div class=\"col-xs-4\"><a href=\"fajianxx.aspx?orderid=" + morder.OrderID +
                      "\"  class=\"ButtonOK\">发货信息</a><br /><br /><a href=\"JieDan.aspx?orderid=" + morder.OrderID +
                      "&cmd=cancel\" >我要取消</a></div>");
            sb.Append("             <div class=\"col-xs-12\">收货人:" + name + " 联系电话:" + phone + "</div>");
            sb.Append("             <div class=\"col-xs-12\">收货地址:" + address + "</div>");
            sb.Append("         </div>");
        }
        phorderitem.Controls.Add(new LiteralControl(sb.ToString()));
    }

    protected void ButtonOK_Click(object sender, EventArgs e)
    {
        string orderid = (sender as Button).CommandArgument;
        MTJ_OrderInfo morder = borderinfo.GetList(int.Parse(orderid));
    }
}