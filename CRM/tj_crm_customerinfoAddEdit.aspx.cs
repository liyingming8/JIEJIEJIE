using System;
using System.Data;
using System.Web.UI;
using TJ.DBUtility;
using TJ.Model;
using commonlib;

public partial class CRM_tj_crm_customerinfoAddEdit : AuthorPage
{
    Mtj_crm_customerinfo mod = new Mtj_crm_customerinfo();
    readonly PGTabExecuteCRM tab = new PGTabExecuteCRM();
    readonly TabExecute _tab = new TabExecute();
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
            FILLDDL();
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    btnDelete.Style["display"] = "none";//新增界面隐藏删除按钮
                    break;
                case "edit":
                    Button1.Text = "修改";
                    rowreturnpsw.Visible = true;
                    Fillinput(int.Parse(HF_ID.Value.Trim()));
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["sid"]))
            {
                parentid.Value = Sc.DecryptQueryString(Request.QueryString["sid"].ToString());
                input_parent_nm.Value = Sc.DecryptQueryString(Request.QueryString["snm"].ToString());
            }
            input_parent_nm.Attributes.Add("onclick", "openWinCenter('tj_crm_customerinfo_select.aspx?fr="+Sc.EncryptQueryString(Request.Path)+ "&para="+Sc.EncryptQueryString("cmd="+Sc.EncryptQueryString(HF_CMD.Value)+"&ID="+Sc.EncryptQueryString(HF_ID.Value))+"', 500, 420, '重新指定父级')");
        }
    }

    private string tempsqlstring = "";
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = GetModel(Convert.ToInt32(HF_ID.Value));
        }
        mod.parentid = int.Parse(string.IsNullOrEmpty(parentid.Value) ? "0" : parentid.Value);
        mod.customername = tx_customername.Value;
        mod.username = tx_wxusernumber.Value;
        mod.idcardnumber = txidcardnumber.Value;
        mod.sexinfo = RBL_SexInfo.SelectedItem.Text;
        mod.phonenumber = txphonenumber.Value;
        mod.wxusernumber = tx_wxusernumber.Value;
        mod.cityid = Convert.ToInt32(ComboBox_Cityid.SelectedValue);
        mod.addressinfo = txaddressinfo.Value;
        mod.authorcode = txauthorcode.Value;
        mod.gradeid = Convert.ToInt32(ddlgradeorder.SelectedValue);
        mod.identicardpicurl = HF_LogoImage.Value;
        mod.ispermit = CheckBox_Ispermit.Checked;
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.compid = Convert.ToInt32(GetCookieCompID());
                tempsqlstring = "INSERT INTO tj_crm_customerinfo(parentid,compid,customername,username,idcardnumber,sexinfo,phonenumber,telnumber,faxnumber,cityid,addressinfo,wxappid,wxopenid,wxusernumber,identicardpicurl,authorcode,passwordstring,gradeid) VALUES(" + mod.parentid + "," + mod.compid + ",'" + mod.customername + "','" + mod.username + "','" + mod.idcardnumber + "','" + mod.sexinfo + "','" + mod.phonenumber + "','" + mod.telnumber + "','" + mod.faxnumber + "'," + mod.cityid + ",'" + mod.addressinfo + "','" + mod.wxappid + "','" + mod.wxopenid + "','" + mod.wxusernumber + "','" + mod.identicardpicurl + "','" + mod.authorcode + "','" + mod.phonenumber.Substring(mod.phonenumber.Length - 6) + "'," + mod.gradeid + ")";
                tab.ExecuteQuery(tempsqlstring, null);
                RecordDealLog(new MTJ_DealLog(0, "tj_crm_customerinfoAddEdit.aspx", "tj_crm_customerinfo", "描述", DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                tempsqlstring = "UPDATE  tj_crm_customerinfo SET parentid=" + Convert.ToInt32(mod.parentid) + ",compid=" + Convert.ToInt32(mod.compid) + ",customername='" + mod.customername + "',username='" + mod.username + "',idcardnumber='" + mod.idcardnumber + "',sexinfo='" + mod.sexinfo + "',phonenumber='" + mod.phonenumber + "',telnumber='" + mod.telnumber + "',faxnumber='" + mod.faxnumber + "',cityid=" + Convert.ToInt32(mod.cityid) + ",addressinfo='" + mod.addressinfo + "',wxusernumber='" + mod.wxusernumber + "',identicardpicurl='" + mod.identicardpicurl + "',authorcode='" + mod.authorcode + "',gradeid=" + Convert.ToInt32(mod.gradeid)+",ispermit='" + mod.ispermit.ToString()+"'"+(CheckBox_ReturnPSW.Checked?",passwordstring='"+mod.phonenumber.Substring(mod.phonenumber.Length-6)+"'":"")+ " where id=" + mod.id;
                tab.ExecuteNonQuery(tempsqlstring);
                RecordDealLog(new MTJ_DealLog(0, "tj_crm_customerinfoAddEdit.aspx", "tj_crm_customerinfo", "描述", DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }
    Mtj_crm_customerinfo modtemp = new Mtj_crm_customerinfo();
    private Mtj_crm_customerinfo GetModel(int id)
    {
        DataTable dtTable = tab.ExecuteQuery("select * from tj_crm_customerinfo where id=" + id, null);
        if (dtTable.Rows.Count > 0)
        {
            modtemp = new Mtj_crm_customerinfo(Convert.ToInt32(dtTable.Rows[0]["id"]), Convert.ToInt32(dtTable.Rows[0]["parentid"].Equals(DBNull.Value) ? "0" : dtTable.Rows[0]["parentid"]), Convert.ToInt32(dtTable.Rows[0]["compid"].Equals(DBNull.Value) ? "0" : dtTable.Rows[0]["compid"]), dtTable.Rows[0]["customername"].ToString(), dtTable.Rows[0]["username"].ToString(), dtTable.Rows[0]["idcardnumber"].ToString(), dtTable.Rows[0]["sexinfo"].ToString(), dtTable.Rows[0]["phonenumber"].ToString(), dtTable.Rows[0]["telnumber"].ToString(), dtTable.Rows[0]["faxnumber"].ToString(), Convert.ToInt32(dtTable.Rows[0]["cityid"].Equals(DBNull.Value) ? "0" : dtTable.Rows[0]["cityid"]), dtTable.Rows[0]["addressinfo"].ToString(), dtTable.Rows[0]["wxappid"].ToString(), dtTable.Rows[0]["wxopenid"].ToString(), dtTable.Rows[0]["wxusernumber"].ToString(), dtTable.Rows[0]["identicardpicurl"].ToString(), dtTable.Rows[0]["authorcode"].ToString(), dtTable.Rows[0]["passwordstring"].ToString(), Convert.ToInt32(dtTable.Rows[0]["gradeid"].Equals(DBNull.Value) ? "0" : dtTable.Rows[0]["gradeid"]),"",bool.Parse(dtTable.Rows[0]["ispermit"].ToString()),"正常","","",Guid.NewGuid().ToString("N"),"","pcd","ccd","dcd",1,1, dtTable.Rows[0]["businesspicurl"].ToString(), "");
        }
        else
        {
            modtemp = null;
        }
        dtTable.Dispose();
        return modtemp;
    }

    readonly CommonFunCrm commoncrm = new CommonFunCrm();
    readonly CommonFun common = new CommonFun();
    private void FILLDDL()
    { 
        common.BindTreeCombox(ComboBox_Cityid, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "请选择...", true, "-", "1=1");
        ComboBox_Cityid.SelectedValue = "0";
        ddlgradeorder.DataSource =
            tab.ExecuteQuery("select id,gradename from tj_crm_customergrade where compid=" + GetCookieCompID() + " order by gradeorder", null);
        ddlgradeorder.DataBind();
        ddlgradeorder.SelectedIndex = 0;
    }

    private void Fillinput(int id)
    {
        Mtj_crm_customerinfo ms = GetModel(id);
        tx_customername.Value = ms.customername;
        tx_wxusernumber.Value = ms.wxusernumber;
        txidcardnumber.Value = ms.idcardnumber.Trim();
        txphonenumber.Value = ms.phonenumber.Trim();
        txaddressinfo.Value = ms.addressinfo;
        txauthorcode.Value = ms.authorcode;
        ComboBox_Cityid.SelectedValue = ms.cityid.ToString();
        input_parent_nm.Value = GetParentInfo(ms.parentid.ToString());
        //ComboBox_parentid.SelectedValue = ms.parentid.ToString();
        parentid.Value = ms.parentid.ToString();
        ddlgradeorder.SelectedValue = ms.gradeid.ToString();
        HF_LogoImage.Value = ms.identicardpicurl;
        Image_Logo.ImageUrl = "http://tjcrm.tj1.me/crm_V2/"+ms.identicardpicurl;
        Image_cardBehind.ImageUrl = "http://tjcrm.tj1.me/crm_V2/" + ms.businesspicurl;
        CheckBox_Ispermit.Checked = ms.ispermit;
    }

    private string GetParentInfo(string currentid)
    {
        return tab.ExecuteQueryForValue("select customername from tj_crm_customerinfo where id="+currentid).ToString();
    }


    /*
         * 删除
         */
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            var deleteId = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
            string sql = "delete from tj_crm_customerinfo where id=" + deleteId;
            DataTable result = tab.ExecuteNonQuery(sql);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
    }

}