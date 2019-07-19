using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_DjManageAddEdit : AuthorPage
{
    private readonly BTJ_DjManage bll = new BTJ_DjManage();
    private MTJ_DjManage mod = new MTJ_DjManage();

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
        //mod.ZjID = Convert.ToInt32(inputZjID.Value.Trim());
        mod.ZjNumber = inputZjNumber.Value.Trim();
        mod.ZjTime = Convert.ToDateTime(inputZjTime.Text.Trim());
        mod.JxName = inputJxName.Value.Trim();
        mod.ZjPhone = inputZjPhone.Value.Trim();
        mod.CpXS = inputCpXS.Value.Trim();
        mod.CpCX = inputCpCX.Value.Trim();
        mod.DjdName = inputDjdName.Value.Trim();
        mod.DhTime = Convert.ToDateTime(inputDhTime.Text.Trim());
        mod.DjFlag = inputDjFlag.Value.Trim();
        mod.LjTime = Convert.ToDateTime(inputLjTime.Text.Trim());
        mod.ZjyzCode = inputZjyzCode.Value.Trim();
        mod.DelFlag = inputDelFlag.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        Response.Write("<script>window.location.href ='TJ_DjManage.aspx';</script>");
    }

    private void fillinput(int id)
    {
        MTJ_DjManage ms = bll.GetList(id);
        //inputZjID.Value = ms.ZjID.ToString().Trim();
        inputZjNumber.Value = ms.ZjNumber.Trim();
        inputZjTime.Text = ms.ZjTime.ToString().Trim();
        inputJxName.Value = ms.JxName.Trim();
        inputZjPhone.Value = ms.ZjPhone.Trim();
        inputCpXS.Value = ms.CpXS.Trim();
        inputCpCX.Value = ms.CpCX.Trim();
        inputDjdName.Value = ms.DjdName.Trim();
        inputDhTime.Text = ms.DhTime.ToString().Trim();
        inputDjFlag.Value = ms.DjFlag.Trim();
        inputLjTime.Text = ms.LjTime.ToString().Trim();
        inputZjyzCode.Value = ms.ZjyzCode.Trim();
        //inputDelFlag.Value = ms.DelFlag.ToString().Trim();
    }
}