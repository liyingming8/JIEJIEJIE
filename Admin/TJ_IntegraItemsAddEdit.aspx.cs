using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_IntegraItemsAddEdit : AuthorPage
{
    private readonly BTJ_IntegraItems bll = new BTJ_IntegraItems();
    private MTJ_IntegraItems mod = new MTJ_IntegraItems();
    private BTJ_RegisterCompanys bllcom = new BTJ_RegisterCompanys();

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
        if (inputItemName.Value.Length > 0)
        {
            mod.CompID = 0;
            mod.ItemName = inputItemName.Value.Trim();
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
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
        else {
            ScriptManager.RegisterStartupScript(UpdatePanel1, Page.GetType(), "info", "alert('名称不能为空！');", true);
        }
        //ScriptManager.RegisterStartupScript(UpdatePanel1, Page.GetType(), "info", "alert('操作成功！');", true);
    }

    private void fillinput(int id)
    {
        MTJ_IntegraItems ms = bll.GetList(id);
        inputItemName.Value = ms.ItemName.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}