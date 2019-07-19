using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_Activity_BoardInfoAddEdit : AuthorPage
{
    BTJ_Activity_BoardInfo bll = new BTJ_Activity_BoardInfo();
    MTJ_Activity_BoardInfo mod = new MTJ_Activity_BoardInfo();
    CommonFun common = new CommonFun();
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
            FillDDL();
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

    private void FillDDL()
    {
        common.BindTreeCombox(ddl_typeid, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.ActivityTypeID, "通用", false, "", "");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.ActivityName = inputActivityName.Value.Trim();
        mod.IsActive = ckb_isactive.Checked;
        mod.LogoURL = HF_Logo.Value.Trim();
        mod.TypeID = Convert.ToInt32(ddl_typeid.SelectedValue);
        mod.UserID = Convert.ToInt32(GetCookieUID());
        mod.Path = input_path.Value;
        mod.Remarks = inputRemarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_Activity_BoardInfoAddEdit.aspx", "TJ_Activity_BoardInfo", "描述", DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                mod.LastUpDate = DateTime.Now;
                bll.Modify(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_Activity_BoardInfoAddEdit.aspx", "TJ_Activity_BoardInfo", "描述", DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void Fillinput(int id)
    {
        MTJ_Activity_BoardInfo ms = bll.GetList(id);
        inputActivityName.Value = ms.ActivityName.Trim();
        ckb_isactive.Checked = ms.IsActive;
        HF_Logo.Value = ms.LogoURL.Trim();
        if (!string.IsNullOrEmpty(ms.LogoURL))
        {
            Image_logo.ImageUrl = ms.LogoURL;
        }
        ddl_typeid.SelectedValue = ms.TypeID.ToString();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}