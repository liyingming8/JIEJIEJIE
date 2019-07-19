using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TB_Products_TypeAddEditjg : AuthorPage
{
    private readonly BTB_Products_Type bll = new BTB_Products_Type();
    private MTB_Products_Type mod = new MTB_Products_Type();
    private readonly CommonFunWL comfun = new CommonFunWL();

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
            comfun.BindTreeCombox(ComboBox_ParentID, "TypeName", "TypeId", "ParentID", "TB_Products_Type", 0, "产品类型...",
                true, "-", "CompID=" + GetCookieCompID());
            ComboBox_ParentID.SelectedValue = "0";
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
        mod.TypeCode = inputTypeCode.Value.Trim();
        mod.TypeName = inputTypeName.Value.Trim();
        mod.ParentID = Convert.ToInt32(ComboBox_ParentID.SelectedValue);
        mod.CompID = Convert.ToInt32(GetCookieCompID());
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
        MTB_Products_Type ms = bll.GetList(id);
        inputTypeCode.Value = ms.TypeCode.Trim();
        inputTypeName.Value = ms.TypeName.Trim();
        ComboBox_ParentID.SelectedValue = ms.ParentID.ToString().Trim();
    }
}