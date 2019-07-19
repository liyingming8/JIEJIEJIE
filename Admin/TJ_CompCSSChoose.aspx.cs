using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_CompCSSChoose : AuthorPage
{
    private readonly BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();
    private readonly BTJ_CSSInfo bll = new BTJ_CSSInfo();
    private MTJ_CSSInfo mod = new MTJ_CSSInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillRadioList();
            string csspath = bcompany.GetList(int.Parse(GetCookieCompID())).CSSPath;
            if (csspath.Trim().Length > 0)
            {
                fillinput(bll.GetListsByFilterString("CSID='" + csspath + "'")[0].CSID);
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        MTJ_RegisterCompanys mcompany = bcompany.GetList(int.Parse(GetCookieCompID()));
        foreach (ListItem item in RadioButtonListCSS.Items)
        {
            if (item.Selected)
            {
                MTJ_CSSInfo mcss = bll.GetList(int.Parse(item.Value));
                mcompany.CSSPath = mcss.CSID.ToString();
                mcompany.LogoDirInfo = mcss.LogoDirInfo;
            }
        }
        bcompany.Modify(mcompany);
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "into", "location.href='/admin/TJ_CompCSSChoose.aspx'",
            true);
    }

    private void FillRadioList()
    {
        RadioButtonListCSS.DataSource = bll.GetLists();
        RadioButtonListCSS.DataBind();
    }

    private void fillinput(int id)
    {
        MTJ_CSSInfo ms = bll.GetList(id);
        foreach (ListItem item in RadioButtonListCSS.Items)
        {
            if (item.Value == ms.CSID.ToString().Trim())
            {
                item.Selected = true;
                ImageShow.ImageUrl = bll.GetList(int.Parse(item.Value)).PicURL;
            }
        }
    }

    protected void RadioButtonListCSS_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImageShow.ImageUrl = bll.GetList(int.Parse(RadioButtonListCSS.SelectedValue)).PicURL;
    }
}