using System;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Web.UI;

public partial class Admin_TB_ProducStandardsAddEdit : AuthorPage
{
    private readonly BTB_ProducStandards bll = new BTB_ProducStandards();
    private MTB_ProducStandards mod = new MTB_ProducStandards();

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
                    btnDelete.Style["display"] = "none";//新增界面隐藏删除按钮
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
        mod.StandarsDes = inputStandarsDes.Value.Trim();
        mod.Remarks = inputRemarks.Value.Trim();
        mod.CompID = int.Parse(GetCookieCompID());
        //===wyz===20170921==================
        if (mod.StandarsDes.Equals(""))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('产品规格不能为空！');", true);
            return;
        }
        //===wyz===20170921==================
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
        //Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTB_ProducStandards ms = bll.GetList(id);
        inputStandarsDes.Value = ms.StandarsDes.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }

    /*
     * 删除
     */
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
        {
            int deleteId = Convert.ToInt32(Request.QueryString["ID"].Trim());
            bll.Delete(deleteId);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
    }

}