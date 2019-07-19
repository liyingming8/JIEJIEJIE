using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_ZJLabelCodeInfoAddEdit : AuthorPage
{
    private readonly BTJ_ZJLabelCodeInfo bll = new BTJ_ZJLabelCodeInfo();
    private MTJ_ZJLabelCodeInfo mod = new MTJ_ZJLabelCodeInfo();

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
        mod.ZJCDID = Convert.ToInt32(inputZJCDID.Value.Trim());
        mod.LabelCode = inputLabelCode.Value.Trim();
        mod.LAID = Convert.ToInt32(inputLAID.Value.Trim());
        mod.JxID = Convert.ToInt32(inputJxID.Value.Trim());
        mod.SaleArea = inputSaleArea.Value.Trim();
        mod.Remarks = inputRemarks.Value.Trim();
        mod.CompID = Convert.ToInt32(inputCompID.Value.Trim());
        mod.DPJXID = Convert.ToInt32(inputDPJXID.Value.Trim());
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
        MTJ_ZJLabelCodeInfo ms = bll.GetList(id);
        inputZJCDID.Value = ms.ZJCDID.ToString().Trim();
        inputLabelCode.Value = ms.LabelCode.Trim();
        inputLAID.Value = ms.LAID.ToString().Trim();
        inputJxID.Value = ms.JxID.ToString().Trim();
        inputSaleArea.Value = ms.SaleArea.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        inputCompID.Value = ms.CompID.ToString().Trim();
        inputDPJXID.Value = ms.DPJXID.ToString().Trim();
    }
}