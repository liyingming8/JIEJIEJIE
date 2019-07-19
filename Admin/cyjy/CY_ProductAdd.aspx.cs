using System;
using System.Data;
using commonlib;
public partial class CY_ProductAdd : AuthorPage
{

    DB375 db = new DB375();

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
        string id = "";
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
          id = HF_ID.Value.Trim();
        }
        string uid = GetCookieUID();
        string name = Pname.Value.Trim();
        decimal price = decimal.Parse(Price.Value.Trim());   
        bool use = CheckBox_Use.Checked;
        string img = HF_ImageURL.Value.Trim();
        string imgs = HF_ImageURL.Value.Trim().Insert(HF_ImageURL.Value.Trim().LastIndexOf('/') + 1, "sm_");
    
        string   Remarks = inputRemarks.Value.Trim();

        switch (HF_CMD.Value.Trim())
        {
            case "add":

                db.InProduct(name,price,img,use);
                
                break;
            case "edit":
                db.UpdteProduct("ProName=" + name + ",Price='" + price + "',ProImg='" + img + "',UseFlag='" + use + "'", "ProID=" + id + "");
                break;
        }
        Response.Write("<script>alert('操作成功！');window.location.href='CY_Product.aspx'</script>");
    }

    private void fillinput(int id)
    {
        DataTable dt = db.GetProInfo("ProID=" + id + "");
        Pname.Value = dt.Rows[0]["ProName"].ToString();
        Price.Value = dt.Rows[0]["Price"].ToString();
        CheckBox_Use.Checked = bool.Parse(dt.Rows[0]["UseFlag"].ToString());
        Image_GoodPic.ImageUrl = dt.Rows[0]["ProImg"].ToString();
        HF_ImageURL.Value = dt.Rows[0]["ProImg"].ToString();

  
    }

    private void FillDLL()
    {
        //DataTable dpro = new DataTable();

        //dpro = db.GetProInfo("UseFlag=1");

        //ComboBox_WLProduct.DataSource = dpro;

        //ComboBox_WLProduct.DataBind();
        //ComboBox_WLProduct.SelectedValue = "0";

    }


    
}