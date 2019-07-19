using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_AcitivityDetailAddEdit : AuthorPage
{
    private readonly BTJ_AcitivityDetail bll = new BTJ_AcitivityDetail();
    private readonly MTJ_AcitivityDetail mod = new MTJ_AcitivityDetail();

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
        mod.ACDTID = Convert.ToInt32(inputACDTID.Value.Trim());
        mod.ITITID = Convert.ToInt32(inputITITID.Value.Trim());
        mod.CAID = Convert.ToInt32(inputCAID.Value.Trim());
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
        Response.Write("<script>location.href='TJ_AcitivityDetail.aspx'</script>");
    }

    private void fillinput(int id)
    {
        MTJ_AcitivityDetail ms = bll.GetList(id);
        inputACDTID.Value = ms.ACDTID.ToString().Trim();
        inputITITID.Value = ms.ITITID.ToString().Trim();
        inputCAID.Value = ms.CAID.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}