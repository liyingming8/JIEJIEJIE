using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_EffectiveQureyRecordInfoAddEdit : AuthorPage
{
    private readonly BTJ_EffectiveQureyRecordInfo bll = new BTJ_EffectiveQureyRecordInfo();
    private readonly MTJ_EffectiveQureyRecordInfo mod = new MTJ_EffectiveQureyRecordInfo();

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
        mod.QRID = Convert.ToInt64(inputQRID.Value.Trim());
        mod.UserID = Convert.ToInt32(inputUserID.Value.Trim());
        mod.CompID = Convert.ToInt32(inputCompID.Value.Trim());
        mod.GoodsID = Convert.ToInt32(inputGoodsID.Value.Trim());
        mod.QueryMethod = Convert.ToInt32(inputQueryMethod.Value.Trim());
        mod.QueryDate = Convert.ToDateTime(inputQueryDate.Value.Trim());
        mod.QueryPlace = inputQueryPlace.Value.Trim();
        mod.QueryResult = inputQueryResult.Value.Trim();
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
        Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTJ_EffectiveQureyRecordInfo ms = bll.GetList(id);
        inputQRID.Value = ms.QRID.ToString().Trim();
        inputUserID.Value = ms.UserID.ToString().Trim();
        inputCompID.Value = ms.CompID.ToString().Trim();
        inputGoodsID.Value = ms.GoodsID.ToString().Trim();
        inputQueryMethod.Value = ms.QueryMethod.ToString().Trim();
        inputQueryDate.Value = ms.QueryDate.ToString().Trim();
        inputQueryPlace.Value = ms.QueryPlace.Trim();
        inputQueryResult.Value = ms.QueryResult.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }
}