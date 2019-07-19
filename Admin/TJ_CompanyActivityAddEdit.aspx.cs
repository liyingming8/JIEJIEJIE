using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_CompanyActivityAddEdit : AuthorPage
{
    private readonly BTJ_CompanyActivity bll = new BTJ_CompanyActivity();
    private readonly MTJ_CompanyActivity mod = new MTJ_CompanyActivity();

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
        mod.CAID = Convert.ToInt32(inputCAID.Value.Trim());
        mod.UserID = Convert.ToInt32(inputUserID.Value.Trim());
        mod.CompID = Convert.ToInt32(inputCompID.Value.Trim());
        mod.ActivityName = inputActivityName.Value.Trim();
        mod.StarDate = Convert.ToDateTime(inputStarDate.Value.Trim());
        mod.EndDate = Convert.ToDateTime(inputEndDate.Value.Trim());
        mod.GoodsIDS = inputGoodsIDS.Value.Trim();
        mod.PlaceIDS = inputPlaceIDS.Value.Trim();
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
        Response.Write("<script>location.href='TJ_CompanyActivity.aspx'</script>");
    }

    private void fillinput(int id)
    {
        MTJ_CompanyActivity ms = bll.GetList(id);
        inputCAID.Value = ms.CAID.ToString().Trim();
        inputUserID.Value = ms.UserID.ToString().Trim();
        inputCompID.Value = ms.CompID.ToString().Trim();
        inputActivityName.Value = ms.ActivityName.Trim();
        inputStarDate.Value = ms.StarDate.ToString().Trim();
        inputEndDate.Value = ms.EndDate.ToString().Trim();
        inputGoodsIDS.Value = ms.GoodsIDS.Trim();
        inputPlaceIDS.Value = ms.PlaceIDS.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}