using System;
using System.Data;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using commonlib;

public partial class CRM_CustormerGradeAuthorManage : AuthorPage
{ 
    PGTabExecuteCRM _pgTabExecuteCrm = new PGTabExecuteCRM(); 
    DataTable dttemp = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["GID"] != null && Request.QueryString["GID"].Length > 0)
            {
                HF_GID.Value = Sc.DecryptQueryString(Request.QueryString["GID"]);
                dttemp = _pgTabExecuteCrm.ExecuteQuery("select gradename,childagent from tj_crm_customergrade where id=" +HF_GID.Value,null);
                if (dttemp.Rows.Count > 0)
                {
                    HF_AuthorGradeIDString.Value = dttemp.Rows[0]["childagent"].ToString();
                    //litercurrentgrader.Text = dttemp.Rows[0]["gradename"].ToString();
                } 
            }
            FillCheckBoxList();
        }
    }

    private void FillCheckBoxList()
    {
        ckblist_customergrade.DataSource = _pgTabExecuteCrm.ExecuteQuery("select * from tj_crm_customergrade where compid=" + GetCookieCompID() + " order by gradeorder", null);
        ckblist_customergrade.DataBind();
        autocheck();
    }

    private void autocheck()
    {
        foreach (ListItem item in ckblist_customergrade.Items)
        {
            if (("," + HF_AuthorGradeIDString.Value + ",").Contains("," + item.Value + ","))
            {
                item.Selected = true;
            }
        }
    }

    private string tempidstr = "";
    private string autoselect()
    {
        tempidstr = "";
        foreach (ListItem item in ckblist_customergrade.Items)
        {
            if (item.Selected)
            {
                if (string.IsNullOrEmpty(tempidstr))
                {
                    tempidstr = item.Value;
                }
                else
                {
                    tempidstr += "," + item.Value;
                }
            } 
        }
        return tempidstr;
    }

    protected void Button_OK_Click(object sender, EventArgs e)
    {
        HF_AuthorGradeIDString.Value = autoselect();
        _pgTabExecuteCrm.ExecuteQuery(
            "update tj_crm_customergrade set childagent='" + HF_AuthorGradeIDString.Value + "' where id=" + HF_GID.Value,
            null);
    }
     
}