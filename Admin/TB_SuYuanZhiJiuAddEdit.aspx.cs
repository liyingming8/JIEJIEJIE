using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TB_SuYuanZhiJiuAddEdit : AuthorPage
{
    readonly BTB_SuYuanZhiJiu bll = new BTB_SuYuanZhiJiu();
    MTB_SuYuanZhiJiu mod = new MTB_SuYuanZhiJiu();
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
           inputZhiJiuTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
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
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.ZJPC = inputZJPC.Value.Trim();
        mod.ZhiJianYuanID = ComboBoxZhiJianYuanID.SelectedValue;
        mod.ZhiJiuTime = Convert.ToDateTime(inputZhiJiuTime.Value.Trim());
        mod.CangKuID = Convert.ToInt32(ComboBoxCangKuID.SelectedValue);
        mod.Remarks = inputRemarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.Compid = Convert.ToInt32(GetCookieCompID());
                bll.Insert(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanZhiJiuAddEdit.aspx","TB_SuYuanZhiJiu","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanZhiJiuAddEdit.aspx","TB_SuYuanZhiJiu","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void Fillinput(int id)
    {
        MTB_SuYuanZhiJiu ms = bll.GetList(id); 
        inputZJPC.Value = ms.ZJPC.Trim();
        ComboBoxZhiJianYuanID.SelectedValue = ms.ZhiJianYuanID; 
        inputZhiJiuTime.Value = ms.ZhiJiuTime.ToString("yyyy-MM-dd").Trim();
        ComboBoxCangKuID.SelectedValue = ms.CangKuID.ToString().Trim(); 
        inputRemarks.Value = ms.Remarks.Trim();
    }
    protected void ButtonGenerate_Click(object sender, EventArgs e)
    {
        inputZJPC.Value = "ZJ-" + DateTime.Now.ToString("yyyyMM") + "-" + GetIndexCouter();
    }

    private string GetIndexCouter()
    {
        return (10000 + (bll.GetListsByFilterString("Compid="+GetCookieCompID()+" and convert(nvarchar(7),ZhiJiuTime,120)='" + DateTime.Now.ToString("yyyy-MM") + "'").Count + 1)).ToString().Substring(1);
    }

    readonly BTB_SuYuanKuFang _btbSuYuanKuFang = new BTB_SuYuanKuFang();
    readonly BTB_SuYuanZhiJianInfo bsyzjy = new BTB_SuYuanZhiJianInfo(); 
    private void FillDdl()
    { 
        ComboBoxCangKuID.DataSource = _btbSuYuanKuFang.GetListsByFilterString("Compid=" + GetCookieCompID()+ " and KuFangType in (" + DAConfig.SYCKZH + "," + DAConfig.SYCKJJ + ")");
        ComboBoxCangKuID.DataBind();
        ComboBoxCangKuID.Items.Add(new ListItem("库房...", "0"));
        ComboBoxCangKuID.SelectedValue = "0";
        ComboBoxZhiJianYuanID.DataSource = bsyzjy.GetListsByFilterString("Compid=" + GetCookieCompID());
        ComboBoxZhiJianYuanID.DataBind();
        ComboBoxZhiJianYuanID.Items.Add(new ListItem("质检员...", "0"));
        ComboBoxZhiJianYuanID.SelectedValue = "0";
    }
}