using System;
using System.Data;
using System.Web.UI;
using TJ.Model;
using TJ.DBUtility;
using commonlib;
public partial class CRM_tj_crm_rewardprogramAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_crm_rewardprogram mod = new Mtj_crm_rewardprogram(); 
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
       }
    }

    private void FillDDL()
    {
        DataTable dttemp =  tab.ExecuteQuery("select id,gradename from public.tj_crm_customergrade where  compid=" + GetCookieCompID(), null);
        ddl_parentgid.DataSource = dttemp;
        ddl_parentgid.DataBind();
        ddl_childgid.DataSource = dttemp;
        ddl_childgid.DataBind();
        dttemp = tab.ExecuteQuery("select id,name from public.tj_crm_rewardtype", null);
        ddl_rewardtype.DataSource = dttemp;
        ddl_rewardtype.DataBind();
    }

private Mtj_crm_rewardprogram  GetModel(int id)
{
       System.Data.DataTable  dtTable =  tab.ExecuteQuery("select * from tj_crm_rewardprogram where id=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_crm_rewardprogram(Convert.ToInt32(dtTable.Rows[0]["id"]),Convert.ToInt32(dtTable.Rows[0]["parentgid"]),Convert.ToInt32(dtTable.Rows[0]["childgid"]),Convert.ToInt32(dtTable.Rows[0]["compid"]),dtTable.Rows[0]["gradetype"].ToString(),Convert.ToInt32(dtTable.Rows[0]["rewardtype"]),Convert.ToDecimal(dtTable.Rows[0]["rewardnum"]));
}
      return null;
}

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
           mod = GetModel(Convert.ToInt32(HF_ID.Value));
        } 
        mod.parentgid = Convert.ToInt32(ddl_parentgid.SelectedValue);
        mod.childgid = Convert.ToInt32(ddl_childgid.SelectedValue);
        mod.compid = Convert.ToInt32(GetCookieCompID());
        mod.gradetype = mod.parentgid.Equals(mod.childgid)?"同级":"父子";
        mod.rewardtype = Convert.ToInt32(ddl_rewardtype.SelectedValue);
        mod.rewardnum = Convert.ToDecimal(inputrewardnum.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_crm_rewardprogram(parentgid,childgid,compid,gradetype,rewardtype,rewardnum) VALUES("+ mod.parentgid+","+mod.childgid+","+mod.compid+",'"+mod.gradetype+"',"+mod.rewardtype+","+mod.rewardnum+")";
      tab.ExecuteQuery(tempsqlstring, null);
                 RecordDealLog(new MTJ_DealLog(0,"tj_crm_rewardprogramAddEdit.aspx","tj_crm_rewardprogram","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_crm_rewardprogram SET parentgid="+Convert.ToInt32(mod.parentgid)+",childgid="+Convert.ToInt32(mod.childgid)+",compid="+Convert.ToInt32(mod.compid)+",gradetype='"+mod.gradetype+"',rewardtype="+Convert.ToInt32(mod.rewardtype)+",rewardnum="+Convert.ToDecimal(mod.rewardnum)+" where id="+mod.id;
      tab.ExecuteNonQuery(tempsqlstring); 
                 RecordDealLog(new MTJ_DealLog(0,"tj_crm_rewardprogramAddEdit.aspx","tj_crm_rewardprogram","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_crm_rewardprogram ms = GetModel(id);
        ddl_parentgid.SelectedValue = ms.parentgid.ToString();
        ddl_childgid.SelectedValue = ms.childgid.ToString().Trim();  
        ddl_rewardtype.SelectedValue = ms.rewardtype.ToString().Trim();
        inputrewardnum.Value = ms.rewardnum.ToString().Trim();
    }
}