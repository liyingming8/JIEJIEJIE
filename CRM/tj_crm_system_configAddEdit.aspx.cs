using System;
using System.Data;
using System.Web.UI;
using TJ.Model;
using TJ.DBUtility;
using commonlib;
public partial class Admin_tj_crm_system_configAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_crm_system_config mod = new Mtj_crm_system_config();
    string tempsqlstring="";
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

private Mtj_crm_system_config  GetModel(int id)
{
       DataTable  dtTable =  tab.ExecuteQuery("select * from tj_crm_system_config where =" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_crm_system_config(Convert.ToInt32(dtTable.Rows[0]["id"]),Convert.ToInt32(dtTable.Rows[0]["compid"]),dtTable.Rows[0]["logourl"].ToString(),dtTable.Rows[0]["hometoppicurl"].ToString(),dtTable.Rows[0]["remarks"].ToString());
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
        mod.compid = Convert.ToInt32(inputcompid.Value.Trim());
        mod.logourl = inputlogourl.Value.Trim();
        mod.hometoppicurl = inputhometoppicurl.Value.Trim();
        mod.remarks = inputremarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                tempsqlstring = "INSERT INTO tj_crm_system_config(id,compid,logourl,hometoppicurl,remarks) VALUES(" +
                                mod.id + "," + mod.compid + "," + mod.logourl + "," + mod.hometoppicurl + "," +
                                mod.remarks + ")";
                tab.ExecuteQuery(tempsqlstring, null);
                RecordDealLog(new MTJ_DealLog(0, "tj_crm_system_configAddEdit.aspx", "tj_crm_system_config", "描述",
                    DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                tempsqlstring = "UPDATE  tj_crm_system_config SET id=" + Convert.ToInt32(mod.id) + ",compid=" +
                                Convert.ToInt32(mod.compid) + ",logourl=" + mod.logourl + ",hometoppicurl=" +
                                mod.hometoppicurl + ",remarks=" + mod.remarks + " where =mod.";
                tab.ExecuteNonQuery(tempsqlstring);
                RecordDealLog(new MTJ_DealLog(0, "tj_crm_system_configAddEdit.aspx", "tj_crm_system_config", "描述",
                    DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void fillinput(int id)
    {
        Mtj_crm_system_config ms = GetModel(id);
        inputid.Value = ms.id.ToString().Trim();
        inputcompid.Value = ms.compid.ToString().Trim();
        inputlogourl.Value = ms.logourl.Trim();
        inputhometoppicurl.Value = ms.hometoppicurl.Trim();
        inputremarks.Value = ms.remarks.Trim();
    }
}