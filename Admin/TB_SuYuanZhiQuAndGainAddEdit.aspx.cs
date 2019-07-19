using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TB_SuYuanZhiQuAndGainAddEdit : AuthorPage
{
    readonly BTB_SuYuanZhiQuAndGain bll = new BTB_SuYuanZhiQuAndGain();
    MTB_SuYuanZhiQuAndGain mod = new MTB_SuYuanZhiQuAndGain();
    readonly BTB_SuYuanZhiQu bzhiQu = new BTB_SuYuanZhiQu();
    MTB_SuYuanZhiQu mzhiqu = new MTB_SuYuanZhiQu();
    readonly BTB_SuYuanGrain bsyGrain = new BTB_SuYuanGrain();
    readonly CommonFun comm = new CommonFun();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ZQID"]))
            {
                HF_ZQID.Value = Sc.DecryptQueryString(Request.QueryString["ZQID"]);
                mzhiqu = bzhiQu.GetList(int.Parse(HF_ZQID.Value));
                labelzhiqupic.Text = mzhiqu.ZhiQuPC;
                HF_ZQPC.Value = mzhiqu.ZhiQuPC;
            }
            else
            {
                Response.End();
                return;
            }
            FillDdl();
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
        mod.YLID = Convert.ToInt32(ComboBoxYLID.SelectedValue);
        mod.YLPC = Convert.ToInt32(ComboBoxYLPC.SelectedValue);
        mod.ZQID = Convert.ToInt32(HF_ZQID.Value); 
        mod.PercentValue = Convert.ToDecimal(inputPercentValue.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanZhiQuAndGainAddEdit.aspx","TB_SuYuanZhiQuAndGain","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanZhiQuAndGainAddEdit.aspx","TB_SuYuanZhiQuAndGain","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void Fillinput(int id)
    {
        MTB_SuYuanZhiQuAndGain ms = bll.GetList(id);
        ComboBoxYLID.SelectedValue = ms.YLID.ToString().Trim();
        FillYlpc();
        HF_ZQID.Value = ms.ZQID.ToString().Trim();
        ComboBoxYLPC.SelectedValue = ms.YLPC.ToString().Trim();
        inputPercentValue.Value = ms.PercentValue.ToString().Trim();
    }

    private void FillDdl()
    {
        comm.BindTreeCombox(ComboBoxYLID,"CName","CID","ParentID","TJ_BaseClass",int.Parse(DAConfig.YuanLiangLeiBie),"类别...",true,"-","");
        ComboBoxYLID.SelectedValue = "0"; 
    }
    protected void ComboBoxYLID_ComboBoxChanged(object sender, EventArgs e)
    {
        FillYlpc();
    }

    private void FillYlpc()
    {
        ComboBoxYLPC.DataSource = null;
        ComboBoxYLPC.DataBind();
        if (!ComboBoxYLID.SelectedValue.Equals("0"))
        {
            ComboBoxYLPC.DataSource = bsyGrain.GetListsByFilterString("CID=" + ComboBoxYLID.SelectedValue);
            ComboBoxYLPC.DataBind();
            ComboBoxYLPC.Items.Add(new ListItem("入库批次...", "0"));
            ComboBoxYLPC.SelectedValue = "0";
        } 
    }
}