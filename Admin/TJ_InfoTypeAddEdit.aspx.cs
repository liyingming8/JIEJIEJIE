using System;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Web.UI;

public partial class Admin_TJ_InfoTypeAddEdit : AuthorPage
{
    private readonly BTJ_InfoType bll = new BTJ_InfoType();
    private MTJ_InfoType mod = new MTJ_InfoType();
    private readonly CommonFun comfun = new CommonFun();

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
            comfun.BindTreeCombox(ComboBox_ParentID, "TypeName", "IFTypeID", "ParentID", "TJ_InfoType", 0, "---", true,
                "-", "CompID=" + GetCookieCompID());
            ComboBox_ParentID.SelectedValue = "0";
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
        if (inputTypeName.Value.Length > 0)
        {
            mod.ParentID = Convert.ToInt32(ComboBox_ParentID.SelectedValue);
            mod.TypeName = inputTypeName.Value.Trim();
            mod.Remarks = inputRemarks.Value.Trim();
            switch (HF_CMD.Value.Trim())
            {
                case "add":
                    mod.CompID = int.Parse(GetCookieCompID());
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

        // Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTJ_InfoType ms = bll.GetList(id);
        ComboBox_ParentID.SelectedValue = ms.ParentID.ToString().Trim();
        inputTypeName.Value = ms.TypeName.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}