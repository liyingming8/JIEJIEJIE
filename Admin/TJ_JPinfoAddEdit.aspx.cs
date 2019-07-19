using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_JPinfoAddEdit : AuthorPage
{
    private readonly BTJ_JPinfo bll = new BTJ_JPinfo();
    private MTJ_JPinfo mod = new MTJ_JPinfo();

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
        //mod.JPID = Convert.ToInt32(inputJPID.Value.Trim());
        mod.JxID = Convert.ToInt32(inputJxID.Value.Trim());
        mod.DjdName = inputDjdName.Value.Trim();
        mod.JpName = inputJpName.Value.Trim();
        mod.PFCount = Convert.ToInt32(inputPFCount.Value.Trim());
        mod.DHCount = Convert.ToInt32(inputDHCount.Value.Trim());
        mod.HXCount = Convert.ToInt32(inputHXCount.Value.Trim());
        mod.SYCount = Convert.ToInt32(inputSYCount.Value.Trim());
        mod.CreatTime = Convert.ToDateTime(inputCreatTime.Text.Trim());
        mod.compID = int.Parse(GetCookieCompID());
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
        Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTJ_JPinfo ms = bll.GetList(id);
        //inputJPID.Value = ms.JPID.ToString().Trim();
        inputJxID.Value = ms.JxID.ToString().Trim();
        inputDjdName.Value = ms.DjdName.Trim();
        inputJpName.Value = ms.JpName.Trim();
        inputPFCount.Value = ms.PFCount.ToString().Trim();
        inputDHCount.Value = ms.DHCount.ToString().Trim();
        inputHXCount.Value = ms.HXCount.ToString().Trim();
        inputSYCount.Value = ms.SYCount.ToString().Trim();
        inputCreatTime.Text = ms.CreatTime.ToString().Trim();
        //inputcompID.Value = ms.compID.ToString().Trim();
        //inputDelFlag.Value = ms.DelFlag.ToString().Trim();
    }
}