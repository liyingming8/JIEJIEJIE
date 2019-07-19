using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_SWM_CommonSuyuanAddEdit : AuthorPage
{
    BTJ_SWM_CommonSuyuan bll = new BTJ_SWM_CommonSuyuan();
    MTJ_SWM_CommonSuyuan mod = new MTJ_SWM_CommonSuyuan();
    BTB_Products_Infor btjGoods = new BTB_Products_Infor(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["cmd"]))
            {
                HF_CMD.Value = Sc.DecryptQueryString(Request.QueryString["cmd"].Trim());
            }
            if (HF_CMD.Value.ToLower().Equals("add"))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
                {
                    HF_PID.Value = Sc.DecryptQueryString(Request.QueryString["pid"]);
                    HF_ProdID.Value = Sc.DecryptQueryString(Request.QueryString["prodid"]);
                    MTB_Products_Infor mgood = btjGoods.GetList(int.Parse(HF_ProdID.Value));
                    Label_goodsname.Text = mgood.Products_Name;
                    hf_product_code.Value = mgood.Product_Code;
                    inputproddate.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                    inputcheckdate.Value = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd");
                    inputoutdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    Response.End();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    HF_ID.Value = Sc.DecryptQueryString(Request.QueryString["ID"].Trim()); 
                    Button1.Text = "修改";
                    fillinput(int.Parse(HF_ID.Value.Trim()));
                    MTB_Products_Infor mgood = btjGoods.GetList(int.Parse(HF_ProdID.Value));
                    Label_goodsname.Text = mgood.Products_Name;
                    hf_product_code.Value = mgood.Product_Code;
                }
            } 
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.compid = Convert.ToInt32(GetCookieCompID());
        mod.pid = Convert.ToInt32(HF_PID.Value);
        mod.prodid = Convert.ToInt32(HF_ProdID.Value);
        mod.prodname = Label_goodsname.Text;
        mod.prodnumber = hf_product_code.Value;
        mod.materials = inputmaterials.Value.Trim();
        mod.proddate = Convert.ToDateTime(inputproddate.Value.Trim());
        mod.checkdate = Convert.ToDateTime(inputcheckdate.Value.Trim());
        mod.checkuser = inputcheckuser.Value.Trim();
        mod.checkreport = HF_ReportImage.Value;
        mod.outdate = Convert.ToDateTime(inputoutdate.Value.Trim());
        mod.prodcompname = input_procompname.Value.Trim();
        mod.prodaddress = input_proaddress.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_SWM_CommonSuyuanAddEdit.aspx", "TJ_SWM_CommonSuyuan", "描述", System.DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                bll.Modify(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_SWM_CommonSuyuanAddEdit.aspx", "TJ_SWM_CommonSuyuan", "描述", System.DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "reload", "closemyWindow();", true);
    }

    private void fillinput(int id)
    {
        MTJ_SWM_CommonSuyuan ms = bll.GetList(id);
        HF_ProdID.Value = ms.prodid.ToString();
        HF_PID.Value = ms.pid.ToString(); 
        inputmaterials.Value = ms.materials.Trim();
        inputproddate.Value = ms.proddate.ToString("yyyy-MM-dd").Trim();
        inputcheckdate.Value = ms.checkdate.ToString("yyyy-MM-dd").Trim();
        inputcheckuser.Value = ms.checkuser.Trim();
        HF_ReportImage.Value = ms.checkreport.Trim();
        inputoutdate.Value = ms.outdate.ToString("yyyy-MM-dd").Trim();
        Image_showlogo.ImageUrl = ms.checkreport.Trim();
        input_procompname.Value = ms.prodcompname.Trim();
        input_proaddress.Value = ms.prodaddress;
    }
}