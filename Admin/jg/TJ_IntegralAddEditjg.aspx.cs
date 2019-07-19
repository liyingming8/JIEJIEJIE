using System;
using System.Web;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_IntegralAddEditjg : AuthorPage
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
                    fillinput(int.Parse(HF_ID.Value.Trim()));
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
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('操作成功！');", true);
    }

    private void fillinput(int id)
    {
        MTJ_Integral ms = bll.GetList(id);
        inputIntegralName.Value = ms.IntegralName.Trim();
        inputBeginDate.Text = ms.BeginDate.ToString().Trim();
        inputEndDate.Text = ms.EndDate.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}