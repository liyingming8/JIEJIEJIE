using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TB_SuYuanZhiQuAndChengQuAddEdit : AuthorPage
{
    readonly BTB_SuYuanZhiQuAndChengQu bll = new BTB_SuYuanZhiQuAndChengQu();
    MTB_SuYuanZhiQuAndChengQu mod = new MTB_SuYuanZhiQuAndChengQu();
    readonly BTB_SuYuanChengQuTB btbSuYuanChengQu = new BTB_SuYuanChengQuTB();
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
          if (!string.IsNullOrEmpty(Request.QueryString["CQID"]))
          {
              HF_CQID.Value = Sc.DecryptQueryString(Request.QueryString["CQID"].Trim());
              labelCQID.Text = btbSuYuanChengQu.GetList(int.Parse(HF_CQID.Value)).CQPC;
          }
          //else
          //{
          //    Response.End();
          //}
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
        mod.QLID = Convert.ToInt32(ComboBoxQLID.SelectedValue);
        mod.CQID = Convert.ToInt32(HF_CQID.Value);
        mod.PercentValue = Convert.ToDecimal(inputPercentValue.Value.Trim());
        mod.Remarks = inputRemarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanZhiQuAndChengQuAddEdit.aspx","TB_SuYuanZhiQuAndChengQu","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanZhiQuAndChengQuAddEdit.aspx","TB_SuYuanZhiQuAndChengQu","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void Fillinput(int id)
    {
        MTB_SuYuanZhiQuAndChengQu ms = bll.GetList(id);
        ComboBoxQLID.SelectedValue = ms.QLID.ToString().Trim();
        labelCQID.Text = btbSuYuanChengQu.GetList(ms.CQID).CQPC; 
        inputPercentValue.Value = ms.PercentValue.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }

    readonly BTB_SuYuanZhiQu btbSuYuanZhiQu = new BTB_SuYuanZhiQu();
    private void FillDdl()
    {
        ComboBoxQLID.DataSource = btbSuYuanZhiQu.GetListsByFilterString("Compid=" + GetCookieCompID());
        ComboBoxQLID.DataBind();
        ComboBoxQLID.Items.Add(new ListItem("原曲批次...","0"));
        ComboBoxQLID.SelectedValue = "0";
    }
}