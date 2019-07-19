using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TB_StoreHouseAddEditjg : AuthorPage
{
    private readonly BTB_StoreHouse bll = new BTB_StoreHouse();
    private MTB_StoreHouse mod = new MTB_StoreHouse();
    private readonly CommonFun commfun = new CommonFun();
    private readonly CommonFunWL comwl = new CommonFunWL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
                if (HF_CMD.Value.ToLower().Trim() == "add")
                {
                    inputStoreHouseCode.Value = comwl.CreateAutoID(GetCookieCompID(), "s");
                }
            }
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim();
            }
            commfun.BindTreeCombox(ComboBox_CID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "选择城市...",
                true, "-", "1=1");
            ComboBox_CID.SelectedValue = "0";
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
        mod.StoreHouseCode = inputStoreHouseCode.Value.Trim();
        mod.StoreHouseName = inputStoreHouseName.Value.Trim();
        mod.Remarks = inputRemarks.Value.Trim();
        mod.CID = Convert.ToInt32(ComboBox_CID.SelectedValue);
        mod.AddressString = inputAddressString.Value.Trim();
        mod.Contractor = inputContractor.Value.Trim();
        mod.TelPhoneNumber = inputTelPhoneNumber.Value.Trim();
        mod.MobilePhone = inputMobilePhone.Value.Trim();
        mod.CompID = Convert.ToInt32(GetCookieCompID());
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
        MTB_StoreHouse ms = bll.GetList(id);
        ComboBox_CID.SelectedValue = ms.CID.ToString();
        inputStoreHouseCode.Value = ms.StoreHouseCode.Trim();
        inputStoreHouseName.Value = ms.StoreHouseName.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        inputAddressString.Value = ms.AddressString.Trim();
        inputContractor.Value = ms.Contractor.Trim();
        inputTelPhoneNumber.Value = ms.TelPhoneNumber.Trim();
        inputMobilePhone.Value = ms.MobilePhone.Trim();
    }
}