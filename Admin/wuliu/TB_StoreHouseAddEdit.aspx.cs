using System;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Web.UI;
using TJ.DBUtility;
using System.Data;

public partial class Admin_TB_StoreHouseAddEdit : AuthorPage
{
    private readonly BTB_StoreHouse bll = new BTB_StoreHouse();
    private MTB_StoreHouse mod = new MTB_StoreHouse();
    private readonly CommonFun commfun = new CommonFun();
    private readonly CommonFunWL comwl = new CommonFunWL();
    readonly TabExecute _tab = new TabExecute();

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
            commfun.BindTreeCombox(ComboBox_CID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "选择城市...",
                true, "-", "1=1");
            ComboBox_CID.SelectedValue = "0";
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    btnDelete.Style["display"] = "none";//新增界面隐藏删除按钮
                    break;
                case "edit":
                    Button1.Text = "修改";
                    Fillinput(int.Parse(HF_ID.Value.Trim()));
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
        mod.MobilePhone = inputMobilePhone.Value.Trim();
        mod.CompID = Convert.ToInt32(GetCookieCompID());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.StoreHouseCode = comwl.CreateAutoCode(GetCookieCompID(), "S");
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true); 
    }

    private void Fillinput(int id)
    {
        MTB_StoreHouse ms = bll.GetList(id);
        ComboBox_CID.SelectedValue = ms.CID.ToString();
        inputStoreHouseCode.Value = ms.StoreHouseCode.Trim();
        inputStoreHouseName.Value = ms.StoreHouseName.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        inputAddressString.Value = ms.AddressString.Trim();
        inputContractor.Value = ms.Contractor.Trim(); 
        inputMobilePhone.Value = ms.MobilePhone.Trim();
    }

    /*
     * 删除
     */
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
        {
            int deleteId = Convert.ToInt32(Request.QueryString["ID"]);
            bll.Delete(deleteId);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
    }

}