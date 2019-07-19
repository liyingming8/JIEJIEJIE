using System;
using System.Data;
using System.Web.UI;
using TJ.Model;
using TJ.DBUtility;
using commonlib;
public partial class Admin_tj_js_brandtypeAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_js_brandtype mod = new Mtj_js_brandtype();
    string tempsqlstring="";
    readonly CommonFunCrm common = new CommonFunCrm();
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
           FillDdl();
          switch (HF_CMD.Value)
          {
             case "add":
                Button1.Text = "添加";
                btnDelete.Style["display"] = "none";//新增界面隐藏删除按钮
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

    private void FillDdl()
    {
        common.BindTreeCombox(ComboBoxBrandType, "typename", "id", "parentid", "tj_js_brandtype", 0, "类别...", true, "-", "1=1");
        ComboBoxBrandType.SelectedValue = "0";
    }

private Mtj_js_brandtype  GetModel(int id)
{
       DataTable  dtTable =  tab.ExecuteQuery("select * from tj_js_brandtype where id=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_js_brandtype(Convert.ToInt32(dtTable.Rows[0]["id"]),Convert.ToInt32(dtTable.Rows[0]["parentid"]),dtTable.Rows[0]["typename"].ToString());
}
      return null;
}

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
           mod = GetModel(Convert.ToInt32(HF_ID.Value));
        } 
        mod.parentid = Convert.ToInt32(ComboBoxBrandType.SelectedValue);
        mod.typename = inputtypename.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_js_brandtype(parentid,typename) VALUES("+ mod.parentid+","+mod.typename+")";
      tab.ExecuteQuery(tempsqlstring, null);
      RecordDealLog(new MTJ_DealLog(0, "tj_js_brandtypeAddEdit.aspx", "tj_js_brandtype", "描述", DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_js_brandtype SET parentid="+Convert.ToInt32(mod.parentid)+",typename="+mod.typename+" where id=mod.id";
      tab.ExecuteNonQuery(tempsqlstring);
      RecordDealLog(new MTJ_DealLog(0, "tj_js_brandtypeAddEdit.aspx", "tj_js_brandtype", "描述", DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void Fillinput(int id)
    {
        Mtj_js_brandtype ms = GetModel(id);
        ComboBoxBrandType.SelectedValue = ms.parentid.ToString().Trim(); 
        inputtypename.Value = ms.typename.Trim();
    }

    /*
     * 删除
     */
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            var deleteId = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
            string sql = "delete from tj_js_brandtype where id=" + deleteId;
            DataTable result = tab.ExecuteNonQuery(sql);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
    }

}