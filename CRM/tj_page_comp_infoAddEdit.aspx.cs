using System;
using System.Data;
using System.Web.UI;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
public partial class CRM_tj_page_comp_infoAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_page_comp_info mod = new Mtj_page_comp_info();
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

private Mtj_page_comp_info  GetModel(int id)
{
       DataTable  dtTable =  tab.ExecuteQuery("select * from tj_page_comp_info where id=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_page_comp_info(Convert.ToInt32(dtTable.Rows[0]["mdid"]), dtTable.Rows[0]["modelcontent"].ToString(), Convert.ToInt32(dtTable.Rows[0]["compid"]), dtTable.Rows[0]["remarks"].ToString(), Convert.ToInt32(dtTable.Rows[0]["id"]), Convert.ToInt32(dtTable.Rows[0]["custgid"]));
}
      return null;
}

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
           mod = GetModel(Convert.ToInt32(HF_ID.Value));
        }
        mod.mdid = Convert.ToInt32(inputmdid.Value.Trim());
        mod.modelcontent = inputmodelcontent.Value.Trim();
        mod.compid = Convert.ToInt32(inputcompid.Value.Trim());
        mod.remarks = inputremarks.Value.Trim();
        mod.id = Convert.ToInt32(inputid.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_page_comp_info(mdid,modelcontent,compid,remarks) VALUES("+ mod.mdid+","+mod.modelcontent+","+mod.compid+","+mod.remarks+")";
      tab.ExecuteQuery(tempsqlstring, null);
                 RecordDealLog(new MTJ_DealLog(0,"tj_page_comp_infoAddEdit.aspx","tj_page_comp_info","描述",DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_page_comp_info SET mdid="+Convert.ToInt32(mod.mdid)+",modelcontent="+mod.modelcontent+",compid="+Convert.ToInt32(mod.compid)+",remarks="+mod.remarks+" where id=mod.id";
      tab.ExecuteNonQuery(tempsqlstring); 
                 RecordDealLog(new MTJ_DealLog(0,"tj_page_comp_infoAddEdit.aspx","tj_page_comp_info","描述",DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_page_comp_info ms = GetModel(id);
        inputmdid.Value = ms.mdid.ToString().Trim();
        inputmodelcontent.Value = ms.modelcontent.Trim();
        inputcompid.Value = ms.compid.ToString().Trim();
        inputremarks.Value = ms.remarks.Trim();
        inputid.Value = ms.id.ToString().Trim();
    }
}