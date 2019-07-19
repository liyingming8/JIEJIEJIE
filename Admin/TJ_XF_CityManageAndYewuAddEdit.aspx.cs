using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_XF_CityManageAndYewuAddEdit : AuthorPage
{
    BTJ_XF_CityManageAndYewu bll = new BTJ_XF_CityManageAndYewu();
    MTJ_XF_CityManageAndYewu mod = new MTJ_XF_CityManageAndYewu();
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
        mod.citymanageid = Convert.ToInt32(inputcitymanageid.Value.Trim());
        mod.yewuuserid = Convert.ToInt32(inputyewuuserid.Value.Trim());
        mod.yewudaibiaona = inputyewudaibiaona.Value.Trim();
        mod.authoruid = Convert.ToInt32(inputauthoruid.Value.Trim());
        mod.authordate = Convert.ToDateTime(inputauthordate.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_XF_CityManageAndYewuAddEdit.aspx","TJ_XF_CityManageAndYewu","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_XF_CityManageAndYewuAddEdit.aspx","TJ_XF_CityManageAndYewu","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTJ_XF_CityManageAndYewu ms = bll.GetList(id);
        inputid.Value = ms.id.ToString().Trim();
        inputcitymanageid.Value = ms.citymanageid.ToString().Trim();
        inputyewuuserid.Value = ms.yewuuserid.ToString().Trim();
        inputyewudaibiaona.Value = ms.yewudaibiaona.ToString().Trim();
        inputauthoruid.Value = ms.authoruid.ToString().Trim();
        inputauthordate.Value = ms.authordate.ToString().Trim();
    }
}