using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TB_SuYuanGouDuiAndZhiJiuAddEdit : AuthorPage
{
    readonly BTB_SuYuanGouDuiAndZhiJiu bll = new BTB_SuYuanGouDuiAndZhiJiu();
    MTB_SuYuanGouDuiAndZhiJiu mod = new MTB_SuYuanGouDuiAndZhiJiu();
    readonly BTB_SuYuanZhiJiu btbSuYuanZhiJiu = new BTB_SuYuanZhiJiu();
    readonly BTB_SuYuanGouDui btbSuYuanGouDui = new BTB_SuYuanGouDui();
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
           if (!string.IsNullOrEmpty(Request.QueryString["GDID"]))
           {
               HF_GDID.Value = Sc.DecryptQueryString(Request.QueryString["GDID"]);
               LabelGouDuiID.Text = btbSuYuanGouDui.GetList(int.Parse(HF_GDID.Value)).GDPC;
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
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.ZhiJiuID = Convert.ToInt32(ComboBoxZhiJiuID.SelectedValue);
        mod.GouDuiID = Convert.ToInt32(HF_GDID.Value);
        mod.PercentValue = Convert.ToDecimal(inputPercentValue.Value.Trim());
        mod.Remarks = inputRemarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanGouDuiAndZhiJiuAddEdit.aspx","TB_SuYuanGouDuiAndZhiJiu","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanGouDuiAndZhiJiuAddEdit.aspx","TB_SuYuanGouDuiAndZhiJiu","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void FillDdl()
    {
        ComboBoxZhiJiuID.DataSource = btbSuYuanZhiJiu.GetListsByFilterString("Compid=" + GetCookieCompID());
        ComboBoxZhiJiuID.DataBind();
        ComboBoxZhiJiuID.Items.Add(new ListItem("制酒批次...","0"));
        ComboBoxZhiJiuID.SelectedValue = "0";
    }

    private void Fillinput(int id)
    {
        MTB_SuYuanGouDuiAndZhiJiu ms = bll.GetList(id); 
        ComboBoxZhiJiuID.SelectedValue = ms.ZhiJiuID.ToString().Trim();
        LabelGouDuiID.Text = ms.GouDuiID.ToString().Trim();
        inputPercentValue.Value = ms.PercentValue.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}