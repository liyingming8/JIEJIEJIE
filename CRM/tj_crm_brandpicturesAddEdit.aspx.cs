using System;
using System.Data;
using System.Web.UI;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
public partial class CRM_tj_crm_brandpicturesAddEdit : AuthorPage
{
     
    Mtj_crm_brandpictures _mod = new Mtj_crm_brandpictures(); 
    readonly PGTabExecuteCRM _tab = new PGTabExecuteCRM();
    private string _sqltempstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
       {
           if (!string.IsNullOrEmpty(Request.QueryString["bid"]))
           {
               hf_bid.Value = Sc.DecryptQueryString(Request.QueryString["bid"]);
               LabelBrandName.Text = _tab.ExecuteQueryForValue("select brandname from tj_crm_brandinfo where id=" + hf_bid.Value).ToString();
           }
           else
           {
               Response.End();
           }
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
                Fillinput(int.Parse(HF_ID.Value.Trim()));
                break;
             default:
                break;
          }
       }
    }

    private Mtj_crm_brandpictures GetModel(int id)
    {
        DataTable dtTable = _tab.ExecuteQuery("select * from tj_crm_brandpictures where id=" + id, null);
        if (dtTable.Rows.Count > 0)
        {
            return new Mtj_crm_brandpictures(Convert.ToInt32(dtTable.Rows[0]["id"]), Convert.ToInt32(dtTable.Rows[0]["brandid"]), Convert.ToInt32(dtTable.Rows[0]["compid"]), dtTable.Rows[0]["picurlstr"].ToString(), dtTable.Rows[0]["remarks"].ToString());
        }
        return null;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            _mod = GetModel(Convert.ToInt32(HF_ID.Value));
        } 
        _mod.brandid = Convert.ToInt32(hf_bid.Value);
        _mod.compid = Convert.ToInt32(GetCookieCompID());
        _mod.picurlstr = HF_LogoImage.Value;
        _mod.remarks = inputremarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                _sqltempstring = "insert into tj_crm_brandpictures(brandid,compid,picurlstr,remarks) values("+_mod.brandid+","+_mod.compid+",'"+_mod.picurlstr+"','"+_mod.remarks+"') returning id;";
                _tab.ExecuteQuery(_sqltempstring, null);
                 RecordDealLog(new MTJ_DealLog(0,"tj_crm_brandpicturesAddEdit.aspx","tj_crm_brandpictures","描述",DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                 _sqltempstring = "update tj_crm_brandpictures set brandid="+_mod.brandid+",compid="+_mod.compid+",picurlstr='"+_mod.picurlstr+"',remarks='"+_mod.remarks+"' where id=" + _mod.id;
                _tab.ExecuteQuery(_sqltempstring, null);
                 RecordDealLog(new MTJ_DealLog(0,"tj_crm_brandpicturesAddEdit.aspx","tj_crm_brandpictures","描述",DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void Fillinput(int id)
    {
        Mtj_crm_brandpictures ms = GetModel(id); 
        hf_bid.Value = ms.brandid.ToString().Trim();
        HF_LogoImage.Value = ms.picurlstr;
        Image_Logo.ImageUrl = ms.picurlstr;
        inputremarks.Value = ms.remarks.Trim();
    }
}