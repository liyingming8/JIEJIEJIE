using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_CompanyGoodsTypesAddEdit : AuthorPage
{
    private readonly BTJ_CompanyGoodsTypes bll = new BTJ_CompanyGoodsTypes();
    private BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();
    private MTJ_CompanyGoodsTypes mod = new MTJ_CompanyGoodsTypes();

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
        mod.CompID = Convert.ToInt32(GetCookieCompID());
        mod.GoodsType = inputGoodsType.Value.Trim();
        mod.Remark = inputRemark.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        // ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('操作成功！');", true);
        // ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "location.href='TJ_CompanyGoodsTypesAddEdit.aspx'", true);
    }

    private void fillinput(int id)
    {
        MTJ_CompanyGoodsTypes ms = bll.GetList(id);
        inputGoodsType.Value = ms.GoodsType.Trim();
        inputRemark.Value = ms.Remark.Trim();
    }
}