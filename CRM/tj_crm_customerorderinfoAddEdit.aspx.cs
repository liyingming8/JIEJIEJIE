using System;
using System.Data;
using System.Web.UI;
using TJ.Model;
using TJ.DBUtility;
using commonlib;
public partial class CRM_tj_crm_customerorderinfoAddEdit : AuthorPage
{
    readonly PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_crm_customerorderinfo mod = new Mtj_crm_customerorderinfo();
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

private Mtj_crm_customerorderinfo  GetModel(int id)
{
       DataTable  dtTable =  tab.ExecuteQuery("select * from tj_crm_customerorderinfo where id=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_crm_customerorderinfo(Convert.ToInt32(dtTable.Rows[0]["id"]), Convert.ToInt32(dtTable.Rows[0]["compid"]), Convert.ToInt32(dtTable.Rows[0]["brandid"]), Convert.ToInt32(dtTable.Rows[0]["ordercustomerid"]), Convert.ToDecimal(dtTable.Rows[0]["ordernumber"]), dtTable.Rows[0]["unitname"].ToString(), Convert.ToDateTime(dtTable.Rows[0]["orderdatetime"].ToString()), Convert.ToInt32(dtTable.Rows[0]["parentcustomerid"]), Convert.ToDecimal(dtTable.Rows[0]["totalprice"]), dtTable.Rows[0]["ispay"].ToString(), dtTable.Rows[0]["paydatetime"].ToString(), dtTable.Rows[0]["paynumber"].ToString(), dtTable.Rows[0]["paymethod"].ToString(), dtTable.Rows[0]["shouhuoren"].ToString(), dtTable.Rows[0]["shouhuophonenumber"].ToString(), dtTable.Rows[0]["shouhuodizhi"].ToString(), dtTable.Rows[0]["isfahuo"].ToString(), dtTable.Rows[0]["kuaididanhao"].ToString(), dtTable.Rows[0]["kuaidicompany"].ToString(), dtTable.Rows[0]["kuaidiquerylink"].ToString(), dtTable.Rows[0]["remarks"].ToString(), false, false, 0, "brandname", "ordercustomername", "fahuocustomername", 0, 1,0,"","");
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
        mod.brandid = Convert.ToInt32(inputbrandid.Value.Trim());
        mod.ordercustomerid = Convert.ToInt32(inputordercustomerid.Value.Trim());
        mod.ordernumber = Convert.ToDecimal(inputordernumber.Value.Trim());
        mod.unitname = inputunitname.Value.Trim();
        mod.orderdatetime = Convert.ToDateTime(inputorderdatetime.Value.Trim());
        mod.parentcustomerid = Convert.ToInt32(inputparentcustomerid.Value.Trim());
        mod.totalprice = Convert.ToDecimal(inputtotalprice.Value.Trim());
        mod.ispay = Convert.ToBoolean(inputispay.Value);
        mod.paydatetime = inputpaydatetime.Value.Trim();
        mod.paynumber = inputpaynumber.Value.Trim();
        mod.paymethod = inputpaymethod.Value.Trim();
        mod.shouhuoren = inputshouhuoren.Value.Trim();
        mod.shouhuophonenumber = inputshouhuophonenumber.Value.Trim();
        mod.shouhuodizhi = inputshouhuodizhi.Value.Trim();
        mod.isfahuo = Convert.ToBoolean(inputisfahuo.Value.Trim());
        mod.kuaididanhao = inputkuaididanhao.Value.Trim();
        mod.kuaidicompany = inputkuaidicompany.Value.Trim();
        mod.kuaidiquerylink = inputkuaidiquerylink.Value.Trim();
        mod.remarks = inputremarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_crm_customerorderinfo(compid,brandid,ordercustomerid,ordernumber,unitname,orderdatetime,parentcustomerid,totalprice,ispay,paydatetime,paynumber,paymethod,shouhuoren,shouhuophonenumber,shouhuodizhi,isfahuo,kuaididanhao,kuaidicompany,kuaidiquerylink,remarks) VALUES("+ mod.compid+","+mod.brandid+","+mod.ordercustomerid+","+mod.ordernumber+","+mod.unitname+","+mod.orderdatetime+","+mod.parentcustomerid+","+mod.totalprice+","+mod.ispay+","+mod.paydatetime+","+mod.paynumber+","+mod.paymethod+","+mod.shouhuoren+","+mod.shouhuophonenumber+","+mod.shouhuodizhi+","+mod.isfahuo+","+mod.kuaididanhao+","+mod.kuaidicompany+","+mod.kuaidiquerylink+","+mod.remarks+")";
      tab.ExecuteQuery(tempsqlstring, null);
                 RecordDealLog(new MTJ_DealLog(0,"tj_crm_customerorderinfoAddEdit.aspx","tj_crm_customerorderinfo","描述",DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_crm_customerorderinfo SET compid="+Convert.ToInt32(mod.compid)+",brandid="+Convert.ToInt32(mod.brandid)+",ordercustomerid="+Convert.ToInt32(mod.ordercustomerid)+",ordernumber="+Convert.ToDecimal(mod.ordernumber)+",unitname="+mod.unitname+",orderdatetime="+mod.orderdatetime+",parentcustomerid="+Convert.ToInt32(mod.parentcustomerid)+",totalprice="+Convert.ToDecimal(mod.totalprice)+",ispay="+mod.ispay+",paydatetime="+mod.paydatetime+",paynumber="+mod.paynumber+",paymethod="+mod.paymethod+",shouhuoren="+mod.shouhuoren+",shouhuophonenumber="+mod.shouhuophonenumber+",shouhuodizhi="+mod.shouhuodizhi+",isfahuo="+mod.isfahuo+",kuaididanhao="+mod.kuaididanhao+",kuaidicompany="+mod.kuaidicompany+",kuaidiquerylink="+mod.kuaidiquerylink+",remarks="+mod.remarks+" where id=mod.id";
      tab.ExecuteNonQuery(tempsqlstring); 
                 RecordDealLog(new MTJ_DealLog(0,"tj_crm_customerorderinfoAddEdit.aspx","tj_crm_customerorderinfo","描述",DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_crm_customerorderinfo ms = GetModel(id);
        inputid.Value = ms.id.ToString().Trim();
        inputcompid.Value = ms.compid.ToString().Trim();
        inputbrandid.Value = ms.brandid.ToString().Trim();
        inputordercustomerid.Value = ms.ordercustomerid.ToString().Trim();
        inputordernumber.Value = ms.ordernumber.ToString().Trim();
        inputunitname.Value = ms.unitname.Trim();
        inputorderdatetime.Value = ms.orderdatetime.ToString().Trim();
        inputparentcustomerid.Value = ms.parentcustomerid.ToString().Trim();
        inputtotalprice.Value = ms.totalprice.ToString().Trim();
        inputispay.Value = ms.ispay.ToString();
        inputpaydatetime.Value = ms.paydatetime.Trim();
        inputpaynumber.Value = ms.paynumber.Trim();
        inputpaymethod.Value = ms.paymethod.Trim();
        inputshouhuoren.Value = ms.shouhuoren.Trim();
        inputshouhuophonenumber.Value = ms.shouhuophonenumber.Trim();
        inputshouhuodizhi.Value = ms.shouhuodizhi.Trim();
        inputisfahuo.Value = ms.isfahuo.ToString();
        inputkuaididanhao.Value = ms.kuaididanhao.Trim();
        inputkuaidicompany.Value = ms.kuaidicompany.Trim();
        inputkuaidiquerylink.Value = ms.kuaidiquerylink.Trim();
        inputremarks.Value = ms.remarks.Trim();
    }
}