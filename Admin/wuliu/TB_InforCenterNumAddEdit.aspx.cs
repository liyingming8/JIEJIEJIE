using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TB_InforCenterNumAddEdit : AuthorPage
{
    private readonly BTB_InforCenterNum bll = new BTB_InforCenterNum();
    private MTB_InforCenterNum mod = new MTB_InforCenterNum();

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
        mod.ID = Convert.ToInt32(inputID.Value.Trim());
        mod.Province = inputProvince.Value.Trim();
        mod.City = inputCity.Value.Trim();
        mod.Server = inputServer.Value.Trim();
        mod.CenterNumber = inputCenterNumber.Value.Trim();
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
        MTB_InforCenterNum ms = bll.GetList(id);
        inputID.Value = ms.ID.ToString().Trim();
        inputProvince.Value = ms.Province.Trim();
        inputCity.Value = ms.City.Trim();
        inputServer.Value = ms.Server.Trim();
        inputCenterNumber.Value = ms.CenterNumber.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}