using System;
using System.Data;
using System.Web.UI;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
public partial class Admin_tj_js_scartAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_js_scart mod = new Mtj_js_scart();
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

private Mtj_js_scart  GetModel(int id)
{
       DataTable  dtTable =  tab.ExecuteQuery("select * from tj_js_scart where id=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_js_scart(Convert.ToInt32(dtTable.Rows[0]["id"]),Convert.ToInt32(dtTable.Rows[0]["userid"]),dtTable.Rows[0]["goodsid"].ToString(),dtTable.Rows[0]["num"].ToString(),dtTable.Rows[0]["type"].ToString(),dtTable.Rows[0]["compid"].ToString());
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
        mod.userid = Convert.ToInt32(inputuserid.Value.Trim());
        mod.goodsid = inputgoodsid.Value.Trim();
        mod.num = inputnum.Value.Trim();
        mod.type = inputtype.Value.Trim();
        mod.compid = inputcompid.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_js_scart(userid,goodsid,num,type,compid) VALUES("+ mod.userid+","+mod.goodsid+","+mod.num+","+mod.type+","+mod.compid+")";
      tab.ExecuteQuery(tempsqlstring, null);
      RecordDealLog(new MTJ_DealLog(0, "tj_js_scartAddEdit.aspx", "tj_js_scart", "描述", DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_js_scart SET userid="+Convert.ToInt32(mod.userid)+",goodsid="+mod.goodsid+",num="+mod.num+",type="+mod.type+",compid="+mod.compid+" where id=mod.id";
      tab.ExecuteNonQuery(tempsqlstring);
      RecordDealLog(new MTJ_DealLog(0, "tj_js_scartAddEdit.aspx", "tj_js_scart", "描述", DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_js_scart ms = GetModel(id);
        inputid.Value = ms.id.ToString().Trim();
        inputuserid.Value = ms.userid.ToString().Trim();
        inputgoodsid.Value = ms.goodsid.Trim();
        inputnum.Value = ms.num.Trim();
        inputtype.Value = ms.type.Trim();
        inputcompid.Value = ms.compid.Trim();
    }
}