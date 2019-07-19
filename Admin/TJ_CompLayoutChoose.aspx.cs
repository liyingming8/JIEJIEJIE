using System;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TJ_CompLayoutChoose : AuthorPage
{
    private readonly BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();
    private readonly BTJ_CSSInfo bll = new BTJ_CSSInfo();
    private MTJ_CSSInfo mod = new MTJ_CSSInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string csspath = bcompany.GetList(int.Parse(GetCookieCompID())).CSSPath;
            if (csspath.Trim().Length > 0)
            {
                fillinput(bll.GetListsByFilterString("FileNamePath='" + csspath + "'")[0].CSID);
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }

        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        Response.Write("<script>location.href='TJ_CompLayoutChoose.aspx'</script>");
    }

    private void fillinput(int id)
    {
        MTJ_CSSInfo ms = bll.GetList(id);
        foreach (DataListItem item in DataList_CSS.Items)
        {
            if (((HiddenField) item.FindControl("HF_CSID")).Value == ms.CSID.ToString().Trim())
            {
                ((RadioButton) item.FindControl("RB_Choose")).Checked = true;
            }
        }
    }
}