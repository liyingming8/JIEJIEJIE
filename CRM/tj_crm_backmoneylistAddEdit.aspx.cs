using System;
using System.Web.UI;
using TJ.Model;
using TJ.DBUtility;
using commonlib;
public partial class CRM_tj_crm_backmoneylistAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_crm_backmoneylist mod = new Mtj_crm_backmoneylist();
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

private Mtj_crm_backmoneylist  GetModel(int id)
{
       System.Data.DataTable  dtTable =  tab.ExecuteQuery("select * from tj_crm_backmoneylist where id=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_crm_backmoneylist(Convert.ToInt32(dtTable.Rows[0]["id"]),Convert.ToInt32(dtTable.Rows[0]["fromuid"]),Convert.ToInt32(dtTable.Rows[0]["touid"]),Convert.ToInt32(dtTable.Rows[0]["compid"]),Convert.ToInt32(dtTable.Rows[0]["orderid"]),Convert.ToInt32(dtTable.Rows[0]["rewardtype"]),Convert.ToDecimal(dtTable.Rows[0]["moneynum"]),Convert.ToDateTime(dtTable.Rows[0]["bdate"]));
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
        mod.fromuid = Convert.ToInt32(inputfromuid.Value.Trim());
        mod.touid = Convert.ToInt32(inputtouid.Value.Trim());
        mod.compid = Convert.ToInt32(inputcompid.Value.Trim());
        mod.orderid = Convert.ToInt32(inputorderid.Value.Trim());
        mod.rewardtype = Convert.ToInt32(inputrewardtype.Value.Trim());
        mod.moneynum = Convert.ToDecimal(inputmoneynum.Value.Trim());
        mod.bdate = Convert.ToDateTime(inputbdate.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_crm_backmoneylist(fromuid,touid,compid,orderid,rewardtype,moneynum,bdate) VALUES("+ mod.fromuid+","+mod.touid+","+mod.compid+","+mod.orderid+","+mod.rewardtype+","+mod.moneynum+","+mod.bdate+")";
      tab.ExecuteQuery(tempsqlstring, null);
                 RecordDealLog(new MTJ_DealLog(0,"tj_crm_backmoneylistAddEdit.aspx","tj_crm_backmoneylist","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_crm_backmoneylist SET fromuid="+Convert.ToInt32(mod.fromuid)+",touid="+Convert.ToInt32(mod.touid)+",compid="+Convert.ToInt32(mod.compid)+",orderid="+Convert.ToInt32(mod.orderid)+",rewardtype="+Convert.ToInt32(mod.rewardtype)+",moneynum="+Convert.ToDecimal(mod.moneynum)+",bdate="+Convert.ToDateTime(mod.bdate)+" where id=mod.id";
      tab.ExecuteNonQuery(tempsqlstring); 
                 RecordDealLog(new MTJ_DealLog(0,"tj_crm_backmoneylistAddEdit.aspx","tj_crm_backmoneylist","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_crm_backmoneylist ms = GetModel(id);
        inputid.Value = ms.id.ToString().Trim();
        inputfromuid.Value = ms.fromuid.ToString().Trim();
        inputtouid.Value = ms.touid.ToString().Trim();
        inputcompid.Value = ms.compid.ToString().Trim();
        inputorderid.Value = ms.orderid.ToString().Trim();
        inputrewardtype.Value = ms.rewardtype.ToString().Trim();
        inputmoneynum.Value = ms.moneynum.ToString().Trim();
        inputbdate.Value = ms.bdate.ToString().Trim();
    }
}