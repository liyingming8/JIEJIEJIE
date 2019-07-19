using System;
using System.Web;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_RecieveAwardInfoAddEdit : AuthorPage
{
    private readonly BTJ_RecieveAwardInfo bll = new BTJ_RecieveAwardInfo();
    private MTJ_RecieveAwardInfo mod = new MTJ_RecieveAwardInfo();

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
            mod = bll.GetList(int.Parse(HF_ID.Value.Trim()));
        }
        mod.RAWID = Convert.ToInt32(inputRAWID.Value.Trim());
        mod.CompID = int.Parse(GetCookieCompID());
        mod.ITGRID = Convert.ToInt32(inputITGRID.Value.Trim());
        mod.AWID = Convert.ToInt32(inputAWID.Value.Trim());
        mod.UserID = Convert.ToInt32(inputUserID.Value.Trim());
        mod.NeedIntegralValue = Convert.ToDecimal(inputNeedIntegralValue.Value.Trim());
        mod.ReceiverName = inputReceiverName.Value.Trim();
        mod.DetailAddress = inputDetailAddress.Value.Trim();
        mod.PhoneNumber = inputPhoneNumber.Value.Trim();
        mod.RecieveDate = Convert.ToDateTime(inputRecieveDate.Value.Trim());
        mod.Remarks = inputRemarks.Value.Trim();
        mod.CompID = int.Parse(HttpUtility.UrlDecode(Request.Cookies["TJCOMPID"].Value.Trim()));
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
        MTJ_RecieveAwardInfo ms = bll.GetList(id);
        inputRAWID.Value = ms.RAWID.ToString().Trim();
        inputITGRID.Value = ms.ITGRID.ToString().Trim();
        inputAWID.Value = ms.AWID.ToString().Trim();
        inputUserID.Value = ms.UserID.ToString().Trim();
        inputNeedIntegralValue.Value = ms.NeedIntegralValue.ToString().Trim();
        inputReceiverName.Value = ms.ReceiverName.Trim();
        inputDetailAddress.Value = ms.DetailAddress.Trim();
        inputPhoneNumber.Value = ms.PhoneNumber.Trim();
        inputRecieveDate.Value = ms.RecieveDate.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}