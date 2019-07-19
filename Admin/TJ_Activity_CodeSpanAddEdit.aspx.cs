using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_Activity_CodeSpanAddEdit : AuthorPage
{
    readonly BTJ_Activity _bll = new BTJ_Activity();
    MTJ_Activity _mod = new MTJ_Activity();
    protected void Page_Load(object sender, EventArgs e)
    {
        HF_CMD.Value = "edit";
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["acid"]))
            {
                if (GetCookieRID().Equals("155"))
                {
                    notswm.Visible = false;
                }
                hf_acid.Value = Sc.DecryptQueryString(Request.QueryString["acid"]);
                _mod = _bll.GetList(int.Parse(hf_acid.Value));
                inputacid.Value = Sc.DecryptQueryString(Request.QueryString["acnm"]);
                Fillinput(int.Parse(hf_acid.Value.Trim()));
            }
            else
            {
                Response.End();
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            _mod = _bll.GetList(Convert.ToInt32(hf_acid.Value));
        }

        _mod.khddh = ddl_dingdancode.SelectedValue.Equals("0")?"": ddl_dingdancode.SelectedValue.Trim();
        _mod.anyfahuodate = CheckBox_AnyFHDate.Checked?1:0;
        _mod.sfahuodate =
            Convert.ToDateTime(inputfhsdate.Value.Trim().Equals("") ? "1900-01-01" : inputfhsdate.Value.Trim());
        _mod.efahuodate =
            Convert.ToDateTime(inputfhedate.Value.Trim().Equals("") ? "1900-01-01" : inputfhedate.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                _bll.Insert(_mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_Activity_CodeSpanAddEdit.aspx", "TJ_Activity_CodeSpan", "描述",
                    DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                _bll.Modify(_mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_Activity_CodeSpanAddEdit.aspx", "TJ_Activity_CodeSpan", "描述",
                    DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }

        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void Fillinput(int id)
    {
        _mod = _bll.GetList(id);
        hf_acid.Value = _mod.id.ToString(); 
        inputacid.Value = _mod.AName.Trim();
        ddl_dingdancode.SelectedValue = _mod.khddh.Trim();
        CheckBox_AnyFHDate.Checked = _mod.anyfahuodate > 0;
        if (_mod.anyfahuodate > 0)
        {
            inputfhsdate.Value = "1900-01-01";
            inputfhedate.Value = "1900-01-01";
            inputfhsdate.Disabled = true;
            inputfhedate.Disabled = true;
        }
        else
        {
            inputfhsdate.Value = _mod.sfahuodate.ToString("yyyy-MM-dd").Trim();
            inputfhedate.Value = _mod.efahuodate.ToString("yyyy-MM-dd").Trim();
        } 
        inputstartfahuodate.Value = _mod.sfahuodate.ToString("yyyy-MM-dd");
        inputendfahuodate.Value = _mod.efahuodate.ToString("yyyy-MM-dd");
        inputremarks.Value = _mod.Remarks.Trim();
    }

    readonly TabExecutewuliu _tabexewuliu = new TabExecutewuliu();
    readonly BTJ_Activity_Agent _btjActivityAgent = new BTJ_Activity_Agent();
    readonly BTJ_Activity_Product _btjActivityProduct = new BTJ_Activity_Product();
    protected void Btn_refresh_Click(object sender, EventArgs e)
    {
        ddl_dingdancode.Items.Clear();
        string fileterstring = "CompID=" + GetCookieCompID();
        IList<MTJ_Activity_Agent> agentlist = _btjActivityAgent.GetListsByFilterString("acid=" + hf_acid.Value);
        IList<MTJ_Activity_Product> productlist = _btjActivityProduct.GetListsByFilterString("acid=" + hf_acid.Value);
        string temp = string.Empty;
        if (agentlist.Count > 0)
        {
            temp = string.Empty;
            foreach (MTJ_Activity_Agent mod in agentlist)
            {
                if (temp.Length.Equals(0))
                {
                    temp = mod.agentid.ToString();
                }
                else
                {
                    temp += "," + mod.agentid;
                }
            }
        }
        if (temp.Length > 0)
        {
            fileterstring += " and AgentID in (" + temp + ")";
        }

        temp = string.Empty;
        if (productlist.Count > 0)
        {
            foreach (MTJ_Activity_Product mod in productlist)
            {
                if (temp.Length.Equals(0))
                {
                    temp = mod.prodid.ToString();
                }
                else
                {
                    temp += "," + mod.prodid;
                }
            }
        }
        if (temp.Length > 0)
        {
            fileterstring += " and ProID in (" + temp + ")";
        }
        if (inputfhsdate.Value.Length > 0 && inputfhedate.Value.Length > 0)
        {
            if (Convert.ToDateTime(inputfhedate).CompareTo(Convert.ToDateTime(inputfhsdate.Value)) > 0)
            {
                fileterstring += " and FHDate between '" + inputfhsdate.Value + "' and '" + Convert.ToDateTime(inputfhedate.Value).AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
        }
        ddl_dingdancode.DataSource = _tabexewuliu.ExecuteNonQuery("select KhDDH from TB_FaHuoInfo_" + GetCookieCompID() + " where " + fileterstring);
        ddl_dingdancode.DataBind();
        ddl_dingdancode.Items.Insert(0, new ListItem("全部...", "0"));
        ddl_dingdancode.SelectedValue = "0";
    }

    protected void CheckBox_AnyFHDate_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_AnyFHDate.Checked)
        {
            inputfhsdate.Disabled = true;
            inputfhedate.Disabled = true;
            inputfhsdate.Value = "1900-01-01";
            inputfhedate.Value = "1900-01-01";
        }
        else
        {
            inputfhsdate.Disabled = false;
            inputfhedate.Disabled = false;
            inputfhsdate.Value = inputstartfahuodate.Value;
            inputfhedate.Value = inputendfahuodate.Value;
        }
    }
}