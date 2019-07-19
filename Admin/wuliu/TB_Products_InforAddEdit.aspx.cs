using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TB_Products_InforAddEdit : AuthorPage
{
    private readonly BTB_Products_Infor bll = new BTB_Products_Infor();
    private MTB_Products_Infor mod = new MTB_Products_Infor();
    private readonly BTB_ProductJingHanLiang bjinghanliang = new BTB_ProductJingHanLiang();
    private readonly BTB_ProductJiuJingDu bjiujingdu = new BTB_ProductJiuJingDu();
    private readonly BTB_ProductXiangXing bxiangxing = new BTB_ProductXiangXing();
    //private readonly BTB_BiaoZhun bbiaozhun = new BTB_BiaoZhun();
    //private readonly BTB_Metries byuanliao = new BTB_Metries();
    private readonly CommonFunWL commfun = new CommonFunWL();
    readonly InternetHandle _ihandle = new InternetHandle();
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
            BindDDL();
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
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

    private void BindDDL()
    {
        commfun.BindTreeCombox(ComboBox_PSID, "StandarsDes", "PSID", "ParentID", "TB_ProducStandards", 0, "产品规格...",
            true, "-", "CompID=" + GetCookieCompID());
        ComboBox_PSID.SelectedValue = "0";
        commfun.BindTreeCombox(ComboBox_TypeID, "TypeName", "TypeId", "ParentID", "TB_Products_Type", 0, "产品类型...", true,
            "-", "CompID=" + GetCookieCompID());
        ComboBox_TypeID.SelectedValue = "0";
        DropDownList_JingHanLiang.DataSource = bjinghanliang.GetListsByFilterString("CompID=" + GetCookieCompID());
        DropDownList_JingHanLiang.DataBind();
        DropDownList_JiuJingDu.DataSource = bjiujingdu.GetListsByFilterString("CompID=" + GetCookieCompID());
        DropDownList_JiuJingDu.DataBind();
        DropDownList_XiangXing.DataSource = bxiangxing.GetListsByFilterString("CompID=" + GetCookieCompID());
        DropDownList_XiangXing.DataBind();
        //DropDownList_BiaoZhun.DataSource = bbiaozhun.GetListsByFilterString("CompID=" + GetCookieCompID());
        //DropDownList_BiaoZhun.DataBind();
        //DropDownList_YuanLiao.DataSource = byuanliao.GetListsByFilterString("CompID=" + GetCookieCompID());
        //DropDownList_YuanLiao.DataBind();
        if (GetCookieCompID() == "11168")
        {
            CpPrice.Style["display"] = "table-row";
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }

        mod.TypeId = Convert.ToInt32(ComboBox_TypeID.SelectedValue);
        mod.PSID = Convert.ToInt32(ComboBox_PSID.SelectedValue);
        mod.Product_Code = inputProduct_Code.Value.Trim();
        mod.Products_Name = inputProducts_Name.Value.Trim();
        mod.Products_Price = inputProducts_Price.Value.Trim();
        mod.Products_Standards = ComboBox_PSID.SelectedItem.Text;
        mod.ProductXiangXing = Convert.ToInt32(DropDownList_XiangXing.SelectedValue);
        mod.ProductJiuJingDu = Convert.ToInt32(DropDownList_JiuJingDu.SelectedValue);
        //mod.MTID = Convert.ToInt32(DropDownList_YuanLiao.SelectedValue);
        //mod.BZID = Convert.ToInt32(DropDownList_BiaoZhun.SelectedValue);
        mod.ProductJingHanLiang = Convert.ToInt32(DropDownList_JingHanLiang.SelectedValue);
        mod.Products_Summary = txtProducts_Summary.Text.Trim();
        mod.Remarks = HF_ImageURL.Value.Trim();
        mod.Products_date = Convert.ToDateTime("1900-01-01");
        mod.IsOwn = CheckBox_IsOwn.Checked;
        string json = "";
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.CompID = Convert.ToInt32(GetCookieCompID());
                object ID = bll.Insert(mod);
                if (GetCookieCompID().EndsWith(DAConfig.JGCompID))
                {
                    json = "[{\"categoryName\":\"liquor\",\"code\":\"" + mod.Product_Code + "\",\"name\":\"" + mod.Products_Name + "\"}]";
                    _ihandle.Web_Go(json, "POST", _ihandle.GetTouYunJiuGuiToken(), "data-service/isv/products/add?companyCode=JGJ");
                    //_ihandle.web_post_frank("POST",json, _ihandle.GetTouYunJiuGuiToken(), "data-service/isv/products/add?companyCode=JGJ");
                } 
                break;
            case "edit":
                bll.Modify(mod);
                if (GetCookieCompID().Equals(DAConfig.JGCompID))
                {
                    json = "[{\"categoryName\":\"liquor\",\"code\":\"" + mod.Product_Code + "\",\"name\":\"" + mod.Products_Name + "\"}]";
                    _ihandle.Web_Go(json, "PUT", _ihandle.GetTouYunJiuGuiToken(), "data-service/isv/products/edit?companyCode=JGJ");
                    //_ihandle.web_post_frank("PUT",json,_ihandle.GetTouYunJiuGuiToken(), "data-service/isv/products/edit?companyCode=JGJ");
                } 
                break;
        }
        Response.Write("<script>alert('操作成功！');</script>");
    }

    private void Fillinput(int id)
    {
        MTB_Products_Infor ms = bll.GetList(id);
        //if (GetCookieCompID() == "10259" && GetCookieCompID() == "131")
        //{
        //    inputProduct_Code.Disabled = false;
        //}
        ComboBox_TypeID.SelectedValue = ms.TypeId.ToString().Trim();
        ComboBox_PSID.SelectedValue = ms.PSID.ToString().Trim();
        inputProduct_Code.Value = ms.Product_Code.Trim();
        inputProducts_Name.Value = ms.Products_Name.Trim();
        inputProducts_Price.Value = ms.Products_Price.Trim();
        DropDownList_JiuJingDu.SelectedValue = ms.ProductJiuJingDu.ToString().Trim();
        DropDownList_XiangXing.SelectedValue = ms.ProductXiangXing.ToString().Trim();
        DropDownList_JingHanLiang.SelectedValue = ms.ProductJingHanLiang.ToString().Trim();
        txtProducts_Summary.Text = ms.Products_Summary.Trim();
        //inputRemarks.Value = ms.Remarks.ToString().Trim();
        CheckBox_IsOwn.Checked = ms.IsOwn;
        //DropDownList_YuanLiao.SelectedValue = ms.MTID.ToString();
        //DropDownList_BiaoZhun.SelectedValue = ms.BZID.ToString();
        if (!string.IsNullOrEmpty(ms.Remarks))
        {
            HF_ImageURL.Value = ms.Remarks;
            Image_GoodPic.ImageUrl = ms.Remarks;
        } 
    }
}