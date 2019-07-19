using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
using TJ.DBUtility;
using System.Data;


public partial class Admin_TJ_AwardInfoAddEdit : AuthorPage
{
    private readonly BTJ_AwardInfo bll = new BTJ_AwardInfo();
    //private readonly BTJ_Integral bintegral = new BTJ_Integral();
    private MTJ_AwardInfo mod = new MTJ_AwardInfo();
    readonly TabExecute _tab = new TabExecute();

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
            FillDdl();
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

    private readonly BTJ_AwardType _btjAwardType = new BTJ_AwardType(); 
    private void FillDdl()
    {
        DDL_AwardType.DataSource =
            _btjAwardType.GetListsByFilterString("id not in (1,2) and compid in (0," + GetCookieCompID() + ")");
        DDL_AwardType.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.ToLower().Trim().Equals("edit"))
        {
            mod = bll.GetList(int.Parse(HF_ID.Value));
        }
        mod.CompID = int.Parse(GetCookieCompID());
        mod.AwardThing = inputAwardThing.Value.Trim();
        mod.ImageURLString = HF_ImageUrl.Value;
        if (HF_ImageUrl.Value.Contains("/"))
        {
            mod.SImageURLString = HF_ImageUrl.Value.Trim().Insert(HF_ImageUrl.Value.Trim().LastIndexOf('/') + 1, "sm_");
        }
        mod.Contents = inputContents.Value.Trim();
        mod.IntegralValue = Convert.ToInt32(txtIntegralValue.Value);
        mod.CashValue = Convert.ToDecimal(input_price.Value);
        mod.Remarks = inputRemarks.Value.Trim();
        mod.AwardType = int.Parse(DDL_AwardType.SelectedValue);
        mod.ExchangeCash = ckb_exchangexianjin.Checked;
        mod.ExchangeIntegral = ckb_exchangebyjifen.Checked;
        mod.IsActive = CKB_IsActive.Checked;
        mod.faceto = int.Parse(string.IsNullOrEmpty(ddl_faceto.SelectedValue) ? "0" : ddl_faceto.SelectedValue);
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.PublishDate = DateTime.Now;
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void Fillinput(int id)
    {
        MTJ_AwardInfo ms = bll.GetList(id);
        //Comb_AGID.SelectedValue = ms.AGID.ToString().Trim();
        inputAwardThing.Value = ms.AwardThing.Trim();
        HF_ImageUrl.Value = ms.ImageURLString;
        Image_AwardUrl.ImageUrl = ms.ImageURLString;
        inputContents.Value = ms.Contents.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        txtIntegralValue.Value = ms.IntegralValue.ToString();
        DDL_AwardType.SelectedValue = ms.AwardType.ToString();
        ckb_exchangebyjifen.Checked = ms.ExchangeIntegral;
        ckb_exchangexianjin.Checked = ms.ExchangeCash;
        CKB_IsActive.Checked = ms.IsActive;
        input_price.Value = ms.CashValue.ToString();
        ddl_faceto.SelectedValue = ms.faceto.ToString();
    }

    /*
   * 删除
   */
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
        {
            var deleteId = Request.QueryString["ID"];
            string sql = "delete from TJ_AwardInfo where awid=" + deleteId;
            DataTable result = _tab.ExecuteNonQuery(sql);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
    }
}