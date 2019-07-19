using System;
using System.Web;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_IntegralAddEdit : AuthorPage
{
    private readonly BTJ_Integral bll = new BTJ_Integral();
    private MTJ_Integral mod = new MTJ_Integral();
    private BTJ_RegisterCompanys bllcomp = new BTJ_RegisterCompanys();

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
                    Fillinput(int.Parse(HF_ID.Value.Trim()));
                    break;
                default:
                    break;
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.ToLower().Trim().Equals("edit"))
        {
            mod = bll.GetList(int.Parse(HF_ID.Value.Trim()));
        }
        mod.CompID = Convert.ToInt32(GetCookieCompID());
        mod.IntegralName = inputIntegralName.Value.Trim();
        mod.BeginDate = Convert.ToDateTime(inputBeginDate.Text.Trim());
        mod.EndDate = Convert.ToDateTime(inputEndDate.Text.Trim());
        mod.Remarks = inputRemarks.Value.Trim();
        mod.JiFenPlatFormiD = int.Parse(rbl_platform.SelectedValue);
        mod.VLPerYuan = input_VLPerYuan.Value.Equals("自定") ? 0 : int.Parse(input_VLPerYuan.Value);
        //判断日期的大小，结束日期应该大于开始日期===WYZ-20170921
        if (DateTime.Compare(mod.BeginDate, mod.EndDate) >= 0) //判断日期大小
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('开始日期必须小于结束日期！');", true);
            return;
        }


        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.PublishDate = DateTime.Now;
                mod.UserID = Convert.ToInt32(HttpUtility.UrlDecode(Request.Cookies["TJUID"].Value));
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        // ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('操作成功！');", true);
    }

    private void Fillinput(int id)
    {
        MTJ_Integral ms = bll.GetList(id);
        inputIntegralName.Value = ms.IntegralName.Trim();
        inputBeginDate.Text = ms.BeginDate.ToString("yyyy-MM-dd").Trim();
        inputEndDate.Text = ms.EndDate.ToString("yyyy-MM-dd").Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        rbl_platform.SelectedValue = ms.JiFenPlatFormiD.ToString();
        input_VLPerYuan.Value = ms.VLPerYuan.Equals(0) ? "自定" : ms.VLPerYuan.ToString();
    }
    protected void rbl_platform_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbl_platform.SelectedValue.Equals("1"))
        {
            input_VLPerYuan.Value = "20";
        }
        else
        {
            input_VLPerYuan.Value = "自定";
        }
    }
}