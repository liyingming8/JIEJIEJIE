using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_SaleActivitysAddEdit : AuthorPage
{
    private readonly BTJ_SaleActivitys bll = new BTJ_SaleActivitys();
    private BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();
    private MTJ_SaleActivitys mod = new MTJ_SaleActivitys();

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
        try
        {
            if (checkinput())
            {
                if (HF_CMD.Value.ToLower().Trim().Equals("edit"))
                {
                    mod = bll.GetList(int.Parse(HF_ID.Value));
                }
                mod.CompID = int.Parse(GetCookieCompID());
                string tempgoodidstring = "";
                foreach (ListItem item in CHKList_Goods.Items)
                {
                    if (item.Selected)
                    {
                        tempgoodidstring += "," + item.Value;
                    }
                }
                mod.GoodIDString = tempgoodidstring.Substring(1);
                mod.Introductions = inputIntroductions.Value.Trim();
                mod.StartDate = Convert.ToDateTime(TextBoxStartDate.Text.Trim());
                mod.EndDate = Convert.ToDateTime(TextBox_EndDate.Text.Trim());
                mod.Discount = Convert.ToDecimal(inputDiscount.Value.Trim());
                mod.Remarks = inputRemarks.Value.Trim();
                //判断日期的大小，结束日期应该大于开始日期===WYZ-20170920
                if (DateTime.Compare(mod.StartDate, mod.EndDate) >= 0) //判断日期大小
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('开始日期必须小于结束日期！');", true);
                    return;
                }
                switch (HF_CMD.Value.Trim())
                {
                    case "add":
                        mod.PublishDate = DateTime.Now;
                        bll.Insert(mod);
                        break;
                    case "edit":
                        bll.Modify(mod);
                        break;
                }
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
            } 
        }
        catch
        {
            Response.Write("<script>alert('请确保您输入的格式正确！');</script>");
        }
    }

    private bool checkinput()
    {
        if(Convert.ToDecimal(inputDiscount.Value)>=1)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('折扣不能大于 1 ！');", true);
            return false;
        }
        return true;
    }

    private void fillinput(int id)
    {
        MTJ_SaleActivitys ms = bll.GetList(id);
        string tempstring = ms.GoodIDString.Trim();
        if (tempstring.Trim().Length > 0)
        {
            tempstring = "," + tempstring + ",";
        }
        foreach (ListItem item in CHKList_Goods.Items)
        {
            if (tempstring.Contains("," + item.Value + ","))
            {
                item.Selected = true;
            }
        }
        inputIntroductions.Value = ms.Introductions.Trim();
        TextBoxStartDate.Text = ms.StartDate.ToString("yyyy-MM-dd");
        TextBox_EndDate.Text = ms.EndDate.ToString("yyyy-MM-dd");
        inputDiscount.Value = ms.Discount.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}