using System;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Web.UI;

public partial class Admin_TB_SY_GongYingShangTypeInfoAddEdit : AuthorPage
{
    private readonly BTB_SY_GongYingShangTypeInfo bll = new BTB_SY_GongYingShangTypeInfo();
    private MTB_SY_GongYingShangTypeInfo mod = new MTB_SY_GongYingShangTypeInfo();

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
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }

        mod.GYSTYPEMingCheng = inputGYSTYPEMingCheng.Value.Trim();
        mod.Remarks = inputRemarks.Value.Trim();
        //mod.Remarks1 = inputRemarks1.Value.Trim();
        //mod.Remarks2 = inputRemarks2.Value.Trim();
        mod.Compid = Convert.ToInt32(GetCookieCompID());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        // Response.Write("<script>alert('操作成功！');</script>");
        // ClientScript.RegisterStartupScript(GetType(), "reload", "window.opener.location.reload();window.close();", true);
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void fillinput(int id)
    {
        MTB_SY_GongYingShangTypeInfo ms = bll.GetList(id);
        //inputGYSTYPEID.Value = ms.GYSTYPEID.ToString().Trim();
        inputGYSTYPEMingCheng.Value = ms.GYSTYPEMingCheng.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        //inputRemarks1.Value = ms.Remarks1.ToString().Trim();
        //inputRemarks2.Value = ms.Remarks2.ToString().Trim();
    }
}