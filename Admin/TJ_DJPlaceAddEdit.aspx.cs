using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_DJPlaceAddEdit : AuthorPage
{
    private readonly BTJ_DJPlace bll = new BTJ_DJPlace();
    private MTJ_DJPlace mod = new MTJ_DJPlace();
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
        //mod.DjdID = Convert.ToInt32(inputDjdID.Value.Trim());
        mod.DjdName = inputDjdName.Value.Trim();
        mod.DjZH = inputDjZH.Value.Trim();
        mod.YanZM = inputYanZM.Value.Trim();
        mod.JxName = inputJxName.Value.Trim();
        mod.JxCount = Convert.ToInt32(inputJxCount.Value.Trim());
        mod.LXname = inputLXname.Value.Trim();
        mod.MPhone = inputMPhone.Value.Trim();
        mod.Address = inputAddress.Value.Trim();
        mod.DjGrade = inputDjGrade.Value.Trim();
        mod.DelFlag = inputDelFlag.Value.Trim();
        mod.PlaceID = Convert.ToInt32(Comb_PlaceID.SelectedValue);
        mod.CompID = int.Parse(GetCookieCompID());
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
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "location.href='TJ_DJPlace.aspx'", true);
    }

    private void fillinput(int id)
    {
        MTJ_DJPlace ms = bll.GetList(id);
        //inputDjdID.Value = ms.DjdID.ToString().Trim();
        inputDjdName.Value = ms.DjdName.Trim();
        inputDjZH.Value = ms.DjZH.Trim();
        inputYanZM.Value = ms.YanZM.Trim();
        inputJxName.Value = ms.JxName.Trim();
        inputJxCount.Value = ms.JxCount.ToString().Trim();
        inputLXname.Value = ms.LXname.Trim();
        inputMPhone.Value = ms.MPhone.Trim();
        inputAddress.Value = ms.Address.Trim();
        inputDjGrade.Value = ms.DjGrade.Trim();
        inputDelFlag.Value = ms.DelFlag.Trim();
        Comb_PlaceID.SelectedValue = ms.PlaceID.ToString().Trim();
        ////inputCompID.Value = ms.CompID.ToString().Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }

    private void FillDDL()
    {
        comfun.BindTreeCombox(Comb_PlaceID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "请选择城市...", true,
            "-", "");
        Comb_PlaceID.SelectedValue = "0";
    }
}