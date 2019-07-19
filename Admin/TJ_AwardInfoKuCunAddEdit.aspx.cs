using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_AwardInfoKuCunAddEdit : AuthorPage
{
    private readonly BTJ_AwardInfo bll = new BTJ_AwardInfo();
    //private readonly BTJ_Integral bintegral = new BTJ_Integral();
    private MTJ_AwardInfo mod = new MTJ_AwardInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim();
                Fillinput(int.Parse(HF_ID.Value.Trim()));
            }
            else
            {
                Response.End();
            }
        }
    } 

    protected void Button1_Click(object sender, EventArgs e)
    {
        mod = bll.GetList(int.Parse(HF_ID.Value));
        mod.kucunshuliang = int.Parse(input_kucunliang.Value);
        bll.Modify(mod); 
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void Fillinput(int id)
    {
        MTJ_AwardInfo ms = bll.GetList(id);
        //Comb_AGID.SelectedValue = ms.AGID.ToString().Trim();
        Label_award_name.Text = ms.AwardThing;
        input_kucunliang.Value = ms.kucunshuliang.ToString();
    }
}