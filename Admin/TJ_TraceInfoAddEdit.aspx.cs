using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_TraceInfoAddEdit : AuthorPage
{
    private readonly BTJ_TraceInfo bll = new BTJ_TraceInfo();
    private MTJ_TraceInfo mod = new MTJ_TraceInfo();
    private readonly CommonFun comfun = new CommonFun();
    private readonly BTB_Products_Infor bproduct = new BTB_Products_Infor();

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
            FILLDDL();
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
        mod.CID = Convert.ToInt32(ComboBox_CID.SelectedValue);
        mod.ShowOrder = Convert.ToInt32(inputShowOrder.Value.Trim());
        mod.Contents = CKEditorControl_Contents.Text.Trim();
        mod.LogoURL = HF_ImgURL.Value;
        mod.WLProID = Convert.ToInt32(ComboBox_WLProID.SelectedValue);
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
        MTJ_TraceInfo ms = bll.GetList(id);
        ComboBox_WLProID.SelectedValue = ms.WLProID.ToString().Trim();
        ComboBox_CID.SelectedValue = ms.CID.ToString().Trim();
        inputShowOrder.Value = ms.ShowOrder.ToString().Trim();
        CKEditorControl_Contents.Text = ms.Contents.Trim();
        Image_Pic.ImageUrl = ms.LogoURL.Trim();
        HF_ImgURL.Value = ms.LogoURL.Trim();
    }

    private void FILLDDL()
    {
        comfun.BindTreeCombox(ComboBox_CID, "CName", "CID", "ParentID", "TJ_BaseClass", int.Parse(DAConfig.TraceItemID),
            "指定项目...", false, "", "");
        ComboBox_CID.SelectedValue = "0";
        ComboBox_WLProID.DataSource = bproduct.GetListsByFilterString("CompID=" + GetCookieCompID());
        ComboBox_WLProID.DataBind();
        ComboBox_WLProID.SelectedValue = "0";
    }
}