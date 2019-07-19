using System;
using System.Data;
using System.Web.UI;
using TJ.BLL;
using TJ.Model;
using TJ.DBUtility;
using commonlib;
public partial class Admin_tj_js_advertisementAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_js_advertisement mod = new Mtj_js_advertisement();
    BTJ_RegisterCompanys _bcomp = new BTJ_RegisterCompanys();
    string tempsqlstring = "";
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
            txCompID.Value = _bcomp.GetList(Convert.ToInt32(GetCookieCompID())).CompName;
            if (IsSuperAdmin())
            {
                txCompID.Attributes.Add("onclick", ReturnCompnaySelectScript("所属单位","0",""));
            }
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    break;
                case "edit":
                    Button1.Text = "修改";
                    Fillinput(int.Parse(HF_ID.Value.Trim()));
                    break; 
            }
        }
    }

    private Mtj_js_advertisement GetModel(int id)
    {
        DataTable dtTable = tab.ExecuteQuery("select * from tj_js_advertisement where id=" + id, null);
        if (dtTable.Rows.Count > 0)
        {
            return new Mtj_js_advertisement(Convert.ToInt32(dtTable.Rows[0]["id"]), Convert.ToInt32(dtTable.Rows[0]["goodsid"]), dtTable.Rows[0]["img"].ToString(), dtTable.Rows[0]["name"].ToString(), dtTable.Rows[0]["intro"].ToString(), dtTable.Rows[0]["price"].ToString(), dtTable.Rows[0]["realprice"].ToString(), dtTable.Rows[0]["position"].ToString(), Convert.ToInt32(dtTable.Rows[0]["compid"]), Convert.ToBoolean(dtTable.Rows[0]["valid"]));
        }
        return null;
    }

    private void FILLDDL()
    {
        ddl_goodsid.DataSource = tab.ExecuteQuery("select goodsid,name from tj_js_goods", null);
        //ddl_goodsid.DataSource =tab.ExecuteQuery("select goodsid,name from tj_js_goods where compid=" + GetCookieCompID(), null);
        ddl_goodsid.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = GetModel(Convert.ToInt32(HF_ID.Value));
        }
        mod.goodsid = Convert.ToInt32(ddl_goodsid.SelectedValue);
        mod.img = inputimg.Value.Trim();
        mod.name = ddl_goodsid.SelectedItem.Text.Trim();
        mod.intro = inputintro.Value.Trim();
        mod.price = inputprice.Value.Trim();
        mod.realprice = inputrealprice.Value.Trim();
        mod.position = inputposition.Value.Trim();
        mod.compid = Convert.ToInt32(GetCookieCompID());
        mod.valid = ckbvalid.Checked;
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                tempsqlstring = "INSERT INTO tj_js_advertisement(goodsid,img,name,intro,price,realprice,position,compid,valid) VALUES(" + mod.goodsid + "," + mod.img + "," + mod.name + "," + mod.intro + "," + mod.price + "," + mod.realprice + "," + mod.position + "," + mod.compid + "," + mod.valid + ")";
                tab.ExecuteQuery(tempsqlstring, null);
                RecordDealLog(new MTJ_DealLog(0, "tj_js_advertisementAddEdit.aspx", "tj_js_advertisement", "描述", DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                tempsqlstring = "UPDATE  tj_js_advertisement SET goodsid=" + Convert.ToInt32(mod.goodsid) + ",img=" + mod.img + ",name=" + mod.name + ",intro=" + mod.intro + ",price=" + mod.price + ",realprice=" + mod.realprice + ",position=" + mod.position + ",compid=" + Convert.ToInt32(mod.compid) + ",valid=" + Convert.ToBoolean(mod.valid) + " where id=mod.id";
                tab.ExecuteNonQuery(tempsqlstring);
                RecordDealLog(new MTJ_DealLog(0, "tj_js_advertisementAddEdit.aspx", "tj_js_advertisement", "描述", DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void Fillinput(int id)
    {
        Mtj_js_advertisement ms = GetModel(id);
        ddl_goodsid.SelectedValue = ms.goodsid.ToString().Trim();
        inputimg.Value = ms.img.Trim();
        inputintro.Value = ms.intro.Trim();
        inputprice.Value = ms.price.Trim();
        inputrealprice.Value = ms.realprice.Trim();
        inputposition.Value = ms.position.Trim();
        ckbvalid.Checked = ms.valid;
    }
}