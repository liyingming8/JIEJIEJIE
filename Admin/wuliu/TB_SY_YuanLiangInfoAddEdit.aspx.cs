using System;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Web.UI;

public partial class Admin_TB_SY_YuanLiangInfoAddEdit : AuthorPage
{
    private readonly BTB_SY_YuanLiangInfo bll = new BTB_SY_YuanLiangInfo();
    private MTB_SY_YuanLiangInfo mod = new MTB_SY_YuanLiangInfo();
    public BTB_SY_GongYingShangInfo bgys = new BTB_SY_GongYingShangInfo();
    private BTB_SY_GongYingShangTypeInfo mgyslb = new BTB_SY_GongYingShangTypeInfo();
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
        //DropDownList_leibie.DataSource = mgyslb.GetListsByFilterString("CompID=" + GetCookieCompID());
        //DropDownList_leibie.DataBind();
        ComboBox_ZID.DataSource = bgys.GetListsByFilterString("CompID=" + GetCookieCompID());
        ComboBox_ZID.DataBind();
        comfun.BindTreeCombox(ComboBox_ylID, "CName", "CID", "ParentID", "TJ_BaseClass", 374, "请选择...", true, "-", "");
        ComboBox_ylID.SelectedValue = "0";
        comfun.BindTreeCombox(ComboBox_CTID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "请选择...", true,
            "-", "");
        ComboBox_CTID.SelectedValue = "0";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try { 
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.Compid = int.Parse(GetCookieCompID());
        mod.YLMingCheng = ComboBox_ylID.SelectedValue;
        mod.YLChanDI = ComboBox_CTID.SelectedValue;
        mod.YLGongYingShangID = Convert.ToInt32(ComboBox_ZID.SelectedValue);
        mod.YLCaiGouDanHao = inputYLCaiGouDanHao.Value.Trim();
        mod.YLCaiGouShiJian = Convert.ToDateTime(inputYLCaiGouShiJian.Text.Trim());
        mod.YLCaiGouShuLiang = inputYLCaiGouShuLiang.Value.Trim();
        mod.YLCaiGouDanWei = inputYLCaiGouDanWei.Value.Trim();
        mod.YLCaiGouRen = inputYLCaiGouRen.Value.Trim();
        mod.YLShenHeRen = inputYLShenHeRen.Value.Trim();
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
            // Response.Write("<script>alert('操作成功！');</script>");
            // ClientScript.RegisterStartupScript(GetType(), "reload", "window.opener.location.reload();window.close();", true);
        }
        catch
        {
            Response.Write("<script>alert('错误！请确保您输入的格式正确！');</script>");
        }
    }

    private void fillinput(int id)
    {
        MTB_SY_YuanLiangInfo ms = bll.GetList(id);

        ComboBox_ylID.SelectedValue = ms.YLMingCheng.Trim();
        ComboBox_CTID.SelectedValue = ms.YLChanDI.Trim();
        ComboBox_ZID.SelectedValue = ms.YLGongYingShangID.ToString().Trim();
        inputYLCaiGouDanHao.Value = ms.YLCaiGouDanHao.Trim();
        inputYLCaiGouShiJian.Text = ms.YLCaiGouShiJian.ToString().Trim();
        inputYLCaiGouShuLiang.Value = ms.YLCaiGouShuLiang.Trim();
        inputYLCaiGouDanWei.Value = ms.YLCaiGouDanWei.Trim();
        inputYLCaiGouRen.Value = ms.YLCaiGouRen.Trim();
        inputYLShenHeRen.Value = ms.YLShenHeRen.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}