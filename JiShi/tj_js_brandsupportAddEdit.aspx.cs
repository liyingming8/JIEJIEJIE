using System;
using System.Data;
using System.Web.UI;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
public partial class Admin_tj_js_brandsupportAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_js_brandsupport mod = new Mtj_js_brandsupport();
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

private Mtj_js_brandsupport  GetModel(int id)
{
       DataTable  dtTable =  tab.ExecuteQuery("select * from tj_js_brandsupport where jishiid=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_js_brandsupport(Convert.ToInt32(dtTable.Rows[0]["jishiid"]),Convert.ToInt32(dtTable.Rows[0]["compid"]),Convert.ToDateTime(dtTable.Rows[0]["joindate"]),Convert.ToBoolean(dtTable.Rows[0]["permit"]),Convert.ToInt32(dtTable.Rows[0]["comfirmuserid"]),Convert.ToDateTime(dtTable.Rows[0]["confirmdate"]),dtTable.Rows[0]["refusereson"].ToString(),dtTable.Rows[0]["supporname"].ToString(),dtTable.Rows[0]["remarks"].ToString(),dtTable.Rows[0]["jishilogourl"].ToString());
}
      return null;
}

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
           mod = GetModel(Convert.ToInt32(HF_ID.Value));
        }
        mod.jishiid = Convert.ToInt32(inputjishiid.Value.Trim());
        mod.compid = Convert.ToInt32(inputcompid.Value.Trim());
        mod.joindate = Convert.ToDateTime(inputjoindate.Value.Trim());
        mod.permit = Convert.ToBoolean(inputpermit.Value.Trim());
        mod.comfirmuserid = Convert.ToInt32(inputcomfirmuserid.Value.Trim());
        mod.confirmdate = Convert.ToDateTime(inputconfirmdate.Value.Trim());
        mod.refusereson = inputrefusereson.Value.Trim();
        mod.supporname = inputsupporname.Value.Trim();
        mod.remarks = inputremarks.Value.Trim();
        mod.jishilogourl = inputjishilogourl.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_js_brandsupport(compid,joindate,permit,comfirmuserid,confirmdate,refusereson,supporname,remarks,jishilogourl) VALUES("+ mod.compid+","+mod.joindate+","+mod.permit+","+mod.comfirmuserid+","+mod.confirmdate+","+mod.refusereson+","+mod.supporname+","+mod.remarks+","+mod.jishilogourl+")";
      tab.ExecuteQuery(tempsqlstring, null);
      RecordDealLog(new MTJ_DealLog(0, "tj_js_brandsupportAddEdit.aspx", "tj_js_brandsupport", "描述", DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_js_brandsupport SET compid="+Convert.ToInt32(mod.compid)+",joindate="+Convert.ToDateTime(mod.joindate)+",permit="+Convert.ToBoolean(mod.permit)+",comfirmuserid="+Convert.ToInt32(mod.comfirmuserid)+",confirmdate="+Convert.ToDateTime(mod.confirmdate)+",refusereson="+mod.refusereson+",supporname="+mod.supporname+",remarks="+mod.remarks+",jishilogourl="+mod.jishilogourl+" where jishiid=mod.jishiid";
      tab.ExecuteNonQuery(tempsqlstring);
      RecordDealLog(new MTJ_DealLog(0, "tj_js_brandsupportAddEdit.aspx", "tj_js_brandsupport", "描述", DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_js_brandsupport ms = GetModel(id);
        inputjishiid.Value = ms.jishiid.ToString().Trim();
        inputcompid.Value = ms.compid.ToString().Trim();
        inputjoindate.Value = ms.joindate.ToString().Trim();
        inputpermit.Value = ms.permit.ToString().Trim();
        inputcomfirmuserid.Value = ms.comfirmuserid.ToString().Trim();
        inputconfirmdate.Value = ms.confirmdate.ToString().Trim();
        inputrefusereson.Value = ms.refusereson.Trim();
        inputsupporname.Value = ms.supporname.Trim();
        inputremarks.Value = ms.remarks.Trim();
        inputjishilogourl.Value = ms.jishilogourl.Trim();
    }
}