using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TB_SuYuanChengQuTBAddEdit : AuthorPage
{
    readonly BTB_SuYuanChengQuTB bll = new BTB_SuYuanChengQuTB();
    MTB_SuYuanChengQuTB mod = new MTB_SuYuanChengQuTB();
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
           inputZhiZuoTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
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
        mod.CQPC = inputCQPC.Value.Trim();
        mod.ZhiZuoTime = Convert.ToDateTime(inputZhiZuoTime.Value.Trim());
        mod.ZhiJianYuan = ComboBoxZhiJianYuan.SelectedValue;
        mod.KuFangID = Convert.ToInt32(ComboBoxKuFangID.SelectedValue);
        mod.ZhiQuTypeID = Convert.ToInt32(ComboBoxZhiQuTypeID.SelectedValue);
        mod.Remarks = inputRemarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.CompID = int.Parse(GetCookieCompID()); 
                bll.Insert(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanChengQuTBAddEdit.aspx","TB_SuYuanChengQuTB","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanChengQuTBAddEdit.aspx","TB_SuYuanChengQuTB","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    readonly BTB_SuYuanKuFang _btbSuYuanKuFang = new BTB_SuYuanKuFang();
    readonly BTB_SuYuanZhiJianInfo bsyzjy = new BTB_SuYuanZhiJianInfo();
    readonly CommonFun common = new CommonFun();
    private void FillDdl()
    {
        common.BindTreeCombox(ComboBoxZhiQuTypeID,"CName","CID","ParentID","TJ_BaseClass",int.Parse(DAConfig.DaQuLeiBie),"类别...",false,"-","");
        ComboBoxZhiQuTypeID.SelectedValue = "0";
        ComboBoxKuFangID.DataSource = _btbSuYuanKuFang.GetListsByFilterString("Compid=" + GetCookieCompID() + " and KuFangType in (" + DAConfig.SYCKZH + "," + DAConfig.SYCKCQ + ")");
        ComboBoxKuFangID.DataBind();
        ComboBoxKuFangID.Items.Add(new ListItem("库房...","0"));
        ComboBoxKuFangID.SelectedValue = "0";
        ComboBoxZhiJianYuan.DataSource = bsyzjy.GetListsByFilterString("Compid=" + GetCookieCompID());
        ComboBoxZhiJianYuan.DataBind();
        ComboBoxZhiJianYuan.Items.Add(new ListItem("质检员...","0"));
        ComboBoxZhiJianYuan.SelectedValue = "0"; 
    }


    private void Fillinput(int id)
    {
        MTB_SuYuanChengQuTB ms = bll.GetList(id); 
        inputCQPC.Value = ms.CQPC.Trim();
        inputZhiZuoTime.Value = ms.ZhiZuoTime.ToString("yyyy-MM-dd").Trim();
        ComboBoxZhiJianYuan.SelectedValue = ms.ZhiJianYuan.Trim();
        ComboBoxKuFangID.SelectedValue = ms.KuFangID.ToString().Trim();
        ComboBoxZhiQuTypeID.SelectedValue = ms.ZhiQuTypeID.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
    protected void ButtonGenerate_Click(object sender, EventArgs e)
    {
        inputCQPC.Value = "CQ-" + DateTime.Now.ToString("yyyyMM") + "-" + GetIndexCouter();
    }

    private string GetIndexCouter()
    {
        return (10000 + (bll.GetListsByFilterString("convert(nvarchar(7),ZhiZuoTime,120)='" + DateTime.Now.ToString("yyyy-MM") + "'").Count + 1)).ToString().Substring(1);
    }
}