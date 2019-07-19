using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TB_Products_tongyong_singleAdd : AuthorPage
{
    private readonly BTB_Products_Infor bll = new BTB_Products_Infor();
    private MTB_Products_Infor mod = new MTB_Products_Infor();
    private readonly BTB_ProductJingHanLiang bjinghanliang = new BTB_ProductJingHanLiang(); 
    private readonly CommonFunWL commfun = new CommonFunWL(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim(); 
            }
            if (!string.IsNullOrEmpty(Request.QueryString["fr"]))
            {
                HF_From.Value = Sc.DecryptQueryString(Request.QueryString["fr"]);
            }
            BindDdl(); 
        }
    }

    private void BindDdl()
    {
        commfun.BindTreeCombox(ComboBox_PSID, "StandarsDes", "PSID", "ParentID", "TB_ProducStandards", 0, "产品规格...",
            true, "-", "CompID=" + GetCookieCompID());
        ComboBox_PSID.SelectedValue = "0"; 
        DropDownList_JingHanLiang.DataSource = bjinghanliang.GetListsByFilterString("CompID=" + GetCookieCompID());
        DropDownList_JingHanLiang.DataBind(); 
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        } 
        mod.PSID = Convert.ToInt32(ComboBox_PSID.SelectedValue);
        mod.Product_Code = inputProduct_Code.Value.Trim();
        mod.Products_Name = inputProducts_Name.Value.Trim(); 
        mod.Products_Standards = ComboBox_PSID.SelectedItem.Text; 
        mod.ProductJingHanLiang = Convert.ToInt32(DropDownList_JingHanLiang.SelectedValue);
        mod.Products_Summary = txtProducts_Summary.Value.Trim();
        mod.Remarks = HF_ImageURL.Value.Trim();
        mod.Products_date = Convert.ToDateTime("1900-01-01");
        mod.CompID = Convert.ToInt32(GetCookieCompID());
        bll.Insert(mod); 
        Response.Redirect(HF_From.Value);
    } 
}