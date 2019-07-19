using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TB_SuYuanGrainAddEdit : AuthorPage
{
    readonly BTB_SuYuanGrain bll = new BTB_SuYuanGrain();
    MTB_SuYuanGrain mod = new MTB_SuYuanGrain();
    readonly CommonFun comm = new CommonFun();
    readonly BTB_SuYuanJianCeJiGou btbSuYuanJianCeJiGou = new BTB_SuYuanJianCeJiGou();
    readonly BTB_SuYuanKuFang btbSuYuankf = new BTB_SuYuanKuFang();
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
            inputRuKuTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
            FillDdl();
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
        mod.CID = Convert.ToInt32(ComboBoxCID.SelectedValue);
        mod.GainName = ComboBoxCID.Text.Trim();
        mod.Count = Convert.ToInt32(inputCount.Value.Trim());
        mod.UNCID = Convert.ToInt32(ComboBoxUNCID.SelectedValue);
        mod.PiCI = inputPiCI.Value.Trim();
        mod.RuKuTime = Convert.ToDateTime(inputRuKuTime.Value.Trim());
        mod.GYSID = Convert.ToInt32(ComboBoxGYSID.SelectedValue);
        mod.ZhiJianID = Convert.ToInt32(ComboBoxZhiJianID.SelectedValue);
        mod.Imageurl = HF_LogoImage.Value;
        mod.Imageurl_small = HF_LogoImage.Value.Insert(HF_LogoImage.Value.LastIndexOf("/", StringComparison.Ordinal)+1, "sm_");
        mod.JCDWID = Convert.ToInt32(ComboBoxJCJG.SelectedValue);
        mod.Compid = Convert.ToInt32(GetCookieCompID());
        mod.Remarks = inputRemarks.Value.Trim();
        mod.KFID = Convert.ToInt32(ComboBoxKuFang.SelectedValue);
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanGrainAddEdit.aspx","TB_SuYuanGrain","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanGrainAddEdit.aspx","TB_SuYuanGrain","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    readonly BTB_SuYuanGongYingShangInfo _btbSuYuanGongYing = new BTB_SuYuanGongYingShangInfo();
    readonly BTB_SuYuanZhiJianInfo _btbSuYuanZhiJian = new BTB_SuYuanZhiJianInfo();
    private void FillDdl()
    {
        comm.BindTreeCombox(ComboBoxCID,"CName","CID","ParentID","TJ_BaseClass",int.Parse(DAConfig.YuanLiangLeiBie),"原粮类型...",true,"-","");
        ComboBoxCID.SelectedValue = "0";
        comm.BindTreeCombox(ComboBoxUNCID,"CName","CID","ParentID","TJ_BaseClass",int.Parse(DAConfig.YuanLiangDanWei),"单位...",true,"-","");
        ComboBoxUNCID.SelectedValue = "0";
        ComboBoxGYSID.DataSource = _btbSuYuanGongYing.GetListsByFilterString("CompID=" + GetCookieCompID());
        ComboBoxGYSID.DataBind(); 
        ComboBoxGYSID.Items.Add(new ListItem("供应商...","0"));
        ComboBoxGYSID.SelectedValue = "0";
        ComboBoxZhiJianID.DataSource = _btbSuYuanZhiJian.GetListsByFilterString("Compid=" + GetCookieCompID());
        ComboBoxZhiJianID.DataBind();
        ComboBoxZhiJianID.Items.Add(new ListItem("质检员...","0"));
        ComboBoxZhiJianID.SelectedValue = "0";
        ComboBoxJCJG.DataSource = btbSuYuanJianCeJiGou.GetListsByFilterString("CompID=" + GetCookieCompID(), "JGName");
        ComboBoxJCJG.DataBind();
        ComboBoxJCJG.Items.Add(new ListItem("检测机构...","0"));
        ComboBoxJCJG.SelectedValue = "0";
        ComboBoxKuFang.DataSource = btbSuYuankf.GetListsByFilterString("Compid=" + GetCookieCompID() + " and KuFangType in ("+DAConfig.SYCKZH+","+DAConfig.SYCKYL+")");
        ComboBoxKuFang.DataBind();
        ComboBoxKuFang.Items.Add(new ListItem("库房...","0"));
        ComboBoxKuFang.SelectedValue = "0";
    }

    private void Fillinput(int id)
    {
        MTB_SuYuanGrain ms = bll.GetList(id);
        ComboBoxCID.SelectedValue = ms.CID.ToString(CultureInfo.InvariantCulture).Trim(); 
        inputCount.Value = ms.Count.ToString(CultureInfo.InvariantCulture).Trim();
        ComboBoxUNCID.SelectedValue = ms.UNCID.ToString(CultureInfo.InvariantCulture).Trim();  
        inputPiCI.Value = ms.PiCI.Trim();
        inputRuKuTime.Value = ms.RuKuTime.ToString("yyyy-MM-dd").Trim();
        ComboBoxGYSID.SelectedValue = ms.GYSID.ToString().Trim();
        ComboBoxZhiJianID.SelectedValue = ms.ZhiJianID.ToString(CultureInfo.InvariantCulture).Trim();
        HF_LogoImage.Value = ms.Imageurl;
        Image_Logo.ImageUrl = ms.Imageurl;
        ComboBoxJCJG.SelectedValue = ms.JCDWID.ToString(CultureInfo.InvariantCulture).Trim();  
        inputRemarks.Value = ms.Remarks.Trim();
        ComboBoxKuFang.SelectedValue = ms.KFID.ToString();
    }
    protected void ButtonShengCheng_Click(object sender, EventArgs e)
    {
        inputPiCI.Value = "YL-" + DateTime.Now.ToString("yyyyMM" + "-" + ComboBoxGYSID.SelectedValue + "-" + ComboBoxCID.SelectedValue +"-"+ GetIndexCouter());
    }

    private string GetIndexCouter()
    {
        return (10000 + (bll.GetListsByFilterString("Compid="+GetCookieCompID()+" and convert(nvarchar(7),RuKuTime,120)='" + DateTime.Now.ToString("yyyy-MM") + "'").Count + 1)).ToString().Substring(1);
    }
}