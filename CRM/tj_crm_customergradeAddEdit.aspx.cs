using System;
using System.Data;
using System.Web.UI;
using TJ.Model;
using TJ.DBUtility;
using commonlib;
public partial class crm_tj_crm_customergradeAddEdit : AuthorPage
{
    readonly PGTabExecuteCRM _tab = new PGTabExecuteCRM();
    Mtj_crm_customergrade _mod = new Mtj_crm_customergrade();
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

    private Mtj_crm_customergrade GetModel(int id)
    {
        DataTable dtTable = _tab.ExecuteQuery("select * from tj_crm_customergrade where id=" + id, null);
        if (dtTable.Rows.Count > 0)
        {
            return new Mtj_crm_customergrade(Convert.ToInt32(dtTable.Rows[0]["id"]), dtTable.Rows[0]["gradename"].ToString(), Convert.ToInt32(dtTable.Rows[0]["compid"]), dtTable.Rows[0]["remarks"].ToString(), Convert.ToInt32(dtTable.Rows[0]["gradeorder"].Equals(DBNull.Value) ? "0" : dtTable.Rows[0]["gradeorder"]), dtTable.Rows[0]["idguid"].ToString(), false, "", false);
        }
        return null;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            _mod = GetModel(Convert.ToInt32(HF_ID.Value));
        }
        _mod.gradename = inputgradename.Value.Trim();
        _mod.compid = Convert.ToInt32(GetCookieCompID());
        _mod.gradeorder = Convert.ToInt32(string.IsNullOrEmpty(inputgradeorder.Value) ? "0" : inputgradeorder.Value);
        _mod.firstcheck = ckb_firstcheck.Checked;
        _mod.blicenses = ckb_blicenses.Checked;
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                _tempsqlstring = "INSERT INTO tj_crm_customergrade(gradename,compid,remarks,gradeorder,firstcheck,blicenses) VALUES('" + _mod.gradename + "'," + _mod.compid + ",'" + _mod.remarks + "'," + _mod.gradeorder + "," + _mod.firstcheck + "," + _mod.blicenses + ")";
                _tab.ExecuteQuery(_tempsqlstring, null);
                RecordDealLog(new MTJ_DealLog(0, "tj_crm_customergradeAddEdit.aspx", "tj_crm_customergrade", "描述", DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                _tempsqlstring = "UPDATE  tj_crm_customergrade SET gradename='" + _mod.gradename + "',compid=" + Convert.ToInt32(_mod.compid) + ",remarks='" + _mod.remarks + "',gradeorder=" + _mod.gradeorder + ",firstcheck=" + _mod.firstcheck + ",blicenses=" + _mod.blicenses + " where id=" + _mod.id;
                _tab.ExecuteNonQuery(_tempsqlstring);
                RecordDealLog(new MTJ_DealLog(0, "tj_crm_customergradeAddEdit.aspx", "tj_crm_customergrade", "描述", DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void Fillinput(int id)
    {
        Mtj_crm_customergrade ms = GetModel(id);
        inputgradename.Value = ms.gradename.Trim();
        inputgradeorder.Value = ms.gradeorder.ToString();
        inputremarks.Value = ms.remarks.Trim();
        ckb_firstcheck.Checked = ms.firstcheck;
        ckb_blicenses.Checked = ms.blicenses;
    }


    /*
     * 删除
     */
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            var deleteId = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
            string sql = "delete from tj_crm_customergrade where id=" + deleteId;
            DataTable result = _tab.ExecuteNonQuery(sql);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
    }
}