using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_VipManageAddEdit : AuthorPage
{
    private readonly BTJ_VipManage bll = new BTJ_VipManage();
    private MTJ_VipManage mod = new MTJ_VipManage();
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
            FillDDL();
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
        mod.MPhone = inputMPhone.Value.Trim();
        mod.ZhuCTime = Convert.ToDateTime(inputZhuCTime.Value.Trim());
        mod.JiFen = Convert.ToInt32(inputJiFen.Value.Trim());
        mod.CompID = int.Parse(GetCookieCompID());
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
        //ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('操作成功！');", true);
    }

    private void fillinput(int id)
    {
        MTJ_VipManage ms = bll.GetList(id);
        //inputVipID.Value = ms.VipID.ToString().Trim();
        inputMPhone.Value = ms.MPhone.Trim();
        inputZhuCTime.Value = ms.ZhuCTime.ToString().Trim();
        inputJiFen.Value = ms.JiFen.ToString().Trim();
        //inputDelFlag.Value = ms.DelFlag.ToString().Trim();       
        //inputCompID.Value = ms.CompID.ToString().Trim();
    }

    private void FillDDL()
    {
        comfun.BindTreeCombox(Comb_PlaceID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "请选择城市...", true,
            "-", "");
        Comb_PlaceID.SelectedValue = "0";
    }
}