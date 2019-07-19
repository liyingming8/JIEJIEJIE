using System;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Web.UI;


public partial class Admin_TB_Products_Infor_tongyong_AddEdit : AuthorPage
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
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim();
            }
            BindDdl();
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    btnDelete.Style["display"] = "none";//新增界面隐藏删除按钮
                    break;
                case "edit":
                    Button1.Text = "修改";
                    Fillinput(int.Parse(HF_ID.Value.Trim()));
                    break; 
            }
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
        mod.PSID = Convert.ToInt32(string.IsNullOrEmpty(ComboBox_PSID.SelectedValue)?"0":ComboBox_PSID.SelectedValue);
        mod.Product_Code = inputProduct_Code.Value.Trim();
        mod.Products_Name = inputProducts_Name.Value.Trim(); 
        mod.Products_Standards = ComboBox_PSID.SelectedItem.Text;
        mod.ProductJingHanLiang = Convert.ToInt32(string.IsNullOrEmpty(DropDownList_JingHanLiang.SelectedValue) ? "0" : DropDownList_JingHanLiang.SelectedValue);
        mod.Products_Summary = txtProducts_Summary.Value.Trim();
        mod.Remarks = HF_ImageURL.Value.Trim();
        mod.Products_date = Convert.ToDateTime("1900-01-01");  
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.CompID = Convert.ToInt32(GetCookieCompID());
                bll.Insert(mod); 
                break;
            case "edit":
                bll.Modify(mod); 
                break;
        } 
        ClientScript.RegisterStartupScript(GetType(), "reload", "closemyWindow();", true);
    }

    private void Fillinput(int id)
    {
        MTB_Products_Infor ms = bll.GetList(id);  
        ComboBox_PSID.SelectedValue = ms.PSID.ToString().Trim();
        inputProduct_Code.Value = ms.Product_Code.Trim();
        inputProducts_Name.Value = ms.Products_Name.Trim(); 
        DropDownList_JingHanLiang.SelectedValue = ms.ProductJingHanLiang.ToString().Trim();
        txtProducts_Summary.Value = ms.Products_Summary.Trim();   
    }

    /*
     * 删除
     */
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
        {
            int deleteId = Convert.ToInt32(Request.QueryString["ID"].Trim());
            bll.Delete(deleteId);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
    }

}