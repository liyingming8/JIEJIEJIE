using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
using TJ.DBUtility;

public partial class Admin_TJ_CompADInfoAddEdit : AuthorPage
{
    private readonly BTJ_CompADInfo bll = new BTJ_CompADInfo();
    private MTJ_CompADInfo mod = new MTJ_CompADInfo();
    private readonly BTJ_AdInfo badinfo = new BTJ_AdInfo();
    private readonly BTB_Products_Infor bproduct = new BTB_Products_Infor();
    private readonly CommonFunWL commwl = new CommonFunWL();
    private readonly CommonFun comm = new CommonFun();
    TabExecute _tab = new TabExecute();

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
        mod.ADID = Convert.ToInt32(ComboBoxADID.SelectedValue);
        mod.CompID = int.Parse(GetCookieCompID());
        mod.IsActive = CheckBox_IsActive.Checked;
        mod.UserID = int.Parse(GetCookieUID());
        mod.Discriptions = inputDiscriptions.Value.Trim();
        mod.Remarks = inputRemarks.Value.Trim();
        string kko = HF_FilePath.Value.ToString();
        if (string.IsNullOrEmpty(HF_FilePath.Value.ToString()))
        {
            MessageBox.Show(this, "请先选择图片并点击上传按钮！");
            //MessageBox.ShowAjax(UpdatePanel1, "请先选择图片并点击上传按钮！");
        }
        else
        {
            mod.FilePath = HF_FilePath.Value;


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
        }
        // ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "location.href='TJ_CompADInfo.aspx'", true);
    }

    private void fillinput(int id)
    {
        MTJ_CompADInfo ms = bll.GetList(id);
        ComboBoxADID.SelectedValue = ms.ADID.ToString().Trim();
        CheckBox_IsActive.Checked = ms.IsActive;
        inputDiscriptions.Value = ms.Discriptions.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        Image_Pic.ImageUrl = ms.FilePath;
        HF_FilePath.Value = ms.FilePath;
        ComboBox_GoodsID.SelectedValue = ms.GoodsID.ToString();
        inputSpecialURLLink.Value = ms.SpecialURLLink;
    }


    private void FillDLL()
    {
        if (int.Parse(GetCookieCompID())==26914) {
            ComboBoxADID.DataSource = _tab.ExecuteNonQuery("select ADID,ADName from TJ_AdInfo where ADID=1");
            ComboBoxADID.DataBind();
            ComboBoxADID.SelectedValue = "0";
        }else
        {
            ComboBoxADID.DataSource = badinfo.GetLists();
            ComboBoxADID.DataBind();
            ComboBoxADID.SelectedValue = "0";
        }
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

    private MTJ_AdInfo madinfo = new MTJ_AdInfo();

    protected void ComboBoxADID_ComboBoxChanged(object sender, EventArgs e)
    {
        madinfo = badinfo.GetList(int.Parse(ComboBoxADID.SelectedValue));
        Label_AdSize.Text = "格式:" + comm.ReturnBaseClassName(madinfo.MediaType.ToString(), false, false) + " 宽:" +
                            madinfo.MWidth + "像素  高:" + madinfo.MHeight + "像素";
    }
}