using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_CompanyMobileSiteInfoAddEdit : AuthorPage
{
    private readonly BTJ_CompanyMobileSiteInfo bll = new BTJ_CompanyMobileSiteInfo();
    private MTJ_CompanyMobileSiteInfo mod = new MTJ_CompanyMobileSiteInfo();

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
        mod.CompSiteID = Convert.ToInt32(inputCompSiteID.Value.Trim());
        mod.CompID = Convert.ToInt32(inputCompID.Value.Trim());
        mod.SiteID = Convert.ToInt32(inputSiteID.Value.Trim());
        mod.ShowOrder = Convert.ToInt32(inputShowOrder.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        Response.Write("<script>location.href='TJ_CompanyMobileSiteInfo.aspx'</script>");
    }

    private void fillinput(int id)
    {
        MTJ_CompanyMobileSiteInfo ms = bll.GetList(id);
        inputCompSiteID.Value = ms.CompSiteID.ToString().Trim();
        inputCompID.Value = ms.CompID.ToString().Trim();
        inputSiteID.Value = ms.SiteID.ToString().Trim();
        inputShowOrder.Value = ms.ShowOrder.ToString().Trim();
    }
}