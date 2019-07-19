using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_LuoDiYeConfigAddEdit : AuthorPage
{
    private readonly BTJ_LuoDiYeConfig bll = new BTJ_LuoDiYeConfig();
    private MTJ_LuoDiYeConfig mod = new MTJ_LuoDiYeConfig();
    private readonly BTB_Products_Infor bproduct = new BTB_Products_Infor();

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
            FILLDDL();
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

    private void FILLDDL()
    {
        ComboBox_WLProID.DataSource = bproduct.GetListsByFilterString("CompID=" + GetCookieCompID());
        ComboBox_WLProID.DataBind();
        ComboBox_WLProID.SelectedValue = "0";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.CompID = Convert.ToInt32(GetCookieCompID());
        mod.WLProID = Convert.ToInt32(ComboBox_WLProID.SelectedValue);

        mod.LimitedCheckNum = Convert.ToInt32(inputLimitedCheckNum.Value.Trim().Length>0?inputLimitedCheckNum.Value.Trim():"0");
        mod.AlertContents = inputAlertContents.Value.Trim();
        mod.ShowTopAd = CheckBox_ShowTopAd.Checked;
        mod.ShowSuYuan = CheckBox_ShowSuYuan.Checked;
        mod.ShowModules = CheckBox_ShowModules.Checked;
        mod.ShowTraceInfo = CheckBox_ShowTraceInfo.Checked;
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
        Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTJ_LuoDiYeConfig ms = bll.GetList(id);
        ComboBox_WLProID.SelectedValue = ms.WLProID.ToString().Trim();
        inputLimitedCheckNum.Value = ms.LimitedCheckNum.ToString().Trim();
        inputAlertContents.Value = ms.AlertContents.Trim();
        CheckBox_ShowTopAd.Checked = ms.ShowTopAd;
        CheckBox_ShowModules.Checked = ms.ShowSuYuan;
        CheckBox_ShowSuYuan.Checked = ms.ShowSuYuan;
        CheckBox_ShowTraceInfo.Checked = ms.ShowTraceInfo;
        inputRemarks.Value = ms.Remarks.Trim();
    }
}