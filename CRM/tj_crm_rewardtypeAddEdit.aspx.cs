using System;
using System.Web.UI;
using TJ.Model;
using TJ.DBUtility;
using commonlib;
public partial class CRM_tj_crm_rewardtypeAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_crm_rewardtype mod = new Mtj_crm_rewardtype();
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

private Mtj_crm_rewardtype  GetModel(int id)
{
       System.Data.DataTable  dtTable =  tab.ExecuteQuery("select * from tj_crm_rewardtype where id=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_crm_rewardtype(Convert.ToInt32(dtTable.Rows[0]["id"]),dtTable.Rows[0]["name"].ToString());
}
      return null;
}

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
           mod = GetModel(Convert.ToInt32(HF_ID.Value));
        } 
        mod.name = inputname.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_crm_rewardtype(name) VALUES("+ mod.name+")";
      tab.ExecuteQuery(tempsqlstring, null);
                 RecordDealLog(new MTJ_DealLog(0,"tj_crm_rewardtypeAddEdit.aspx","tj_crm_rewardtype","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_crm_rewardtype SET name="+mod.name+" where id=mod.id";
      tab.ExecuteNonQuery(tempsqlstring); 
                 RecordDealLog(new MTJ_DealLog(0,"tj_crm_rewardtypeAddEdit.aspx","tj_crm_rewardtype","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_crm_rewardtype ms = GetModel(id); 
        inputname.Value = ms.name.ToString().Trim();
    }
}