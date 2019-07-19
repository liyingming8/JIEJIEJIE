using System;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class msite_JieDan : MAuthorPage
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
            if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"] != "")
            {
                HF_Cmd.Value = Request.QueryString["cmd"];
                if (HF_Cmd.Value.Trim().ToLower().Equals("cancel"))
                {
                    labelalert.Text = "您确认取消发货吗？";
                    Button_OK.Text = "确定";
                }
            }
        }
    }

    protected void Button_OK_Click(object sender, EventArgs e)
    {
        morder = border.GetList(int.Parse(HF_OrderID.Value));
        if (HF_Cmd.Value.Trim().ToLower().Equals("cancel"))
        {
            morder.DeliveryCompID = 0;
            morder.DeliveryUserID = 0;
            morder.DeliveryComfirmDate = DateTime.Parse("1900-01-01");
        }
        else
        {
            morder.DeliveryCompID = int.Parse(GetCookieCompID());
            morder.DeliveryUserID = int.Parse(GetCookieUID());
            morder.DeliveryComfirmDate = DateTime.Now;
        }

        border.Modify(morder);
        Response.Redirect("OrderInfoAcceptIng.aspx");
    }
}