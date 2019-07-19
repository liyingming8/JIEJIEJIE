using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TB_SuYuanGongYingShangInfoAddEdit : AuthorPage
{
    readonly CommonFun comfun = new CommonFun();
    readonly BTB_SuYuanGongYingShangInfo bll = new BTB_SuYuanGongYingShangInfo();
    MTB_SuYuanGongYingShangInfo mod = new MTB_SuYuanGongYingShangInfo();
    BTJ_RoleInfo brole = new BTJ_RoleInfo();
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
          FillComBox();
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
        //mod.ID = Convert.ToInt32(inputID.Value.Trim());
        mod.GYSName = inputGYSName.Value.Trim();
        mod.GYSPhoneNum = inputGYSPhoneNum.Value.Trim();
        mod.GYSAddress = inputGYSAddress.Value.Trim(); 
        mod.CtiyID = Convert.ToInt32(ComboBox_CTID.SelectedValue);
        mod.Compid = Convert.ToInt32(GetCookieCompID());
        mod.Remarks = inputRemarks.Value.Trim();
        mod.SpID = ReturnSpidString();
        //mod.Remarks1 = inputRemarks1.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanGongYingShangInfoAddEdit.aspx","TB_SuYuanGongYingShangInfo","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                //RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanGongYingShangInfoAddEdit.aspx","TB_SuYuanGongYingShangInfo","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void Fillinput(int id)
    {
        MTB_SuYuanGongYingShangInfo ms = bll.GetList(id);
        //inputID.Value = ms.ID.ToString().Trim();
        inputGYSName.Value = ms.GYSName.Trim();
        inputGYSPhoneNum.Value = ms.GYSPhoneNum.Trim();
        inputGYSAddress.Value = ms.GYSAddress.Trim(); 
        ComboBox_CTID.SelectedValue = ms.CtiyID.ToString().Trim(); 
        //inputCompid.Value = ms.Compid.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        Makechecklist(ms.SpID);
        //inputRemarks1.Value = ms.Remarks1.ToString().Trim();
    }

    readonly BTJ_BaseClass _btjBase = new BTJ_BaseClass();
    private void FillComBox()
    {
        comfun.BindTreeCombox(ComboBox_CTID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "请选择...", true,"-", "");
        ComboBox_CTID.SelectedValue = "0";
        cbl_spID.DataSource = _btjBase.GetListsByFilterString("ParentID=" + DAConfig.YuanLiangLeiBie);
        cbl_spID.DataBind(); 
    }

    private void Makechecklist(string spidString)
    {
        foreach (ListItem item in cbl_spID.Items)
        {
            if (spidString.Contains("," + item.Value + ","))
            {
                item.Selected = true;
            }
        }
    }

    private string _returnstring = "";
    private string ReturnSpidString()
    {
        _returnstring = "";
        foreach (ListItem item in cbl_spID.Items)
        {
            if (item.Selected)
            {
                if (string.IsNullOrEmpty(_returnstring))
                {
                    _returnstring = "," + item.Value + ",";
                }
                else
                {
                    _returnstring += item.Value+",";
                }
            }
        }
        return _returnstring;
    }
}