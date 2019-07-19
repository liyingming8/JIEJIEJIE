using System;
using System.Data;
using System.Web.UI;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
public partial class Admin_tj_js_wxbaseinfoAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_js_wxbaseinfo mod = new Mtj_js_wxbaseinfo();
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

private Mtj_js_wxbaseinfo  GetModel(int id)
{
       DataTable  dtTable =  tab.ExecuteQuery("select * from tj_js_wxbaseinfo where id=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_js_wxbaseinfo(Convert.ToInt32(dtTable.Rows[0]["id"]),dtTable.Rows[0]["wx_appid"].ToString(),dtTable.Rows[0]["wx_appsecret"].ToString(),dtTable.Rows[0]["wx_payid"].ToString(),dtTable.Rows[0]["wx_paykey"].ToString(),dtTable.Rows[0]["wx_paycert"].ToString(),dtTable.Rows[0]["compname"].ToString());
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
        mod.wx_appid = inputwx_appid.Value.Trim();
        mod.wx_appsecret = inputwx_appsecret.Value.Trim();
        mod.wx_payid = inputwx_payid.Value.Trim();
        mod.wx_paykey = inputwx_paykey.Value.Trim();
        mod.wx_paycert = inputwx_paycert.Value.Trim();
        mod.compname = inputcompname.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_js_wxbaseinfo(wx_appid,wx_appsecret,wx_payid,wx_paykey,wx_paycert,compname) VALUES("+ mod.wx_appid+","+mod.wx_appsecret+","+mod.wx_payid+","+mod.wx_paykey+","+mod.wx_paycert+","+mod.compname+")";
      tab.ExecuteQuery(tempsqlstring, null);
      RecordDealLog(new MTJ_DealLog(0, "tj_js_wxbaseinfoAddEdit.aspx", "tj_js_wxbaseinfo", "描述", DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_js_wxbaseinfo SET wx_appid="+mod.wx_appid+",wx_appsecret="+mod.wx_appsecret+",wx_payid="+mod.wx_payid+",wx_paykey="+mod.wx_paykey+",wx_paycert="+mod.wx_paycert+",compname="+mod.compname+" where id=mod.id";
      tab.ExecuteNonQuery(tempsqlstring);
      RecordDealLog(new MTJ_DealLog(0, "tj_js_wxbaseinfoAddEdit.aspx", "tj_js_wxbaseinfo", "描述", DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_js_wxbaseinfo ms = GetModel(id);
        inputid.Value = ms.id.ToString().Trim();
        inputwx_appid.Value = ms.wx_appid.Trim();
        inputwx_appsecret.Value = ms.wx_appsecret.Trim();
        inputwx_payid.Value = ms.wx_payid.Trim();
        inputwx_paykey.Value = ms.wx_paykey.Trim();
        inputwx_paycert.Value = ms.wx_paycert.Trim();
        inputcompname.Value = ms.compname.Trim();
    }
}