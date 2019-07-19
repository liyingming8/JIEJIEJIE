using System;
using System.Web.UI;
using TJ.DBUtility;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_Comp_RolesAddEdit : AuthorPage
{
    BTJ_Comp_Roles bll = new BTJ_Comp_Roles();
    MTJ_Comp_Roles mod = new MTJ_Comp_Roles();
    BTJ_RegisterCompanys btjRegister = new BTJ_RegisterCompanys();
    BTJ_Role_Package btjRole = new BTJ_Role_Package();
    TabExecute tab = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["cmd"]))
            {
                HF_CMD.Value = Sc.DecryptQueryString(Request.QueryString["cmd"].Trim());
            }
            if (!string.IsNullOrEmpty(Request.QueryString["objcompid"]))
            {
                HF_CompID.Value = Sc.DecryptQueryString(Request.QueryString["objcompid"]);
                inputcompid.Text = btjRegister.GetList(int.Parse(HF_CompID.Value)).CompName;
                FillRolePackageList(HF_CompID.Value);  
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
                    Fillinput(int.Parse(HF_ID.Value.Trim()));
                    break;
                default:
                    break;
            }
        }
    }

    private void FillRolePackageList(string compid)
    {
        rdb_list_rolepackage.DataSource =
            tab.ExecuteNonQuery(
                "SELECT [id],[rpackage],[ridstring]  FROM [TJMarketingSystemYin].[dbo].[TJ_Role_Package] rp where rp.id not in (SELECT cr.[rpackid] FROM [TJMarketingSystemYin].[dbo].[TJ_Comp_Roles] cr where cr.compid="+compid+")");
        rdb_list_rolepackage.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.compid = int.Parse(HF_CompID.Value);
        mod.authoruserid = Convert.ToInt32(GetCookieUID());
        mod.authordate = DateTime.Now;
        mod.isactive = ckb_isactive.Checked;
        mod.remarks = inputremarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.rpackid = int.Parse(rdb_list_rolepackage.SelectedValue);
                bll.Insert(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_Comp_RolesAddEdit.aspx", "TJ_Comp_Roles", "描述", System.DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                bll.Modify(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_Comp_RolesAddEdit.aspx", "TJ_Comp_Roles", "描述", System.DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "reload", "closemyWindow();", true);
    }

    private void Fillinput(int id)
    {
        MTJ_Comp_Roles ms = bll.GetList(id);
        HF_CompID.Value = ms.compid.ToString();
        inputcompid.Text = btjRegister.GetList(ms.compid).CompName;
        inputridstring.Text = btjRole.GetList(ms.rpackid).rpackage;
        ckb_isactive.Checked = ms.isactive;
        inputremarks.Value = ms.remarks.Trim();
    }
}