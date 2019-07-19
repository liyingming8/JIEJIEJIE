using System;
using System.Data;
using System.Web.UI;
using TJ.Model;
using TJ.DBUtility;
using commonlib;
public partial class CRM_tj_page_modelAddEdit : AuthorPage
{
    PGTabExecuteCRM tab = new PGTabExecuteCRM();
    Mtj_page_model mod = new Mtj_page_model();
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
                btnDelete.Style["display"] = "none";//新增界面隐藏删除按钮
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

private Mtj_page_model  GetModel(int id)
{
       DataTable  dtTable =  tab.ExecuteQuery("select * from tj_page_model where id=" + id, null);
       if (dtTable.Rows.Count > 0)
       {
           return new Mtj_page_model(Convert.ToInt32(dtTable.Rows[0]["id"]),dtTable.Rows[0]["pagename"].ToString(),dtTable.Rows[0]["logourl"].ToString(),dtTable.Rows[0]["linkpathstring"].ToString(),dtTable.Rows[0]["belongmodel"].ToString(),Convert.ToBoolean(dtTable.Rows[0]["isneeded"]),dtTable.Rows[0]["remarks"].ToString(),dtTable.Rows[0]["userurl"].ToString());
}
      return null;
}

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
           mod = GetModel(Convert.ToInt32(HF_ID.Value));
        } 
        mod.pagename = inputpagename.Value.Trim();
        mod.logourl = HF_LogoImage.Value;
        mod.linkpathstring = inputlinkpathstring.Value.Trim();
        mod.belongmodel = "CRM";
        mod.isneeded = ckb_isneeded.Checked;
        mod.remarks = inputremarks.Value.Trim();
         
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                 tempsqlstring = "INSERT INTO tj_page_model(pagename,logourl,linkpathstring,belongmodel,isneeded,remarks) VALUES('"+ mod.pagename+"','"+mod.logourl+"','"+mod.linkpathstring+"','"+mod.belongmodel+"',"+mod.isneeded+",'"+mod.remarks+"')";
      tab.ExecuteQuery(tempsqlstring, null);
                 RecordDealLog(new MTJ_DealLog(0,"tj_page_modelAddEdit.aspx","tj_page_model","描述",DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                 tempsqlstring = "UPDATE  tj_page_model SET pagename='"+mod.pagename+"',logourl='"+mod.logourl+"',linkpathstring='"+mod.linkpathstring+"',belongmodel='"+mod.belongmodel+"',isneeded="+Convert.ToBoolean(mod.isneeded)+",remarks='"+mod.remarks+"' where id="+mod.id;
      tab.ExecuteNonQuery(tempsqlstring); 
                 RecordDealLog(new MTJ_DealLog(0,"tj_page_modelAddEdit.aspx","tj_page_model","描述",DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void Fillinput(int id)
    {
        Mtj_page_model ms = GetModel(id); 
        inputpagename.Value = ms.pagename.Trim();
        Image_logourl.ImageUrl = ms.logourl;
        HF_LogoImage.Value = ms.logourl.Trim();
        inputlinkpathstring.Value = ms.linkpathstring.Trim(); 
        ckb_isneeded.Checked = ms.isneeded;
        inputremarks.Value = ms.remarks.Trim(); 
    }
    /*
     * 删除
     */
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            var deleteId = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
            string sql = "delete from tj_page_model where id=" + deleteId;
            DataTable result = tab.ExecuteNonQuery(sql);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
    }

}