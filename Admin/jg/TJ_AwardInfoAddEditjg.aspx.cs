using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_AwardInfoAddEditjg : AuthorPage
{
    private readonly BTJ_AwardInfo bll = new BTJ_AwardInfo();
    private readonly BTJ_Integral bintegral = new BTJ_Integral();
    private MTJ_AwardInfo mod = new MTJ_AwardInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
            }
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim();
            }
            FillDLL();
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
        if (HF_CMD.Value.ToLower().Trim().Equals("edit"))
        {
            mod = bll.GetList(int.Parse(HF_ID.Value));
        }
        mod.CompID = int.Parse(GetCookieCompID());
        mod.AGID = Convert.ToInt32(ddl_agid.SelectedValue);
        mod.AwardThing = inputAwardThing.Value.Trim();
        mod.ImageURLString = HF_ImageUrl.Value;
        if (HF_ImageUrl.Value.Contains("/"))
        {
            mod.SImageURLString = HF_ImageUrl.Value.Trim().Insert(HF_ImageUrl.Value.Trim().LastIndexOf('/') + 1, "sm_");
        }
        mod.Contents = inputContents.Value.Trim();
        mod.IntegralValue = Convert.ToInt32(txtIntegralValue.Value);
        mod.Remarks = inputRemarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.PublishDate = DateTime.Now;
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "location.href='TJ_AwardInfo.aspx';", true);
    }

    private void fillinput(int id)
    {
        MTJ_AwardInfo ms = bll.GetList(id);
        ddl_agid.SelectedValue = ms.AGID.ToString().Trim();
        inputAwardThing.Value = ms.AwardThing.Trim();
        HF_ImageUrl.Value = ms.ImageURLString;
        Image_AwardUrl.ImageUrl = ms.ImageURLString;
        inputContents.Value = ms.Contents.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }

    private void FillDLL()
    {
        ddl_agid.DataSource =
            bintegral.GetListsByFilterString("CompID=" + GetCookieCompID() + " and EndDate>='" + DateTime.Now + "'");
        ddl_agid.DataBind();
    }
}