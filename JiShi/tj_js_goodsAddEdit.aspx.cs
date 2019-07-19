using System;
using System.Data;
using System.Web.UI;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
public partial class Admin_tj_js_goodsAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_js_goods mod = new Mtj_js_goods();
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

private Mtj_js_goods  GetModel(int id)
{
       DataTable  dtTable =  tab.ExecuteQuery("select * from tj_js_goods where id=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_js_goods(Convert.ToInt32(dtTable.Rows[0]["id"]),Convert.ToInt32(dtTable.Rows[0]["goodsid"]),dtTable.Rows[0]["name"].ToString(),dtTable.Rows[0]["price"].ToString(),dtTable.Rows[0]["img"].ToString(),dtTable.Rows[0]["type"].ToString(),dtTable.Rows[0]["position"].ToString(),dtTable.Rows[0]["intro"].ToString(),dtTable.Rows[0]["remark"].ToString(),dtTable.Rows[0]["realprice"].ToString(),Convert.ToBoolean(dtTable.Rows[0]["valid"]),Convert.ToInt32(dtTable.Rows[0]["compid"]));
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
        mod.goodsid = Convert.ToInt32(inputgoodsid.Value.Trim());
        mod.name = inputname.Value.Trim();
        mod.price = inputprice.Value.Trim();
        mod.img = inputimg.Value.Trim();
        mod.type = inputtype.Value.Trim();
        mod.position = inputposition.Value.Trim();
        mod.intro = inputintro.Value.Trim();
        mod.remark = inputremark.Value.Trim();
        mod.realprice = inputrealprice.Value.Trim();
        mod.valid = Convert.ToBoolean(inputvalid.Value.Trim());
        mod.compid = Convert.ToInt32(inputcompid.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_js_goods(goodsid,name,price,img,type,position,intro,remark,realprice,valid,compid) VALUES("+ mod.goodsid+","+mod.name+","+mod.price+","+mod.img+","+mod.type+","+mod.position+","+mod.intro+","+mod.remark+","+mod.realprice+","+mod.valid+","+mod.compid+")";
      tab.ExecuteQuery(tempsqlstring, null);
      RecordDealLog(new MTJ_DealLog(0, "tj_js_goodsAddEdit.aspx", "tj_js_goods", "描述", DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_js_goods SET goodsid="+Convert.ToInt32(mod.goodsid)+",name="+mod.name+",price="+mod.price+",img="+mod.img+",type="+mod.type+",position="+mod.position+",intro="+mod.intro+",remark="+mod.remark+",realprice="+mod.realprice+",valid="+Convert.ToBoolean(mod.valid)+",compid="+Convert.ToInt32(mod.compid)+" where id=mod.id";
      tab.ExecuteNonQuery(tempsqlstring);
      RecordDealLog(new MTJ_DealLog(0, "tj_js_goodsAddEdit.aspx", "tj_js_goods", "描述", DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_js_goods ms = GetModel(id);
        inputid.Value = ms.id.ToString().Trim();
        inputgoodsid.Value = ms.goodsid.ToString().Trim();
        inputname.Value = ms.name.Trim();
        inputprice.Value = ms.price.Trim();
        inputimg.Value = ms.img.Trim();
        inputtype.Value = ms.type.Trim();
        inputposition.Value = ms.position.Trim();
        inputintro.Value = ms.intro.Trim();
        inputremark.Value = ms.remark.Trim();
        inputrealprice.Value = ms.realprice.Trim();
        inputvalid.Value = ms.valid.ToString().Trim();
        inputcompid.Value = ms.compid.ToString().Trim();
    }
}