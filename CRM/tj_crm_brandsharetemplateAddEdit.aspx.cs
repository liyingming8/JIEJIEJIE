using System;
using System.Data;
using System.Web.UI;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
public partial class CRM_tj_crm_brandsharetemplateAddEdit : AuthorPage
{
    readonly PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_crm_brandsharetemplate mod = new Mtj_crm_brandsharetemplate();
    private string _tempsqlstring = "";
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

private Mtj_crm_brandsharetemplate  GetModel(int id)
{
       DataTable  dtTable =  tab.ExecuteQuery("select * from tj_crm_brandsharetemplate where id=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_crm_brandsharetemplate(Convert.ToInt32(dtTable.Rows[0]["id"]),Convert.ToInt32(dtTable.Rows[0]["brandid"]),dtTable.Rows[0]["title"].ToString(),dtTable.Rows[0]["innerhtml"].ToString(),dtTable.Rows[0]["remarks"].ToString());
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
        mod.brandid = Convert.ToInt32(inputbrandid.Value.Trim());
        mod.title = inputtitle.Value.Trim();
        mod.innerhtml = inputinnerhtml.Value.Trim();
        mod.remarks = inputremarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 _tempsqlstring = "INSERT INTO tj_crm_brandsharetemplate(brandid,title,innerhtml,remarks) VALUES(mod.brandid,mod.title,mod.innerhtml,mod.remarks)";
      tab.ExecuteQuery(_tempsqlstring, null);
                 RecordDealLog(new MTJ_DealLog(0,"tj_crm_brandsharetemplateAddEdit.aspx","tj_crm_brandsharetemplate","描述",DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                 _tempsqlstring = "UPDATE  tj_crm_brandsharetemplate SET brandid=Convert.ToInt32(brandid),title=title,innerhtml=innerhtml,remarks=remarks where id=mod.id";
      tab.ExecuteNonQuery(_tempsqlstring); 
                 RecordDealLog(new MTJ_DealLog(0,"tj_crm_brandsharetemplateAddEdit.aspx","tj_crm_brandsharetemplate","描述",DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_crm_brandsharetemplate ms = GetModel(id);
        inputid.Value = ms.id.ToString().Trim();
        inputbrandid.Value = ms.brandid.ToString().Trim();
        inputtitle.Value = ms.title.Trim();
        inputinnerhtml.Value = ms.innerhtml.Trim();
        inputremarks.Value = ms.remarks.Trim();
    }
}