using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TB_SuYuanZhiJiuZhongJianTBAddEdit : AuthorPage
{
    readonly BTB_SuYuanZhiJiuZhongJianTB _bll = new BTB_SuYuanZhiJiuZhongJianTB();
    MTB_SuYuanZhiJiuZhongJianTB _mod = new MTB_SuYuanZhiJiuZhongJianTB();
    readonly BTB_SuYuanZhiJiu _btbSuYuanZhiJiu = new BTB_SuYuanZhiJiu();
    readonly BTB_SuYuanChengQuTB _btbSuYuanChengQu = new BTB_SuYuanChengQuTB();
    readonly BTB_SuYuanGrain _btbSuYuanGrain = new BTB_SuYuanGrain();
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
          if (!string.IsNullOrEmpty(Request.QueryString["ZJID"]))
          {
              HF_ZJID.Value = Sc.DecryptQueryString(Request.QueryString["ZJID"].Trim());
              labelzjid.Text = _btbSuYuanZhiJiu.GetList(int.Parse(HF_ZJID.Value)).ZJPC;
          } 
          else
          {
              Response.End();
          }
           if (!string.IsNullOrEmpty(Request.QueryString["YL"]))
           {
               HF_YL.Value = Request.QueryString["YL"]; 
               if (HF_YL.Value.Trim().Equals("1"))
               {
                   divchengqu.Visible = false;
                   divyuanliang.Visible = true;
                   //literalValue.Text = "原粮";
               }
               else
               {
                   divchengqu.Visible = true;
                   divyuanliang.Visible = false;
                   //literalValue.Text = "成曲";
               }
           }
          FillDdl();
          inputMergeTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
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
        _mod.CQID = Convert.ToInt32(ComboBoxCQID.SelectedValue);
        _mod.YLID = Convert.ToInt32(ComboBoxYLID.SelectedValue);
        _mod.PercertValue = Convert.ToDecimal(inputPercertValue.Value.Trim());
        _mod.Remarks = inputRemarks.Value.Trim();
        _mod.ZJID = int.Parse(HF_ZJID.Value);
        _mod.MergeDate = Convert.ToDateTime(inputMergeTime.Value);
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                _bll.Insert(_mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanZhiJiuZhongJianTBAddEdit.aspx","TB_SuYuanZhiJiuZhongJianTB","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                _bll.Modify(_mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanZhiJiuZhongJianTBAddEdit.aspx","TB_SuYuanZhiJiuZhongJianTB","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void FillDdl()
    { 
        ComboBoxCQID.DataSource = _btbSuYuanChengQu.GetListsByFilterString("Compid=" + GetCookieCompID());
        ComboBoxCQID.DataBind();
        ComboBoxCQID.Items.Add(new ListItem("成曲批次...","0"));
        ComboBoxCQID.SelectedValue = "0";
        ComboBoxYLID.DataSource = _btbSuYuanGrain.GetListsByFilterString("Compid=" + GetCookieCompID());
        ComboBoxYLID.DataBind();
        ComboBoxYLID.Items.Add(new ListItem("原粮入库批次...","0"));
        ComboBoxYLID.SelectedValue = "0";
    }

    private void Fillinput(int id)
    {
        MTB_SuYuanZhiJiuZhongJianTB ms = _bll.GetList(id); 
        
        if (HF_YL.Value.Trim().Equals("1"))
        {
            ComboBoxYLID.SelectedValue = ms.YLID.ToString().Trim();
            ComboBoxCQID.SelectedValue = "0";
        }
        else
        {
            ComboBoxCQID.SelectedValue = ms.CQID.ToString().Trim();
            ComboBoxYLID.SelectedValue = "0";
        }
        inputPercertValue.Value = ms.PercertValue.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        inputMergeTime.Value = ms.MergeDate.ToString("yyyy-MM-dd");

    }
}