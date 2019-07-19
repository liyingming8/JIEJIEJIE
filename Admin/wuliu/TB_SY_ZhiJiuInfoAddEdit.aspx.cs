using System;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Web.UI;

public partial class Admin_TB_SY_ZhiJiuInfoAddEdit : AuthorPage
{
    private readonly BTB_SY_ZhiJiuInfo bll = new BTB_SY_ZhiJiuInfo();
    private MTB_SY_ZhiJiuInfo mod = new MTB_SY_ZhiJiuInfo();
    public BTB_SY_ZhiQuInfo bzq = new BTB_SY_ZhiQuInfo();

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
        DropDownList_zq.DataSource = bzq.GetListsByFilterString("CompID=" + GetCookieCompID());
        DropDownList_zq.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try { 
        if (DropDownList_zq.SelectedValue == "0 ")
        {
            Response.Write(" <script> alert( '请您选择制曲批次！ ');</script> ");
            return;
        }
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }

        mod.ZhiQuID = Convert.ToInt32(DropDownList_zq.SelectedValue.Trim());
        mod.ZhiJiuPiCi = inputZhiJiuPiCi.Value.Trim();
        mod.ZhiJiuCheJian = inputZhiJiuCheJian.Value.Trim();
        mod.ZhiJiuBanZu = inputZhiJiuBanZu.Value.Trim();
        mod.ZhiJiuShiJian = Convert.ToDateTime(inputZhiJiuShiJian.Text.Trim());
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
            //Response.Write("<script>alert('操作成功！');</script>");

            // ClientScript.RegisterStartupScript(GetType(), "reload", "window.opener.location.reload();window.close();", true);
        }
        catch
        {
            Response.Write("<script>alert('错误！请确保您输入的格式正确！');</script>");
        }
    }

    private void fillinput(int id)
    {
        MTB_SY_ZhiJiuInfo ms = bll.GetList(id);

        DropDownList_zq.SelectedValue = ms.ZhiQuID.ToString().Trim();
        inputZhiJiuPiCi.Value = ms.ZhiJiuPiCi.Trim();
        inputZhiJiuCheJian.Value = ms.ZhiJiuCheJian.Trim();
        inputZhiJiuBanZu.Value = ms.ZhiJiuBanZu.Trim();
        inputZhiJiuShiJian.Text = ms.ZhiJiuShiJian.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}