using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_wuliu_suyan_TB_WorkLineInfoAddEdit : AuthorPage
{
    private readonly BTB_WorkLineInfo bll = new BTB_WorkLineInfo();
    private MTB_WorkLineInfo mod = new MTB_WorkLineInfo();

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
        mod.WLID = Convert.ToInt32(inputWLID.Value.Trim());
        mod.WSID = Convert.ToInt32(inputWSID.Value.Trim());
        mod.WorkLineName = inputWorkLineName.Value.Trim();
        mod.CompID = Convert.ToInt32(inputCompID.Value.Trim());
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
        ClientScript.RegisterStartupScript(GetType(), "reload", "window.opener.location.reload();window.close();", true);
    }

    private void fillinput(int id)
    {
        MTB_WorkLineInfo ms = bll.GetList(id);
        inputWLID.Value = ms.WLID.ToString().Trim();
        inputWSID.Value = ms.WSID.ToString().Trim();
        inputWorkLineName.Value = ms.WorkLineName.Trim();
        inputCompID.Value = ms.CompID.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}