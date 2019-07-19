using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_MeshPointAddEdit : AuthorPage
{
    private readonly BTJ_MeshPoint bll = new BTJ_MeshPoint();
    private MTJ_MeshPoint mod = new MTJ_MeshPoint();
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
            mod = bll.GetList(int.Parse(HF_ID.Value));
        }
        mod.CompID = int.Parse(GetCookieCompID());
        mod.PlaceID = Convert.ToInt32(Comb_PlaceID.SelectedValue);
        mod.MeshPointName = inputMeshPointName.Value.Trim();
        mod.Tel = inputTel.Value.Trim();
        mod.Fax = inputFax.Value.Trim();
        mod.Contracter = inputContracter.Value.Trim();
        mod.AddressInfo = inputAddressInfo.Value.Trim();
        mod.Position = inputPosition.Value.Trim();
        mod.Remark = inputRemark.Value.Trim();
        mod.DiscriptionString = CommonFun.ReplaceHtmlNoImg(CKEditorControl1.Text.Trim());
        mod.AdvantageString = TextAdvantageString.Value;
        mod.ImageURL = HF_LogoImage.Value;
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('操作成功！');", true);
    }

    private void fillinput(int id)
    {
        MTJ_MeshPoint ms = bll.GetList(id);
        Comb_PlaceID.SelectedValue = ms.PlaceID.ToString().Trim();
        inputMeshPointName.Value = ms.MeshPointName.Trim();
        inputTel.Value = ms.Tel.Trim();
        inputFax.Value = ms.Fax.Trim();
        inputContracter.Value = ms.Contracter.Trim();
        inputAddressInfo.Value = ms.AddressInfo.Trim();
        inputPosition.Value = ms.Position.Trim();
        inputRemark.Value = ms.Remark.Trim();
        CKEditorControl1.Text = ms.DiscriptionString;
        TextAdvantageString.Value = ms.AdvantageString;
        Image_Logo.ImageUrl = ms.ImageURL;
        HF_LogoImage.Value = ms.ImageURL;
    }

    private void FillDDL()
    {
        comfun.BindTreeCombox(Comb_PlaceID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "请选择城市...", true,
            "-", "");
        Comb_PlaceID.SelectedValue = "0";
    }
}