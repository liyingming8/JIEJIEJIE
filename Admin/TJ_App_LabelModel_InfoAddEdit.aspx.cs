using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_App_LabelModel_InfoAddEdit : AuthorPage
{
    BTJ_App_LabelModel_Info bll = new BTJ_App_LabelModel_Info();
    MTJ_App_LabelModel_Info mod = new MTJ_App_LabelModel_Info();
    BTJ_RegisterCompanys btjRegisterCompanys = new BTJ_RegisterCompanys();
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
          if (!string.IsNullOrEmpty(Request.QueryString["pcompid"]))
          {
              HF_CompID.Value = Request.QueryString["pcompid"];
              inputcompid.Value = btjRegisterCompanys.GetList(int.Parse(HF_CompID.Value)).CompName;
          }
          inputcompid.Attributes.Add("onclick",ReturnCompnaySelectScript("选择公司","0",""));
       }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CompID.Value.Length.Equals(0) || string.IsNullOrEmpty(HF_CompID.Value))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('请指定公司信息!');", true);
            return;
        }
        if (string.IsNullOrEmpty(TextB.Value) || string.IsNullOrEmpty(TextS.Value))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('请正确输入!');", true);
            return;
        }
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.compid = Convert.ToInt32(HF_CompID.Value.Trim());
        mod.labelmodelvalue = TextB.Value + ":" + TextM.Value + ":" + TextS.Value;
        mod.labelmodeldiscription = TextB.Value + "托" + (TextM.Value.Equals("0") ? "" : TextM.Value + "托") + TextS.Value;
        mod.createtm = DateTime.Now;
        mod.createuserid = Convert.ToInt32(GetCookieUID());
        mod.iscommonswm = false;
        mod.remarks = inputremarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_App_LabelModel_InfoAddEdit.aspx", "TJ_App_LabelModel_Info", "描述",
                    System.DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                bll.Modify(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_App_LabelModel_InfoAddEdit.aspx", "TJ_App_LabelModel_Info", "描述",
                    System.DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "reload", "closemyWindow();", true);
    }

    BTJ_User btjUser = new BTJ_User();
    private void fillinput(int id)
    {
        MTJ_App_LabelModel_Info ms = bll.GetList(id);
        HF_CompID.Value = ms.compid.ToString();
        inputcompid.Value = btjRegisterCompanys.GetList(int.Parse(HF_CompID.Value)).CompName;
        string[] temp = ms.labelmodelvalue.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
        TextB.Value = temp[0].Trim();
        TextM.Value = temp[1].Trim();
        TextS.Value = temp[2].Trim();
        Label_userid.Text = btjUser.GetList(ms.createuserid).LoginName;
        Label_updatetm.Text = ms.createtm.ToString("yyyy-MM-dd HH:mm:ss");
        inputremarks.Value = ms.remarks.Trim();
    }
}