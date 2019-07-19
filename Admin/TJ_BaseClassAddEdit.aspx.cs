using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_BaseClassAddEdit : AuthorPage
{
    private readonly BTJ_BaseClass bll = new BTJ_BaseClass();
    private MTJ_BaseClass mod = new MTJ_BaseClass();
    private readonly CommonFun comfun = new CommonFun();

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
            if (Request.QueryString["ParentID"] != null && Request.QueryString["ParentID"] != "")
            {
                HF_ParentID.Value = Request.QueryString["ParentID"];
            }
            FillCombox(HF_ParentID.Value.Trim().Length.Equals(0) ? 0 : int.Parse(HF_ParentID.Value.Trim()));
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    break;
                case "edit":
                    Button1.Text = "修改";
                    Fillinput(int.Parse(HF_ID.Value.Trim()));
                    break;
                default:
                    break;
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.ToLower().Trim().Equals("edit"))
        {
            mod = bll.GetList(int.Parse(HF_ID.Value));
        }
        if (IsSuperAdmin())
        {
            mod.CompID = 0;
        }
        else
        {
            mod.CompID = int.Parse(GetCookieCompID());
        }
        mod.CID = HF_ID.Value.Trim().Equals("") ? 0 : int.Parse(HF_ID.Value.Trim());
        mod.ParentID = int.Parse(ComboBox_ParentID.SelectedValue);
        mod.CName = inputCName.Value.Trim();
        mod.ShowOrder = Convert.ToInt32(inputShowOrder.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                if (
                    !bll.CheckIsExistByFilterString("CName='" + inputCName.Value.Trim() + "' and CompID=" +
                                                    GetCookieCompID()))
                {
                    bll.Insert(mod);
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "Info",
                    //    "location.href='TJ_BaseClass.aspx'", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "Info", "alert('该类型已经存在！')", true);
                }

                break;
            case "edit":
                if (
                    !bll.CheckIsExistByFilterString("CName='" + inputCName.Value.Trim() + "' and CompID=" +
                                                    GetCookieCompID() + " and CID<>" + HF_ID.Value))
                {
                    bll.Modify(mod);
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "Info",
                    //    "location.href='TJ_BaseClass.aspx'", true);
                   ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "Info", "alert('该类型已经存在！')", true);
                }
                break;
        }
    }

    private void Fillinput(int id)
    {
        MTJ_BaseClass ms = bll.GetList(id);
        ComboBox_ParentID.SelectedValue = ms.ParentID.ToString().Trim();
        inputCName.Value = ms.CName.Trim();
        inputShowOrder.Value = ms.ShowOrder.ToString().Trim();
    }

    private void FillCombox(int ParentID)
    {
        comfun.BindTreeCombox(ComboBox_ParentID, "CName", "CID", "ParentID", "TJ_BaseClass", ParentID, "请选择...", true,
            "—", "");
        ComboBox_ParentID.SelectedValue = "0";
    }
}