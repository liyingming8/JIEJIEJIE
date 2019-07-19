using System;
using System.Data;
using System.Web.UI;
using TJ.Model;
using TJ.DBUtility;
using commonlib;
public partial class CRM_tj_crm_customerbrandinfoAddEdit : AuthorPage
{
    readonly PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_crm_customerbrandinfo mod = new Mtj_crm_customerbrandinfo();
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

private Mtj_crm_customerbrandinfo  GetModel(int id)
{
       DataTable  dtTable =  tab.ExecuteQuery("select * from tj_crm_customerbrandinfo where id=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_crm_customerbrandinfo(Convert.ToInt32(dtTable.Rows[0]["id"]),Convert.ToInt32(dtTable.Rows[0]["customerid"]),Convert.ToInt32(dtTable.Rows[0]["brandid"]),dtTable.Rows[0]["startdate"].ToString(),dtTable.Rows[0]["enddate"].ToString(),dtTable.Rows[0]["ispermit"].ToString(),Convert.ToInt32(dtTable.Rows[0]["compid"]),Convert.ToInt32(dtTable.Rows[0]["permituserid"]),Convert.ToInt32(dtTable.Rows[0]["agentlevel"]),Convert.ToInt32(dtTable.Rows[0]["natureagentleve"]),Convert.ToInt32(dtTable.Rows[0]["sharetemplateid"]),dtTable.Rows[0]["remarks"].ToString());
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
        mod.customerid = Convert.ToInt32(inputcustomerid.Value.Trim());
        mod.brandid = Convert.ToInt32(inputbrandid.Value.Trim());
        mod.startdate = inputstartdate.Value.Trim();
        mod.enddate = inputenddate.Value.Trim();
        mod.ispermit = inputispermit.Value.Trim();
        mod.compid = Convert.ToInt32(inputcompid.Value.Trim());
        mod.permituserid = Convert.ToInt32(inputpermituserid.Value.Trim());
        mod.agentlevel = Convert.ToInt32(inputagentlevel.Value.Trim());
        mod.natureagentleve = Convert.ToInt32(inputnatureagentleve.Value.Trim());
        mod.sharetemplateid = Convert.ToInt32(inputsharetemplateid.Value.Trim());
        mod.remarks = inputremarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_crm_customerbrandinfo(customerid,brandid,startdate,enddate,ispermit,compid,permituserid,agentlevel,natureagentleve,sharetemplateid,remarks) VALUES("+ mod.customerid+","+mod.brandid+","+mod.startdate+","+mod.enddate+","+mod.ispermit+","+mod.compid+","+mod.permituserid+","+mod.agentlevel+","+mod.natureagentleve+","+mod.sharetemplateid+","+mod.remarks+")";
      tab.ExecuteQuery(tempsqlstring, null);
                 RecordDealLog(new MTJ_DealLog(0,"tj_crm_customerbrandinfoAddEdit.aspx","tj_crm_customerbrandinfo","描述",DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_crm_customerbrandinfo SET customerid="+Convert.ToInt32(mod.customerid)+",brandid="+Convert.ToInt32(mod.brandid)+",startdate="+mod.startdate+",enddate="+mod.enddate+",ispermit="+mod.ispermit+",compid="+Convert.ToInt32(mod.compid)+",permituserid="+Convert.ToInt32(mod.permituserid)+",agentlevel="+Convert.ToInt32(mod.agentlevel)+",natureagentleve="+Convert.ToInt32(mod.natureagentleve)+",sharetemplateid="+Convert.ToInt32(mod.sharetemplateid)+",remarks="+mod.remarks+" where id=mod.id";
      tab.ExecuteNonQuery(tempsqlstring); 
                 RecordDealLog(new MTJ_DealLog(0,"tj_crm_customerbrandinfoAddEdit.aspx","tj_crm_customerbrandinfo","描述",DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_crm_customerbrandinfo ms = GetModel(id);
        inputid.Value = ms.id.ToString().Trim();
        inputcustomerid.Value = ms.customerid.ToString().Trim();
        inputbrandid.Value = ms.brandid.ToString().Trim();
        inputstartdate.Value = ms.startdate.Trim();
        inputenddate.Value = ms.enddate.Trim();
        inputispermit.Value = ms.ispermit.Trim();
        inputcompid.Value = ms.compid.ToString().Trim();
        inputpermituserid.Value = ms.permituserid.ToString().Trim();
        inputagentlevel.Value = ms.agentlevel.ToString().Trim();
        inputnatureagentleve.Value = ms.natureagentleve.ToString().Trim();
        inputsharetemplateid.Value = ms.sharetemplateid.ToString().Trim();
        inputremarks.Value = ms.remarks.Trim();
    }
}