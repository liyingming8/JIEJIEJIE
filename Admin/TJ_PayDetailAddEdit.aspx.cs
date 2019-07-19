using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_PayDetailAddEdit : AuthorPage
{
    private readonly BTJ_PayDetail bll = new BTJ_PayDetail();
    private MTJ_PayDetail mod = new MTJ_PayDetail();

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
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.ID = Convert.ToInt32(inputID.Value.Trim());
        mod.CompID = Convert.ToInt32(inputCompID.Value.Trim());
        mod.OrderIDString = inputOrderIDString.Value.Trim();
        mod.PayOrderNo = inputPayOrderNo.Value.Trim();
        mod.InnerOrderNo = inputInnerOrderNo.Value.Trim();
        mod.ProductType = inputProductType.Value.Trim();
        mod.PayMethod = inputPayMethod.Value.Trim();
        mod.PayMoney = inputPayMoney.Value.Trim();
        mod.Statues = inputStatues.Value.Trim();
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
        MTJ_PayDetail ms = bll.GetList(id);
        inputID.Value = ms.ID.ToString().Trim();
        inputCompID.Value = ms.CompID.ToString().Trim();
        inputOrderIDString.Value = ms.OrderIDString.Trim();
        inputPayOrderNo.Value = ms.PayOrderNo.Trim();
        inputInnerOrderNo.Value = ms.InnerOrderNo.Trim();
        inputProductType.Value = ms.ProductType.Trim();
        inputPayMethod.Value = ms.PayMethod.Trim();
        inputPayMoney.Value = ms.PayMoney.Trim();
        inputStatues.Value = ms.Statues.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}