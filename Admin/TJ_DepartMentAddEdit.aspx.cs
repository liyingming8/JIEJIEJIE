using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_DepartMentAddEdit : AuthorPage
{
    BTJ_DepartMent bll = new BTJ_DepartMent();
    MTJ_DepartMent mod = new MTJ_DepartMent();
    BTJ_RegisterCompanys btjRegister = new BTJ_RegisterCompanys();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["pcompid"]))
            {
                hd_compid.Value = Request.QueryString["pcompid"].Trim();
                inputcompid.Value = btjRegister.GetList(int.Parse(hd_compid.Value)).CompName;
                FillDDL();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["cmd"]))
            {
                HF_CMD.Value = Sc.DecryptQueryString(Request.QueryString["cmd"].Trim());
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                HF_ID.Value = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
            }
            if (IsSuperAdmin())
            {
                inputcompid.Attributes.Add("onclick", ReturnCompnaySelectScript("所属单位", "0",""));
            }
            else
            {
                hd_compid.Value = GetCookieCompID();
                inputcompid.Value = btjRegister.GetList(int.Parse(hd_compid.Value)).CompName;
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
    CommonFun common = new CommonFun();
    private void FillDDL()
    {
        if (hd_compid.Value.Length > 0)
        {
            common.BindTreeCombox(ddl_parentid, "department", "id", "parentid", "TJ_DepartMent",0,"顶级...",true,"-","compid="+hd_compid.Value);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        string checkrst = Checkinput();
        if (checkrst.Length > 0)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('"+checkrst+"');", true);
        }
        else
        {
            mod.compid = Convert.ToInt32(hd_compid.Value.Trim());
            mod.parentid = Convert.ToInt32(ddl_parentid.SelectedValue);
            mod.department = inputdepartment.Value.Trim();
            mod.leadername = inputleadername.Value.Trim();
            mod.createtm = DateTime.Now;
            mod.creatuserid = Convert.ToInt32(GetCookieUID());
            mod.remarks = inputremarks.Value.Trim();
            mod.compname = inputcompid.Value;
            mod.parentdepartment = ddl_parentid.SelectedItem.Text;
            switch (HF_CMD.Value.Trim())
            {
                case "add":
                    bll.Insert(mod);
                    RecordDealLog(new MTJ_DealLog(0, "TJ_DepartMentAddEdit.aspx", "TJ_DepartMent", "描述", System.DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                    break;
                case "edit":
                    bll.Modify(mod);
                    RecordDealLog(new MTJ_DealLog(0, "TJ_DepartMentAddEdit.aspx", "TJ_DepartMent", "描述", System.DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                    break;
            }
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "reload", "closemyWindow();", true);
        }
        
    }

    private string Checkinput()
    {
        if (hd_compid.Value.Length.Equals(0))
        {
            return "请选择所属单位!";
        }
        if (string.IsNullOrEmpty(ddl_parentid.SelectedValue))
        {
            ddl_parentid.SelectedValue = "0";
        }
        if (string.IsNullOrEmpty(inputdepartment.Value))
        {
            return "请输入单位或部门名称!";
        }
        if (string.IsNullOrEmpty(inputleadername.Value))
        {
            return "请输入负责人名称!";
        }
        return "";
    }

    readonly BTJ_User _btjUser = new BTJ_User();
    private void Fillinput(int id)
    {
        MTJ_DepartMent ms = bll.GetList(id); 
        hd_compid.Value = ms.compid.ToString().Trim();
        FillDDL();
        inputcompid.Value = btjRegister.GetList(int.Parse(hd_compid.Value)).CompName;
        ddl_parentid.SelectedValue = ms.parentid.ToString().Trim();
        inputdepartment.Value = ms.department.Trim();
        inputleadername.Value = ms.leadername.Trim();
        Labelcreatetm.Text = ms.createtm.ToString("yyyy-MM-dd HH:mm:ss").Trim();
        Label_creatuserid.Text = _btjUser.GetList(ms.creatuserid).LoginName.Trim();
        inputremarks.Value = ms.remarks.Trim();
    }
}