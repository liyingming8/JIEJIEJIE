using System;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Web.UI;

public partial class Admin_TB_ProductJingHanLiangAddEdit : AuthorPage
{
    private readonly BTB_ProductJingHanLiang bll = new BTB_ProductJingHanLiang();
    private MTB_ProductJingHanLiang mod = new MTB_ProductJingHanLiang();

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
        mod.JingHanLiang = inputJingHanLiang.Value.Trim();
        mod.Remarks = inputRemarks.Value.Trim();
        //===wyz===20170921==================
        if (mod.JingHanLiang.Equals(""))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('净含量不能为空！');", true);
            return;
        }
        //===wyz===20170921==================
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.CompID = Convert.ToInt32(GetCookieCompID());
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        // Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTB_ProductJingHanLiang ms = bll.GetList(id);
        inputJingHanLiang.Value = ms.JingHanLiang.Trim();
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