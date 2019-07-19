using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TB_Products_InforAddEditjg : AuthorPage
{
    private readonly BTB_Products_Infor bll = new BTB_Products_Infor();
    private MTB_Products_Infor mod = new MTB_Products_Infor();
    private BTB_ProducStandards bprostandard = new BTB_ProducStandards();
    private readonly BTB_ProductJingHanLiang bjinghanliang = new BTB_ProductJingHanLiang();
    private readonly BTB_ProductJiuJingDu bjiujingdu = new BTB_ProductJiuJingDu();
    private readonly BTB_ProductXiangXing bxiangxing = new BTB_ProductXiangXing();
    private readonly BTB_BiaoZhun bbiaozhun = new BTB_BiaoZhun();
    private readonly BTB_Metries byuanliao = new BTB_Metries();
    private readonly CommonFunWL commfun = new CommonFunWL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
                //if(HF_CMD.Value.ToLower().Trim()=="add")
                //{
                //    if (GetCookieCompID() == "10259" && GetCookieCompID() == "131")
                //    {
                //        inputProduct_Code.value="";
                //    }
                //    else
                //    {
                //        inputProduct_Code.value = "";
                //       // inputProduct_Code.Value = commfun.CreateAutoID(GetCookieCompID(), "P");
                //    }
                //}
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
                    fillinput(int.Parse(HF_ID.Value.Trim()));
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
        DropDownList_BiaoZhun.DataSource = bbiaozhun.GetListsByFilterString("CompID=" + GetCookieCompID());
        DropDownList_BiaoZhun.DataBind();
        DropDownList_YuanLiao.DataSource = byuanliao.GetListsByFilterString("CompID=" + GetCookieCompID());
        DropDownList_YuanLiao.DataBind();
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
        mod.Products_Standards = ComboBox_PSID.SelectedItem.Text;
        mod.ProductXiangXing = Convert.ToInt32(DropDownList_XiangXing.SelectedValue);
        mod.ProductJiuJingDu = Convert.ToInt32(DropDownList_JiuJingDu.SelectedValue);
        mod.MTID = Convert.ToInt32(DropDownList_YuanLiao.SelectedValue);
        mod.BZID = Convert.ToInt32(DropDownList_BiaoZhun.SelectedValue);
        mod.ProductJingHanLiang = Convert.ToInt32(DropDownList_JingHanLiang.SelectedValue);
        mod.Products_Summary = txtProducts_Summary.Text.Trim();
        mod.Remarks = inputRemarks.Value.Trim();
        mod.Products_date = Convert.ToDateTime("1900-01-01");
        mod.IsOwn = CheckBox_IsOwn.Checked;
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
        Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
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
        DropDownList_JiuJingDu.SelectedValue = ms.ProductJiuJingDu.ToString().Trim();
        DropDownList_XiangXing.SelectedValue = ms.ProductXiangXing.ToString().Trim();
        DropDownList_JingHanLiang.SelectedValue = ms.ProductJingHanLiang.ToString().Trim();
        txtProducts_Summary.Text = ms.Products_Summary.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        CheckBox_IsOwn.Checked = ms.IsOwn;
        DropDownList_YuanLiao.SelectedValue = ms.MTID.ToString();
        DropDownList_BiaoZhun.SelectedValue = ms.BZID.ToString();
    }
}