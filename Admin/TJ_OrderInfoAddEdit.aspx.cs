using System;
using System.Web;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_OrderInfoAddEdit : AuthorPage
{
    private readonly BTJ_OrderInfo bll = new BTJ_OrderInfo();
    private MTJ_OrderInfo mod = new MTJ_OrderInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
            }
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim();
            }
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    break;
                case "edit":
                    Button1.Text = "修改";
                    fillinput(int.Parse(HF_ID.Value.Trim()));
                    break;
                default:
                    break;
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.ToLower().Equals("edit"))
        {
            mod = bll.GetList(int.Parse(HF_ID.Value));
        }
        mod.CompID = int.Parse(HttpUtility.UrlDecode(Request.Cookies["TJCOMPID"].Value.Trim()));
        mod.OrderID = Convert.ToInt32(inputOrderID.Value.Trim());
        mod.GoodsID = Convert.ToInt32(inputGoodsID.Value.Trim());
        mod.UserID = Convert.ToInt32(inputUserID.Value.Trim());
        mod.OrderStatusID = Convert.ToInt32(inputOrderStatusID.Value.Trim());
        mod.OrderDate = Convert.ToDateTime(inputOrderDate.Value.Trim());
        mod.OrderNum = Convert.ToInt32(inputOrderNum.Value.Trim());
        mod.UnitPrice = Convert.ToDecimal(inputUnitPrice.Value.Trim());
        mod.ShoulPayMoney = Convert.ToDecimal(inputShoulPayMoney.Value.Trim());
        mod.Remarks = inputRemarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTJ_OrderInfo ms = bll.GetList(id);
        inputOrderID.Value = ms.OrderID.ToString().Trim();
        inputGoodsID.Value = ms.GoodsID.ToString().Trim();
        inputUserID.Value = ms.UserID.ToString().Trim();
        inputOrderStatusID.Value = ms.OrderStatusID.ToString().Trim();
        inputOrderDate.Value = ms.OrderDate.ToString().Trim();
        inputOrderNum.Value = ms.OrderNum.ToString().Trim();
        inputUnitPrice.Value = ms.UnitPrice.ToString().Trim();
        inputShoulPayMoney.Value = ms.ShoulPayMoney.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}