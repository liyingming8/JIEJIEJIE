using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_CompADInfoAddEditSimple : AuthorPage
{
    private readonly BTJ_CompADInfo bll = new BTJ_CompADInfo();
    private MTJ_CompADInfo mod = new MTJ_CompADInfo(); 
    private readonly BTB_Products_Infor bproduct = new BTB_Products_Infor();
    private readonly CommonFunWL commwl = new CommonFunWL();
    public string Bilv = "2/1";
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
            if (!string.IsNullOrEmpty(Request.QueryString["bilv"]))
            {
                Bilv = Request.QueryString["bilv"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ADID"]))
            {
                hdadid.Value = Request.QueryString["ADID"];
            } 
            FillDll();
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
        mod.ADID = Convert.ToInt32(hdadid.Value);
        mod.CompID = int.Parse(GetCookieCompID());
        mod.IsActive = true;
        mod.UserID = int.Parse(GetCookieUID());
        mod.Discriptions = inputDiscriptions.Value.Trim();
        mod.Remarks = inputRemarks.Value.Trim();
        mod.FilePath = savefilepath.Value;
        mod.SpecialURLLink = inputSpecialURLLink.Value;
        mod.GoodsID = Convert.ToInt32(ComboBox_GoodsID.SelectedValue.Equals(null) ? "0" : ComboBox_GoodsID.SelectedValue);
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.UploadDate = DateTime.Now;
                mod.ModifyDate = DateTime.Now;
                bll.Insert(mod);
                break;
            case "edit":
                mod.ModifyDate = DateTime.Now;
                bll.Modify(mod);
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        // ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "location.href='TJ_CompADInfo.aspx'", true);
    }

    private void fillinput(int id)
    {
        MTJ_CompADInfo ms = bll.GetList(id);
        hdadid.Value = ms.ADID.ToString().Trim(); 
        inputDiscriptions.Value = ms.Discriptions.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        showimage.Src = ms.FilePath;
        savefilepath.Value = ms.FilePath;
        ComboBox_GoodsID.SelectedValue = ms.GoodsID.ToString();
        inputSpecialURLLink.Value = ms.SpecialURLLink;
    } 

    private void FillDll()
    { 
        ComboBox_GoodsID.DataTextField = "Products_Name";
        ComboBox_GoodsID.DataValueField = "Infor_ID";
        if (GetCookieCompTypeID() == DAConfig.CompTypeIDChangJia.ToString())
        {
            ComboBox_GoodsID.DataSource = bproduct.GetListsByFilterString("CompID=" + GetCookieCompID());
        }
        if (GetCookieCompTypeID() == DAConfig.CompTypeIDJingXiaoShang.ToString())
        {
            ComboBox_GoodsID.DataSource = commwl.ReturnGetAgentAuthorProductInfo(GetCookieCompID());
        }
        ComboBox_GoodsID.DataBind();
        ComboBox_GoodsID.Items.Add(new ListItem("指定产品...", "0"));
        ComboBox_GoodsID.SelectedValue = "0";
    } 
}