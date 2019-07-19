using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_DriverInfoAddEdit : AuthorPage
{
    private readonly BTJ_DriverInfo bll = new BTJ_DriverInfo();
    private readonly MTJ_DriverInfo mod = new MTJ_DriverInfo();

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
        mod.DriverID = Convert.ToInt32(inputDriverID.Value.Trim());
        mod.DrivingOld = Convert.ToDecimal(inputDrivingOld.Value.Trim());
        mod.PhoneNumber = inputPhoneNumber.Value.Trim();
        mod.IDCardCode = inputIDCardCode.Value.Trim();
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
        Response.Write("<script>location.href='TJ_DriverInfo.aspx'</script>");
    }

    private void fillinput(int id)
    {
        MTJ_DriverInfo ms = bll.GetList(id);
        inputDriverID.Value = ms.DriverID.ToString().Trim();
        inputDrivingOld.Value = ms.DrivingOld.ToString().Trim();
        inputPhoneNumber.Value = ms.PhoneNumber.Trim();
        inputIDCardCode.Value = ms.IDCardCode.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}