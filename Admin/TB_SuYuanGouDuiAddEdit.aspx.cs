using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls; 
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TB_SuYuanGouDuiAddEdit : AuthorPage
{
    readonly BTB_SuYuanGouDui _bll = new BTB_SuYuanGouDui();
    MTB_SuYuanGouDui _mod = new MTB_SuYuanGouDui();
    readonly BTB_SuYuanZhiQuAndGouDui _btbSuYuanZhiQuAndGouDui = new BTB_SuYuanZhiQuAndGouDui();
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
            inputGouDuiTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
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
            _mod = _bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        _mod.GDPC = inputGDPC.Value.Trim();
        _mod.GouDuiTime = Convert.ToDateTime(inputGouDuiTime.Value.Trim());
        _mod.ZhiJianID = Convert.ToInt32(ComboBoxZhiJianID.SelectedValue);
        //_mod.ProID = Convert.ToInt32(ComboBoxProID.SelectedValue);
        _mod.DaYangJiuMingCheng = inputDaYangJiuMingCheng.Value.Trim();
        _mod.Compid = Convert.ToInt32(GetCookieCompID());
        _mod.Remarks = inputRemarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                object id= _bll.Insert(_mod);
                HF_ID.Value = id.ToString();
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanGouDuiAddEdit.aspx","TB_SuYuanGouDui","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                _bll.Modify(_mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanGouDuiAddEdit.aspx","TB_SuYuanGouDui","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
        //RecordGouDuiAndZhiQu(int.Parse(HF_ID.Value), int.Parse(ComboBoxZQPC.SelectedValue), 100); 
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void FillDdl()
    {
        //ComboBoxProID.DataSource = _bproInfor.GetListsByFilterString("CompID=" + GetCookieCompID());
        //ComboBoxProID.DataBind(); 
        //ComboBoxProID.Items.Add(new ListItem("请指定产品...","0"));
        //ComboBoxProID.SelectedValue = "0";
        ComboBoxZhiJianID.DataSource = _btbSuYuanZhiJian.GetListsByFilterString("Compid=" + GetCookieCompID());
        ComboBoxZhiJianID.DataBind();
        ComboBoxZhiJianID.Items.Add(new ListItem("质检员...", "0"));
        ComboBoxZhiJianID.SelectedValue = "0"; 
    }

    private void Fillinput(int id)
    {
        MTB_SuYuanGouDui ms = _bll.GetList(id); 
        inputGDPC.Value = ms.GDPC.Trim();
        inputGouDuiTime.Value = ms.GouDuiTime.ToString("yyyy-MM-dd").Trim();
        ComboBoxZhiJianID.SelectedValue = ms.ZhiJianID.ToString().Trim();
        inputDaYangJiuMingCheng.Value = ms.DaYangJiuMingCheng;
        //ComboBoxProID.SelectedValue = ms.ProID.ToString().Trim(); 
        inputRemarks.Value = ms.Remarks.Trim(); 
    }
    protected void ButtonShengCheng_Click(object sender, EventArgs e)
    {
        inputGDPC.Value = "GT-" + DateTime.Now.ToString("yyyyMM") + "-" + GetIndexCouter();
    }

    private string GetZqid(int gdid)
    {
        _zqgdlist = _btbSuYuanZhiQuAndGouDui.GetListsByFilterString("GDID=" + gdid);
        if (_zqgdlist.Count > 0)
        {
           return  _zqgdlist[0].ZQID.ToString();
        }
        return "0";
    }

    readonly BTB_SuYuanZhiJianInfo _btbSuYuanZhiJian = new BTB_SuYuanZhiJianInfo(); 
    private string GetIndexCouter()
    {
        return (10000 + (_bll.GetListsByFilterString("Compid="+GetCookieCompID()+" and convert(nvarchar(7),GouDuiTime,120)='" + DateTime.Now.ToString("yyyy-MM") + "'").Count + 1)).ToString().Substring(1);
    }

    private IList<MTB_SuYuanZhiQuAndGouDui> _zqgdlist;
}