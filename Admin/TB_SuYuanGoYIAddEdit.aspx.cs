using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TB_SuYuanGoYIAddEdit : AuthorPage
{
    readonly BTB_SuYuanGoYI _bll = new BTB_SuYuanGoYI();
    MTB_SuYuanGoYI _mod = new MTB_SuYuanGoYI();
    readonly BTB_Products_Infor _bproduct = new BTB_Products_Infor();
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
          FillDdl();
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
        _mod.PID = Convert.ToInt32(ComboBoxPID.SelectedValue);
        _mod.GoYiName = inputGoYiName.Value.Trim();
        _mod.Description = inputDescription.Value.Trim();
        _mod.ImageUrl = HF_LogoImage.Value;
        _mod.ShowOrder = inputShowOrder.Value.Trim();
        _mod.Compid = Convert.ToInt32(GetCookieCompID());
        _mod.Remarks1 = inputRemarks1.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                _bll.Insert(_mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanGoYIAddEdit.aspx","TB_SuYuanGoYI","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                _bll.Modify(_mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanGoYIAddEdit.aspx","TB_SuYuanGoYI","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void Fillinput(int id)
    {
        MTB_SuYuanGoYI ms = _bll.GetList(id); 
        ComboBoxPID.SelectedValue = ms.PID.ToString().Trim();
        inputGoYiName.Value = ms.GoYiName.Trim();
        inputDescription.Value = ms.Description.Trim();
        HF_LogoImage.Value = ms.ImageUrl.Trim();
        Image_Logo.ImageUrl = ms.ImageUrl.Trim();
        inputShowOrder.Value = ms.ShowOrder.Trim(); 
        inputRemarks1.Value = ms.Remarks1.Trim();
    }
    
    private void FillDdl()
    {
        ComboBoxPID.DataSource = _bproduct.GetListsByFilterString("CompID=" + GetCookieCompID(), "Products_Name");
        ComboBoxPID.DataBind();
        ComboBoxPID.Items.Add(new ListItem("产品...","0"));
        ComboBoxPID.SelectedValue = "0";
    }
}