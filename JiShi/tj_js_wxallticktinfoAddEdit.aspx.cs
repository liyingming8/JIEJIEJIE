using System;
using System.Data;
using System.Web.UI;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
public partial class Admin_tj_js_wxallticktinfoAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_js_wxallticktinfo mod = new Mtj_js_wxallticktinfo();
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

private Mtj_js_wxallticktinfo  GetModel(int id)
{
       DataTable  dtTable =  tab.ExecuteQuery("select * from tj_js_wxallticktinfo where id=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_js_wxallticktinfo(Convert.ToInt32(dtTable.Rows[0]["id"]),dtTable.Rows[0]["wxappid"].ToString(),dtTable.Rows[0]["wxtickt"].ToString(),dtTable.Rows[0]["tickttype"].ToString(),Convert.ToInt32(dtTable.Rows[0]["ticket_expires"]));
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
        mod.wxtickt = inputwxtickt.Value.Trim();
        mod.tickttype = inputtickttype.Value.Trim();
        mod.ticket_expires = Convert.ToInt32(inputticket_expires.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_js_wxallticktinfo(wxappid,wxtickt,tickttype,ticket_expires) VALUES("+ mod.wxappid+","+mod.wxtickt+","+mod.tickttype+","+mod.ticket_expires+")";
      tab.ExecuteQuery(tempsqlstring, null);
                 RecordDealLog(new MTJ_DealLog(0,"tj_js_wxallticktinfoAddEdit.aspx","tj_js_wxallticktinfo","描述",DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_js_wxallticktinfo SET wxappid="+mod.wxappid+",wxtickt="+mod.wxtickt+",tickttype="+mod.tickttype+",ticket_expires="+Convert.ToInt32(mod.ticket_expires)+" where id=mod.id";
      tab.ExecuteNonQuery(tempsqlstring); 
                 RecordDealLog(new MTJ_DealLog(0,"tj_js_wxallticktinfoAddEdit.aspx","tj_js_wxallticktinfo","描述",DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_js_wxallticktinfo ms = GetModel(id);
        inputid.Value = ms.id.ToString().Trim();
        inputwxappid.Value = ms.wxappid.Trim();
        inputwxtickt.Value = ms.wxtickt.Trim();
        inputtickttype.Value = ms.tickttype.Trim();
        inputticket_expires.Value = ms.ticket_expires.ToString().Trim();
    }
}