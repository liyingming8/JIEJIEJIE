using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_ExceptionQueryInfoAddEdit : AuthorPage
{
    BTJ_ExceptionQueryInfo bll = new BTJ_ExceptionQueryInfo();
    MTJ_ExceptionQueryInfo mod = new MTJ_ExceptionQueryInfo();
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
                Fillinput(int.Parse(HF_ID.Value.Trim()));
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
        mod.issolved = checkissolved.Checked; 
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_ExceptionQueryInfoAddEdit.aspx","TJ_ExceptionQueryInfo","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_ExceptionQueryInfoAddEdit.aspx","TJ_ExceptionQueryInfo","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void Fillinput(int id)
    {
        MTJ_ExceptionQueryInfo ms = bll.GetList(id); 
        inputlbcode.InnerText = ms.lbcode.Trim();
        inputquerytm.InnerText = ms.querytm.ToString().Trim();
        inputextype.InnerText = ms.extype.Trim();
        inputrestype.InnerText = ms.restype.ToString().Trim().Equals("1") ? "放行" : "第二天再试";
        checkissolved.Checked = ms.issolved;
        inputplatform.InnerText = ms.platform.Trim();
        inputqueryaddress.InnerText = ms.queryaddress.Trim();
    }
}