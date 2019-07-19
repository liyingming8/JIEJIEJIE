using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TB_SuYuanShengChanPCAddEdit : AuthorPage
{
    readonly BTB_SuYuanShengChanPC bll = new BTB_SuYuanShengChanPC();
    MTB_SuYuanShengChanPC mod = new MTB_SuYuanShengChanPC();
    readonly BTB_SuYuanGouDui bsygd = new BTB_SuYuanGouDui();
    readonly CommonFun common = new CommonFun();

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
            inputSCTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
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

    private string tempstring = "";
   private object RID; 
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.SCPC = inputSCPC.Value.Trim();
        mod.GDPC = ComboBoxGDID.Text.Trim();
        mod.GDID = Convert.ToInt32(ComboBoxGDID.SelectedValue.Trim());
        mod.SCTime = Convert.ToDateTime(inputSCTime.Value.Trim());
        mod.Compid = Convert.ToInt32(GetCookieCompID());
        mod.Remarks = inputRemarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                RID = bll.Insert(mod);
                HF_ID.Value = RID.ToString();
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanShengChanPCAddEdit.aspx","TB_SuYuanShengChanPC","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanShengChanPCAddEdit.aspx","TB_SuYuanShengChanPC","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
        tempstring = ReturnTraceInfo(mod.SCPC);
        if (tempstring.StartsWith("E:"))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(),"alert","alert('"+tempstring.Substring(2)+"');",true);
        }
        else
        {
            mod = bll.GetList(int.Parse(HF_ID.Value));
            mod.SYJSONString = tempstring;
            bll.Modify(mod);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        } 
    }

    private void FillDdl()
    {
        ComboBoxGDID.DataSource = bsygd.GetListsByFilterString("1=1", "ID desc");
        ComboBoxGDID.DataBind();
        ComboBoxGDID.Items.Add(new ListItem("勾兑批次...","0"));
        ComboBoxGDID.SelectedValue = "0";
    }
   
    private void Fillinput(int id)
    {
        MTB_SuYuanShengChanPC ms = bll.GetList(id); 
        inputSCPC.Value = ms.SCPC.Trim();
        ComboBoxGDID.SelectedValue = ms.GDID.ToString().Trim(); 
        inputSCTime.Value = ms.SCTime.ToString("yyyy-MM-dd").Trim(); 
        inputRemarks.Value = ms.Remarks.Trim();
    }

    private string ReturnTraceInfo(string scpc)
    {
        return common.GetTraceSourceInfo(scpc);
    }
}