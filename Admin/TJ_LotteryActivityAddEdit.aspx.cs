using System;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_LotteryActivityAddEdit : AuthorPage
{
    private readonly BTJ_LotteryActivity bll = new BTJ_LotteryActivity();
    private MTJ_LotteryActivity mod = new MTJ_LotteryActivity();
    private readonly BTB_Products_Infor bproduct = new BTB_Products_Infor();
    private readonly CommonFunWL comfunwl = new CommonFunWL();

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
            string tempauthorprididstring = comfunwl.GetAuthorProdIDString(GetCookieCompID());
            if (tempauthorprididstring.Length > 0)
            {
                CheckBoxList_Product.DataSource =
                    bproduct.GetListsByFilterString("CompID=" + GetCookieCompID() + " or CompID in(" +
                                                    tempauthorprididstring + ")");
            }
            else
            {
                CheckBoxList_Product.DataSource = bproduct.GetListsByFilterString("CompID=" + GetCookieCompID());
            }
            CheckBoxList_Product.DataBind();
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
        mod.CompID = Convert.ToInt32(GetCookieCompID());
        mod.LotteryActivityName = inputGoodsInfo.Value.Trim();
        mod.PublishstartDate = Convert.ToDateTime(inputPublishstartDate.Text.Trim());
        mod.UserID = Convert.ToInt32(GetCookieUID());
        mod.Remarks = inputRemarks.Value.Trim();
        mod.PublishendDate = Convert.ToDateTime(inputPublishendDate.Text.Trim());
        mod.ProductIDString = GetProductIDString();

        //判断日期的大小，结束日期应该大于开始日期===WYZ-20170921
        if (DateTime.Compare(mod.PublishstartDate, mod.PublishendDate) >= 0) //判断日期大小
        {
            Response.Write("<script>alert('开始日期必须小于结束日期！');</script>");
            return;
        }

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

    private string ProductIDString = "";

    private string GetProductIDString()
    {
        ProductIDString = "";
        foreach (ListItem item in CheckBoxList_Product.Items)
        {
            ProductIDString += (item.Selected ? "," + item.Value : "");
        }
        return ProductIDString.StartsWith(",") ? ProductIDString.Substring(1) : ProductIDString;
    }

    private void fillinput(int id)
    {
        MTJ_LotteryActivity ms = bll.GetList(id);
        inputGoodsInfo.Value = ms.LotteryActivityName.Trim();
        inputPublishstartDate.Text = ms.PublishstartDate.ToString("yyyy-MM-dd");
        inputRemarks.Value = ms.Remarks.Trim();
        inputPublishendDate.Text = ms.PublishendDate.ToString("yyyy-MM-dd");
        ProductIDString = ms.ProductIDString;
        if (ProductIDString.Length > 0)
        {
            ProductIDString = "," + ProductIDString + ",";
            foreach (ListItem item in CheckBoxList_Product.Items)
            {
                if (ProductIDString.Contains("," + item.Value + ","))
                {
                    item.Selected = true;
                }
            }
        }
    }
}