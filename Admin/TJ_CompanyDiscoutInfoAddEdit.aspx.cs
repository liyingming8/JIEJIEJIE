using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_CompanyDiscoutInfoAddEdit : AuthorPage
{
    private readonly BTJ_CompanyDiscoutInfo bll = new BTJ_CompanyDiscoutInfo();
    private MTJ_CompanyDiscoutInfo mod = new MTJ_CompanyDiscoutInfo();

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
        mod.CDSCID = Convert.ToInt32(inputCDSCID.Value.Trim());
        mod.CompID = Convert.ToInt32(inputCompID.Value.Trim());
        mod.CID = Convert.ToInt32(inputCID.Value.Trim());
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
        Response.Write("<script>location.href='TJ_CompanyDiscoutInfo.aspx'</script>");
    }

    private void fillinput(int id)
    {
        MTJ_CompanyDiscoutInfo ms = bll.GetList(id);
        inputCDSCID.Value = ms.CDSCID.ToString().Trim();
        inputCompID.Value = ms.CompID.ToString().Trim();
        inputCID.Value = ms.CID.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}