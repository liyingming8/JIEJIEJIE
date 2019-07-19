using commonlib;
using System;
using System.Web.UI;
using TJ.BLL;
using TJ.Model;
public partial class Admin_TJ_Activity_PrizesAddEdit : AuthorPage
{
    BTJ_Activity_Prizes bll = new BTJ_Activity_Prizes();
    MTJ_Activity_Prizes mod = new MTJ_Activity_Prizes();
    BTJ_AwardType btjAwardType = new BTJ_AwardType();
    BTJ_AwardInfo btjAward = new BTJ_AwardInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["acid"]))
            {
                hf_acid.Value = Sc.DecryptQueryString(Request.QueryString["acid"]);
                if (!string.IsNullOrEmpty(Request.QueryString["cmd"]))
                {
                    HF_CMD.Value = Sc.DecryptQueryString(Request.QueryString["cmd"].Trim());
                }
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    HF_ID.Value = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
                }
                FILLDDL();
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
                ShowOrHideRow(ddl_awtype.SelectedValue);
            }
            else
            {
                Response.End();
            } 
        }
    }

    private void FILLDDL()
    {
        ddl_awtype.DataSource = btjAwardType.GetLists();
        ddl_awtype.DataBind();
    }

    private void FillAwinfo(string awtype)
    {
        rbl_awid.DataSource = btjAward.GetListsByFilterString("AwardType=" + awtype + " and compid=" + GetCookieCompID());
        rbl_awid.DataBind();
    }

    private void ShowOrHideRow(string awtype)
    {
        switch (awtype)
        {
            case "1":
                wxhbrow.Visible = true;
                jiangpinrow.Visible = false;
                jifenrow.Visible = false;
                break;
            case "2":
                wxhbrow.Visible = false;
                jiangpinrow.Visible = false;
                jifenrow.Visible = true;
                break;
            case "3":
                wxhbrow.Visible = false;
                jiangpinrow.Visible = true;
                jifenrow.Visible = false;
                break;
            case "4":
                wxhbrow.Visible = false;
                jiangpinrow.Visible = true;
                jifenrow.Visible = false;
                break;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        } 
        mod.awtype = Convert.ToInt32(ddl_awtype.SelectedValue); 
        mod.awtypenm = ddl_awtype.SelectedItem.Text;
        mod.creatusernm = GetCookieTJUName();
        mod.awid = Convert.ToInt32(string.IsNullOrEmpty(rbl_awid.SelectedValue) ? "0" : rbl_awid.SelectedValue);
        mod.awnm = btjAward.GetList(mod.awid).AwardThing;
        switch (mod.awtype)
        {
            case 1:
                mod.prizevalue = Convert.ToDecimal(string.IsNullOrEmpty(inputwxhbvalue.Value.Trim()) ? "0" : inputwxhbvalue.Value.Trim());
                break;
            case 2:
                mod.prizevalue = Convert.ToDecimal(string.IsNullOrEmpty(inputjifen.Value.Trim()) ? "0" : inputjifen.Value.Trim());
                break;
            case 3:
                mod.prizevalue = 0;
                break;
            case 4:
                mod.prizevalue = 0;
                break;
        } 
        mod.percentvl = Convert.ToInt32(inputpercentvl.Value.Trim());
        mod.remarks = inputremarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.acid = Convert.ToInt32(hf_acid.Value);
                bll.Insert(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_Activity_PrizesAddEdit.aspx", "TJ_Activity_Prizes", "描述", DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                bll.Modify(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_Activity_PrizesAddEdit.aspx", "TJ_Activity_Prizes", "描述", DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void Fillinput(int id)
    {
        MTJ_Activity_Prizes ms = bll.GetList(id);
        inputacid.Value = ms.acid.ToString().Trim();
        ddl_awtype.SelectedValue = ms.awtype.ToString().Trim();
        FillAwinfo(ddl_awtype.SelectedValue);
        if (ms.awid > 0)
        {
            rbl_awid.SelectedValue = ms.awid.ToString().Trim();
        }
        switch (ms.awtype)
        {
            case  1:
                wxhbrow.Visible = true;
                jifenrow.Visible = false;
                inputwxhbvalue.Value = ms.prizevalue.ToString().Trim();
                inputjifen.Value = "0";
                break;
            case 2:
                wxhbrow.Visible = false;
                jifenrow.Visible = true;
                inputjifen.Value = ms.prizevalue.ToString("0");
                inputwxhbvalue.Value = "0";
                break;
            case 3:
                wxhbrow.Visible = false;
                jifenrow.Visible = false;
                inputjifen.Value = "0";
                inputwxhbvalue.Value = "0";
                break;
            case 4:
                wxhbrow.Visible = false;
                jifenrow.Visible = false;
                inputjifen.Value = "0";
                inputwxhbvalue.Value = "0";
                break;
        }  
        inputpercentvl.Value = ms.percentvl.ToString().Trim();
        inputremarks.Value = ms.remarks.Trim();
    }
    protected void ddl_awtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillAwinfo(ddl_awtype.SelectedValue);
        ShowOrHideRow(ddl_awtype.SelectedValue);
    }
}