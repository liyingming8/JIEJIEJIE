using System;
using TJ.Model;
using TJ.BLL;
using commonlib; 
public partial class Admin_TB_WorkShopInfoAddEdit : AuthorPage
{
    private readonly BTB_WorkShopInfo bll = new BTB_WorkShopInfo();
    private MTB_WorkShopInfo mod = new MTB_WorkShopInfo();
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
            comfun.BindTreeCombox(ComboBox_PlaceID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china,
                "省份城市...", true, "-", "1=1");
            ComboBox_PlaceID.SelectedValue = "0";
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
        mod.PlaceID = Convert.ToInt32(ComboBox_PlaceID.SelectedValue.Trim());
        mod.Address = inputAddress.Value.Trim();
        mod.Workshop = inputWorkshop.Value.Trim();
        mod.ZhuRen = inputZhuRen.Value.Trim();
        mod.TelePhone = inputTelePhone.Value.Trim();
        mod.CompID = Convert.ToInt32(GetCookieCompID());
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
        MTB_WorkShopInfo ms = bll.GetList(id);
        ComboBox_PlaceID.SelectedValue = ms.PlaceID.ToString().Trim();
        inputAddress.Value = ms.Address.Trim();
        inputWorkshop.Value = ms.Workshop.Trim();
        inputZhuRen.Value = ms.ZhuRen.Trim();
        inputTelePhone.Value = ms.TelePhone.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}