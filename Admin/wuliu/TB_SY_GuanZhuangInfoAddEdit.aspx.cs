using System;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Web.UI;

public partial class Admin_TB_SY_GuanZhuangInfoAddEdit : AuthorPage
{
    private readonly BTB_SY_GuanZhuangInfo bll = new BTB_SY_GuanZhuangInfo();
    private MTB_SY_GuanZhuangInfo mod = new MTB_SY_GuanZhuangInfo();

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
        mod.GuanZhuangID = Convert.ToInt32(inputGuanZhuangID.Value.Trim());
        mod.GouTiaoID = Convert.ToInt32(inputGouTiaoID.Value.Trim());
        mod.DaYangJiuPICI = inputDaYangJiuPICI.Value.Trim();
        mod.GuanZhuangCheJian = inputGuanZhuangCheJian.Value.Trim();
        mod.GuanZhuangChanXian = inputGuanZhuangChanXian.Value.Trim();
        mod.GuanZhuangBanZu = inputGuanZhuangBanZu.Value.Trim();
        mod.ShengChanPICI = inputShengChanPICI.Value.Trim();
        mod.GuanZhuangShiJian = Convert.ToDateTime(inputGuanZhuangShiJian.Value.Trim());
        mod.GuanZhuangXianZhiJianYuan = inputGuanZhuangXianZhiJianYuan.Value.Trim();
        mod.GuanZhuangXianZhiJianShiJian = Convert.ToDateTime(inputGuanZhuangXianZhiJianShiJian.Value.Trim());
        mod.Remarks = inputRemarks.Value.Trim();
        mod.Remarks1 = inputRemarks1.Value.Trim();
        mod.Remarks2 = inputRemarks2.Value.Trim();
        mod.Remarks3 = inputRemarks3.Value.Trim();
        mod.Remarks4 = inputRemarks4.Value.Trim();
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
        // ClientScript.RegisterStartupScript(GetType(), "reload", "window.opener.location.reload();window.close();", true);
    }

    private void fillinput(int id)
    {
        MTB_SY_GuanZhuangInfo ms = bll.GetList(id);
        inputGuanZhuangID.Value = ms.GuanZhuangID.ToString().Trim();
        inputGouTiaoID.Value = ms.GouTiaoID.ToString().Trim();
        inputDaYangJiuPICI.Value = ms.DaYangJiuPICI.Trim();
        inputGuanZhuangCheJian.Value = ms.GuanZhuangCheJian.Trim();
        inputGuanZhuangChanXian.Value = ms.GuanZhuangChanXian.Trim();
        inputGuanZhuangBanZu.Value = ms.GuanZhuangBanZu.Trim();
        inputShengChanPICI.Value = ms.ShengChanPICI.Trim();
        inputGuanZhuangShiJian.Value = ms.GuanZhuangShiJian.ToString().Trim();
        inputGuanZhuangXianZhiJianYuan.Value = ms.GuanZhuangXianZhiJianYuan.Trim();
        inputGuanZhuangXianZhiJianShiJian.Value = ms.GuanZhuangXianZhiJianShiJian.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        inputRemarks1.Value = ms.Remarks1.Trim();
        inputRemarks2.Value = ms.Remarks2.Trim();
        inputRemarks3.Value = ms.Remarks3.Trim();
        inputRemarks4.Value = ms.Remarks4.Trim();
    }
}