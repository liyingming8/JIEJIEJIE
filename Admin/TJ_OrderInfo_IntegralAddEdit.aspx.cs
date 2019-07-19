using System; 
using System.Web.UI; 
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_OrderInfo_IntegralAddEdit : AuthorPage
{
    BTJ_OrderInfo_Integral bll = new BTJ_OrderInfo_Integral();
    MTJ_OrderInfo_Integral mod = new MTJ_OrderInfo_Integral();
    BTJ_AwardInfo btjAward = new BTJ_AwardInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
       {
          if (!string.IsNullOrEmpty(Request.QueryString["cmd"]))
          {
             HF_CMD.Value = Sc.DecryptQueryString(Request.QueryString["cmd"].Trim());
          }
          if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
          {
             HF_ID.Value = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
          }
           inputDeliveryComfirmDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
           FillDDL();
          switch (HF_CMD.Value)
          {
             case "add":
                Button1.Text = "添加";
                break;
             case "edit":
                Button1.Text = "确定";
                Fillinput(int.Parse(HF_ID.Value.Trim()));
                break;
             default:
                break;
          }
       }
    }

    BTJ_LogisticsCompany btjLogistics = new BTJ_LogisticsCompany();
    private void FillDDL()
    {
        DDL_DeliveryCompID.DataSource = btjLogistics.GetLists("logisticcompany");
        DDL_DeliveryCompID.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
           mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        if (string.IsNullOrEmpty(DDL_DeliveryCompID.SelectedValue) || DDL_DeliveryCompID.SelectedValue.Equals("0"))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('请指定快递公司信息');", true);
        }
        else
        {
            if (string.IsNullOrEmpty(inputWuLiuDanHao.Value))
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('请输入快递单号');", true);
            }
            else
            {
                mod.DeliveryCompID = Convert.ToInt32(DDL_DeliveryCompID.SelectedValue);
                mod.DeliveryComfirmDate = Convert.ToDateTime(inputDeliveryComfirmDate.Value.Trim());
                mod.DeliveryUserID = Convert.ToInt32(GetCookieUID());
                mod.WuLiuDanHao = inputWuLiuDanHao.Value.Trim();
                mod.YunFei = Convert.ToDecimal(inputYunFei.Value.Trim());
                mod.WuLiuCompName = DDL_DeliveryCompID.SelectedItem.Text;
                mod.OrderStatusID = 2;
                mod.DeliveryUserNM = GetCookieTJUName();
                switch (HF_CMD.Value.Trim())
                {
                    case "add":
                        bll.Insert(mod);
                        RecordDealLog(new MTJ_DealLog(0, "TJ_OrderInfo_IntegralAddEdit.aspx", "TJ_OrderInfo_Integral", "描述", System.DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                        break;
                    case "edit":
                        bll.Modify(mod);
                        RecordDealLog(new MTJ_DealLog(0, "TJ_OrderInfo_IntegralAddEdit.aspx", "TJ_OrderInfo_Integral", "描述", System.DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                        break;
                }
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "reload", "closemyWindow();", true);
            } 
        } 
}

    private void Fillinput(int id)
    {
        MTJ_OrderInfo_Integral ms = bll.GetList(id); 
        LabelOrderNumber.Text = ms.OrderNumber.Trim();
        inputGoodsID.Text = btjAward.GetList(ms.GoodsID).AwardThing;
        inputOrderDate.Text = ms.OrderDate.ToString().Trim();
        inputOrderNum.Text = ms.OrderNum.ToString().Trim();
        inputRemarks.Text = ms.Remarks.Trim();
        inputCustomerName.Text = ms.CustomerName.Trim();
        inputCustomerPhone.Text = ms.CustomerPhone.Trim();
        DDL_DeliveryCompID.SelectedValue = ms.DeliveryCompID.ToString().Trim();
        if (!ms.DeliveryComfirmDate.ToString("yyyy-MM-dd").Trim().Equals("1900-01-01"))
        {
            inputDeliveryComfirmDate.Value = ms.DeliveryComfirmDate.ToString("yyyy-MM-dd").Trim();  
        } 
        inputWuLiuDanHao.Value = ms.WuLiuDanHao.Trim();
        inputYunFei.Value = ms.YunFei.ToString().Trim();
        inputAddress.Text = ms.Address;
    }
}