using System;
using System.Data;
using System.Web.UI;
using TJ.DBUtility;
using TJ.Model; 
using commonlib;
public partial class CRM_tj_crm_unitinfoAddEdit : AuthorPage
{
    Mtj_crm_unitinfo mod = new Mtj_crm_unitinfo();
    readonly PGTabExecuteCRM tabcrm = new PGTabExecuteCRM();
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
                    fillinput(int.Parse(HF_ID.Value.Trim()));
                    break;
                default:
                    break;
            }
        }
    }

    private Mtj_crm_unitinfo GetModel(int id)
    {
        DataTable dtTable = tabcrm.ExecuteQuery("select * from tj_crm_unitinfo where id=" + id, null);
        if (dtTable.Rows.Count > 0)
        {
            return new Mtj_crm_unitinfo(Convert.ToInt32(dtTable.Rows[0]["id"]), dtTable.Rows[0]["unitname"].ToString(), dtTable.Rows[0]["remarks"].ToString(), Convert.ToInt32(dtTable.Rows[0]["compid"]),"");
        }
        return null;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = GetModel(Convert.ToInt32(HF_ID.Value));
        }
        mod.unitname = inputunitname.Value.Trim();
        mod.remarks = inputremarks.Value.Trim();
        mod.compid = Convert.ToInt32(GetCookieCompID());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                //bll.Insert(mod);
                tabcrm.ExecuteQuery("insert into tj_crm_unitinfo(unitname,remarks,compid) values('" + mod.unitname + "','" + mod.remarks + "'," + mod.compid + ") returning id", null);
                RecordDealLog(new MTJ_DealLog(0, "tj_crm_unitinfoAddEdit.aspx", "tj_crm_unitinfo", "描述", DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                //bll.Modify(mod);
                tabcrm.ExecuteQuery("update tj_crm_unitinfo set unitname='" + mod.unitname + "',remarks='" + mod.remarks + "',compid=" + mod.compid + " where id=" + mod.id, null);
                RecordDealLog(new MTJ_DealLog(0, "tj_crm_unitinfoAddEdit.aspx", "tj_crm_unitinfo", "描述", DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void fillinput(int id)
    {
        Mtj_crm_unitinfo ms = GetModel(id);
        inputunitname.Value = ms.unitname.Trim();
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
            string sql = "delete from tj_crm_unitinfo where id=" + deleteId;
            DataTable result = tabcrm.ExecuteNonQuery(sql);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
    }

}