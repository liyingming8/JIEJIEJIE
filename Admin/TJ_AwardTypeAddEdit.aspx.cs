using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_AwardTypeAddEdit : AuthorPage
{
    BTJ_AwardType bll = new BTJ_AwardType();
    MTJ_AwardType mod = new MTJ_AwardType();
    private readonly CommonFun comfun = new CommonFun();
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
           FillDDL();
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
          if (!string.IsNullOrEmpty(Request.QueryString["pcompid"]))
          {
              ckb_compid.Checked = false;
              input.Attributes.Add("style", "style=\"display:inline-block;\"");
              hdcompid.Value = Request.QueryString["pcompid"];
              input_compid.Value = Request.QueryString["pcompnm"];
          }
          input_compid.Attributes.Add("onclick",ReturnCompnaySelectScript("指定单位","0",""));
       }
    }
    
    private void FillDDL()
    {
        comfun.BindTreeCombox(ddl_parentid, "awardtype", "id", "parentid", "TJ_AwardType", 0, "顶级...", true, "—", "");
        ddl_parentid.SelectedValue = "0";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.awardtype = inputawardtype.Value.Trim();
        mod.remarks = inputremarks.Value.Trim();
        mod.parentid = int.Parse(ddl_parentid.SelectedValue);
        if (!ckb_compid.Checked)
        {
            mod.compid = int.Parse(hdcompid.Value);
        }
        else
        {
            mod.compid = 0;
        }
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_AwardTypeAddEdit.aspx", "TJ_AwardType", "描述", DateTime.Now,
                    int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                bll.Modify(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_AwardTypeAddEdit.aspx", "TJ_AwardType", "描述", DateTime.Now,
                    int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    BTJ_RegisterCompanys btjRegister = new BTJ_RegisterCompanys();
    private void fillinput(int id)
    {
        MTJ_AwardType ms = bll.GetList(id);
        ddl_parentid.SelectedValue = ms.parentid.ToString();
        inputawardtype.Value = ms.awardtype.Trim();
        inputremarks.Value = ms.remarks.Trim();
        hdcompid.Value = ms.compid.ToString();
        if (!ms.compid.Equals(0))
        {
            input.Attributes.Add("style", "style=\"display:inline-block;\"");
            ckb_compid.Checked = false;
            input_compid.Value = btjRegister.GetList(int.Parse(hdcompid.Value)).CompName;
        }
        else
        {
            ckb_compid.Checked = true; 
        }
    }
}