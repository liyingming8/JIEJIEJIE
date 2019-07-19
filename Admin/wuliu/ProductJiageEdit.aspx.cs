using System;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_wuliu_ProductJiageEdit : AuthorPage
{
    private readonly BTB_Products_Infor bproduct = new BTB_Products_Infor();
    private MTB_Products_Infor mproduct = new MTB_Products_Infor();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["prod_id"] != null && Request.QueryString["prod_id"].Trim().Length > 0)
            {
                HF_ProductID.Value = Request.QueryString["prod_id"].Trim();
                mproduct = bproduct.GetList(int.Parse(HF_ProductID.Value));
                lab_productname.Text = mproduct.Products_Name;
                txt_Price.Text =
                    Convert.ToDecimal(mproduct.Products_Price.Trim().Equals("") ? "0" : mproduct.Products_Price.Trim())
                        .ToString("0.00");
            }
        }
    }

    protected void Button_OK_Click(object sender, EventArgs e)
    {
        mproduct = bproduct.GetList(int.Parse(HF_ProductID.Value));
        mproduct.Products_Price = txt_Price.Text;
        bproduct.Modify(mproduct);
        ClientScript.RegisterStartupScript(GetType(), "reload", "window.opener.location.reload();window.close();", true);
    }
}