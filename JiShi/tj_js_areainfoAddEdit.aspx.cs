using System;
using System.Data;
using System.Web.UI;
using TJ.Model;
using TJ.DBUtility;
using commonlib;
public partial class Admin_tj_js_areainfoAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_js_areainfo mod = new Mtj_js_areainfo();
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

private Mtj_js_areainfo  GetModel(int id)
{
       DataTable  dtTable =  tab.ExecuteQuery("select * from tj_js_areainfo where =" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_js_areainfo(Convert.ToInt32(dtTable.Rows[0]["id"]),Convert.ToInt32(dtTable.Rows[0]["parentid"]),dtTable.Rows[0]["acode"].ToString(),dtTable.Rows[0]["aname"].ToString());
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
        mod.parentid = Convert.ToInt32(inputparentid.Value.Trim());
        mod.acode = inputacode.Value.Trim();
        mod.aname = inputaname.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_js_areainfo(id,parentid,acode,aname) VALUES("+ mod.id+","+mod.parentid+","+mod.acode+","+mod.aname+")";
      tab.ExecuteQuery(tempsqlstring, null);
                 RecordDealLog(new MTJ_DealLog(0,"tj_js_areainfoAddEdit.aspx","tj_js_areainfo","描述",DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_js_areainfo SET id="+Convert.ToInt32(mod.id)+",parentid="+Convert.ToInt32(mod.parentid)+",acode="+mod.acode+",aname="+mod.aname+" where =mod.";
      tab.ExecuteNonQuery(tempsqlstring); 
                 RecordDealLog(new MTJ_DealLog(0,"tj_js_areainfoAddEdit.aspx","tj_js_areainfo","描述",DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_js_areainfo ms = GetModel(id);
        inputid.Value = ms.id.ToString().Trim();
        inputparentid.Value = ms.parentid.ToString().Trim();
        inputacode.Value = ms.acode.Trim();
        inputaname.Value = ms.aname.Trim();
    }
}