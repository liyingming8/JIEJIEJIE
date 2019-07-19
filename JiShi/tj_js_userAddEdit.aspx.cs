using System;
using System.Data;
using System.Web.UI;
using TJ.Model;
using TJ.DBUtility;
using commonlib;
public partial class Admin_tj_js_userAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_js_user mod = new Mtj_js_user();
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

private Mtj_js_user  GetModel(int id)
{
       DataTable  dtTable =  tab.ExecuteQuery("select * from tj_js_user where userid=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_js_user(Convert.ToInt32(dtTable.Rows[0]["userid"]),dtTable.Rows[0]["name"].ToString(),dtTable.Rows[0]["nickname"].ToString(),dtTable.Rows[0]["password"].ToString(),dtTable.Rows[0]["sex"].ToString(),dtTable.Rows[0]["cellphone"].ToString(),dtTable.Rows[0]["telphone"].ToString(),dtTable.Rows[0]["address"].ToString(),dtTable.Rows[0]["city"].ToString(),dtTable.Rows[0]["idcard"].ToString(),dtTable.Rows[0]["postcode"].ToString(),dtTable.Rows[0]["headpic"].ToString(),dtTable.Rows[0]["qianming"].ToString(),dtTable.Rows[0]["deliveryname"].ToString(),dtTable.Rows[0]["deliveryphone"].ToString(),dtTable.Rows[0]["deliveryaddress"].ToString(),dtTable.Rows[0]["deliverycity"].ToString(),dtTable.Rows[0]["likes"].ToString(),Convert.ToInt32(dtTable.Rows[0]["compid"]),dtTable.Rows[0]["registerdate"].ToString(),dtTable.Rows[0]["wxgongzhonghao"].ToString(),dtTable.Rows[0]["wxopenid"].ToString());
}
      return null;
}

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
           mod = GetModel(Convert.ToInt32(HF_ID.Value));
        }
        mod.userid = Convert.ToInt32(inputuserid.Value.Trim());
        mod.name = inputname.Value.Trim();
        mod.nickname = inputnickname.Value.Trim();
        mod.password = inputpassword.Value.Trim();
        mod.sex = inputsex.Value.Trim();
        mod.cellphone = inputcellphone.Value.Trim();
        mod.telphone = inputtelphone.Value.Trim();
        mod.address = inputaddress.Value.Trim();
        mod.city = inputcity.Value.Trim();
        mod.idcard = inputidcard.Value.Trim();
        mod.postcode = inputpostcode.Value.Trim();
        mod.headpic = inputheadpic.Value.Trim();
        mod.qianming = inputqianming.Value.Trim();
        mod.deliveryname = inputdeliveryname.Value.Trim();
        mod.deliveryphone = inputdeliveryphone.Value.Trim();
        mod.deliveryaddress = inputdeliveryaddress.Value.Trim();
        mod.deliverycity = inputdeliverycity.Value.Trim();
        mod.likes = inputlikes.Value.Trim();
        mod.compid = Convert.ToInt32(inputcompid.Value.Trim());
        mod.registerdate = inputregisterdate.Value.Trim();
        mod.wxgongzhonghao = inputwxgongzhonghao.Value.Trim();
        mod.wxopenid = inputwxopenid.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_js_user(name,nickname,password,sex,cellphone,telphone,address,city,idcard,postcode,headpic,qianming,deliveryname,deliveryphone,deliveryaddress,deliverycity,likes,compid,registerdate,wxgongzhonghao,wxopenid) VALUES("+ mod.name+","+mod.nickname+","+mod.password+","+mod.sex+","+mod.cellphone+","+mod.telphone+","+mod.address+","+mod.city+","+mod.idcard+","+mod.postcode+","+mod.headpic+","+mod.qianming+","+mod.deliveryname+","+mod.deliveryphone+","+mod.deliveryaddress+","+mod.deliverycity+","+mod.likes+","+mod.compid+","+mod.registerdate+","+mod.wxgongzhonghao+","+mod.wxopenid+")";
      tab.ExecuteQuery(tempsqlstring, null);
                 RecordDealLog(new MTJ_DealLog(0,"tj_js_userAddEdit.aspx","tj_js_user","描述",DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_js_user SET name="+mod.name+",nickname="+mod.nickname+",password="+mod.password+",sex="+mod.sex+",cellphone="+mod.cellphone+",telphone="+mod.telphone+",address="+mod.address+",city="+mod.city+",idcard="+mod.idcard+",postcode="+mod.postcode+",headpic="+mod.headpic+",qianming="+mod.qianming+",deliveryname="+mod.deliveryname+",deliveryphone="+mod.deliveryphone+",deliveryaddress="+mod.deliveryaddress+",deliverycity="+mod.deliverycity+",likes="+mod.likes+",compid="+Convert.ToInt32(mod.compid)+",registerdate="+mod.registerdate+",wxgongzhonghao="+mod.wxgongzhonghao+",wxopenid="+mod.wxopenid+" where userid=mod.userid";
      tab.ExecuteNonQuery(tempsqlstring); 
                 RecordDealLog(new MTJ_DealLog(0,"tj_js_userAddEdit.aspx","tj_js_user","描述",DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        Mtj_js_user ms = GetModel(id);
        inputuserid.Value = ms.userid.ToString().Trim();
        inputname.Value = ms.name.Trim();
        inputnickname.Value = ms.nickname.Trim();
        inputpassword.Value = ms.password.Trim();
        inputsex.Value = ms.sex.Trim();
        inputcellphone.Value = ms.cellphone.Trim();
        inputtelphone.Value = ms.telphone.Trim();
        inputaddress.Value = ms.address.Trim();
        inputcity.Value = ms.city.Trim();
        inputidcard.Value = ms.idcard.Trim();
        inputpostcode.Value = ms.postcode.Trim();
        inputheadpic.Value = ms.headpic.Trim();
        inputqianming.Value = ms.qianming.Trim();
        inputdeliveryname.Value = ms.deliveryname.Trim();
        inputdeliveryphone.Value = ms.deliveryphone.Trim();
        inputdeliveryaddress.Value = ms.deliveryaddress.Trim();
        inputdeliverycity.Value = ms.deliverycity.Trim();
        inputlikes.Value = ms.likes.Trim();
        inputcompid.Value = ms.compid.ToString().Trim();
        inputregisterdate.Value = ms.registerdate.Trim();
        inputwxgongzhonghao.Value = ms.wxgongzhonghao.Trim();
        inputwxopenid.Value = ms.wxopenid.Trim();
    }
}