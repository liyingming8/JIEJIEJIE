using System;
using System.Data;
using System.Web.UI;
using TJ.BLL;
using TJ.DBUtility;
using TJ.Model;
using commonlib;

public partial class CRM_TJ_CRM_BrandInfoAddEdit : AuthorPage
{
    Mtj_crm_brandinfo _mod = new Mtj_crm_brandinfo();
    readonly PGTabExecuteCRM _tab = new PGTabExecuteCRM();
    readonly CommonFunCrm common = new CommonFunCrm();
    private string _tempsqlstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["cmd"]))
            {
                HF_CMD.Value = Sc.DecryptQueryString(Request.QueryString["cmd"].Trim());
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                HF_ID.Value = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
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
                default:
                    break;
            }
        }
    }

    private void FillDdl()
    {
        common.BindTreeCombox(ComboBoxBrandType, "typename", "id", "parentid", "tj_js_brandtype", 0, "类别...", true, "-", "1=1");
        ComboBoxBrandType.SelectedValue = "0";
    }

    private string _temp = "";
    //private string tempsqlstring = "";
    readonly BTJ_RegisterCompanys _btjRegister = new BTJ_RegisterCompanys();
    private void UpdateBrandSupport()
    {
        _temp = _tab.ExecuteQueryForValue("select count(*) as cnt from tj_js_brandsupport where compid=" + GetCookieCompID()).ToString();
        if (_temp.Equals("0"))
        {
            var mod = new Mtj_js_brandsupport(0, int.Parse(GetCookieCompID()), DateTime.Now, false, 0, Convert.ToDateTime("1900-01-01"), "", _btjRegister.GetList(int.Parse(GetCookieCompID())).CompName, "","");
            _tempsqlstring = "INSERT INTO tj_js_brandsupport(compid,joindate,permit,comfirmuserid,confirmdate,refusereson,supporname,remarks) VALUES(" + mod.compid + ",'" + mod.joindate + "'," + mod.permit + "," + mod.comfirmuserid + ",'" + mod.confirmdate + "','" + mod.refusereson + "','" + mod.supporname + "','" + mod.remarks + "')";
            _tab.ExecuteQuery(_tempsqlstring, null);
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            _mod = GetModel(Convert.ToInt32(HF_ID.Value));
        }
        _mod.brandname = inputBrandName.Value.Trim();
        _mod.shortdescription = inputShortDescription.InnerText.Trim();
        _mod.description = TextAreaDescription.Value.Trim();
        _mod.brandlogo = HF_LogoImage.Value.Trim();
        _mod.remarks = inputRemarks.Value.Trim();
        _mod.isvisible = checkboxIsVisible.Checked;
        _mod.btypeid = int.Parse(ComboBoxBrandType.SelectedValue);
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                _mod.compid = Convert.ToInt32(GetCookieCompID());
                _tempsqlstring = "INSERT INTO public.tj_crm_brandinfo(brandname,shortdescription,description,compid,brandlogo,remarks,isvisible,btypeid) VALUES ('" + _mod.brandname + "','" + _mod.shortdescription + "','" + _mod.description + "'," + _mod.compid + ",'" + _mod.brandlogo + "','" + _mod.remarks + "'," + _mod.isvisible + "," + _mod.btypeid + ")  returning id;";
                _tab.ExecuteQuery(_tempsqlstring, null);
                RecordDealLog(new MTJ_DealLog(0, "TJ_CRM_BrandInfoAddEdit.aspx", "TJ_CRM_BrandInfo", "描述", DateTime.Now,int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                _tempsqlstring =
                "UPDATE public.tj_crm_brandinfo SET brandname='" + _mod.brandname + "',shortdescription='" + _mod.shortdescription + "',description='" + _mod.description + "',compid=" + GetCookieCompID() + ",brandlogo='" + _mod.brandlogo + "',remarks='" + _mod.remarks + "',isvisible=" + _mod.isvisible +",btypeid="+_mod.btypeid+" where id=" + _mod.id;
                _tab.ExecuteNonQuery(_tempsqlstring);
                Updateorderinfo(HF_ID.Value, _mod.brandlogo, _mod.brandname);
                RecordDealLog(new MTJ_DealLog(0, "TJ_CRM_BrandInfoAddEdit.aspx", "TJ_CRM_BrandInfo", "描述", DateTime.Now,int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        UpdateBrandSupport();
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void Updateorderinfo(string brandid, string brandimg, string brandinfo)
    {
        _tempsqlstring = "update public.tj_crm_customerorderinfo set brandimg='" +"http://os.china315net.com/CRM/"+ brandimg + "',brandinfo='" + brandinfo +
        "' where brandid=" + brandid;
        _tab.ExecuteNonQuery(_tempsqlstring);
    }

    private void Fillinput(int id)
    {
        Mtj_crm_brandinfo ms = GetModel(id);
        inputBrandName.Value = ms.brandname.Trim();
        inputShortDescription.InnerText = ms.shortdescription.Trim();
        TextAreaDescription.Value = ms.description.Trim();
        HF_LogoImage.Value = ms.brandlogo.Trim();
        Image_Logo.ImageUrl = ms.brandlogo.Trim();
        inputRemarks.Value = ms.remarks.Trim();
        checkboxIsVisible.Checked = ms.isvisible;
        ComboBoxBrandType.SelectedValue = ms.btypeid.ToString();
    }

    private Mtj_crm_brandinfo GetModel(int id)
    {
        DataTable dtTable = _tab.ExecuteQuery("select * from tj_crm_brandinfo where id=" + id, null);
        if (dtTable.Rows.Count > 0)
        {
            return new Mtj_crm_brandinfo(Convert.ToInt32(dtTable.Rows[0]["id"].Equals(DBNull.Value) ? "0" : dtTable.Rows[0]["id"]), dtTable.Rows[0]["brandname"].ToString(), dtTable.Rows[0]["shortdescription"].ToString(), dtTable.Rows[0]["description"].ToString(), Convert.ToInt32(dtTable.Rows[0]["compid"].Equals(DBNull.Value) ? "0" : dtTable.Rows[0]["compid"]), dtTable.Rows[0]["brandlogo"].ToString(), dtTable.Rows[0]["remarks"].ToString(), dtTable.Rows[0]["isvisible"].ToString().ToLower().Equals("true"), Convert.ToInt32(dtTable.Rows[0]["btypeid"].Equals(DBNull.Value) ? "0" : dtTable.Rows[0]["btypeid"]));
        }
        return null;
    }

    /*
     * 删除
     */
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            var deleteId = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
            string sql = "delete from tj_crm_brandinfo where id=" + deleteId;
            DataTable result = _tab.ExecuteNonQuery(sql);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
    }

}