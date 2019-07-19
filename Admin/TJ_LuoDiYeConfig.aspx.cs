using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_TJ_LuoDiYeConfig : AuthorPage
{
    private readonly BTJ_LuoDiYeConfig bll = new BTJ_LuoDiYeConfig();
    private MTJ_LuoDiYeConfig mod = new MTJ_LuoDiYeConfig();
    public BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();
    private readonly BTB_Products_Infor bproduct = new BTB_Products_Infor();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgridview();
        }
    }

    public string GetProductName(string ProdID)
    {
        if (!ProdID.Equals("0") && !ProdID.Equals("") && !ProdID.Equals(null))
        {
            return bproduct.GetList(int.Parse(ProdID)).Products_Name;
        }
        else
        {
            return "所有产品";
        }
    }

    private void fillgridview()
    {
        GridView1.DataSource = bll.GetListsByFilterString(ReturnSQLFilterString());
        GridView1.DataBind();
    }

    private string tempfilterstring = "";

    private string ReturnSQLFilterString()
    {
        tempfilterstring = "1=1";
        if (!IsSuperAdmin())
        {
            tempfilterstring += " and CompID=" + GetCookieCompID();
        }
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            tempfilterstring += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
        }
        return tempfilterstring;
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["LDYCFGID"].ToString()));
        fillgridview();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgridview();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgridview();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["LDYCFGID"].ToString()));
        mod.CompID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCompID")).Text.Trim());
        mod.CSID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtCSID")).Text.Trim());
        mod.WLProID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtWLProID")).Text.Trim());
        mod.LimitedCheckNum =
            Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtLimitedCheckNum")).Text.Trim());
        mod.AlertContents = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtAlertContents")).Text.Trim();
        mod.ShowTopAd = Convert.ToBoolean(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtShowTopAd")).Text.Trim());
        mod.ShowSuYuan =
            Convert.ToBoolean(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtShowSuYuan")).Text.Trim());
        mod.ShowModules =
            Convert.ToBoolean(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtShowModules")).Text.Trim());
        mod.ShowTraceInfo =
            Convert.ToBoolean(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtShowTraceInfo")).Text.Trim());
        mod.Remarks = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtRemarks")).Text.Trim();
        bll.Modify(mod);
        GridView1.EditIndex = -1;
        fillgridview();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillgridview();
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            //双击后编辑修改方法
            return string.Format("javascript:var win=openWinCenter('TJ_LuoDiYeConfigAddEdit.aspx?cmd=edit&ID={0}',680,700,' 落地页编辑')", ID);
        }
        else
        {
            return "";
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton) e.Row.Cells[10].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')");
            }
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview();
    }
}