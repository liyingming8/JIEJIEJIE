using System;
using System.Data;
using System.Web.UI;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
public partial class CRM_tj_crm_customercommissionAddEdit : AuthorPage
{
    Mtj_crm_customercommission mod = new Mtj_crm_customercommission();
    readonly PGTabExecuteCRM tab = new PGTabExecuteCRM();
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

    private Mtj_crm_customercommission GetModel(int id)
    {
        DataTable dtTable = tab.ExecuteQuery("select * from tj_crm_customercommission where id=" + id, null);
        if (dtTable.Rows.Count > 0)
        {
            return new Mtj_crm_customercommission(Convert.ToInt32(dtTable.Rows[0]["id"]), Convert.ToInt32(dtTable.Rows[0]["orderid"]), Convert.ToInt32(dtTable.Rows[0]["ordercid"]), Convert.ToInt32(dtTable.Rows[0]["getcid"]), Convert.ToDateTime(dtTable.Rows[0]["orderdate"].ToString()), Convert.ToDecimal(dtTable.Rows[0]["ordertotalprice"]), Convert.ToDecimal(dtTable.Rows[0]["returnvalue"]), Convert.ToInt32(dtTable.Rows[0]["compid"]), dtTable.Rows[0]["remarks"].ToString());
        }
        return null;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = GetModel(Convert.ToInt32(HF_ID.Value));
        }
        mod.id = Convert.ToInt32(inputid.Value.Trim());
        mod.orderid = Convert.ToInt32(inputorderid.Value.Trim());
        mod.ordercid = Convert.ToInt32(inputordercustomerid.Value.Trim());
        mod.getcid = Convert.ToInt32(inputparentcustomerid.Value.Trim()); 
        mod.ordertotalprice = Convert.ToDecimal(inputordertotalprice.Value.Trim());
        mod.returnvalue = Convert.ToDecimal(inputreturnvalue.Value.Trim());
        mod.compid = Convert.ToInt32(inputcompid.Value.Trim());
        mod.remarks = inputremarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.orderdate = DateTime.Now;
                _tempsqlstring = "INSERT INTO tj_crm_customercommission(orderid,ordercustomerid,parentcustomerid,orderdate,ordertotalprice,returnvalue,compid,remarks) VALUES(" + mod.orderid + "," + mod.ordercid + "," + mod.getcid + "," + mod.orderdate + "," + mod.ordertotalprice + "," + mod.returnvalue + "," + mod.compid + "," + mod.remarks + ")";
                tab.ExecuteQuery(_tempsqlstring, null);
                RecordDealLog(new MTJ_DealLog(0, "tj_crm_customercommissionAddEdit.aspx", "tj_crm_customercommission", "描述", DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                _tempsqlstring = "UPDATE  tj_crm_customercommission SET orderid=" + Convert.ToInt32(mod.orderid) + ",ordercid=" + Convert.ToInt32(mod.ordercid) + ",getcid=" + Convert.ToInt32(mod.getcid) + ",orderdate=" + mod.orderdate + ",ordertotalprice=" + Convert.ToDecimal(mod.ordertotalprice) + ",returnvalue=" + Convert.ToDecimal(mod.returnvalue) + ",compid=" + Convert.ToInt32(mod.compid) + ",remarks=" + mod.remarks + " where id=mod.id";
                tab.ExecuteNonQuery(_tempsqlstring);
                RecordDealLog(new MTJ_DealLog(0, "tj_crm_customercommissionAddEdit.aspx", "tj_crm_customercommission", "描述", DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void fillinput(int id)
    {
        Mtj_crm_customercommission ms = GetModel(id);
        inputid.Value = ms.id.ToString().Trim();
        inputorderid.Value = ms.orderid.ToString().Trim();
        inputordercustomerid.Value = ms.ordercid.ToString().Trim();
        inputparentcustomerid.Value = ms.getcid.ToString().Trim();
        inputorderdate.Value = ms.orderdate.ToString("yyyy-MM-dd");
        inputordertotalprice.Value = ms.ordertotalprice.ToString().Trim();
        inputreturnvalue.Value = ms.returnvalue.ToString().Trim();
        inputcompid.Value = ms.compid.ToString().Trim();
        inputremarks.Value = ms.remarks.Trim();
    }
}