using System;
using System.Data;
using System.Web.UI;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
public partial class Admin_tj_js_wxsqinfoAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_js_wxsqinfo mod = new Mtj_js_wxsqinfo();
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
          }
       }
    }

private Mtj_js_wxsqinfo  GetModel(int id)
{
       DataTable  dtTable =  tab.ExecuteQuery("select * from tj_js_wxsqinfo where id=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_js_wxsqinfo(Convert.ToInt32(dtTable.Rows[0]["id"]),Convert.ToInt32(dtTable.Rows[0]["compid"]),Convert.ToInt32(dtTable.Rows[0]["sid"]),dtTable.Rows[0]["wx_appid"].ToString(),dtTable.Rows[0]["wx_appsecret"].ToString(),dtTable.Rows[0]["wx_redirect_url"].ToString(),dtTable.Rows[0]["wx_cl_url"].ToString(),dtTable.Rows[0]["wx_scope"].ToString(),Convert.ToBoolean(dtTable.Rows[0]["wx_gz"]),Convert.ToBoolean(dtTable.Rows[0]["ownurl"]),dtTable.Rows[0]["remarkes"].ToString());
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
        mod.sid = Convert.ToInt32(inputsid.Value.Trim());
        mod.wx_appid = inputwx_appid.Value.Trim();
        mod.wx_appsecret = inputwx_appsecret.Value.Trim();
        mod.wx_redirect_url = inputwx_redirect_url.Value.Trim();
        mod.wx_cl_url = inputwx_cl_url.Value.Trim();
        mod.wx_scope = inputwx_scope.Value.Trim();
        mod.wx_gz = Convert.ToBoolean(inputwx_gz.Value.Trim());
        mod.ownurl = Convert.ToBoolean(inputownurl.Value.Trim());
        mod.remarkes = inputremarkes.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_js_wxsqinfo(compid,sid,wx_appid,wx_appsecret,wx_redirect_url,wx_cl_url,wx_scope,wx_gz,ownurl,remarkes) VALUES("+ mod.compid+","+mod.sid+","+mod.wx_appid+","+mod.wx_appsecret+","+mod.wx_redirect_url+","+mod.wx_cl_url+","+mod.wx_scope+","+mod.wx_gz+","+mod.ownurl+","+mod.remarkes+")";
      tab.ExecuteQuery(tempsqlstring, null);
      RecordDealLog(new MTJ_DealLog(0, "tj_js_wxsqinfoAddEdit.aspx", "tj_js_wxsqinfo", "描述", DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_js_wxsqinfo SET compid="+Convert.ToInt32(mod.compid)+",sid="+Convert.ToInt32(mod.sid)+",wx_appid="+mod.wx_appid+",wx_appsecret="+mod.wx_appsecret+",wx_redirect_url="+mod.wx_redirect_url+",wx_cl_url="+mod.wx_cl_url+",wx_scope="+mod.wx_scope+",wx_gz="+Convert.ToBoolean(mod.wx_gz)+",ownurl="+Convert.ToBoolean(mod.ownurl)+",remarkes="+mod.remarkes+" where id=mod.id";
      tab.ExecuteNonQuery(tempsqlstring);
      RecordDealLog(new MTJ_DealLog(0, "tj_js_wxsqinfoAddEdit.aspx", "tj_js_wxsqinfo", "描述", DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_js_wxsqinfo ms = GetModel(id);
        inputid.Value = ms.id.ToString().Trim();
        inputcompid.Value = ms.compid.ToString().Trim();
        inputsid.Value = ms.sid.ToString().Trim();
        inputwx_appid.Value = ms.wx_appid.Trim();
        inputwx_appsecret.Value = ms.wx_appsecret.Trim();
        inputwx_redirect_url.Value = ms.wx_redirect_url.Trim();
        inputwx_cl_url.Value = ms.wx_cl_url.Trim();
        inputwx_scope.Value = ms.wx_scope.Trim();
        inputwx_gz.Value = ms.wx_gz.ToString().Trim();
        inputownurl.Value = ms.ownurl.ToString().Trim();
        inputremarkes.Value = ms.remarkes.Trim();
    }
}