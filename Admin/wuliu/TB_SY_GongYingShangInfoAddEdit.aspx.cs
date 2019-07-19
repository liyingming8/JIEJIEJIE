using System;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Web.UI;

public partial class Admin_TB_SY_GongYingShangInfoAddEdit : AuthorPage
{
    private readonly BTB_SY_GongYingShangInfo bll = new BTB_SY_GongYingShangInfo();
    private MTB_SY_GongYingShangInfo mod = new MTB_SY_GongYingShangInfo();
    private readonly BTB_SY_GongYingShangTypeInfo mgyslb = new BTB_SY_GongYingShangTypeInfo();

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
            BindDDL();
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

    private void BindDDL()
    {
        DropDownList_leibie.DataSource = mgyslb.GetListsByFilterString("CompID=" + GetCookieCompID());
        DropDownList_leibie.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }

        mod.GYSType = Convert.ToInt32(DropDownList_leibie.SelectedValue);
        mod.GYSMingCheng = inputGYSMingCheng.Value.Trim();
        mod.GYSLianXiRen = inputGYSLianXiRen.Value.Trim();
        mod.GYSAddress = inputGYSAddress.Value.Trim();
        mod.GYSPhone = inputGYSPhone.Value.Trim();
        mod.Compid = int.Parse(GetCookieCompID());
        mod.GYSCreateTime = DateTime.Now;

        switch (HF_CMD.Value.Trim())
        {
            case "add":

                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
       // Response.Write("<script>alert('操作成功！');</script>");
       //ClientScript.RegisterStartupScript(GetType(), "reload", "window.opener.location.reload();window.close();", true);
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void fillinput(int id)
    {
        MTB_SY_GongYingShangInfo ms = bll.GetList(id);

        DropDownList_leibie.SelectedValue = ms.GYSType.ToString().Trim();
        inputGYSMingCheng.Value = ms.GYSMingCheng.Trim();
        inputGYSLianXiRen.Value = ms.GYSLianXiRen.Trim();
        inputGYSAddress.Value = ms.GYSAddress.Trim();
        inputGYSPhone.Value = ms.GYSPhone.Trim();
    }
}