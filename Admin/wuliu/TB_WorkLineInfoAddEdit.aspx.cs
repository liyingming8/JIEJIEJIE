using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TB_WorkLineInfoAddEdit : AuthorPage
{
    private readonly BTB_WorkLineInfo bll = new BTB_WorkLineInfo();
    private MTB_WorkLineInfo mod = new MTB_WorkLineInfo();
    private readonly BTB_WorkShopInfo bworkshop = new BTB_WorkShopInfo();

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
            FillDDL();
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
        mod.WSID = Convert.ToInt32(DDL_WorkShop.SelectedValue);
        mod.WorkLineName = inputWorkLineName.Value.Trim();
        mod.Remarks = inputRemarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.CompID = int.Parse(GetCookieCompID());
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        Response.Write("<script>alert('操作成功！');</script>");
    }

    private void FillDDL()
    {
        DDL_WorkShop.DataSource = bworkshop.GetListsByFilterString("CompID=" + GetCookieCompID());
        DDL_WorkShop.DataBind();
    }

    private void fillinput(int id)
    {
        MTB_WorkLineInfo ms = bll.GetList(id);
        DDL_WorkShop.SelectedValue = ms.WSID.ToString().Trim();
        inputWorkLineName.Value = ms.WorkLineName.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}