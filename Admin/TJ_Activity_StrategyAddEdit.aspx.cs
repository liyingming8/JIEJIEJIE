using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using TJ.DBUtility;
using commonlib;
using System.Data;

public partial class Admin_TJ_Activity_StrategyAddEdit : AuthorPage
{
    BTJ_Activity_Strategy bll = new BTJ_Activity_Strategy();
    MTJ_Activity_Strategy mod = new MTJ_Activity_Strategy();
    readonly TabExecute _tab = new TabExecute();
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
          switch (HF_CMD.Value)
          {
             case "add":
                RBL_Mode.SelectedValue = "1";
                Button1.Text = "添加";
                btnDelete.Style["display"] = "none";//新增界面隐藏删除按钮
                break;
             case "edit":
                Button1.Text = "修改";
                fillinput(int.Parse(HF_ID.Value.Trim()));
                break;
             default:
                break;
          }
          ShowOrHide();
       }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.compid = Convert.ToInt32(GetCookieCompID());
        mod.isactive = CKB_isactive.Checked;
        mod.totalwinrate = Convert.ToInt32(string.IsNullOrEmpty(inputtotalwinrate.Value) ? "0" : inputtotalwinrate.Value.Trim());
        mod.strategyname = inputstrategyname.Value.Trim(); 
        mod.timelimit = Convert.ToInt32(string.IsNullOrEmpty(inputtimelimit.Value) ? "0" : inputtimelimit.Value.Trim());
        mod.toself = RBL_Mode.SelectedValue.Equals("3");
        mod.maxtimeself = Convert.ToInt32(string.IsNullOrEmpty(inputmaxtimeself.Value) ? "0" : inputmaxtimeself.Value.Trim());
        mod.topackage = RBL_Mode.SelectedValue.Equals("2"); 
        mod.packagenum = Convert.ToInt32(string.IsNullOrEmpty(inputpackagenum.Value)?"0":inputpackagenum.Value.Trim());
        mod.remarks = inputremarks.Value.Trim();
        if (mod.topackage)
        {
            mod.maxtimeself = 0;
            mod.totalwinrate = 0;
        }
        if (mod.toself)
        {
            mod.packagenum = 0;
            mod.totalwinrate = 0;
        }
        if (RBL_Mode.SelectedValue.Equals("1"))
        {
            mod.maxtimeself = 0;
            mod.packagenum = 0;
        } 
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.createdate = DateTime.Now;
                mod.createuserid = Convert.ToInt32(GetCookieUID());
                bll.Insert(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_Activity_StrategyAddEdit.aspx", "TJ_Activity_Strategy", "描述",DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                bll.Modify(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_Activity_StrategyAddEdit.aspx", "TJ_Activity_Strategy", "描述",DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void ShowOrHide()
    {
        switch (RBL_Mode.SelectedValue)
        {
            case "1":
                rowzongtizhongjiang.Visible = true;
                rowtopackage.Visible = false;
                rowtoself.Visible = false;
                break;
            case "2":
                rowzongtizhongjiang.Visible = false;
                rowtopackage.Visible = true;
                rowtoself.Visible = false;
                break;
            case "3":
                rowzongtizhongjiang.Visible = false;
                rowtopackage.Visible = false;
                rowtoself.Visible = true;
                break;
        }
    }

    private void fillinput(int id)
    {
        MTJ_Activity_Strategy ms = bll.GetList(id); 
        CKB_isactive.Checked = ms.isactive;
        inputstrategyname.Value = ms.strategyname.Trim();
        inputtotalwinrate.Value = ms.totalwinrate.ToString().Trim();
        inputtimelimit.Value = ms.timelimit.ToString().Trim();
        if (ms.totalwinrate > 0)
        {
            RBL_Mode.SelectedValue = "1";
        }
        else
        {
            if (ms.topackage)
            {
                RBL_Mode.SelectedValue = "2";
            }
            if (ms.toself)
            {
                RBL_Mode.SelectedValue = "3";
            }
        } 
        inputmaxtimeself.Value = ms.maxtimeself.ToString().Trim(); 
        inputpackagenum.Value = ms.packagenum.ToString().Trim();
        inputremarks.Value = ms.remarks.Trim();
    }
    protected void RBL_Mode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowOrHide();
    }

    /*
    * 删除
    */
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            var deleteId = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
            string sql = "delete from TJ_Activity_Strategy where id=" + deleteId;
            DataTable result = _tab.ExecuteNonQuery(sql);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
    }
}