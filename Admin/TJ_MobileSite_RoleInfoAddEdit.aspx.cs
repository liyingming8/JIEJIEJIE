using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_MobileSite_RoleInfoAddEdit : AuthorPage
{
    readonly BTJ_MobileSite_RoleInfo _bll = new BTJ_MobileSite_RoleInfo();
    MTJ_MobileSite_RoleInfo _mod = new MTJ_MobileSite_RoleInfo();
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
                Fillinput(int.Parse(HF_ID.Value.Trim()));
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
           _mod = _bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        _mod.id = Convert.ToInt32(inputid.Value.Trim());
        _mod.compid = Convert.ToInt32(inputcompid.Value.Trim());
        _mod.rid = Convert.ToInt32(inputrid.Value.Trim());
        _mod.rname = inputrname.Value.Trim();
        _mod.createuid = Convert.ToInt32(inputcreateuid.Value.Trim());
        _mod.createtm = Convert.ToDateTime(inputcreatetm.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                _bll.Insert(_mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_MobileSite_RoleInfoAddEdit.aspx","TJ_MobileSite_RoleInfo","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                _bll.Modify(_mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_MobileSite_RoleInfoAddEdit.aspx","TJ_MobileSite_RoleInfo","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void Fillinput(int id)
    {
        MTJ_MobileSite_RoleInfo ms = _bll.GetList(id);
        inputid.Value = ms.id.ToString().Trim();
        inputcompid.Value = ms.compid.ToString().Trim();
        inputrid.Value = ms.rid.ToString().Trim();
        inputrname.Value = ms.rname.Trim();
        inputcreateuid.Value = ms.createuid.ToString().Trim();
        inputcreatetm.Value = ms.createtm.ToString().Trim();
    }
}