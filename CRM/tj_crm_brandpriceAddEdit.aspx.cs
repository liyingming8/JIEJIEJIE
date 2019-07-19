using System;
using System.Data;
using System.Web.UI;
using TJ.Model;
using TJ.DBUtility;
using commonlib;
public partial class CRM_tj_crm_brandpriceAddEdit : AuthorPage
{
    readonly PGTabExecuteCRM _tab = new PGTabExecuteCRM();
    Mtj_crm_brandprice _mod = new Mtj_crm_brandprice();
    string _tempsqlstring = "";
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
        ddl_brandinfo.DataSource = _tab.ExecuteQuery("select id,brandname from tj_crm_brandinfo where compid=" + GetCookieCompID(),
            null);
        ddl_brandinfo.DataBind();
        ddlunit.DataSource =
            _tab.ExecuteQuery("select id,unitname from tj_crm_unitinfo where compid=" + GetCookieCompID(), null);
        ddlunit.DataBind();
        DDL_CGrade.DataSource =
            _tab.ExecuteQuery(
                "select id,gradename from tj_crm_customergrade where compid=" + GetCookieCompID() +
                " order by gradeorder", null);
        DDL_CGrade.DataBind();
    }

    private Mtj_crm_brandprice GetModel(int id)
    {
        DataTable dtTable = _tab.ExecuteQuery("select * from tj_crm_brandprice where id=" + id, null);
        if (dtTable.Rows.Count > 0)
        {
            return new Mtj_crm_brandprice(Convert.ToInt32(dtTable.Rows[0]["id"]), Convert.ToInt32(dtTable.Rows[0]["brandid"]), Convert.ToDecimal(dtTable.Rows[0]["orignalprice"]), Convert.ToDecimal(dtTable.Rows[0]["commissionvalue"]), Convert.ToDecimal(dtTable.Rows[0]["parentrcommissionpercent"]), Convert.ToDateTime(dtTable.Rows[0]["inputdate"]), Convert.ToInt32(dtTable.Rows[0]["inputuserid"]), Convert.ToDateTime(dtTable.Rows[0]["lastupdatedate"]), Convert.ToInt32(dtTable.Rows[0]["lastupdateuserid"]), Convert.ToInt32(dtTable.Rows[0]["unitid"]), dtTable.Rows[0]["remarks"].ToString(), Convert.ToInt32(dtTable.Rows[0]["compid"]), Convert.ToInt32(dtTable.Rows[0]["cgradeid"]), Convert.ToInt32(dtTable.Rows[0]["startvalue"]), dtTable.Rows[0]["unitname"].ToString(), Convert.ToInt32(dtTable.Rows[0]["commisiongradenumber"]));
        }
        return null;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            _mod = GetModel(Convert.ToInt32(HF_ID.Value));
        }
        _mod.brandid = Convert.ToInt32(ddl_brandinfo.SelectedValue);
        _mod.orignalprice = Convert.ToDecimal(inputorignalprice.Value.Trim());
        _mod.commissionvalue = Convert.ToDecimal(inputcommissionvalue.Value.Trim());
        _mod.parentrcommissionpercent = Convert.ToDecimal(inputparentrcommissionpercent.Value.Trim());
        _mod.unitid = Convert.ToInt32(ddlunit.SelectedValue.Trim());
        _mod.remarks = inputremarks.Value.Trim();
        _mod.cgradeid = Convert.ToInt32(DDL_CGrade.SelectedValue);
        _mod.startvalue = Convert.ToInt32(input_startvalue.Value);
        _mod.unitname = ddlunit.SelectedItem.Text.Trim();
        _mod.commisiongradenumber = Convert.ToInt32(inputcommisonlevelnum.Value);
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                _mod.inputdate = DateTime.Now;
                _mod.inputuserid = Convert.ToInt32(GetCookieUID());
                _mod.compid = Convert.ToInt32(GetCookieCompID());
                _tempsqlstring = "INSERT INTO tj_crm_brandprice(brandid,orignalprice,commissionvalue,parentrcommissionpercent,inputdate,inputuserid,lastupdatedate,lastupdateuserid,unitid,remarks,compid,cgradeid,startvalue,unitname,commisiongradenumber) VALUES(" + _mod.brandid + "," + _mod.orignalprice + "," + _mod.commissionvalue + "," + _mod.parentrcommissionpercent / 100 + ",'" + _mod.inputdate + "'," + _mod.inputuserid + ",'" + _mod.lastupdatedate + "'," + _mod.lastupdateuserid + "," + _mod.unitid + ",'" + _mod.remarks + "'," + _mod.compid + "," + _mod.cgradeid + "," + _mod.startvalue + ",'" + _mod.unitname + "'," + _mod.commisiongradenumber + ")";
                _tab.ExecuteQuery(_tempsqlstring, null);
                RecordDealLog(new MTJ_DealLog(0, "tj_crm_brandpriceAddEdit.aspx", "tj_crm_brandprice", "描述", DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                _mod.lastupdatedate = DateTime.Now;
                _mod.lastupdateuserid = Convert.ToInt32(GetCookieUID());
                _tempsqlstring = "UPDATE  tj_crm_brandprice SET brandid=" + Convert.ToInt32(_mod.brandid) + ",orignalprice=" + Convert.ToDecimal(_mod.orignalprice) + ",commissionvalue=" + Convert.ToDecimal(_mod.commissionvalue) + ",parentrcommissionpercent=" + Convert.ToDecimal(_mod.parentrcommissionpercent) / 100 + ",inputdate='" + Convert.ToDateTime(_mod.inputdate) + "',inputuserid=" + Convert.ToInt32(_mod.inputuserid) + ",lastupdatedate='" + Convert.ToDateTime(_mod.lastupdatedate) + "',lastupdateuserid=" + Convert.ToInt32(_mod.lastupdateuserid) + ",unitid=" + Convert.ToInt32(_mod.unitid) + ",remarks='" + _mod.remarks + "',compid=" + _mod.compid + ",cgradeid=" + _mod.cgradeid + ",startvalue=" + _mod.startvalue + ",unitname='" + _mod.unitname + "',commisiongradenumber=" + _mod.commisiongradenumber + " where id=" + _mod.id;
                _tab.ExecuteQuery(_tempsqlstring,null);
                RecordDealLog(new MTJ_DealLog(0, "tj_crm_brandpriceAddEdit.aspx", "tj_crm_brandprice", "描述", DateTime.Now, int.Parse(GetCookieRID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void Fillinput(int id)
    {
        Mtj_crm_brandprice ms = GetModel(id);
        ddl_brandinfo.Enabled = false;
        ddl_brandinfo.SelectedValue = ms.brandid.ToString().Trim(); 
        inputorignalprice.Value = ms.orignalprice.ToString().Trim();
        inputcommissionvalue.Value = ms.commissionvalue.ToString().Trim();
        inputparentrcommissionpercent.Value = (ms.parentrcommissionpercent*100).ToString().Trim();
        ddlunit.SelectedValue = ms.unitid.ToString().Trim();
        DDL_CGrade.SelectedValue = ms.cgradeid.ToString();
        inputremarks.Value = ms.remarks.Trim();
        input_startvalue.Value = ms.startvalue.ToString();
        inputcommisonlevelnum.Value = ms.commisiongradenumber.ToString();
    }

    /*
         * 删除
         */
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            var deleteId = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
            string sql = "delete from tj_crm_brandprice where id=" + deleteId;
            DataTable result = _tab.ExecuteNonQuery(sql);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
    }
}