using System;
using System.Data;
using System.Web.UI;
using TJ.Model;
using TJ.DBUtility;
using commonlib;
public partial class Admin_tj_js_wxaccesstokeninfoAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_js_wxaccesstokeninfo mod = new Mtj_js_wxaccesstokeninfo();
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

private Mtj_js_wxaccesstokeninfo  GetModel(int id)
{
       DataTable  dtTable =  tab.ExecuteQuery("select * from tj_js_wxaccesstokeninfo where id=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_js_wxaccesstokeninfo(Convert.ToInt32(dtTable.Rows[0]["id"]),dtTable.Rows[0]["wxappid"].ToString(),dtTable.Rows[0]["access_token"].ToString(),Convert.ToInt32(dtTable.Rows[0]["expires_in"]));
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
        mod.wxappid = inputwxappid.Value.Trim();
        mod.access_token = inputaccess_token.Value.Trim();
        mod.expires_in = Convert.ToInt32(inputexpires_in.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_js_wxaccesstokeninfo(wxappid,access_token,expires_in) VALUES("+ mod.wxappid+","+mod.access_token+","+mod.expires_in+")";
      tab.ExecuteQuery(tempsqlstring, null);
      RecordDealLog(new MTJ_DealLog(0, "tj_js_wxaccesstokeninfoAddEdit.aspx", "tj_js_wxaccesstokeninfo", "描述", DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_js_wxaccesstokeninfo SET wxappid="+mod.wxappid+",access_token="+mod.access_token+",expires_in="+Convert.ToInt32(mod.expires_in)+" where id=mod.id";
      tab.ExecuteNonQuery(tempsqlstring);
      RecordDealLog(new MTJ_DealLog(0, "tj_js_wxaccesstokeninfoAddEdit.aspx", "tj_js_wxaccesstokeninfo", "描述", DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_js_wxaccesstokeninfo ms = GetModel(id);
        inputid.Value = ms.id.ToString().Trim();
        inputwxappid.Value = ms.wxappid.Trim();
        inputaccess_token.Value = ms.access_token.Trim();
        inputexpires_in.Value = ms.expires_in.ToString().Trim();
    }
}