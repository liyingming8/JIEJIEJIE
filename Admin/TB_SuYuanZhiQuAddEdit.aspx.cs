using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TB_SuYuanZhiQuAddEdit : AuthorPage
{
    readonly BTB_SuYuanZhiQu bll = new BTB_SuYuanZhiQu();
    MTB_SuYuanZhiQu mod = new MTB_SuYuanZhiQu();
    private readonly BTB_SuYuanKuFang bsycangk = new BTB_SuYuanKuFang();
    private readonly BTB_SuYuanPai bsyfangpai = new BTB_SuYuanPai();
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
           inputStartTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
           inputEndTime.Value = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"); 
          switch (HF_CMD.Value)
          {
             case "add":
                Button1.Text = "添加";
                break;
             case "edit":
                ButtonShengCheng.Enabled = false;
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
        mod.ZhiQuPC = inputZhiQuPC.Value.Trim();
        mod.Compid = Convert.ToInt32(GetCookieCompID());
        mod.Remarks = inputRemarks.Value.Trim();
        mod.ZJYID = Convert.ToInt32(ComboBoxZJYID.SelectedValue);
        mod.StartTime = Convert.ToDateTime(inputStartTime.Value);
        mod.EndTime = Convert.ToDateTime(inputEndTime.Value);
        mod.KuFangID = Convert.ToInt32(CompBox_KuFang.SelectedValue);
        mod.KuFangPaiID = Convert.ToInt32(CompBox_FangPai.SelectedValue);
        //mod.Remarks1 = inputRemarks1.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.ZhiQuTime = DateTime.Now;
                bll.Insert(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanZhiQuAddEdit.aspx","TB_SuYuanZhiQu","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanZhiQuAddEdit.aspx","TB_SuYuanZhiQu","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void Fillinput(int id)
    {
        MTB_SuYuanZhiQu ms = bll.GetList(id); 
        inputStartTime.Value = ms.StartTime.ToString("yyyy-MM-dd").Trim();
        inputEndTime.Value = ms.EndTime.ToString("yyyy-MM-dd").Trim();
        CompBox_KuFang.SelectedValue = ms.KuFangID.ToString().Trim();
        CompBox_FangPai.SelectedValue = ms.KuFangPaiID.ToString().Trim();
        inputZhiQuPC.Value = ms.ZhiQuPC.Trim(); 
        inputRemarks.Value = ms.Remarks.Trim();
        ComboBoxZJYID.SelectedValue = ms.ZJYID.ToString();
        
    }

    readonly BTB_SuYuanZhiJianInfo bsyzjy = new BTB_SuYuanZhiJianInfo();
    private void FillDdl()
    {
        ComboBoxZJYID.DataSource = bsyzjy.GetListsByFilterString("Compid=" + GetCookieCompID());
        ComboBoxZJYID.DataBind();
        ComboBoxZJYID.Items.Add(new ListItem("质检员...","0"));
        ComboBoxZJYID.SelectedValue = "0";
        CompBox_KuFang.DataSource = bsycangk.GetListsByFilterString("Compid=" + GetCookieCompID() + " and KuFangType in (" + DAConfig.SYCKZH + "," + DAConfig.SYCKYQ + ")");
        CompBox_KuFang.DataBind();
        CompBox_KuFang.Items.Add(new ListItem("库房...","0"));
        CompBox_KuFang.SelectedValue = "0";
        CompBox_FangPai.DataSource = bsyfangpai.GetListsByFilterString("Compid=" + GetCookieCompID());
        CompBox_FangPai.DataBind();
        CompBox_FangPai.Items.Add(new ListItem("房排...","0"));
        CompBox_FangPai.SelectedValue = "0";
    }
    protected void ButtonShengCheng_Click(object sender, EventArgs e)
    {
        inputZhiQuPC.Value = "YQ-" + DateTime.Now.ToString("yyyyMM") + "-" + GetIndexCouter();
    }

    private string GetIndexCouter()
    {
        return (10000 + (bll.GetListsByFilterString("Compid="+GetCookieCompID()+" and convert(nvarchar(7),ZhiQuTime,120)='" + DateTime.Now.ToString("yyyy-MM") + "'").Count + 1)).ToString().Substring(1);
    }
}