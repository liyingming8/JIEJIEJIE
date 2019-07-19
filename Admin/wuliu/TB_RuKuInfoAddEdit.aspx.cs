using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TB_RuKuInfoAddEdit : AuthorPage
{
    private readonly BTB_RuKuInfo bll = new BTB_RuKuInfo();
    private MTB_RuKuInfo mod = new MTB_RuKuInfo();

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
        mod.RKID = Convert.ToInt32(inputRKID.Value.Trim());
        mod.RKPiCi = inputRKPiCi.Value.Trim();
        mod.RKDate = Convert.ToDateTime(inputRKDate.Value.Trim());
        mod.StoreHouseID = Convert.ToInt32(inputStoreHouseID.Value.Trim());
        mod.RuKuUserID = Convert.ToInt32(inputRuKuUserID.Value.Trim());
        mod.TableNameInfo = inputTableNameInfo.Value.Trim();
        mod.RKKey = inputRKKey.Value.Trim();
        mod.Remarks = inputRemarks.Value.Trim();
        mod.CompID = Convert.ToInt32(inputCompID.Value.Trim());
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
        MTB_RuKuInfo ms = bll.GetList(id);
        inputRKID.Value = ms.RKID.ToString().Trim();
        inputRKPiCi.Value = ms.RKPiCi.Trim();
        inputRKDate.Value = ms.RKDate.ToString().Trim();
        inputStoreHouseID.Value = ms.StoreHouseID.ToString().Trim();
        inputRuKuUserID.Value = ms.RuKuUserID.ToString().Trim();
        inputTableNameInfo.Value = ms.TableNameInfo.Trim();
        inputRKKey.Value = ms.RKKey.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        inputCompID.Value = ms.CompID.ToString().Trim();
    }
}