using System;
using System.Data;
using System.Web.UI;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
public partial class CRM_tj_crm_customerapplyAddEdit : AuthorPage
{
    readonly PGTabExecuteCRM _tab = new PGTabExecuteCRM();
    Mtj_crm_customerapply _mod = new Mtj_crm_customerapply();
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

    private Mtj_crm_customerapply GetModel(int id)
    {
        DataTable dtTable = _tab.ExecuteQuery("select * from tj_crm_customerapply where id=" + id, null);
        if (dtTable.Rows.Count > 0)
        {
            return new Mtj_crm_customerapply(Convert.ToInt32(dtTable.Rows[0]["id"]), Convert.ToInt32(dtTable.Rows[0]["compid"]), Convert.ToInt32(dtTable.Rows[0]["brandid"]), Convert.ToInt32(dtTable.Rows[0]["objcompid"]), Convert.ToDateTime(dtTable.Rows[0]["applydate"]), dtTable.Rows[0]["ispermit"].ToString(), dtTable.Rows[0]["refusereson"].ToString(), Convert.ToDateTime(dtTable.Rows[0]["handledate"]), Convert.ToInt32(dtTable.Rows[0]["handleuserid"]), dtTable.Rows[0]["remarks"].ToString());
        }
        return null;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            _mod = GetModel(int.Parse(HF_ID.Value));
        } 
        _mod.compid = Convert.ToInt32(GetCookieCompID());
        _mod.brandid = Convert.ToInt32(inputbrandid.Value.Trim());
        _mod.objcompid = Convert.ToInt32(inputobjcompid.Value.Trim());
        _mod.applydate = Convert.ToDateTime(inputapplydate.Value.Trim());
        _mod.ispermit = inputispermit.Value.Trim();
        _mod.refusereson = inputrefusereson.Value.Trim();
        _mod.handledate = Convert.ToDateTime(inputhandledate.Value.Trim());
        _mod.handleuserid = Convert.ToInt32(inputhandleuserid.Value.Trim());
        _mod.remarks = inputremarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                _tempsqlstring = "INSERT INTO tj_crm_customerapply(compid,brandid,objcompid,applydate,ispermit,refusereson,handledate,handleuserid,remarks) VALUES(" + _mod.compid + "," + _mod.brandid + "," + _mod.objcompid + "," + _mod.applydate + "," + _mod.ispermit + "," + _mod.refusereson + "," + _mod.handledate + "," + _mod.handleuserid + "," + _mod.remarks + ")";
                _tab.ExecuteQuery(_tempsqlstring, null);
                RecordDealLog(new MTJ_DealLog(0, "tj_crm_customerapplyAddEdit.aspx","tj_crm_customerapply", "描述", DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                _tempsqlstring = "UPDATE  tj_crm_customerapply SET compid=" + Convert.ToInt32(_mod.compid) + ",brandid=" + Convert.ToInt32(_mod.brandid) + ",objcompid=" + Convert.ToInt32(_mod.objcompid) + ",applydate=" + Convert.ToDateTime(_mod.applydate) + ",ispermit=" + _mod.ispermit + ",refusereson=" + _mod.refusereson + ",handledate=" + Convert.ToDateTime(_mod.handledate) + ",handleuserid=" + Convert.ToInt32(_mod.handleuserid) + ",remarks=" + _mod.remarks + " where id=mod.id";
                RecordDealLog(new MTJ_DealLog(0, "tj_crm_customerapplyAddEdit.aspx", "tj_crm_customerapply", "描述", DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void Fillinput(int id)
    {
        Mtj_crm_customerapply ms = GetModel(id); 
        inputbrandid.Value = ms.brandid.ToString().Trim();
        inputobjcompid.Value = ms.objcompid.ToString().Trim();
        inputapplydate.Value = ms.applydate.ToString().Trim();
        inputispermit.Value = ms.ispermit.Trim();
        inputrefusereson.Value = ms.refusereson.Trim();
        inputhandledate.Value = ms.handledate.ToString().Trim();
        inputhandleuserid.Value = ms.handleuserid.ToString().Trim();
        inputremarks.Value = ms.remarks.Trim();
    }
    /*
     * 删除
     */
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            var deleteId = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
            string sql = "delete from tj_crm_customerapply where id=" + deleteId;
            DataTable result = _tab.ExecuteNonQuery(sql);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
    }

}