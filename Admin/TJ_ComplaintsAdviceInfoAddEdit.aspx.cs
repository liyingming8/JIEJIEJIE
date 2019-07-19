using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_ComplaintsAdviceInfoAddEdit : AuthorPage
{
    private readonly BTJ_ComplaintsAdviceInfo bll = new BTJ_ComplaintsAdviceInfo();
    private readonly MTJ_ComplaintsAdviceInfo mod = new MTJ_ComplaintsAdviceInfo();

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
        mod.CID = Convert.ToInt32(inputCID.Value.Trim());
        mod.PlaceID = Convert.ToInt32(inputPlaceID.Value.Trim());
        mod.ComplainsAdviceContents = inputComplainsAdviceContents.Value.Trim();
        mod.PublishDate = Convert.ToDateTime(inputPublishDate.Value.Trim());
        mod.MobilePhone = inputMobilePhone.Value.Trim();
        mod.Name = inputName.Value.Trim();
        mod.CompID = int.Parse(GetCookieCompID());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        Response.Write("<script>location.href='TJ_ComplaintsAdviceInfo.aspx'</script>");
    }

    private void fillinput(int id)
    {
        MTJ_ComplaintsAdviceInfo ms = bll.GetList(id);
        inputCAID.Value = ms.CAID.ToString().Trim();
        inputCID.Value = ms.CID.ToString().Trim();
        inputPlaceID.Value = ms.PlaceID.ToString().Trim();
        inputComplainsAdviceContents.Value = ms.ComplainsAdviceContents.Trim();
        inputPublishDate.Value = ms.PublishDate.ToString().Trim();
        inputMobilePhone.Value = ms.MobilePhone.Trim();
        inputName.Value = ms.Name.Trim();
    }
}