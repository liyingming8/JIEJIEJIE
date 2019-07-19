using System;
using System.Data;
using System.Web.UI;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
public partial class Admin_tj_js_orderAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_js_order mod = new Mtj_js_order();
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

private Mtj_js_order  GetModel(int id)
{
       DataTable  dtTable =  tab.ExecuteQuery("select * from tj_js_order where id=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_js_order(Convert.ToInt32(dtTable.Rows[0]["id"]),Convert.ToInt32(dtTable.Rows[0]["userid"]),dtTable.Rows[0]["ordernumber"].ToString(),dtTable.Rows[0]["goodsid"].ToString(),dtTable.Rows[0]["goodsnum"].ToString(),dtTable.Rows[0]["goodstype"].ToString(),dtTable.Rows[0]["shouldpaymoney"].ToString(),dtTable.Rows[0]["remarks"].ToString(),dtTable.Rows[0]["yunfei"].ToString(),dtTable.Rows[0]["orderdate"].ToString(),dtTable.Rows[0]["deliveryname"].ToString(),dtTable.Rows[0]["deliveryphone"].ToString(),dtTable.Rows[0]["deliveryaddress"].ToString());
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
        mod.ordernumber = inputordernumber.Value.Trim();
        mod.goodsid = inputgoodsid.Value.Trim();
        mod.goodsnum = inputgoodsnum.Value.Trim();
        mod.goodstype = inputgoodstype.Value.Trim();
        mod.shouldpaymoney = inputshouldpaymoney.Value.Trim();
        mod.remarks = inputremarks.Value.Trim();
        mod.yunfei = inputyunfei.Value.Trim();
        mod.orderdate = inputorderdate.Value.Trim();
        mod.deliveryname = inputdeliveryname.Value.Trim();
        mod.deliveryphone = inputdeliveryphone.Value.Trim();
        mod.deliveryaddress = inputdeliveryaddress.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_js_order(userid,ordernumber,goodsid,goodsnum,goodstype,shouldpaymoney,remarks,yunfei,orderdate,deliveryname,deliveryphone,deliveryaddress) VALUES("+ mod.userid+","+mod.ordernumber+","+mod.goodsid+","+mod.goodsnum+","+mod.goodstype+","+mod.shouldpaymoney+","+mod.remarks+","+mod.yunfei+","+mod.orderdate+","+mod.deliveryname+","+mod.deliveryphone+","+mod.deliveryaddress+")";
      tab.ExecuteQuery(tempsqlstring, null);
                 RecordDealLog(new MTJ_DealLog(0,"tj_js_orderAddEdit.aspx","tj_js_order","描述",DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_js_order SET userid="+Convert.ToInt32(mod.userid)+",ordernumber="+mod.ordernumber+",goodsid="+mod.goodsid+",goodsnum="+mod.goodsnum+",goodstype="+mod.goodstype+",shouldpaymoney="+mod.shouldpaymoney+",remarks="+mod.remarks+",yunfei="+mod.yunfei+",orderdate="+mod.orderdate+",deliveryname="+mod.deliveryname+",deliveryphone="+mod.deliveryphone+",deliveryaddress="+mod.deliveryaddress+" where id=mod.id";
      tab.ExecuteNonQuery(tempsqlstring); 
                 RecordDealLog(new MTJ_DealLog(0,"tj_js_orderAddEdit.aspx","tj_js_order","描述",DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_js_order ms = GetModel(id);
        inputid.Value = ms.id.ToString().Trim();
        inputuserid.Value = ms.userid.ToString().Trim();
        inputordernumber.Value = ms.ordernumber.Trim();
        inputgoodsid.Value = ms.goodsid.Trim();
        inputgoodsnum.Value = ms.goodsnum.Trim();
        inputgoodstype.Value = ms.goodstype.Trim();
        inputshouldpaymoney.Value = ms.shouldpaymoney.Trim();
        inputremarks.Value = ms.remarks.Trim();
        inputyunfei.Value = ms.yunfei.Trim();
        inputorderdate.Value = ms.orderdate.Trim();
        inputdeliveryname.Value = ms.deliveryname.Trim();
        inputdeliveryphone.Value = ms.deliveryphone.Trim();
        inputdeliveryaddress.Value = ms.deliveryaddress.Trim();
    }
}