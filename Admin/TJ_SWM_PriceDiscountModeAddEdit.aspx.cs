using System; 
using System.Web.UI; 
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_SWM_PriceDiscountModeAddEdit : AuthorPage
{
    BTJ_SWM_PriceDiscountMode bll = new BTJ_SWM_PriceDiscountMode();
    MTJ_SWM_PriceDiscountMode mod = new MTJ_SWM_PriceDiscountMode();
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
        mod.shuliang = Convert.ToInt32(inputshuliang.Value.Trim());
        mod.discount = Convert.ToDecimal(inputdiscount.Value.Trim());
        mod.jieshao = inputjieshao.Value.Trim();
        mod.remarks = inputremarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_SWM_PriceDiscountModeAddEdit.aspx","TJ_SWM_PriceDiscountMode","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_SWM_PriceDiscountModeAddEdit.aspx","TJ_SWM_PriceDiscountMode","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTJ_SWM_PriceDiscountMode ms = bll.GetList(id); 
        inputshuliang.Value = ms.shuliang.ToString().Trim();
        inputdiscount.Value = ms.discount.ToString().Trim();
        inputjieshao.Value = ms.jieshao.Trim();
        inputremarks.Value = ms.remarks.Trim();
    }
}