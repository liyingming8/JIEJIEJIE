using System;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Web.UI;

public partial class Admin_TB_SY_ZhiQuInfoAddEdit : AuthorPage
{
    private readonly BTB_SY_ZhiQuInfo bll = new BTB_SY_ZhiQuInfo();
    private MTB_SY_ZhiQuInfo mod = new MTB_SY_ZhiQuInfo();
    private readonly BTB_WorkLineInfo bwor = new BTB_WorkLineInfo();
    private readonly BTB_SY_YuanLiangInfo byl = new BTB_SY_YuanLiangInfo();

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
            BindDDL();
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

    private void BindDDL()
    {
        DropDownList_dm.DataSource = byl.GetListsByFilterString("YLMingCheng='410' and CompID=" + GetCookieCompID());
        DropDownList_dm.DataBind();
        DropDownList_gl.DataSource = byl.GetListsByFilterString("YLMingCheng='412' and CompID=" + GetCookieCompID());
        DropDownList_gl.DataBind();
        DropDownList_wd.DataSource = byl.GetListsByFilterString("YLMingCheng='413' and CompID=" + GetCookieCompID());
        DropDownList_wd.DataBind();
        DropDownList_cj.DataSource = bwor.GetListsByFilterString("CompID=" + GetCookieCompID());
        DropDownList_cj.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }

        mod.ZhiQuCheJian = DropDownList_cj.SelectedValue.Trim();

        mod.ZhiQuPiCi = inputZhiQuPiCi.Value.Trim();
        mod.YLID1 = Convert.ToInt32(DropDownList_dm.SelectedItem.Text);
        mod.YLID2 = Convert.ToInt32(DropDownList_gl.SelectedItem.Text);
        mod.YLID3 = Convert.ToInt32(DropDownList_wd.SelectedItem.Text);

        mod.ZhiQuShiJian = Convert.ToDateTime(inputZhiQuShiJian.Text.Trim());
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
        // Response.Write("<script>alert('操作成功！');</script>");

        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        //ClientScript.RegisterStartupScript(GetType(), "reload", "window.opener.location.reload();window.close();", true);
    }

    private void fillinput(int id)
    {
        MTB_SY_ZhiQuInfo ms = bll.GetList(id);


        DropDownList_cj.SelectedValue = ms.ZhiQuCheJian.Trim();
        inputZhiQuPiCi.Value = ms.ZhiQuPiCi.Trim();
        DropDownList_dm.SelectedValue = ms.YLID1.ToString().Trim();
        DropDownList_gl.SelectedValue = ms.YLID2.ToString().Trim();
        DropDownList_wd.SelectedValue = ms.YLID3.ToString().Trim();

        inputZhiQuShiJian.Text = ms.ZhiQuShiJian.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}