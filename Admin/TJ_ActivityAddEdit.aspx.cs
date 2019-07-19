using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
using TJ.DBUtility;
using System.Data;
public partial class Admin_TJ_ActivityAddEdit : AuthorPage
{
    readonly BTJ_Activity _bll = new BTJ_Activity();
    MTJ_Activity _mod = new MTJ_Activity();
    readonly TabExecute _tab = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
       {
           inputSTM.Value = DateTime.Now.ToString("yyyy-MM-dd");
           inputETM.Value = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
          if (!string.IsNullOrEmpty(Request.QueryString["cmd"]))
          {
             HF_CMD.Value = Sc.DecryptQueryString(Request.QueryString["cmd"].Trim());
          }
          if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
          {
             HF_ID.Value = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
          }
          FILLDDL();
          switch (HF_CMD.Value)
          {
             case "add":
                Button1.Text = "添加";
                btnDelete.Style["display"] = "none";//新增界面隐藏删除按钮
                break;
             case "edit":
                Button1.Text = "修改";
                Fillinput(int.Parse(HF_ID.Value.Trim()));
                break; 
          }
       }
    }

    private void FILLDDL()
    {
        var btjActivityStrategy = new BTJ_Activity_Strategy();
        DDL_ASID.DataSource = btjActivityStrategy.GetListsByFilterString("compid=" + GetCookieCompID());
        DDL_ASID.DataBind();
        DDL_ASID.SelectedValue = "0";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            _mod = _bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        _mod.AName = inputAName.Value.Trim();
        _mod.STM = Convert.ToDateTime(inputSTM.Value.Trim());
        _mod.ETM = Convert.ToDateTime(inputETM.Value.Trim());
        _mod.ObCodeType = GetProdIDString();
        _mod.ASID = Convert.ToInt32(DDL_ASID.SelectedValue.Trim());
        _mod.CompID = Convert.ToInt32(GetCookieCompID());
        _mod.CreateID = Convert.ToInt32(GetCookieUID());
        _mod.Remarks = inputRemarks.Value.Trim();
        _mod.YZM = ckb_yzm.Checked;
        _mod.FaceTo = int.Parse(rbl_faceto.SelectedValue);
        _mod.feedbacktoparent = ckb_to_parent.Checked ? 1 : 0;
        _mod.feedvalue = string.IsNullOrEmpty(feedbackvalue.Value) ? 0 : Convert.ToInt32(feedbackvalue.Value);
        _mod.priority = int.Parse(string.IsNullOrEmpty(input_priority.Value)?"0":input_priority.Value);
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                _mod.CreateDate = DateTime.Now;
                _bll.Insert(_mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_ActivityAddEdit.aspx", "TJ_Activity", "描述", DateTime.Now,int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                _bll.Modify(_mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_ActivityAddEdit.aspx", "TJ_Activity", "描述", DateTime.Now,int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void Fillinput(int id)
    {
        MTJ_Activity ms = _bll.GetList(id);
        inputAName.Value = ms.AName.Trim();
        inputSTM.Value = ms.STM.ToString("yyyy-MM-dd").Trim();
        inputETM.Value = ms.ETM.ToString("yyyy-MM-dd").Trim();
        FillCheckList(ms.ObCodeType.Trim());
        DDL_ASID.SelectedValue = ms.ASID.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        ckb_yzm.Checked = ms.YZM;
        rbl_faceto.SelectedValue = ms.FaceTo.ToString();
        if (ms.feedbacktoparent > 0)
        {
            ckb_to_parent.Checked = true;
        }
        else
        {
            ckb_to_parent.Checked = false;
        }
        feedbackvalue.Value = ms.feedvalue.ToString();
    } 
    private string GetProdIDString()
    {
        return rbl_codetype.SelectedValue;
        ////foreach (ListItem item in rbl_codetype.Items)
        ////{
        ////    if (item.Selected)
        ////    {
        ////        _prodidstring += string.IsNullOrEmpty(_prodidstring) ? item.Value : "," + item.Value;
        ////    }
        ////}
        //return _prodidstring;
    }

    private void FillCheckList(string prodidstring)
    {
        //foreach (ListItem item in CKB_CodeType.Items)
        //{
        //    if (("," + prodidstring + ",").Contains("," + item.Value + ","))
        //    {
        //        item.Selected = true;
        //    }
        //}   
        rbl_codetype.SelectedValue = prodidstring;
    }

    /*
     * 删除
     */
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            var deleteId = Sc.DecryptQueryString(Request.QueryString["ID"].Trim()); 
            string sql = "delete from TJ_Activity where id=" + deleteId;
            DataTable result = _tab.ExecuteNonQuery(sql);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
    }
}