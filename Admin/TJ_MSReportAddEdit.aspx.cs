using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_MSReportAddEdit : AuthorPage
{
    private readonly BTJ_MSReport bll = new BTJ_MSReport();
    private MTJ_MSReport mod = new MTJ_MSReport();
    private readonly BTB_Products_Infor bproduct = new BTB_Products_Infor();
    private readonly CommonFunWL comfunwl = new CommonFunWL();

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
            FillDLL();
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

        mod.PID = Convert.ToInt16(ComboBox_WLProduct.SelectedValue);
        mod.BGType = DDLField.SelectedValue;
        //mod.GoodsPicURL = Image_GoodPic.ImageUrl.Trim();
        mod.GoodsPicURL = HF_ImageURL.Value.Trim();
        if (HF_ImageURL.Value.Contains("/"))
        {
            mod.GoodsPicURLSmal = HF_ImageURL.Value.Trim().Insert(HF_ImageURL.Value.Trim().LastIndexOf('/') + 1, "sm_");
        }

        mod.Time = Convert.ToDateTime(inputTime.Text.Trim());
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
        MTJ_MSReport ms = bll.GetList(id);

        ComboBox_WLProduct.SelectedValue = ms.PID.ToString().Trim();
        DDLField.SelectedValue = ms.BGType.Trim();
        HF_ImageURL.Value = ms.GoodsPicURL.Trim();
        Image_GoodPic.ImageUrl = ms.GoodsPicURL.Trim();
        inputTime.Text = ms.Time.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }

    private void FillDLL()
    {
        if (GetCookieCompTypeID() == DAConfig.CompTypeIDJingXiaoShang.ToString())
        {
            ComboBox_WLProduct.DataSource = comfunwl.ReturnGetAgentAuthorProductInfo(GetCookieCompID());
        }
        if (GetCookieCompTypeID() == DAConfig.CompTypeIDChangJia.ToString() ||
            GetCookieCompTypeID() == DAConfig.CompTypeTianJianZongBuID.ToString())
        {
            ComboBox_WLProduct.DataSource = bproduct.GetListsByFilterString("CompID=" + GetCookieCompID());
        }
        ComboBox_WLProduct.DataBind();
        ComboBox_WLProduct.SelectedValue = "0";
    }
}