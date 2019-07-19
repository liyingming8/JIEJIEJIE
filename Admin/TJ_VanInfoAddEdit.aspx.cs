using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_VanInfoAddEdit : AuthorPage
{
    private readonly BTJ_VanInfo bll = new BTJ_VanInfo();
    private readonly MTJ_VanInfo mod = new MTJ_VanInfo();

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
        mod.VanID = Convert.ToInt32(inputVanID.Value.Trim());
        mod.VanTypeID = Convert.ToInt32(inputVanTypeID.Value.Trim());
        mod.VanBrandID = Convert.ToInt32(inputVanBrandID.Value.Trim());
        mod.VanCarryAbID = Convert.ToInt32(inputVanCarryAbID.Value.Trim());
        mod.VanSizeID = Convert.ToInt32(inputVanSizeID.Value.Trim());
        mod.VanMaterID = Convert.ToInt32(inputVanMaterID.Value.Trim());
        mod.DriverID = Convert.ToInt32(inputDriverID.Value.Trim());
        mod.VanCertifID = Convert.ToInt32(inputVanCertifID.Value.Trim());
        mod.NumberPlate = inputNumberPlate.Value.Trim();
        mod.VanPicture = inputVanPicture.Value.Trim();
        mod.VanIntructions = inputVanIntructions.Value.Trim();
        mod.VehicleLicenseCode = inputVehicleLicenseCode.Value.Trim();
        mod.VehicleLicensePicture = inputVehicleLicensePicture.Value.Trim();
        mod.OperationCertificateCode = inputOperationCertificateCode.Value.Trim();
        mod.OperationCertificatePicture = inputOperationCertificatePicture.Value.Trim();
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
        MTJ_VanInfo ms = bll.GetList(id);
        inputVanID.Value = ms.VanID.ToString().Trim();
        inputVanTypeID.Value = ms.VanTypeID.ToString().Trim();
        inputVanBrandID.Value = ms.VanBrandID.ToString().Trim();
        inputVanCarryAbID.Value = ms.VanCarryAbID.ToString().Trim();
        inputVanSizeID.Value = ms.VanSizeID.ToString().Trim();
        inputVanMaterID.Value = ms.VanMaterID.ToString().Trim();
        inputDriverID.Value = ms.DriverID.ToString().Trim();
        inputVanCertifID.Value = ms.VanCertifID.ToString().Trim();
        inputNumberPlate.Value = ms.NumberPlate.Trim();
        inputVanPicture.Value = ms.VanPicture.Trim();
        inputVanIntructions.Value = ms.VanIntructions.Trim();
        inputVehicleLicenseCode.Value = ms.VehicleLicenseCode.Trim();
        inputVehicleLicensePicture.Value = ms.VehicleLicensePicture.Trim();
        inputOperationCertificateCode.Value = ms.OperationCertificateCode.Trim();
        inputOperationCertificatePicture.Value = ms.OperationCertificatePicture.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}