using System;
using System.Web.UI;
using TJ.Model;
using TJ.DBUtility;
using commonlib;
public partial class CRM_tj_crm_backmoneynumAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_crm_backmoneynum mod = new Mtj_crm_backmoneynum();
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

private Mtj_crm_backmoneynum  GetModel(int id)
{
       System.Data.DataTable  dtTable =  tab.ExecuteQuery("select * from tj_crm_backmoneynum where id=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_crm_backmoneynum(Convert.ToInt32(dtTable.Rows[0]["id"]),Convert.ToInt32(dtTable.Rows[0]["uid"]),Convert.ToInt32(dtTable.Rows[0]["compid"]),Convert.ToDecimal(dtTable.Rows[0]["money"]));
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
        mod.uid = Convert.ToInt32(inputuid.Value.Trim());
        mod.compid = Convert.ToInt32(inputcompid.Value.Trim());
        mod.money = Convert.ToDecimal(inputmoney.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_crm_backmoneynum(uid,compid,money) VALUES("+ mod.uid+","+mod.compid+","+mod.money+")";
      tab.ExecuteQuery(tempsqlstring, null);
                 RecordDealLog(new MTJ_DealLog(0,"tj_crm_backmoneynumAddEdit.aspx","tj_crm_backmoneynum","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_crm_backmoneynum SET uid="+Convert.ToInt32(mod.uid)+",compid="+Convert.ToInt32(mod.compid)+",money="+Convert.ToDecimal(mod.money)+" where id=mod.id";
      tab.ExecuteNonQuery(tempsqlstring); 
                 RecordDealLog(new MTJ_DealLog(0,"tj_crm_backmoneynumAddEdit.aspx","tj_crm_backmoneynum","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_crm_backmoneynum ms = GetModel(id);
        inputid.Value = ms.id.ToString().Trim();
        inputuid.Value = ms.uid.ToString().Trim();
        inputcompid.Value = ms.compid.ToString().Trim();
        inputmoney.Value = ms.money.ToString().Trim();
    }
}