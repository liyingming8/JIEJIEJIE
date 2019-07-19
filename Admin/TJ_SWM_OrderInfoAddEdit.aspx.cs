using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_SWM_OrderInfoAddEdit : AuthorPage
{
    BTJ_SWM_OrderInfo bll = new BTJ_SWM_OrderInfo();
    MTJ_SWM_OrderInfo mod = new MTJ_SWM_OrderInfo();
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
        mod.id = Convert.ToInt32(inputid.Value.Trim());
        mod.ordernumber = inputordernumber.Value.Trim();
        mod.psid = Convert.ToInt32(inputpsid.Value.Trim());
        mod.orderquantity = Convert.ToInt32(inputorderquantity.Value.Trim());
        mod.orderremark = inputorderremark.Value.Trim();
        mod.totalprice = Convert.ToDecimal(inputtotalprice.Value.Trim());
        mod.ordercompid = Convert.ToInt32(inputordercompid.Value.Trim());
        mod.orderuserid = Convert.ToInt32(inputorderuserid.Value.Trim());
        mod.ordertm = Convert.ToDateTime(inputordertm.Value.Trim());
        mod.payconfirm = Convert.ToBoolean(inputpayconfirm.Value.Trim());
        mod.confirmuserid = Convert.ToInt32(inputconfirmuserid.Value.Trim());
        mod.isfahuo = Convert.ToBoolean(inputisfahuo.Value.Trim());
        mod.fahuouserid = Convert.ToInt32(inputfahuouserid.Value.Trim());
        mod.isactive = Convert.ToBoolean(inputisactive.Value.Trim());
        mod.ordercompnm = inputordercompnm.Value.Trim();
        mod.orderusernm = inputorderusernm.Value.Trim();
        mod.orderphonenm = inputorderphonenm.Value.Trim();
        mod.paytype = inputpaytype.Value.Trim();
        mod.ispay = Convert.ToBoolean(inputispay.Value.Trim());
        mod.remarks = inputremarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_SWM_OrderInfoAddEdit.aspx","TJ_SWM_OrderInfo","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_SWM_OrderInfoAddEdit.aspx","TJ_SWM_OrderInfo","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTJ_SWM_OrderInfo ms = bll.GetList(id);
        inputid.Value = ms.id.ToString().Trim();
        inputordernumber.Value = ms.ordernumber.Trim();
        inputpsid.Value = ms.psid.ToString().Trim();
        inputorderquantity.Value = ms.orderquantity.ToString().Trim();
        inputorderremark.Value = ms.orderremark.Trim();
        inputtotalprice.Value = ms.totalprice.ToString().Trim();
        inputordercompid.Value = ms.ordercompid.ToString().Trim();
        inputorderuserid.Value = ms.orderuserid.ToString().Trim();
        inputordertm.Value = ms.ordertm.ToString().Trim();
        inputpayconfirm.Value = ms.payconfirm.ToString().Trim();
        inputconfirmuserid.Value = ms.confirmuserid.ToString().Trim();
        inputisfahuo.Value = ms.isfahuo.ToString().Trim();
        inputfahuouserid.Value = ms.fahuouserid.ToString().Trim();
        inputisactive.Value = ms.isactive.ToString().Trim();
        inputordercompnm.Value = ms.ordercompnm.Trim();
        inputorderusernm.Value = ms.orderusernm.Trim();
        inputorderphonenm.Value = ms.orderphonenm.Trim();
        inputpaytype.Value = ms.paytype.Trim();
        inputispay.Value = ms.ispay.ToString().Trim();
        inputremarks.Value = ms.remarks.Trim();
    }
}