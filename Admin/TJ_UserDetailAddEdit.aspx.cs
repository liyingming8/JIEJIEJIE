using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_UserDetailAddEdit : AuthorPage
{
    private readonly BTJ_UserDetail bll = new BTJ_UserDetail();
    private readonly MTJ_UserDetail mod = new MTJ_UserDetail();

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
        mod.UserDetailID = Convert.ToInt32(inputUserDetailID.Value.Trim());
        mod.UserID = Convert.ToInt32(inputUserID.Value.Trim());
        mod.CTID = Convert.ToInt32(inputCTID.Value.Trim());
        mod.Birthday = Convert.ToDateTime(inputBirthday.Value.Trim());
        mod.Address = inputAddress.Value.Trim();
        mod.IdentificationCode = inputIdentificationCode.Value.Trim();
        mod.PhoneNumber = inputPhoneNumber.Value.Trim();
        mod.EMail = inputEMail.Value.Trim();
        mod.QQ = inputQQ.Value.Trim();
        mod.WeiXinNumber = inputWeiXinNumber.Value.Trim();
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
        MTJ_UserDetail ms = bll.GetList(id);
        inputUserDetailID.Value = ms.UserDetailID.ToString().Trim();
        inputUserID.Value = ms.UserID.ToString().Trim();
        inputCTID.Value = ms.CTID.ToString().Trim();
        inputBirthday.Value = ms.Birthday.ToString().Trim();
        inputAddress.Value = ms.Address.Trim();
        inputIdentificationCode.Value = ms.IdentificationCode.Trim();
        inputPhoneNumber.Value = ms.PhoneNumber.Trim();
        inputEMail.Value = ms.EMail.Trim();
        inputQQ.Value = ms.QQ.Trim();
        inputWeiXinNumber.Value = ms.WeiXinNumber.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}