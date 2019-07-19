using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_SWM_PID_ActiveAddEdit : AuthorPage
{
    BTJ_SWM_PID_Active bll = new BTJ_SWM_PID_Active();
    MTJ_SWM_PID_Active mod = new MTJ_SWM_PID_Active();
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
                Button1.Text = "添加";
                break;
             case "edit":
                Button1.Text = "修改";
                fillinput(int.Parse(HF_ID.Value.Trim()));
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
        mod.id = Convert.ToInt32(inputid.Value.Trim());
        mod.pid = Convert.ToInt32(inputpid.Value.Trim());
        mod.prodid = Convert.ToInt32(inputprodid.Value.Trim());
        mod.rpidstring = inputrpidstring.Value.Trim();
        mod.isactive = Convert.ToBoolean(inputisactive.Value.Trim());
        mod.number = Convert.ToInt32(inputnumber.Value.Trim());
        mod.pricevl = Convert.ToDecimal(inputpricevl.Value.Trim());
        mod.lastupdate = Convert.ToDateTime(inputlastupdate.Value.Trim());
        mod.updateuserid = Convert.ToInt32(inputupdateuserid.Value.Trim());
        mod.mchbillno = inputmchbillno.Value.Trim();
        mod.remarks = inputremarks.Value.Trim();
        mod.compid = Convert.ToInt32(inputcompid.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_SWM_PID_ActiveAddEdit.aspx","TJ_SWM_PID_Active","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_SWM_PID_ActiveAddEdit.aspx","TJ_SWM_PID_Active","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTJ_SWM_PID_Active ms = bll.GetList(id);
        inputid.Value = ms.id.ToString().Trim();
        inputpid.Value = ms.pid.ToString().Trim();
        inputprodid.Value = ms.prodid.ToString().Trim();
        inputrpidstring.Value = ms.rpidstring.Trim();
        inputisactive.Value = ms.isactive.ToString().Trim();
        inputnumber.Value = ms.number.ToString().Trim();
        inputpricevl.Value = ms.pricevl.ToString().Trim();
        inputlastupdate.Value = ms.lastupdate.ToString().Trim();
        inputupdateuserid.Value = ms.updateuserid.ToString().Trim();
        inputmchbillno.Value = ms.mchbillno.Trim();
        inputremarks.Value = ms.remarks.Trim();
        inputcompid.Value = ms.compid.ToString().Trim();
    }
}