using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_DepartMent_CompAuthorAddEdit : AuthorPage
{
    BTJ_DepartMent_CompAuthor bll = new BTJ_DepartMent_CompAuthor();
    MTJ_DepartMent_CompAuthor mod = new MTJ_DepartMent_CompAuthor();
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
           mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.id = Convert.ToInt32(inputid.Value.Trim());
        mod.departid = Convert.ToInt32(inputdepartid.Value.Trim());
        mod.authorcompid = Convert.ToInt32(inputauthorcompid.Value.Trim());
        mod.authortm = Convert.ToDateTime(inputauthortm.Value.Trim());
        mod.authoruserid = Convert.ToInt32(inputauthoruserid.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_DepartMent_CompAuthorAddEdit.aspx","TJ_DepartMent_CompAuthor","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_DepartMent_CompAuthorAddEdit.aspx","TJ_DepartMent_CompAuthor","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTJ_DepartMent_CompAuthor ms = bll.GetList(id);
        inputid.Value = ms.id.ToString().Trim();
        inputdepartid.Value = ms.departid.ToString().Trim();
        inputauthorcompid.Value = ms.authorcompid.ToString().Trim();
        inputauthortm.Value = ms.authortm.ToString().Trim();
        inputauthoruserid.Value = ms.authoruserid.ToString().Trim();
    }
}