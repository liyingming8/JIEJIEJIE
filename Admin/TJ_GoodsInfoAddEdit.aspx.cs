using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_GoodsInfoAddEdit : AuthorPage
{
    private readonly BTJ_GoodsInfo bll = new BTJ_GoodsInfo();
    private readonly BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys(); 
    private MTJ_GoodsInfo mod = new MTJ_GoodsInfo(); 
    private readonly CommonFunWL comfunwl = new CommonFunWL();
    private readonly BTB_Products_Infor bproduct = new BTB_Products_Infor();

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
            if (GetCookieCompTypeID() == DAConfig.CompTypeIDJingXiaoShang.ToString() ||
                GetCookieCompTypeID() == DAConfig.CompTypeIDChangJia.ToString())
            {
                HF_ShowInputName.Value = "N";
                inputGoodsName.Visible = false;
                ComboBox_WLProduct.Visible = true;
            }
            else
            {
                HF_ShowInputName.Value = "Y";
                inputGoodsName.Visible = true;
                ComboBox_WLProduct.Visible = false;
            }
            Label_companyname.Text = bcompany.GetList(int.Parse(GetCookieCompID())).CompName;
            //comfun.BindTreeCombox(ComboBox_CompID, "CompName", "CompID", "ParentID", "TJ_RegisterCompanys", int.Parse(GetCookieCompID()), bcompany.GetList(int.Parse(GetCookieCompID())).CompName, true, "-", "CompID=" + GetCookieCompID() + " or ParentID=" + GetCookieCompID());
            //ComboBox_CompID.SelectedValue = GetCookieCompID();
            FillDLL(GetCookieCompID());
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    TextBox_BeginSaleDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    TextBox_EndSaleDate.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
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
        if (HF_CMD.Value.ToLower().Trim().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value.Trim()));
        }
        mod.CompID = int.Parse(GetCookieCompID());
        //mod.GoodsTypeID = Convert.ToInt32(ComboBox_TypeID.SelectedValue.Trim());
        mod.SaleUnitID = txtSaleUnitID.Text;
        if (HF_ShowInputName.Value.Trim().ToUpper() == "Y")
        {
            mod.GoodsName = inputGoodsName.Value.Trim();
        }
        else
        {
            mod.GoodsName = ComboBox_WLProduct.SelectedItem.Text;
        }
        mod.Descriptions = TextArea1.Value.Trim();
        mod.GoodsPicURL = Image_GoodPic.ImageUrl.Trim();
        mod.BeginSaleDate = Convert.ToDateTime(TextBox_BeginSaleDate.Text.Trim());
        mod.EndSaleDate = Convert.ToDateTime(TextBox_EndSaleDate.Text.Trim());
        mod.Price = Convert.ToDecimal(inputPrice.Value.Trim());
        mod.InnerPrice = 0;
        mod.GoodsPicURL = HF_ImageURL.Value.Trim();
        if (HF_ImageURL.Value.Contains("/"))
        {
            mod.GoodsPicURLSmal = HF_ImageURL.Value.Trim().Insert(HF_ImageURL.Value.Trim().LastIndexOf('/') + 1, "sm_");
        }
        mod.PicturesLinks = "";
        mod.ViedoLinks = "";
        mod.OuterLinkURL = txtOuterLinkURL.Text.Trim();
        mod.Remarks = inputRemarks.Text.Trim();
        mod.WLProID = int.Parse(ComboBox_WLProduct.SelectedValue);
        mod.GoodsTypeID = bproduct.GetList(int.Parse(ComboBox_WLProduct.SelectedValue)).TypeId;
        mod.AuhtorSaleCompIDString = GetAuthorSaleCompID();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.DisplayDate = DateTime.Now;
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        // ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('操作成功!');", true);
    }

    private string tempreturnstring = "";

    private string GetAuthorSaleCompID()
    {
        foreach (ListItem item in CheckBoxList_AuthorCompID.Items)
        {
            if (item.Selected)
            {
                tempreturnstring += "," + item.Value;
            }
        }
        return tempreturnstring.StartsWith(",") ? tempreturnstring.Substring(1) : tempreturnstring;
    }

    private void fillinput(int id)
    {
        MTJ_GoodsInfo ms = bll.GetList(id);
        //ComboBox_TypeID.SelectedValue = ms.GoodsTypeID.ToString();
        txtSaleUnitID.Text = ms.SaleUnitID;
        inputGoodsName.Value = ms.GoodsName.Trim();
        TextArea1.Value = ms.Descriptions.Trim();
        Image_GoodPic.ImageUrl = ms.GoodsPicURL.Trim();
        HF_ImageURL.Value = ms.GoodsPicURL.Trim();
        TextBox_BeginSaleDate.Text = ms.BeginSaleDate.ToString("yyyy-MM-dd");
        TextBox_EndSaleDate.Text = ms.EndSaleDate.ToString("yyyy-MM-dd");
        inputPrice.Value = ms.Price.ToString().Trim();
        //inputPriceShangmen.Text = ms.InnerPrice.ToString().Trim();
        //inputPicturesLinks.Value = ms.PicturesLinks.ToString().Trim();
        //inputViedoLinks.Value = ms.ViedoLinks.ToString().Trim();
        ComboBox_WLProduct.SelectedValue = ms.WLProID.ToString();
        txtOuterLinkURL.Text = ms.OuterLinkURL.Trim();
        inputRemarks.Text = ms.Remarks.Trim();
        if (ms.AuhtorSaleCompIDString.Length > 0)
        {
            foreach (ListItem item in CheckBoxList_AuthorCompID.Items)
            {
                if (("," + ms.AuhtorSaleCompIDString + ",").Contains("," + item.Value + ","))
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }
            }
        }
    }

    private void FillDLL(string CompID)
    {
        if (CompID.Trim() == "0" || CompID == "")
        {
            CompID = GetCookieCompID();
        }
        CheckBoxList_AuthorCompID.DataSource = bcompany.GetListsByFilterString("ParentID=" + CompID);
        CheckBoxList_AuthorCompID.DataBind();
     

        if (CheckBoxList_AuthorCompID.Items.Count > 0)
        {
            Label_MeshPointName.Visible = true;
        }
        else
        {
            Label_MeshPointName.Visible = false;
        }
        //if (HF_CMD.Value.ToLower().Equals("edit"))
        //{
        //    CheckBoxList_AuthorCompID.Enabled = false;
        //}

        if (GetCookieCompTypeID() == DAConfig.CompTypeIDChangJia.ToString().Trim() || GetCookieCompID() == "1" ||
            GetCookieCompID() == "2")
        {
            ComboBox_WLProduct.DataSource = bproduct.GetListsByFilterString("CompID=" + GetCookieCompID());
        }
        else
        {
            CommonFunWL comwl = new CommonFunWL();
            ComboBox_WLProduct.DataSource = comwl.ReturnGetAgentAuthorProductInfo(GetCookieCompID());
        }
        ComboBox_WLProduct.DataBind();
        ComboBox_WLProduct.SelectedValue = "0";
        ComboBox_WLProduct.Visible = true;
        if (GetCookieCompTypeID() == DAConfig.CompTypeIDChangJia.ToString() ||
            GetCookieCompTypeID() == DAConfig.CompTypeIDJingXiaoShang.ToString())
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
            ComboBox_WLProduct.Visible = true;
        }
        //else
        //{
        //    ComboBox_WLProduct.Visible = false;
        //}
    }

    //protected void ComboBox_CompID_ComboBoxChanged(object sender, EventArgs e)
    //{
    //    FillDLL(ComboBox_CompID.SelectedValue);
    //}
    protected void ComboBox_WLProduct_ComboBoxChanged(object sender, EventArgs e)
    {
        inputGoodsName.Value = ComboBox_WLProduct.SelectedItem.Text;
        if (TextArea1.Value.Trim().Length == 0)
        {
            TextArea1.Value = bproduct.GetList(int.Parse(ComboBox_WLProduct.SelectedValue)).Remarks;
        } 
        cpbm.Value = bproduct.GetList(int.Parse(ComboBox_WLProduct.SelectedValue)).Product_Code;
    }
}