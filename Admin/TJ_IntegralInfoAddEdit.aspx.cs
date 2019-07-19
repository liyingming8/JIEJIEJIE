using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_IntegralInfoAddEdit : AuthorPage
{
    BTJ_IntegralInfo bll = new BTJ_IntegralInfo();
    MTJ_IntegralInfo mod = new MTJ_IntegralInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
       {
           if (!string.IsNullOrEmpty(Request.QueryString["ITGRID"]))
           {
               HF_ITGRID.Value = Sc.DecryptQueryString(Request.QueryString["ITGRID"]);
           }
           else
           {
               Response.End();
           }
          if (!string.IsNullOrEmpty(Request.QueryString["cmd"]))
          {
             HF_CMD.Value = Sc.DecryptQueryString(Request.QueryString["cmd"].Trim());
          }
          if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
          {
             HF_ID.Value = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
          }
           FillDDL();
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
    BTJ_IntegraItems btjIntegraItems = new BTJ_IntegraItems();
    private void FillDDL()
    {
        DDL_IntegralItemID.DataSource = btjIntegraItems.GetListsByFilterString("IsActive=1");
        DDL_IntegralItemID.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
           mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        } 
        mod.ITGRID = Convert.ToInt32(HF_ITGRID.Value); 
        mod.IntegralItemID = Convert.ToInt32(DDL_IntegralItemID.SelectedValue.Trim());
        mod.IntegralReword = Convert.ToInt32(inputIntegralReword.Value.Trim()); 
        mod.Remarks = inputRemarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_IntegralInfoAddEdit.aspx","TJ_IntegralInfo","描述",DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_IntegralInfoAddEdit.aspx","TJ_IntegralInfo","描述",DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void Fillinput(int id)
    {
        MTJ_IntegralInfo ms = bll.GetList(id);
        HF_ITGRID.Value = ms.ITGRID.ToString();
        DDL_IntegralItemID.SelectedValue = ms.IntegralItemID.ToString(); 
        inputIntegralReword.Value = ms.IntegralReword.ToString().Trim(); 
        inputRemarks.Value = ms.Remarks.Trim();
    }
}