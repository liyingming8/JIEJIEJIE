using System; 
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using commonlib;
using TJ.DBUtility;

public partial class CRM_ChooseModels : AuthorPage
{
    readonly PGTabExecuteCRM _pgTabExecute = new PGTabExecuteCRM();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDdl();
            DataTable dtTable = _pgTabExecute.ExecuteNonQuery("select * from tj_page_model");
            rpt_models.DataSource = dtTable;
            rpt_models.DataBind(); 
        }
    }

    private void FillDdl()
    {
        DDLCustomerGrade.DataSource =
            _pgTabExecute.ExecuteQuery(
                "select id,gradename from tj_crm_customergrade where compid=" + GetCookieCompID() + " order by gradeorder", null);
        DDLCustomerGrade.DataBind();
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (DDLCustomerGrade.SelectedValue.Equals("0"))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "alert", "alert('请指定经销商级别！');", true);
            return;
        }
        if (rpt_models.Items.Count > 0)
        {
            CheckBox cb;
            string id;
            string type;
            foreach (RepeaterItem item in rpt_models.Items)
            {
                cb = item.FindControl("ckb") as CheckBox;
                id = ((HiddenField)item.FindControl("hfid")).Value;  
                if (cb.Checked)
                {
                    type = "add";
                }
                else
                {
                    type = "del";
                }
                Record(id, type,DDLCustomerGrade.SelectedValue);
            }
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('授权成功！');", true);
        } 
    }

    private string _tempvalue;
    private void Record(string id,string type,string gradeid)
    {
        _tempvalue = _pgTabExecute.ExecuteQueryForValue("select count(id) as num from tj_page_comp_info where custgid=" + gradeid + " and mdid=" + id + " and compid=" + GetCookieCompID()).ToString();
        if (type.Equals("add"))
        { 
            if (_tempvalue.Equals("0"))
            {
                _pgTabExecute.ExecuteNonQuery("insert into tj_page_comp_info(mdid,compid,custgid) values(" + id + "," + GetCookieCompID() + "," + gradeid + ")");
            } 
        }
        else
        {
            if (!_tempvalue.Equals("0"))
            {
                _pgTabExecute.ExecuteNonQuery("delete from tj_page_comp_info where custgid="+gradeid+" and mdid=" + id + " and compid=" + GetCookieCompID());
            }
        }
    }

    private object tempvalue = "";
    protected void rpt_models_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            CheckBox hgc = (CheckBox)e.Item.FindControl("ckb");
            HiddenField hfid = (HiddenField)e.Item.FindControl("hfid");
            HiddenField hfneed = (HiddenField)e.Item.FindControl("hfneed");
            if (hfneed.Value.Equals("True"))
            { 
                hgc.Checked = true;
                hgc.Enabled = false;
            }
            else
            {
                tempvalue = _pgTabExecute.ExecuteQueryForValue("select count(*) from tj_page_comp_info where custgid="+DDLCustomerGrade.SelectedValue+" and mdid=" + hfid.Value +" and compid=" + GetCookieCompID());
                if (int.Parse(tempvalue.ToString()) > 0)
                {
                    hgc.Checked = true;
                }
            }
        }  
    }
     DataTable _dttemp=new DataTable();
    protected void DDLCustomerGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!DDLCustomerGrade.SelectedValue.Equals("0"))
        {
            hf_authordedmdid_str.Value = string.Empty;
            _dttemp = _pgTabExecute.ExecuteQuery("select mdid from tj_page_comp_info where compid=" +
                                                   GetCookieCompID() + " and custgid=" + DDLCustomerGrade.SelectedValue,null);
            if (_dttemp.Rows.Count>0)
            {
                foreach (DataRow row in _dttemp.Rows)
                {
                    if (hf_authordedmdid_str.Value.Length.Equals(0))
                    {
                        hf_authordedmdid_str.Value = row[0].ToString();
                    }
                    else
                    {
                        hf_authordedmdid_str.Value += "," + row[0];
                    }
                }
            }
            Autoselect(hf_authordedmdid_str.Value); 
        }
        else
        {
            foreach (RepeaterItem rptModel in rpt_models.Items)
            {
                var cb = rptModel.FindControl("ckb") as CheckBox; 
                if (cb.Enabled)
                {
                    cb.Checked = false;
                }
                else
                {
                    cb.Checked = true;
                }
            }
        }
    }

    private void Autoselect(string chileagentstring)
    {
        if (chileagentstring.Length > 0)
        {
            foreach (RepeaterItem rptModel in rpt_models.Items)
            {
                var cb = rptModel.FindControl("ckb") as CheckBox;
                string id = ((HiddenField)rptModel.FindControl("hfid")).Value;
                if (("," + chileagentstring + ",").Contains("," + id + ","))
                {
                    cb.Checked = true;
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }
        else
        {
            foreach (RepeaterItem rptModel in rpt_models.Items)
            {
                var cb = rptModel.FindControl("ckb") as CheckBox; 
                if (cb.Enabled)
                {
                    cb.Checked = false;
                }
                else
                {
                    cb.Checked = true;
                }
            }
        }
    }
}