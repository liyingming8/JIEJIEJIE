using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_GoodsImageInfoAddEdit : AuthorPage
{
    private readonly BTJ_GoodsImageInfo bll = new BTJ_GoodsImageInfo();
    private MTJ_GoodsImageInfo mod = new MTJ_GoodsImageInfo();
    private readonly BTJ_GoodsInfo bgoods = new BTJ_GoodsInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["GoodsID"] != null && Request.QueryString["GoodsID"].Length > 0)
            {
                HF_GoodsID.Value = Request.QueryString["GoodsID"].Trim();
            }

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
        mod.GoodsID = Convert.ToInt32(HF_GoodsID.Value);
        mod.Show = CheckBox_Show.Checked;
        mod.ImagePathString = HF_ImageURL.Value;
        if (HF_ImageURL.Value.Contains("/"))
        {
            mod.SMImagePathString = HF_ImageURL.Value.Trim()
                .Insert(HF_ImageURL.Value.Trim().LastIndexOf('/') + 1, "sm_");
        }
        mod.Remarks = "";
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
        MTJ_GoodsImageInfo ms = bll.GetList(id);
        Label_GoodsName.Text = bgoods.GetList(ms.GoodsID).GoodsName;
        CheckBox_Show.Checked = ms.Show;
        Image_GoodPic.ImageUrl = ms.ImagePathString.Trim();
        HF_ImageURL.Value = ms.ImagePathString.Trim();
        HF_GoodsID.Value = ms.GoodsID.ToString();
    }
}