using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TB_SY_GouTiaoInfoAddEdit : AuthorPage
{
    private readonly BTB_SY_GouTiaoInfo bll = new BTB_SY_GouTiaoInfo();
    private MTB_SY_GouTiaoInfo mod = new MTB_SY_GouTiaoInfo();
    private readonly BTB_SY_ZhiJiuInfo bzj = new BTB_SY_ZhiJiuInfo();
    private MTB_SY_ZhiJiuInfo mzj = new MTB_SY_ZhiJiuInfo();

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
            FillDLL();
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

    private void FillDLL()
    {
        ComboBox_ZID.DataSource = bzj.GetListsByFilterString("CompID=" + GetCookieCompID());
        ComboBox_ZID.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }

        mod.ZhiJiuID = Convert.ToInt32(ComboBox_ZID.SelectedValue);
        mod.DaYangJiuMingCheng = inputDaYangJiuMingCheng.Value.Trim();
        mod.DaYangJiuPiCi = inputDaYangJiuPiCi.Value.Trim();
        mod.GouTiaoShiJian = Convert.ToDateTime(inputGouTiaoShiJian.Text.Trim());
        mod.ZhiJianYuan = inputZhiJianYuan.Value.Trim();
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
        ClientScript.RegisterStartupScript(GetType(), "reload", "window.opener.location.reload();window.close();", true);
    }

    private void fillinput(int id)
    {
        MTB_SY_GouTiaoInfo ms = bll.GetList(id);

        ComboBox_ZID.SelectedValue = ms.ZhiJiuID.ToString().Trim();
        inputDaYangJiuMingCheng.Value = ms.DaYangJiuMingCheng.Trim();
        inputDaYangJiuPiCi.Value = ms.DaYangJiuPiCi.Trim();
        inputGouTiaoBanZu.Value = ms.GouTiaoBanZu.Trim();
        inputGouTiaoShiJian.Text = ms.GouTiaoShiJian.ToString().Trim();
        inputZhiJianYuan.Value = ms.ZhiJianYuan.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}