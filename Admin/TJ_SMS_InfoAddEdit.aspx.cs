using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
using TJ.DBUtility;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class Admin_TJ_SMS_InfoAddEdit : AuthorPage
{
    BTJ_SMS_Info bll = new BTJ_SMS_Info();
    MTJ_SMS_Info mod = new MTJ_SMS_Info();
    DAConfig config = new DAConfig();
    TabExecute tab = new TabExecute();
    BTJ_RegisterCompanys btjcompany = new BTJ_RegisterCompanys();
    BTJ_DepartMent BTJ_Depart = new BTJ_DepartMent();
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
            FillDDL();
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
            if (!string.IsNullOrEmpty(Request.QueryString["pcompid"]))
            {
                hf_compid.Value = Request.QueryString["pcompid"];
                input_compid.Value = btjcompany.GetList(Convert.ToInt32(hf_compid.Value)).CompName;
                ddl_departid.Items.Clear();
                ddl_departid.Items.Add(new ListItem("全部", "0"));
                ddl_departid.SelectedValue = "0";
                IList<MTJ_DepartMent> list = BTJ_Depart.GetListsByFilterString("compid=" + hf_compid.Value);
                if (list.Count > 0)
                {
                    ddl_departid.DataSource = list;
                    ddl_departid.DataBind();
                }
            }
            input_compid.Attributes.Add("onclick", ReturnCompnaySelectScript("所属单位", "0", ""));
        }
    }

    private void FillDDL()
    {
        ddl_alert_case.DataSource = tab.ExecuteQuery("select CID,CName from TJ_BaseClass where ParentID=" + DAConfig.SMSAlert, null);
        ddl_alert_case.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.uname = inputuname.Value.Trim();
        mod.phonenumber = inputphonenumber.Value.Trim();
        mod.caseid = Convert.ToInt32(ddl_alert_case.SelectedValue);
        mod.ctm = DateTime.Now;
        mod.userid = Convert.ToInt32(GetCookieUID());
        mod.remarks = inputremarks.Value.Trim();
        mod.compid = int.Parse(hf_compid.Value);
        mod.compname = input_compid.Value;
        mod.departname = ddl_departid.SelectedValue.Equals("0") ? "" : ddl_departid.SelectedItem.Text;
        mod.departid = int.Parse(string.IsNullOrEmpty(ddl_departid.SelectedValue) ? "0" : ddl_departid.SelectedValue);
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_SMS_InfoAddEdit.aspx", "TJ_SMS_Info", "描述", System.DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                bll.Modify(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_SMS_InfoAddEdit.aspx", "TJ_SMS_Info", "描述", System.DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "reload", "closemyWindow();", true);
    }

    private void fillinput(int id)
    {
        MTJ_SMS_Info ms = bll.GetList(id);
        inputuname.Value = ms.uname.ToString().Trim();
        inputphonenumber.Value = ms.phonenumber.ToString().Trim();
        ddl_alert_case.SelectedValue = ms.caseid.ToString().Trim();
        inputremarks.Value = ms.remarks.ToString().Trim();
        hf_compid.Value = ms.compid.ToString();
        input_compid.Value = btjcompany.GetList(int.Parse(hf_compid.Value)).CompName;
        ddl_departid.DataSource = BTJ_Depart.GetListsByFilterString("compid=" + hf_compid.Value);
        ddl_departid.DataBind();
        if (ms.departid > 0)
        {
            ddl_departid.SelectedValue = ms.departid.ToString();
        }
    }
}