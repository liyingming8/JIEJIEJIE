using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_VanPassCitysAddEdit : AuthorPage
{
    private readonly BTJ_VanPassCitys bll = new BTJ_VanPassCitys();
    private readonly MTJ_VanPassCitys mod = new MTJ_VanPassCitys();

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
        mod.VanPassID = Convert.ToInt32(inputVanPassID.Value.Trim());
        mod.VanID = Convert.ToInt32(inputVanID.Value.Trim());
        mod.CTID = Convert.ToInt32(inputCTID.Value.Trim());
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
        Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTJ_VanPassCitys ms = bll.GetList(id);
        inputVanPassID.Value = ms.VanPassID.ToString().Trim();
        inputVanID.Value = ms.VanID.ToString().Trim();
        inputCTID.Value = ms.CTID.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}