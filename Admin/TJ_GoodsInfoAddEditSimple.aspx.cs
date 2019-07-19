using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
using TJ.DBUtility;
using System.Data;


public partial class Admin_TJ_GoodsInfoAddEditSimple : AuthorPage
{
    private readonly BTJ_GoodsInfo bll = new BTJ_GoodsInfo();
    private MTJ_GoodsInfo mod = new MTJ_GoodsInfo();
    readonly TabExecute _tab = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        string bbj=GetCookieCompID();
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
                    TextBox_BeginSaleDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    TextBox_EndSaleDate.Text = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd");
                    btnDelete.Style["display"] = "none";//新增界面隐藏删除按钮
                    break;
                case "edit":
                    Button1.Text = "修改";
                    fillinput(int.Parse(HF_ID.Value.Trim()));
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
        mod.GoodsName = inputGoodsName.Value.Trim();
        mod.Descriptions = TextArea1.Value.Trim();
        mod.GoodsPicURL = savefilepath.Value;
        mod.BeginSaleDate = Convert.ToDateTime(TextBox_BeginSaleDate.Text.Trim());
        mod.EndSaleDate = Convert.ToDateTime(TextBox_EndSaleDate.Text.Trim());
        mod.Price = Convert.ToDecimal(inputPrice.Value.Trim());
        mod.InnerPrice = 0;
        if (savefilepath.Value.Contains("/"))
        {
            mod.GoodsPicURLSmal = savefilepath.Value.Trim().Insert(savefilepath.Value.Trim().LastIndexOf('/') + 1, "sm_");
        }
        mod.PicturesLinks = "";
        mod.ViedoLinks = "";
        mod.OuterLinkURL = txtOuterLinkURL.Text.Trim();
        mod.Remarks = inputRemarks.Text.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.DisplayDate = DateTime.Now;
                object id = bll.Insert(mod);
                mod.GoodsID = int.Parse(id.ToString());
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        UpdateWuLiuProduct(mod);
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        // ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('操作成功!');", true);
    }

    private readonly BTB_Products_Infor _bproduct = new BTB_Products_Infor();
    private void UpdateWuLiuProduct(MTJ_GoodsInfo mod)
    {
        var mproduct = new MTB_Products_Infor();
        if (mod.WLProID.Equals(0))
        {
            mproduct.Products_Name = mod.GoodsName;
            mproduct.Products_Summary = mod.Descriptions;
            mproduct.CompID = int.Parse(GetCookieCompID());
            object id = _bproduct.Insert(mproduct);
            mod.WLProID = int.Parse(id.ToString());
            bll.Modify(mod);
        }
        else
        {
            mproduct = _bproduct.GetList(mod.WLProID);
            mproduct.Products_Name = mod.GoodsName;
            mproduct.Products_Summary = mod.Descriptions;
            mproduct.CompID = int.Parse(GetCookieCompID());
            _bproduct.Modify(mproduct);
        }
    }

    private void fillinput(int id)
    {
        MTJ_GoodsInfo ms = bll.GetList(id);
        //ComboBox_TypeID.SelectedValue = ms.GoodsTypeID.ToString();
        txtSaleUnitID.Text = ms.SaleUnitID;
        inputGoodsName.Value = ms.GoodsName.Trim();
        TextArea1.Value = ms.Descriptions.Trim();
        showimage.Src = ms.GoodsPicURL.Trim();
        savefilepath.Value = ms.GoodsPicURL.Trim();
        TextBox_BeginSaleDate.Text = ms.BeginSaleDate.ToString("yyyy-MM-dd");
        TextBox_EndSaleDate.Text = ms.EndSaleDate.ToString("yyyy-MM-dd");
        inputPrice.Value = ms.Price.ToString().Trim();
        //inputPriceShangmen.Text = ms.InnerPrice.ToString().Trim();
        //inputPicturesLinks.Value = ms.PicturesLinks.ToString().Trim();
        //inputViedoLinks.Value = ms.ViedoLinks.ToString().Trim(); 
        txtOuterLinkURL.Text = ms.OuterLinkURL.Trim();
        inputRemarks.Text = ms.Remarks.Trim();
    }

    /*
     * 删除
     */
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
        {
            var deleteId = Request.QueryString["ID"];           
            string sql = "delete from TJ_GoodsInfo where GoodsID="+ deleteId;
            DataTable result = _tab.ExecuteNonQuery(sql);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
    }

}