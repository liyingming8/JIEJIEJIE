using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.IO;

public partial class Admin_TJ_RegisterInnerDJPlaceAddEdit : AuthorPage
{
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();
    private MTJ_RegisterCompanys mod = new MTJ_RegisterCompanys();
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
            FillComBox();
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    break;
                case "edit":
                    Button1.Text = "修改";
                    if (HF_ID.Value == null || HF_ID.Value == "")
                    {
                        HF_ID.Value = GetCookieCompID();
                        inputCompName.Disabled = true;
                    }

                    fillinput(int.Parse(HF_ID.Value.Trim()));
                    break;
                default:
                    break;
            }
            if (IsSuperAdmin())
            {
                    new LiteralControl(
                        "<a href=\"TJ_RegisterInnerDJPlace.aspx\"><img title=\"返回\" src=\"images/back.png\" border=\"0\" /></a>");
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.ToLower().Trim().Equals("edit"))
        {
            mod = bll.GetList(int.Parse(HF_ID.Value));
        }
        mod.CTID = Convert.ToInt32(ComboBox_CTID.SelectedValue);
        mod.CompName = inputCompName.Value.Trim();
        mod.CompLogo = HF_LogoImage.Value.Trim();
        mod.LegalPerson = inputLegalPerson.Value.Trim();
        mod.Address = inputAddress.Value.Trim();
        mod.TelNumber = inputTelNumber.Value.Trim();
        mod.BusinessLicencePicture = HF_LectureImage.Value.Trim();
        mod.Remarks = inputRemarks.Text.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.RegisterDate = DateTime.Now;
                mod.AuthoredDate = Convert.ToDateTime("1900-01-01");
                mod.DisAuthorDate = Convert.ToDateTime("1900-01-01");
                mod.ParentID = int.Parse(GetCookieCompID());
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        //ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('操作成功');", true);
        //this.Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTJ_RegisterCompanys ms = bll.GetList(id);
        ComboBox_CTID.SelectedValue = ms.CTID.ToString().Trim();
        inputCompName.Value = ms.CompName.Trim();
        HF_LogoImage.Value = ms.CompLogo.Trim();
        inputLegalPerson.Value = ms.LegalPerson.Trim();
        inputAddress.Value = ms.Address.Trim();
        inputTelNumber.Value = ms.TelNumber.Trim();
        inputRemarks.Text = ms.Remarks.Trim();
    }

    private void FillComBox()
    {
        comfun.BindTreeCombox(ComboBox_CTID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "请选择...", true,
            "-", "");
        ComboBox_CTID.SelectedValue = "0";
    }

    public bool CheckFileType(string fileName)
    {
        //获取文件的扩展名,前提要用这个方法必须引入命名空间io

        string ext = Path.GetExtension(fileName);

        switch (ext.ToLower())
        {
            case ".gif":

                return true;

            case ".png":

                return true;

            case ".jpeg":

                return true;

            case "jpg":

                return true;

            default:

                return false;
        }
    }
}