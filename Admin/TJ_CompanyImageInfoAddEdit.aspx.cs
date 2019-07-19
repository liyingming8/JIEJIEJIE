using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_CompanyImageInfoAddEdit : AuthorPage
{
    private readonly BTJ_CompanyImageInfo bll = new BTJ_CompanyImageInfo();
    private MTJ_CompanyImageInfo mod = new MTJ_CompanyImageInfo();
    private readonly BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();

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
            if (Request.QueryString["CPID"] != null && Request.QueryString["CPID"].Trim().Length > 0)
            {
                HF_CPID.Value = Request.QueryString["CPID"].Trim();
                Label_CompanyName.Text = bcompany.GetList(int.Parse(HF_CPID.Value)).CompName;
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
        mod.CompID = Convert.ToInt32(HF_CPID.Value);
        mod.ImageLinkURL = HF_ImageURL.Value.Trim();
        if (HF_ImageURL.Value.Contains("/"))
        {
            mod.SMImageLinkURL = HF_ImageURL.Value.Trim().Insert(HF_ImageURL.Value.Trim().LastIndexOf('/') + 1, "sm_");
        }
        mod.Remarks = inputRemarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        Response.Redirect("TJ_CompanyImageInfo.aspx?CPID=" + HF_CPID.Value, true);
      //  Response.Write("<script>window.location.href='TJ_CompanyImageInfo.aspx?CPID='" + HF_CPID .Value+ "</script>");
    }

    private void fillinput(int id)
    {
        MTJ_CompanyImageInfo ms = bll.GetList(id);
        HF_CPID.Value = ms.CompID.ToString().Trim();
        Image_GoodPic.ImageUrl = ms.ImageLinkURL.Trim();
        HF_ImageURL.Value = ms.ImageLinkURL.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}