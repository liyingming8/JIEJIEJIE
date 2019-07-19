using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TB_LabelCodeInfoAddEdit : AuthorPage
{
    private readonly BTB_LabelCodeInfo bll = new BTB_LabelCodeInfo();
    private MTB_LabelCodeInfo mod = new MTB_LabelCodeInfo();

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
        mod.ID = Convert.ToInt32(inputID.Value.Trim());
        mod.startfournum = inputstartfournum.Value.Trim();
        mod.tablenameinfo = inputtablenameinfo.Value.Trim();
        mod.startvalue = inputstartvalue.Value.Trim();
        mod.endvalue = inputendvalue.Value.Trim();
        mod.taozongshu = Convert.ToInt32(inputtaozongshu.Value.Trim());
        mod.xiangnum = Convert.ToInt32(inputxiangnum.Value.Trim());
        mod.pingnum = Convert.ToInt32(inputpingnum.Value.Trim());
        mod.pianyiliang = Convert.ToInt32(inputpianyiliang.Value.Trim());
        mod.rowcout = Convert.ToInt64(inputrowcout.Value.Trim());
        mod.remarks = inputremarks.Value.Trim();
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
        MTB_LabelCodeInfo ms = bll.GetList(id);
        inputID.Value = ms.ID.ToString().Trim();
        inputstartfournum.Value = ms.startfournum.Trim();
        inputtablenameinfo.Value = ms.tablenameinfo.Trim();
        inputstartvalue.Value = ms.startvalue.Trim();
        inputendvalue.Value = ms.endvalue.Trim();
        inputtaozongshu.Value = ms.taozongshu.ToString().Trim();
        inputxiangnum.Value = ms.xiangnum.ToString().Trim();
        inputpingnum.Value = ms.pingnum.ToString().Trim();
        inputpianyiliang.Value = ms.pianyiliang.ToString().Trim();
        inputrowcout.Value = ms.rowcout.ToString().Trim();
        inputremarks.Value = ms.remarks.Trim();
        inputCompID.Value = ms.CompID.ToString().Trim();
    }
}